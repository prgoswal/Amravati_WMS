using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for BlStudentDetail
/// </summary>
public class BlStudentDetail
{
    DlStudentDetail dl = new DlStudentDetail();
	public BlStudentDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetExamYear(PlStudentDetail pl)
    {
        return dl.GetExamYear(pl);
    }
    public DataTable GetExamSession(PlStudentDetail pl)
    {
        return dl.GetExamSession(pl);
    }
    public DataTable GetExamName(PlStudentDetail pl)
    {
        return dl.GetExamName(pl);
    }

    public DataTable GetExamType(PlStudentDetail pl)
    {
        return dl.GetExamType(pl);
    }
    public DataTable GetForApply(PlStudentDetail pl)
    {
        return dl.GetForApply(pl);
    }
    public DataTable GetStudentDetail(PlStudentDetail pl)
    {
        return dl.GetStudentDetail(pl);
    }
    public DataTable SaveStudentDetail(PlStudentDetail pl)
    {
        return dl.SaveStudentDetail(pl);
    }
}
