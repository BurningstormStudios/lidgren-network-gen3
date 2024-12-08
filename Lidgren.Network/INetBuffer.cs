namespace Lidgren.Network
{
	public partial interface INetBuffer
	{
		byte[] Data { get; set; }
		int LengthBytes { get; set; }
		int LengthBits { get; set; }
		long Position { get; set; }
		int PositionInBytes { get; }

		void EnsureBufferSize(int numberOfBits);
	}
}
