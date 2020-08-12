using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace xClient.Core.NetSerializer
{
	// Token: 0x02000019 RID: 25
	public static class Primitives
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000065B8 File Offset: 0x000047B8
		public static MethodInfo GetWritePrimitive(Type type)
		{
			return typeof(Primitives).GetMethod("WritePrimitive", BindingFlags.Static | BindingFlags.Public | BindingFlags.ExactBinding, null, new Type[]
			{
				typeof(Stream),
				type
			}, null);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000065FC File Offset: 0x000047FC
		public static MethodInfo GetReaderPrimitive(Type type)
		{
			return typeof(Primitives).GetMethod("ReadPrimitive", BindingFlags.Static | BindingFlags.Public | BindingFlags.ExactBinding, null, new Type[]
			{
				typeof(Stream),
				type.MakeByRefType()
			}, null);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002582 File Offset: 0x00000782
		private static uint EncodeZigZag32(int n)
		{
			return (uint)(n << 1 ^ n >> 31);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000258C File Offset: 0x0000078C
		private static ulong EncodeZigZag64(long n)
		{
			return (ulong)(n << 1 ^ n >> 63);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002596 File Offset: 0x00000796
		private static int DecodeZigZag32(uint n)
		{
			return (int)(n >> 1 ^ -(int)(n & 1U));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000025A0 File Offset: 0x000007A0
		private static long DecodeZigZag64(ulong n)
		{
			return (long)(n >> 1 ^ -(long)(n & 1UL));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006644 File Offset: 0x00004844
		private static uint ReadVarint32(Stream stream)
		{
			int num = 0;
			for (int i = 0; i < 32; i += 7)
			{
				int num2 = stream.ReadByte();
				if (num2 == -1)
				{
					throw new EndOfStreamException();
				}
				num |= (num2 & 127) << i;
				if ((num2 & 128) == 0)
				{
					return (uint)num;
				}
			}
			throw new InvalidDataException();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000025B2 File Offset: 0x000007B2
		private static void WriteVarint32(Stream stream, uint value)
		{
			while (value >= 128U)
			{
				stream.WriteByte((byte)(value | 128U));
				value >>= 7;
			}
			stream.WriteByte((byte)value);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006690 File Offset: 0x00004890
		private static ulong ReadVarint64(Stream stream)
		{
			long num = 0L;
			for (int i = 0; i < 64; i += 7)
			{
				int num2 = stream.ReadByte();
				if (num2 == -1)
				{
					throw new EndOfStreamException();
				}
				num |= (long)(num2 & 127) << i;
				if ((num2 & 128) == 0)
				{
					return (ulong)num;
				}
			}
			throw new InvalidDataException();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000025DA File Offset: 0x000007DA
		private static void WriteVarint64(Stream stream, ulong value)
		{
			while (value >= 128UL)
			{
				stream.WriteByte((byte)(value | 128UL));
				value >>= 7;
			}
			stream.WriteByte((byte)value);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000260A File Offset: 0x0000080A
		public static void WritePrimitive(Stream stream, bool value)
		{
			stream.WriteByte(value ? 1 : 0);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000066E4 File Offset: 0x000048E4
		public static void ReadPrimitive(Stream stream, out bool value)
		{
			int num = stream.ReadByte();
			value = (num != 0);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002619 File Offset: 0x00000819
		public static void WritePrimitive(Stream stream, byte value)
		{
			stream.WriteByte(value);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002622 File Offset: 0x00000822
		public static void ReadPrimitive(Stream stream, out byte value)
		{
			value = (byte)stream.ReadByte();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000262D File Offset: 0x0000082D
		public static void WritePrimitive(Stream stream, sbyte value)
		{
			stream.WriteByte((byte)value);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002637 File Offset: 0x00000837
		public static void ReadPrimitive(Stream stream, out sbyte value)
		{
			value = (sbyte)stream.ReadByte();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002642 File Offset: 0x00000842
		public static void WritePrimitive(Stream stream, char value)
		{
			Primitives.WriteVarint32(stream, (uint)value);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000264B File Offset: 0x0000084B
		public static void ReadPrimitive(Stream stream, out char value)
		{
			value = (char)Primitives.ReadVarint32(stream);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002642 File Offset: 0x00000842
		public static void WritePrimitive(Stream stream, ushort value)
		{
			Primitives.WriteVarint32(stream, (uint)value);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000264B File Offset: 0x0000084B
		public static void ReadPrimitive(Stream stream, out ushort value)
		{
			value = (ushort)Primitives.ReadVarint32(stream);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002656 File Offset: 0x00000856
		public static void WritePrimitive(Stream stream, short value)
		{
			Primitives.WriteVarint32(stream, Primitives.EncodeZigZag32((int)value));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002664 File Offset: 0x00000864
		public static void ReadPrimitive(Stream stream, out short value)
		{
			value = (short)Primitives.DecodeZigZag32(Primitives.ReadVarint32(stream));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002642 File Offset: 0x00000842
		public static void WritePrimitive(Stream stream, uint value)
		{
			Primitives.WriteVarint32(stream, value);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002674 File Offset: 0x00000874
		public static void ReadPrimitive(Stream stream, out uint value)
		{
			value = Primitives.ReadVarint32(stream);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002656 File Offset: 0x00000856
		public static void WritePrimitive(Stream stream, int value)
		{
			Primitives.WriteVarint32(stream, Primitives.EncodeZigZag32(value));
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000267E File Offset: 0x0000087E
		public static void ReadPrimitive(Stream stream, out int value)
		{
			value = Primitives.DecodeZigZag32(Primitives.ReadVarint32(stream));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000268D File Offset: 0x0000088D
		public static void WritePrimitive(Stream stream, ulong value)
		{
			Primitives.WriteVarint64(stream, value);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002696 File Offset: 0x00000896
		public static void ReadPrimitive(Stream stream, out ulong value)
		{
			value = Primitives.ReadVarint64(stream);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000026A0 File Offset: 0x000008A0
		public static void WritePrimitive(Stream stream, long value)
		{
			Primitives.WriteVarint64(stream, Primitives.EncodeZigZag64(value));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000026AE File Offset: 0x000008AE
		public static void ReadPrimitive(Stream stream, out long value)
		{
			value = Primitives.DecodeZigZag64(Primitives.ReadVarint64(stream));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00006704 File Offset: 0x00004904
		public unsafe static void WritePrimitive(Stream stream, float value)
		{
			uint value2 = *(uint*)(&value);
			Primitives.WriteVarint32(stream, value2);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00006720 File Offset: 0x00004920
		public unsafe static void ReadPrimitive(Stream stream, out float value)
		{
			uint num = Primitives.ReadVarint32(stream);
			value = *(float*)(&num);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000673C File Offset: 0x0000493C
		public unsafe static void WritePrimitive(Stream stream, double value)
		{
			ulong value2 = (ulong)(*(long*)(&value));
			Primitives.WriteVarint64(stream, value2);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006758 File Offset: 0x00004958
		public unsafe static void ReadPrimitive(Stream stream, out double value)
		{
			ulong num = Primitives.ReadVarint64(stream);
			value = *(double*)(&num);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006774 File Offset: 0x00004974
		public static void WritePrimitive(Stream stream, DateTime value)
		{
			long value2 = value.ToBinary();
			Primitives.WritePrimitive(stream, value2);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006790 File Offset: 0x00004990
		public static void ReadPrimitive(Stream stream, out DateTime value)
		{
			long dateData;
			Primitives.ReadPrimitive(stream, out dateData);
			value = DateTime.FromBinary(dateData);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000067B4 File Offset: 0x000049B4
		public unsafe static void WritePrimitive(Stream stream, string value)
		{
			if (value == null)
			{
				Primitives.WritePrimitive(stream, 0U);
				return;
			}
			if (value.Length == 0)
			{
				Primitives.WritePrimitive(stream, 1U);
				return;
			}
			Primitives.StringHelper stringHelper = Primitives.s_stringHelper;
			if (stringHelper == null)
			{
				stringHelper = (Primitives.s_stringHelper = new Primitives.StringHelper());
			}
			Encoder encoder = stringHelper.Encoder;
			byte[] byteBuffer = stringHelper.ByteBuffer;
			int length = value.Length;
			int byteCount;
			fixed (char* chars = value)
			{
				byteCount = encoder.GetByteCount(chars, length, true);
			}
			Primitives.WritePrimitive(stream, (uint)(byteCount + 1));
			Primitives.WritePrimitive(stream, (uint)length);
			int num = 0;
			bool flag = false;
			while (!flag)
			{
				int num2;
				int count;
				fixed (char* ptr = value)
				{
					byte[] array;
					byte* ptr2;
					if ((array = byteBuffer) != null && array.Length != 0)
					{
						fixed (byte* ptr2 = &array[0])
						{
						}
					}
					else
					{
						ptr2 = null;
					}
					encoder.Convert(ptr + num, length - num, ptr2, byteBuffer.Length, true, out num2, out count, out flag);
					ptr2 = null;
				}
				stream.Write(byteBuffer, 0, count);
				num += num2;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000068A8 File Offset: 0x00004AA8
		public static void ReadPrimitive(Stream stream, out string value)
		{
			uint num;
			Primitives.ReadPrimitive(stream, out num);
			if (num == 0U)
			{
				value = null;
				return;
			}
			if (num == 1U)
			{
				value = string.Empty;
				return;
			}
			num -= 1U;
			uint num2;
			Primitives.ReadPrimitive(stream, out num2);
			Primitives.StringHelper stringHelper = Primitives.s_stringHelper;
			if (stringHelper == null)
			{
				stringHelper = (Primitives.s_stringHelper = new Primitives.StringHelper());
			}
			Decoder decoder = stringHelper.Decoder;
			byte[] byteBuffer = stringHelper.ByteBuffer;
			char[] array;
			if (num2 <= 128U)
			{
				array = stringHelper.CharBuffer;
			}
			else
			{
				array = new char[num2];
			}
			int i = (int)num;
			int num3 = 0;
			while (i > 0)
			{
				int num4 = stream.Read(byteBuffer, 0, Math.Min(byteBuffer.Length, i));
				if (num4 == 0)
				{
					throw new EndOfStreamException();
				}
				i -= num4;
				bool flush = i == 0;
				bool flag = false;
				int num5 = 0;
				while (!flag)
				{
					int num6;
					int num7;
					decoder.Convert(byteBuffer, num5, num4 - num5, array, num3, (int)(num2 - (uint)num3), flush, out num6, out num7, out flag);
					num5 += num6;
					num3 += num7;
				}
			}
			value = new string(array, 0, (int)num2);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000026BD File Offset: 0x000008BD
		public static void WritePrimitive(Stream stream, byte[] value)
		{
			if (value == null)
			{
				Primitives.WritePrimitive(stream, 0U);
				return;
			}
			Primitives.WritePrimitive(stream, (uint)(value.Length + 1));
			stream.Write(value, 0, value.Length);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000069A0 File Offset: 0x00004BA0
		public static void ReadPrimitive(Stream stream, out byte[] value)
		{
			uint num;
			Primitives.ReadPrimitive(stream, out num);
			if (num == 0U)
			{
				value = null;
				return;
			}
			if (num == 1U)
			{
				value = Primitives.s_emptyByteArray;
				return;
			}
			num -= 1U;
			value = new byte[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				int num3 = stream.Read(value, num2, (int)(num - (uint)num2));
				if (num3 == 0)
				{
					throw new EndOfStreamException();
				}
				num2 += num3;
			}
		}

		// Token: 0x0400003C RID: 60
		[ThreadStatic]
		private static Primitives.StringHelper s_stringHelper;

		// Token: 0x0400003D RID: 61
		private static readonly byte[] s_emptyByteArray = new byte[0];

		// Token: 0x0200001A RID: 26
		private sealed class StringHelper
		{
			// Token: 0x060000C3 RID: 195 RVA: 0x000026ED File Offset: 0x000008ED
			public StringHelper()
			{
				this.Encoding = new UTF8Encoding(false, true);
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002702 File Offset: 0x00000902
			// (set) Token: 0x060000C5 RID: 197 RVA: 0x0000270A File Offset: 0x0000090A
			public UTF8Encoding Encoding { get; private set; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002713 File Offset: 0x00000913
			public Encoder Encoder
			{
				get
				{
					if (this.m_encoder == null)
					{
						this.m_encoder = this.Encoding.GetEncoder();
					}
					return this.m_encoder;
				}
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002734 File Offset: 0x00000934
			public Decoder Decoder
			{
				get
				{
					if (this.m_decoder == null)
					{
						this.m_decoder = this.Encoding.GetDecoder();
					}
					return this.m_decoder;
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002755 File Offset: 0x00000955
			public byte[] ByteBuffer
			{
				get
				{
					if (this.m_byteBuffer == null)
					{
						this.m_byteBuffer = new byte[256];
					}
					return this.m_byteBuffer;
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002775 File Offset: 0x00000975
			public char[] CharBuffer
			{
				get
				{
					if (this.m_charBuffer == null)
					{
						this.m_charBuffer = new char[128];
					}
					return this.m_charBuffer;
				}
			}

			// Token: 0x0400003E RID: 62
			public const int BYTEBUFFERLEN = 256;

			// Token: 0x0400003F RID: 63
			public const int CHARBUFFERLEN = 128;

			// Token: 0x04000040 RID: 64
			private Encoder m_encoder;

			// Token: 0x04000041 RID: 65
			private Decoder m_decoder;

			// Token: 0x04000042 RID: 66
			private byte[] m_byteBuffer;

			// Token: 0x04000043 RID: 67
			private char[] m_charBuffer;
		}
	}
}
