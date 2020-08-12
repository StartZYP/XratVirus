using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace xClient.Core.NetSerializer.TypeSerializers
{
	// Token: 0x0200001F RID: 31
	internal class ArraySerializer : ITypeSerializer, IDynamicTypeSerializer
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x000027EE File Offset: 0x000009EE
		public bool Handles(Type type)
		{
			if (!type.IsArray)
			{
				return false;
			}
			if (type.GetArrayRank() != 1)
			{
				throw new NotSupportedException(string.Format("Multi-dim arrays not supported: {0}", type.FullName));
			}
			return true;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006E9C File Offset: 0x0000509C
		public IEnumerable<Type> GetSubtypes(Type type)
		{
			yield return type.GetElementType();
			yield break;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006EC0 File Offset: 0x000050C0
		public void GenerateWriterMethod(Type type, CodeGenContext ctx, ILGenerator il)
		{
			Type elementType = type.GetElementType();
			Label label = il.DefineLabel();
			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Brtrue_S, label);
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Ldc_I4_0);
			il.Emit(OpCodes.Tailcall);
			il.Emit(OpCodes.Call, ctx.GetWriterMethodInfo(typeof(uint)));
			il.Emit(OpCodes.Ret);
			il.MarkLabel(label);
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Ldlen);
			il.Emit(OpCodes.Ldc_I4_1);
			il.Emit(OpCodes.Add);
			il.Emit(OpCodes.Call, ctx.GetWriterMethodInfo(typeof(uint)));
			LocalBuilder local = il.DeclareLocal(typeof(int));
			il.Emit(OpCodes.Ldc_I4_0);
			il.Emit(OpCodes.Stloc_S, local);
			Label label2 = il.DefineLabel();
			Label label3 = il.DefineLabel();
			il.Emit(OpCodes.Br_S, label3);
			il.MarkLabel(label2);
			TypeData typeDataForCall = ctx.GetTypeDataForCall(elementType);
			if (typeDataForCall.NeedsInstanceParameter)
			{
				il.Emit(OpCodes.Ldarg_0);
			}
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Ldloc_S, local);
			il.Emit(OpCodes.Ldelem, elementType);
			il.Emit(OpCodes.Call, typeDataForCall.WriterMethodInfo);
			il.Emit(OpCodes.Ldloc_S, local);
			il.Emit(OpCodes.Ldc_I4_1);
			il.Emit(OpCodes.Add);
			il.Emit(OpCodes.Stloc_S, local);
			il.MarkLabel(label3);
			il.Emit(OpCodes.Ldloc_S, local);
			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Ldlen);
			il.Emit(OpCodes.Conv_I4);
			il.Emit(OpCodes.Clt);
			il.Emit(OpCodes.Brtrue_S, label2);
			il.Emit(OpCodes.Ret);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000070C0 File Offset: 0x000052C0
		public void GenerateReaderMethod(Type type, CodeGenContext ctx, ILGenerator il)
		{
			Type elementType = type.GetElementType();
			LocalBuilder local = il.DeclareLocal(typeof(uint));
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Ldloca_S, local);
			il.Emit(OpCodes.Call, ctx.GetReaderMethodInfo(typeof(uint)));
			Label label = il.DefineLabel();
			il.Emit(OpCodes.Ldloc_S, local);
			il.Emit(OpCodes.Brtrue_S, label);
			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Ldnull);
			il.Emit(OpCodes.Stind_Ref);
			il.Emit(OpCodes.Ret);
			il.MarkLabel(label);
			LocalBuilder local2 = il.DeclareLocal(type);
			il.Emit(OpCodes.Ldloc_S, local);
			il.Emit(OpCodes.Ldc_I4_1);
			il.Emit(OpCodes.Sub);
			il.Emit(OpCodes.Newarr, elementType);
			il.Emit(OpCodes.Stloc_S, local2);
			LocalBuilder local3 = il.DeclareLocal(typeof(int));
			il.Emit(OpCodes.Ldc_I4_0);
			il.Emit(OpCodes.Stloc_S, local3);
			Label label2 = il.DefineLabel();
			Label label3 = il.DefineLabel();
			il.Emit(OpCodes.Br_S, label3);
			il.MarkLabel(label2);
			TypeData typeDataForCall = ctx.GetTypeDataForCall(elementType);
			if (typeDataForCall.NeedsInstanceParameter)
			{
				il.Emit(OpCodes.Ldarg_0);
			}
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Ldloc_S, local2);
			il.Emit(OpCodes.Ldloc_S, local3);
			il.Emit(OpCodes.Ldelema, elementType);
			il.Emit(OpCodes.Call, typeDataForCall.ReaderMethodInfo);
			il.Emit(OpCodes.Ldloc_S, local3);
			il.Emit(OpCodes.Ldc_I4_1);
			il.Emit(OpCodes.Add);
			il.Emit(OpCodes.Stloc_S, local3);
			il.MarkLabel(label3);
			il.Emit(OpCodes.Ldloc_S, local3);
			il.Emit(OpCodes.Ldloc_S, local2);
			il.Emit(OpCodes.Ldlen);
			il.Emit(OpCodes.Conv_I4);
			il.Emit(OpCodes.Clt);
			il.Emit(OpCodes.Brtrue_S, label2);
			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Ldloc_S, local2);
			il.Emit(OpCodes.Stind_Ref);
			il.Emit(OpCodes.Ret);
		}
	}
}
