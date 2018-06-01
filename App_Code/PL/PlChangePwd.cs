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
/// Summary description for PlChangePwd
/// </summary>
public class PlChangePwd
{
	public PlChangePwd()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Ind { get; set; }
    public string UserLoginId { get; set; }
    public string OldPwd { get; set; }
    public string NewPwd { get; set; }
    public int UserId { get; set; }
    public DateTime ChangePwdDate { get; set; }
    public int cnt { get; set; }
}
