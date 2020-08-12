using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace xClient.Core.NetSerializer.TypeSerializers
{
	// Token: 0x02000027 RID: 39
	internal class ObjectSerializer : ITypeSerializer, IDynamicTypeSerializer
	{
		// Token: 0x06000119 RID: 281 RVA: 0x0000293B File Offset: 0x00000B3B
		public bool Handles(Type type)
		{
			return type == typeof(object);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000294D File Offset: 0x00000B4D
		public IEnumerable<Type> GetSubtypes(Type type)
		{
			return new Type[0];
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007CC8 File Offset: 0x00005EC8
		public void GenerateWriterMethod(Type obtype, CodeGenContext ctx, ILGenerator il)
		{
			MethodInfo method = typeof(Serializer).GetMethod("GetTypeID", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
			{
				typeof(object)
			}, null);
			IDictionary<Type, TypeData> typeMap = ctx.TypeMap;
			LocalBuilder local = il.DeclareLocal(typeof(ushort));
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Call, method);
			il.Emit(OpCodes.Stloc_S, local);
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Ldloc_S, local);
			il.Emit(OpCodes.Call, ctx.GetWriterMethodInfo(typeof(ushort)));
			Label[] array = new Label[typeMap.Count + 1];
			array[0] = il.DefineLabel();
			foreach (KeyValuePair<Type, TypeData> keyValuePair in typeMap)
			{
				array[(int)keyValuePair.Value.TypeID] = il.DefineLabel();
			}
			il.Emit(OpCodes.Ldloc_S, local);
			il.Emit(OpCodes.Switch, array);
			il.Emit(OpCodes.Newobj, Helpers.ExceptionCtorInfo);
			il.Emit(OpCodes.Throw);
			il.MarkLabel(array[0]);
			il.Emit(OpCodes.Ret);
			foreach (KeyValuePair<Type, TypeData> keyValuePair2 in typeMap)
			{
				Type key = keyValuePair2.Key;
				TypeData value = keyValuePair2.Value;
				il.MarkLabel(array[(int)value.TypeID]);
				if (value.NeedsInstanceParameter)
				{
					il.Emit(OpCodes.Ldarg_0);
				}
				il.Emit(OpCodes.Ldarg_1);
				il.Emit(OpCodes.Ldarg_2);
				il.Emit(key.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, key);
				il.Emit(OpCodes.Tailcall);
				il.Emit(OpCodes.Call, value.WriterMethodInfo);
				il.Emit(OpCodes.Ret);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007F1C File Offset: 0x0000611C
		public void GenerateReaderMethod(Type obtype, CodeGenContext ctx, ILGenerator il)
		{
			IDictionary<Type, TypeData> typeMap = ctx.TypeMap;
			LocalBuilder local = il.DeclareLocal(typeof(ushort));
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Ldloca_S, local);
			il.Emit(OpCodes.Call, ctx.GetReaderMethodInfo(typeof(ushort)));
			Label[] array = new Label[typeMap.Count + 1];
			array[0] = il.DefineLabel();
			foreach (KeyValuePair<Type, TypeData> keyValuePair in typeMap)
			{
				array[(int)keyValuePair.Value.TypeID] = il.DefineLabel();
			}
			il.Emit(OpCodes.Ldloc_S, local);
			il.Emit(OpCodes.Switch, array);
			il.Emit(OpCodes.Newobj, Helpers.ExceptionCtorInfo);
			il.Emit(OpCodes.Throw);
			il.MarkLabel(array[0]);
			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Ldnull);
			il.Emit(OpCodes.Stind_Ref);
			il.Emit(OpCodes.Ret);
			foreach (KeyValuePair<Type, TypeData> keyValuePair2 in typeMap)
			{
				Type key = keyValuePair2.Key;
				TypeData value = keyValuePair2.Value;
				il.MarkLabel(array[(int)value.TypeID]);
				LocalBuilder localBuilder = il.DeclareLocal(key);
				if (value.NeedsInstanceParameter)
				{
					il.Emit(OpCodes.Ldarg_0);
				}
				il.Emit(OpCodes.Ldarg_1);
				if (localBuilder.LocalIndex < 256)
				{
					il.Emit(OpCodes.Ldloca_S, localBuilder);
				}
				else
				{
					il.Emit(OpCodes.Ldloca, localBuilder);
				}
				il.Emit(OpCodes.Call, value.ReaderMethodInfo);
				il.Emit(OpCodes.Ldarg_2);
				if (localBuilder.LocalIndex < 256)
				{
					il.Emit(OpCodes.Ldloc_S, localBuilder);
				}
				else
				{
					il.Emit(OpCodes.Ldloc, localBuilder);
				}
				if (key.IsValueType)
				{
					il.Emit(OpCodes.Box, key);
				}
				il.Emit(OpCodes.Stind_Ref);
				il.Emit(OpCodes.Ret);
			}
		}
	}
}
