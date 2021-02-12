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
    /// <param name="fwd"></param>
    /// <param name="list"></param>
    /// 
    /// <returns> True if the list was null, false otherwise. </returns>
    /// 
    public static bool
    BufToCollection<T>(byte[] buf, int offset, ref int byteFwd, ICollection<T> c)
      where T : IBufSeriable, new()
    {
      if (c == null)
      {
        throw new NullReferenceException("ICollection to be filled by the deserialization must be initialized.");
      }

      int idx = offset;
      bool ret = false;

      int count = Serial.BufToInt32(buf, idx, ref idx);
      if (count < 0)
      {
        ret = true;
        goto set_fwd;
      }

      c.Clear();

      for (int i = 0; i < count; i++)
      {
        T t = new T();
        t.Deserialize(buf, idx, ref idx);
        c.Add(t);
      }

    set_fwd:
      byteFwd += (idx - offset);
      return ret;
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
    /// <param name="fwd"></param>
    /// <param name="list"></param>
    /// 
    /// <returns> True if the list was null, false otherwise. </returns>
    /// 
    public static bool
    BufToCollection(byte[] buf, int offset, ref int byteFwd, ICollection<string> c)
    {
      if (c == null)
      {
        throw new NullReferenceException("ICollection to be filled by the deserialization must be initialized.");
      }

      int idx = offset;
      bool ret = false;

      int count = Serial.BufToInt32(buf, idx, ref idx);
      if (count < 0)
      {
        ret = true;
        goto set_fwd;
      }

      c.Clear();

      for (int i = 0; i < count; i++)
      {
        c.Add(Serial.BufToString(buf, idx, ref idx));
      }

    set_fwd:
      byteFwd += (idx - offset);
      return ret;
    }




  }
}
