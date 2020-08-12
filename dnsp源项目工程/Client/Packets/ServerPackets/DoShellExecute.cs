using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x0200009D RID: 157
	[Serializable]
	public class DoShellExecute : IPacket
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000400D File Offset: 0x0000220D
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00004015 File Offset: 0x00002215
		public string Command { get; set; }

		// Token: 0x06000402 RID: 1026 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoShellExecute()
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000401E File Offset: 0x0000221E
		public DoShellExecute(string command)
		{
			this.Command = command;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000402D File Offset: 0x0000222D
		public void Execute(Client client)
		{
			client.Send<DoShellExecute>(this);
		}
	}
}
