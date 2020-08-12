using System;
using System.Windows.Forms;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x02000061 RID: 97
	internal class ButtonSet
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00003397 File Offset: 0x00001597
		public ButtonSet()
		{
			this.m_Set = MouseButtons.None;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000033A6 File Offset: 0x000015A6
		public void Add(MouseButtons element)
		{
			this.m_Set |= element;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000033B6 File Offset: 0x000015B6
		public void Remove(MouseButtons element)
		{
			this.m_Set &= ~element;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000033C7 File Offset: 0x000015C7
		public bool Contains(MouseButtons element)
		{
			return (this.m_Set & element) != MouseButtons.None;
		}

		// Token: 0x0400010A RID: 266
		private MouseButtons m_Set;
	}
}
