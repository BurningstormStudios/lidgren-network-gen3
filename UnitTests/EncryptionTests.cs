using Lidgren.Network;
using Xunit;

namespace UnitTests
{
	public class EncryptionTests : BaseTest
	{
		[Fact]
		public void Encryption_Xor_ShouldEncryptAndDecryptCorrectly()
		{
			var peer = CreatePeer();
			TestEncryptionAlgorithm(peer, new NetXorEncryption(peer, "TopSecret"));
		}

		[Fact]
		public void Encryption_Xtea_ShouldEncryptAndDecryptCorrectly()
		{
			var peer = CreatePeer();
			TestEncryptionAlgorithm(peer, new NetXtea(peer, "TopSecret"));
		}

		[Fact]
		public void Encryption_AES_ShouldEncryptAndDecryptCorrectly()
		{
			var peer = CreatePeer();
			TestEncryptionAlgorithm(peer, new NetAESEncryption(peer, "TopSecret"));
		}

		[Fact]
		public void Encryption_RC2_ShouldEncryptAndDecryptCorrectly()
		{
			var peer = CreatePeer();
			TestEncryptionAlgorithm(peer, new NetRC2Encryption(peer, "TopSecret"));
		}

		[Fact]
		public void Encryption_DES_ShouldEncryptAndDecryptCorrectly()
		{
			var peer = CreatePeer();
			TestEncryptionAlgorithm(peer, new NetDESEncryption(peer, "TopSecret"));
		}

		[Fact]
		public void Encryption_TripleDES_ShouldEncryptAndDecryptCorrectly()
		{
			var peer = CreatePeer();
			TestEncryptionAlgorithm(peer, new NetTripleDESEncryption(peer, "TopSecret"));
		}

		[Fact]
		public void SRP_Protocol_ShouldComputeMatchingSessionValues()
		{
			var config = new NetPeerConfiguration("unittests");
			var peer = new NetPeer(config);
			peer.Start();

			for (int i = 0; i < 100; i++)
			{
				byte[] salt = NetSRP.CreateRandomSalt();
				byte[] x = NetSRP.ComputePrivateKey("user", "password", salt);

				byte[] v = NetSRP.ComputeServerVerifier(x);

				byte[] a = NetSRP.CreateRandomEphemeral();
				byte[] A = NetSRP.ComputeClientEphemeral(a);

				byte[] b = NetSRP.CreateRandomEphemeral();
				byte[] B = NetSRP.ComputeServerEphemeral(b, v);

				byte[] u = NetSRP.ComputeU(A, B);

				byte[] Ss = NetSRP.ComputeServerSessionValue(A, v, u, b);
				byte[] Sc = NetSRP.ComputeClientSessionValue(B, x, u, a);

				// Verify session values match
				Assert.Equal(Ss.Length, Sc.Length);
				for (int j = 0; j < Ss.Length; j++)
				{
					Assert.Equal(Ss[j], Sc[j]);
				}

				// Create encryption based on session
				var test = NetSRP.CreateEncryption(peer, Ss);

				Assert.NotNull(test);
			}

			peer.Shutdown("bye");
		}

		private void TestEncryptionAlgorithm(NetPeer peer, NetEncryption algo)
		{
			var om = peer.CreateMessage();
			om.Write("Hallon");
			om.Write(42);
			om.Write(5, 5);
			om.Write(true);
			om.Write("kokos");
			int unencLen = om.LengthBits;
			om.Encrypt(algo);

			// Convert to incoming message
			var im = CreateIncomingMessage(om.PeekDataBuffer(), om.LengthBits);
			Assert.NotNull(im.Data);
			Assert.NotEmpty(im.Data);

			im.Decrypt(algo);

			Assert.NotNull(im.Data);
			Assert.NotEmpty(im.Data);
			Assert.Equal(unencLen, im.LengthBits);

			// Verify data integrity after decryption
			Assert.Equal("Hallon", im.ReadString());
			Assert.Equal(42, im.ReadInt32());
			Assert.Equal(5, im.ReadInt32(5));
			Assert.True(im.ReadBoolean());
			Assert.Equal("kokos", im.ReadString());

			peer.Shutdown("bye");
		}

		private NetPeer CreatePeer()
		{
			var config = new NetPeerConfiguration("unittests");
			var peer = new NetPeer(config);
			peer.Start();
			return peer;
		}
	}
}
