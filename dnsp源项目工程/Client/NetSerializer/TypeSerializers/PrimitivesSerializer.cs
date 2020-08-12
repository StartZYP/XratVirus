using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace xClient.Core.NetSerializer.TypeSerializers
{
	// Token: 0x02000028 RID: 40
	public class PrimitivesSerializer : ITypeSerializer, IStaticTypeSerializer
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00002955 File Offset: 0x00000B55
		public bool Handles(Type type)
		{
			return PrimitivesSerializer.s_primitives.Contains(type);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000081A0 File Offset: 0x000063A0
		public IEnumerable<Type> GetSubtypes(Type type)
		{
			yield break;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00002962 File Offset: 0x00000B62
		public void GetStaticMethods(Type type, out MethodInfo writer, out MethodInfo reader)
		{
			writer = Primitives.GetWritePrimitive(type);
			reader = Primitives.GetReaderPrimitive(type);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00002974 File Offset: 0x00000B74
		public static IEnumerable<Type> GetSupportedTypes()
		{
			return PrimitivesSerializer.s_primitives;
		}

		// Token: 0x0400006E RID: 110
		private static Type[] s_primitives = new Type[]
		{
			typeof(bool),
			typeof(byte),
			typeof(sbyte),
			typeof(char),
			typeof(ushort),
			typeof(short),
			typeof(uint),
			typeof(int),
			typeof(ulong),
			typeof(long),
			typeof(float),
			typeof(double),
			typeof(string),
			typeof(DateTime),
			typeof(byte[])
		};
	}
}
