using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x0200008A RID: 138
	[Serializable]
	public class DoShellExecuteResponse : IPacket
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00003C79 File Offset: 0x00001E79
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00003C81 File Offset: 0x00001E81
		public string Output { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00003C8A File Offset: 0x00001E8A
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00003C92 File Offset: 0x00001E92
		public bool IsError { get; private set; }

		// Token: 0x0600039C RID: 924 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoShellExecuteResponse()
		{
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00003C9B File Offset: 0x00001E9B
		public DoShellExecuteResponse(string output, bool isError = false)
		{
			this.Output = output;
			this.IsError = isError;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00003CB1 File Offset: 0x00001EB1
		public void Execute(Client client)
		{
			client.Send<DoShellExecuteResponse>(this);
		}
	}
}
