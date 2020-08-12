using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

// Token: 0x020000E0 RID: 224
public static class GClass34
{
	// Token: 0x17000112 RID: 274
	// (get) Token: 0x0600059D RID: 1437 RVA: 0x00004AFC File Offset: 0x00002CFC
	// (set) Token: 0x0600059E RID: 1438 RVA: 0x00004B03 File Offset: 0x00002D03
	public static int ImageIndex { get; set; }

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x0600059F RID: 1439 RVA: 0x00004B0B File Offset: 0x00002D0B
	// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00004B12 File Offset: 0x00002D12
	public static GClass1 GeoInfo { get; private set; }

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00004B1A File Offset: 0x00002D1A
	// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00004B21 File Offset: 0x00002D21
	public static DateTime LastLocated { get; private set; } = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00004B29 File Offset: 0x00002D29
	// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00004B30 File Offset: 0x00002D30
	public static bool LocationCompleted { get; private set; }

	// Token: 0x060005A6 RID: 1446 RVA: 0x000139D4 File Offset: 0x00011BD4
	public static void smethod_0()
	{
		TimeSpan timeSpan = new TimeSpan(DateTime.UtcNow.Ticks - GClass34.LastLocated.Ticks);
		if (timeSpan.TotalMinutes > 30.0 || !GClass34.LocationCompleted)
		{
			GClass34.smethod_1();
			if (GClass34.GeoInfo.country_code == "-" || GClass34.GeoInfo.country == "Unknown")
			{
				GClass34.ImageIndex = 247;
				return;
			}
			for (int i = 0; i < GClass34.string_0.Length; i++)
			{
				if (GClass34.string_0[i].Contains(GClass34.GeoInfo.country_code.ToLower()))
				{
					GClass34.ImageIndex = i;
					return;
				}
			}
		}
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00013A90 File Offset: 0x00011C90
	private static void smethod_1()
	{
		GClass34.LocationCompleted = false;
		try
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(GClass1));
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://telize.com/geoip");
			httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0";
			httpWebRequest.Proxy = null;
			httpWebRequest.Timeout = 5000;
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (Stream responseStream = httpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						string s = streamReader.ReadToEnd();
						using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(s)))
						{
							GClass34.GeoInfo = (GClass1)dataContractJsonSerializer.ReadObject(memoryStream);
						}
					}
				}
			}
			GClass34.LastLocated = DateTime.UtcNow;
			GClass34.LocationCompleted = true;
		}
		catch
		{
			GClass34.smethod_2();
		}
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x00013BB8 File Offset: 0x00011DB8
	private static void smethod_2()
	{
		GClass34.GeoInfo = new GClass1();
		try
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://freegeoip.net/xml/");
			httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0";
			httpWebRequest.Proxy = null;
			httpWebRequest.Timeout = 5000;
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (Stream responseStream = httpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						string xml = streamReader.ReadToEnd();
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.LoadXml(xml);
						string innerXml = xmlDocument.SelectSingleNode("Response//IP").InnerXml;
						string innerXml2 = xmlDocument.SelectSingleNode("Response//CountryName").InnerXml;
						string innerXml3 = xmlDocument.SelectSingleNode("Response//CountryCode").InnerXml;
						string innerXml4 = xmlDocument.SelectSingleNode("Response//RegionName").InnerXml;
						string innerXml5 = xmlDocument.SelectSingleNode("Response//City").InnerXml;
						GClass34.GeoInfo.ip = ((!string.IsNullOrEmpty(innerXml)) ? innerXml : "-");
						GClass34.GeoInfo.country = ((!string.IsNullOrEmpty(innerXml2)) ? innerXml2 : "Unknown");
						GClass34.GeoInfo.country_code = ((!string.IsNullOrEmpty(innerXml3)) ? innerXml3 : "-");
						GClass34.GeoInfo.region = ((!string.IsNullOrEmpty(innerXml4)) ? innerXml4 : "Unknown");
						GClass34.GeoInfo.city = ((!string.IsNullOrEmpty(innerXml5)) ? innerXml5 : "Unknown");
					}
				}
			}
			GClass34.LastLocated = DateTime.UtcNow;
			GClass34.LocationCompleted = true;
		}
		catch
		{
			GClass34.GeoInfo.country = "Unknown";
			GClass34.GeoInfo.country_code = "-";
			GClass34.GeoInfo.region = "Unknown";
			GClass34.GeoInfo.city = "Unknown";
			GClass34.LocationCompleted = false;
		}
		if (string.IsNullOrEmpty(GClass34.GeoInfo.ip))
		{
			GClass34.smethod_3();
		}
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00013E10 File Offset: 0x00012010
	private static void smethod_3()
	{
		string ip = "-";
		try
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.ipify.org/");
			httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0";
			httpWebRequest.Proxy = null;
			httpWebRequest.Timeout = 5000;
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (Stream responseStream = httpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						ip = streamReader.ReadToEnd();
					}
				}
			}
		}
		catch (Exception)
		{
		}
		GClass34.GeoInfo.ip = ip;
	}

	// Token: 0x0400029C RID: 668
	public static readonly string[] string_0 = new string[]
	{
		"ad.png",
		"ae.png",
		"af.png",
		"ag.png",
		"ai.png",
		"al.png",
		"am.png",
		"an.png",
		"ao.png",
		"ar.png",
		"as.png",
		"at.png",
		"au.png",
		"aw.png",
		"ax.png",
		"az.png",
		"ba.png",
		"bb.png",
		"bd.png",
		"be.png",
		"bf.png",
		"bg.png",
		"bh.png",
		"bi.png",
		"bj.png",
		"bm.png",
		"bn.png",
		"bo.png",
		"br.png",
		"bs.png",
		"bt.png",
		"bv.png",
		"bw.png",
		"by.png",
		"bz.png",
		"ca.png",
		"catalonia.png",
		"cc.png",
		"cd.png",
		"cf.png",
		"cg.png",
		"ch.png",
		"ci.png",
		"ck.png",
		"cl.png",
		"cm.png",
		"cn.png",
		"co.png",
		"cr.png",
		"cs.png",
		"cu.png",
		"cv.png",
		"cx.png",
		"cy.png",
		"cz.png",
		"de.png",
		"dj.png",
		"dk.png",
		"dm.png",
		"do.png",
		"dz.png",
		"ec.png",
		"ee.png",
		"eg.png",
		"eh.png",
		"england.png",
		"er.png",
		"es.png",
		"et.png",
		"europeanunion.png",
		"fam.png",
		"fi.png",
		"fj.png",
		"fk.png",
		"fm.png",
		"fo.png",
		"fr.png",
		"ga.png",
		"gb.png",
		"gd.png",
		"ge.png",
		"gf.png",
		"gh.png",
		"gi.png",
		"gl.png",
		"gm.png",
		"gn.png",
		"gp.png",
		"gq.png",
		"gr.png",
		"gs.png",
		"gt.png",
		"gu.png",
		"gw.png",
		"gy.png",
		"hk.png",
		"hm.png",
		"hn.png",
		"hr.png",
		"ht.png",
		"hu.png",
		"id.png",
		"ie.png",
		"il.png",
		"in.png",
		"io.png",
		"iq.png",
		"ir.png",
		"is.png",
		"it.png",
		"jm.png",
		"jo.png",
		"jp.png",
		"ke.png",
		"kg.png",
		"kh.png",
		"ki.png",
		"km.png",
		"kn.png",
		"kp.png",
		"kr.png",
		"kw.png",
		"ky.png",
		"kz.png",
		"la.png",
		"lb.png",
		"lc.png",
		"li.png",
		"lk.png",
		"lr.png",
		"ls.png",
		"lt.png",
		"lu.png",
		"lv.png",
		"ly.png",
		"ma.png",
		"mc.png",
		"md.png",
		"me.png",
		"mg.png",
		"mh.png",
		"mk.png",
		"ml.png",
		"mm.png",
		"mn.png",
		"mo.png",
		"mp.png",
		"mq.png",
		"mr.png",
		"ms.png",
		"mt.png",
		"mu.png",
		"mv.png",
		"mw.png",
		"mx.png",
		"my.png",
		"mz.png",
		"na.png",
		"nc.png",
		"ne.png",
		"nf.png",
		"ng.png",
		"ni.png",
		"nl.png",
		"no.png",
		"np.png",
		"nr.png",
		"nu.png",
		"nz.png",
		"om.png",
		"pa.png",
		"pe.png",
		"pf.png",
		"pg.png",
		"ph.png",
		"pk.png",
		"pl.png",
		"pm.png",
		"pn.png",
		"pr.png",
		"ps.png",
		"pt.png",
		"pw.png",
		"py.png",
		"qa.png",
		"re.png",
		"ro.png",
		"rs.png",
		"ru.png",
		"rw.png",
		"sa.png",
		"sb.png",
		"sc.png",
		"scotland.png",
		"sd.png",
		"se.png",
		"sg.png",
		"sh.png",
		"si.png",
		"sj.png",
		"sk.png",
		"sl.png",
		"sm.png",
		"sn.png",
		"so.png",
		"sr.png",
		"st.png",
		"sv.png",
		"sy.png",
		"sz.png",
		"tc.png",
		"td.png",
		"tf.png",
		"tg.png",
		"th.png",
		"tj.png",
		"tk.png",
		"tl.png",
		"tm.png",
		"tn.png",
		"to.png",
		"tr.png",
		"tt.png",
		"tv.png",
		"tw.png",
		"tz.png",
		"ua.png",
		"ug.png",
		"um.png",
		"us.png",
		"uy.png",
		"uz.png",
		"va.png",
		"vc.png",
		"ve.png",
		"vg.png",
		"vi.png",
		"vn.png",
		"vu.png",
		"wales.png",
		"wf.png",
		"ws.png",
		"ye.png",
		"yt.png",
		"za.png",
		"zm.png",
		"zw.png"
	};

	// Token: 0x0400029D RID: 669
	[CompilerGenerated]
	private static int int_0;

	// Token: 0x0400029E RID: 670
	[CompilerGenerated]
	private static GClass1 gclass1_0;

	// Token: 0x0400029F RID: 671
	[CompilerGenerated]
	private static DateTime dateTime_0;

	// Token: 0x040002A0 RID: 672
	[CompilerGenerated]
	private static bool bool_0;
}
