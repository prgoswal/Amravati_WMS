using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


public class BlExamMaster
{
    DataTable dt;
    PlExamMaster pl = new PlExamMaster();
    DlExamMaster dl = new DlExamMaster();
    public BlExamMaster()
    {
    }
    public int InsertExamMaster(PlExamMaster pl)
    {
        int i = dl.InsertExamMaster(pl);
        return i;
    }
    public DataTable GetLastExamMaster(PlExamMaster pl)
    {
        return dt = dl.GetLastExamMaster(pl);
    }
}