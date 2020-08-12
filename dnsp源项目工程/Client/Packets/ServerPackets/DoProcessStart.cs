using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x020000A3 RID: 163
	[Serializable]
	public class DoProcessStart : IPacket
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000042BC File Offset: 0x000024BC
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x000042C4 File Offset: 0x000024C4
		public string Processname { get; set; }

		// Token: 0x06000444 RID: 1092 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoProcessStart()
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000042CD File Offset: 0x000024CD
		public DoProcessStart(string processname)
		{
			this.Processname = processname;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000042DC File Offset: 0x000024DC
		public void Execute(Client client)
		{
			client.Send<DoProcessStart>(this);
		}
	}
}
