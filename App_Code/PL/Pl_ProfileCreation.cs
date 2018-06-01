using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Pl_ProfileCreation
/// </summary>
public class Pl_ProfileCreation
{
	public Pl_ProfileCreation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Ind { get; set; }
    public int ItemId { get; set; }
    public int UserTypeId { get; set; }
    public DataTable dt { get; set; }
}