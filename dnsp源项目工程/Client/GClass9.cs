using System;
using Microsoft.Win32;

// Token: 0x0200000B RID: 11
public static class GClass9
{
	// Token: 0x06000064 RID: 100 RVA: 0x000054C0 File Offset: 0x000036C0
	public static bool smethod_0(RegistryHive hive, string path, string name, string value, bool addQuotes = false)
	{
		bool result;
		try
		{
			using (RegistryKey registryKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).smethod_3(path))
			{
				if (registryKey == null)
				{
					result = false;
				}
				else
				{
					if (addQuotes && !value.StartsWith("\"") && !value.EndsWith("\""))
					{
						value = "\"" + value + "\"";
					}
					registryKey.SetValue(name, value);
					result = true;
				}
			}
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00005550 File Offset: 0x00003750
	public static RegistryKey smethod_1(RegistryHive hive, string path)
	{
		RegistryKey result;
		try
		{
			result = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).OpenSubKey(path, false);
		}
		catch
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00005588 File Offset: 0x00003788
	public static bool smethod_2(RegistryHive hive, string path, string name)
	{
		bool result;
		try
		{
			using (RegistryKey registryKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).smethod_3(path))
			{
				if (registryKey == null)
				{
					result = false;
				}
				else
				{
					registryKey.DeleteValue(name, true);
					result = true;
				}
			}
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}
}
