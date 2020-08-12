using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x0200008F RID: 143
	[Serializable]
	public class DoUploadFile : IPacket
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00003D65 File Offset: 0x00001F65
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x00003D6D File Offset: 0x00001F6D
		public int ID { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00003D76 File Offset: 0x00001F76
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00003D7E File Offset: 0x00001F7E
		public string RemotePath { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00003D87 File Offset: 0x00001F87
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00003D8F File Offset: 0x00001F8F
		public byte[] Block { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00003D98 File Offset: 0x00001F98
		// (set) Token: 0x060003BA RID: 954 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public int MaxBlocks { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00003DA9 File Offset: 0x00001FA9
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00003DB1 File Offset: 0x00001FB1
		public int CurrentBlock { get; set; }

		// Token: 0x060003BD RID: 957 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoUploadFile()
		{
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00003DBA File Offset: 0x00001FBA
		public DoUploadFile(int id, string remotepath, byte[] block, int maxblocks, int currentblock)
		{
			this.ID = id;
			this.RemotePath = remotepath;
			this.Block = block;
			this.MaxBlocks = maxblocks;
			this.CurrentBlock = currentblock;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00003DE7 File Offset: 0x00001FE7
		public void Execute(Client client)
		{
			client.SendBlocking<DoUploadFile>(this);
		}
	}
}
