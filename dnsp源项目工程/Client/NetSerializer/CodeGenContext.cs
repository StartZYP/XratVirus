using System;
using System.Collections.Generic;
using System.Reflection;

namespace xClient.Core.NetSerializer
{
	// Token: 0x02000014 RID: 20
	public sealed class CodeGenContext
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00006390 File Offset: 0x00004590
		public CodeGenContext(Dictionary<Type, TypeData> typeMap)
		{
			this.m_typeMap = typeMap;
			TypeData typeData = this.m_typeMap[typeof(object)];
			this.SerializerSwitchMethodInfo = typeData.WriterMethodInfo;
			this.DeserializerSwitchMethodInfo = typeData.ReaderMethodInfo;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000024D8 File Offset: 0x000006D8
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000024E0 File Offset: 0x000006E0
		public MethodInfo SerializerSwitchMethodInfo { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000024E9 File Offset: 0x000006E9
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000024F1 File Offset: 0x000006F1
		public MethodInfo DeserializerSwitchMethodInfo { get; private set; }

		// Token: 0x06000088 RID: 136 RVA: 0x000024FA File Offset: 0x000006FA
		public MethodInfo GetWriterMethodInfo(Type type)
		{
			return this.m_typeMap[type].WriterMethodInfo;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000250D File Offset: 0x0000070D
		public MethodInfo GetReaderMethodInfo(Type type)
		{
			return this.m_typeMap[type].ReaderMethodInfo;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002520 File Offset: 0x00000720
		public bool IsGenerated(Type type)
		{
			return this.m_typeMap[type].IsGenerated;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002533 File Offset: 0x00000733
		public IDictionary<Type, TypeData> TypeMap
		{
			get
			{
				return this.m_typeMap;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000063D8 File Offset: 0x000045D8
		private bool CanCallDirect(Type type)
		{
			return type.IsValueType || type.IsArray || (type.IsSealed && !this.IsGenerated(type));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000253B File Offset: 0x0000073B
		public TypeData GetTypeData(Type type)
		{
			return this.m_typeMap[type];
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006414 File Offset: 0x00004614
		public TypeData GetTypeDataForCall(Type type)
		{
			if (!this.CanCallDirect(type))
			{
				type = typeof(object);
			}
			return this.GetTypeData(type);
		}

		// Token: 0x04000036 RID: 54
		private readonly Dictionary<Type, TypeData> m_typeMap;
	}
}
