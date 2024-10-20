using System;
using System.IO;
using System.Security.Cryptography;

namespace Lidgren.Network
{
	public class NetTripleDESEncryption : NetCryptoProviderBase
	{
		public NetTripleDESEncryption(NetPeer peer)
			: base(peer, TripleDES.Create())
		{
		}

		public NetTripleDESEncryption(NetPeer peer, string key)
			: this(peer)
		{
			SetKey(key);
		}

		public NetTripleDESEncryption(NetPeer peer, byte[] data, int offset, int count)
			: this(peer)
		{
			SetKey(data, offset, count);
		}
	}
}
