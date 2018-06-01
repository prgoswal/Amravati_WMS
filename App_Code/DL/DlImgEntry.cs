using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class DlImgEntry
{
    public DlImgEntry()
    {
    }
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd; SqlDataAdapter da; DataTable dt; DataSet ds;

    public int InserImgEntry(PlImgEntry pl)
    {
            cmd = new SqlCommand("SPImgEntry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", pl.Ind);
            cmd.Parameters.AddWithValue("@RegNo", pl.RegNo);
            cmd.Parameters.AddWithValue("@RollNo", pl.RollNo);
            cmd.Parameters.AddWithValue("@SName", pl.SName);
            cmd.Parameters.AddWithValue("@FilePath", pl.FilePath);
            cmd.Parameters.AddWithValue("@CompletedPages", pl.CompletedPages);
            cmd.Parameters.AddWithValue("@TotalPages", pl.TotalPages);
            cmd.Parameters.AddWithValue("@ErrorMark", pl.ErrorMark);
            cmd.Parameters.AddWithValue("@IpAddress", pl.IpAddress);
            cmd.Parameters.AddWithValue("@UserID", pl.UserId);
            cmd.Parameters.AddWithValue("@CreationDate", pl.CreationDate);
            cmd.Parameters.AddWithValue("@AllotmentNo", pl.AllotmentNo);
            cmd.Parameters.AddWithValue("@EntryByRegNo", pl.EntryByRegNo);
            cmd.Parameters.AddWithValue("@IsUpdated", pl.IsUpdated);
            cmd.Parameters.AddWithValue("@UpdatedBy", pl.UpdatedBy);
            cmd.Parameters.AddWithValue("@UpdationDate", pl.UpdationDate);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            //Int64 i = Convert.ToInt64(cmd.ExecuteScalar());
            con.Close();
            return i;
    }
    public int InsertCFImgEntry(PlImgEntry pl)
    {
        cmd = new SqlCommand("SPImgEntry", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@RollNo", pl.RollNoFrom);
        cmd.Parameters.AddWithValue("@RollNoTo", pl.RollNoTo);
        cmd.Parameters.AddWithValue("@FilePath", pl.FilePath);
        cmd.Parameters.AddWithValue("@CompletedPages", pl.CompletedPages);
        cmd.Parameters.AddWithValue("@TotalPages", pl.TotalPages);
        cmd.Parameters.AddWithValue("@ErrorMark", pl.ErrorMark);
        cmd.Parameters.AddWithValue("@IpAddress", pl.IpAddress);
        cmd.Parameters.AddWithValue("@UserID", pl.UserId);
        cmd.Parameters.AddWithValue("@CreationDate", pl.CreationDate);
        cmd.Parameters.AddWithValue("@AllotmentNo", pl.AllotmentNo);
        cmd.Parameters.AddWithValue("@EntryByRegNo", pl.EntryByRegNo);
        cmd.Parameters.AddWithValue("@IsUpdated", pl.IsUpdated);
        cmd.Parameters.AddWithValue("@UpdatedBy", pl.UpdatedBy);
        cmd.Parameters.AddWithValue("@UpdationDate", pl.UpdationDate);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        //Int64 i = Convert.ToInt64(cmd.ExecuteScalar());
        con.Close();
        return i;
    }
    public int GetICompetedPages(PlImgEntry pl)
    {
        cmd = new SqlCommand("SPImgEntry", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegNo", pl.RegNo);
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }
    public DataSet LastPageNoWithLotNo(PlImgEntry pl)
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegisterNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@UserId", pl.UserId);
        ds = new DataSet();
        da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        return ds;
    }
    public int DeletePageNoData(PlImgEntry pl)
    {
        cmd = new SqlCommand("SPImgEntry", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@CompletedPages", pl.CurrentPageNo);
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }
}