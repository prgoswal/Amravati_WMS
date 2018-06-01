using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;


public class DlChangePwd
{
    public DlChangePwd()
    {

    }
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd; SqlDataAdapter da; SqlDataReader dr; DataTable dt;
    public int SelectOldPwd(PlChangePwd pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@UserId", pl.UserLoginId);
        cmd.Parameters.AddWithValue("@LoginPwd", pl.OldPwd);
        con.Open();
        //int i = Convert.ToInt32(cmd.ExecuteScalar());
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }

    public int ChangePwd(PlChangePwd pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@UserId", pl.UserLoginId);
        cmd.Parameters.AddWithValue("@LoginPwd", pl.OldPwd);
        cmd.Parameters.AddWithValue("@NewPwd", pl.NewPwd);
        con.Open();
        //int j = Convert.ToInt32(cmd.ExecuteScalar());
        int j = cmd.ExecuteNonQuery();
        con.Close();
        return j;
    }

    public DataTable CheckPassword(PlChangePwd pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@UserId", pl.UserLoginId);
        cmd.Parameters.AddWithValue("@NewPwd", pl.NewPwd);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }
    public DataTable InsertPwd(PlChangePwd pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@UserId", pl.UserLoginId);
        cmd.Parameters.AddWithValue("@NewPwd", pl.NewPwd);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

}
