using System.Net;

namespace Lidgren.Network
{
  public interface INetOutgoingMessage : INetBuffer
  {
    bool Encrypt(NetEncryption encryption);
  }

  public interface INetIncomingMessage : INetBuffer
  {
    NetIncomingMessageType MessageType { get; }
    NetDeliveryMethod DeliveryMethod { get; }
    int SequenceChannel { get; }
    IPEndPoint SenderEndPoint { get; }
    NetConnection SenderConnection { get; }
    double ReceiveTime { get; }
    bool Decrypt(NetEncryption encryption);
    double ReadTime(bool highPrecision);
  }
}