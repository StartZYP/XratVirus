using System;
using Microsoft.Win32;

// Token: 0x02000012 RID: 18
public static class GClass16
{
	// Token: 0x0600007B RID: 123 RVA: 0x0000245A File Offset: 0x0000065A
	private static string smethod_0()
	{
		if (!GClass8.Is64Bit)
		{
			return "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
		}
		return "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run";
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000062BC File Offset: 0x000044BC
	public static bool smethod_1()
	{
		if (GClass3.smethod_1() == "Admin")
		{
			return GClass9.smethod_0(RegistryHive.LocalMachine, GClass16.smethod_0(), GClass35.string_7, GClass0.CurrentPath, true) || GClass9.smethod_0(RegistryHive.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Run", GClass35.string_7, GClass0.CurrentPath, true);
		}
		return GClass9.smethod_0(RegistryHive.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Run", GClass35.string_7, GClass0.CurrentPath, true);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00006330 File Offset: 0x00004530
	public static bool smethod_2()
	{
		if (GClass3.smethod_1() == "Admin")
		{
			return GClass9.smethod_2(RegistryHive.LocalMachine, GClass16.smethod_0(), GClass35.string_7) || GClass9.smethod_2(RegistryHive.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Run", GClass35.string_7);
		}
		return GClass9.smethod_2(RegistryHive.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Run", GClass35.string_7);
	}
}
