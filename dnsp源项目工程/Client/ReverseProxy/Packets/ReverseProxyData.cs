using System;
using xClient.Core.Networking;
using xClient.Core.Packets;

namespace xClient.Core.ReverseProxy.Packets
{
	// Token: 0x020000D5 RID: 213
	[Serializable]
	public class ReverseProxyData : IPacket
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x000048DA File Offset: 0x00002ADA
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x000048E2 File Offset: 0x00002AE2
		public int ConnectionId { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000048EB File Offset: 0x00002AEB
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x000048F3 File Offset: 0x00002AF3
		public byte[] Data { get; set; }

		// Token: 0x06000555 RID: 1365 RVA: 0x000021D4 File Offset: 0x000003D4
		public ReverseProxyData()
		{
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000048FC File Offset: 0x00002AFC
		public ReverseProxyData(int connectionId, byte[] data)
		{
			this.ConnectionId = connectionId;
			this.Data = data;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00004912 File Offset: 0x00002B12
		public void Execute(Client client)
		{
			client.Send<ReverseProxyData>(this);
		}
	}
}
