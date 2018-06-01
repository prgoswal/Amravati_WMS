using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DLFileUpload
/// </summary>
public class DLFileUpload
{
    //SqlConnection ConPrg50 = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDb"].ToString());
    SqlConnection LocalCon = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = null;
    DataTable dt; DataSet ds; SqlDataAdapter sda;

    public bool CheckTable(PlUpload PlFU)//
    {
        cmd = new SqlCommand("SPFileUpload", LocalCon);
        cmd.CommandType = CommandType.StoredProcedure;        
        cmd.Parameters.AddWithValue("@Ind", 2);
        cmd.Parameters.AddWithValue("@TableName", PlFU.tableName);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        //int i;
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            return true;
            //i = dt.Rows.Count;    
        }
        else
        {
            return false;
            //i = 0;
        }
        //return i;     
    }

    public void TruncateTable(PlUpload PlFU)///
    {
        cmd = new SqlCommand("SPFileUpload", LocalCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 2);
        cmd.Parameters.AddWithValue("@tableName", PlFU.tableName);

        LocalCon.Open();
        cmd.ExecuteNonQuery();
        LocalCon.Close();                
    }

    public DataTable GetSession(PlUpload Plobjfu)
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 2);
        dt = new DataTable();
        sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        return dt;
    }

    public DataTable GetExamName(PlUpload Plobjfu)
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 3);
        dt = new DataTable();
        sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        return dt;
    }

    public void BulkDataInsert(DataTable dt, string tableName)//
    {
        ConClose();
        SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(LocalCon);
        sqlBulkCopy.DestinationTableName = tableName;
        LocalCon.Open();
        sqlBulkCopy.WriteToServer(dt);
        LocalCon.Close();
    }

    public int DynamicTable(string Query)///
    {
        try
        {
        ConClose();
        int status = 0;
            SqlCommand CreateTableCMD = new SqlCommand(Query, LocalCon);
            LocalCon.Open();
            status =  CreateTableCMD.ExecuteNonQuery();
            LocalCon.Close();
            return status;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //try
        //{
        //}
        //catch { }
        //finally
        //{
        //    con.Close();
        //    con.Dispose();
        //}
        //return status;
    }

    public bool FileNameCheck(PlUpload PlFU)
    {
        cmd = new SqlCommand("SPFileUpload", LocalCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 3);
        cmd.Parameters.AddWithValue("@TableName", PlFU.tableName);
        cmd.Parameters.AddWithValue("@FileName", PlFU.FileName);
        dt = new DataTable();
        sda = new SqlDataAdapter(cmd);
       
        bool i;
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            i = true;
        }
        else
        {
            i = false;
        }
        return i;
    }

    public void DeleteData(PlUpload PlFU)
    {
        cmd = new SqlCommand("SPFileUpload", LocalCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 4);
        cmd.Parameters.AddWithValue("@TableName", PlFU.tableName);
        cmd.Parameters.AddWithValue("@FileName", PlFU.FileName);
        dt = new DataTable();
        sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
    }

    public DataTable DtColumnCheck(string TableName)
    {
        cmd = new SqlCommand("SPFileUpload", LocalCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 5);
        cmd.Parameters.AddWithValue("@TableName", TableName);
        dt = new DataTable();
        sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        return dt;
        //throw new NotImplementedException();
    }

    public DataSet CreateRDRL(PlUpload PlFU)
    {
        cmd = new SqlCommand("SPFileUpload", LocalCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        cmd.Parameters.AddWithValue("@Ind", 6);
        cmd.Parameters.AddWithValue("@ExamYear", PlFU.ExamYear);
        cmd.Parameters.AddWithValue("@ExamSession", PlFU.ExamSession);
        DataSet ds = new DataSet();
        sda = new SqlDataAdapter(cmd);
        sda.Fill(ds);
        return ds;
    }

    public bool CheckRDRL(PlUpload PlFU)//
    {
        cmd = new SqlCommand("SPFileUpload", LocalCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 7);
        cmd.Parameters.AddWithValue("@ExamSession", PlFU.ExamSession);
        cmd.Parameters.AddWithValue("@ExamYear", PlFU.ExamYear);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        //int i;
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            return true;
            //i = dt.Rows.Count;    
        }
        else
        {
            return false;
            //i = 0;
        }
        //return i;     
    }

    public DataSet SoftAndOccplBind(PlUpload PlFU)
    {
        ds= new DataSet();
        cmd = new SqlCommand("SPFileUpload", LocalCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 8);
        cmd.Parameters.AddWithValue("@ExamSession", PlFU.ExamSession);
        cmd.Parameters.AddWithValue("@ExamYear", PlFU.ExamYear);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }
        return ds = null;
    }

    void ConClose()
    {
        if (LocalCon.State == ConnectionState.Open)
        {
            LocalCon.Close();            
        }
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    public DataSet UniversityAndOccplBind(PlUpload PlFU)
    {
        ds = new DataSet();
        cmd = new SqlCommand("SPFileUpload", LocalCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 10);
        cmd.Parameters.AddWithValue("@ExamSession", PlFU.ExamSession);
        cmd.Parameters.AddWithValue("@ExamYear", PlFU.ExamYear);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }
        return ds = null;
    }
}