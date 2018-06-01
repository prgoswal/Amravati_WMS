using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Collections;
using System.Net;

public partial class UserLockingUnlocking : System.Web.UI.Page
{
    DlUserCreation dlUserCreation = new DlUserCreation();
    PlUserCreation plUserCreation = new PlUserCreation();
    plTrRegister objpltrregister = new plTrRegister();
    DataTable dt;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    static int rowind = 0;
  static  int SelectedUserID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         
        }
        list();
    }
    public async void list()
    {
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        DataSet myDataSet = null;

        var response = HClient.GetAsync("api/Login/GetLockList/?Ind=" + 9).Result;
        if (response.IsSuccessStatusCode)
        {
            var productJsonString = await response.Content.ReadAsStringAsync();
            myDataSet = JsonConvert.DeserializeObject<DataSet>(productJsonString);
        }

        if (myDataSet.Tables.Count > 0) 
        {
            if (rblist.SelectedIndex == 0) //For Active User
                grdUserUnlocking.DataSource = myDataSet.Tables[1];
            else if (rblist.SelectedIndex == 1) //For InActive User            
                grdUserUnlocking.DataSource = myDataSet.Tables[3];
            else if (rblist.SelectedIndex == 2) //For Lock User            
                grdUserUnlocking.DataSource = myDataSet.Tables[2];
            else if (rblist.SelectedIndex == 3) //For Unlock User
                grdUserUnlocking.DataSource = myDataSet.Tables[0];
                
            grdUserUnlocking.DataBind();
        }


        //}
        //else
        //{
        //    var uri = string.Format("api/SecondLevel/GetappDetail/?ind={0}&levelid={1}", Ind, usertypeid);
        //    var response = HClient.GetAsync(uri).Result;
        //    var productJsonString = await response.Content.ReadAsStringAsync();
        //    myDataSet = JsonConvert.DeserializeObject<DataSet>(productJsonString);
        //}

        //grdPandingDetails.DataSource = myDataSet.Tables[0];
        //grdPandingDetails.DataBind();
        //grdCompleted.DataSource = myDataSet.Tables[1];
        //grdCompleted.DataBind();

        //if (myDataSet.Tables[0] != null)
        //{
        //    Session["PendingPopup"] = myDataSet.Tables[0];
        //}


    }


    //private DataTable GetLockedUsers()
    //{
    //    plUserCreation.Ind = 9;
    //    plUserCreation.UserId = Convert.ToInt32(Session["UserId"]);
    //    dt = dlUserCreation.GetLockedUsers(plUserCreation);
    //    if (dt.Rows.Count > 0)
    //    {
    //        grdUserUnlocking.Visible = true;
    //        grdUserUnlocking.DataSource = dt;
    //        grdUserUnlocking.DataBind();
    //    }
    //    else
    //    {
    //        grdUserUnlocking.Visible = false;
    //        grdUserUnlocking.DataSource = null;
    //        lblMsg.Text = "No User(s) To Unlock";
    //    }
    //    return dt;
    //}
    protected void grdUserUnlocking_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int i = Convert.ToInt32(grdUserUnlocking.DataKeys[e.RowIndex].Value);
        ////string LnkBtn = grdUserUnlocking.FindControl("lnkUpdte").ToString();
        //plUserCreation.Ind = 10;
        //plUserCreation.LockId = 0;
        //plUserCreation.UserId = i;
        //plUserCreation.InvalidAttempt = 0;
        //i = dlUserCreation.UpdateUnlockedUser(plUserCreation);
        //if (i > 0)
        //{
        //    lblMsg.Text = "User Unlocked Successfully";
        //}
        //else
        //    lblMsg.Text = "User Could Not Be Unlocked Successfully";
    }
    protected void grdUserUnlocking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnklock");
            if (rblist.SelectedIndex == 0)//For Active User
            {
                lblMsg.Text = "";
                lblaction.Text = "InActive";
                lnk.Text = "InActive";                
            }
            else if (rblist.SelectedIndex == 1)//For InActive User
            {
                lblMsg.Text = "";
                lblaction.Text = "Active";
                lnk.Text = "Active";
            }
            else if (rblist.SelectedIndex == 2)//For Lock User
            {
                lblMsg.Text = "";
                lblaction.Text = "Unlock";
                lnk.Text = "Unlock";             
            }
            else if (rblist.SelectedIndex == 3)//For Unlock User
            {                
                lblMsg.Text = "";
                lblaction.Text = "Lock";
                lnk.Text = "Lock";   
            }
        }

            //if (e.Row.Cells[3].Text == "Lock")
            //{
            //    lnk.Text = "Unlock";
            //}
            //else
            //{
            //    lnk.Text = "Lock";
            //}       
    }
    protected void grdUserUnlocking_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //int Userid = Convert.ToInt32(grdUserUnlocking.DataKeys[e.NewSelectedIndex].Value);
        //string Action = grdUserUnlocking.Rows[e.NewSelectedIndex].Cells[3].Text;
        //if (Action == "Lock")
        //{
        //    Action = "Unlock";
        //}
        //else
        //{
        //    Action = "Lock";
        //}
        //plUserCreation.Ind = 10;
        //plUserCreation.LockId = 0;
        //plUserCreation.UserId = Userid;
        //plUserCreation.InvalidAttempt = 0;
        //int i = dlUserCreation.UpdateUnlockedUser(plUserCreation);
        //if (i > 0)
        //{
        //    lblMsg.Text = "User Unlocked Successfully";
        //}
        //else 
        //{
        //    lblMsg.Text = "User Could Not Be Unlocked Successfully"; 
        //}
           
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (txtremarks.Text.Length >= 10)
        {
            HttpClient hclient = new HttpClient();
            hclient.BaseAddress = new Uri(DataAcces.Url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            objpltrregister.Ind = 10;

            //objpltrregister.UserId = Convert.ToInt32(Session["UserId"]);
            objpltrregister.UserId = SelectedUserID;
            
            if (rblist.SelectedIndex == 1)//For InActive User
                objpltrregister.UserInd = 4;
            else if (rblist.SelectedIndex == 2)//For Unlock User
                objpltrregister.UserInd = 3;
            else if (rblist.SelectedIndex == 3)//For Unlock User
                objpltrregister.UserInd = 1;
            else if (rblist.SelectedIndex == 0)//For Active User
                objpltrregister.UserInd = 2;

            var uri = string.Format("api/Login/UpdateLockUnlock/?Ind={0}&UserInd={1}&UserId={2}&Remark={3}", 10, objpltrregister.UserInd, objpltrregister.UserId, txtremarks.Text);
            var response = hclient.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var getdata = response.Content.ReadAsAsync<DataTable>().Result;
                if (getdata.Rows.Count > 0)
                {
                    lblMsg.Text = "User - " + lblusername.Text + " Has " + lblaction.Text.ToString()+".";
                    list();
                }
                else
                    lblMsg.Text = "User - " + lblusername.Text + " Has Not " + lblaction.Text.ToString() + ". Please Contact System Administrator.";

                getdata = null;
                txtremarks.Text = "";
            }
            response = null; uri = null;
        }
        else        
            lblremark.Text = "Please Enter Atleast 10 Characters.";
    }
    void PopupPrint(bool isDisplayPopupPrint)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplayPopupPrint)
        {
            builder.Append("<script language=JavaScript> ShowPopupPrint(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
        }
        else
        {
            builder.Append("<script language=JavaScript> HidePopupPrint(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
        }
    }
    protected void grdUserUnlocking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Lock")
        {
            txtremarks.Text = "";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow gvRow = grdUserUnlocking.Rows[rowIndex];           
            ////Access Cell values.
            lblusername.Text = gvRow.Cells[2].Text;           
            SelectedUserID = Convert.ToInt32(grdUserUnlocking.DataKeys[rowIndex]["UserId"]);            
            PopupPrint(true);
        }
    }
}