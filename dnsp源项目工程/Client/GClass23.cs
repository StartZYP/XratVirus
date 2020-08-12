using System;
using System.IO;
using System.Runtime.CompilerServices;

// Token: 0x02000048 RID: 72
public class GClass23
{
	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060001DB RID: 475 RVA: 0x00002E01 File Offset: 0x00001001
	// (set) Token: 0x060001DC RID: 476 RVA: 0x00002E09 File Offset: 0x00001009
	public string Path { get; private set; }

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060001DD RID: 477 RVA: 0x00002E12 File Offset: 0x00001012
	// (set) Token: 0x060001DE RID: 478 RVA: 0x00002E1A File Offset: 0x0000101A
	public string LastError { get; private set; }

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060001DF RID: 479 RVA: 0x0000CB28 File Offset: 0x0000AD28
	public int MaxBlocks
	{
		get
		{
			if (this.int_1 <= 0)
			{
				if (this.int_1 != -1)
				{
					try
					{
						FileInfo fileInfo = new FileInfo(this.Path);
						if (!fileInfo.Exists)
						{
							throw new FileNotFoundException();
						}
						this.int_1 = (int)Math.Ceiling((double)fileInfo.Length / 65535.0);
					}
					catch (UnauthorizedAccessException)
					{
						this.int_1 = -1;
						this.LastError = "Access denied";
					}
					catch (IOException ex)
					{
						this.int_1 = -1;
						if (ex is FileNotFoundException)
						{
							this.LastError = "File not found";
						}
						if (ex is PathTooLongException)
						{
							this.LastError = "Path is too long";
						}
					}
					return this.int_1;
				}
			}
			return this.int_1;
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00002E23 File Offset: 0x00001023
	public GClass23(string path)
	{
		this.Path = path;
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x00002E3D File Offset: 0x0000103D
	private int method_0(long length)
	{
		if (length >= 65535L)
		{
			return 65535;
		}
		return (int)length;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
	public bool method_1(int blockNumber, out byte[] readBytes)
	{
		try
		{
			if (blockNumber > this.MaxBlocks)
			{
				throw new ArgumentOutOfRangeException();
			}
			lock (this.object_0)
			{
				using (FileStream fileStream = File.OpenRead(this.Path))
				{
					if (blockNumber == 0)
					{
						fileStream.Seek(0L, SeekOrigin.Begin);
						long num = fileStream.Length - fileStream.Position;
						if (num < 0L)
						{
							throw new IOException("negative length");
						}
						readBytes = new byte[this.method_0(num)];
						fileStream.Read(readBytes, 0, readBytes.Length);
					}
					else
					{
						fileStream.Seek((long)(blockNumber * 65535), SeekOrigin.Begin);
						long num2 = fileStream.Length - fileStream.Position;
						if (num2 < 0L)
						{
							throw new IOException("negative length");
						}
						readBytes = new byte[this.method_0(num2)];
						fileStream.Read(readBytes, 0, readBytes.Length);
					}
				}
			}
			return true;
		}
		catch (ArgumentOutOfRangeException)
		{
			readBytes = new byte[0];
			this.LastError = "BlockNumber bigger than MaxBlocks";
		}
		catch (UnauthorizedAccessException)
		{
			readBytes = new byte[0];
			this.LastError = "Access denied";
		}
		catch (IOException ex)
		{
			readBytes = new byte[0];
			if (ex is FileNotFoundException)
			{
				this.LastError = "File not found";
			}
			else if (ex is DirectoryNotFoundException)
			{
				this.LastError = "Directory not found";
			}
			else if (ex is PathTooLongException)
			{
				this.LastError = "Path is too long";
			}
			else
			{
				this.LastError = "Unable to read from File Stream";
			}
		}
		return false;
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
	public bool method_2(byte[] block, int blockNumber)
	{
		try
		{
			if (!File.Exists(this.Path) && blockNumber > 0)
			{
				throw new FileNotFoundException();
			}
			lock (this.object_0)
			{
				if (blockNumber == 0)
				{
					using (FileStream fileStream = File.Open(this.Path, FileMode.Create, FileAccess.Write))
					{
						fileStream.Seek(0L, SeekOrigin.Begin);
						fileStream.Write(block, 0, block.Length);
					}
					return true;
				}
				using (FileStream fileStream2 = File.Open(this.Path, FileMode.Append, FileAccess.Write))
				{
					fileStream2.Seek((long)(blockNumber * 65535), SeekOrigin.Begin);
					fileStream2.Write(block, 0, block.Length);
				}
			}
			return true;
		}
		catch (UnauthorizedAccessException)
		{
			this.LastError = "Access denied";
		}
		catch (IOException ex)
		{
			if (ex is FileNotFoundException)
			{
				this.LastError = "File not found";
			}
			else if (ex is DirectoryNotFoundException)
			{
				this.LastError = "Directory not found";
			}
			else if (ex is PathTooLongException)
			{
				this.LastError = "Path is too long";
			}
			else
			{
				this.LastError = "Unable to write to File Stream";
			}
		}
		return false;
	}

	// Token: 0x040000D0 RID: 208
	private const int int_0 = 65535;

	// Token: 0x040000D1 RID: 209
	private int int_1;

	// Token: 0x040000D2 RID: 210
	private readonly object object_0 = new object();

	// Token: 0x040000D3 RID: 211
	[CompilerGenerated]
	private string string_0;

	// Token: 0x040000D4 RID: 212
	[CompilerGenerated]
	private string string_1;
}
