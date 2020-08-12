using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

// Token: 0x0200004F RID: 79
public static class GClass27
{
	// Token: 0x06000210 RID: 528 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
	public static void smethod_0(this Socket socket, uint keepAliveInterval, uint keepAliveTime)
	{
		GClass27.Struct0 @struct = new GClass27.Struct0
		{
			uint_0 = 1U,
			uint_2 = keepAliveInterval,
			uint_1 = keepAliveTime
		};
		int num = Marshal.SizeOf(@struct);
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr(@struct, intPtr, true);
		byte[] array = new byte[num];
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.FreeHGlobal(intPtr);
		socket.IOControl(IOControlCode.KeepAliveValues, array, null);
	}

	// Token: 0x02000050 RID: 80
	internal struct Struct0
	{
		// Token: 0x040000E8 RID: 232
		internal uint uint_0;

		// Token: 0x040000E9 RID: 233
		internal uint uint_1;

		// Token: 0x040000EA RID: 234
		internal uint uint_2;
	}
}
