using System;
using System.IO;


namespace BufSeria
{
  /// <summary>
  ///   Encoded lengths.
  /// </summary>
  public enum EncodedLen
  {

    /// <summary>
    ///   The length is encoded in two bytes.
    /// </summary>
    L16 = 253,

    /// <summary>
    ///   The length is encoded in four bytes.
    /// </summary>
    L32 = 254,

    /// <summary>
    ///   Special value to encode a variable set to NULL.
    /// </summary>
    NULL = 255,

    // FIXME: is U64 really needed?
  }

  /// <summary>
  ///   Length representation in a serialized buffer.
  /// </summary>
  public static class LenSerial
  {

    /// <summary>
    ///   Encodes the NULL value as a length.
    /// </summary>
    ///
    /// <param name="ms"> MemoryStream where NULL value should be encoded. </param>
    /// 
    /// <returns> 1, the number of byte added to the stream. </returns>
    /// 
    public static int
    EncodeNull(MemoryStream ms)
    {
      ms.Append((byte)EncodedLen.NULL);
      return 1;
    }


    /// <summary>
    ///   Encodes a length in the given stream.
    /// </summary>
    /// 
    /// <param name="len"> Length to be encoded. </param>
    /// <param name="ms"> MemoryStream where the encoded length is added. </param>
    /// 
    /// <returns> Number of bytes for the encoded length. </returns>
    /// 
    public static int
    Encode(int len, MemoryStream ms)
    {
      if (len < (int)EncodedLen.L16)
      {
        ms.Append((byte)len);
        return 1;
      }

      if (len <= ushort.MaxValue)
      {
        ms.Append((byte)EncodedLen.L16);
        Serial.UInt16ToBuf((ushort)len, ms);

        return 3;
      }

      ms.Append((byte)EncodedLen.L32);
      Serial.Int32ToBuf(len, ms);

      return 5;
    }

    /// <summary>
    ///   Decodes a encoded length into the given buffer.
    /// </summary>
    /// 
    /// <param name="buf"> Buffer containing the encoded length. </param>
    /// <param name="offset"> Offset of the first byte of the encoded length into the buffer. </param
    /// 
    /// <returns> Decoded length value. </returns>
    /// 
    /// <exception cref="IndexOutOfRangeException"> The buffer is too short to decode the length. </exception>
    /// <exception cref="ArgumentOutOfRangeException"> Use of ToUInt16(), toUInt32(). </exception>
    /// <exception cref="BufSeriaException"> Provided offset is lower than 0. </exception>
    /// 
    public static int
    Decode(ReadOnlySpan<byte> buf, ref int offset)
    {
      if (offset < 0)
      {
        throw new BufSeriaException("Offset can't be lower than 0.");
      }

      byte l = buf[offset++];

      if (l < (byte)EncodedLen.L16)
      {
        return l;
      }


      else if (l == (byte)EncodedLen.NULL)
      {
        // 255.
        return -1;
      }

      else if (l == (byte)EncodedLen.L16)
      {
        if (buf.Length - offset < 2)
        {
          throw new IndexOutOfRangeException("Buffer too short to decode U16 encoded length.");
        }

        return Serial.BufToUInt16(buf, ref offset);
      }

      else
      {
        // L32.

        if (buf.Length - offset < 4)
        {
          throw new IndexOutOfRangeException("Buffer too short to decode U32 encoded length.");
        }

        return Serial.BufToInt32(buf, ref offset);
      }
    }
  }
}
