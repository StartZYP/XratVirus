using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000095 RID: 149
	[Serializable]
	public class DoDownloadFileCancel : IPacket
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00003F35 File Offset: 0x00002135
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00003F3D File Offset: 0x0000213D
		public int ID { get; set; }

		// Token: 0x060003E5 RID: 997 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoDownloadFileCancel()
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00003F46 File Offset: 0x00002146
		public DoDownloadFileCancel(int id)
		{
			this.ID = id;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00003F55 File Offset: 0x00002155
		public void Execute(Client client)
		{
			client.Send<DoDownloadFileCancel>(this);
		}
	}
}
