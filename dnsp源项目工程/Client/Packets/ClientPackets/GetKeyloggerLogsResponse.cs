using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x02000085 RID: 133
	[Serializable]
	public class GetKeyloggerLogsResponse : IPacket
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00003AE8 File Offset: 0x00001CE8
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00003AF0 File Offset: 0x00001CF0
		public string Filename { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00003AF9 File Offset: 0x00001CF9
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00003B01 File Offset: 0x00001D01
		public byte[] Block { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00003B0A File Offset: 0x00001D0A
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00003B12 File Offset: 0x00001D12
		public int MaxBlocks { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00003B1B File Offset: 0x00001D1B
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00003B23 File Offset: 0x00001D23
		public int CurrentBlock { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00003B2C File Offset: 0x00001D2C
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00003B34 File Offset: 0x00001D34
		public string CustomMessage { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00003B3D File Offset: 0x00001D3D
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00003B45 File Offset: 0x00001D45
		public int Index { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00003B4E File Offset: 0x00001D4E
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00003B56 File Offset: 0x00001D56
		public int FileCount { get; set; }

		// Token: 0x0600037D RID: 893 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetKeyloggerLogsResponse()
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00003B5F File Offset: 0x00001D5F
		public GetKeyloggerLogsResponse(string filename, byte[] block, int maxblocks, int currentblock, string custommessage, int index, int fileCount)
		{
			this.Filename = filename;
			this.Block = block;
			this.MaxBlocks = maxblocks;
			this.CurrentBlock = currentblock;
			this.CustomMessage = custommessage;
			this.Index = index;
			this.FileCount = fileCount;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00003B9C File Offset: 0x00001D9C
		public void Execute(Client client)
		{
			client.Send<GetKeyloggerLogsResponse>(this);
		}
	}
}
