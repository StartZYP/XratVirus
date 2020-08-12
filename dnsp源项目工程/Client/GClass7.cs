using System;
using System.Drawing;
using System.Runtime.InteropServices;

// Token: 0x02000009 RID: 9
public static class GClass7
{
	// Token: 0x06000047 RID: 71 RVA: 0x00005288 File Offset: 0x00003488
	public static uint smethod_0()
	{
		GClass26.GStruct0 gstruct = default(GClass26.GStruct0);
		gstruct.uint_0 = (uint)Marshal.SizeOf(gstruct);
		gstruct.uint_1 = 0U;
		if (!GClass26.GetLastInputInfo(ref gstruct))
		{
			return 0U;
		}
		return gstruct.uint_1;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000022E8 File Offset: 0x000004E8
	public static void smethod_1(Point p, bool isMouseDown)
	{
		GClass26.mouse_event(isMouseDown ? 2U : 4U, p.X, p.Y, 0, 0);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00002306 File Offset: 0x00000506
	public static void smethod_2(Point p, bool isMouseDown)
	{
		GClass26.mouse_event(isMouseDown ? 8U : 16U, p.X, p.Y, 0, 0);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00002325 File Offset: 0x00000525
	public static void smethod_3(Point p)
	{
		GClass26.SetCursorPos(p.X, p.Y);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x0000233B File Offset: 0x0000053B
	public static void smethod_4(Point p, bool scrollDown)
	{
		GClass26.mouse_event(2048U, p.X, p.Y, scrollDown ? -120 : 120, 0);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x0000235F File Offset: 0x0000055F
	public static void smethod_5(byte key, bool keyDown)
	{
		GClass26.keybd_event(key, 0, keyDown ? 0U : 2U, 0);
	}

	// Token: 0x0400001D RID: 29
	private const uint uint_0 = 2U;

	// Token: 0x0400001E RID: 30
	private const uint uint_1 = 4U;

	// Token: 0x0400001F RID: 31
	private const uint uint_2 = 8U;

	// Token: 0x04000020 RID: 32
	private const uint uint_3 = 16U;

	// Token: 0x04000021 RID: 33
	private const uint uint_4 = 2048U;

	// Token: 0x04000022 RID: 34
	private const uint uint_5 = 0U;

	// Token: 0x04000023 RID: 35
	private const uint uint_6 = 2U;
}
