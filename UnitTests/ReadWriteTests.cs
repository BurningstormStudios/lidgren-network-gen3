using System;
using System.Collections.Generic;

using Lidgren.Network;
using System.Reflection;
using System.Text;

using Xunit;

namespace UnitTests
{
  public class ReadWriteTests : BaseTest
  {
    [Fact]
    public void ReadWrite_MessageData_ShouldMatchExpectedValues()
    {
      var config = new NetPeerConfiguration("unittests");
      var peer = new NetPeer(config);
      peer.Start();

      var msg = peer.CreateMessage();

      // Writing to message
      msg.Write(false);
      msg.Write(-3, 6);
      msg.Write(42);
      msg.Write("duke of earl");
      msg.Write((byte)43);
      msg.Write((ushort)44);
      msg.Write(UInt64.MaxValue, 64);
      msg.Write(true);

      msg.WritePadBits();

      int bcnt = 0;

      msg.Write(567845.0f);
      msg.WriteVariableInt32(2115998022);
      msg.Write(46.0);
      msg.Write((ushort)14, 9);
      bcnt += msg.WriteVariableInt32(-47);
      msg.WriteVariableInt32(470000);
      msg.WriteVariableUInt32(48);
      bcnt += msg.WriteVariableInt64(-49);

      // Check byte count
      Assert.Equal(2, bcnt);

      byte[] data = msg.Data;

      NetIncomingMessage inc = CreateIncomingMessage(data, msg.LengthBits);

      StringBuilder bdr = new StringBuilder();

      // Reading from message
      bdr.Append(inc.ReadBoolean());
      bdr.Append(inc.ReadInt32(6));
      bdr.Append(inc.ReadInt32());

      Assert.True(inc.ReadString(out var strResult));
      bdr.Append(strResult);

      bdr.Append(inc.ReadByte());

      // Peek tests
      Assert.Equal((ushort)44, inc.PeekUInt16());
      bdr.Append(inc.ReadUInt16());

      Assert.Equal(UInt64.MaxValue, inc.PeekUInt64(64));
      bdr.Append(inc.ReadUInt64());
      bdr.Append(inc.ReadBoolean());

      inc.SkipPadBits();

      bdr.Append(inc.ReadSingle());
      bdr.Append(inc.ReadVariableInt32());
      bdr.Append(inc.ReadDouble());
      bdr.Append(inc.ReadUInt32(9));
      bdr.Append(inc.ReadVariableInt32());
      bdr.Append(inc.ReadVariableInt32());
      bdr.Append(inc.ReadVariableUInt32());
      bdr.Append(inc.ReadVariableInt64());

      // Verify the final string result
      Assert.Equal("False-342duke of earl434418446744073709551615True56784521159980224614-4747000048-49", bdr.ToString());

      peer.Shutdown("bye");
    }

    [Fact]
    public void NetOutgoingMessage_WriteAndRead_ShouldPreserveData()
    {
      var config = new NetPeerConfiguration("unittests");
      var peer = new NetPeer(config);
      peer.Start();

      var msg = peer.CreateMessage();
      var tmp = peer.CreateMessage();
      tmp.Write(42, 14);

      msg.Write(tmp);
      msg.Write(tmp);

      // Validate length
      Assert.Equal(tmp.LengthBits * 2, msg.LengthBits);

      peer.Shutdown("bye");
    }

    [Fact]
    public void WriteAllFields_Message_ShouldMatchOriginalObject()
    {
      var config = new NetPeerConfiguration("unittests");
      var peer = new NetPeer(config);
      peer.Start();

      var msg = peer.CreateMessage();

      Test test = new Test
      {
        Number = 42,
        Name = "Hallon",
        Age = 8.2f
      };

      msg.WriteAllFields(test, BindingFlags.Public | BindingFlags.Instance);

      var data = msg.Data;
      var inc = CreateIncomingMessage(data, msg.LengthBits);

      Test readTest = new Test();
      inc.ReadAllFields(readTest, BindingFlags.Public | BindingFlags.Instance);

      Assert.Equal(42, readTest.Number);
      Assert.Equal("Hallon", readTest.Name);
      Assert.Equal(8.2f, readTest.Age);

      peer.Shutdown("bye");
    }
  }

  public class TestBase
  {
    public int Number;
  }

  public class Test : TestBase
  {
    public float Age;
    public string Name;
  }
}
