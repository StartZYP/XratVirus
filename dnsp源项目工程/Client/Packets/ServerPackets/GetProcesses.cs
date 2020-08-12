using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x020000A4 RID: 164
	[Serializable]
	public class GetProcesses : IPacket
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x000042E5 File Offset: 0x000024E5
		public void Execute(Client client)
		{
			client.Send<GetProcesses>(this);
		}
	}
}
