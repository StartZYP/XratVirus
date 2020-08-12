using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

// Token: 0x02000003 RID: 3
[DataContract]
public class GClass1
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600000C RID: 12 RVA: 0x000020B3 File Offset: 0x000002B3
	// (set) Token: 0x0600000D RID: 13 RVA: 0x000020BB File Offset: 0x000002BB
	[DataMember]
	public double longitude { get; set; }

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600000E RID: 14 RVA: 0x000020C4 File Offset: 0x000002C4
	// (set) Token: 0x0600000F RID: 15 RVA: 0x000020CC File Offset: 0x000002CC
	[DataMember]
	public double latitude { get; set; }

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000010 RID: 16 RVA: 0x000020D5 File Offset: 0x000002D5
	// (set) Token: 0x06000011 RID: 17 RVA: 0x000020DD File Offset: 0x000002DD
	[DataMember]
	public string asn { get; set; }

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000012 RID: 18 RVA: 0x000020E6 File Offset: 0x000002E6
	// (set) Token: 0x06000013 RID: 19 RVA: 0x000020EE File Offset: 0x000002EE
	[DataMember]
	public string offset { get; set; }

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000014 RID: 20 RVA: 0x000020F7 File Offset: 0x000002F7
	// (set) Token: 0x06000015 RID: 21 RVA: 0x000020FF File Offset: 0x000002FF
	[DataMember]
	public string ip { get; set; }

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000016 RID: 22 RVA: 0x00002108 File Offset: 0x00000308
	// (set) Token: 0x06000017 RID: 23 RVA: 0x00002110 File Offset: 0x00000310
	[DataMember]
	public string area_code { get; set; }

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000018 RID: 24 RVA: 0x00002119 File Offset: 0x00000319
	// (set) Token: 0x06000019 RID: 25 RVA: 0x00002121 File Offset: 0x00000321
	[DataMember]
	public string continent_code { get; set; }

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600001A RID: 26 RVA: 0x0000212A File Offset: 0x0000032A
	// (set) Token: 0x0600001B RID: 27 RVA: 0x00002132 File Offset: 0x00000332
	[DataMember]
	public string dma_code { get; set; }

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600001C RID: 28 RVA: 0x0000213B File Offset: 0x0000033B
	// (set) Token: 0x0600001D RID: 29 RVA: 0x00002143 File Offset: 0x00000343
	[DataMember]
	public string city { get; set; }

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600001E RID: 30 RVA: 0x0000214C File Offset: 0x0000034C
	// (set) Token: 0x0600001F RID: 31 RVA: 0x00002154 File Offset: 0x00000354
	[DataMember]
	public string timezone { get; set; }

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000020 RID: 32 RVA: 0x0000215D File Offset: 0x0000035D
	// (set) Token: 0x06000021 RID: 33 RVA: 0x00002165 File Offset: 0x00000365
	[DataMember]
	public string region { get; set; }

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000022 RID: 34 RVA: 0x0000216E File Offset: 0x0000036E
	// (set) Token: 0x06000023 RID: 35 RVA: 0x00002176 File Offset: 0x00000376
	[DataMember]
	public string country_code { get; set; }

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000024 RID: 36 RVA: 0x0000217F File Offset: 0x0000037F
	// (set) Token: 0x06000025 RID: 37 RVA: 0x00002187 File Offset: 0x00000387
	[DataMember]
	public string isp { get; set; }

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000026 RID: 38 RVA: 0x00002190 File Offset: 0x00000390
	// (set) Token: 0x06000027 RID: 39 RVA: 0x00002198 File Offset: 0x00000398
	[DataMember]
	public string postal_code { get; set; }

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000028 RID: 40 RVA: 0x000021A1 File Offset: 0x000003A1
	// (set) Token: 0x06000029 RID: 41 RVA: 0x000021A9 File Offset: 0x000003A9
	[DataMember]
	public string country { get; set; }

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600002A RID: 42 RVA: 0x000021B2 File Offset: 0x000003B2
	// (set) Token: 0x0600002B RID: 43 RVA: 0x000021BA File Offset: 0x000003BA
	[DataMember]
	public string country_code3 { get; set; }

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600002C RID: 44 RVA: 0x000021C3 File Offset: 0x000003C3
	// (set) Token: 0x0600002D RID: 45 RVA: 0x000021CB File Offset: 0x000003CB
	[DataMember]
	public string region_code { get; set; }

	// Token: 0x04000006 RID: 6
	[CompilerGenerated]
	private double double_0;

	// Token: 0x04000007 RID: 7
	[CompilerGenerated]
	private double double_1;

	// Token: 0x04000008 RID: 8
	[CompilerGenerated]
	private string string_0;

	// Token: 0x04000009 RID: 9
	[CompilerGenerated]
	private string string_1;

	// Token: 0x0400000A RID: 10
	[CompilerGenerated]
	private string string_2;

	// Token: 0x0400000B RID: 11
	[CompilerGenerated]
	private string string_3;

	// Token: 0x0400000C RID: 12
	[CompilerGenerated]
	private string string_4;

	// Token: 0x0400000D RID: 13
	[CompilerGenerated]
	private string string_5;

	// Token: 0x0400000E RID: 14
	[CompilerGenerated]
	private string string_6;

	// Token: 0x0400000F RID: 15
	[CompilerGenerated]
	private string string_7;

	// Token: 0x04000010 RID: 16
	[CompilerGenerated]
	private string string_8;

	// Token: 0x04000011 RID: 17
	[CompilerGenerated]
	private string string_9;

	// Token: 0x04000012 RID: 18
	[CompilerGenerated]
	private string string_10;

	// Token: 0x04000013 RID: 19
	[CompilerGenerated]
	private string string_11;

	// Token: 0x04000014 RID: 20
	[CompilerGenerated]
	private string string_12;

	// Token: 0x04000015 RID: 21
	[CompilerGenerated]
	private string string_13;

	// Token: 0x04000016 RID: 22
	[CompilerGenerated]
	private string string_14;
}
