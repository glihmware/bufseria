using System;
using System.IO;
using System.Text;


namespace BufSeria
{
  public static partial class Serial
  {

    /// <summary>
    ///   Serializes a string using UTF-8 encoding.
    /// </summary>
    /// 
    /// <param name="s"> String to be serialized. </param>
    /// <param name="ms"> MemoryStream where the bytes are written. </param>
    /// 
    /// <returns> Number of bytes serialized. </returns>
    /// 
    public static int
    StringToBuf(string s, MemoryStream ms)
    {
      if (s == "")
      {
        return LenSerial.Encode(0, ms);
      }

      if (s == null)
      {
        return LenSerial.EncodeNull(ms);
      }

      byte[] b = Encoding.UTF8.GetBytes(s);

      if (b.LongLength > Int32.MaxValue)
      {
        throw new BufSeriaLenException("String to be encoded is too long.");
      }

      int lenBytes = LenSerial.Encode(b.Length, ms);
      ms.Append(b);

      return lenBytes + b.Length;
    }

    /// <summary>
    ///   Deserializes a string from a buffer.
    /// </summary>
    /// 
    /// <param name="buf"> Buffer contaning the serialized form. </param>
    /// <param name="offset"> Offset from where deserialization starts. </param>
    /// <param name="byteFwd"> Incremented by the number of bytes used to deserialized. </param>
    /// 
    /// <returns> Deserialized string. </returns>
    /// 
    /// <exception cref = "BufSeriaLenException"> Length decoded error. </exception>
    /// 
    public static string
    BufToString(ReadOnlySpan<byte> buf, int offset, ref int byteFwd)
    {
      int idx = offset;

      int decodedLen;
      try
      {
        decodedLen = LenSerial.Decode(buf, idx, ref idx);
      }
      catch (ArgumentOutOfRangeException)
      {
        throw new BufSeriaLenException("Length can't be converted to uint.");
      }
      catch (IndexOutOfRangeException)
      {
        throw new BufSeriaLenException("Length can't be decoded.");
      }

      if (decodedLen > buf.Length)
      {
        throw new BufSeriaLenException("String's length is overflowing the buffer.");
      }

      if (decodedLen == 0)
      {
        byteFwd++;
        return "";
      }

      if (decodedLen < 0)
      {
        byteFwd++;
        return null;
      }

      string ret = Encoding.UTF8.GetString(buf.ToArray(), idx, decodedLen);
      idx += decodedLen;

      byteFwd += (idx - offset);

      return ret;
    }

    /// <summary>
    ///   Builds a string from a serialized buffer.
    ///   Starts at the first byte of the buffer.
    /// </summary>
    /// 
    /// <param name="buf"> Serialized string buffer. </param>
    /// 
    /// <returns> Deserialized string. </returns
    public static string
    BufToString(byte[] buf)
    {
      int forward = 0;
      return Serial.BufToString(buf, 0, ref forward);
    }



  }
}
