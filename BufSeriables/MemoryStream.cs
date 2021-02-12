using System;
using System.IO;


namespace BufSeria
{
  public static partial class Serial
  {

    /// <summary> Serializes a stream. </summary>
    /// 
    /// <param name="inputStream"> Stream to be serialized. </param>
    /// <param name="outputStream"> Memory stream where the bytes are written. </param>
    /// 
    public static void
    StreamToBuf(MemoryStream src, MemoryStream dst)
    {
      if (src == null)
      {
        LenSerial.EncodeNull(dst);
        return;
      }

      LenSerial.Encode((int)src.Length, dst);
      src.WriteTo(dst);
    }

    /// <summary> Deserializes a stream. </summary>
    /// 
    /// <param name="buf"> Stream serialized buffer. </param>
    /// <param name="offset"> Byte offset where deserialization starts. </param>
    /// <param name="forward"> Number of bytes consumed to deserialized. </param>
    /// 
    /// <returns> Deserialized stream, or null. </returns>
    ///
    /// <exception cref = "BufSeriaException" > Deserialization can't occur. </exception>
    /// 
    public static MemoryStream
    BufToStream(byte[] buf, int offset, ref int byteFwd)
    {
      int idx = offset;
      int len = LenSerial.Decode(buf, idx, ref idx);

      if (len < 0)
      {
        // Stream was null.
        byteFwd += (idx - offset);
        return null;
      }

      if (len == 0)
      {
        byteFwd += (idx - offset);
        return new MemoryStream();
      }

      byte[] arr = new byte[len];
      Array.Copy(buf, idx, arr, 0, len);
      MemoryStream ms = new MemoryStream(arr);

      idx += len;

      byteFwd += (idx - offset);
      return ms;
    }
  }

}

