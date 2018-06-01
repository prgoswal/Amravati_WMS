using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class DlReports
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd; SqlDataAdapter da; DataTable dt;

    public DlReports()
    {
    }

    public DataTable GetReport(PlReports plRpt)
    {
        cmd = new SqlCommand("SPReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", plRpt.Ind);
        //cmd.Parameters.AddWithValue("@RptInd", plRpt.RptInd);
        cmd.Parameters.AddWithValue("@DateFrom", plRpt.DateFrom);
        cmd.Parameters.AddWithValue("@DateTo", plRpt.DateTo);
        cmd.CommandTimeout = 0;
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable GetRptFoilCFValidity(PlReports plRpt)
    {
        cmd = new SqlCommand("SPReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", plRpt.Ind);
        //cmd.Parameters.AddWithValue("@RptInd", plRpt.RptInd);
        //cmd.Parameters.AddWithValue("@ExamYear", plRpt.ExamYear);
        //cmd.Parameters.AddWithValue("@Session", plRpt.Session);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable GetPhysicalPath(PlReports plRpt)
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", plRpt.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
}