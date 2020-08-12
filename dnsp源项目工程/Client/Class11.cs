using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

// Token: 0x020000E8 RID: 232
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Class11
{
	// Token: 0x060005C7 RID: 1479 RVA: 0x000021D4 File Offset: 0x000003D4
	internal Class11()
	{
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00014B70 File Offset: 0x00012D70
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (object.ReferenceEquals(Class11.resourceManager_0, null))
			{
				ResourceManager resourceManager = new ResourceManager("xClient.Properties.Resources", typeof(Class11).Assembly);
				Class11.resourceManager_0 = resourceManager;
			}
			return Class11.resourceManager_0;
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00004CBA File Offset: 0x00002EBA
	// (set) Token: 0x060005CA RID: 1482 RVA: 0x00004CC1 File Offset: 0x00002EC1
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return Class11.cultureInfo_0;
		}
		set
		{
			Class11.cultureInfo_0 = value;
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x060005CB RID: 1483 RVA: 0x00014BB0 File Offset: 0x00012DB0
	internal static Bitmap information
	{
		get
		{
			object @object = Class11.ResourceManager.GetObject("information", Class11.cultureInfo_0);
			return (Bitmap)@object;
		}
	}

	// Token: 0x040002D4 RID: 724
	private static ResourceManager resourceManager_0;

	// Token: 0x040002D5 RID: 725
	private static CultureInfo cultureInfo_0;
}
