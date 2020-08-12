using System;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x02000078 RID: 120
	internal class HookResult : IDisposable
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00003839 File Offset: 0x00001A39
		public HookResult(HookProcedureHandle handle, HookProcedure procedure)
		{
			this.m_Handle = handle;
			this.m_Procedure = procedure;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000384F File Offset: 0x00001A4F
		public HookProcedureHandle Handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00003857 File Offset: 0x00001A57
		public HookProcedure Procedure
		{
			get
			{
				return this.m_Procedure;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000385F File Offset: 0x00001A5F
		public void Dispose()
		{
			this.m_Handle.Dispose();
		}

		// Token: 0x04000146 RID: 326
		private readonly HookProcedureHandle m_Handle;

		// Token: 0x04000147 RID: 327
		private readonly HookProcedure m_Procedure;
	}
}
