using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class DlExamMaster
{
    public DlExamMaster()
    {
    }
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd; SqlDataAdapter da; DataTable dt;

    public int InsertExamMaster(PlExamMaster pl)
    {
        cmd = new SqlCommand("SPExamMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@UnivCode", pl.UnivCode);
        //cmd.Parameters.AddWithValue("@Ctrl", pl.Ctrl);
        cmd.Parameters.AddWithValue("@FacultyCD", pl.FacultyCD);
        cmd.Parameters.AddWithValue("@ExamName", pl.ExamName);
        cmd.Parameters.AddWithValue("@ExamShortName", pl.ExamShortName);
        cmd.Parameters.AddWithValue("@ExamType", pl.ExamType);
        cmd.Parameters.AddWithValue("@TotalYearSem", pl.TotalYearSem);
        cmd.Parameters.AddWithValue("@NoOfYearSem", pl.NoOfYearSem);
        cmd.Parameters.AddWithValue("@CourseCD", pl.CourseCD);
        cmd.Parameters.AddWithValue("@CourseName", pl.CourseName);
        cmd.Parameters.AddWithValue("@RefCtrl", pl.RefCtrl);
        cmd.Parameters.AddWithValue("@IPAddress", pl.IPAddress);
        cmd.Parameters.AddWithValue("@UserID", pl.UserID);
        cmd.Parameters.AddWithValue("@CreationDate", pl.CreationDate);
        cmd.Parameters.AddWithValue("@GroupID", pl.GroupID);
        cmd.Parameters.AddWithValue("@GroupSubID", pl.GroupSubID);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }

    public DataTable GetLastExamMaster(PlExamMaster pl)
    {
        cmd = new SqlCommand("SPExamMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
}