using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PlUpload
/// </summary>
public class PlUpload
{
    public int Ind { get; set; }
    public string  tableName { get; set; }
    public string FileName { get; set; }
    public string ExamSession { get; set; }
    public int ExamYear { get; set; }
    public ExcelWorksheet excel { get; set; }

}