using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PL_DocumentApprovalLevel
/// </summary>
public class PL_DocumentApprovalLevel
{
	public PL_DocumentApprovalLevel()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int ParentMenuId { get; set; }
    public int LevelProfile { get; set; }
    public int AllowMenu { get; set; }
    public int Ind { get; set; }
}