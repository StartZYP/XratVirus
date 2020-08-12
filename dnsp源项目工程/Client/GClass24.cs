using System;
using System.Runtime.CompilerServices;

// Token: 0x02000049 RID: 73
public class GClass24
{
	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060001E4 RID: 484 RVA: 0x00002E53 File Offset: 0x00001053
	// (set) Token: 0x060001E5 RID: 485 RVA: 0x00002E5B File Offset: 0x0000105B
	public string Hostname { get; set; }

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060001E6 RID: 486 RVA: 0x00002E64 File Offset: 0x00001064
	// (set) Token: 0x060001E7 RID: 487 RVA: 0x00002E6C File Offset: 0x0000106C
	public ushort Port { get; set; }

	// Token: 0x060001E8 RID: 488 RVA: 0x00002E75 File Offset: 0x00001075
	public virtual string vmethod_0()
	{
		return this.Hostname + ":" + this.Port;
	}

	// Token: 0x040000D5 RID: 213
	[CompilerGenerated]
	private string string_0;

	// Token: 0x040000D6 RID: 214
	[CompilerGenerated]
	private ushort ushort_0;
}
