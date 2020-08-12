using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using xClient.Core.Compression;

namespace xClient.Core.Utilities
{
	// Token: 0x0200004D RID: 77
	public class UnsafeStreamCodec : IDisposable
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00002EB8 File Offset: 0x000010B8
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00002EC0 File Offset: 0x000010C0
		public int Monitor { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00002EC9 File Offset: 0x000010C9
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00002ED1 File Offset: 0x000010D1
		public string Resolution { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00002EDA File Offset: 0x000010DA
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00002EE2 File Offset: 0x000010E2
		public Size CheckBlock { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00002EEB File Offset: 0x000010EB
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000CFB4 File Offset: 0x0000B1B4
		public int ImageQuality
		{
			get
			{
				return this._imageQuality;
			}
			private set
			{
				lock (this._imageProcessLock)
				{
					this._imageQuality = value;
					if (this._jpgCompression != null)
					{
						this._jpgCompression.Dispose();
					}
					this._jpgCompression = new JpgCompression((long)this._imageQuality);
				}
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00002EF3 File Offset: 0x000010F3
		public UnsafeStreamCodec(int imageQuality, int monitor, string resolution)
		{
			this.ImageQuality = imageQuality;
			this.Monitor = monitor;
			this.Resolution = resolution;
			this.CheckBlock = new Size(50, 1);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00002F29 File Offset: 0x00001129
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00002F38 File Offset: 0x00001138
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._decodedBitmap != null)
				{
					this._decodedBitmap.Dispose();
				}
				if (this._jpgCompression != null)
				{
					this._jpgCompression.Dispose();
				}
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000D01C File Offset: 0x0000B21C
		public unsafe void CodeImage(IntPtr scan0, Rectangle scanArea, Size imageSize, PixelFormat format, Stream outStream)
		{
			lock (this._imageProcessLock)
			{
				byte* ptr = scan0.ToInt32();
				if (!outStream.CanWrite)
				{
					throw new Exception("Must have access to Write in the Stream");
				}
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				if (format <= PixelFormat.Format32bppRgb)
				{
					if (format == PixelFormat.Format24bppRgb || format == PixelFormat.Format32bppRgb)
					{
						num3 = 3;
						goto IL_81;
					}
				}
				else if (format == PixelFormat.Format32bppPArgb || format == PixelFormat.Format32bppArgb)
				{
					num3 = 4;
					goto IL_81;
				}
				throw new NotSupportedException(format.ToString());
				IL_81:
				num = imageSize.Width * num3;
				num2 = num * imageSize.Height;
				if (this._encodeBuffer == null)
				{
					this._encodedFormat = format;
					this._encodedWidth = imageSize.Width;
					this._encodedHeight = imageSize.Height;
					this._encodeBuffer = new byte[num2];
					byte[] encodeBuffer;
					byte* ptr2;
					if ((encodeBuffer = this._encodeBuffer) != null && encodeBuffer.Length != 0)
					{
						fixed (byte* ptr2 = &encodeBuffer[0])
						{
						}
					}
					else
					{
						ptr2 = null;
					}
					byte[] array = null;
					using (Bitmap bitmap = new Bitmap(imageSize.Width, imageSize.Height, num, format, scan0))
					{
						array = this._jpgCompression.Compress(bitmap);
					}
					outStream.Write(BitConverter.GetBytes(array.Length), 0, 4);
					outStream.Write(array, 0, array.Length);
					GClass26.memcpy(new IntPtr((void*)ptr2), scan0, (uint)num2);
					ptr2 = null;
				}
				else
				{
					if (this._encodedFormat != format)
					{
						throw new Exception("PixelFormat is not equal to previous Bitmap");
					}
					if (this._encodedWidth == imageSize.Width)
					{
						if (this._encodedHeight == imageSize.Height)
						{
							long position = outStream.Position;
							outStream.Write(new byte[4], 0, 4);
							long num4 = 0L;
							List<Rectangle> list = new List<Rectangle>();
							Size size = new Size(scanArea.Width, this.CheckBlock.Height);
							Size size2 = new Size(scanArea.Width % this.CheckBlock.Width, scanArea.Height % this.CheckBlock.Height);
							int num5 = scanArea.Height - size2.Height;
							int num6 = scanArea.Width - size2.Width;
							Rectangle rectangle = default(Rectangle);
							List<Rectangle> list2 = new List<Rectangle>();
							size = new Size(scanArea.Width, size.Height);
							byte[] encodeBuffer2;
							byte* ptr3;
							if ((encodeBuffer2 = this._encodeBuffer) != null && encodeBuffer2.Length != 0)
							{
								fixed (byte* ptr3 = &encodeBuffer2[0])
								{
								}
							}
							else
							{
								ptr3 = null;
							}
							for (int num7 = scanArea.Y; num7 != scanArea.Height; num7 += size.Height)
							{
								if (num7 == num5)
								{
									size = new Size(scanArea.Width, size2.Height);
								}
								rectangle = new Rectangle(scanArea.X, num7, scanArea.Width, size.Height);
								int num8 = num7 * num + scanArea.X * num3;
								if (GClass26.memcmp(ptr3 + num8, ptr + num8, (uint)num) != 0)
								{
									int index = list.Count - 1;
									if (list.Count != 0 && list[index].Y + list[index].Height == rectangle.Y)
									{
										rectangle = new Rectangle(list[index].X, list[index].Y, list[index].Width, list[index].Height + rectangle.Height);
										list[index] = rectangle;
									}
									else
									{
										list.Add(rectangle);
									}
								}
							}
							for (int i = 0; i < list.Count; i++)
							{
								size = new Size(this.CheckBlock.Width, list[i].Height);
								for (int num9 = scanArea.X; num9 != scanArea.Width; num9 += size.Width)
								{
									if (num9 == num6)
									{
										size = new Size(size2.Width, list[i].Height);
									}
									rectangle = new Rectangle(num9, list[i].Y, size.Width, list[i].Height);
									bool flag2 = false;
									uint count = (uint)(num3 * rectangle.Width);
									for (int j = 0; j < rectangle.Height; j++)
									{
										int num10 = num * (rectangle.Y + j) + num3 * rectangle.X;
										if (GClass26.memcmp(ptr3 + num10, ptr + num10, count) != 0)
										{
											flag2 = true;
										}
										GClass26.memcpy_1((void*)((byte*)ptr3 + num10), (void*)(ptr + num10), count);
									}
									if (flag2)
									{
										int index = list2.Count - 1;
										if (list2.Count > 0 && list2[index].X + list2[index].Width == rectangle.X)
										{
											Rectangle rectangle2 = list2[index];
											int width = rectangle.Width + rectangle2.Width;
											rectangle = new Rectangle(rectangle2.X, rectangle2.Y, width, rectangle2.Height);
											list2[index] = rectangle;
										}
										else
										{
											list2.Add(rectangle);
										}
									}
								}
							}
							ptr3 = null;
							for (int k = 0; k < list2.Count; k++)
							{
								Rectangle rectangle3 = list2[k];
								int num11 = num3 * rectangle3.Width;
								Bitmap bitmap2 = null;
								BitmapData bitmapData = null;
								long num14;
								try
								{
									bitmap2 = new Bitmap(rectangle3.Width, rectangle3.Height, format);
									bitmapData = bitmap2.LockBits(new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), ImageLockMode.ReadWrite, bitmap2.PixelFormat);
									int l = 0;
									int num12 = 0;
									while (l < rectangle3.Height)
									{
										int num13 = num * (rectangle3.Y + l) + num3 * rectangle3.X;
										GClass26.memcpy_1((void*)((byte*)bitmapData.Scan0.ToPointer() + num12), (void*)(ptr + num13), (uint)num11);
										num12 += num11;
										l++;
									}
									outStream.Write(BitConverter.GetBytes(rectangle3.X), 0, 4);
									outStream.Write(BitConverter.GetBytes(rectangle3.Y), 0, 4);
									outStream.Write(BitConverter.GetBytes(rectangle3.Width), 0, 4);
									outStream.Write(BitConverter.GetBytes(rectangle3.Height), 0, 4);
									outStream.Write(new byte[4], 0, 4);
									num14 = outStream.Length;
									long position2 = outStream.Position;
									this._jpgCompression.Compress(bitmap2, ref outStream);
									num14 = outStream.Position - num14;
									outStream.Position = position2 - 4L;
									outStream.Write(BitConverter.GetBytes(num14), 0, 4);
									outStream.Position += num14;
								}
								finally
								{
									bitmap2.UnlockBits(bitmapData);
									bitmap2.Dispose();
								}
								num4 += num14 + 20L;
							}
							outStream.Position = position;
							outStream.Write(BitConverter.GetBytes(num4), 0, 4);
							return;
						}
					}
					throw new Exception("Bitmap width/height are not equal to previous bitmap");
				}
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		public unsafe Bitmap DecodeData(IntPtr codecBuffer, uint length)
		{
			if (length < 4U)
			{
				return this._decodedBitmap;
			}
			int num = *(int*)((void*)codecBuffer);
			if (this._decodedBitmap == null)
			{
				byte[] array = new byte[num];
				byte[] array2;
				byte* ptr;
				if ((array2 = array) != null && array2.Length != 0)
				{
					fixed (byte* ptr = &array2[0])
					{
					}
				}
				else
				{
					ptr = null;
				}
				GClass26.memcpy(new IntPtr((void*)ptr), new IntPtr(codecBuffer.ToInt32() + 4), (uint)num);
				ptr = null;
				this._decodedBitmap = (Bitmap)Image.FromStream(new MemoryStream(array));
				return this._decodedBitmap;
			}
			return this._decodedBitmap;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000D868 File Offset: 0x0000BA68
		public Bitmap DecodeData(Stream inStream)
		{
			byte[] array = new byte[4];
			inStream.Read(array, 0, 4);
			int i = BitConverter.ToInt32(array, 0);
			if (this._decodedBitmap == null)
			{
				array = new byte[i];
				inStream.Read(array, 0, array.Length);
				this._decodedBitmap = (Bitmap)Image.FromStream(new MemoryStream(array));
				return this._decodedBitmap;
			}
			using (Graphics graphics = Graphics.FromImage(this._decodedBitmap))
			{
				while (i > 0)
				{
					byte[] array2 = new byte[20];
					inStream.Read(array2, 0, array2.Length);
					Rectangle rectangle = new Rectangle(BitConverter.ToInt32(array2, 0), BitConverter.ToInt32(array2, 4), BitConverter.ToInt32(array2, 8), BitConverter.ToInt32(array2, 12));
					int num = BitConverter.ToInt32(array2, 16);
					byte[] array3 = new byte[num];
					inStream.Read(array3, 0, array3.Length);
					using (MemoryStream memoryStream = new MemoryStream(array3))
					{
						using (Bitmap bitmap = (Bitmap)Image.FromStream(memoryStream))
						{
							graphics.DrawImage(bitmap, rectangle.Location);
						}
					}
					i -= num + 20;
				}
			}
			return this._decodedBitmap;
		}

		// Token: 0x040000DB RID: 219
		private int _imageQuality;

		// Token: 0x040000DC RID: 220
		private byte[] _encodeBuffer;

		// Token: 0x040000DD RID: 221
		private Bitmap _decodedBitmap;

		// Token: 0x040000DE RID: 222
		private PixelFormat _encodedFormat;

		// Token: 0x040000DF RID: 223
		private int _encodedWidth;

		// Token: 0x040000E0 RID: 224
		private int _encodedHeight;

		// Token: 0x040000E1 RID: 225
		private readonly object _imageProcessLock = new object();

		// Token: 0x040000E2 RID: 226
		private JpgCompression _jpgCompression;
	}
}
