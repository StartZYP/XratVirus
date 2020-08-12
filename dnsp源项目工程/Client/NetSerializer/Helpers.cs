using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace xClient.Core.NetSerializer
{
	// Token: 0x02000015 RID: 21
	internal static class Helpers
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00006440 File Offset: 0x00004640
		public static IEnumerable<FieldInfo> GetFieldInfos(Type type)
		{
			IOrderedEnumerable<FieldInfo> orderedEnumerable = (from fi in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			where (fi.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope
			select fi).OrderBy((FieldInfo f) => f.Name, StringComparer.Ordinal);
			if (type.BaseType == null)
			{
				return orderedEnumerable;
			}
			IEnumerable<FieldInfo> fieldInfos = Helpers.GetFieldInfos(type.BaseType);
			return fieldInfos.Concat(orderedEnumerable);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000064C4 File Offset: 0x000046C4
		public static DynamicMethod GenerateDynamicSerializerStub(Type type)
		{
			DynamicMethod dynamicMethod = new DynamicMethod("Serialize", null, new Type[]
			{
				typeof(Serializer),
				typeof(Stream),
				type
			}, typeof(Serializer), true);
			dynamicMethod.DefineParameter(1, ParameterAttributes.None, "serializer");
			dynamicMethod.DefineParameter(2, ParameterAttributes.None, "stream");
			dynamicMethod.DefineParameter(3, ParameterAttributes.None, "value");
			return dynamicMethod;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000653C File Offset: 0x0000473C
		public static DynamicMethod GenerateDynamicDeserializerStub(Type type)
		{
			DynamicMethod dynamicMethod = new DynamicMethod("Deserialize", null, new Type[]
			{
				typeof(Serializer),
				typeof(Stream),
				type.MakeByRefType()
			}, typeof(Serializer), true);
			dynamicMethod.DefineParameter(1, ParameterAttributes.None, "serializer");
			dynamicMethod.DefineParameter(2, ParameterAttributes.None, "stream");
			dynamicMethod.DefineParameter(3, ParameterAttributes.Out, "value");
			return dynamicMethod;
		}

		// Token: 0x04000039 RID: 57
		public static readonly ConstructorInfo ExceptionCtorInfo = typeof(Exception).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null);
	}
}
