using System;
using System.IO;

namespace BufSeria
{
  public interface IBufSeriable
  {

    /// <summary>
    ///   Generates a buffer with the serialized object.
    /// </summary>
    /// 
    /// <returns> A buffer contaning the serialized object. </returns>
    /// 
    byte[]
    Serialize();

    /// <summary>
    ///   Constructs the object from a buffer containing it's serialized form.
    /// </summary>
    /// 
    /// <param name="buf"> Bufer containing the serialized object. </param>
    /// <param name="offset"> Offset in the buffer from where the deserialization starts. </param>
    /// <param name="byteFwd"> Incremented by the number of bytes used to deserialize the object. </param>
    /// 
    void
    Deserialize(byte[] buf, int offset, ref int byteFwd);

  }
}
