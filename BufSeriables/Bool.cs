using System;
using System.IO;


namespace BufSeria
{
  public static partial class Serial
  {

    public static int
    BoolToBuf(bool b, MemoryStream ms)
    {
      ms.Append(b ? 1 : 0);
      return 1;
    }

    public static bool
    BufToBool(ReadOnlySpan<byte> buf, int offset, ref int byteFwd)
    {
      if (buf.Length - offset < 1)
      {
        throw new BufSeriaLenException("Buffer too short to deserialize Bool.");
      }

      byteFwd++;
      return buf[offset] == 1 ? true : false;
    }

  }
}