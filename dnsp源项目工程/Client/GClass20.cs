using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

// Token: 0x0200003D RID: 61
public static class GClass20
{
	// Token: 0x060001A9 RID: 425 RVA: 0x00002C96 File Offset: 0x00000E96
	private static bool smethod_0(this string keyName, RegistryKey key)
	{
		return string.IsNullOrEmpty(keyName) || key == null;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
	public static string smethod_1(this RegistryKey key, string keyName, string defaultValue = "")
	{
		string result;
		try
		{
			result = key.GetValue(keyName, defaultValue).ToString();
		}
		catch
		{
			result = defaultValue;
		}
		return result;
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000C00C File Offset: 0x0000A20C
	public static RegistryKey smethod_2(this RegistryKey key, string name)
	{
		RegistryKey result;
		try
		{
			result = key.OpenSubKey(name, false);
		}
		catch
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000C03C File Offset: 0x0000A23C
	public static RegistryKey smethod_3(this RegistryKey key, string name)
	{
		RegistryKey result;
		try
		{
			result = key.OpenSubKey(name, true);
		}
		catch
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000C06C File Offset: 0x0000A26C
	public static IEnumerable<string> smethod_4(this RegistryKey key)
	{
		GClass20.<GetFormattedKeyValues>d__5 <GetFormattedKeyValues>d__ = new GClass20.<GetFormattedKeyValues>d__5(-2);
		<GetFormattedKeyValues>d__.<>3__key = key;
		return <GetFormattedKeyValues>d__;
	}

	// Token: 0x060001AE RID: 430 RVA: 0x00002CA6 File Offset: 0x00000EA6
	[CompilerGenerated]
	private static bool smethod_5(string k)
	{
		return !string.IsNullOrEmpty(k);
	}

	// Token: 0x040000BE RID: 190
	[CompilerGenerated]
	private static Func<string, bool> func_0;

	// Token: 0x0200003E RID: 62
	[CompilerGenerated]
	private sealed class Class8
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00002CB1 File Offset: 0x00000EB1
		public bool method_0(string keyVal)
		{
			return !keyVal.smethod_0(this.registryKey_0);
		}

		// Token: 0x040000BF RID: 191
		public RegistryKey registryKey_0;
	}
}
