using System;
using System.Windows.Forms;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x0200005A RID: 90
	internal abstract class EventFacade : IDisposable, IKeyboardEvents, IMouseEvents, IKeyboardMouseEvents
	{
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000251 RID: 593 RVA: 0x0000311E File Offset: 0x0000131E
		// (remove) Token: 0x06000252 RID: 594 RVA: 0x0000312C File Offset: 0x0000132C
		public event KeyEventHandler KeyDown
		{
			add
			{
				this.GetKeyListener().KeyDown += value;
			}
			remove
			{
				this.GetKeyListener().KeyDown -= value;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000253 RID: 595 RVA: 0x0000313A File Offset: 0x0000133A
		// (remove) Token: 0x06000254 RID: 596 RVA: 0x00003148 File Offset: 0x00001348
		public event KeyPressEventHandler KeyPress
		{
			add
			{
				this.GetKeyListener().KeyPress += value;
			}
			remove
			{
				this.GetKeyListener().KeyPress -= value;
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000255 RID: 597 RVA: 0x00003156 File Offset: 0x00001356
		// (remove) Token: 0x06000256 RID: 598 RVA: 0x00003164 File Offset: 0x00001364
		public event KeyEventHandler KeyUp
		{
			add
			{
				this.GetKeyListener().KeyUp += value;
			}
			remove
			{
				this.GetKeyListener().KeyUp -= value;
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000257 RID: 599 RVA: 0x00003172 File Offset: 0x00001372
		// (remove) Token: 0x06000258 RID: 600 RVA: 0x00003180 File Offset: 0x00001380
		public event MouseEventHandler MouseMove
		{
			add
			{
				this.GetMouseListener().MouseMove += value;
			}
			remove
			{
				this.GetMouseListener().MouseMove -= value;
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000259 RID: 601 RVA: 0x0000318E File Offset: 0x0000138E
		// (remove) Token: 0x0600025A RID: 602 RVA: 0x0000319C File Offset: 0x0000139C
		public event EventHandler<MouseEventExtArgs> MouseMoveExt
		{
			add
			{
				this.GetMouseListener().MouseMoveExt += value;
			}
			remove
			{
				this.GetMouseListener().MouseMoveExt -= value;
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600025B RID: 603 RVA: 0x000031AA File Offset: 0x000013AA
		// (remove) Token: 0x0600025C RID: 604 RVA: 0x000031B8 File Offset: 0x000013B8
		public event MouseEventHandler MouseClick
		{
			add
			{
				this.GetMouseListener().MouseClick += value;
			}
			remove
			{
				this.GetMouseListener().MouseClick -= value;
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600025D RID: 605 RVA: 0x000031C6 File Offset: 0x000013C6
		// (remove) Token: 0x0600025E RID: 606 RVA: 0x000031D4 File Offset: 0x000013D4
		public event MouseEventHandler MouseDown
		{
			add
			{
				this.GetMouseListener().MouseDown += value;
			}
			remove
			{
				this.GetMouseListener().MouseDown -= value;
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600025F RID: 607 RVA: 0x000031E2 File Offset: 0x000013E2
		// (remove) Token: 0x06000260 RID: 608 RVA: 0x000031F0 File Offset: 0x000013F0
		public event EventHandler<MouseEventExtArgs> MouseDownExt
		{
			add
			{
				this.GetMouseListener().MouseDownExt += value;
			}
			remove
			{
				this.GetMouseListener().MouseDownExt -= value;
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000261 RID: 609 RVA: 0x000031FE File Offset: 0x000013FE
		// (remove) Token: 0x06000262 RID: 610 RVA: 0x0000320C File Offset: 0x0000140C
		public event MouseEventHandler MouseUp
		{
			add
			{
				this.GetMouseListener().MouseUp += value;
			}
			remove
			{
				this.GetMouseListener().MouseUp -= value;
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000263 RID: 611 RVA: 0x0000321A File Offset: 0x0000141A
		// (remove) Token: 0x06000264 RID: 612 RVA: 0x00003228 File Offset: 0x00001428
		public event EventHandler<MouseEventExtArgs> MouseUpExt
		{
			add
			{
				this.GetMouseListener().MouseUpExt += value;
			}
			remove
			{
				this.GetMouseListener().MouseUpExt -= value;
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000265 RID: 613 RVA: 0x00003236 File Offset: 0x00001436
		// (remove) Token: 0x06000266 RID: 614 RVA: 0x00003244 File Offset: 0x00001444
		public event MouseEventHandler MouseWheel
		{
			add
			{
				this.GetMouseListener().MouseWheel += value;
			}
			remove
			{
				this.GetMouseListener().MouseWheel -= value;
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000267 RID: 615 RVA: 0x00003252 File Offset: 0x00001452
		// (remove) Token: 0x06000268 RID: 616 RVA: 0x00003260 File Offset: 0x00001460
		public event MouseEventHandler MouseDoubleClick
		{
			add
			{
				this.GetMouseListener().MouseDoubleClick += value;
			}
			remove
			{
				this.GetMouseListener().MouseDoubleClick -= value;
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000326E File Offset: 0x0000146E
		public void Dispose()
		{
			if (this.m_MouseListenerCache != null)
			{
				this.m_MouseListenerCache.Dispose();
			}
			if (this.m_KeyListenerCache != null)
			{
				this.m_KeyListenerCache.Dispose();
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000DF98 File Offset: 0x0000C198
		private KeyListener GetKeyListener()
		{
			KeyListener keyListener = this.m_KeyListenerCache;
			if (keyListener != null)
			{
				return keyListener;
			}
			keyListener = this.CreateKeyListener();
			this.m_KeyListenerCache = keyListener;
			return keyListener;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		private MouseListener GetMouseListener()
		{
			MouseListener mouseListener = this.m_MouseListenerCache;
			if (mouseListener != null)
			{
				return mouseListener;
			}
			mouseListener = this.CreateMouseListener();
			this.m_MouseListenerCache = mouseListener;
			return mouseListener;
		}

		// Token: 0x0600026C RID: 620
		protected abstract MouseListener CreateMouseListener();

		// Token: 0x0600026D RID: 621
		protected abstract KeyListener CreateKeyListener();

		// Token: 0x040000F8 RID: 248
		private KeyListener m_KeyListenerCache;

		// Token: 0x040000F9 RID: 249
		private MouseListener m_MouseListenerCache;
	}
}
