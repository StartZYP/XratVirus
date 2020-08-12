using System;

namespace xClient.Core.MouseKeyHook.WinApi
{
	// Token: 0x0200007F RID: 127
	internal struct Point
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000388F File Offset: 0x00001A8F
		public Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000389F File Offset: 0x00001A9F
		public static bool operator ==(Point a, Point b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000038C3 File Offset: 0x00001AC3
		public static bool operator !=(Point a, Point b)
		{
			return !(a == b);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000038CF File Offset: 0x00001ACF
		public bool Equals(Point other)
		{
			return other.X == this.X && other.Y == this.Y;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000038F1 File Offset: 0x00001AF1
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && !(obj.GetType() != typeof(Point)) && this.Equals((Point)obj);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00003923 File Offset: 0x00001B23
		public override int GetHashCode()
		{
			return this.X * 397 ^ this.Y;
		}

		// Token: 0x0400017C RID: 380
		public int X;

		// Token: 0x0400017D RID: 381
		public int Y;
	}
}
