using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AutoFeedCompress
{
  public class ClassDB
  {
    private string _MasterSource = string.Empty;

    public ClassDB()
    {
      _MasterSource = "Persist Security Info=False;Integrated Security=SSPI;database=Autofeeds;server=10.10.140.167;Connect Timeout=30";
    }

    public void SaveToDatabase(byte[] p_Data, int p_intFulfillmentID)
    {
      SqlCommand cmm = new SqlCommand();
      using (SqlConnection cnn = new SqlConnection(_MasterSource))
      {
        try
        {
          cnn.Open();

          cmm.Connection = cnn;
          cmm.CommandType = CommandType.StoredProcedure;
          cmm.CommandText = "dbo.WriteCompressedData";
          cmm.CommandTimeout = 1000;
          cmm.Parameters.Add("@FulfillmentID", SqlDbType.Int, -1).Value = p_intFulfillmentID;
          cmm.Parameters.Add("@CompressedDataSent", SqlDbType.VarBinary, -1).Value = p_Data;
          cmm.ExecuteNonQuery();
        }
        finally
        {
          // No need to clean up  the connection since we are using 'using'.

          if (cmm != null)
          {
            cmm.Dispose();
          }

          cmm = null;
        }
      }
      /*
      using (var cmd = Conn.CreateCommand())
      {
        cmd.CommandText = @"MergeFileUploads";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@File", data);
        cmd.Parameters["@File"].DbType = DbType.Binary;

        cmd.Parameters.Add("@SourceField");
        var parameter = cmd.Parameters["@SourceField"];
        parameter.DbType = DbType.Int32;
        parameter.Direction = ParameterDirection.Output;

        cmd.ExecuteNonQuery();
        sourceFileId = (int)parameter.Value;
      }
      */
    }

    public SqlDataReader GetFulfillmentIDsByFID(int p_intFID)
    {
      SqlCommand cmm = new SqlCommand();
      SqlDataReader dr = null;
      SqlConnection cnn = new SqlConnection(_MasterSource);

      bool blnErrorOccurred = false;

      try
      {
        cnn.Open();

        cmm.Connection = cnn;
        cmm.CommandType = CommandType.StoredProcedure;
        cmm.CommandText = "dbo.GetFulfillmentIDsToCleanUp";
        cmm.Parameters.Add("@FID", SqlDbType.Int, -1).Value = p_intFID;

        cmm.CommandTimeout = 1000;
        dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);

        return dr;
      }
      catch (Exception ex)
      {
        blnErrorOccurred = true;

        throw ex;
      }
      finally
      {
        if (blnErrorOccurred == true)
        {
          if (cnn != null)
          {
            if (cnn.State == ConnectionState.Open)
            {
              cnn.Close();
            }
          }

          cnn = null;

          if (!dr.IsClosed)
          {
            dr.Close();
          }

          dr = null;
        }

        if (cmm != null)
        {
          cmm.Dispose();
        }

        cmm = null;
      }
    }

    public void CleanUpFulfillmentByID(int p_intFulfillmentID)
    {
      SqlCommand cmm = new SqlCommand();
      using (SqlConnection cnn = new SqlConnection(_MasterSource))
      {
        try
        {
          cnn.Open();

          cmm.Connection = cnn;
          cmm.CommandType = CommandType.StoredProcedure;
          cmm.CommandText = "dbo.CleanUpFulfillmentByID";
          cmm.CommandTimeout = 1000;
          cmm.Parameters.Add("@FulfillmentID", SqlDbType.Int, -1).Value = p_intFulfillmentID;
          cmm.ExecuteNonQuery();
        }
        finally
        {
          // No need to clean up  the connection since we are using 'using'.

          if (cmm != null)
          {
            cmm.Dispose();
          }

          cmm = null;
        }
      }
    }

    public SqlDataReader GetCompressedFileData(int p_intFulfillmentID)
    {
      SqlCommand cmm = new SqlCommand();
      SqlDataReader dr = null;
      SqlConnection cnn = new SqlConnection(_MasterSource);

      bool blnErrorOccurred = false;

      try
      {
        cnn.Open();

        cmm.Connection = cnn;
        cmm.CommandType = CommandType.StoredProcedure;
        cmm.CommandText = "dbo.GetCompressedFileData";

        cmm.Parameters.Add("@FulfillmentID", SqlDbType.Int, -1).Value = p_intFulfillmentID;
        dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);

        return dr;
      }
      catch (Exception ex)
      {
        blnErrorOccurred = true;

        throw ex;
      }
      finally
      {
        if (blnErrorOccurred == true)
        {
          if (cnn != null)
          {
            if (cnn.State == ConnectionState.Open)
            {
              cnn.Close();
            }
          }

          cnn = null;

          if (!dr.IsClosed)
          {
            dr.Close();
          }

          dr = null;
        }

        if (cmm != null)
        {
          cmm.Dispose();
        }

        cmm = null;
      }
    }

    public SqlDataReader GetFulfillmentIDs()
    {
      SqlCommand cmm = new SqlCommand();
      SqlDataReader dr = null;
      SqlConnection cnn = new SqlConnection(_MasterSource);

      bool blnErrorOccurred = false;

      try
      {
        cnn.Open();

        cmm.Connection = cnn;
        cmm.CommandType = CommandType.StoredProcedure;
        cmm.CommandText = "dbo.GetFulfillmentIDs";
        cmm.CommandTimeout = 1000;
        dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);

        return dr;
      }
      catch (Exception ex)
      {
        blnErrorOccurred = true;

        throw ex;
      }
      finally
      {
        if (blnErrorOccurred == true)
        {
          if (cnn != null)
          {
            if (cnn.State == ConnectionState.Open)
            {
              cnn.Close();
            }
          }

          cnn = null;

          if (!dr.IsClosed)
          {
            dr.Close();
          }

          dr = null;
        }

        if (cmm != null)
        {
          cmm.Dispose();
        }

        cmm = null;
      }
    }

    public SqlDataReader GetFromDatabase(int p_intFulfillmentID, CommandBehavior p_cmdB)
    {
      SqlCommand cmm = new SqlCommand();
      SqlDataReader dr = null;
      SqlConnection cnn = new SqlConnection(_MasterSource);

      bool blnErrorOccurred = false;

      try
      {
        cnn.Open();

        cmm.Connection = cnn;
        cmm.CommandType = CommandType.StoredProcedure;
        cmm.CommandText = "dbo.GetDataSentToCompress";
        cmm.CommandTimeout = 1000;
        cmm.Parameters.Add("@FulfillmentID", SqlDbType.Int, -1).Value = p_intFulfillmentID;
        dr = cmm.ExecuteReader(p_cmdB);

        return dr;
      }
      catch (Exception ex)
      {
        blnErrorOccurred = true;

        throw ex;
      }
      finally
      {
        if (blnErrorOccurred == true)
        {
          if (cnn != null)
          {
            if (cnn.State == ConnectionState.Open)
            {
              cnn.Close();
            }
          }

          cnn = null;

          if (!dr.IsClosed)
          {
            dr.Close();
          }

          dr = null;
        }

        if (cmm != null)
        {
          cmm.Dispose();
        }

        cmm = null;
      }
    }
  }
}
