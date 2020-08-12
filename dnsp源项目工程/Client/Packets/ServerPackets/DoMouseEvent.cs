using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ServerPackets
{
	// Token: 0x020000A2 RID: 162
	[Serializable]
	public class DoMouseEvent : IPacket
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00004231 File Offset: 0x00002431
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x00004239 File Offset: 0x00002439
		public GEnum0 Action { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00004242 File Offset: 0x00002442
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000424A File Offset: 0x0000244A
		public bool IsMouseDown { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00004253 File Offset: 0x00002453
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000425B File Offset: 0x0000245B
		public int X { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00004264 File Offset: 0x00002464
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0000426C File Offset: 0x0000246C
		public int Y { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00004275 File Offset: 0x00002475
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000427D File Offset: 0x0000247D
		public int MonitorIndex { get; set; }

		// Token: 0x0600043F RID: 1087 RVA: 0x000021D4 File Offset: 0x000003D4
		public DoMouseEvent()
		{
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00004286 File Offset: 0x00002486
		public DoMouseEvent(GEnum0 action, bool isMouseDown, int x, int y, int monitorIndex)
		{
			this.Action = action;
			this.IsMouseDown = isMouseDown;
			this.X = x;
			this.Y = y;
			this.MonitorIndex = monitorIndex;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000042B3 File Offset: 0x000024B3
		public void Execute(Client client)
		{
			client.Send<DoMouseEvent>(this);
		}
	}
}
