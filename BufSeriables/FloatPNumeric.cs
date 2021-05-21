using System;
using System.IO;


namespace BufSeria
{
  public static partial class Serial
  {

    /// <summary>
    ///   Float serialization.
    /// </summary>
    /// <param name="f"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static int
    FloatToBuf(float f, MemoryStream ms)
    {
      ms.Append(BitConverter.GetBytes(f));
      return 4;
    }

    /// <summary>
    ///   Float deserialization.
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static float
    BufToFloat(ReadOnlySpan<byte> buf, ref int offset)
    {
      float f = BitConverter.ToSingle(buf.ToArray(), offset);

      offset += 4;
      return f;
    }


    /// <summary>
    ///   Double serialization.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static int
    DoubleToBuf(double d, MemoryStream ms)
    {
      ms.Append(BitConverter.GetBytes(d));
      return 8;
    }


    /// <summary>
    ///   Double deserialization.
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static double
    BufToDouble(ReadOnlySpan<byte> buf, ref int offset)
    {
      double d = BitConverter.ToDouble(buf.ToArray(), offset);

      offset  += 8;
      return d;
    }


  }
}
