using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	public class GetSystemInfoResponse : IPacket
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00003C27 File Offset: 0x00001E27
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00003C2F File Offset: 0x00001E2F
		public string[] SystemInfos { get; set; }

		// Token: 0x06000390 RID: 912 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetSystemInfoResponse()
		{
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00003C38 File Offset: 0x00001E38
		public GetSystemInfoResponse(string[] systeminfos)
		{
			this.SystemInfos = systeminfos;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00003C47 File Offset: 0x00001E47
		public void Execute(Client client)
		{
			client.Send<GetSystemInfoResponse>(this);
		}
	}
}
