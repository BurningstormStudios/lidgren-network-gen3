namespace Lidgren.Network
{
  public partial interface INetBuffer
  {
    byte[] PeekDataBuffer();
    bool PeekBoolean();
    byte PeekByte();
    sbyte PeekSByte();
    byte PeekByte(int numberOfBits);
    byte[] PeekBytes(int numberOfBytes);
    void PeekBytes(byte[] into, int offset, int numberOfBytes);
    Int16 PeekInt16();
    UInt16 PeekUInt16();
    Int32 PeekInt32();
    Int32 PeekInt32(int numberOfBits);
    UInt32 PeekUInt32();
    UInt32 PeekUInt32(int numberOfBits);
    UInt64 PeekUInt64();
    Int64 PeekInt64();
    UInt64 PeekUInt64(int numberOfBits);
    Int64 PeekInt64(int numberOfBits);
    float PeekFloat();
    float PeekSingle();
    double PeekDouble();
    string PeekString();
  }
}