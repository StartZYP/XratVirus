using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

// Token: 0x020000B4 RID: 180
public static class GClass31
{
	// Token: 0x060004D9 RID: 1241 RVA: 0x00010CC0 File Offset: 0x0000EEC0
	public static string smethod_0<T>(T o)
	{
		DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
		string @string;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			dataContractJsonSerializer.WriteObject(memoryStream, o);
			@string = Encoding.UTF8.GetString(memoryStream.ToArray());
		}
		return @string;
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00010D20 File Offset: 0x0000EF20
	public static T smethod_1<T>(string json)
	{
		DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
		T result;
		using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
		{
			result = (T)((object)dataContractJsonSerializer.ReadObject(memoryStream));
		}
		return result;
	}
}
