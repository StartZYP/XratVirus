using System;
using System.Collections.Generic;
using System.Reflection;

namespace xClient.Core.NetSerializer.TypeSerializers
{
	// Token: 0x02000023 RID: 35
	public class EnumSerializer : ITypeSerializer, IStaticTypeSerializer
	{
		// Token: 0x060000FF RID: 255 RVA: 0x000028B9 File Offset: 0x00000AB9
		public bool Handles(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007788 File Offset: 0x00005988
		public IEnumerable<Type> GetSubtypes(Type type)
		{
			Type underlyingType = Enum.GetUnderlyingType(type);
			yield return underlyingType;
			yield break;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000077AC File Offset: 0x000059AC
		public void GetStaticMethods(Type type, out MethodInfo writer, out MethodInfo reader)
		{
			Type underlyingType = Enum.GetUnderlyingType(type);
			writer = Primitives.GetWritePrimitive(underlyingType);
			reader = Primitives.GetReaderPrimitive(underlyingType);
		}
	}
}
