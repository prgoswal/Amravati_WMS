using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class PlImgEntry
{
    public PlImgEntry()
    {
    }
    public int Ind { get; set; }
    public Int64 RegNo { get; set; }
    public Int64 RollNo { get; set; }
    public Int64 RollNoFrom { get; set; }
    public Int64 RollNoTo { get; set; }
    public string SName { get; set; }
    public string FilePath { get; set; }
    public int CompletedPages { get; set; }
    public int TotalPages { get; set; }
    public int ErrorMark { get; set; }
    public string IpAddress { get; set; }
    public int UserId { get; set; }
    public int RecId { get; set; }
    public Int64 EntryByRegNo { get; set; }
    public int AllotmentNo { get; set; }
    public DateTime CreationDate { get; set; }
    public int CurrentPageNo { get; set; }
    public int IsUpdated { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdationDate { get; set; }
}