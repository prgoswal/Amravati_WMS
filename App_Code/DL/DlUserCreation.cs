using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class DlUserCreation
{
    public DlUserCreation()
    {
    }
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd; SqlDataAdapter da; DataTable dt;

    public int InsertUserEntry(PlUserCreation pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@UserTypeID", pl.UserType);
        cmd.Parameters.AddWithValue("@UserName", pl.UserName);
        cmd.Parameters.AddWithValue("@UserState", pl.State);
        cmd.Parameters.AddWithValue("@UserCity", pl.City);
        cmd.Parameters.AddWithValue("@UserAddress", pl.Address);
        cmd.Parameters.AddWithValue("@UserMobileNo", pl.ContactNo);
        cmd.Parameters.AddWithValue("@UserEmailAddr", pl.Email);
        cmd.Parameters.AddWithValue("@UserLoginID", pl.LoginId);
        cmd.Parameters.AddWithValue("@UserLoginPwd", pl.Pwd);
        cmd.Parameters.AddWithValue("@IsOutSideUser", pl.IsOutSideUser);
        cmd.Parameters.AddWithValue("@IsActive", pl.IsActive);
        cmd.Parameters.AddWithValue("@CreationDate", pl.CreationDate);
        cmd.Parameters.AddWithValue("@UserLoginStatus", pl.UserLoginStatus);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }

    public DataTable GetLockedUsers(PlUserCreation pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@UserId", pl.UserId);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public int UpdateUnlockedUser(PlUserCreation pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@LockInd", pl.LockId);
        cmd.Parameters.AddWithValue("@UserId", pl.UserId);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }

}