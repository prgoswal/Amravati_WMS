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
    static  int Ind = 0;
    static  int usertypeid = 0;
    static string UserId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        btnCorrection.Enabled = false;
        if (!IsPostBack)
        {
            if (Session["UserId"]!=null)
                UserId = Convert.ToString(Session["UserId"]);
            if (Session["UserTypeId"] != null)
                usertypeid = Convert.ToInt32(Session["UserTypeId"]);       

            GetAppDetails();//Calling funtion for load data according to users.

            lblcompletecount.Text =Convert.ToString(grdCompleted.Rows.Count);
            lblpendingcount.Text = Convert.ToString(grdPandingDetails.Rows.Count);            
        }
    }

    public  async void GetAppDetails()
    {
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(DataAcces.Url);
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DataSet myDataSet = null;
            if (usertypeid > 53)// this for all Other users  Other than admin and data entry users.
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
                    Session["PendingPopup"] = myDataSet.Tables[0];
                }
                myDataSet = null; productJsonString = null; response1 = null; uri1 = null;
            }
        }
        catch
        { }
    }
 public  async void GetAppDetail()
    {
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(DataAcces.Url);
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DataSet myDataSet = null;
            if (usertypeid > 53)// this for all Other users  Other than admin and data entry users.
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
                    Session["PendingPopup"] = myDataSet.Tables[0];
                }
                myDataSet = null; productJsonString = null; response1 = null; uri1 = null;
            }
        }
        catch
        { }
    }
  protected async void grdPandingDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {           
                if (e.CommandName == "ShowPopup")
                {
                    j = Convert.ToInt32(Session["UserTypeId"]);

                    pnlform.Visible = true;
                    pnlform.Enabled = false;
                    lnkSubmit.Visible = false;

                    LinkButton btndetails = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                    int rwindex = gvrow.RowIndex;
                    dt = new DataTable();
                    dt = (DataTable)Session["PendingPopup"];
                    if (dt.Rows.Count > 0)
                    {
                        lblEntry.Text = "Applied For : " + dt.Rows[rwindex]["AppliedFor"].ToString().ToUpper();
                        ApprovalCount = 0;

                        txtRollNo.Text = dt.Rows[rwindex]["RollNo"].ToString();
                        txtStudentName.Text = dt.Rows[rwindex]["StudentName"].ToString().ToUpper();                
                        ApplicatioID = Convert.ToInt64(dt.Rows[rwindex]["ApplicationID"].ToString());
                        lblappno.Text = Convert.ToString(ApplicatioID);
                        lngapplicationId = Convert.ToInt32(dt.Rows[rwindex]["ApplicationID"].ToString());
                        DocTypeID = Convert.ToInt32(dt.Rows[rwindex]["DocTypeID"].ToString());

                        //txtExamSession.Text = dt.Rows[rwindex]["ExamSession"].ToString();
                        if ((dt.Rows[rwindex]["ExamSession"].ToString() == "SUMMER") || (dt.Rows[rwindex]["ExamSession"].ToString() == "2"))
                            txtExamSession.Text = "SUMMER";
                        else if ((dt.Rows[rwindex]["ExamSession"].ToString() == "WINTER") || (dt.Rows[rwindex]["ExamSession"].ToString() == "1"))
                            txtExamSession.Text = "WINTER";

                        txtExamYear.Text = dt.Rows[rwindex]["ExamYear"].ToString().ToUpper();
                        HideFoil.ImageUrl = dt.Rows[rwindex]["PartAImagePath"].ToString().ToUpper();
                        HideCF.ImageUrl = dt.Rows[rwindex]["PartBImagePath"].ToString().ToUpper();
                        ImageFoil.ImageUrl = dt.Rows[rwindex]["PartAImagePath"].ToString().ToUpper();
                        ImageCF.ImageUrl = dt.Rows[rwindex]["PartBImagePath"].ToString().ToUpper();
                        txtBranchName.Text = dt.Rows[rwindex]["ExamBranch"].ToString().ToUpper();
                        ImageCF.Width = 400;

                        objPlSecondLevel.Ind = 11;
                        objPlSecondLevel.Rollno = dt.Rows[rwindex]["RollNo"].ToString();
                        objPlSecondLevel.ExamYear = dt.Rows[rwindex]["ExamYear"].ToString();
                        objPlSecondLevel.OccCtrl = 0;

                        if (dt.Rows[rwindex]["ExamSession"].ToString() == "SUMMER")
                            objPlSecondLevel.ExamSession = "2";
                        else if (dt.Rows[rwindex]["ExamSession"].ToString() == "WINTER")
                            objPlSecondLevel.ExamSession = "1";

                        Convert.ToInt32(objPlSecondLevel.Rollno);
                        Convert.ToInt32(objPlSecondLevel.ExamYear);
                        Convert.ToInt32(objPlSecondLevel.ExamSession);

                        txtExamName.Text = dt.Rows[0]["ExamName"].ToString().Trim().ToUpper();

                        txtExamName.Enabled = true; txtExamSession.Enabled = false; txtExamYear.Enabled = false; txtRollNo.Enabled = false;
                        trDivision.Visible = trCgpa.Visible = trEnroll.Visible = trCollege.Visible =
                        trSubjectName.Visible = trDistinctionSub.Visible = trMeritNo.Visible =
                        trLaterReferenceNo.Visible = trLaterReferenceDate.Visible = trPassingYear.Visible = trExamMedium.Visible = 
                        trAwardedBy.Visible = TrawardPrice.Visible = trResulDeclarationDate.Visible = trRank.Visible = false;

                        if (DocTypeID == 1)
                        {
                            trDivision.Visible = true;
                            trCgpa.Visible = true;
                            txtDivision.Text = dt.Rows[rwindex]["Division"].ToString().ToUpper();
                            txtCGPA.Text = dt.Rows[rwindex]["CGPAPercentage"].ToString().ToUpper();
                        }
                        else if (DocTypeID == 2)
                        {
                            trEnroll.Visible = true;
                            trDivision.Visible = true;
                            trCgpa.Visible = true;
                            trCollege.Visible = true;

                            txtDivision.Text = dt.Rows[rwindex]["Division"].ToString().ToUpper();
                            txtEnrollmentlNo.Text = dt.Rows[rwindex]["EnrollmentNo"].ToString().ToUpper();
                            txtCollege.Text = dt.Rows[rwindex]["College"].ToString().ToUpper();
                            txtCGPA.Text = dt.Rows[rwindex]["CGPAPercentage"].ToString();
                        }
                        else if (DocTypeID == 3)
                        {
                            trEnroll.Visible = true;
                            trSubjectName.Visible = true;
                            trDistinctionSub.Visible = true;

                            txtEnrollmentlNo.Text = dt.Rows[rwindex]["EnrollmentNo"].ToString().ToUpper();
                            txtSubjectName.Text = dt.Rows[rwindex]["SubjectName"].ToString().ToUpper();
                            txtDistinctionSub.Text = dt.Rows[rwindex]["DistinctionSubject"].ToString().ToUpper();
                        }
                        else if (DocTypeID == 4)
                        {
                            trMeritNo.Visible = true;
                            txtMeritNo.Text = dt.Rows[rwindex]["MeritNo"].ToString().ToUpper();
                        }
                        else if (DocTypeID == 6)
                        {
                            trDivision.Visible = true;
                            trMeritNo.Visible = trRank.Visible = trAwardedBy.Visible = TrawardPrice.Visible = true;
                            txtAwardPrize.Text = dt.Rows[rwindex]["AwardPrize"].ToString();
                            if (dt.Rows[rwindex]["Ranking"] == "IInd") ddlRank.SelectedIndex = 2;
                            else ddlRank.SelectedIndex = 1;

                            if (dt.Rows[rwindex]["AwardedBy"].ToString() == "Gold Medal") ddlAwardedby.SelectedIndex = 1;
                            else if (dt.Rows[rwindex]["AwardedBy"].ToString() == "Silver Medal") ddlAwardedby.SelectedIndex = 2;
                            else if (dt.Rows[rwindex]["AwardedBy"].ToString() == "Cash Prize") ddlAwardedby.SelectedIndex = 3;

                            txtDivision.Text = dt.Rows[rwindex]["Division"].ToString().ToUpper();
                            txtMeritNo.Text = dt.Rows[rwindex]["MeritNo"].ToString().ToUpper();
                        }
                        else if (DocTypeID == 8)
                        {
                            trDivision.Visible = true;
                            trLaterReferenceNo.Visible = true;
                            trLaterReferenceDate.Visible = true;

                            txtDivision.Text = dt.Rows[rwindex]["Division"].ToString().ToUpper();
                            txtLaterReferenceNo.Text = dt.Rows[rwindex]["LetterRefNo"].ToString().ToUpper();
                            txtLaterReferenceDate.Text = Convert.ToDateTime(dt.Rows[rwindex]["LetterRefDate"]).ToString("yyyy-MM-dd");

                        }
                        else if (DocTypeID == 9)
                        {
                            trDivision.Visible = trLaterReferenceNo.Visible = 
                            trLaterReferenceDate.Visible = trPassingYear.Visible = true;
                            txtDivision.Text = dt.Rows[rwindex]["Division"].ToString().ToUpper();
                            txtPassingYear.Text = dt.Rows[rwindex]["ExamPassingYear"].ToString().ToUpper();
                            txtLaterReferenceNo.Text = dt.Rows[rwindex]["LetterRefNo"].ToString().ToUpper();
                            txtLaterReferenceDate.Text = Convert.ToDateTime(dt.Rows[rwindex]["LetterRefDate"]).ToString("yyyy-MM-dd");
                        }
                        else if (DocTypeID == 11)
                        {
                            trExamMedium.Visible = true;
                            txtExamMedium.Text = dt.Rows[rwindex]["ExamMedium"].ToString().ToUpper();
                        }
                        viewApplicationModal.Style.Add("display", "block");
                        //Popup(true);
                    }
                }
        

            HttpClient hclient = new HttpClient();
            hclient.BaseAddress = new Uri(DataAcces.Url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            objPlSecondLevel = new PL_SecondLevel();
            var uri = string.Format("api/SecondLevel/GetLevelRemark/?Ind={0}&ApplicationId={1}", 5, lngapplicationId);
            DataTable dtlevel = null;
            var response = hclient.GetAsync(uri).Result;
            var productJsonString = await response.Content.ReadAsStringAsync();
            dtlevel = JsonConvert.DeserializeObject<DataTable>(productJsonString);

            
            StringBuilder sb = new StringBuilder();

            if (dtlevel.Rows.Count > 0)
            {
                sb.Append("<table style='width:100%; border:1px solid;'>");
                sb.Append("<tr style='background-color:#50618c; color:white; border:1px solid;'> <th style='width:15%;'>Approval By </th><th style='width:15%;'> Approval Date </th> <th style='width:25%;'> Remark</th></tr>");
                for (int i = 0; i < dtlevel.Rows.Count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='display:none;'>" + dtlevel.Rows[i]["LevelID"] + "</td>");
                    sb.Append("<td style='border:1px solid; width:15%;'> " + dtlevel.Rows[i]["LevelDesc"] + "</td>");
                    sb.Append("<td style='border:1px solid; text-align:center; width:15%;'>" + dtlevel.Rows[i]["ApprovalDate"] + " </td>");
                    sb.Append("<td style='border:1px solid; width:25%;'>" + dtlevel.Rows[i]["LevelRemark"] + " </td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                tblRemark.InnerHtml = sb.ToString();      
                              
            }
           // dtlevel = null;
        }
        catch
        { } 
    }
  protected void grdCompleted_RowCommand(object sender, GridViewCommandEventArgs e)
  {
      if (e.CommandName == "ShowStatus")
      {
          Response.Redirect("FrmApplicationProgress.aspx?CallID=2&ApplicationID=" + e.CommandArgument);
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
    public int  GetRecordId()
    {
        HttpClient hclient = new HttpClient();
        hclient.BaseAddress = new Uri(DataAcces.Url);
        hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objPlSecondLevel = new PL_SecondLevel();

        var uri =string.Format("api/SecondLevel/GetRecordId/?Ind={0}&ApplicationId={1}",3,lngapplicationId);
        var response = hclient.GetAsync(uri).Result;
        var getdata = response.Content.ReadAsAsync<IEnumerable<PL_SecondLevel>>().Result;
        foreach (var a in getdata)
        {
            lngRecordId =a.recordId;
        }
        getdata = null; response = null; uri = null;
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
        if (txtExamName.Text != "")
        {
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Application Approved');window.location='FrmApproval.aspx';", true);
                                    
                }
                getdata = null;
            }
            response = null; uri = null;
        }
        else
            lblmsg.Text = "Please Enter Exam Name";
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        try
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

            if (txtMeritNo.Text != string.Empty)
                pl.MeritNo = Convert.ToInt32(txtMeritNo.Text);
            else
                pl.MeritNo = 0;

            pl.LaterReferenceNo = txtLaterReferenceNo.Text;
            pl.PassingYear = txtPassingYear.Text;
            pl.ExamMedium = txtExamMedium.Text;

            var uri = "api/SecondLevel/PostSubmitPopDetail/";
            var response = HClient.PostAsJsonAsync(uri, pl).Result;
            if (response.IsSuccessStatusCode)
            {
                var getdata = response.Content.ReadAsAsync<DataTable>().Result;
                if (getdata.Rows.Count > 0)
                {
                    Server.Transfer("FrmApproval.aspx");
                }
                getdata = null;
            }
            response = null; uri = null;
        }
        catch
        { }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtRemark.Text != null && txtRemark.Text != "")
            {
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
                //  pl.maxaproval = ApprovalCount;
                var uri = "api/SecondLevel/PostRejectionRecord";
                var response = HClient.PostAsJsonAsync(uri, pl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var getdata = response.Content.ReadAsAsync<DataTable>().Result;
                    if (getdata.Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Application Rejected');window.location='FrmApproval.aspx';", true);

                    }
                    getdata = null;
                }
                response = null; uri = null;

            }
            else
            {
                lblmsg.Text = "Please Enter Remarks";
            }

        }
        catch { }
    }

    protected void btnCorrection_Click(object sender, EventArgs e)
    {
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objPlSecondLevel = new PL_SecondLevel();
        objPlSecondLevel.Ind = 2;
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Data Send to Correction');window.location='FrmApproval.aspx';", true);               
          
                //lblmsg.Text = "Mission Success";
            }
            getdata = null;
        }
        response = null; uri = null;      
    }

    public DataTable ShowExamDetail(PL_SecondLevel objPlSecondLevel)
    {   
        cmd = new SqlCommand("SPApplicationDetail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", objPlSecondLevel.Ind);
        cmd.Parameters.AddWithValue("@Rollno", objPlSecondLevel.Rollno);
        cmd.Parameters.AddWithValue("@ExamYear", objPlSecondLevel.ExamYear);
        cmd.Parameters.AddWithValue("@OccCtrl", objPlSecondLevel.OccCtrl);
        cmd.Parameters.AddWithValue("@ExamSession", objPlSecondLevel.ExamSession);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    protected void lnkViewAppClose_Click(object sender, EventArgs e)
    {
        viewApplicationModal.Style.Add("display", "none");
    }
}
