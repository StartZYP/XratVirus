using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

// Token: 0x02000008 RID: 8
public static class GClass6
{
	// Token: 0x06000043 RID: 67 RVA: 0x00005104 File Offset: 0x00003304
	public static List<GClass24> smethod_0(string rawHosts)
	{
		List<GClass24> list = new List<GClass24>();
		if (string.IsNullOrEmpty(rawHosts))
		{
			return list;
		}
		string[] array = rawHosts.Split(new char[]
		{
			';'
		});
		IEnumerable<string> source = array;
		if (GClass6.func_0 == null)
		{
			GClass6.func_0 = new Func<string, bool>(GClass6.smethod_2);
		}
		IEnumerable<string> source2 = source.Where(GClass6.func_0);
		if (GClass6.func_1 == null)
		{
			GClass6.func_1 = new Func<string, string[]>(GClass6.smethod_3);
		}
		foreach (string[] array2 in source2.Select(GClass6.func_1))
		{
			ushort port;
			if (array2.Length == 2 && array2[0].Length >= 1 && array2[1].Length >= 1 && ushort.TryParse(array2[1], out port))
			{
				list.Add(new GClass24
				{
					Hostname = array2[0],
					Port = port
				});
			}
		}
		return list;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00005200 File Offset: 0x00003400
	public static string smethod_1(List<GClass24> hosts)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (GClass24 arg in hosts)
		{
			stringBuilder.Append(arg + ";");
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000022D4 File Offset: 0x000004D4
	[CompilerGenerated]
	private static bool smethod_2(string host)
	{
		return !string.IsNullOrEmpty(host) && host.Contains(':');
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00005268 File Offset: 0x00003468
	[CompilerGenerated]
	private static string[] smethod_3(string host)
	{
		return host.Split(new char[]
		{
			':'
		});
	}

	// Token: 0x0400001B RID: 27
	[CompilerGenerated]
	private static Func<string, bool> func_0;

	// Token: 0x0400001C RID: 28
	[CompilerGenerated]
	private static Func<string, string[]> func_1;
}
