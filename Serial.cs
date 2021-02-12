using System;


namespace BufSeria
{

  /// <summary>
  /// 
  /// </summary>
  public static partial class Serial
  {


    /// <summary>
    ///   Checks if the endianness is supported to be able to serialize / deserialize interoperable data.
    /// </summary>
    /// <returns></returns>
    public static bool CheckForSafeEndianness()
    {
      return BitConverter.IsLittleEndian;
    }


  }
}
