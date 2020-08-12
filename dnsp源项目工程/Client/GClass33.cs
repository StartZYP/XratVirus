using System;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x020000B6 RID: 182
public class GClass33
{
	// Token: 0x060004E4 RID: 1252 RVA: 0x00010D78 File Offset: 0x0000EF78
	public GClass33(string baseName)
	{
		if (File.Exists(baseName))
		{
			FileSystem.FileOpen(1, baseName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
			string s = Strings.Space((int)FileSystem.LOF(1));
			FileSystem.FileGet(1, ref s, -1L, false);
			FileSystem.FileClose(new int[]
			{
				1
			});
			this.byte_0 = Encoding.Default.GetBytes(s);
			if (Encoding.Default.GetString(this.byte_0, 0, 15).CompareTo("SQLite format 3") != 0)
			{
				throw new Exception("Not a valid SQLite 3 Database File");
			}
			if (this.byte_0[52] != 0)
			{
				throw new Exception("Auto-vacuum capable database is not supported");
			}
			this.ushort_0 = (ushort)this.method_0(16, 2);
			this.ulong_0 = this.method_0(56, 4);
			if (decimal.Compare(new decimal(this.ulong_0), 0m) == 0)
			{
				this.ulong_0 = 1UL;
			}
			this.method_8(100UL);
		}
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x00010E90 File Offset: 0x0000F090
	private ulong method_0(int startIndex, int Size)
	{
		if (Size > 8 | Size == 0)
		{
			return 0UL;
		}
		ulong num = 0UL;
		int num2 = Size - 1;
		for (int i = 0; i <= num2; i++)
		{
			num = (num << 8 | (ulong)this.byte_0[startIndex + i]);
		}
		return num;
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00010EE0 File Offset: 0x0000F0E0
	private long method_1(int startIndex, int endIndex)
	{
		endIndex++;
		byte[] array = new byte[8];
		int num = endIndex - startIndex;
		bool flag = false;
		if (num == 0 | num > 9)
		{
			return 0L;
		}
		if (num == 1)
		{
			array[0] = (this.byte_0[startIndex] & 127);
			return BitConverter.ToInt64(array, 0);
		}
		if (num == 9)
		{
			flag = true;
		}
		int num2 = 1;
		int num3 = 7;
		int num4 = 0;
		if (flag)
		{
			array[0] = this.byte_0[endIndex - 1];
			endIndex--;
			num4 = 1;
		}
		for (int i = endIndex - 1; i >= startIndex; i += -1)
		{
			if (i - 1 >= startIndex)
			{
				array[num4] = (byte)(((int)((byte)(this.byte_0[i] >> (num2 - 1 & 7))) & 255 >> num2) | (int)((byte)(this.byte_0[i - 1] << (num3 & 7))));
				num2++;
				num4++;
				num3--;
			}
			else if (!flag)
			{
				array[num4] = (byte)((int)((byte)(this.byte_0[i] >> (num2 - 1 & 7))) & 255 >> num2);
			}
		}
		return BitConverter.ToInt64(array, 0);
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00004654 File Offset: 0x00002854
	public int method_2()
	{
		return this.struct3_0.Length;
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x00010FF0 File Offset: 0x0000F1F0
	public string[] method_3()
	{
		string[] array = null;
		int num = 0;
		int num2 = this.struct2_0.Length - 1;
		for (int i = 0; i <= num2; i++)
		{
			if (this.struct2_0[i].string_0 == "table")
			{
				array = (string[])Utils.CopyArray(array, new string[num + 1]);
				array[num] = this.struct2_0[i].string_1;
				num++;
			}
		}
		return array;
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x0000465E File Offset: 0x0000285E
	public string method_4(int row_num, int field)
	{
		if (row_num >= this.struct3_0.Length)
		{
			return null;
		}
		if (field >= this.struct3_0[row_num].string_0.Length)
		{
			return null;
		}
		return this.struct3_0[row_num].string_0[field];
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00011064 File Offset: 0x0000F264
	public string method_5(int row_num, string field)
	{
		int num = -1;
		int num2 = this.string_0.Length - 1;
		int i = 0;
		while (i <= num2)
		{
			if (this.string_0[i].ToLower().CompareTo(field.ToLower()) != 0)
			{
				i++;
			}
			else
			{
				num = i;
				IL_37:
				if (num == -1)
				{
					return null;
				}
				return this.method_4(row_num, num);
			}
		}
		goto IL_37;
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x000110B8 File Offset: 0x0000F2B8
	private int method_6(int startIndex)
	{
		if (startIndex > this.byte_0.Length)
		{
			return 0;
		}
		int num = startIndex + 8;
		for (int i = startIndex; i <= num; i++)
		{
			if (i > this.byte_0.Length - 1)
			{
				return 0;
			}
			if ((this.byte_0[i] & 128) != 128)
			{
				return i;
			}
		}
		return startIndex + 8;
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x00004698 File Offset: 0x00002898
	private bool method_7(long value)
	{
		return (value & 1L) == 1L;
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00011110 File Offset: 0x0000F310
	private void method_8(ulong Offset)
	{
		if (this.byte_0[(int)Offset] == 13)
		{
			ushort num = Convert.ToUInt16(decimal.Subtract(new decimal(this.method_0(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
			int num2 = 0;
			if (this.struct2_0 != null)
			{
				num2 = this.struct2_0.Length;
				this.struct2_0 = (GClass33.Struct2[])Utils.CopyArray(this.struct2_0, new GClass33.Struct2[this.struct2_0.Length + (int)num + 1]);
			}
			else
			{
				this.struct2_0 = new GClass33.Struct2[(int)(num + 1)];
			}
			int num3 = (int)num;
			for (int i = 0; i <= num3; i++)
			{
				ulong num4 = this.method_0(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8m), new decimal(i * 2))), 2);
				if (decimal.Compare(new decimal(Offset), 100m) != 0)
				{
					num4 += Offset;
				}
				int num5 = this.method_6((int)num4);
				this.method_1((int)num4, num5);
				int num6 = this.method_6(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)));
				this.struct2_0[num2 + i].long_0 = this.method_1(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)), num6);
				num4 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num6), new decimal(num4))), 1m));
				num5 = this.method_6((int)num4);
				num6 = num5;
				long value = this.method_1((int)num4, num5);
				long[] array = new long[5];
				int num7 = 0;
				do
				{
					num5 = num6 + 1;
					num6 = this.method_6(num5);
					array[num7] = this.method_1(num5, num6);
					if (array[num7] > 9L)
					{
						if (this.method_7(array[num7]))
						{
							array[num7] = (long)Math.Round((double)(array[num7] - 13L) / 2.0);
						}
						else
						{
							array[num7] = (long)Math.Round((double)(array[num7] - 12L) / 2.0);
						}
					}
					else
					{
						array[num7] = (long)((ulong)this.byte_1[(int)array[num7]]);
					}
					num7++;
				}
				while (num7 <= 4);
				if (decimal.Compare(new decimal(this.ulong_0), 1m) == 0)
				{
					this.struct2_0[num2 + i].string_0 = Encoding.Default.GetString(this.byte_0, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
				}
				else if (decimal.Compare(new decimal(this.ulong_0), 2m) == 0)
				{
					this.struct2_0[num2 + i].string_0 = Encoding.Unicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
				}
				else if (decimal.Compare(new decimal(this.ulong_0), 3m) == 0)
				{
					this.struct2_0[num2 + i].string_0 = Encoding.BigEndianUnicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
				}
				if (decimal.Compare(new decimal(this.ulong_0), 1m) == 0)
				{
					this.struct2_0[num2 + i].string_1 = Encoding.Default.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
				}
				else if (decimal.Compare(new decimal(this.ulong_0), 2m) == 0)
				{
					this.struct2_0[num2 + i].string_1 = Encoding.Unicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
				}
				else if (decimal.Compare(new decimal(this.ulong_0), 3m) == 0)
				{
					this.struct2_0[num2 + i].string_1 = Encoding.BigEndianUnicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
				}
				this.struct2_0[num2 + i].long_1 = (long)this.method_0(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2]))), (int)array[3]);
				if (decimal.Compare(new decimal(this.ulong_0), 1m) == 0)
				{
					this.struct2_0[num2 + i].string_3 = Encoding.Default.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
				}
				else if (decimal.Compare(new decimal(this.ulong_0), 2m) == 0)
				{
					this.struct2_0[num2 + i].string_3 = Encoding.Unicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
				}
				else if (decimal.Compare(new decimal(this.ulong_0), 3m) == 0)
				{
					this.struct2_0[num2 + i].string_3 = Encoding.BigEndianUnicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
				}
			}
			return;
		}
		if (this.byte_0[(int)Offset] == 5)
		{
			ushort num8 = Convert.ToUInt16(decimal.Subtract(new decimal(this.method_0(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
			int num9 = (int)num8;
			for (int j = 0; j <= num9; j++)
			{
				ushort num10 = (ushort)this.method_0(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(j * 2))), 2);
				if (decimal.Compare(new decimal(Offset), 100m) == 0)
				{
					this.method_8(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.method_0((int)num10, 4)), 1m), new decimal((int)this.ushort_0))));
				}
				else
				{
					this.method_8(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.method_0((int)(Offset + (ulong)num10), 4)), 1m), new decimal((int)this.ushort_0))));
				}
			}
			this.method_8(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.method_0(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.ushort_0))));
		}
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0001196C File Offset: 0x0000FB6C
	public bool method_9(string TableName)
	{
		int num = -1;
		int num2 = this.struct2_0.Length - 1;
		int i = 0;
		while (i <= num2)
		{
			if (this.struct2_0[i].string_1.ToLower().CompareTo(TableName.ToLower()) != 0)
			{
				i++;
			}
			else
			{
				num = i;
				IL_40:
				if (num == -1)
				{
					return false;
				}
				string[] array = this.struct2_0[num].string_3.Substring(this.struct2_0[num].string_3.IndexOf("(") + 1).Split(new char[]
				{
					','
				});
				int num3 = array.Length - 1;
				for (int j = 0; j <= num3; j++)
				{
					array[j] = array[j].TrimStart(new char[0]);
					int num4 = array[j].IndexOf(" ");
					if (num4 > 0)
					{
						array[j] = array[j].Substring(0, num4);
					}
					if (array[j].IndexOf("UNIQUE") == 0)
					{
						break;
					}
					this.string_0 = (string[])Utils.CopyArray(this.string_0, new string[j + 1]);
					this.string_0[j] = array[j];
				}
				return this.method_10((ulong)((this.struct2_0[num].long_1 - 1L) * (long)((ulong)this.ushort_0)));
			}
		}
		goto IL_40;
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00011AC0 File Offset: 0x0000FCC0
	private bool method_10(ulong Offset)
	{
		if (this.byte_0[(int)Offset] == 13)
		{
			int num = Convert.ToInt32(decimal.Subtract(new decimal(this.method_0(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
			int num2 = 0;
			if (this.struct3_0 != null)
			{
				num2 = this.struct3_0.Length;
				this.struct3_0 = (GClass33.Struct3[])Utils.CopyArray(this.struct3_0, new GClass33.Struct3[this.struct3_0.Length + num + 1]);
			}
			else
			{
				this.struct3_0 = new GClass33.Struct3[num + 1];
			}
			int num3 = num;
			for (int i = 0; i <= num3; i++)
			{
				GClass33.Struct1[] array = null;
				ulong num4 = this.method_0(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8m), new decimal(i * 2))), 2);
				if (decimal.Compare(new decimal(Offset), 100m) != 0)
				{
					num4 += Offset;
				}
				int num5 = this.method_6((int)num4);
				this.method_1((int)num4, num5);
				int num6 = this.method_6(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)));
				this.struct3_0[num2 + i].long_0 = this.method_1(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)), num6);
				num4 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num6), new decimal(num4))), 1m));
				num5 = this.method_6((int)num4);
				num6 = num5;
				long num7 = this.method_1((int)num4, num5);
				long num8 = Convert.ToInt64(decimal.Add(decimal.Subtract(new decimal(num4), new decimal(num5)), 1m));
				int num9 = 0;
				while (num8 < num7)
				{
					array = (GClass33.Struct1[])Utils.CopyArray(array, new GClass33.Struct1[num9 + 1]);
					num5 = num6 + 1;
					num6 = this.method_6(num5);
					array[num9].long_1 = this.method_1(num5, num6);
					if (array[num9].long_1 > 9L)
					{
						if (this.method_7(array[num9].long_1))
						{
							array[num9].long_0 = (long)Math.Round((double)(array[num9].long_1 - 13L) / 2.0);
						}
						else
						{
							array[num9].long_0 = (long)Math.Round((double)(array[num9].long_1 - 12L) / 2.0);
						}
					}
					else
					{
						array[num9].long_0 = (long)((ulong)this.byte_1[(int)array[num9].long_1]);
					}
					num8 = num8 + (long)(num6 - num5) + 1L;
					num9++;
				}
				this.struct3_0[num2 + i].string_0 = new string[array.Length - 1 + 1];
				int num10 = 0;
				int num11 = array.Length - 1;
				for (int j = 0; j <= num11; j++)
				{
					if (array[j].long_1 > 9L)
					{
						if (!this.method_7(array[j].long_1))
						{
							if (decimal.Compare(new decimal(this.ulong_0), 1m) == 0)
							{
								this.struct3_0[num2 + i].string_0[j] = Encoding.Default.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].long_0);
							}
							else if (decimal.Compare(new decimal(this.ulong_0), 2m) == 0)
							{
								this.struct3_0[num2 + i].string_0[j] = Encoding.Unicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].long_0);
							}
							else if (decimal.Compare(new decimal(this.ulong_0), 3m) == 0)
							{
								this.struct3_0[num2 + i].string_0[j] = Encoding.BigEndianUnicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].long_0);
							}
						}
						else
						{
							this.struct3_0[num2 + i].string_0[j] = Encoding.Default.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].long_0);
						}
					}
					else
					{
						this.struct3_0[num2 + i].string_0[j] = Conversions.ToString(this.method_0(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].long_0));
					}
					num10 += (int)array[j].long_0;
				}
			}
		}
		else if (this.byte_0[(int)Offset] == 5)
		{
			ushort num12 = Convert.ToUInt16(decimal.Subtract(new decimal(this.method_0(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
			int num13 = (int)num12;
			for (int k = 0; k <= num13; k++)
			{
				ushort num14 = (ushort)this.method_0(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(k * 2))), 2);
				this.method_10(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.method_0((int)(Offset + (ulong)num14), 4)), 1m), new decimal((int)this.ushort_0))));
			}
			this.method_10(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.method_0(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.ushort_0))));
		}
		return true;
	}

	// Token: 0x04000207 RID: 519
	private byte[] byte_0;

	// Token: 0x04000208 RID: 520
	private ulong ulong_0;

	// Token: 0x04000209 RID: 521
	private string[] string_0;

	// Token: 0x0400020A RID: 522
	private GClass33.Struct2[] struct2_0;

	// Token: 0x0400020B RID: 523
	private ushort ushort_0;

	// Token: 0x0400020C RID: 524
	private byte[] byte_1 = new byte[]
	{
		0,
		1,
		2,
		3,
		4,
		6,
		8,
		8,
		0,
		0
	};

	// Token: 0x0400020D RID: 525
	private GClass33.Struct3[] struct3_0;

	// Token: 0x020000B7 RID: 183
	private struct Struct1
	{
		// Token: 0x0400020E RID: 526
		public long long_0;

		// Token: 0x0400020F RID: 527
		public long long_1;
	}

	// Token: 0x020000B8 RID: 184
	private struct Struct2
	{
		// Token: 0x04000210 RID: 528
		public long long_0;

		// Token: 0x04000211 RID: 529
		public string string_0;

		// Token: 0x04000212 RID: 530
		public string string_1;

		// Token: 0x04000213 RID: 531
		public string string_2;

		// Token: 0x04000214 RID: 532
		public long long_1;

		// Token: 0x04000215 RID: 533
		public string string_3;
	}

	// Token: 0x020000B9 RID: 185
	private struct Struct3
	{
		// Token: 0x04000216 RID: 534
		public long long_0;

		// Token: 0x04000217 RID: 535
		public string[] string_0;
	}
}
