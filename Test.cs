using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BufSeria
{

  public class ObjSer: IBufSeriable
  {
    public int A;

    public byte[] Serialize()
    {
      return new byte[] { 1, 2, 3, 4 };
    }

    public void Deserialize(ReadOnlySpan<byte> buf, ref int offset)
    {
      this.A = buf[offset];
      offset += 4;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public static class Test
  {

    /// <summary>
    /// 
    /// </summary>
    public static void Test_Combine()
    {
      using MemoryStream ms = new();
      int offset = 0;

      Serial.StringToBuf("string 1", ms);
      Serial.StringToBuf("", ms);
      Serial.BytesToBuf(new byte[] { 1, 2, 3 }, ms);
      Serial.UInt16ToBuf(234, ms);
      Serial.ByteToBuf(23, ms);
      Serial.StringToBuf(null, ms);
      Serial.Int64ToBuf(2252526526, ms);
      Serial.StringToBuf("final", ms);

      ReadOnlySpan<byte> a = ms.ToArray();

      string s1 = Serial.BufToString(a, ref offset);
      string s2 = Serial.BufToString(a, ref offset);
      byte[] b1 = Serial.BufToBytes(a, ref offset);
      ushort ush1 = Serial.BufToUInt16(a, ref offset);
      byte by1 = Serial.BufToByte(a, ref offset);
      string s3 = Serial.BufToString(a, ref offset);
      long u1 = Serial.BufToInt64(a, ref offset);
      string sf = Serial.BufToString(a, ref offset);

      Debug.Assert(s1 == "string 1"
        && s2 == ""
        && b1.Length == 3
        && ush1 == 234
        && by1 == 23
        && s3 == null
        && u1 == 2252526526
        && sf == "final");
    }


    public static void Test_Stream()
    {
      using MemoryStream ms = new();
      int offset = 0;

      MemoryStream ms1 = new();
      ms1.Write(new byte[] { 6, 7, 8, 9 });

      ms.SetLength(0);
      Serial.StreamToBuf(null, ms);
      offset = 0;
      Debug.Assert(Serial.BufToStream(ms.ToArray(), ref offset) == null);

      ms.SetLength(0);
      Serial.StreamToBuf(ms1, ms);
      offset = 0;
      Debug.Assert(Serial.BufToStream(ms.ToArray(), ref offset).Length == 4);
      offset = 0;
      Debug.Assert(Serial.BufToStream(ms.ToArray(), ref offset).ReadByte() == 6);

    }


    /// <summary>
    /// 
    /// </summary>
    public static void Test_Collection()
    {
      using MemoryStream ms = new();
      int offset = 0;

      List<string> output = new List<string>();
      bool wasNull;

      ms.SetLength(0);
      Serial.CollectionToBuf(null, ms);
      offset = 0;
      wasNull = Serial.BufToCollection(ms.ToArray(), ref offset, output);
      Debug.Assert(wasNull);

      ms.SetLength(0);
      Serial.CollectionToBuf(new List<string>() { "a", "b", "c" }, ms);
      offset = 0;
      wasNull = Serial.BufToCollection(ms.ToArray(), ref offset, output);
      Debug.Assert(!wasNull);
      Debug.Assert(output[0] == "a" && output[1] == "b" && output[2] == "c");

      // Todo, test of IBufSerializable.
      List<ObjSer> objs = new List<ObjSer>();

      ms.SetLength(0);
      offset = 0;
      Serial.CollectionToBuf<ObjSer>(new List<ObjSer>() { new ObjSer(), new ObjSer() }, ms);
      wasNull = Serial.BufToCollection<ObjSer>(ms.ToArray(), ref offset, objs);
      Debug.Assert(objs.Count == 2);
      Debug.Assert(objs[0].A == 1);
      Debug.Assert(objs[1].A == 1);

      ms.SetLength(0);
      offset = 0;
      Serial.CollectionToBuf<ObjSer>(new List<ObjSer>(), ms);
      wasNull = Serial.BufToCollection<ObjSer>(ms.ToArray(), ref offset, objs);
      Debug.Assert(objs.Count == 0);

      ms.SetLength(0);
      offset = 0;
      Serial.CollectionToBuf<ObjSer>(null, ms);
      wasNull = Serial.BufToCollection<ObjSer>(ms.ToArray(), ref offset, objs);
      Debug.Assert(wasNull);

    }

    /// <summary>
    /// 
    /// </summary>
    public static void Test_Bytes()
    {
      using MemoryStream ms = new();
      int offset = 0;

      ms.SetLength(0);
      Serial.BytesToBuf(null, ms);
      offset = 0;
      Debug.Assert(Serial.BufToBytes(ms.ToArray(), ref offset) == null);

      ms.SetLength(0);
      Serial.BytesToBuf(new byte[0], ms);
      offset = 0;
      Debug.Assert(Serial.BufToBytes(ms.ToArray(), ref offset).Length == 0);

      ms.SetLength(0);
      Serial.BytesToBuf(new byte[3] { 1, 2, 3 }, ms);
      offset = 0;
      byte[] res = Serial.BufToBytes(ms.ToArray(), ref offset);
      Debug.Assert(res.Length == 3 && res[0] == 1 && res[1] == 2 && res[2] == 3);
    }

    /// <summary>
    /// 
    /// </summary>
    public static void Test_Integral()
    {
      using MemoryStream ms = new();
      int offset = 0;

      ms.SetLength(0);
      Serial.SByteToBuf(-34, ms);
      offset = 0;
      Debug.Assert(Serial.BufToSByte(ms.ToArray(), ref offset) == -34);

      ms.SetLength(0);
      Serial.ByteToBuf(42, ms);
      offset = 0;
      Debug.Assert(Serial.BufToByte(ms.ToArray(), ref offset) == 42);

      ms.SetLength(0);
      Serial.Int16ToBuf(2452, ms);
      offset = 0;
      Debug.Assert(Serial.BufToInt16(ms.ToArray(), ref offset) == 2452);

      ms.SetLength(0);
      Serial.Int16ToBuf(-2452, ms);
      offset = 0;
      Debug.Assert(Serial.BufToInt16(ms.ToArray(), ref offset) == -2452);

      ms.SetLength(0);
      Serial.UInt16ToBuf(42000, ms);
      offset = 0;
      Debug.Assert(Serial.BufToUInt16(ms.ToArray(), ref offset) == 42000);

      ms.SetLength(0);
      Serial.Int32ToBuf(53526262, ms);
      offset = 0;
      Debug.Assert(Serial.BufToInt32(ms.ToArray(), ref offset) == 53526262);

      ms.SetLength(0);
      Serial.Int32ToBuf(-53526262, ms);
      offset = 0;
      Debug.Assert(Serial.BufToInt32(ms.ToArray(), ref offset) == -53526262);

      ms.SetLength(0);
      Serial.UInt32ToBuf(53526262, ms);
      offset = 0;
      Debug.Assert(Serial.BufToUInt32(ms.ToArray(), ref offset) == 53526262);



      ms.SetLength(0);
      Serial.Int64ToBuf(5352626232, ms);
      offset = 0;
      Debug.Assert(Serial.BufToInt64(ms.ToArray(), ref offset) == 5352626232);

      ms.SetLength(0);
      Serial.Int64ToBuf(-5352626232, ms);
      offset = 0;
      Debug.Assert(Serial.BufToInt64(ms.ToArray(), ref offset) == -5352626232);

      ms.SetLength(0);
      Serial.UInt64ToBuf(5352626232, ms);
      offset = 0;
      Debug.Assert(Serial.BufToUInt64(ms.ToArray(), ref offset) == 5352626232);

    }


    /// <summary>
    /// 
    /// </summary>
    public static void Test_Float()
    {
      using MemoryStream ms = new();
      int offset = 0;

      ms.SetLength(0);
      Serial.FloatToBuf(22.3f, ms);
      offset = 0;
      Debug.Assert(Serial.BufToFloat(ms.ToArray(), ref offset) == 22.3f);

      ms.SetLength(0);
      Serial.DoubleToBuf(3975.354d, ms);
      offset = 0;
      Debug.Assert(Serial.BufToDouble(ms.ToArray(), ref offset) == 3975.354d);
    }




    /// <summary>
    /// 
    /// </summary>
    public static void Test_Bool()
    {
      using MemoryStream ms = new();
      int offset = 0;

      ms.SetLength(0);
      Serial.BoolToBuf(false, ms);
      offset = 0;
      Debug.Assert(Serial.BufToBool(ms.ToArray(), ref offset) == false);

      ms.SetLength(0);
      Serial.BoolToBuf(true, ms);
      offset = 0;
      Debug.Assert(Serial.BufToBool(ms.ToArray(), ref offset) == true);
    }




    /// <summary>
    /// 
    /// </summary>
    public static void Test_String()
    {
      using MemoryStream ms = new();
      int offset = 0;

      ms.SetLength(0);
      Serial.StringToBuf(null, ms);
      offset = 0;
      Debug.Assert(Serial.BufToString(ms.ToArray(), ref offset) == null);

      ms.SetLength(0);
      Serial.StringToBuf("", ms);
      offset = 0;
      Debug.Assert(Serial.BufToString(ms.ToArray(), ref offset) == "");

      ms.SetLength(0);
      Serial.StringToBuf("AAAAABBBBIEHIHEHGIEH", ms);
      offset = 0;
      Debug.Assert(Serial.BufToString(ms.ToArray(), ref offset) == "AAAAABBBBIEHIHEHGIEH");
    }



    /// <summary>
    /// 
    /// </summary>
    public static void Test_BufLen()
    {
      using MemoryStream ms = new();
      int offset = 0;
      int n;

      LenSerial.EncodeNull(ms);
      Debug.Assert(ms.ToArray()[0] == (byte)EncodedLen.NULL);

      ms.SetLength(0);
      LenSerial.Encode(10, ms);
      offset = 0;
      Debug.Assert(LenSerial.Decode(ms.ToArray(), ref offset) == 10);

      ms.SetLength(0);
      n = LenSerial.Encode(2345, ms);
      Debug.Assert(n == 3 && ms.ToArray().Length == 3);
      offset = 0;
      Debug.Assert(LenSerial.Decode(ms.ToArray(), ref offset) == 2345);

      ms.SetLength(0);
      LenSerial.Encode(123123123, ms);
      offset = 0;
      Debug.Assert(LenSerial.Decode(ms.ToArray(), ref offset) == 123123123);

    }



  }
}
