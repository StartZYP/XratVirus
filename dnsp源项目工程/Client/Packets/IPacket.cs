using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets
{
	// Token: 0x02000040 RID: 64
	public interface IPacket
	{
		// Token: 0x060001BA RID: 442
		void Execute(Client client);
	}
}
