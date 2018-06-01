using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PL_Print
/// </summary>
public class PL_Print
{
	public PL_Print()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Ind { get; set; }
    public int DocID { get; set; }
    public string DocType { get; set; }
    public int Cnt { get; set; }
   
      public int DocTypeId  { get; set; }
     public int LevelID { get; set; }
     public int UserID { get; set; }
}