using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

// Token: 0x02000002 RID: 2
public static class GClass0
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x0000205C File Offset: 0x0000025C
	// (set) Token: 0x06000002 RID: 2 RVA: 0x00002063 File Offset: 0x00000263
	public static bool Disconnect { get; set; }

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000003 RID: 3 RVA: 0x0000206B File Offset: 0x0000026B
	// (set) Token: 0x06000004 RID: 4 RVA: 0x00002072 File Offset: 0x00000272
	public static string CurrentPath { get; set; } = Application.ExecutablePath;

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000005 RID: 5 RVA: 0x0000207A File Offset: 0x0000027A
	// (set) Token: 0x06000006 RID: 6 RVA: 0x00002081 File Offset: 0x00000281
	public static string InstallPath { get; set; }

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000007 RID: 7 RVA: 0x00002089 File Offset: 0x00000289
	// (set) Token: 0x06000008 RID: 8 RVA: 0x00002090 File Offset: 0x00000290
	public static bool AddToStartupFailed { get; set; }

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000009 RID: 9 RVA: 0x00002098 File Offset: 0x00000298
	// (set) Token: 0x0600000A RID: 10 RVA: 0x0000209F File Offset: 0x0000029F
	public static bool IsAuthenticated { get; set; }

	// Token: 0x04000001 RID: 1
	[CompilerGenerated]
	private static bool bool_0;

	// Token: 0x04000002 RID: 2
	[CompilerGenerated]
	private static string string_0;

	// Token: 0x04000003 RID: 3
	[CompilerGenerated]
	private static string string_1;

	// Token: 0x04000004 RID: 4
	[CompilerGenerated]
	private static bool bool_1;

	// Token: 0x04000005 RID: 5
	[CompilerGenerated]
	private static bool bool_2;
}
