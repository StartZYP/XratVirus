using System;
using System.Runtime.InteropServices;

// Token: 0x0200004B RID: 75
public static class GClass26
{
	// Token: 0x060001ED RID: 493
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DeleteFile(string name);

	// Token: 0x060001EE RID: 494
	[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
	public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

	// Token: 0x060001EF RID: 495
	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string procName);

	// Token: 0x060001F0 RID: 496
	[DllImport("user32.dll")]
	public static extern bool GetLastInputInfo(ref GClass26.GStruct0 plii);

	// Token: 0x060001F1 RID: 497
	[DllImport("user32.dll")]
	public static extern bool SetCursorPos(int x, int y);

	// Token: 0x060001F2 RID: 498
	[DllImport("user32.dll")]
	public static extern void mouse_event(uint dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

	// Token: 0x060001F3 RID: 499
	[DllImport("user32.dll")]
	public static extern bool keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

	// Token: 0x060001F4 RID: 500
	[DllImport("gdi32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

	// Token: 0x060001F5 RID: 501
	[DllImport("gdi32.dll")]
	public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

	// Token: 0x060001F6 RID: 502
	[DllImport("gdi32.dll")]
	public static extern bool DeleteDC([In] IntPtr hdc);

	// Token: 0x060001F7 RID: 503
	[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
	public unsafe static extern int memcmp(byte* ptr1, byte* ptr2, uint count);

	// Token: 0x060001F8 RID: 504
	[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "memcmp")]
	public static extern int memcmp_1(IntPtr ptr1, IntPtr ptr2, uint count);

	// Token: 0x060001F9 RID: 505
	[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int memcpy(IntPtr dst, IntPtr src, uint count);

	// Token: 0x060001FA RID: 506
	[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "memcpy")]
	public unsafe static extern int memcpy_1(void* dst, void* src, uint count);

	// Token: 0x0200004C RID: 76
	public struct GStruct0
	{
		// Token: 0x040000D8 RID: 216
		public static readonly int int_0 = Marshal.SizeOf(typeof(GClass26.GStruct0));

		// Token: 0x040000D9 RID: 217
		[MarshalAs(UnmanagedType.U4)]
		public uint uint_0;

		// Token: 0x040000DA RID: 218
		[MarshalAs(UnmanagedType.U4)]
		public uint uint_1;
	}
}
