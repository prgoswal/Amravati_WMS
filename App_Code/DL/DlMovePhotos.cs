using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class DlMovePhotos
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MlcvConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd; SqlDataAdapter da; DataTable dt;
    public DlMovePhotos()
    {
    }
    public DataTable GetDestPath(PlMovePhotos pl)
    {
        cmd = new SqlCommand("SPPhotoMove", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public int InsertDestPath(PlMovePhotos pl)
    {
        cmd = new SqlCommand("SPPhotoMove", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@PartType", pl.Ind);
        cmd.Parameters.AddWithValue("@SourcePath", pl.SourcePath);
        cmd.Parameters.AddWithValue("@DestPath", pl.DestPath);
        cmd.Parameters.AddWithValue("@PartNoFrom", pl.PartNoFrom);
        cmd.Parameters.AddWithValue("@PartNoTo", pl.PartNoTo);
        cmd.Parameters.AddWithValue("@RemainingParts", pl.RemainingPart);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }

    public DataTable GetMachineName(PlMovePhotos pl)
    {
        cmd = new SqlCommand("SPPhotoMove", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }


    // ---------------------------------------------------------------------------------------
    public int GetPartNoFromTo(PlMovePhotos pl)
    {
        cmd = new SqlCommand("SPPhotoMove", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@PartNoFrom", pl.PartNoFrom);
        cmd.Parameters.AddWithValue("@PartNoTo", pl.PartNoTo);
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }

    public int UpdateRemainingPart(PlMovePhotos pl)
    {
        cmd = new SqlCommand("SPPhotoMove", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@PartNoFrom", pl.PartNoFrom);
        cmd.Parameters.AddWithValue("@PartNoTo", pl.PartNoTo);
        cmd.Parameters.AddWithValue("@RemainingParts", pl.RemainingPart);
        cmd.Parameters.AddWithValue("@MoveDate", pl.MoveDate);
        cmd.Parameters.AddWithValue("@TotalImagesCopied", pl.TotalImagesCopied);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }

    // ---------------------------------------------------------------------------------------

    public DataTable FindLastNo(PlMovePhotos pl)
    {
        cmd = new SqlCommand("SPPhotoMove", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
}