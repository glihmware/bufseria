using System;
using System.IO;


namespace BufSeria
{

  /// <summary>
  ///   Convenient MemoryStream extension for shorter syntax.
  /// </summary>
  /// 
  public static class MemoryStreamExt
  {
    /// <summary>
    ///   Adds a byte to the stream.
    /// </summary>
    /// 
    /// <param name="stream"> MemoryStream where the byte is added. </param>
    /// <param name="b"> Byte to be added. </param>
    /// 
    public static void Append(this MemoryStream stream, byte b)
    {
      Span<byte> span = stackalloc byte[1];
      span[0] = b;

      stream.Append(span);
    }

    /// <summary>
    ///   Appends an entire buffer to a memory stream.
    /// </summary>
    /// 
    /// <param name="stream"> MemoryStream where the buffer is added. </param>
    /// <param name="buf"> The buffer to be added, from the very first byte to the end of the array. </param>
    /// 
    public static void Append(this MemoryStream stream, ReadOnlySpan<byte> buf)
    {
      stream.Write(buf);
    }
  }

}
