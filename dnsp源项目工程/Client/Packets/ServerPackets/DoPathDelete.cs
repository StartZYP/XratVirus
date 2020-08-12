using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000092 RID: 146
	[Serializable]
	public class DoPathDelete : IPacket
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00003E5A File Offset: 0x0000205A
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00003E62 File Offset: 0x00002062
		public string Path { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00003E6B File Offset: 0x0000206B
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00003E73 File Offset: 0x00002073
		public GEnum1 PathType { get; set; }

		// Token: 0x060003D0 RID: 976 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoPathDelete()
		{
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00003E7C File Offset: 0x0000207C
		public DoPathDelete(string path, GEnum1 pathtype)
		{
			this.Path = path;
			this.PathType = pathtype;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00003E92 File Offset: 0x00002092
		public void Execute(Client client)
		{
			client.Send<DoPathDelete>(this);
		}
	}
}
