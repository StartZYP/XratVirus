using System;
using System.Runtime.InteropServices;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000C6 RID: 198
	public struct SHFILEINFO
	{
		// Token: 0x0400025F RID: 607
		public IntPtr hIcon;

		// Token: 0x04000260 RID: 608
		public IntPtr iIcon;

		// Token: 0x04000261 RID: 609
		public uint dwAttributes;

		// Token: 0x04000262 RID: 610
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string szDisplayName;

		// Token: 0x04000263 RID: 611
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
		public string szTypeName;
	}
}
