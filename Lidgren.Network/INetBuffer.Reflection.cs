using System.Reflection;

namespace Lidgren.Network
{
  public partial interface INetBuffer
  {
    void ReadAllFields(object target);
    void ReadAllFields(object target, BindingFlags flags);
    void ReadAllProperties(object target);
    void ReadAllProperties(object target, BindingFlags flags);

    void WriteAllFields(object obj);
    void WriteAllFields(object obj, BindingFlags flags);
    void WriteAllProperties(object obj);
    void WriteAllProperties(object obj, BindingFlags flags);
  }
}