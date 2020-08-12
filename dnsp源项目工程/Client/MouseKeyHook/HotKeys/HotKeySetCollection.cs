using System;
using System.Collections.Generic;

namespace xClient.Core.MouseKeyHook.HotKeys
{
	// Token: 0x02000055 RID: 85
	public sealed class HotKeySetCollection : List<HotKeySet>
	{
		// Token: 0x06000231 RID: 561 RVA: 0x000030A9 File Offset: 0x000012A9
		public new void Add(HotKeySet hks)
		{
			this.m_keyChain = (HotKeySetCollection.KeyChainHandler)Delegate.Combine(this.m_keyChain, new HotKeySetCollection.KeyChainHandler(hks.OnKey));
			base.Add(hks);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000030D4 File Offset: 0x000012D4
		public new void Remove(HotKeySet hks)
		{
			this.m_keyChain = (HotKeySetCollection.KeyChainHandler)Delegate.Remove(this.m_keyChain, new HotKeySetCollection.KeyChainHandler(hks.OnKey));
			base.Remove(hks);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00003100 File Offset: 0x00001300
		internal void OnKey(KeyEventArgsExt e)
		{
			if (this.m_keyChain != null)
			{
				this.m_keyChain(e);
			}
		}

		// Token: 0x040000F7 RID: 247
		private HotKeySetCollection.KeyChainHandler m_keyChain;

		// Token: 0x02000056 RID: 86
		// (Invoke) Token: 0x06000236 RID: 566
		private delegate void KeyChainHandler(KeyEventArgsExt kex);
	}
}
