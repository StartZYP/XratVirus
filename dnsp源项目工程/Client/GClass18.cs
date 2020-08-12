using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Token: 0x0200003B RID: 59
public static class GClass18
{
	// Token: 0x060001A1 RID: 417 RVA: 0x0000BB98 File Offset: 0x00009D98
	public static void smethod_0(string key)
	{
		using (MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider())
		{
			//将key进行utf-8编码 再进行md5运算
			GClass18.byte_0 = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(key));
			
		}
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00002C46 File Offset: 0x00000E46
	public static string smethod_1(string input, string key)
	{
		return Convert.ToBase64String(GClass18.smethod_4(Encoding.UTF8.GetBytes(input), Encoding.UTF8.GetBytes(key)));
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x00002C68 File Offset: 0x00000E68
	public static string smethod_2(string input)
	{
		return Convert.ToBase64String(GClass18.smethod_3(Encoding.UTF8.GetBytes(input)));
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x0000BBE0 File Offset: 0x00009DE0
	public static byte[] smethod_3(byte[] input)
	{
		if (GClass18.byte_0 != null && GClass18.byte_0.Length != 0)
		{
			if (input != null && input.Length != 0)
			{
				byte[] result = new byte[0];
				try
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (RijndaelManaged rijndaelManaged = new RijndaelManaged
						{
							Key = GClass18.byte_0
						})
						{
							rijndaelManaged.GenerateIV();
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
							{
								memoryStream.Write(rijndaelManaged.IV, 0, rijndaelManaged.IV.Length);
								cryptoStream.Write(input, 0, input.Length);
							}
						}
						result = memoryStream.ToArray();
					}
					return result;
				}
				catch
				{
					return result;
				}
			}
			throw new ArgumentException("Input can not be empty.");
		}
		throw new Exception("Key can not be empty.");
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0000BCEC File Offset: 0x00009EEC
	public static byte[] smethod_4(byte[] input, byte[] key)
	{
		if (key != null && key.Length != 0)
		{
			if (input != null && input.Length != 0)
			{
				using (MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider())
				{
					key = md5CryptoServiceProvider.ComputeHash(key);
				}
				byte[] result = new byte[0];
				try
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (RijndaelManaged rijndaelManaged = new RijndaelManaged
						{
							Key = key
						})
						{
							rijndaelManaged.GenerateIV();
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
							{
								memoryStream.Write(rijndaelManaged.IV, 0, rijndaelManaged.IV.Length);
								cryptoStream.Write(input, 0, input.Length);
							}
						}
						result = memoryStream.ToArray();
					}
					return result;
				}
				catch
				{
					return result;
				}
			}
			throw new ArgumentException("Input can not be empty.");
		}
		throw new Exception("Key can not be empty.");
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00002C7F File Offset: 0x00000E7F
	public static string smethod_5(string input)
	{
		//先将编码base64转换然后 进入GClass18.smethod_6()函数 最后进行utf-8编码得到字符串
		return Encoding.UTF8.GetString(GClass18.smethod_6(Convert.FromBase64String(input)));
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000BE18 File Offset: 0x0000A018
	public static byte[] smethod_6(byte[] input)
	{
		if (GClass18.byte_0 != null && GClass18.byte_0.Length != 0)
		{
			if (input != null && input.Length != 0)
			{
				byte[] array = new byte[0];
				try
				{
					using (MemoryStream memoryStream = new MemoryStream(input))
					{
						using (RijndaelManaged rijndaelManaged = new RijndaelManaged
						{
							Key = GClass18.byte_0
						})
						{
							byte[] array2 = new byte[16];
							memoryStream.Read(array2, 0, 16);
							rijndaelManaged.IV = array2;
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Read))
							{
								byte[] array3 = new byte[memoryStream.Length - 16L + 1L];
								array = new byte[cryptoStream.Read(array3, 0, array3.Length)];
								Buffer.BlockCopy(array3, 0, array, 0, array.Length);
							}
						}
					}
					return array;
				}
				catch
				{
					return array;
				}
			}
			throw new ArgumentException("Input can not be empty.");
		}
		throw new Exception("Key can not be empty.");
	}

	// Token: 0x040000BC RID: 188
	private const int int_0 = 16;

	// Token: 0x040000BD RID: 189
	private static byte[] byte_0;
}
