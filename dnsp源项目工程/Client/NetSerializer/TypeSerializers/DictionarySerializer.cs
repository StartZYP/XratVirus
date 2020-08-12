using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace xClient.Core.NetSerializer.TypeSerializers
{
	// Token: 0x02000021 RID: 33
	public class DictionarySerializer : ITypeSerializer, IStaticTypeSerializer
	{
		// Token: 0x060000ED RID: 237 RVA: 0x000073AC File Offset: 0x000055AC
		public bool Handles(Type type)
		{
			if (!type.IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			return genericTypeDefinition == typeof(Dictionary<, >);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000073DC File Offset: 0x000055DC
		public IEnumerable<Type> GetSubtypes(Type type)
		{
			Type[] genArgs = type.GetGenericArguments();
			Type serializedType = typeof(KeyValuePair<, >).MakeGenericType(genArgs).MakeArrayType();
			yield return serializedType;
			yield break;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007400 File Offset: 0x00005600
		public void GetStaticMethods(Type type, out MethodInfo writer, out MethodInfo reader)
		{
			if (!type.IsGenericType)
			{
				throw new Exception();
			}
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			Type type2 = base.GetType();
			writer = DictionarySerializer.GetGenWriter(type2, genericTypeDefinition);
			reader = DictionarySerializer.GetGenReader(type2, genericTypeDefinition);
			Type[] genericArguments = type.GetGenericArguments();
			writer = writer.MakeGenericMethod(genericArguments);
			reader = reader.MakeGenericMethod(genericArguments);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00007458 File Offset: 0x00005658
		private static MethodInfo GetGenWriter(Type containerType, Type genType)
		{
			IEnumerable<MethodInfo> enumerable = from mi in containerType.GetMethods(BindingFlags.Static | BindingFlags.Public)
			where mi.IsGenericMethod && mi.Name == "WritePrimitive"
			select mi;
			foreach (MethodInfo methodInfo in enumerable)
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters.Length == 3 && !(parameters[1].ParameterType != typeof(Stream)))
				{
					Type parameterType = parameters[2].ParameterType;
					if (parameterType.IsGenericType)
					{
						Type genericTypeDefinition = parameterType.GetGenericTypeDefinition();
						if (genType == genericTypeDefinition)
						{
							return methodInfo;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000751C File Offset: 0x0000571C
		private static MethodInfo GetGenReader(Type containerType, Type genType)
		{
			IEnumerable<MethodInfo> enumerable = from mi in containerType.GetMethods(BindingFlags.Static | BindingFlags.Public)
			where mi.IsGenericMethod && mi.Name == "ReadPrimitive"
			select mi;
			foreach (MethodInfo methodInfo in enumerable)
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters.Length == 3 && !(parameters[1].ParameterType != typeof(Stream)))
				{
					Type type = parameters[2].ParameterType;
					if (type.IsByRef)
					{
						type = type.GetElementType();
						if (type.IsGenericType)
						{
							Type genericTypeDefinition = type.GetGenericTypeDefinition();
							if (genType == genericTypeDefinition)
							{
								return methodInfo;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000075F0 File Offset: 0x000057F0
		public static void WritePrimitive<TKey, TValue>(Serializer serializer, Stream stream, Dictionary<TKey, TValue> value)
		{
			KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[value.Count];
			int num = 0;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in value)
			{
				array[num++] = keyValuePair;
			}
			serializer.Serialize(stream, array);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007660 File Offset: 0x00005860
		public static void ReadPrimitive<TKey, TValue>(Serializer serializer, Stream stream, out Dictionary<TKey, TValue> value)
		{
			KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])serializer.Deserialize(stream);
			value = new Dictionary<TKey, TValue>(array.Length);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in array)
			{
				value.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}
