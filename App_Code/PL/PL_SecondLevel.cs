using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PL_SecondLevel
/// </summary>
public class PL_SecondLevel
{
	public PL_SecondLevel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int ApplicationID { get; set; }
    public string StudentName { get; set; }

    public string ExamYear { get; set; }
    public string ExamSession { get; set; }
    public string ExamName { get; set; }
    public int Ind { get; set; }
    public decimal SubmittedFees { get; set; }
    public string Rollno { get; set; }
    public string AppliedFor { get; set; }
    public string Remarks { get; set; }
    public string InserttionDate { get; set; }
    public string InsertionBy { get; set; }
    public string InsertionByIP { get; set; }
    public string EstimateDate { get; set; }
    public string ItemDesc { get; set; }
    public int PartARegNo { get; set; }
    public string PartAImagePath { get; set; }
    public int PartBRegNo { get; set; }
    public string PartBImagePath { get; set; }

    public int itemId { get; set; }
    public DataTable tbl1 { get; set; }
    public DataTable tbl2 { get; set; }
    public string DocumentType { get; set; }
    public string SName { get; set; }
    public string FilePath { get; set; }
    public DataSet ds { get; set; }
    public string ApprovalCount { get; set; }
    public int DocTypeID { get; set; }
    public string BranchName { get; set; }
    public string EnrollmentNo { get; set; }
    public int DocType { get; set; }
  
    public string College { get; set; }
    public string CGPA { get; set; }
    public string Division { get; set; }
    public string UserID { get; set; }
    public string EntryByIP { get; set; }
    public string UserTypeId { get; set; }
    public string maxaproval { get; set; }
    public int recordId { get; set; }
    public int levelid { get; set; }
    public int isconfirmed { get; set; }
    public int acnt { get; set; }
    public DataTable dt { get; set; }
    //public string LevelRemark { get; set; }
    public int OccCtrl { get; set; }
 
}