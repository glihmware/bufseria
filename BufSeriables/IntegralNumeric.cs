using System;
using System.IO;


namespace BufSeria
{
  public static partial class Serial
  {

    /// <summary>
    ///   Signed byte serialization.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static int
    SByteToBuf(sbyte s, MemoryStream ms)
    {
      ms.Append((byte)s);
      return 1;
    }

    /// <summary>
    ///   Signed byte deserialization.
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static sbyte
    BufToSByte(ReadOnlySpan<byte> buf, ref int offset)
    {
      if (buf.Length - offset < 1)
      {
        throw new BufSeriaLenException("Buffer too short to deserialize SByte.");
      }

      return (sbyte)buf[offset++];
    }

    /// <summary>
    ///   Byte serialization.
    /// </summary>
    /// <param name="b"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static int
    ByteToBuf(byte b, MemoryStream ms)
    {
      ms.Append(b);
      return 1;
    }


    /// <summary>
    ///   Byte deserialization.
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static byte
    BufToByte(ReadOnlySpan<byte> buf, ref int offset)
    {
      if (buf.Length - offset < 1)
      {
        throw new BufSeriaLenException("Buffer too short to deserialize Byte.");
      }

      return buf[offset++];
    }


    /// <summary>
    ///   UInt16 serialization.
    /// </summary>
    /// <param name="u"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static int
    UInt16ToBuf(ushort u, MemoryStream ms)
    {
      Span<byte> b = stackalloc byte[2];

      b[0] = (byte)(u >> 0);
      b[1] = (byte)(u >> 8);

      ms.Append(b);
      return b.Length;
    }


    /// <summary>
    ///   UInt16 deserialization.
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static ushort
    BufToUInt16(ReadOnlySpan<byte> buf, ref int offset)
    {
      if (buf.Length - offset < 2)
      {
        throw new BufSeriaLenException("Buffer too short to deserialize UInt16.");
      }

      ushort u = (ushort)(buf[offset] << 0 | buf[offset + 1] << 8);

      offset += 2;
      return u;
    }


    /// <summary>
    ///   Int16 serialization.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static int
    Int16ToBuf(short s, MemoryStream ms)
    {
      Span<byte> b = stackalloc byte[2];

      b[0] = (byte)(s >> 0);
      b[1] = (byte)(s >> 8);

      ms.Append(b);
      return b.Length;
    }


    /// <summary>
    ///   Int16 deserialization.
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static short
    BufToInt16(ReadOnlySpan<byte> buf, ref int offset)
    {
      if (buf.Length - offset < 2)
      {
        throw new BufSeriaLenException("Buffer too short to deserialize Int16.");
      }

      short s = (short)(buf[offset] << 0 | buf[offset + 1] << 8);

      offset += 2;
      return s;
    }


    /// <summary>
    ///   UInt32 serialization.
    /// </summary>
    /// <param name="u"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static int
    UInt32ToBuf(uint u, MemoryStream ms)
    {
      Span<byte> b = stackalloc byte[4];

      b[0] = (byte)(u >> 0);
      b[1] = (byte)(u >> 8);
      b[2] = (byte)(u >> 16);
      b[3] = (byte)(u >> 24);

      ms.Append(b);
      return b.Length;
    }


    /// <summary>
    ///   UInt32 deserialization.
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static uint
    BufToUInt32(ReadOnlySpan<byte> buf, ref int offset)
    {
      if (buf.Length - offset < 4)
      {
        throw new BufSeriaLenException("Buffer too short to deserialize UInt32.");
      }

      int o = offset;
      uint u = (uint)
        (buf[o + 0] << 0 | buf[o + 1] << 8 | buf[o + 2] << 16 | buf[o + 3] << 24);

      offset += 4;
      return u;
    }


    /// <summary>
    ///   Int32 serialization.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static int
    Int32ToBuf(int i, MemoryStream ms)
    {
      Span<byte> b = stackalloc byte[4];

      b[0] = (byte)(i >> 0);
      b[1] = (byte)(i >> 8);
      b[2] = (byte)(i >> 16);
      b[3] = (byte)(i >> 24);

      ms.Append(b);
      return b.Length;
    }

    /// <summary>
    ///   Int32 deserialization.
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static int
    BufToInt32(ReadOnlySpan<byte> buf, ref int offset)
    {
      if (buf.Length - offset < 4)
      {
        throw new BufSeriaLenException("Buffer too short to deserialize Int32.");
      }

      int o = offset;
      int i = (int)
        (buf[o + 0] << 0 | buf[o + 1] << 8 | buf[o + 2] << 16 | buf[o + 3] << 24);

      offset += 4;
      return i;
    }


    /// <summary>
    ///   UInt64 serialization.
    /// </summary>
    /// <param name="u"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static int
    UInt64ToBuf(ulong u, MemoryStream ms)
    {
      Span<byte> b = stackalloc byte[8];

      b[0] = (byte)(u >> 0);
      b[1] = (byte)(u >> 8);
      b[2] = (byte)(u >> 16);
      b[3] = (byte)(u >> 24);
      b[4] = (byte)(u >> 32);
      b[5] = (byte)(u >> 40);
      b[6] = (byte)(u >> 48);
      b[7] = (byte)(u >> 56);

      ms.Append(b);
      return b.Length;
    }


    /// <summary>
    ///   UInt64 deserialization.
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static ulong
    BufToUInt64(ReadOnlySpan<byte> buf, ref int offset)
    {
      if (buf.Length - offset < 8)
      {
        throw new BufSeriaLenException("Buffer too short to deserialize UInt64.");
      }

      int o = offset;
      ulong u = (ulong)
        (
        (ulong)(buf[o + 0]) << 0 | (ulong)(buf[o + 1]) << 8 | (ulong)(buf[o + 2]) << 16 | (ulong)(buf[o + 3]) << 24
        |
        (ulong)(buf[o + 4]) << 32 | (ulong)(buf[o + 5]) << 40 | (ulong)(buf[o + 6]) << 48 | (ulong)(buf[o + 7]) << 56
        );

      offset += 8;
      return u;
    }


    /// <summary>
    ///   Int64 serialization.
    /// </summary>
    /// <param name="l"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static int
    Int64ToBuf(long l, MemoryStream ms)
    {
      Span<byte> b = stackalloc byte[8];

      b[0] = (byte)(l >> 0);
      b[1] = (byte)(l >> 8);
      b[2] = (byte)(l >> 16);
      b[3] = (byte)(l >> 24);
      b[4] = (byte)(l >> 32);
      b[5] = (byte)(l >> 40);
      b[6] = (byte)(l >> 48);
      b[7] = (byte)(l >> 56);

      ms.Append(b);
      return b.Length;
    }

    /// <summary>
    ///   Int64 deserialization.
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static long
    BufToInt64(ReadOnlySpan<byte> buf, ref int offset)
    {
      if (buf.Length - offset < 8)
      {
        throw new BufSeriaLenException("Buffer too short to deserialize Int64.");
      }

      int o = offset;
      long l = (long)
        (
        (long)(buf[o + 0]) << 0 | (long)(buf[o + 1]) << 8 | (long)(buf[o + 2]) << 16 | (long)(buf[o + 3]) << 24
        |
        (long)(buf[o + 4]) << 32 | (long)(buf[o + 5]) << 40 | (long)(buf[o + 6]) << 48 | (long)(buf[o + 7]) << 56
        );

      offset += 8;
      return l;
    }

  }
}
