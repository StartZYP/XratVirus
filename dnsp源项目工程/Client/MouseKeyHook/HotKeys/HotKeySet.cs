using System;
using System.Collections.Generic;
using System.Windows.Forms;
using xClient.Core.MouseKeyHook.Implementation;

namespace xClient.Core.MouseKeyHook.HotKeys
{
	// Token: 0x02000053 RID: 83
	public class HotKeySet
	{
		// Token: 0x06000215 RID: 533 RVA: 0x00002FC5 File Offset: 0x000011C5
		public HotKeySet(IEnumerable<Keys> hotkeys)
		{
			this.m_hotkeystate = new Dictionary<Keys, bool>();
			this.m_remapping = new Dictionary<Keys, Keys>();
			this.m_hotkeys = hotkeys;
			this.InitializeKeys();
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00002FF7 File Offset: 0x000011F7
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00002FFF File Offset: 0x000011FF
		public string Name { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00003008 File Offset: 0x00001208
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00003010 File Offset: 0x00001210
		public string Description { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00003019 File Offset: 0x00001219
		public IEnumerable<Keys> HotKeys
		{
			get
			{
				return this.m_hotkeys;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00003021 File Offset: 0x00001221
		public bool HotKeysActivated
		{
			get
			{
				return this.m_hotkeydowncount == this.m_hotkeystate.Count - this.m_remappingCount;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000303D File Offset: 0x0000123D
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00003045 File Offset: 0x00001245
		public bool Enabled
		{
			get
			{
				return this.m_enabled;
			}
			set
			{
				if (value)
				{
					this.InitializeKeys();
				}
				this.m_enabled = value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600021E RID: 542 RVA: 0x0000DB28 File Offset: 0x0000BD28
		// (remove) Token: 0x0600021F RID: 543 RVA: 0x0000DB60 File Offset: 0x0000BD60
		public event HotKeySet.HotKeyHandler OnHotKeysDownHold;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000220 RID: 544 RVA: 0x0000DB98 File Offset: 0x0000BD98
		// (remove) Token: 0x06000221 RID: 545 RVA: 0x0000DBD0 File Offset: 0x0000BDD0
		public event HotKeySet.HotKeyHandler OnHotKeysUp;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000222 RID: 546 RVA: 0x0000DC08 File Offset: 0x0000BE08
		// (remove) Token: 0x06000223 RID: 547 RVA: 0x0000DC40 File Offset: 0x0000BE40
		public event HotKeySet.HotKeyHandler OnHotKeysDownOnce;

		// Token: 0x06000224 RID: 548 RVA: 0x00003057 File Offset: 0x00001257
		private void InvokeHotKeyHandler(HotKeySet.HotKeyHandler hotKeyDelegate)
		{
			if (hotKeyDelegate != null)
			{
				hotKeyDelegate(this, new HotKeyArgs(DateTime.Now));
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000DC78 File Offset: 0x0000BE78
		private void InitializeKeys()
		{
			foreach (Keys key in this.HotKeys)
			{
				if (this.m_hotkeystate.ContainsKey(key))
				{
					this.m_hotkeystate.Add(key, false);
				}
				this.m_hotkeystate[key] = KeyboardState.GetCurrent().IsDown(key);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		public bool UnregisterExclusiveOrKey(Keys anyKeyInTheExclusiveOrSet)
		{
			Keys exclusiveOrPrimaryKey = this.GetExclusiveOrPrimaryKey(anyKeyInTheExclusiveOrSet);
			if (exclusiveOrPrimaryKey != Keys.None && this.m_remapping.ContainsValue(exclusiveOrPrimaryKey))
			{
				List<Keys> list = new List<Keys>();
				foreach (KeyValuePair<Keys, Keys> keyValuePair in this.m_remapping)
				{
					if (keyValuePair.Value == exclusiveOrPrimaryKey)
					{
						list.Add(keyValuePair.Key);
					}
				}
				foreach (Keys key in list)
				{
					this.m_remapping.Remove(key);
				}
				this.m_remappingCount--;
				return true;
			}
			return false;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		public Keys RegisterExclusiveOrKey(IEnumerable<Keys> orKeySet)
		{
			foreach (Keys key in orKeySet)
			{
				if (!this.m_hotkeystate.ContainsKey(key))
				{
					return Keys.None;
				}
			}
			int num = 0;
			Keys keys = Keys.None;
			foreach (Keys keys2 in orKeySet)
			{
				if (num == 0)
				{
					keys = keys2;
				}
				this.m_remapping[keys2] = keys;
				num++;
			}
			this.m_remappingCount++;
			return keys;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000306D File Offset: 0x0000126D
		private Keys GetExclusiveOrPrimaryKey(Keys k)
		{
			if (!this.m_remapping.ContainsKey(k))
			{
				return Keys.None;
			}
			return this.m_remapping[k];
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000308B File Offset: 0x0000128B
		private Keys GetPrimaryKey(Keys k)
		{
			if (!this.m_remapping.ContainsKey(k))
			{
				return k;
			}
			return this.m_remapping[k];
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000DE90 File Offset: 0x0000C090
		internal void OnKey(KeyEventArgsExt kex)
		{
			if (!this.Enabled)
			{
				return;
			}
			Keys primaryKey = this.GetPrimaryKey(kex.KeyCode);
			if (kex.IsKeyDown)
			{
				this.OnKeyDown(primaryKey);
				return;
			}
			this.OnKeyUp(primaryKey);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000DECC File Offset: 0x0000C0CC
		private void OnKeyDown(Keys k)
		{
			if (this.HotKeysActivated)
			{
				this.InvokeHotKeyHandler(this.OnHotKeysDownHold);
				return;
			}
			if (this.m_hotkeystate.ContainsKey(k) && !this.m_hotkeystate[k])
			{
				this.m_hotkeystate[k] = true;
				this.m_hotkeydowncount++;
				if (this.HotKeysActivated)
				{
					this.InvokeHotKeyHandler(this.OnHotKeysDownOnce);
				}
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000DF3C File Offset: 0x0000C13C
		private void OnKeyUp(Keys k)
		{
			if (this.m_hotkeystate.ContainsKey(k) && this.m_hotkeystate[k])
			{
				bool hotKeysActivated = this.HotKeysActivated;
				this.m_hotkeystate[k] = false;
				this.m_hotkeydowncount--;
				if (hotKeysActivated)
				{
					this.InvokeHotKeyHandler(this.OnHotKeysUp);
				}
			}
		}

		// Token: 0x040000EC RID: 236
		private readonly IEnumerable<Keys> m_hotkeys;

		// Token: 0x040000ED RID: 237
		private readonly Dictionary<Keys, bool> m_hotkeystate;

		// Token: 0x040000EE RID: 238
		private readonly Dictionary<Keys, Keys> m_remapping;

		// Token: 0x040000EF RID: 239
		private bool m_enabled = true;

		// Token: 0x040000F0 RID: 240
		private int m_hotkeydowncount;

		// Token: 0x040000F1 RID: 241
		private int m_remappingCount;

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x0600022E RID: 558
		public delegate void HotKeyHandler(object sender, HotKeyArgs e);
	}
}
