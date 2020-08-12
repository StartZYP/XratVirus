using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using xClient.Core.MouseKeyHook.WinApi;

namespace xClient.Core.MouseKeyHook
{
	// Token: 0x0200006E RID: 110
	public class MouseEventExtArgs : MouseEventArgs
	{
		// Token: 0x060002FE RID: 766 RVA: 0x000036A5 File Offset: 0x000018A5
		internal MouseEventExtArgs(MouseButtons buttons, int clicks, Point point, int delta, int timestamp, bool isMouseKeyDown, bool isMouseKeyUp) : base(buttons, clicks, point.X, point.Y, delta)
		{
			this.IsMouseKeyDown = isMouseKeyDown;
			this.IsMouseKeyUp = isMouseKeyUp;
			this.Timestamp = timestamp;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000036D7 File Offset: 0x000018D7
		// (set) Token: 0x06000300 RID: 768 RVA: 0x000036DF File Offset: 0x000018DF
		public bool Handled { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000301 RID: 769 RVA: 0x000036E8 File Offset: 0x000018E8
		public bool WheelScrolled
		{
			get
			{
				return base.Delta != 0;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000036F6 File Offset: 0x000018F6
		public bool Clicked
		{
			get
			{
				return base.Clicks > 0;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00003701 File Offset: 0x00001901
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00003709 File Offset: 0x00001909
		public bool IsMouseKeyDown { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00003712 File Offset: 0x00001912
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000371A File Offset: 0x0000191A
		public bool IsMouseKeyUp { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00003723 File Offset: 0x00001923
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000372B File Offset: 0x0000192B
		public int Timestamp { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00003734 File Offset: 0x00001934
		internal Point Point
		{
			get
			{
				return new Point(base.X, base.Y);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000F198 File Offset: 0x0000D398
		internal static MouseEventExtArgs FromRawDataApp(CallbackData data)
		{
			IntPtr wparam = data.WParam;
			IntPtr lparam = data.LParam;
			return MouseEventExtArgs.FromRawDataUniversal(wparam, ((AppMouseStruct)Marshal.PtrToStructure(lparam, typeof(AppMouseStruct))).ToMouseStruct());
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000F1D8 File Offset: 0x0000D3D8
		internal static MouseEventExtArgs FromRawDataGlobal(CallbackData data)
		{
			IntPtr wparam = data.WParam;
			IntPtr lparam = data.LParam;
			MouseStruct mouseInfo = (MouseStruct)Marshal.PtrToStructure(lparam, typeof(MouseStruct));
			return MouseEventExtArgs.FromRawDataUniversal(wparam, mouseInfo);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000F214 File Offset: 0x0000D414
		private static MouseEventExtArgs FromRawDataUniversal(IntPtr wParam, MouseStruct mouseInfo)
		{
			MouseButtons buttons = MouseButtons.None;
			short delta = 0;
			int clicks = 0;
			bool isMouseKeyDown = false;
			bool isMouseKeyUp = false;
			long num = (long)wParam;
			if (num <= 526L && num >= 513L)
			{
				switch ((int)(num - 513L))
				{
				case 0:
					isMouseKeyDown = true;
					buttons = MouseButtons.Left;
					clicks = 1;
					break;
				case 1:
					isMouseKeyUp = true;
					buttons = MouseButtons.Left;
					clicks = 1;
					break;
				case 2:
					isMouseKeyDown = true;
					buttons = MouseButtons.Left;
					clicks = 2;
					break;
				case 3:
					isMouseKeyDown = true;
					buttons = MouseButtons.Right;
					clicks = 1;
					break;
				case 4:
					isMouseKeyUp = true;
					buttons = MouseButtons.Right;
					clicks = 1;
					break;
				case 5:
					isMouseKeyDown = true;
					buttons = MouseButtons.Right;
					clicks = 2;
					break;
				case 6:
					isMouseKeyDown = true;
					buttons = MouseButtons.Middle;
					clicks = 1;
					break;
				case 7:
					isMouseKeyUp = true;
					buttons = MouseButtons.Middle;
					clicks = 1;
					break;
				case 8:
					isMouseKeyDown = true;
					buttons = MouseButtons.Middle;
					clicks = 2;
					break;
				case 9:
					delta = mouseInfo.MouseData;
					break;
				case 10:
					buttons = ((mouseInfo.MouseData == 1) ? MouseButtons.XButton1 : MouseButtons.XButton2);
					isMouseKeyDown = true;
					clicks = 1;
					break;
				case 11:
					buttons = ((mouseInfo.MouseData == 1) ? MouseButtons.XButton1 : MouseButtons.XButton2);
					isMouseKeyUp = true;
					clicks = 1;
					break;
				case 12:
					isMouseKeyDown = true;
					buttons = ((mouseInfo.MouseData == 1) ? MouseButtons.XButton1 : MouseButtons.XButton2);
					clicks = 2;
					break;
				case 13:
					delta = mouseInfo.MouseData;
					break;
				}
			}
			return new MouseEventExtArgs(buttons, clicks, mouseInfo.Point, (int)delta, mouseInfo.Timestamp, isMouseKeyDown, isMouseKeyUp);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00003747 File Offset: 0x00001947
		internal MouseEventExtArgs ToDoubleClickEventArgs()
		{
			return new MouseEventExtArgs(base.Button, 2, this.Point, base.Delta, this.Timestamp, this.IsMouseKeyDown, this.IsMouseKeyUp);
		}
	}
}
