using System.Net;

namespace Lidgren.Network
{
  public partial interface INetBuffer
  {
    void Write(bool value);
    void Write(byte source);
    void WriteAt(Int32 offset, byte source);
    void Write(sbyte source);
    void Write(byte source, int numberOfBits);
    void Write(byte[] source);
    void Write(byte[] source, int offsetInBytes, int numberOfBytes);
    void Write(UInt16 source);
    void WriteAt(Int32 offset, UInt16 source);
    void Write(UInt16 source, int numberOfBits);
    void Write(Int16 source);
    void WriteAt(Int32 offset, Int16 source);

#if UNSAFE
		unsafe void Write(Int32 source);
#else
    void Write(Int32 source);
#endif

    void WriteAt(Int32 offset, Int32 source);

#if UNSAFE
		unsafe void Write(UInt32 source);
#else
    void Write(UInt32 source);
#endif

    void WriteAt(Int32 offset, UInt32 source);
    void Write(UInt32 source, int numberOfBits);
    void Write(Int32 source, int numberOfBits);
    void Write(UInt64 source);
    void WriteAt(Int32 offset, UInt64 source);
    void Write(UInt64 source, int numberOfBits);
    void Write(Int64 source);
    void Write(Int64 offset, int numberOfBits);

#if UNSAFE
		unsafe void Write(float source);
		unsafe void Write(double source);
#else
    void Write(float source);
    void Write(double source);
#endif

    int WriteVariableUInt32(uint value);
    int WriteVariableInt32(int value);
    int WriteVariableInt64(Int64 value);
    int WriteVariableUInt64(UInt64 value);

    void WriteSignedSingle(float value, int numberOfBits);
    void WriteUnitSingle(float value, int numberOfBits);
    void WriteRangedSingle(float value, float min, float max, int numberOfBits);
    int WriteRangedInteger(int value, int min, int max);
    int WriteRangedInteger(long value, long min, long max);

    void Write(string source);
    void Write(IPEndPoint endPoint);
    void WriteTime(bool highPrecision);
    void WriteTime(double localTime, bool highPrecision);

    void WritePadBits();
    void WritePadBits(int numberOfBits);

    void Write(NetBuffer source);
  }
}