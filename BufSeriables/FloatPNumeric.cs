using System;
using System.IO;


namespace BufSeria
{
  public static partial class Serial
  {

    // Float.
    public static int
    FloatToBuf(float f, MemoryStream ms)
    {
      ms.Append(BitConverter.GetBytes(f));
      return 4;
    }

    public static float
    BufToFloat(ReadOnlySpan<byte> buf, int offset, ref int byteFwd)
    {

      float f = BitConverter.ToSingle(buf.ToArray(), offset);

      byteFwd += 4;
      return f;
    }


    // Double.
    public static int
    DoubleToBuf(double d, MemoryStream ms)
    {
      ms.Append(BitConverter.GetBytes(d));
      return 8;
    }

    public static double
    BufToDouble(ReadOnlySpan<byte> buf, int offset, ref int byteFwd)
    {

      double d = BitConverter.ToDouble(buf.ToArray(), offset);

      byteFwd += 8;
      return d;
    }


  }
}
