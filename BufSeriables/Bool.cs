using System;
using System.IO;


namespace BufSeria
{
  public static partial class Serial
  {

    /// <summary>
    ///   Bool serialization.
    /// </summary>
    /// <param name="b"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static int
    BoolToBuf(bool b, MemoryStream ms)
    {
      ms.Append(b ? (byte)1 : (byte)0);
      return 1;
    }

    /// <summary>
    ///   Bool deserialization.
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static bool
    BufToBool(ReadOnlySpan<byte> buf, ref int offset)
    {
      if (buf.Length - offset < 1)
      {
        throw new BufSeriaLenException("Buffer too short to deserialize Bool.");
      }

      return buf[offset++] == 1 ? true : false;
    }

  }
}