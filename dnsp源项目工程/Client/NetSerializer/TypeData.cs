using System;
using System.Reflection;

namespace xClient.Core.NetSerializer
{
	// Token: 0x02000013 RID: 19
	public sealed class TypeData
	{
		// Token: 0x0600007E RID: 126 RVA: 0x0000246E File Offset: 0x0000066E
		public TypeData(ushort typeID, IDynamicTypeSerializer serializer)
		{
			this.TypeID = typeID;
			this.TypeSerializer = serializer;
			this.NeedsInstanceParameter = true;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000248B File Offset: 0x0000068B
		public TypeData(ushort typeID, MethodInfo writer, MethodInfo reader)
		{
			this.TypeID = typeID;
			this.WriterMethodInfo = writer;
			this.ReaderMethodInfo = reader;
			this.NeedsInstanceParameter = (writer.GetParameters().Length == 3);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000024B9 File Offset: 0x000006B9
		public bool IsGenerated
		{
			get
			{
				return this.TypeSerializer != null;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000024C7 File Offset: 0x000006C7
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000024CF File Offset: 0x000006CF
		public bool NeedsInstanceParameter { get; private set; }

		// Token: 0x04000031 RID: 49
		public readonly ushort TypeID;

		// Token: 0x04000032 RID: 50
		public readonly IDynamicTypeSerializer TypeSerializer;

		// Token: 0x04000033 RID: 51
		public MethodInfo WriterMethodInfo;

		// Token: 0x04000034 RID: 52
		public MethodInfo ReaderMethodInfo;
	}
}
