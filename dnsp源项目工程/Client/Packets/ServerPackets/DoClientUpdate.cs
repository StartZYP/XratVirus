using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	public class DoClientUpdate : IPacket
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x000040A8 File Offset: 0x000022A8
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x000040B0 File Offset: 0x000022B0
		public int ID { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000040B9 File Offset: 0x000022B9
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x000040C1 File Offset: 0x000022C1
		public string DownloadURL { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000040CA File Offset: 0x000022CA
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x000040D2 File Offset: 0x000022D2
		public string FileName { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x000040DB File Offset: 0x000022DB
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x000040E3 File Offset: 0x000022E3
		public byte[] Block { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000040EC File Offset: 0x000022EC
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x000040F4 File Offset: 0x000022F4
		public int MaxBlocks { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000040FD File Offset: 0x000022FD
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00004105 File Offset: 0x00002305
		public int CurrentBlock { get; set; }

		// Token: 0x0600041C RID: 1052 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoClientUpdate()
		{
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000410E File Offset: 0x0000230E
		public DoClientUpdate(int id, string downloadurl, string filename, byte[] block, int maxblocks, int currentblock)
		{
			this.ID = id;
			this.DownloadURL = downloadurl;
			this.FileName = filename;
			this.Block = block;
			this.MaxBlocks = maxblocks;
			this.CurrentBlock = currentblock;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00004143 File Offset: 0x00002343
		public void Execute(Client client)
		{
			client.Send<DoClientUpdate>(this);
		}
	}
}
