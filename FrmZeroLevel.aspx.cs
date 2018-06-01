using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Collections;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public partial class FrmSecondLevel : System.Web.UI.Page
{
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;
    DataRow dr;
    int j = 0;
    static int ApprovalCount; static Int64 ApplicatioID; static int DocTypeID; static string IpAddress;
    static int acnt;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    PL_SecondLevel objPlSecondLevel = new PL_SecondLevel();
    int lngRecordId = 0;
    static int lngapplicationId = 0;
    static int Ind = 0;
    static int usertypeid = 0;
    static string UserId = "";

    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!IsPostBack)
        {
            lblmsg.Text = lblCancelMSG.Text = lblCancelSuccessMSG.Text = "";

            hiddenfield.Value = DataAcces.hiddenURL;//this for duplicate marksheet url.
            if (Session["UserId"] != null)
                UserId = Convert.ToString(Session["UserId"]);

            if (Session["UserTypeId"] != null)
                usertypeid = Convert.ToInt32(Session["UserTypeId"]);

            lblPendingsince.Text = string.Empty;
            GetAppDetails();//Calling funtion for load data according to users.
            GetCancelApplicationList();

            lblcompletecount.Text = Convert.ToString(grdCompleted.Rows.Count);
            lblpendingcount.Text = Convert.ToString(grdPandingDetails.Rows.Count);
            //  Session["lblpendingcount"] = Convert.ToString(grdPandingDetails.Rows.Count);
        }
    }

    public async void GetAppDetails()
    {

        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        DataSet myDataSet = null;
        if (usertypeid > 53)        //this for all Other users Other than admin and data entry users.
        {
            objPlSecondLevel.Ind = 4;
            objPlSecondLevel.UserTypeId = Convert.ToString(usertypeid);

            var uri1 = string.Format("api/SecondLevel/GetappDetail/?ind={0}&levelid={1}", objPlSecondLevel.Ind, objPlSecondLevel.UserTypeId);
            var response1 = HClient.GetAsync(uri1).Result;
            var productJsonString = await response1.Content.ReadAsStringAsync();
            myDataSet = JsonConvert.DeserializeObject<DataSet>(productJsonString);

            if (myDataSet.Tables.Count > 0)
            {
                if (myDataSet.Tables[0].Rows.Count > 0)
                {
                    lblPendingsince.Text = myDataSet.Tables[0].Rows[0]["PendingSince"].ToString() + " Days";
                    grdPandingDetails.DataSource = myDataSet.Tables[0];
                    grdPandingDetails.DataBind();
                }
                else
                    lblPendingsince.Text = "0 Days";

                if (myDataSet.Tables[1].Rows.Count > 0)
                {
                    grdCompleted.DataSource = myDataSet.Tables[1];
                    grdCompleted.DataBind();
                }
            }
            productJsonString = null; response1 = null; uri1 = null;
        }

        else if (usertypeid == 51 || usertypeid == 52 || usertypeid == 53)      //this for only data entry users.
        {
            objPlSecondLevel.Ind = 3;
            if (usertypeid == 51)       //here i send userid = 0 for admin pendinglist and userid (Another) for all other users              
                objPlSecondLevel.UserID = "0";
            else
                objPlSecondLevel.UserID = UserId;

            var uri = string.Format("api/SecondLevel/GetAppDetailZeroLevel/?Ind={0}&UserID={1}", objPlSecondLevel.Ind, objPlSecondLevel.UserID);
            var response = HClient.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var productJsonString = await response.Content.ReadAsStringAsync();
                myDataSet = JsonConvert.DeserializeObject<DataSet>(productJsonString);
                productJsonString = null;
            }
            if (myDataSet.Tables.Count > 0)
            {
                if (myDataSet.Tables[0].Rows.Count > 0)
                    lblPendingsince.Text = myDataSet.Tables[0].Rows[0]["PendingSince"].ToString() + " Days";

                grdPandingDetails.DataSource = myDataSet.Tables[0];
                grdPandingDetails.DataBind();

                grdCompleted.DataSource = myDataSet.Tables[1];
                grdCompleted.DataBind();
            }
            response = null; uri = null;
        }
        if (myDataSet != null && myDataSet.Tables.Count > 0)
        {
            if (myDataSet.Tables[0] != null)
                Session["PendingPopup"] = myDataSet.Tables[0];
        }

        myDataSet = null;
    }

    protected void grdPandingDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblCancelSuccessMSG.Text = "";
            if (e.CommandName == "ShowPopup")
            {
                cancelApplicationModal.Style.Add("display", "none");

                j = Convert.ToInt32(Session["UserTypeId"]);
                if (j == 51 || j == 53 || j == 52)
                {
                    pnlform.Visible = true;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    btnCorrection.Visible = false;
                    lnkSubmit.Visible = true;
                }
                else
                {
                    pnlform.Visible = true;
                    pnlform.Enabled = false;
                    lnkSubmit.Visible = false;
                }

                LinkButton btndetails = (LinkButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                dt = new DataTable();
                dt = (DataTable)Session["PendingPopup"];
                int rwindex = gvrow.RowIndex;
                Session["gvrowIndex"] = rwindex;

                if (dt.Rows.Count > 0)
                {
                    //if (dt.Rows[rwindex]["DocTypeID"].ToString() == "7")
                    //    Response.Redirect("FrmMarksheetEntry.aspx");
                    ////ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:windowOpen(); ", true);
                    //else
                    //{

                    HideFoil.ImageUrl = dt.Rows[rwindex]["PartAImagePath"].ToString();
                    HideCF.ImageUrl = dt.Rows[rwindex]["PartBImagePath"].ToString();
                    ImageFoil.ImageUrl = dt.Rows[rwindex]["PartAImagePath"].ToString();
                    ImageCF.ImageUrl = dt.Rows[rwindex]["PartBImagePath"].ToString();
                    ImageCF.Width = 400;
                    txtRollNo.Text = dt.Rows[rwindex]["RollNo"].ToString();
                    txtStudentName.Text = dt.Rows[rwindex]["StudentName"].ToString();
                    ApplicatioID = Convert.ToInt64(dt.Rows[rwindex]["ApplicationID"].ToString());
                    lblappno.Text = Convert.ToString(ApplicatioID);
                    lngapplicationId = Convert.ToInt32(dt.Rows[rwindex]["ApplicationID"].ToString());
                    DocTypeID = Convert.ToInt32(dt.Rows[rwindex]["DocTypeID"].ToString());

                    if (usertypeid <= 53)
                        ApprovalCount = Convert.ToInt32(dt.Rows[rwindex]["ApprovalCount"].ToString());
                    else
                        ApprovalCount = 0;

                    lblEntry.Text = "Applied For : " + dt.Rows[rwindex]["AppliedFor"].ToString();

                    if ((dt.Rows[rwindex]["ExamSession"].ToString() == "SUMMER") || (dt.Rows[rwindex]["ExamSession"].ToString() == "2"))
                        txtExamSession.Text = "SUMMER";
                    else if ((dt.Rows[rwindex]["ExamSession"].ToString() == "WINTER") || (dt.Rows[rwindex]["ExamSession"].ToString() == "1"))
                        txtExamSession.Text = "WINTER";

                    txtExamYear.Text = dt.Rows[rwindex]["ExamYear"].ToString();
                    lnkSubmit.Visible = true;
                    DivBlink.Visible = true;
                    VisibilityForApply(DocTypeID);

                    ////For Data Fetching From University Data Table
                    objPlSecondLevel.Ind = 11;
                    objPlSecondLevel.Rollno = dt.Rows[rwindex]["RollNo"].ToString();
                    objPlSecondLevel.ExamYear = dt.Rows[rwindex]["ExamYear"].ToString();

                    if ((dt.Rows[rwindex]["ExamSession"].ToString() == "SUMMER") || (dt.Rows[rwindex]["ExamSession"].ToString() == "2"))
                        objPlSecondLevel.ExamSession = "2";
                    else if ((dt.Rows[rwindex]["ExamSession"].ToString() == "WINTER") || (dt.Rows[rwindex]["ExamSession"].ToString() == "1"))
                        objPlSecondLevel.ExamSession = "1";

                    if (usertypeid <= 53)
                        objPlSecondLevel.OccCtrl = Convert.ToInt32(dt.Rows[rwindex]["Ctrl"].ToString());
                    else
                        objPlSecondLevel.OccCtrl = 0;

                    DataTable DtUnivData = new DataTable();
                    DtUnivData = ShowExamDetail(objPlSecondLevel);

                    if (DtUnivData.Rows.Count > 0)
                    {
                        DivBlink.Visible = false;

                        txtExamName.Enabled = true; txtExamName.Text = DtUnivData.Rows[0]["ExamName"].ToString().Trim();
                        txtExamSession.Enabled = false; txtExamSession.Text = DtUnivData.Rows[0]["ExamSession"].ToString();
                        txtExamYear.Enabled = false; txtExamYear.Text = DtUnivData.Rows[0]["ExamYear"].ToString();
                        txtRollNo.Enabled = false; txtRollNo.Text = DtUnivData.Rows[0]["RollNo"].ToString();

                        txtStudentName.Enabled = true; txtStudentName.Text = DtUnivData.Rows[0]["StudentName"].ToString();
                        txtEnrollmentlNo.Text = DtUnivData.Rows[0]["EnrollmentNo"].ToString();
                        txtExamMedium.Text = DtUnivData.Rows[0]["Medium"].ToString();
                        txtCollege.Text = DtUnivData.Rows[0]["CollegeCode"].ToString();
                        txtDivision.Text = DtUnivData.Rows[0]["CurrentDivision"].ToString();

                        if (DocTypeID == 6)
                        {
                            txtSubjectName.Text = "";
                        }
                        else
                            txtSubjectName.Text = DtUnivData.Rows[0]["PassingSub"].ToString();
                    }
                    viewApplicationModal.Style.Add("display", "block");
                    //Popup(true);
                    txtBranchName.Focus();
                    // }
                }
                else
                    lblmsg.Text = "Record Not Found.";
            }
            if (e.CommandName == "ShowCancelPopup")
            {
                j = Convert.ToInt32(Session["UserTypeId"]);
                if (j == 51 || j == 53 || j == 52)
                {
                    pnlform.Visible = true;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    btnCorrection.Visible = false;
                    lnkSubmit.Visible = true;
                }
                else
                {
                    pnlform.Visible = true;
                    pnlform.Enabled = false;
                    lnkSubmit.Visible = false;
                }

                LinkButton btndetails = (LinkButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                dt = new DataTable();
                dt = (DataTable)Session["PendingPopup"];
                int rwindex = gvrow.RowIndex;
                Session["gvrowIndex"] = rwindex;

                if (dt.Rows.Count > 0)
                {
                    cancelApplicationModal.Style.Add("display", "block");

                    lblApplicationID.Text = dt.Rows[rwindex]["ApplicationID"].ToString();
                    lblApplicationDate.Text = dt.Rows[rwindex]["ApplicationDate"].ToString();
                    lblRollNo.Text = dt.Rows[rwindex]["RollNo"].ToString();
                    lblStudentName.Text = dt.Rows[rwindex]["StudentName"].ToString();
                    lblAppliedFor.Text = dt.Rows[rwindex]["AppliedFor"].ToString();
                    txtCancelReason.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            lblmsg.Visible = true;
        }
    }

    void Popup(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            builder.Append("<script language=JavaScript> ShowPopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
        }
        else
        {
            builder.Append("<script language=JavaScript> HidePopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
        }
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        lblCancelMSG.Text = lblCancelSuccessMSG.Text = "";
        if (string.IsNullOrEmpty(txtCancelReason.Text))
        {
            cancelApplicationModal.Style.Add("display", "block");
            lblCancelMSG.Text = "Please Enter Cancel Reason.";
            txtCancelReason.Text = "";
            txtCancelReason.Focus();
        }
        else
        {
            if (txtCancelReason.Text.Count() < 10)
            {
                cancelApplicationModal.Style.Add("display", "block");
                lblCancelMSG.Text = "Please Enter Minimum 10 Char.";
                //txtCancelReason.Text = "";
                txtCancelReason.Focus();
            }
            else
            {
                HttpClient HClient = new HttpClient();
                HClient.BaseAddress = new Uri(DataAcces.Url);
                HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Pl_SubmitZeroLevel pl = new Pl_SubmitZeroLevel();
                pl.Ind = 51;
                pl.ApplicationID = Convert.ToInt32(lblApplicationID.Text);
                pl.UserID = Convert.ToInt32(Session["UserId"]);
                pl.CancelInd = 1;
                pl.CancelReason = txtCancelReason.Text;
                pl.CancelDateTime = DateTime.Now;

                var uri = "api/SecondLevel/PostCancelPopDetail";
                var response = HClient.PostAsJsonAsync(uri, pl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var getdata = response.Content.ReadAsAsync<DataTable>().Result;
                    if (getdata.Rows.Count > 0)
                    {
                        if (getdata.Rows[0]["ReturnInd"].ToString() == "1")
                        {
                            lblCancelSuccessMSG.Text = "Record Cancel Successfully.";
                            cancelApplicationModal.Style.Add("display", "none");
                            lblCancelSuccessMSG.Style.Add("color", "#27c24c");
                            GetAppDetails();
                        }
                        else
                        {
                            lblCancelMSG.Text = "Record Not Cancelled Successfully.";
                            cancelApplicationModal.Style.Add("display", "block");
                        }
                    }
                }
                txtCancelReason.Text = "";
            }
        }
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        cancelApplicationModal.Style.Add("display", "none");
        txtCancelReason.Text = lblCancelMSG.Text = lblCancelSuccessMSG.Text = ""; 
    }

    protected void grdCompleted_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowStatus")
        {
            cancelApplicationModal.Style.Add("display", "none");

            Response.Redirect("FrmApplicationProgress.aspx?CallID=1&ApplicationID=" + e.CommandArgument);
        }
    }

    public static string GetIpAddress()  // Get IP Address
    {
        string ip = "";
        IPHostEntry ipEntry = Dns.GetHostEntry(GetCompCode());
        IPAddress[] addr = ipEntry.AddressList;
        ip = addr[1].ToString();
        return ip;
    }

    public static string GetCompCode()  // Get Computer Name
    {
        string strHostName = "";
        strHostName = Dns.GetHostName();
        return strHostName;
    }

    public int GetRecordId()
    {
        HttpClient hclient = new HttpClient();
        hclient.BaseAddress = new Uri(DataAcces.Url);
        hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objPlSecondLevel = new PL_SecondLevel();

        var uri = string.Format("api/SecondLevel/GetRecordId/?Ind={0}&ApplicationId={1}", 3, lngapplicationId);
        var response = hclient.GetAsync(uri).Result;
        var getdata = response.Content.ReadAsAsync<IEnumerable<PL_SecondLevel>>().Result;
        foreach (var a in getdata)
        {
            lngRecordId = a.recordId;
        }
        return lngRecordId;
    }

    public int GetApprovalCount()
    {
        HttpClient hclient = new HttpClient();
        hclient.BaseAddress = new Uri(DataAcces.Url);
        hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objPlSecondLevel = new PL_SecondLevel();
        var uri = string.Format("api/SecondLevel/GetApprovalCount/?ind={0}&applicationid={1}&isconfirm={2}&doctype={3}", 4, lngapplicationId, 1, DocTypeID);
        var response = hclient.GetAsync(uri).Result;
        var getdata = response.Content.ReadAsAsync<IEnumerable<PL_SecondLevel>>().Result;
        if (getdata.Count() > 0)
        {
            foreach (var a in getdata)
            {
                acnt = Convert.ToInt32(a.acnt);
            }
        }
        else
            acnt = 0;
        getdata = null; response = null; uri = null;
        return acnt;
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        lblCancelSuccessMSG.Text = "";
        lngRecordId = GetRecordId();
        acnt = GetApprovalCount();
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Pl_SubmitZeroLevel pl = new Pl_SubmitZeroLevel();

        pl.Ind = 2;
        pl.ApplicationID = Convert.ToInt32(ApplicatioID);
        pl.UserID = Convert.ToInt32(Session["UserId"]);
        pl.EntryByIP = (IpAddress = GetIpAddress());
        pl.UserTypeId = Convert.ToInt32(Session["UserTypeId"]);
        pl.recordId = lngRecordId;
        pl.acnt = acnt;
        pl.DocType = DocTypeID;
        pl.Remarks = txtRemark.Text;
        var uri = "api/SecondLevel/PostApprovalRecord";
        var response = HClient.PostAsJsonAsync(uri, pl).Result;
        if (response.IsSuccessStatusCode)
        {
            var getdata = response.Content.ReadAsAsync<IEnumerable<PL_SecondLevel>>().Result;
            if (getdata != null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Data Submitted.');window.location='FrmZeroLevel.aspx';", true);
            }
            getdata = null;
        }
        response = null; uri = null;
    }

    public string zeroLevelCheck()
    {
        msgVal.Text = "";
        if (DocTypeID == 1)
        {

            if (txtBranchName.Text == "")
            {
                msgVal.Text = "Please Insert BranchName.";
            }
            else
                if (txtDivision.Text == "")
                {
                    msgVal.Text = "Please Insert Division.";
                }
                else
                    if (txtCGPA.Text == "")
                    {
                        msgVal.Text = "Please Insert CGPA";
                    }
                    else
                    {
                        msgVal.Text = "";
                    }
        }
        else if (DocTypeID == 2)
        {
            if (txtBranchName.Text == "")
            {
                msgVal.Text = "Please Insert BranchName.";
            }
            else if (txtDivision.Text == "")
            {
                msgVal.Text = "Please Insert Division.";
            }
            else if (txtCGPA.Text == "")
            {
                msgVal.Text = "Please Insert CGPA";
            }
            else if (txtEnrollmentlNo.Text == "")
            {
                msgVal.Text = "Please Insert Enrollment No";
            }
            else if (txtCollege.Text == "")
            {
                msgVal.Text = "Please Insert College";
            }
            else
            {
                msgVal.Text = "";
            }

        }
        else if (DocTypeID == 3)
        {
            if (txtBranchName.Text == "")
            {
                msgVal.Text = "Please Insert BranchName.";
            }
            else if (txtEnrollmentlNo.Text == "")
            {
                msgVal.Text = "Please Insert Enrollment No";
            }
            else if (txtSubjectName.Text == "")
            {
                msgVal.Text = "Please Insert Subject Name";
            }
            else if (txtDistinctionSub.Text == "")
            {
                msgVal.Text = "Please Insert Distinction Subject";
            }
            else
            {
                msgVal.Text = "";
            }

        }
        else if (DocTypeID == 4)
        {
            if (txtBranchName.Text == "")
            {
                msgVal.Text = "Please Insert BranchName.";
            }
            else if (txtMeritNo.Text == "")
            {
                msgVal.Text = "Please Insert Merit No.";
            }
            else
            {
                msgVal.Text = "";
            }
        }
        else if (DocTypeID == 6)
        {
            if (txtBranchName.Text == "")
            {
                msgVal.Text = "Please Insert BranchName.";
            }
            else if (txtDivision.Text == "")
            {
                msgVal.Text = "Please Insert Division.";
            }
            else if (txtMeritNo.Text == "")
            {
                msgVal.Text = "Please Insert Merit No.";
            }
            else if (txtEnrollmentlNo.Text == "")
            {
                msgVal.Text = "Please Insert Enrollment No.";
            }
            else if (ddlRank.SelectedIndex == 0)
            {
                msgVal.Text = "Please Select Rank";
            }
            else if (txtSubjectName.Text == "")
            {
                msgVal.Text = "Please Enter Subject";
            }
            else if (ddlAwardedby.SelectedIndex == 0)
            {
                msgVal.Text = "Please Select Awarded By";
            }
            else if (txtAwardPrize.Text == "")
            {
                msgVal.Text = "Please Enter Award Prize";
            }
            else
            {
                msgVal.Text = "";
            }
        }
        else if (DocTypeID == 8)
        {
            if (txtBranchName.Text == "")
            {
                msgVal.Text = "Please Insert BranchName.";
            }
            else if (txtDivision.Text == "")
            {
                msgVal.Text = "Please Insert Division.";
            }
            else if (txtLaterReferenceNo.Text == "")
            {
                msgVal.Text = "Please Insert Later Reference No.";
            }
            else if (txtLaterReferenceDate.Text == "")
            {
                msgVal.Text = "Please Insert Later Reference Date.";
            }
            else
            {
                msgVal.Text = "";
            }

        }
        else if (DocTypeID == 9)
        {
            if (txtBranchName.Text == "")
            {
                msgVal.Text = "Please Insert BranchName.";
            }
            else if (txtDivision.Text == "")
            {
                msgVal.Text = "Please Insert Division.";
            }
            else if (txtPassingYear.Text == "")
            {
                msgVal.Text = "Please Insert PassingYear.";
            }
            else
            {
                msgVal.Text = "";
            }
        }
        else if (DocTypeID == 10)
        {
            if (txtBranchName.Text == "")
            {
                msgVal.Text = "Please Insert BranchName.";
            }
            else if (txtDivision.Text == "")
            {
                msgVal.Text = "Please Insert Division.";
            }
            else if (txtPassingYear.Text == "")
            {
                msgVal.Text = "Please Insert PassingYear.";
            }
            else
            {
                msgVal.Text = "";
            }
        }
        else if (DocTypeID == 11)
        {
            if (txtBranchName.Text == "")
            {
                msgVal.Text = "Please Insert BranchName.";
            }
            else if (txtExamMedium.Text == "")
            {
                msgVal.Text = "Please Insert ExamMedium.";

            }
            else
            {
                msgVal.Text = "";
            }
        }
        return msgVal.Text;
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string progress = "";
        lblCancelSuccessMSG.Text = "";
        //progress = zeroLevelCheck();
        if (txtExamName.Text != "")
        {
            if (progress == "")
            {
                HttpClient HClient = new HttpClient();
                HClient.BaseAddress = new Uri(DataAcces.Url);
                HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Pl_SubmitZeroLevel pl = new Pl_SubmitZeroLevel();
                pl.Ind = 1;
                pl.ApplicationID = Convert.ToInt32(ApplicatioID);
                pl.Rollno = txtRollNo.Text;
                pl.SName = txtStudentName.Text;
                pl.ExamName = txtExamName.Text;
                pl.BranchName = txtBranchName.Text;
                pl.ExamSession = Convert.ToString(0);
                pl.EnrollmentNo = txtEnrollmentlNo.Text == "" ? "0" : txtEnrollmentlNo.Text;
                pl.College = txtCollege.Text;
                pl.CGPA = txtCGPA.Text == "" ? "0" : txtCGPA.Text;
                pl.Division = txtDivision.Text;
                pl.UserID = Convert.ToInt32(Session["UserId"]);
                pl.EntryByIP = (IpAddress = GetIpAddress());
                pl.UserTypeId = Convert.ToInt32(Session["UserTypeId"]);
                pl.maxaproval = ApprovalCount;
                pl.DocType = DocTypeID;
                pl.SubjectName = txtSubjectName.Text;
                pl.DistinctionSub = txtDistinctionSub.Text;
                pl.Remarks = txtRemark.Text;
                pl.Rank = ddlRank.SelectedItem.Text;
                pl.AwardedBy = ddlAwardedby.Text;

                if (ddlAwardedby.SelectedIndex == 3)
                    pl.AwardPrize = Convert.ToDecimal(txtAwardPrize.Text);
                else
                    pl.AwardPrize = 0;

                if (txtMeritNo.Text != string.Empty)
                    pl.MeritNo = Convert.ToInt32(txtMeritNo.Text);
                else
                    pl.MeritNo = 0;
                pl.LaterReferenceNo = txtLaterReferenceNo.Text;

                if (DocTypeID == 10)
                {
                    pl.LaterReferenceDate = Convert.ToDateTime(txtDateResultDeclaration.Text);
                }
                else if (DocTypeID == 8 || DocTypeID == 9)
                    pl.LaterReferenceDate = Convert.ToDateTime(txtLaterReferenceDate.Text);
                else
                    pl.LaterReferenceDate = DateTime.Now.Date;

                // pl.DateResultDeclaration = Convert.ToDateTime(txtDateResultDeclaration.Text);

                pl.PassingYear = txtPassingYear.Text;
                pl.ExamMedium = txtExamMedium.Text;

                var uri = "api/SecondLevel/PostSubmitPopDetail/";
                var response = HClient.PostAsJsonAsync(uri, pl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var getdata = response.Content.ReadAsAsync<DataTable>().Result;
                    if (getdata.Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Data Saved.');window.location='FrmZeroLevel.aspx';", true);
                    }
                    getdata = null;
                }
                response = null; uri = null;
            }
            else
            {
                msgVal.Text = progress;
                viewApplicationModal.Style.Add("display", "block");
                //Popup(true);
                return;
            }
        }
        else
        {
            msgVal.Text = "Please Enter Exam Name";
            viewApplicationModal.Style.Add("display", "block");
            //Popup(true);
            return;
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        lblCancelSuccessMSG.Text = "";
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objPlSecondLevel = new PL_SecondLevel();
        objPlSecondLevel.Ind = 1;
        objPlSecondLevel.ApplicationID = Convert.ToInt32(ApplicatioID);
        objPlSecondLevel.Rollno = txtRollNo.Text;
        objPlSecondLevel.SName = txtStudentName.Text;
        objPlSecondLevel.ExamName = txtExamName.Text;
        objPlSecondLevel.BranchName = txtBranchName.Text;
        objPlSecondLevel.ExamSession = Convert.ToString(0);
        objPlSecondLevel.EnrollmentNo = txtEnrollmentlNo.Text;
        objPlSecondLevel.College = txtCollege.Text;
        objPlSecondLevel.CGPA = txtCGPA.Text;
        objPlSecondLevel.Division = txtDivision.Text;
        objPlSecondLevel.UserID = Session["UserId"].ToString();
        objPlSecondLevel.EntryByIP = (IpAddress = GetIpAddress());
        var uri = "api/SecondLevel/PostRejectionRecord";
        var response = HClient.PostAsJsonAsync(uri, objPlSecondLevel).Result;

        if (response.IsSuccessStatusCode)
        {
            var getdata = response.Content.ReadAsAsync<IEnumerable<PL_SecondLevel>>().Result;
            if (getdata != null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Rejection Successfully');window.location='FrmZeroLevel.aspx';", true);
                getdata = null;
            }
            response = null; uri = null;
        }
    }

    protected void btnCorrection_Click(object sender, EventArgs e)
    {
        lblCancelSuccessMSG.Text = "";
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objPlSecondLevel = new PL_SecondLevel();
        objPlSecondLevel.Ind = 1;
        objPlSecondLevel.ApplicationID = Convert.ToInt32(ApplicatioID);
        objPlSecondLevel.Rollno = txtRollNo.Text;
        objPlSecondLevel.SName = txtStudentName.Text;
        objPlSecondLevel.ExamName = txtExamName.Text;
        objPlSecondLevel.BranchName = txtBranchName.Text;
        objPlSecondLevel.ExamSession = Convert.ToString(0);
        objPlSecondLevel.EnrollmentNo = txtEnrollmentlNo.Text;
        objPlSecondLevel.College = txtCollege.Text;
        objPlSecondLevel.CGPA = txtCGPA.Text;
        objPlSecondLevel.Division = txtDivision.Text;
        objPlSecondLevel.UserID = Session["UserId"].ToString();
        objPlSecondLevel.EntryByIP = (IpAddress = GetIpAddress());
        var uri = "api/SecondLevel/PostCorrectionRecord";
        var response = HClient.PostAsJsonAsync(uri, objPlSecondLevel).Result;
        if (response.IsSuccessStatusCode)
        {
            var getdata = response.Content.ReadAsAsync<IEnumerable<PL_SecondLevel>>().Result;
            if (getdata != null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Data Send to Correction.');window.location='FrmZeroLevel.aspx';", true);
            }
            getdata = null;
        }
        response = null; uri = null;
    }

    public DataTable ShowExamDetail(PL_SecondLevel pl)
    {
        HttpClient hclint = new HttpClient();
        hclint.BaseAddress = new Uri(DataAcces.Url);
        hclint.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/Json"));
        dt = new DataTable();
        var uri = string.Format("api/SecondLevel/ShowExamDetail?Ind={0}&Rollno={1}&ExamYear={2}&OccCtrl={3}&ExamSession={4}", pl.Ind, pl.Rollno, pl.ExamYear, pl.OccCtrl, pl.ExamSession);
        var response = hclint.PostAsJsonAsync(uri, objPlSecondLevel).Result;
        if (response.IsSuccessStatusCode)
        {
            dt = response.Content.ReadAsAsync<DataTable>().Result;
        }
        return dt;
    }

    public void VisibilityForApply(int DocTypeID)
    {
        msgVal.Text = "";
        trDivision.Visible = trCgpa.Visible = trEnroll.Visible = trCollege.Visible =
        trSubjectName.Visible = trDistinctionSub.Visible = trMeritNo.Visible =
        TrawardPrice.Visible = trAwardedBy.Visible = trRank.Visible = trResulDeclarationDate.Visible = trLaterReferenceNo.Visible = trLaterReferenceDate.Visible = trPassingYear.Visible = trExamMedium.Visible = false;

        if (DocTypeID == 1) //For Degree Certification Document Purpose
        {
            trDivision.Visible = true;
            trCgpa.Visible = true;
            txtDivision.Text = "";
        }
        else if (DocTypeID == 2) //For Provisional Degree Document Purpose
        {
            trEnroll.Visible = true;
            trDivision.Visible = true;
            trCgpa.Visible = true;
            trCollege.Visible = true;
            txtDivision.Text = "";
            txtEnrollmentlNo.Text = "";
            txtCollege.Text = "";
        }
        else if (DocTypeID == 3) //For Passing Certificate Document Purpose
        {
            trEnroll.Visible = true;
            trSubjectName.Visible = true;
            trDistinctionSub.Visible = true;
            txtEnrollmentlNo.Text = "";
            txtSubjectName.Text = "";
            txtDistinctionSub.Text = "";
        }
        else if (DocTypeID == 4) //For Merit Certificate Document Purpose
        {
            trMeritNo.Visible = true;
            txtMeritNo.Text = "";
        }
        else if (DocTypeID == 6) //For Medal Certificate Document Purpose
        {
            trAwardedBy.Visible = true;
            trDivision.Visible = true;
            trMeritNo.Visible = true;
            trEnroll.Visible = true;
            trRank.Visible = true;
            trSubjectName.Visible = true;
            txtDivision.Text = "";
            txtMeritNo.Text = "";
            txtEnrollmentlNo.Text = "";
        }
        else if (DocTypeID == 8) //For Marksheet Varification Certificate Document Purpose
        {
            trDivision.Visible = true;
            trLaterReferenceNo.Visible = true;
            trLaterReferenceDate.Visible = true;
            txtDivision.Text = "";
            txtLaterReferenceNo.Text = "";
            txtLaterReferenceDate.Text = "";
            //lnkSubmit_Click.Visible = false;
            //msgVal.Text = "At This Time, You Cant Prepare This Document From System.";
        }
        else if (DocTypeID == 9) //For Degree Verification Certificate Document Purpose
        {
            trDivision.Visible = true;
            trPassingYear.Visible = true;
            trLaterReferenceNo.Visible = true;
            trLaterReferenceDate.Visible = true;
            txtDivision.Text = "";
            txtPassingYear.Text = "";
        }
        else if (DocTypeID == 10) //For Date of Declaration Document Purpose
        {
            trDivision.Visible = true;
            trPassingYear.Visible = true;
            trResulDeclarationDate.Visible = true;
            trSubjectName.Visible = true;
            txtSubjectName.Text = "";
            txtDivision.Text = "";
            txtPassingYear.Text = "";
            txtDateResultDeclaration.Text = "";
        }
        else if (DocTypeID == 11) //For Medium Certificate Document Purpose
        {
            trExamMedium.Visible = true;
            txtExamMedium.Text = "";
        }
        else
        {
            lnkSubmit.Visible = true;

            msgVal.Text = "At This Time, You Can't Prepare This Document From System.";
        }
    }

    protected void btnReject_Click1(object sender, EventArgs e)
    {
        lblCancelSuccessMSG.Text = "";
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objPlSecondLevel = new PL_SecondLevel();
        objPlSecondLevel.Ind = 1;
        objPlSecondLevel.ApplicationID = Convert.ToInt32(ApplicatioID);
        objPlSecondLevel.Rollno = txtRollNo.Text;
        objPlSecondLevel.SName = txtStudentName.Text;
        objPlSecondLevel.ExamName = txtExamName.Text;
        objPlSecondLevel.BranchName = txtBranchName.Text;
        objPlSecondLevel.ExamSession = Convert.ToString(0);
        objPlSecondLevel.EnrollmentNo = txtEnrollmentlNo.Text;
        objPlSecondLevel.College = txtCollege.Text;
        objPlSecondLevel.CGPA = txtCGPA.Text;
        objPlSecondLevel.Division = txtDivision.Text;
        objPlSecondLevel.UserID = Session["UserId"].ToString();
        objPlSecondLevel.EntryByIP = (IpAddress = GetIpAddress());
        var uri = "api/SecondLevel/PostRejectionRecord";
        var response = HClient.PostAsJsonAsync(uri, objPlSecondLevel).Result;

        if (response.IsSuccessStatusCode)
        {
            var getdata = response.Content.ReadAsAsync<IEnumerable<PL_SecondLevel>>().Result;
            if (getdata != null)
            {
                lblmsg.Text = "Mission Success";
            }
            getdata = null;
        }
        response = null; uri = null;
    }

    protected void ddlAwardedby_TextChanged(object sender, EventArgs e)
    {
        if (ddlAwardedby.SelectedIndex == 3)
            TrawardPrice.Visible = true;
        else TrawardPrice.Visible = false;
    }

    protected void lnkViewAppClose_Click(object sender, EventArgs e)
    {
        lblCancelSuccessMSG.Text = "";
        viewApplicationModal.Style.Add("display", "none");
    }

    public static int ConvertIntZero(string val)
    {
        int convertTo;
        try
        {
            if (string.IsNullOrEmpty(val))
            {
                return 0;
            }
            convertTo = Convert.ToInt32(val);
            return convertTo;
        }
        catch
        {
            return 0;
        }
    }

    public static int ConvertIntZero(object val)
    {
        int convertTo;
        try
        {
            if (val == string.Empty)
            {
                return 0;
            }
            convertTo = Convert.ToInt32(val);
            return convertTo;
        }
        catch
        {
            return 0;
        }
    }

    protected void lnkCancelApplication_Click(object sender, EventArgs e)
    {
        GetCancelApplicationList();

        if (ConvertIntZero(lblCounter.Text) <= 0)
        {
            lblNoRecord.Text = "Record Not Found.";
            lblNoRecord.Visible = true;
        }
        else
        {
            lblNoRecord.Text = "";
            lblNoRecord.Visible = false;
        }

        cancelApplicationListModal.Style.Add("display", "block");
    }

    void GetCancelApplicationList()
    {
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Pl_SubmitZeroLevel pl = new Pl_SubmitZeroLevel();
        pl.Ind = 52;
        pl.UserID = Convert.ToInt32(Session["UserId"]);
        pl.CancelInd = 1;

        var uri = "api/SecondLevel/CancelApplicationList";
        var response = HClient.PostAsJsonAsync(uri, pl).Result;
        if (response.IsSuccessStatusCode)
        {
            var getCanceldata = response.Content.ReadAsAsync<DataTable>().Result;
            if (getCanceldata.Rows.Count > 0)
            {
                lblCounter.Text = Convert.ToString(getCanceldata.Rows.Count);
                grdCancelApplicationList.DataSource = getCanceldata;
                grdCancelApplicationList.DataBind();
            }
            else
            {
                lblCounter.Text = "0";
                lblCounter.Visible = true;
                grdCancelApplicationList.DataSource = new DataTable();
                grdCancelApplicationList.DataBind();
            }
        }
    }

    protected void lnkCancelAppListClose_Click(object sender, EventArgs e)
    {
        cancelApplicationListModal.Style.Add("display", "none");
    }
}