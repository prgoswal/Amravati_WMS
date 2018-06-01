using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class PlRegister
{
    public PlRegister()
    {

    }
    public int Ind { get; set; }
    public int Faculty { get; set; }
    public Int64 Ctrl { get; set; }
    public int ExamYear { get; set; }
    public int Session { get; set; }
    public int ExamType { get; set; }
    public int FoilCuonterfoil { get; set; }
    public int NoOfBook { get; set; }
    public int CureentBookNo { get; set; }
    public int PageNoFrom { get; set; }
    public int PageNoTo { get; set; }
    public int TotalPage { get; set; }
    public DateTime RegCreationDate { get; set; }
    public Int64 RegNo { get; set; }
    public int UserID { get; set; }
    public int PageNo { get; set; }
    public string FilePath { get; set; }

    public string CancelInd { get; set; }
    public DateTime CancelDate { get; set; }

    public string CancelReason { get; set; }

    public string IpAddress { get; set; }
    public string CancelledIpAddress { get; set; }
    public int RegisterPageNo { get; set; }
    public string SName { get; set; }
    public Int64 RollNo { get; set; }
   
    public DateTime LoginDatetime { get; set; }
    public DateTime CreationDate { get; set; }

    public int PartNo { get; set; }

}