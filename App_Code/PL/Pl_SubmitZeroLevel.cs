using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Pl_SubmitZeroLevel
/// </summary>
public class Pl_SubmitZeroLevel
{
    public Pl_SubmitZeroLevel()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Ind { get; set; }
    public int ApplicationID { get; set; }
    public string Rollno { get; set; }
    public string SName { get; set; }
    public string ExamName { get; set; }
    public string BranchName { get; set; }
    public string ExamSession { get; set; }
    public string EnrollmentNo { get; set; }
    public string College { get; set; }
    public string CGPA { get; set; }
    public string Division { get; set; }
    public int UserID { get; set; }
    public string EntryByIP { get; set; }
    public int UserTypeId { get; set; }
    public int maxaproval { get; set; }
    public int DocType { get; set; }
    public int recordId { get; set; }
    public int acnt { get; set; }
    public string Remarks { get; set; }
    public int OccCtrl { get; set; }
    public string SubjectName { get; set; }
    public string DistinctionSub { get; set; }
    public int MeritNo { get; set; }
    public string LaterReferenceNo { get; set; }

    public DateTime LaterReferenceDate { get; set; }
    public string PassingYear { get; set; }
    public string ExamMedium { get; set; }

    public DateTime DateResultDeclaration { get; set; }
    public string Rank { get; set; }
    public string AwardedBy { get; set; }
    public decimal AwardPrize { get; set; }
    public int CancelInd { get; set; }
    public string CancelReason { get; set; }
    public DateTime CancelDateTime { get; set; }
}