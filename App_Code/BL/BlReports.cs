using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class BlReports
{
    DlReports dlRpt = new DlReports();

    public BlReports()
    {
    }
    public DataTable GetReport(PlReports plRpt)
    {
        return dlRpt.GetReport(plRpt);
    }

    public DataTable GetRptFoilCFValidity(PlReports plRpt)
    {
        return dlRpt.GetRptFoilCFValidity(plRpt);
    }

    public DataTable GetPhysicalPath(PlReports plRpt)
    {
        return dlRpt.GetPhysicalPath(plRpt);
    }
}