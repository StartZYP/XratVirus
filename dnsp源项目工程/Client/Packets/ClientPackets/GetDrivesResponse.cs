using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x02000084 RID: 132
	[Serializable]
	public class GetDrivesResponse : IPacket
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00003AA7 File Offset: 0x00001CA7
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00003AAF File Offset: 0x00001CAF
		public string[] DriveDisplayName { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00003AB8 File Offset: 0x00001CB8
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00003AC0 File Offset: 0x00001CC0
		public string[] RootDirectory { get; set; }

		// Token: 0x0600036C RID: 876 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetDrivesResponse()
		{
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00003AC9 File Offset: 0x00001CC9
		public GetDrivesResponse(string[] driveDisplayName, string[] rootDirectory)
		{
			this.DriveDisplayName = driveDisplayName;
			this.RootDirectory = rootDirectory;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00003ADF File Offset: 0x00001CDF
		public void Execute(Client client)
		{
			client.Send<GetDrivesResponse>(this);
		}
	}
}
