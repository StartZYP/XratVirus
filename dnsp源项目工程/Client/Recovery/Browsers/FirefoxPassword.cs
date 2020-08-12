using System;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000B0 RID: 176
	public class FirefoxPassword
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00004489 File Offset: 0x00002689
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00004491 File Offset: 0x00002691
		public string Username { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000449A File Offset: 0x0000269A
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x000044A2 File Offset: 0x000026A2
		public string Password { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x000044AB File Offset: 0x000026AB
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x000044B3 File Offset: 0x000026B3
		public Uri Host { get; set; }

		// Token: 0x060004A7 RID: 1191 RVA: 0x00010848 File Offset: 0x0000EA48
		public override string ToString()
		{
			return string.Format("User: {0}{3}Pass: {1}{3}Host: {2}", new object[]
			{
				this.Username,
				this.Password,
				this.Host.Host,
				Environment.NewLine
			});
		}
	}
}
