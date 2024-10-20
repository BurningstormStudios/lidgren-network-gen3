using Lidgren.Network;
using Xunit;

namespace UnitTests
{
	public class MiscTests : BaseTest
	{
		[Fact]
		public void Misc_NetPeerConfiguration_ShouldEnableAndDisableMessageTypesCorrectly()
		{
			var config = new NetPeerConfiguration("Test");

			// Enable a message type and check if it's enabled
			config.EnableMessageType(NetIncomingMessageType.UnconnectedData);
			Assert.True(config.IsMessageTypeEnabled(NetIncomingMessageType.UnconnectedData),
									"Failed to enable message type.");

			// Disable the message type and check if it's disabled
			config.SetMessageTypeEnabled(NetIncomingMessageType.UnconnectedData, false);
			Assert.False(config.IsMessageTypeEnabled(NetIncomingMessageType.UnconnectedData),
									 "Failed to disable message type.");
		}

		[Fact]
		public void Misc_NetUtility_ShouldConvertToHexStringCorrectly()
		{
			var hexString = NetUtility.ToHexString(new byte[] { 0xDE, 0xAD, 0xBE, 0xEF });
			Assert.Equal("DEADBEEF", hexString);
		}

		[Fact]
		public void Misc_NetUtility_ShouldCalculateBitsToHoldUInt64Correctly()
		{
			var bits = NetUtility.BitsToHoldUInt64((ulong)UInt32.MaxValue + 1ul);
			Assert.Equal(33, bits);
		}
	}
}
