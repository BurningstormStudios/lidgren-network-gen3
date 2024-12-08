using System.Net;

namespace Lidgren.Network
{
  public partial interface INetBuffer
  {
    bool ReadBoolean();
    byte ReadByte();
    bool ReadByte(out byte result);
    sbyte ReadSByte();
    byte ReadByte(int numberOfBits);
    byte[] ReadBytes(int numberOfBytes);
    bool ReadBytes(int numberOfBytes, out byte[] result);
    void ReadBytes(byte[] into, int offsetInBytes, int numberOfBytes);
    void ReadBits(byte[] into, int offsetInBits, int numberOfBits);
    Int16 ReadInt16();
    UInt16 ReadUInt16();
    Int32 ReadInt32();
    bool ReadInt32(out Int32 result);
    Int32 ReadInt32(int numberOfBits);
    UInt32 ReadUInt32();
    bool ReadUInt32(out UInt32 result);
    UInt32 ReadUInt32(int numberOfBits);
    UInt64 ReadUInt64();
    Int64 ReadInt64();
    UInt64 ReadUInt64(int numberOfBits);
    Int64 ReadInt64(int numberOfBits);
    float ReadFloat();
    float ReadSingle();
    bool ReadSingle(out float result);
    double ReadDouble();

    uint ReadVariableUInt32();
    bool ReadVariableUInt32(out uint result);
    int ReadVariableInt32();
    Int64 ReadVariableInt64();
    UInt64 ReadVariableUInt64();
    float ReadSignedSingle(int numberOfBits);
    float ReadUnitSingle(int numberOfBits);
    float ReadRangedSingle(float min, float max, int numberOfBits);
    int ReadRangedInteger(int min, int max);
    long ReadRangedInteger(long min, long max);
    string ReadString();
    bool ReadString(out string result);
    double ReadTime(NetConnection senderConnection, bool highPrecision);
    IPEndPoint ReadIPEndPoint();

    void SkipPadBits();
    void ReadPadBits();
    void SkipPadBits(int numberOfBytes);

  }
}