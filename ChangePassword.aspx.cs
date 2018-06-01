using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
public partial class ChangePassword : System.Web.UI.Page
{
    BlChangePwd BlChngPwd = new BlChangePwd();
    PlChangePwd PlChngPwd = new PlChangePwd();
    plTrRegister objpltrRegister = new plTrRegister();
    DataSet Ds; SqlCommand cmd; SqlDataAdapter Da; DataTable dt, dtnull;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Convert.ToString(Session["st"]) == "1")//This for First Time login User.
        {
            divremark.Visible = true;//It is a msg for first time userlogin.
        }
        else {
            divremark.Visible = false;
        }
    }
    private String EncodePassword(String Pwd)
    {
        int Calc = 0; String FnlStr = "";
        for (int i = 1; i <= Pwd.Length; i++)
        {
            if (i < 2)
            {
                Calc = 100;
            }
            else if (i < 4)
            {
                Calc = 200;
            }
            else if (i < 6)
            {
                Calc = 300;
            }
            else if (i < 8)
            {
                Calc = 400;
            }
            else if (i < 10)
            {
                Calc = 500;
            }
            byte[] bt = Encoding.ASCII.GetBytes(Pwd.ToString().Substring(i - 1, 1));
            FnlStr = FnlStr + Convert.ToInt32(bt[0] + Calc + i).ToString();
        }
        return FnlStr;
    }
    public String DecodePassword(String Pwd)
    {
        int Calc = 0; String FnlStr = "";
        int i = 1; int j = 1; int A = 0; byte[] bt; String Str = ""; ;
        while (i <= Pwd.Length)
        {
            if (j < 2)
            {
                Calc = 100;
            }
            else if (j < 4)
            {
                Calc = 200;
            }
            else if (j < 6)
            {
                Calc = 300;
            }
            else if (j < 8)
            {
                Calc = 400;
            }
            else if (j < 10)
            {
                Calc = 500;
            }
            A = Convert.ToInt32(Pwd.ToString().Substring(i - 1, 3)) - Calc - j;
            bt = new byte[1];
            bt[0] = (byte)A;
            Str = Encoding.ASCII.GetString(bt);
            FnlStr = FnlStr + Str;
            i = i + 3;
            j++;
        }
        return FnlStr;
    }

    public HttpClient ApiCall()
    {
        HttpClient ht = new HttpClient();
        ht.BaseAddress = new Uri(DataAcces.Url);
        ht.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
        return ht;
    }

    protected void linkSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNewPwd.Text != txtOldPwd.Text)
            {
                if (Session["UserId"].ToString() != "" && Session["UserId"] != null)
                {
                    PlChngPwd.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    PlChngPwd.OldPwd = EncodePassword(txtOldPwd.Text);
                    PlChngPwd.NewPwd = EncodePassword(txtNewPwd.Text);
                    PlChngPwd.Ind = 4;
                    HttpClient ht = ApiCall();
                    var uri = string.Format("api/Login/SelectOldPwd/?Ind={0}&UserId={1}&OldPwd={2}", PlChngPwd.Ind, PlChngPwd.UserId, PlChngPwd.OldPwd);
                    var response = ht.GetAsync(uri).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsAsync<IEnumerable<PlChangePwd>>().Result;
                        foreach (var item in data)
                        {
                            if (item.cnt == 1)
                            {
                                //this check for user already used this password or not.
                                PlChngPwd.Ind = 6;
                                PlChngPwd.UserId = Convert.ToInt32(Session["UserId"]);
                                PlChngPwd.NewPwd = EncodePassword(txtNewPwd.Text);
                                HttpClient ht1 = ApiCall();
                                var uri1 = string.Format("api/Login/CheckPassword/?Ind={0}&UserId={1}&NewPwd={2}", PlChngPwd.Ind, PlChngPwd.UserId, PlChngPwd.NewPwd);
                                var response1 = ht1.GetAsync(uri1).Result;

                                if (response1.IsSuccessStatusCode)
                                {
                                    var data1 = response1.Content.ReadAsAsync<DataTable>().Result;
                                    //dt = CheckOldPwd();

                                    if (data1.Rows.Count > 0)
                                    {
                                        lblmsg.Text = "You Have Already Used Of This Password : Enter Other Password";
                                        return;
                                    }
                                    else
                                    {
                                        //this for change password successfully.
                                        PlChngPwd.Ind = 5;
                                        HttpClient ht2 = ApiCall();
                                        var uri2 = string.Format("api/Login/ChangePwd/?Ind={0}&UserId={1}&LoginPwd={2}&NewPwd={3}", PlChngPwd.Ind, PlChngPwd.UserId, PlChngPwd.OldPwd, PlChngPwd.NewPwd);
                                        var response2 = ht2.GetAsync(uri2).Result;
                                        if (response2.IsSuccessStatusCode)
                                        {
                                            //int j = BlChngPwd.ChangePwd(PlChngPwd);
                                            var data2 = response2.Content.ReadAsAsync<DataTable>().Result;
                                            if (data2.Rows.Count > 0)
                                            {
                                                //lblmsg.Text = "Password Change Succesfully";
                                                //this is for logout.
                                                HttpClient hc = new HttpClient();
                                                hc.BaseAddress = new Uri(DataAcces.Url);
                                                hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
                                                objpltrRegister.Ind = 3;
                                                objpltrRegister.UserId = Convert.ToInt32(Session["UserId"]);
                                                var v = string.Format("api/Login/UpdateActiveStatus/?Ind={0}&UserId={1}", objpltrRegister.Ind, objpltrRegister.UserId);

                                                var resp = hc.GetAsync(v).Result;
                                                if (resp.IsSuccessStatusCode)
                                                {
                                                    var get = resp.Content.ReadAsAsync<DataTable>().Result;
                                                    if (get.Rows.Count > 0)
                                                    {
                                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Password Change Successfully.');window.location='UserLogin.aspx';", true);
                                                        Session["USERNAME"] = null;
                                                        Session["UserId"] = null;
                                                        Session["UserType"] = null;
                                                        Session.Clear();
                                                        Session.Abandon();
                                                    }

                                                }

                                            }
                                            else                                            
                                                lblmsg.Text = "Password Not Change : Error";                                            
                                        }
                                    }
                                }
                            }
                            else                            
                                lblmsg.Text = "Wrong Old Password";                            
                        }
                    }
                }
                else                
                    Response.Redirect("UserLogin.aspx");                
            }
            else
                lblmsg.Text = "Old Password And New Password Should Not Be Same.";
        }
        catch
        { }
        
    }
   
    public void Clear()
    {
        txtOldPwd.Text = txtNewPwd.Text = txtConfirmPwd.Text = "";
        linkSave.Enabled = true;
    }

    protected void linkCancel_Click(object sender, EventArgs e)
    {
        Clear();
        lblmsg.Text = "";
    }
    protected void btnClose_Click(object sender, EventArgs e)
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
}
