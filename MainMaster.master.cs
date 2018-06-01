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

public partial class MainMaster : System.Web.UI.MasterPage
{
    BlTRRegiter BlTrReg = new BlTRRegiter();
    plTrRegister plTrReg = new plTrRegister();
    string Access;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd = null;
    DataTable dt = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    static int usertypeId = 0;
    static string usertype = "";
    plTrRegister objpltrRegister = new plTrRegister();
    PlUserCreation objplusercreation = new PlUserCreation();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.Header.DataBind();
        if (Session["tabId"] != null)
            hfvalue.Value = Session["tabId"].ToString();

        if (!IsPostBack)
        {
            if (Session["UserTypeId"] != null)
                usertypeId = Convert.ToInt32(Session["UserTypeId"]);

            if (Session["UserType"] != null)
                usertype = Convert.ToString(Session["UserType"]);

            A5_PendingApp.Visible = A1_Serching.Visible = A4_Authority.Visible = AEntry.Visible = A_Entry.Visible =
                BApproval.Visible = B_Approval.Visible = D_Reports.Visible = E_AppTrail.Visible = false; //C_Print.Visible = 

            HttpClient hclient = new HttpClient();
            hclient.BaseAddress = new Uri(DataAcces.Url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            objplusercreation.Ind = 10;
            objplusercreation.UserTypeID = usertypeId;
            var v = string.Format("api/UserCreation/GetMenuList/?Ind={0}&UserTypeID={1}", objplusercreation.Ind, objplusercreation.UserTypeID);

            var resp = hclient.GetAsync(v).Result;
            if (resp.IsSuccessStatusCode)
            {
                var get = resp.Content.ReadAsAsync<DataTable>().Result;
                if (get.Rows.Count > 0)
                {
                    foreach (DataRow item in get.Rows)
                    {
                        if (Convert.ToInt32(item["ItemID"]) == 61)//for Entry
                        {
                            A5_PendingApp.Visible = A1_Serching.Visible = AEntry.Visible = A_Entry.Visible = E_AppTrail.Visible = true;
                            A2_User.Visible = A3_Profile.Visible = false;
                        }
                        if (Convert.ToInt32(item["ItemID"]) == 62)//for Approval
                        {
                            BApproval.Visible = B_Approval.Visible = E_AppTrail.Visible = true;
                        }
                        if (Convert.ToInt32(item["ItemID"]) == 63)//for Print
                        {
                            E_AppTrail.Visible = true; //C_Print.Visible = 
                        }
                        if (Convert.ToInt32(item["ItemID"]) == 64)//for Report
                        {
                            D_Reports.Visible = E_AppTrail.Visible = true;
                        }
                    }
                }
                if (usertypeId == 51)
                {
                    A4_Authority.Visible = AEntry.Visible = A_Entry.Visible = B_Approval.Visible = D_Reports.Visible = E_AppTrail.Visible = true;

                }
            }
            if (Session["USERNAME"] != null)
            {
                lblUserName.Text = Session["USERNAME"].ToString() + " [" + Session["UserType"] + "] " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            }
            else
            {
                Response.Redirect("UserLogin.aspx");
                return;
            }

            // Must be Dynamic Entry from tbl profile creration.

            //AEntry.Visible = A_Entry.Visible = BApproval.Visible = B_Approval.Visible = C_Print.Visible = D_Reports.Visible = E_AppTrail.Visible = false;           

            //if (usertypeId == 51)//For Admin            
            //    AEntry.Visible = A_Entry.Visible = BApproval.Visible = B_Approval.Visible = C_Print.Visible = D_Reports.Visible = E_AppTrail.Visible = true;
            //else if ((usertypeId == 52) || (usertypeId == 53))//For Office Asst+Exam. Officer
            //{
            //    AEntry.Visible = A_Entry.Visible = E_AppTrail.Visible = true;
            //    A2_User.Visible = A3_Profile.Visible = false;   
            //}
            //else if (usertypeId >= 54)//For Approval Authorities
            //    BApproval.Visible = B_Approval.Visible = E_AppTrail.Visible = true;
            //USERNAME INFORMATION
        }

        //        usertypeId = Convert.ToInt32(Session["UserTypeId"]);

        //        usertype = Convert.ToString(Session["UserType"]);


        //        if (usertypeId == 51)
        //        {
        //            if (Convert.ToString(Session["st"]) == Convert.ToString("1"))
        //            {

        //                //sp001.Visible = false;
        //                menu_disable();
        //                A101.Visible = false;
        //                A1.Visible = false;
        //            //    A102.Visible = false;
        //                A103.Visible = false;
        //                A104.Visible = false;
        //              //  A102.Visible = false;
        //                A3.Visible = false;
        //                // A001.Visible = false;
        //                //  k100.Visible = false;
        //                //A002.Visible = false;
        //                Ahome.Visible = false;
        //                //A003.Visible = false;
        //            }
        //            else
        //            {

        //                menu_disable();
        //                A101.Visible = true;
        //                A1.Visible = true;
        //              //  A102.Visible = true;
        //                A103.Visible = true;
        //                A104.Visible = true;
        //                A105.Visible = true;
        //                // A2.Visible = true;
        //                A3.Visible = true;
        //                //  A9.Visible = true;
        //                // A99.Visible = true;  
        //            }
        //        }
        //        else
        //        {
        //            if (Convert.ToString(Session["st"]) == Convert.ToString("1"))
        //            {
        //                spentry.Visible = false;
        //                //lblentry.Visible = false;
        //                menu_disable();
        //                A101.Visible = false;
        //                A1.Visible = false;
        //               // A102.Visible = false;
        //                A103.Visible = false;
        //                A104.Visible = false;

        //                A3.Visible = false;
        //                //A001.Visible = false;

        //                Ahome.Visible = false;
        //                // A003.Visible = false;
        //            }
        //            else
        //            {
        //                menu_disable();

        //                if (usertypeId == 52 || usertypeId==53) //For Window Level Person
        //                {

        //                    A101.Visible = true;
        //                   // A102.Visible = true;
        //                }
        //               // else
        //                   // A102.Visible = true; //For Approval Level Person
        //            }
        //        }

        //        //ExamMaster.Visible = true;
        //        if (Session["USERNAME"] != null)
        //        {
        //            if (usertype == "1")
        //            {
        //                lblUserName.Text = Session["USERNAME"].ToString() + " [" + Session["UserType"] + "] " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        //                //if(FOREXAM==)
        //                //    FOREXAM.Visible=true;
        //            }
        //            else if (usertype == "REPORT")
        //            {
        //                lblUserName.Text = Session["USERNAME"].ToString() + " [" + Session["UserType"] + "] " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        //            }
        //            else
        //            {
        //                lblUserName.Text = Session["USERNAME"].ToString() + " [" + Session["UserType"] + "] " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        //            }
        //        }
        //        else
        //        {
        //           // Server.Transfer("ChangePassword.aspx");
        //            //plTrReg.Ind = 3;
        //            //plTrReg.UserId = Convert.ToInt16(Session["UserId"]);
        //            //int i = BlTrReg.ChangeLoginIdStatus(plTrReg);
        //            Response.Redirect("UserLogin.aspx");
        //        }
        //    }
        //}
        //catch
        //{ }


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
