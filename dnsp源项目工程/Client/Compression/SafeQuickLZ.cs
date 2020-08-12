using System;

namespace xClient.Core.Compression
{
	// Token: 0x0200003A RID: 58
	public static class SafeQuickLZ
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00002BEA File Offset: 0x00000DEA
		private static int HeaderLength(byte[] source)
		{
			if ((source[0] & 2) != 2)
			{
				return 3;
			}
			return 9;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public static int SizeDecompressed(byte[] source)
		{
			if (SafeQuickLZ.HeaderLength(source) == 9)
			{
				return (int)source[5] | (int)source[6] << 8 | (int)source[7] << 16 | (int)source[8] << 24;
			}
			return (int)source[2];
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00002C1F File Offset: 0x00000E1F
		public static int SizeCompressed(byte[] source)
		{
			if (SafeQuickLZ.HeaderLength(source) == 9)
			{
				return (int)source[1] | (int)source[2] << 8 | (int)source[3] << 16 | (int)source[4] << 24;
			}
			return (int)source[1];
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000B104 File Offset: 0x00009304
		private static void WriteHeader(byte[] dst, int level, bool compressible, int sizeCompressed, int sizeDecompressed)
		{
			dst[0] = (byte)(2 | (compressible ? 1 : 0));
			int num = 0;
			dst[num] |= (byte)(level << 2);
			int num2 = 0;
			dst[num2] |= 64;
			int num3 = 0;
			dst[num3] = dst[num3];
			SafeQuickLZ.FastWrite(dst, 1, sizeDecompressed, 4);
			SafeQuickLZ.FastWrite(dst, 5, sizeCompressed, 4);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000B174 File Offset: 0x00009374
		public static byte[] Compress(byte[] source, int level = 3)
		{
			if (source.Length == 0)
			{
				return new byte[0];
			}
			switch (level)
			{
			case 1:
			{
				int[,] array = new int[4096, 1];
				goto IL_4C;
			}
			case 3:
			{
				int[,] array = new int[4096, 16];
				goto IL_4C;
			}
			}
			throw new ArgumentException("C# version only supports level 1 and 3");
			IL_4C:
			int i = 0;
			int num = 13;
			uint num2 = 2147483648U;
			int i2 = 9;
			byte[] array2 = new byte[source.Length + 400];
			int[] array3 = new int[4096];
			byte[] array4 = new byte[4096];
			int num3 = 0;
			int num4 = source.Length - 6 - 4 - 1;
			int num5 = 0;
			if (0 <= num4)
			{
				num3 = ((int)source[i] | (int)source[i + 1] << 8 | (int)source[i + 2] << 16);
			}
			byte[] array5;
			while (i <= num4)
			{
				if ((num2 & 1U) == 1U)
				{
					if (i > source.Length >> 1 && num > i - (i >> 5))
					{
						array5 = new byte[source.Length + 9];
						SafeQuickLZ.WriteHeader(array5, level, false, source.Length, source.Length + 9);
						Array.Copy(source, 0, array5, 9, source.Length);
						return array5;
					}
					SafeQuickLZ.FastWrite(array2, i2, (int)(num2 >> 1 | 2147483648U), 4);
					i2 = num;
					num += 4;
					num2 = 2147483648U;
				}
				if (level == 1)
				{
					int num6 = (num3 >> 12 ^ num3) & 4095;
					int[,] array;
					int num7 = array[num6, 0];
					int num8 = array3[num6] ^ num3;
					array3[num6] = num3;
					array[num6, 0] = i;
					if (num8 == 0 && array4[num6] != 0 && (i - num7 > 2 || (i == num7 + 1 && num5 >= 3 && i > 3 && source[i] == source[i - 3] && source[i] == source[i - 2] && source[i] == source[i - 1] && source[i] == source[i + 1] && source[i] == source[i + 2])))
					{
						num2 = (num2 >> 1 | 2147483648U);
						if (source[num7 + 3] != source[i + 3])
						{
							int num9 = 1 | num6 << 4;
							array2[num] = (byte)num9;
							array2[num + 1] = (byte)(num9 >> 8);
							i += 3;
							num += 2;
						}
						else
						{
							int num10 = i;
							int num11 = (source.Length - 4 - i + 1 - 1 > 255) ? 255 : (source.Length - 4 - i + 1 - 1);
							i += 4;
							if (source[num7 + i - num10] == source[i])
							{
								i++;
								if (source[num7 + i - num10] == source[i])
								{
									i++;
									while (source[num7 + (i - num10)] == source[i] && i - num10 < num11)
									{
										i++;
									}
								}
							}
							int num12 = i - num10;
							num6 <<= 4;
							if (num12 < 18)
							{
								int num13 = num6 | num12 - 2;
								array2[num] = (byte)num13;
								array2[num + 1] = (byte)(num13 >> 8);
								num += 2;
							}
							else
							{
								SafeQuickLZ.FastWrite(array2, num, num6 | num12 << 16, 3);
								num += 3;
							}
						}
						num3 = ((int)source[i] | (int)source[i + 1] << 8 | (int)source[i + 2] << 16);
						num5 = 0;
					}
					else
					{
						num5++;
						array4[num6] = 1;
						array2[num] = source[i];
						num2 >>= 1;
						i++;
						num++;
						num3 = ((num3 >> 8 & 65535) | (int)source[i + 2] << 16);
					}
				}
				else
				{
					num3 = ((int)source[i] | (int)source[i + 1] << 8 | (int)source[i + 2] << 16);
					int num14 = (source.Length - 4 - i + 1 - 1 > 255) ? 255 : (source.Length - 4 - i + 1 - 1);
					int num15 = (num3 >> 12 ^ num3) & 4095;
					byte b = array4[num15];
					int num16 = 0;
					int num17 = 0;
					int num18 = 0;
					int[,] array;
					int num19;
					while (num18 < 16 && (int)b > num18)
					{
						num19 = array[num15, num18];
						if ((byte)num3 == source[num19] && (byte)(num3 >> 8) == source[num19 + 1] && (byte)(num3 >> 16) == source[num19 + 2] && num19 < i - 2)
						{
							int num20 = 3;
							while (source[num19 + num20] == source[i + num20] && num20 < num14)
							{
								num20++;
							}
							if (num20 > num16 || (num20 == num16 && num19 > num17))
							{
								num17 = num19;
								num16 = num20;
							}
						}
						num18++;
					}
					num19 = num17;
					array[num15, (int)(b & 15)] = i;
					b += 1;
					array4[num15] = b;
					if (num16 >= 3 && i - num19 < 131071)
					{
						int num21 = i - num19;
						for (int j = 1; j < num16; j++)
						{
							num3 = ((int)source[i + j] | (int)source[i + j + 1] << 8 | (int)source[i + j + 2] << 16);
							num15 = ((num3 >> 12 ^ num3) & 4095);
							byte[] array6 = array4;
							int num22 = num15;
							byte b2;
							array6[num22] = (b2 = array6[num22]) + 1;
							b = b2;
							array[num15, (int)(b & 15)] = i + j;
						}
						i += num16;
						num2 = (num2 >> 1 | 2147483648U);
						if (num16 == 3 && num21 <= 63)
						{
							SafeQuickLZ.FastWrite(array2, num, num21 << 2, 1);
							num++;
						}
						else if (num16 == 3 && num21 <= 16383)
						{
							SafeQuickLZ.FastWrite(array2, num, num21 << 2 | 1, 2);
							num += 2;
						}
						else if (num16 <= 18 && num21 <= 1023)
						{
							SafeQuickLZ.FastWrite(array2, num, num16 - 3 << 2 | num21 << 6 | 2, 2);
							num += 2;
						}
						else if (num16 <= 33)
						{
							SafeQuickLZ.FastWrite(array2, num, num16 - 2 << 2 | num21 << 7 | 3, 3);
							num += 3;
						}
						else
						{
							SafeQuickLZ.FastWrite(array2, num, num16 - 3 << 7 | num21 << 15 | 3, 4);
							num += 4;
						}
						num5 = 0;
					}
					else
					{
						array2[num] = source[i];
						num2 >>= 1;
						i++;
						num++;
					}
				}
			}
			while (i <= source.Length - 1)
			{
				if ((num2 & 1U) == 1U)
				{
					SafeQuickLZ.FastWrite(array2, i2, (int)(num2 >> 1 | 2147483648U), 4);
					i2 = num;
					num += 4;
					num2 = 2147483648U;
				}
				array2[num] = source[i];
				i++;
				num++;
				num2 >>= 1;
			}
			while ((num2 & 1U) != 1U)
			{
				num2 >>= 1;
			}
			SafeQuickLZ.FastWrite(array2, i2, (int)(num2 >> 1 | 2147483648U), 4);
			SafeQuickLZ.WriteHeader(array2, level, true, source.Length, num);
			array5 = new byte[num];
			Array.Copy(array2, array5, num);
			return array5;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000B794 File Offset: 0x00009994
		private static void FastWrite(byte[] a, int i, int value, int numbytes)
		{
			for (int j = 0; j < numbytes; j++)
			{
				a[i + j] = (byte)(value >> j * 8);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000B7BC File Offset: 0x000099BC
		public static byte[] Decompress(byte[] source)
		{
			if (source.Length == 0)
			{
				return new byte[0];
			}
			int num = source[0] >> 2 & 3;
			if (num != 1 && num != 3)
			{
				throw new ArgumentException("C# version only supports level 1 and 3");
			}
			int num2 = SafeQuickLZ.SizeDecompressed(source);
			int num3 = SafeQuickLZ.HeaderLength(source);
			int i = 0;
			uint num4 = 1U;
			byte[] array = new byte[num2];
			int[] array2 = new int[4096];
			byte[] array3 = new byte[4096];
			int num5 = num2 - 6 - 4 - 1;
			int j = -1;
			uint num6 = 0U;
			if ((source[0] & 1) != 1)
			{
				byte[] array4 = new byte[num2];
				Array.Copy(source, SafeQuickLZ.HeaderLength(source), array4, 0, num2);
				return array4;
			}
			for (;;)
			{
				if (num4 == 1U)
				{
					num4 = (uint)((int)source[num3] | (int)source[num3 + 1] << 8 | (int)source[num3 + 2] << 16 | (int)source[num3 + 3] << 24);
					num3 += 4;
					if (i <= num5)
					{
						if (num == 1)
						{
							num6 = (uint)((int)source[num3] | (int)source[num3 + 1] << 8 | (int)source[num3 + 2] << 16);
						}
						else
						{
							num6 = (uint)((int)source[num3] | (int)source[num3 + 1] << 8 | (int)source[num3 + 2] << 16 | (int)source[num3 + 3] << 24);
						}
					}
				}
				if ((num4 & 1U) == 1U)
				{
					num4 >>= 1;
					uint num8;
					uint num9;
					if (num == 1)
					{
						int num7 = (int)num6 >> 4 & 4095;
						num8 = (uint)array2[num7];
						if ((num6 & 15U) != 0U)
						{
							num9 = (num6 & 15U) + 2U;
							num3 += 2;
						}
						else
						{
							num9 = (uint)source[num3 + 2];
							num3 += 3;
						}
					}
					else
					{
						uint num10;
						if ((num6 & 3U) == 0U)
						{
							num10 = (num6 & 255U) >> 2;
							num9 = 3U;
							num3++;
						}
						else if ((num6 & 2U) == 0U)
						{
							num10 = (num6 & 65535U) >> 2;
							num9 = 3U;
							num3 += 2;
						}
						else if ((num6 & 1U) == 0U)
						{
							num10 = (num6 & 65535U) >> 6;
							num9 = (num6 >> 2 & 15U) + 3U;
							num3 += 2;
						}
						else if ((num6 & 127U) != 3U)
						{
							num10 = (num6 >> 7 & 131071U);
							num9 = (num6 >> 2 & 31U) + 2U;
							num3 += 3;
						}
						else
						{
							num10 = num6 >> 15;
							num9 = (num6 >> 7 & 255U) + 3U;
							num3 += 4;
						}
						num8 = (uint)((long)i - (long)((ulong)num10));
					}
					array[i] = array[(int)((UIntPtr)num8)];
					array[i + 1] = array[(int)((UIntPtr)(num8 + 1U))];
					array[i + 2] = array[(int)((UIntPtr)(num8 + 2U))];
					int num11 = 3;
					while ((long)num11 < (long)((ulong)num9))
					{
						array[i + num11] = array[(int)(checked((IntPtr)(unchecked((ulong)num8 + (ulong)((long)num11)))))];
						num11++;
					}
					i += (int)num9;
					if (num == 1)
					{
						num6 = (uint)((int)array[j + 1] | (int)array[j + 2] << 8 | (int)array[j + 3] << 16);
						while ((long)j < (long)i - (long)((ulong)num9))
						{
							j++;
							int num7 = (int)((num6 >> 12 ^ num6) & 4095U);
							array2[num7] = j;
							array3[num7] = 1;
							num6 = (uint)((ulong)(num6 >> 8 & 65535U) | (ulong)((long)((long)array[j + 3] << 16)));
						}
						num6 = (uint)((int)source[num3] | (int)source[num3 + 1] << 8 | (int)source[num3 + 2] << 16);
					}
					else
					{
						num6 = (uint)((int)source[num3] | (int)source[num3 + 1] << 8 | (int)source[num3 + 2] << 16 | (int)source[num3 + 3] << 24);
					}
					j = i - 1;
				}
				else
				{
					if (i > num5)
					{
						break;
					}
					array[i] = source[num3];
					i++;
					num3++;
					num4 >>= 1;
					if (num == 1)
					{
						while (j < i - 3)
						{
							j++;
							int num12 = (int)array[j] | (int)array[j + 1] << 8 | (int)array[j + 2] << 16;
							int num7 = (num12 >> 12 ^ num12) & 4095;
							array2[num7] = j;
							array3[num7] = 1;
						}
						num6 = (uint)((ulong)(num6 >> 8 & 65535U) | (ulong)((long)((long)source[num3 + 2] << 16)));
					}
					else
					{
						num6 = (uint)((ulong)(num6 >> 8 & 65535U) | (ulong)((long)((long)source[num3 + 2] << 16)) | (ulong)((long)((long)source[num3 + 3] << 24)));
					}
				}
			}
			while (i <= num2 - 1)
			{
				if (num4 == 1U)
				{
					num3 += 4;
					num4 = 2147483648U;
				}
				array[i] = source[num3];
				i++;
				num3++;
				num4 >>= 1;
			}
			return array;
		}

		// Token: 0x040000AF RID: 175
		public const int QLZ_VERSION_MAJOR = 1;

		// Token: 0x040000B0 RID: 176
		public const int QLZ_VERSION_MINOR = 5;

		// Token: 0x040000B1 RID: 177
		public const int QLZ_VERSION_REVISION = 0;

		// Token: 0x040000B2 RID: 178
		public const int QLZ_STREAMING_BUFFER = 0;

		// Token: 0x040000B3 RID: 179
		public const int QLZ_MEMORY_SAFE = 0;

		// Token: 0x040000B4 RID: 180
		private const int HASH_VALUES = 4096;

		// Token: 0x040000B5 RID: 181
		private const int MINOFFSET = 2;

		// Token: 0x040000B6 RID: 182
		private const int UNCONDITIONAL_MATCHLEN = 6;

		// Token: 0x040000B7 RID: 183
		private const int UNCOMPRESSED_END = 4;

		// Token: 0x040000B8 RID: 184
		private const int CWORD_LEN = 4;

		// Token: 0x040000B9 RID: 185
		private const int DEFAULT_HEADERLEN = 9;

		// Token: 0x040000BA RID: 186
		private const int QLZ_POINTERS_1 = 1;

		// Token: 0x040000BB RID: 187
		private const int QLZ_POINTERS_3 = 16;
	}
}
