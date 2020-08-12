using System;
using System.Windows.Forms;

namespace xClient.Core.MouseKeyHook
{
	// Token: 0x02000058 RID: 88
	public interface IMouseEvents
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600023F RID: 575
		// (remove) Token: 0x06000240 RID: 576
		event MouseEventHandler MouseMove;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000241 RID: 577
		// (remove) Token: 0x06000242 RID: 578
		event EventHandler<MouseEventExtArgs> MouseMoveExt;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000243 RID: 579
		// (remove) Token: 0x06000244 RID: 580
		event MouseEventHandler MouseClick;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000245 RID: 581
		// (remove) Token: 0x06000246 RID: 582
		event MouseEventHandler MouseDown;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000247 RID: 583
		// (remove) Token: 0x06000248 RID: 584
		event EventHandler<MouseEventExtArgs> MouseDownExt;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000249 RID: 585
		// (remove) Token: 0x0600024A RID: 586
		event MouseEventHandler MouseUp;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600024B RID: 587
		// (remove) Token: 0x0600024C RID: 588
		event EventHandler<MouseEventExtArgs> MouseUpExt;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600024D RID: 589
		// (remove) Token: 0x0600024E RID: 590
		event MouseEventHandler MouseWheel;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600024F RID: 591
		// (remove) Token: 0x06000250 RID: 592
		event MouseEventHandler MouseDoubleClick;
	}
}
