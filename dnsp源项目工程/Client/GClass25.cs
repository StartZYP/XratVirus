using System;
using System.Collections.Generic;

// Token: 0x0200004A RID: 74
public class GClass25
{
	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060001EA RID: 490 RVA: 0x00002E92 File Offset: 0x00001092
	public bool IsEmpty
	{
		get
		{
			return this.queue_0.Count == 0;
		}
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000CF24 File Offset: 0x0000B124
	public GClass25(List<GClass24> hosts)
	{
		foreach (GClass24 item in hosts)
		{
			this.queue_0.Enqueue(item);
		}
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000CF8C File Offset: 0x0000B18C
	public GClass24 method_0()
	{
		GClass24 gclass = this.queue_0.Dequeue();
		this.queue_0.Enqueue(gclass);
		return gclass;
	}

	// Token: 0x040000D7 RID: 215
	private readonly Queue<GClass24> queue_0 = new Queue<GClass24>();
}
