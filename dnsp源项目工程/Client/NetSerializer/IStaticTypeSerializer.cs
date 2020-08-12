using System;
using System.Reflection;

namespace xClient.Core.NetSerializer
{
	// Token: 0x02000017 RID: 23
	public interface IStaticTypeSerializer : ITypeSerializer
	{
		// Token: 0x06000097 RID: 151
		void GetStaticMethods(Type type, out MethodInfo writer, out MethodInfo reader);
	}
}
