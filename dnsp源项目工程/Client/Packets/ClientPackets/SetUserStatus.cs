using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x0200008B RID: 139
	[Serializable]
	public class SetUserStatus : IPacket
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00003CBA File Offset: 0x00001EBA
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00003CC2 File Offset: 0x00001EC2
		public GEnum3 Message { get; set; }

		// Token: 0x060003A1 RID: 929 RVA: 0x000021D4 File Offset: 0x000003D4
		public SetUserStatus()
		{
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00003CCB File Offset: 0x00001ECB
		public SetUserStatus(GEnum3 message)
		{
			this.Message = message;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00003CDA File Offset: 0x00001EDA
		public void Execute(Client client)
		{
			client.Send<SetUserStatus>(this);
		}
	}
}
