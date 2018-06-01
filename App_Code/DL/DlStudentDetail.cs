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

/// <summary>
/// Summary description for DlStudentDetail
/// </summary>
public class DlStudentDetail
{
    
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd; SqlDataAdapter da; DataTable dt;
	public DlStudentDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetExamYear(PlStudentDetail pl)
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public DataTable GetExamSession(PlStudentDetail pl)
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public DataTable GetExamType(PlStudentDetail pl) 
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable GetExamName(PlStudentDetail pl) 
    {
        cmd = new SqlCommand("SPExamMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable GetForApply(PlStudentDetail pl)
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public DataTable GetStudentDetail(PlStudentDetail pl)
    {
      
        cmd = new SqlCommand("SPApplicationDetail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);

        cmd.Parameters.AddWithValue("@ExamSession", pl.ExamSession);
        cmd.Parameters.AddWithValue("@ExamYear", pl.ExamYear);
        cmd.Parameters.AddWithValue("@StudentName", pl.StudentName);
        cmd.Parameters.AddWithValue("@Rollno", pl.Rollno);
        //cmd.Parameters.AddWithValue("@SumittedFees", pl.SubmittedFees);
        //cmd.Parameters.AddWithValue("@ApplicationID", pl.ApplicationID);
        //cmd.Parameters.AddWithValue("@Remarks", pl.Remarks);
        //cmd.Parameters.AddWithValue("@InsertionDate", pl.InserttionDate);
        //cmd.Parameters.AddWithValue("@InsertionBy", pl.InsertionBy);
        //cmd.Parameters.AddWithValue("@InsertionByIP", pl.InsertionByIP);
        //cmd.Parameters.AddWithValue("@EstimateDate", pl.EstimateDate);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToInt32(dt.Rows[0]["RollNo"]) == 0)
            {

                dt = null;
            }

        }
        else
            dt = null;
        return dt;
    }
    public DataTable SaveStudentDetail(PlStudentDetail pl)
    {
          
       // decimal roll = Convert.ToDecimal(pl.Rollno);
        //cmd = new SqlCommand("select SName,RollNo from TblFoilImgEntry where RollNo='" + roll + "'  ", con);
        cmd = new SqlCommand("SPApplicationDetail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@AppID", pl.ApplicationID);
        cmd.Parameters.AddWithValue("@ExamSession", Convert.ToInt32(pl.ExamSession));
        cmd.Parameters.AddWithValue("@ExamYear", Convert.ToInt32(pl.ExamYear));
        cmd.Parameters.AddWithValue("@ExamName", pl.ExamName);
        cmd.Parameters.AddWithValue("@StudentName", pl.StudentName);
        cmd.Parameters.AddWithValue("@RollNo", pl.Rollno);
        cmd.Parameters.AddWithValue("@Remarks", pl.Remarks);
        cmd.Parameters.AddWithValue("@AppliedFor", pl.AppliedFor);
        cmd.Parameters.AddWithValue("@SubmittedFees", pl.SubmittedFees);
        
        cmd.Parameters.AddWithValue("@PartARegNo", pl.PartARegNo);
        cmd.Parameters.AddWithValue("@PartAImagePath", pl.PartAImagePath);
        cmd.Parameters.AddWithValue("@PartBRegNo", pl.PartBRegNo);
        cmd.Parameters.AddWithValue("@PartBImagePath", pl.PartBImagePath);
       // cmd.Parameters.AddWithValue("@InsertionDate", pl.InserttionDate);
        cmd.Parameters.AddWithValue("@InsertionBy", pl.InsertionBy);
        cmd.Parameters.AddWithValue("@InsertionByIP", pl.InsertionByIP);
        cmd.Parameters.AddWithValue("@EstimateDate",Convert.ToDateTime(pl.EstimateDate));
        con.Open();
        
        
        int Ind = 0;
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt != null)
        {

            if (dt.Rows.Count > 0)
            {
             

                Ind = 1;
            
            }
        }
        else
            dt = null;
        return dt;
    }

}
