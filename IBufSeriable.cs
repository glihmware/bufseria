using System;
using System.IO;

namespace BufSeria
{

  /// <summary>
  ///   Defines methods to serialize / deserialize objects in binary format.
  /// </summary>
  public interface IBufSeriable
  {

    /// <summary>
    ///   Generates a buffer with the serialized object.
    /// </summary>
    /// 
    /// <returns> A buffer contaning the serialized object. </returns>
    /// 
    byte[] Serialize();

    

    /// <summary>
    ///   Constructs the object from a buffer containing it's serialized form.
    /// </summary>
    /// 
    /// <param name="buf"> Bufer containing the serialized object. </param>
    /// <param name="offset">
    ///    Offset in the buffer from where the deserialization starts.
    ///    The offset will be moved forward by the bytes consumed by deserialization.
    /// </param>
    /// 
    void Deserialize(ReadOnlySpan<byte> buf, ref int offset);

  }
}
