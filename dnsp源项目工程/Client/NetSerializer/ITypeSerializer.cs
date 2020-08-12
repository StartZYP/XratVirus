using System;
using System.Collections.Generic;

namespace xClient.Core.NetSerializer
{
	// Token: 0x02000016 RID: 22
	public interface ITypeSerializer
	{
		// Token: 0x06000095 RID: 149
		bool Handles(Type type);

		// Token: 0x06000096 RID: 150
		IEnumerable<Type> GetSubtypes(Type type);
	}
}
