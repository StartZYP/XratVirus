using System;
using System.Management;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

// Token: 0x0200000A RID: 10
public static class GClass8
{
	// Token: 0x0600004D RID: 77 RVA: 0x000052CC File Offset: 0x000034CC
	static GClass8()
	{
		GClass8.RunningOnMono = (Type.GetType("Mono.Runtime") != null);
		GClass8.Name = "Unknown OS";
		using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem"))
		{
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ManagementObject managementObject = (ManagementObject)enumerator.Current;
					GClass8.Name = managementObject["Caption"].ToString();
				}
			}
		}
		GClass8.Name = Regex.Replace(GClass8.Name, "^.*(?=Windows)", "").TrimEnd(new char[0]).TrimStart(new char[0]);
		GClass8.Is64Bit = Environment.Is64BitOperatingSystem;
		GClass8.FullName = string.Format("{0} {1} Bit", GClass8.Name, GClass8.Is64Bit ? 64 : 32);
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x0600004E RID: 78 RVA: 0x00002371 File Offset: 0x00000571
	// (set) Token: 0x0600004F RID: 79 RVA: 0x00002378 File Offset: 0x00000578
	public static string FullName { get; private set; }

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000050 RID: 80 RVA: 0x00002380 File Offset: 0x00000580
	// (set) Token: 0x06000051 RID: 81 RVA: 0x00002387 File Offset: 0x00000587
	public static string Name { get; private set; }

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000052 RID: 82 RVA: 0x0000238F File Offset: 0x0000058F
	// (set) Token: 0x06000053 RID: 83 RVA: 0x00002396 File Offset: 0x00000596
	public static bool Is64Bit { get; private set; }

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000054 RID: 84 RVA: 0x0000239E File Offset: 0x0000059E
	// (set) Token: 0x06000055 RID: 85 RVA: 0x000023A5 File Offset: 0x000005A5
	public static bool RunningOnMono { get; private set; }

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000056 RID: 86 RVA: 0x000023AD File Offset: 0x000005AD
	// (set) Token: 0x06000057 RID: 87 RVA: 0x000023B4 File Offset: 0x000005B4
	public static bool Win32NT { get; private set; } = Environment.OSVersion.Platform == PlatformID.Win32NT;

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000058 RID: 88 RVA: 0x000023BC File Offset: 0x000005BC
	// (set) Token: 0x06000059 RID: 89 RVA: 0x000023C3 File Offset: 0x000005C3
	public static bool XpOrHigher { get; private set; } = GClass8.Win32NT && Environment.OSVersion.Version.Major >= 5;

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600005A RID: 90 RVA: 0x000023CB File Offset: 0x000005CB
	// (set) Token: 0x0600005B RID: 91 RVA: 0x000023D2 File Offset: 0x000005D2
	public static bool VistaOrHigher { get; private set; } = GClass8.Win32NT && Environment.OSVersion.Version.Major >= 6;

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600005C RID: 92 RVA: 0x000023DA File Offset: 0x000005DA
	// (set) Token: 0x0600005D RID: 93 RVA: 0x000023E1 File Offset: 0x000005E1
	public static bool SevenOrHigher { get; private set; } = GClass8.Win32NT && Environment.OSVersion.Version >= new Version(6, 1);

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x0600005E RID: 94 RVA: 0x000023E9 File Offset: 0x000005E9
	// (set) Token: 0x0600005F RID: 95 RVA: 0x000023F0 File Offset: 0x000005F0
	public static bool EightOrHigher { get; private set; } = GClass8.Win32NT && Environment.OSVersion.Version >= new Version(6, 2, 9200);

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000060 RID: 96 RVA: 0x000023F8 File Offset: 0x000005F8
	// (set) Token: 0x06000061 RID: 97 RVA: 0x000023FF File Offset: 0x000005FF
	public static bool EightPointOneOrHigher { get; private set; } = GClass8.Win32NT && Environment.OSVersion.Version >= new Version(6, 3);

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000062 RID: 98 RVA: 0x00002407 File Offset: 0x00000607
	// (set) Token: 0x06000063 RID: 99 RVA: 0x0000240E File Offset: 0x0000060E
	public static bool TenOrHigher { get; private set; } = GClass8.Win32NT && Environment.OSVersion.Version >= new Version(10, 0);

	// Token: 0x04000024 RID: 36
	[CompilerGenerated]
	private static string string_0;

	// Token: 0x04000025 RID: 37
	[CompilerGenerated]
	private static string string_1;

	// Token: 0x04000026 RID: 38
	[CompilerGenerated]
	private static bool bool_0;

	// Token: 0x04000027 RID: 39
	[CompilerGenerated]
	private static bool bool_1;

	// Token: 0x04000028 RID: 40
	[CompilerGenerated]
	private static bool bool_2;

	// Token: 0x04000029 RID: 41
	[CompilerGenerated]
	private static bool bool_3;

	// Token: 0x0400002A RID: 42
	[CompilerGenerated]
	private static bool bool_4;

	// Token: 0x0400002B RID: 43
	[CompilerGenerated]
	private static bool bool_5;

	// Token: 0x0400002C RID: 44
	[CompilerGenerated]
	private static bool bool_6;

	// Token: 0x0400002D RID: 45
	[CompilerGenerated]
	private static bool bool_7;

	// Token: 0x0400002E RID: 46
	[CompilerGenerated]
	private static bool bool_8;
}
