using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Net.Http;
using System.Net.Http.Headers;

public partial class SubMaster : System.Web.UI.MasterPage
{
    BlTRRegiter BlTrReg = new BlTRRegiter();
    plTrRegister plTrReg = new plTrRegister();
    //string Access;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    //SqlCommand cmd = null;
    //DataTable dt = null;
    //DataSet ds = null;
    //SqlDataAdapter da = null;
   static int usertypeId = 0;
    static string usertype = "";
    plTrRegister objpltrRegister = new plTrRegister();
    PlUserCreation objplusercreation = new PlUserCreation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserTypeId"] != null)
            usertypeId = Convert.ToInt32(Session["UserTypeId"]);

        if (Session["UserType"] != null)
            usertype = Convert.ToString(Session["UserType"]);

        if (Session["USERNAME"] != null)
        {
            lblUserName.Text = Session["USERNAME"].ToString() + " [" + Session["UserType"] + "] " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        }
        else
        {
            Response.Redirect("UserLogin.aspx");
            return;
        }
    }
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        try
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(DataAcces.Url);
            //hc.BaseAddress = new Uri(DataAcces.Url);
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
            objpltrRegister.Ind = 3;
            objpltrRegister.UserId = Convert.ToInt32(Session["UserId"]);
            // var v = string.Format("api/Login/UpdateActiveStatus/");
            var v = string.Format("api/Login/UpdateActiveStatus/?Ind={0}&UserId={1}", objpltrRegister.Ind, objpltrRegister.UserId);

            var resp = hc.GetAsync(v).Result;
            if (resp.IsSuccessStatusCode)
            {
                var get = resp.Content.ReadAsAsync<IEnumerable<plTrRegister>>().Result;
                if (get.Count() > 0)
                {
                    Session["USERNAME"] = null;
                    Session["UserId"] = null;
                    Session["UserType"] = null;
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect("UserLogin.aspx");
                    Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "closeWin()", true);
                }

            }
        }
        catch
        { }
    }
}
