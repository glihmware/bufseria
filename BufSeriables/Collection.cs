using System;
using System.IO;
using System.Collections.Generic;


namespace BufSeria
{
  public static partial class Serial
  {
    // FIXME: need to add serialization of collection for numeric using Serial.<type>ToBuf and Serial.BufTo<type>.


    /// <summary>
    ///   Serializes any object implementing ICollection interface.
    /// </summary>
    /// 
    /// <typeparam name="T"></typeparam>
    /// <param name="c"></param>
    /// <param name="ms"></param>
    /// 
    public static void
    CollectionToBuf<T>(ICollection<T> c, MemoryStream ms)
      where T : IBufSeriable
    {
      if (c == null)
      {
        Serial.Int32ToBuf(-1, ms);
        return;
      }

      int n = c.Count;
      Serial.Int32ToBuf(n, ms);

      foreach (var i in c)
      {
        ms.Append(i.Serialize());
      }
    }

    /// <summary>
    ///   Deserializes any object implementing ICollection interface.
    ///   
    ///   Output collection (`c`) is cleared if the serialized collection was not null.
    /// </summary>
    /// 
    /// <typeparam name="T"></typeparam>
    /// 
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <param name="c"></param>
    /// 
    /// <returns> True if the list was null, false otherwise. </returns>
    /// 
    public static bool
    BufToCollection<T>(ReadOnlySpan<byte> buf, ref int offset, ICollection<T> c)
      where T : IBufSeriable, new()
    {
      if (c == null)
      {
        throw new NullReferenceException("ICollection to be filled by the deserialization must be initialized.");
      }

      int count = Serial.BufToInt32(buf, ref offset);
      if (count < 0)
      {
        return true;
      }

      c.Clear();

      for (int i = 0; i < count; i++)
      {
        T t = new T();
        t.Deserialize(buf, ref offset);
        c.Add(t);
      }

      return false;
    }



    /// <summary>
    ///   Serializes a collection of strings.
    /// </summary>
    /// 
    /// <param name="c"></param>
    /// <param name="ms"></param>
    /// 
    public static void
    CollectionToBuf(ICollection<string> c, MemoryStream ms)
    {
      if (c == null)
      {
        Serial.Int32ToBuf(-1, ms);
        return;
      }

      int n = c.Count;
      Serial.Int32ToBuf(n, ms);

      foreach (var i in c)
      {
        Serial.StringToBuf(i, ms);
      }
    }

    /// <summary>
    ///   Deserializes a collection of strings.
    ///   
    ///   Output collection (`c`) is cleared if the serialized collection was not null.
    /// </summary>
    /// 
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// 
    /// <returns> True if the list was null, false otherwise. </returns>
    /// 
    public static bool
    BufToCollection(byte[] buf, ref int offset, ICollection<string> c)
    {
      if (c == null)
      {
        throw new NullReferenceException("ICollection to be filled by the deserialization must be initialized.");
      }

      int count = Serial.BufToInt32(buf, ref offset);
      if (count < 0)
      {
        return true;
      }

      c.Clear();

      for (int i = 0; i < count; i++)
      {
        c.Add(Serial.BufToString(buf, ref offset));
      }

      return false;
    }




  }
}
