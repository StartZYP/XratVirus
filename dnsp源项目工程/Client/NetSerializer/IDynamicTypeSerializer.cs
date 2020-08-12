using System;
using System.Reflection.Emit;

namespace xClient.Core.NetSerializer
{
	// Token: 0x02000018 RID: 24
	public interface IDynamicTypeSerializer : ITypeSerializer
	{
		// Token: 0x06000098 RID: 152
		void GenerateWriterMethod(Type type, CodeGenContext ctx, ILGenerator il);

		// Token: 0x06000099 RID: 153
		void GenerateReaderMethod(Type type, CodeGenContext ctx, ILGenerator il);
	}
}
