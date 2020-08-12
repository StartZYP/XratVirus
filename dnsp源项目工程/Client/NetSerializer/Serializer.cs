using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using xClient.Core.NetSerializer.TypeSerializers;

namespace xClient.Core.NetSerializer
{
	// Token: 0x0200001B RID: 27
	public class Serializer
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00002795 File Offset: 0x00000995
		public Serializer(IEnumerable<Type> rootTypes) : this(rootTypes, new ITypeSerializer[0])
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000069FC File Offset: 0x00004BFC
		public Serializer(IEnumerable<Type> rootTypes, ITypeSerializer[] userTypeSerializers)
		{
			if (!userTypeSerializers.All((ITypeSerializer s) => s is IDynamicTypeSerializer || s is IStaticTypeSerializer))
			{
				throw new ArgumentException("TypeSerializers have to implement IDynamicTypeSerializer or  IStaticTypeSerializer");
			}
			this.m_userTypeSerializers = userTypeSerializers;
			Dictionary<Type, TypeData> dictionary = this.GenerateTypeData(rootTypes);
			this.GenerateDynamic(dictionary);
			this.m_typeIDMap = dictionary.ToDictionary((KeyValuePair<Type, TypeData> kvp) => kvp.Key, (KeyValuePair<Type, TypeData> kvp) => kvp.Value.TypeID);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000027A4 File Offset: 0x000009A4
		public void Serialize(Stream stream, object data)
		{
			this.m_serializerSwitch(this, stream, data);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006A9C File Offset: 0x00004C9C
		public object Deserialize(Stream stream)
		{
			object result;
			this.m_deserializerSwitch(this, stream, out result);
			return result;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00006ABC File Offset: 0x00004CBC
		private Dictionary<Type, TypeData> GenerateTypeData(IEnumerable<Type> rootTypes)
		{
			Dictionary<Type, TypeData> dictionary = new Dictionary<Type, TypeData>();
			Stack<Type> stack = new Stack<Type>(PrimitivesSerializer.GetSupportedTypes().Concat(rootTypes));
			stack.Push(typeof(object));
			ushort num = 1;
			while (stack.Count > 0)
			{
				Type type = stack.Pop();
				if (!dictionary.ContainsKey(type) && !type.IsAbstract && !type.IsInterface)
				{
					if (type.ContainsGenericParameters)
					{
						throw new NotSupportedException(string.Format("Type {0} contains generic parameters", type.FullName));
					}
					ITypeSerializer typeSerializer = this.m_userTypeSerializers.FirstOrDefault((ITypeSerializer h) => h.Handles(type));
					if (typeSerializer == null)
					{
						typeSerializer = Serializer.s_typeSerializers.FirstOrDefault((ITypeSerializer h) => h.Handles(type));
					}
					if (typeSerializer == null)
					{
						throw new NotSupportedException(string.Format("No serializer for {0}", type.FullName));
					}
					foreach (Type item in typeSerializer.GetSubtypes(type))
					{
						stack.Push(item);
					}
					TypeData value;
					if (typeSerializer is IStaticTypeSerializer)
					{
						IStaticTypeSerializer staticTypeSerializer = (IStaticTypeSerializer)typeSerializer;
						MethodInfo writer;
						MethodInfo reader;
						staticTypeSerializer.GetStaticMethods(type, out writer, out reader);
						ushort num2 = num;
						num = num2 + 1;
						value = new TypeData(num2, writer, reader);
					}
					else
					{
						if (!(typeSerializer is IDynamicTypeSerializer))
						{
							throw new Exception();
						}
						IDynamicTypeSerializer serializer = (IDynamicTypeSerializer)typeSerializer;
						ushort num3 = num;
						num = num3 + 1;
						value = new TypeData(num3, serializer);
					}
					dictionary[type] = value;
				}
			}
			return dictionary;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006C8C File Offset: 0x00004E8C
		private void GenerateDynamic(Dictionary<Type, TypeData> map)
		{
			foreach (KeyValuePair<Type, TypeData> keyValuePair in map)
			{
				Type key = keyValuePair.Key;
				TypeData value = keyValuePair.Value;
				if (value.IsGenerated)
				{
					value.WriterMethodInfo = Helpers.GenerateDynamicSerializerStub(key);
					value.ReaderMethodInfo = Helpers.GenerateDynamicDeserializerStub(key);
				}
			}
			CodeGenContext codeGenContext = new CodeGenContext(map);
			foreach (KeyValuePair<Type, TypeData> keyValuePair2 in map)
			{
				Type key2 = keyValuePair2.Key;
				TypeData value2 = keyValuePair2.Value;
				if (value2.IsGenerated)
				{
					DynamicMethod dynamicMethod = (DynamicMethod)value2.WriterMethodInfo;
					value2.TypeSerializer.GenerateWriterMethod(key2, codeGenContext, dynamicMethod.GetILGenerator());
					DynamicMethod dynamicMethod2 = (DynamicMethod)value2.ReaderMethodInfo;
					value2.TypeSerializer.GenerateReaderMethod(key2, codeGenContext, dynamicMethod2.GetILGenerator());
				}
			}
			DynamicMethod dynamicMethod3 = (DynamicMethod)codeGenContext.GetWriterMethodInfo(typeof(object));
			DynamicMethod dynamicMethod4 = (DynamicMethod)codeGenContext.GetReaderMethodInfo(typeof(object));
			this.m_serializerSwitch = (Serializer.SerializerSwitch)dynamicMethod3.CreateDelegate(typeof(Serializer.SerializerSwitch));
			this.m_deserializerSwitch = (Serializer.DeserializerSwitch)dynamicMethod4.CreateDelegate(typeof(Serializer.DeserializerSwitch));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006E10 File Offset: 0x00005010
		private ushort GetTypeID(object ob)
		{
			if (ob == null)
			{
				return 0;
			}
			Type type = ob.GetType();
			ushort result;
			if (!this.m_typeIDMap.TryGetValue(type, out result))
			{
				throw new InvalidOperationException(string.Format("Unknown type {0}", type.FullName));
			}
			return result;
		}

		// Token: 0x04000045 RID: 69
		private Dictionary<Type, ushort> m_typeIDMap;

		// Token: 0x04000046 RID: 70
		private Serializer.SerializerSwitch m_serializerSwitch;

		// Token: 0x04000047 RID: 71
		private Serializer.DeserializerSwitch m_deserializerSwitch;

		// Token: 0x04000048 RID: 72
		private static ITypeSerializer[] s_typeSerializers = new ITypeSerializer[]
		{
			new ObjectSerializer(),
			new PrimitivesSerializer(),
			new ArraySerializer(),
			new EnumSerializer(),
			new DictionarySerializer(),
			new GenericSerializer()
		};

		// Token: 0x04000049 RID: 73
		private ITypeSerializer[] m_userTypeSerializers;

		// Token: 0x0200001C RID: 28
		// (Invoke) Token: 0x060000D6 RID: 214
		private delegate void SerializerSwitch(Serializer serializer, Stream stream, object ob);

		// Token: 0x0200001D RID: 29
		// (Invoke) Token: 0x060000DA RID: 218
		private delegate void DeserializerSwitch(Serializer serializer, Stream stream, out object ob);
	}
}
