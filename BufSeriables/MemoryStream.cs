using System;
using System.IO;


namespace BufSeria
{
  public static partial class Serial
  {

    /// <summary> Serializes a memory stream. </summary>
    /// 
    /// <param name="src"> Stream to be serialized. </param>
    /// <param name="dst"> Memory stream where the bytes are written. </param>
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
    /// 
    /// <returns> Deserialized stream, or null. </returns>
    ///
    /// <exception cref = "BufSeriaException" > Deserialization can't occur. </exception>
    /// 
    public static MemoryStream
    BufToStream(ReadOnlySpan<byte> buf, ref int offset)
    {
      int len = LenSerial.Decode(buf, ref offset);

      if (len < 0)
      {
        // Stream was null.
        return null;
      }

      if (len == 0)
      {
        return new MemoryStream();
      }

      // Need extra check... (buf.Length - offset) should be greater or equal to decoded len.

      byte[] arr = new byte[len];
      Span<byte> s = new Span<byte>(arr);

      buf.Slice(offset, len).CopyTo(s);

      MemoryStream ms = new MemoryStream(arr);

      offset += len;
      return ms;
    }
  }

}

