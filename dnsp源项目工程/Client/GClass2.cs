using System;
using System.Threading;

// Token: 0x02000004 RID: 4
public static class GClass2
{
	// Token: 0x0600002F RID: 47 RVA: 0x00004CF0 File Offset: 0x00002EF0
	public static bool smethod_0(string name)
	{
		bool result;
		GClass2.mutex_0 = new Mutex(false, name, ref result);
		return result;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000021DC File Offset: 0x000003DC
	public static void smethod_1()
	{
		if (GClass2.mutex_0 != null)
		{
			GClass2.mutex_0.Close();
			GClass2.mutex_0 = null;
		}
	}

	// Token: 0x04000017 RID: 23
	private static Mutex mutex_0;
}
