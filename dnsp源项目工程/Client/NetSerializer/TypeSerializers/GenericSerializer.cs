using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace xClient.Core.NetSerializer.TypeSerializers
{
	// Token: 0x02000025 RID: 37
	public class GenericSerializer : ITypeSerializer, IDynamicTypeSerializer
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00007880 File Offset: 0x00005A80
		public bool Handles(Type type)
		{
			if (!type.IsSerializable)
			{
				throw new NotSupportedException(string.Format("Type {0} is not marked as Serializable", type.FullName));
			}
			if (typeof(ISerializable).IsAssignableFrom(type))
			{
				throw new NotSupportedException(string.Format("Cannot serialize {0}: ISerializable not supported", type.FullName));
			}
			return true;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000078D4 File Offset: 0x00005AD4
		public IEnumerable<Type> GetSubtypes(Type type)
		{
			GenericSerializer.<GetSubtypes>d__0 <GetSubtypes>d__ = new GenericSerializer.<GetSubtypes>d__0(-2);
			<GetSubtypes>d__.<>4__this = this;
			<GetSubtypes>d__.<>3__type = type;
			return <GetSubtypes>d__;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000078F8 File Offset: 0x00005AF8
		public void GenerateWriterMethod(Type type, CodeGenContext ctx, ILGenerator il)
		{
			IEnumerable<FieldInfo> fieldInfos = Helpers.GetFieldInfos(type);
			foreach (FieldInfo fieldInfo in fieldInfos)
			{
				Type fieldType = fieldInfo.FieldType;
				TypeData typeDataForCall = ctx.GetTypeDataForCall(fieldType);
				if (typeDataForCall.NeedsInstanceParameter)
				{
					il.Emit(OpCodes.Ldarg_0);
				}
				il.Emit(OpCodes.Ldarg_1);
				if (type.IsValueType)
				{
					il.Emit(OpCodes.Ldarga_S, 2);
				}
				else
				{
					il.Emit(OpCodes.Ldarg_2);
				}
				il.Emit(OpCodes.Ldfld, fieldInfo);
				il.Emit(OpCodes.Call, typeDataForCall.WriterMethodInfo);
			}
			il.Emit(OpCodes.Ret);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000079BC File Offset: 0x00005BBC
		public void GenerateReaderMethod(Type type, CodeGenContext ctx, ILGenerator il)
		{
			if (type.IsClass)
			{
				il.Emit(OpCodes.Ldarg_2);
				MethodInfo method = typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Static | BindingFlags.Public);
				MethodInfo method2 = typeof(FormatterServices).GetMethod("GetUninitializedObject", BindingFlags.Static | BindingFlags.Public);
				il.Emit(OpCodes.Ldtoken, type);
				il.Emit(OpCodes.Call, method);
				il.Emit(OpCodes.Call, method2);
				il.Emit(OpCodes.Castclass, type);
				il.Emit(OpCodes.Stind_Ref);
			}
			IEnumerable<FieldInfo> fieldInfos = Helpers.GetFieldInfos(type);
			foreach (FieldInfo fieldInfo in fieldInfos)
			{
				Type fieldType = fieldInfo.FieldType;
				TypeData typeDataForCall = ctx.GetTypeDataForCall(fieldType);
				if (typeDataForCall.NeedsInstanceParameter)
				{
					il.Emit(OpCodes.Ldarg_0);
				}
				il.Emit(OpCodes.Ldarg_1);
				il.Emit(OpCodes.Ldarg_2);
				if (type.IsClass)
				{
					il.Emit(OpCodes.Ldind_Ref);
				}
				il.Emit(OpCodes.Ldflda, fieldInfo);
				il.Emit(OpCodes.Call, typeDataForCall.ReaderMethodInfo);
			}
			if (typeof(IDeserializationCallback).IsAssignableFrom(type))
			{
				MethodInfo method3 = typeof(IDeserializationCallback).GetMethod("OnDeserialization", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
				{
					typeof(object)
				}, null);
				il.Emit(OpCodes.Ldarg_2);
				il.Emit(OpCodes.Ldnull);
				il.Emit(OpCodes.Constrained, type);
				il.Emit(OpCodes.Callvirt, method3);
			}
			il.Emit(OpCodes.Ret);
		}
	}
}
