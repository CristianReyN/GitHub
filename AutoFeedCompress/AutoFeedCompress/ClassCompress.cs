using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;

namespace AutoFeedCompress
{
  public class ClassCompress
  {
    ClassDB clsData = new ClassDB();

    public void StartProcess(bool blnCompress, int intFID)
    {
      if (blnCompress == true)
      {
        SqlDataReader dr = null;

        if (intFID == 0)
          dr = clsData.GetFulfillmentIDs();
        else
          dr = clsData.GetFulfillmentIDsByFID(intFID);

        while (dr.Read())
        {
          Execute(Convert.ToInt32(dr["FulfillmentID"]), intFID);
        }

        dr.Close();
        dr.Dispose();
        dr = null;
      }
      else
      {
        DeCompressToFile(2690);
      }
    }

    private void Execute(int intFulfillmentID, int intFID)
    {
      try
      {
        if ((intFID == 0) && (intFulfillmentID > 0))
        {
          byte[] data;
          //byte[] cmpData;

          //create temp zip file
          // OnMessage("Reading file to memory");
          //128073--423
          //128315--linkedin
          SqlDataReader dr = clsData.GetFromDatabase(intFulfillmentID, CommandBehavior.CloseConnection); //File.ReadAllBytes(@"D:\CRey\from me\fulfilled.txt");

          dr.Read();

          data = (byte[])dr["datasent"];

          // OnMessage("Zipping file to memory");
          byte[] compressedData = CompressData(data);
          //**File.WriteAllBytes(@"d:\crey\from me\compressed.zip", compressedData);
          // OnMessage("Saving file to database");
          clsData.SaveToDatabase(compressedData, intFulfillmentID);
        }
        else
        {
          clsData.CleanUpFulfillmentByID(intFulfillmentID);
        }
      }
      catch
      {
      }
    }

    private static byte[] CompressData(byte[] data)
    {
      var output = new MemoryStream();

      using (Stream fs = new System.IO.MemoryStream(data))
      using (var gzip = new GZipStream(output, CompressionMode.Compress, true))
      {
        byte[] buffer = new byte[1024];
        int nRead;
        while ((nRead = fs.Read(buffer, 0, buffer.Length)) > 0)
        {
          gzip.Write(buffer, 0, nRead);
        }

        /*
        gzip.Write(data, 0, data.Length);
        gzip.Close();
         */
      }
      return output.ToArray();
    }

    private void DeCompressToFile(int intFulfillmentID)
    {
      SqlDataReader dr = clsData.GetCompressedFileData(intFulfillmentID);
      dr.Read();
      byte[] data = (byte[])dr["CompressedDataSent"];

      byte[] decompressed = Decompress(data);

      File.WriteAllBytes(@"d:\crey\from me\update.txt", decompressed);
    }

    private static byte[] Decompress(byte[] data)
    {
      var output = new MemoryStream();
      var input = new MemoryStream();
      input.Write(data, 0, data.Length);
      input.Position = 0;

      using (var gzip = new GZipStream(input, CompressionMode.Decompress, true))
      {
        var buff = new byte[1024];
        var read = gzip.Read(buff, 0, buff.Length);

        while (read > 0)
        {
          output.Write(buff, 0, read);
          read = gzip.Read(buff, 0, buff.Length);
        }

        gzip.Close();
      }
      return output.ToArray();
    }
  }
}
