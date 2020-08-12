using System;
using System.Net;
using xClient.Core.Networking;
using xClient.Core.Packets;

namespace xClient.Core.ReverseProxy.Packets
{
	// Token: 0x020000D4 RID: 212
	[Serializable]
	public class ReverseProxyConnectResponse : IPacket
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000487C File Offset: 0x00002A7C
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00004884 File Offset: 0x00002A84
		public int ConnectionId { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000488D File Offset: 0x00002A8D
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x00004895 File Offset: 0x00002A95
		public bool IsConnected { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000489E File Offset: 0x00002A9E
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x000048A6 File Offset: 0x00002AA6
		private IPAddress LocalAddress { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x000048AF File Offset: 0x00002AAF
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x000048B7 File Offset: 0x00002AB7
		public int LocalPort { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000048C0 File Offset: 0x00002AC0
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x000048C8 File Offset: 0x00002AC8
		public string HostName { get; set; }

		// Token: 0x0600054F RID: 1359 RVA: 0x00012B44 File Offset: 0x00010D44
		public ReverseProxyConnectResponse(int connectionId, bool isConnected, IPAddress localAddress, int localPort, string targetServer)
		{
			this.ConnectionId = connectionId;
			this.IsConnected = isConnected;
			this.LocalAddress = localAddress;
			this.LocalPort = localPort;
			this.HostName = "";
			if (isConnected)
			{
				try
				{
					IPHostEntry hostEntry = Dns.GetHostEntry(targetServer);
					if (hostEntry != null && !string.IsNullOrEmpty(hostEntry.HostName))
					{
						this.HostName = hostEntry.HostName;
					}
				}
				catch
				{
					this.HostName = "";
				}
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000048D1 File Offset: 0x00002AD1
		public void Execute(Client client)
		{
			client.Send<ReverseProxyConnectResponse>(this);
		}
	}
}
