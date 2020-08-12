using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000091 RID: 145
	[Serializable]
	public class GetDirectory : IPacket
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00003E31 File Offset: 0x00002031
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x00003E39 File Offset: 0x00002039
		public string RemotePath { get; set; }

		// Token: 0x060003C9 RID: 969 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetDirectory()
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00003E42 File Offset: 0x00002042
		public GetDirectory(string remotepath)
		{
			this.RemotePath = remotepath;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00003E51 File Offset: 0x00002051
		public void Execute(Client client)
		{
			client.Send<GetDirectory>(this);
		}
	}
}
