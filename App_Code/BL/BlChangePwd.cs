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
/// Summary description for BlChangePwd
/// </summary>
public class BlChangePwd
{
    DlChangePwd dl = new DlChangePwd();
    public BlChangePwd()
    {
    }
    public int ChangePwd(PlChangePwd pl)
    {
        return dl.ChangePwd(pl);
    }
    public int SelectOldPwd(PlChangePwd pl)
    {
        return dl.SelectOldPwd(pl);
    }
    public DataTable CheckPassword(PlChangePwd pl)
    {
        return dl.CheckPassword(pl);
    }
    public DataTable InsertPwd(PlChangePwd pl)
    {
        return dl.InsertPwd(pl);
    }
}
