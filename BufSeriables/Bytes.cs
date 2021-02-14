using System;
using System.IO;


namespace BufSeria
{
  public static partial class Serial
  {

    /// <summary> Serializes an array of bytes. </summary>
    /// 
    /// <param name="src"> Stream to be serialized. </param>
    /// <param name="ms"> Memory stream where the bytes are written. </param>
    /// 
    public static void
    BytesToBuf(byte[] buf, MemoryStream ms)
    {
      if (buf == null)
      {
        LenSerial.EncodeNull(ms);
        return;
      }

      LenSerial.Encode((int)buf.Length, ms);
      ms.Append(buf);
    }

    /// <summary> Deserializes a stream. </summary>
    /// 
    /// <param name="buf"> Bytes serialized buffer. </param>
    /// <param name="offset"> Byte offset where deserialization starts. </param>
    /// <param name="forward"> Number of bytes consumed to deserialized. </param>
    /// 
    /// <returns> Deserialized stream, or null. </returns>
    ///
    /// <exception cref = "BufSeriaException" > Deserialization can't occur. </exception>
    /// 
    public static byte[]
    BufToBytes(byte[] buf, int offset, ref int byteFwd)
    {
      int idx = offset;
      int len = LenSerial.Decode(buf, idx, ref idx);

      if (len < 0)
      {
        // byte[] was null.
        byteFwd += (idx - offset);
        return null;
      }

      if (len == 0)
      {
        byteFwd += (idx - offset);
        return new byte[0];
      }

      byte[] arr = new byte[len];
      Array.Copy(buf, idx, arr, 0, len);

      idx += len;

      byteFwd += (idx - offset);
      return arr;
    }
  }

}

