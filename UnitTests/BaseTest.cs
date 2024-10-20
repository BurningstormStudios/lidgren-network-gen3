using Lidgren.Network;
using System.Reflection;

namespace UnitTests
{
  public class BaseTest
  {

    protected NetIncomingMessage CreateIncomingMessage(byte[] fromData, int bitLength)
    {
      NetIncomingMessage inc = (NetIncomingMessage)Activator.CreateInstance(typeof(NetIncomingMessage), true);
      typeof(NetIncomingMessage).GetField("m_data", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(inc, fromData);
      typeof(NetIncomingMessage).GetField("m_bitLength", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(inc, bitLength);
      return inc;
    }
  }
}
