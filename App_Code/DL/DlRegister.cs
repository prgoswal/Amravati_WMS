using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class DlRegister
{
    public DlRegister()
    {
    }

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd; SqlDataAdapter da; DataTable dt; DataSet ds;

    public Int64 InsertRegister(PlRegister pl)
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@Ctrl", pl.Ctrl);
        cmd.Parameters.AddWithValue("@ExamYear", pl.ExamYear);
        cmd.Parameters.AddWithValue("@SessionType", pl.Session);
        cmd.Parameters.AddWithValue("@ExamType", pl.ExamType);
        cmd.Parameters.AddWithValue("@TRType", pl.FoilCuonterfoil);
        cmd.Parameters.AddWithValue("@NoOfBooks", pl.NoOfBook);
        cmd.Parameters.AddWithValue("@CurrentBookNo", pl.CureentBookNo);
        cmd.Parameters.AddWithValue("@PageNoFrom", pl.PageNoFrom);
        cmd.Parameters.AddWithValue("@PageNoTo", pl.PageNoTo);
        cmd.Parameters.AddWithValue("@TotalPages", pl.TotalPage);
        cmd.Parameters.AddWithValue("@UserID", pl.UserID);
        cmd.Parameters.AddWithValue("@IpAddress", pl.IpAddress);
        cmd.Parameters.AddWithValue("@RegCreationDate", pl.RegCreationDate);
        cmd.Parameters.AddWithValue("@FilePath", pl.FilePath);
        con.Open();
        //int i = cmd.ExecuteNonQuery();
        Int64 i = Convert.ToInt64(cmd.ExecuteScalar());
        con.Close();
        return i;
    }

    public int UpdateRegister(PlRegister pl)
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@Ctrl", pl.Ctrl);
        cmd.Parameters.AddWithValue("@ExamYear", pl.ExamYear);
        cmd.Parameters.AddWithValue("@SessionType", pl.Session);
        cmd.Parameters.AddWithValue("@ExamType", pl.ExamType);
        cmd.Parameters.AddWithValue("@TRType", pl.FoilCuonterfoil);
        cmd.Parameters.AddWithValue("@NoOfBooks", pl.NoOfBook);
        cmd.Parameters.AddWithValue("@CurrentBookNo", pl.CureentBookNo);
        cmd.Parameters.AddWithValue("@PageNoFrom", pl.PageNoFrom);
        cmd.Parameters.AddWithValue("@PageNoTo", pl.PageNoTo);
        cmd.Parameters.AddWithValue("@TotalPages", pl.TotalPage);
        cmd.Parameters.AddWithValue("@RegisterNo", pl.RegNo);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }

    public DataTable BindRegisterGrid()
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 4);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable SearchRegister(PlRegister pl)
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegisterNo", pl.RegNo);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }
    public DataSet ShowBothImages(PlRegister pl)
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegisterNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@PageNoFrom", pl.PageNo);
        ds = new DataSet();
        da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        return ds;
    }
    public int CancelRegister(PlRegister pl)
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 8);
        cmd.Parameters.AddWithValue("@RegisterNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@CancelInd", 1);
        cmd.Parameters.AddWithValue("@CancelDate", pl.CancelDate);
        cmd.Parameters.AddWithValue("@UserID", pl.UserID);
        cmd.Parameters.AddWithValue("@CancelledByIp", pl.CancelledIpAddress);
        cmd.Parameters.AddWithValue("@CanceledReason", pl.CancelReason);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }

    public int InsertAvailablePageNo(PlRegister pl)
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegisterNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@PageNoFrom", pl.PageNoFrom);
        cmd.Parameters.AddWithValue("@PageNoTo", pl.PageNoTo);
        cmd.Parameters.AddWithValue("@TotalPages", pl.TotalPage);
        cmd.Parameters.AddWithValue("@Ctrl", pl.Ctrl);
        cmd.Parameters.AddWithValue("@CurrentBookNo", pl.PartNo);
        cmd.Parameters.AddWithValue("@IpAddress", pl.IpAddress);
        cmd.Parameters.AddWithValue("@RegCreationDate", pl.CreationDate);
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }


    public int UpdateRegisterFilePath(PlRegister pl)
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegisterNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@FilePath", pl.FilePath);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }

    public DataTable MoveTrRegisterFiles(PlRegister pl)
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 5);
        cmd.Parameters.AddWithValue("@RegisterNo", pl.RegNo);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }
    public int RegisterEntry(PlRegister pl)
    {
        cmd = new SqlCommand("SPTrScanned", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@SName", pl.SName);
        cmd.Parameters.AddWithValue("@RollNo", pl.RollNo);
        cmd.Parameters.AddWithValue("@RegNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@UserLoginId", pl.UserID);
        cmd.Parameters.AddWithValue("@IpAddress", pl.IpAddress);
        cmd.Parameters.AddWithValue("@LoginDatetime", pl.LoginDatetime);
        cmd.Parameters.AddWithValue("@CreationDate", pl.CreationDate);
        //cmd.Parameters.AddWithValue("@RegisterPageNo", pl.RegisterPageNo);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        //int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }
}


