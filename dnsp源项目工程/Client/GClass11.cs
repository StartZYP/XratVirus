using System;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

// Token: 0x0200000D RID: 13
public static class GClass11
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000069 RID: 105 RVA: 0x00002424 File Offset: 0x00000624
	// (set) Token: 0x0600006A RID: 106 RVA: 0x0000242B File Offset: 0x0000062B
	public static string HardwareId { get; private set; } = GClass19.smethod_0(GClass11.smethod_2() + GClass11.smethod_1() + GClass11.smethod_0());

	// Token: 0x0600006C RID: 108 RVA: 0x00005690 File Offset: 0x00003890
	public static string smethod_0()
	{
		try
		{
			string text = string.Empty;
			string queryString = "SELECT * FROM Win32_BIOS";
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(queryString))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject["Manufacturer"].ToString();
					}
				}
			}
			return (!string.IsNullOrEmpty(text)) ? text : "N/A";
		}
		catch
		{
		}
		return "Unknown";
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00005748 File Offset: 0x00003948
	public static string smethod_1()
	{
		try
		{
			string text = string.Empty;
			string queryString = "SELECT * FROM Win32_BaseBoard";
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(queryString))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject["Manufacturer"].ToString() + managementObject["SerialNumber"].ToString();
					}
				}
			}
			return (!string.IsNullOrEmpty(text)) ? text : "N/A";
		}
		catch
		{
		}
		return "Unknown";
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00005814 File Offset: 0x00003A14
	public static string smethod_2()
	{
		try
		{
			string text = string.Empty;
			string queryString = "SELECT * FROM Win32_Processor";
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(queryString))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					text = text + managementObject["Name"].ToString() + "; ";
				}
			}
			text = GClass5.smethod_3(text);
			return (!string.IsNullOrEmpty(text)) ? text : "N/A";
		}
		catch
		{
		}
		return "Unknown";
	}

	// Token: 0x0600006F RID: 111 RVA: 0x000058E0 File Offset: 0x00003AE0
	public static int smethod_3()
	{
		int result;
		try
		{
			int num = 0;
			string queryString = "Select * From Win32_ComputerSystem";
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(queryString))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						double num2 = Convert.ToDouble(managementObject["TotalPhysicalMemory"]);
						num = (int)(num2 / 1048576.0);
					}
				}
			}
			result = num;
		}
		catch
		{
			result = -1;
		}
		return result;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00005990 File Offset: 0x00003B90
	public static string smethod_4()
	{
		string result;
		try
		{
			string text = string.Empty;
			string queryString = "SELECT * FROM Win32_DisplayConfiguration";
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(queryString))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					text = text + managementObject["Description"].ToString() + "; ";
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

	// Token: 0x06000071 RID: 113 RVA: 0x00005A5C File Offset: 0x00003C5C
	public static string smethod_5()
	{
		foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
		{
			if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet && networkInterface.OperationalStatus == OperationalStatus.Up))
			{
				foreach (UnicastIPAddressInformation unicastIPAddressInformation in networkInterface.GetIPProperties().UnicastAddresses)
				{
					if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork && unicastIPAddressInformation.AddressPreferredLifetime != 4294967295L)
					{
						return unicastIPAddressInformation.Address.ToString();
					}
				}
			}
		}
		return "-";
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00005B24 File Offset: 0x00003D24
	public static string smethod_6()
	{
		foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
		{
			if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet && networkInterface.OperationalStatus == OperationalStatus.Up))
			{
				bool flag = false;
				foreach (UnicastIPAddressInformation unicastIPAddressInformation in networkInterface.GetIPProperties().UnicastAddresses)
				{
					if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork && unicastIPAddressInformation.AddressPreferredLifetime != 4294967295L)
					{
						flag = (unicastIPAddressInformation.Address.ToString() == GClass11.smethod_5());
					}
				}
				if (flag)
				{
					return GClass5.smethod_0(networkInterface.GetPhysicalAddress().ToString());
				}
			}
		}
		return "-";
	}

	// Token: 0x04000030 RID: 48
	[CompilerGenerated]
	private static string string_0;
}
