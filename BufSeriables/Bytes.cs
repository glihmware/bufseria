using System;
using System.IO;


namespace BufSeria
{
  public static partial class Serial
  {

    /// <summary> Serializes an array of bytes. </summary>
    /// 
    /// <param name="buf"> Stream to be serialized. </param>
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

      LenSerial.Encode(buf.Length, ms);
      ms.Append(buf);
    }

    /// <summary> Deserializes a stream. </summary>
    /// 
    /// <param name="buf"> Bytes serialized buffer. </param>
    /// <param name="offset"> Byte offset where deserialization starts. </param>
    /// 
    /// <returns> Deserialized stream, or null. </returns>
    ///
    /// <exception cref = "BufSeriaException" > Deserialization can't occur. </exception>
    /// 
    public static byte[]
    BufToBytes(ReadOnlySpan<byte> buf, ref int offset)
    {
      int idx = offset;
      int len = LenSerial.Decode(buf, ref offset);

      if (len < 0)
      {
        return null;
      }

      if (len == 0)
      {
        return new byte[0];
      }

      byte[] arr = new byte[len];
      Span<byte> s = new Span<byte>(arr);

      buf.Slice(offset, len).CopyTo(s);

      offset += len;
      return arr;
    }
  }

}

