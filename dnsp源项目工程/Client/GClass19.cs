using System;
using System.Security.Cryptography;
using System.Text;

// Token: 0x0200003C RID: 60
public static class GClass19
{
	// Token: 0x060001A8 RID: 424 RVA: 0x0000BF50 File Offset: 0x0000A150
	public static string smethod_0(string input)
	{
		byte[] array = Encoding.UTF8.GetBytes(input);
		using (SHA256Managed sha256Managed = new SHA256Managed())
		{
			array = sha256Managed.ComputeHash(array, 0, array.Length);
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (byte b in array)
		{
			stringBuilder.Append(b.ToString("X2"));
		}
		return stringBuilder.ToString().ToUpper();
	}
}
