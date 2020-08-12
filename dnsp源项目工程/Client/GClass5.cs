using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

// Token: 0x02000007 RID: 7
public static class GClass5
{
	// Token: 0x0600003F RID: 63 RVA: 0x00002272 File Offset: 0x00000472
	public static string smethod_0(string macAddress)
	{
		if (macAddress.Length == 12)
		{
			return Regex.Replace(macAddress, "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})", "$1:$2:$3:$4:$5:$6");
		}
		return "00:00:00:00:00:00";
	}

	// Token: 0x06000040 RID: 64 RVA: 0x000050C4 File Offset: 0x000032C4
	public static string smethod_1(DriveType type)
	{
		switch (type)
		{
		case DriveType.Removable:
			return "Removable Drive";
		case DriveType.Fixed:
			return "Local Disk";
		case DriveType.Network:
			return "Network Drive";
		default:
			return type.ToString();
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002294 File Offset: 0x00000494
	public static string smethod_2(Rectangle resolution)
	{
		return string.Format("{0}x{1}", resolution.Width, resolution.Height);
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000022B8 File Offset: 0x000004B8
	public static string smethod_3(string input)
	{
		if (input.Length > 2)
		{
			input = input.Remove(input.Length - 2);
		}
		return input;
	}
}
