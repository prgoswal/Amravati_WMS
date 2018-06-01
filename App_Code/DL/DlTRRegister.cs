using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public class DlTRRegister
{
    public DlTRRegister()
    {
    }
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd; SqlDataAdapter da; DataTable dt;

    public DataTable GetAllRegister(plTrRegister pl, int Ind)
    {

        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", Ind);
        if (Ind == 6)
        cmd.Parameters.AddWithValue("@ItmId", pl.ItemID);

        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;


    }

    public DataTable GetExamName(int Ind, int facultytype)
    {

        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", Ind);
        cmd.Parameters.AddWithValue("@Facultytype", facultytype);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;

    }

    public DataTable GetLoginDetails(plTrRegister pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@LoginId", pl.UserLoginId);
        cmd.Parameters.AddWithValue("@LoginPwd", pl.LoginPwd);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public int ChackLoginId(plTrRegister pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@LoginId", pl.UserLoginId);
        cmd.Parameters.AddWithValue("@LoginPwd", pl.LoginPwd);
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }
    public int ChangeLoginIdStatus(plTrRegister pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@UserId", pl.UserId);
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }

    public int CheckLockInd(plTrRegister pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@LoginId", pl.UserLoginId);
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }

    public DataTable GetLoginDetailsfromservice(plTrRegister pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 11);
        cmd.Parameters.AddWithValue("@LoginId", pl.UserLoginId);
        cmd.Parameters.AddWithValue("@LoginPwd", pl.LoginPwd);
        da = new SqlDataAdapter(cmd);
        pl.dt = new DataTable();
        da.Fill(pl.dt);
        return pl.dt;
    }

}