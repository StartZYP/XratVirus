using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000094 RID: 148
	[Serializable]
	public class DoDownloadFile : IPacket
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00003EF4 File Offset: 0x000020F4
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00003EFC File Offset: 0x000020FC
		public string RemotePath { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00003F05 File Offset: 0x00002105
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00003F0D File Offset: 0x0000210D
		public int ID { get; set; }

		// Token: 0x060003E0 RID: 992 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoDownloadFile()
		{
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00003F16 File Offset: 0x00002116
		public DoDownloadFile(string remotepath, int id)
		{
			this.RemotePath = remotepath;
			this.ID = id;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00003F2C File Offset: 0x0000212C
		public void Execute(Client client)
		{
			client.Send<DoDownloadFile>(this);
		}
	}
}
