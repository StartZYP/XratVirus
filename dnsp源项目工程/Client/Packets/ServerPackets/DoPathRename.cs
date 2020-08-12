using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000093 RID: 147
	[Serializable]
	public class DoPathRename : IPacket
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00003E9B File Offset: 0x0000209B
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00003EA3 File Offset: 0x000020A3
		public string Path { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00003EAC File Offset: 0x000020AC
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00003EB4 File Offset: 0x000020B4
		public string NewPath { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00003EBD File Offset: 0x000020BD
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00003EC5 File Offset: 0x000020C5
		public GEnum1 PathType { get; set; }

		// Token: 0x060003D9 RID: 985 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoPathRename()
		{
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00003ECE File Offset: 0x000020CE
		public DoPathRename(string path, string newpath, GEnum1 pathtype)
		{
			this.Path = path;
			this.NewPath = newpath;
			this.PathType = pathtype;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00003EEB File Offset: 0x000020EB
		public void Execute(Client client)
		{
			client.Send<DoPathRename>(this);
		}
	}
}
