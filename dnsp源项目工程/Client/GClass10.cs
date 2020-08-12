using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

// Token: 0x0200000C RID: 12
public static class GClass10
{
	// Token: 0x06000067 RID: 103 RVA: 0x000055E8 File Offset: 0x000037E8
	public static Bitmap smethod_0(int screenNumber)
	{
		Rectangle rectangle = GClass10.smethod_1(screenNumber);
		Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppPArgb);
		using (Graphics graphics = Graphics.FromImage(bitmap))
		{
			IntPtr hdc = graphics.GetHdc();
			IntPtr intPtr = GClass26.CreateDC("DISPLAY", null, null, IntPtr.Zero);
			GClass26.BitBlt(hdc, 0, 0, rectangle.Width, rectangle.Height, intPtr, rectangle.X, rectangle.Y, 13369376);
			GClass26.DeleteDC(intPtr);
			graphics.ReleaseHdc(hdc);
		}
		return bitmap;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00002416 File Offset: 0x00000616
	public static Rectangle smethod_1(int screenNumber)
	{
		return Screen.AllScreens[screenNumber].Bounds;
	}

	// Token: 0x0400002F RID: 47
	private const int int_0 = 13369376;
}
