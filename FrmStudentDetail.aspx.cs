using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.Web.Services;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

public partial class FrmStudentDetail : System.Web.UI.Page
{
    BlStudentDetail BlStudentDetail = new BlStudentDetail();
    PlStudentDetail objplstudetail = new PlStudentDetail();
    PL_SecondLevel objplsecondlevel = new PL_SecondLevel();
    SqlCommand cmd; SqlDataAdapter da; DataTable dt;
    string ipAddress;
    static int applicationID = 0;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    string msg = ""; static string CFImgPath; static string FoilImgPath;
    List<PlStudentDetail> obj = new List<PlStudentDetail>();
    PlStudentDetail objplstudentDetail = new PlStudentDetail();
    static string UserId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //pnlcheck.Visible = false;
                if (Session["UserId"] != null)
                    UserId = Convert.ToString(Session["UserId"]);

                TxtRmark.Enabled = false;
                BindExamYear();
                DDLExamYear.Items.Insert(0, new ListItem("Select Year", "0"));
                DDLExamYear.Focus();
                BindExamSession();
                DDLExamSession.Items.Insert(0, new ListItem("Select Session", "0"));
                //DDLExamSession.Focus();
                BindForApply();
                DDLApply.Items.Insert(0, new ListItem("Select Applied For", "0"));
                //DDLApply.Focus();
                BtnSave.Enabled = true;
                pendingData();
                Panel1.Visible = false;
                //string sysDate = System.DateTime.Now.ToShortDateString();
                //txtRecieptDate.Text = sysDate.Substring(0, 2) + "/" + sysDate.Substring(3, 2) + "/" + sysDate.Substring(6, 4);
                LblSearchCount.Text = "";
            }
            catch (Exception ex)
            {
                lblSms.Text = ex.Message;
            }
        }
    }

    public async void pendingData()
    {
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(DataAcces.Url);
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DataSet myDataSet = null;
            objplsecondlevel.Ind = 3;
            if (Convert.ToInt32(Session["UserTypeId"]) == 51)
                objplsecondlevel.UserID = "0";
            else
                objplsecondlevel.UserID = UserId;

            var uri = string.Format("api/SecondLevel/GetAppDetailZeroLevel/?Ind={0}&UserID={1}", objplsecondlevel.Ind, objplsecondlevel.UserID);
            var response = HClient.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var productJsonString = await response.Content.ReadAsStringAsync();
                myDataSet = JsonConvert.DeserializeObject<DataSet>(productJsonString);
            }
            if (myDataSet.Tables.Count > 0)
            {
                lblCounter.Text = Convert.ToString(myDataSet.Tables[0].Rows.Count);
            }
            myDataSet = null;
            response = null; uri = null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }
        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

    private void BindExamYear()   // Exam Year Purpose
    {
        objplstudetail.Ind = 3;
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objplstudentDetail = new PlStudentDetail();
        var uri = "api/StudentDetail/GetExamYear/";
        var response = HClient.GetAsync(uri).Result;
        if (response.IsSuccessStatusCode)
        {
            var getdata = response.Content.ReadAsAsync<IEnumerable<PlStudentDetail>>().Result;
            if (getdata.Count() > 0)
            {
                DDLExamYear.DataSource = getdata;
                DDLExamYear.DataValueField = "ItemID";
                DDLExamYear.DataTextField = "ItemDesc";
                DDLExamYear.DataBind();
            }
            getdata = null;
        }
        response = null; uri = null;
    }

    private void BindExamSession()   // Exam Session Purpose
    {
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objplstudentDetail = new PlStudentDetail();
        var uri = "api/StudentDetail/GetExamSession/?ind=" + "2";
        var response = HClient.GetAsync(uri).Result;
        if (response.IsSuccessStatusCode)
        {
            var getdata = response.Content.ReadAsAsync<IEnumerable<PlStudentDetail>>().Result;
            if (getdata.Count() > 0)
            {
                DDLExamSession.DataSource = getdata;
                DDLExamSession.DataValueField = "ItemID";
                DDLExamSession.DataTextField = "ItemDesc";
                DDLExamSession.DataBind();
            }
            getdata = null;
        }
        response = null; uri = null;
    }

    private void BindForApply()   // Exam  Purpose For Apply
    {
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objplstudentDetail = new PlStudentDetail();
        var uri = "api/StudentDetail/GetApply/?ind=" + "15";
        var response = HClient.GetAsync(uri).Result;
        if (response.IsSuccessStatusCode)
        {
            var getdata = response.Content.ReadAsAsync<IEnumerable<PlStudentDetail>>().Result;
            if (getdata.Count() > 0)
            {
                DDLApply.DataSource = getdata;
                DDLApply.DataValueField = "ItemID";
                DDLApply.DataTextField = "DocumentType";
                DDLApply.DataBind();
            }
            getdata = null;
        }
        response = null; uri = null;
    }

    private void Clear()
    {
        if (DDLExamSession.SelectedIndex > 0)
            DDLExamSession.SelectedIndex = 0;
        if (DDLExamYear.SelectedIndex > 0)
            DDLExamYear.SelectedIndex = 0;
        if (DDLApply.SelectedIndex > 0)
            DDLApply.SelectedIndex = 0;

        TxtStuName.Text = txtRollNo.Text = TxtRmark.Text = lblSms.Text = txtRecieptNo.Text = txtRecieptAmt.Text =
            txtRecieptDate.Text = TxtRmark.Text = "";
        DDLApply.Enabled = DDLExamSession.Enabled = DDLExamYear.Enabled = TxtRmark.Enabled = TxtStuName.Enabled = txtRollNo.Enabled = true;
        GridStudentDetail.DataSource = null;
        GridStudentDetail.DataBind();
        Panel1.Visible = false;
        LblSearchCount.Text = "";
        FoilImgPath = CFImgPath = "";

        DDLExamYear.Focus();
        //Response.Redirect("FrmStudentDetail.aspx");
    }

    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void SearchDetail()   // Search Detail Purpose
    {
        try
        {
            lblSms.Text = "";

            string rollno;
            if (txtRollNo.Text.Trim() == "")
                rollno = "0";
            else
                rollno = Convert.ToString(txtRollNo.Text.Trim());

            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(DataAcces.Url);
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            objplstudentDetail = new PlStudentDetail();

            var uri = string.Format("api/StudentDetail/GetStudentDetail/?ind={0}&ExamYear={1}&ExamSession={2}&StudentName={3}&Rollno={4}", 2, DDLExamYear.SelectedItem.ToString(), DDLExamSession.SelectedValue.ToString(), TxtStuName.Text.Trim(), rollno);
            var response = HClient.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                //var getdata = response.Content.ReadAsAsync<IEnumerable<PlStudentDetail>>().Result;
                var getdata = response.Content.ReadAsAsync<DataTable>().Result;
                if (getdata.Rows.Count > 0)
                {
                    LblSearchCount.Text = "Total Searched Records : " + getdata.Rows.Count;
                    Session["dtSession"] = getdata;

                    Panel1.Visible = true;
                    GridStudentDetail.DataSource = getdata;
                    GridStudentDetail.DataBind();

                    DDLApply.Enabled = false;
                    DDLExamSession.Enabled = false;
                    DDLExamYear.Enabled = false;
                    TxtRmark.Enabled = true;
                    TxtStuName.Enabled = false;
                    txtRollNo.Enabled = false;

                    foreach (DataRow a in getdata.Rows)
                    {
                        objplstudetail.ExamName = Convert.ToString(a["Faculty_Exam_Name"]);
                        objplstudetail.PartAImagePath = Convert.ToString(a["PartAImagePath"]);
                        // if (a["PartARegNo"] != null || a["PartARegNo"] != "")
                        objplstudetail.PartARegNo = Convert.ToInt32(a["PartARegNo"]);
                        objplstudetail.PartBImagePath = Convert.ToString(a["PartBImagePath"]);
                        // if (a["PartBRegNo"] != null || a["PartBRegNo"] != "")
                        objplstudetail.PartBRegNo = Convert.ToInt32(a["PartBRegNo"]);

                        objplstudetail.Rollno = Convert.ToString(a["Rollno"]);
                        objplstudetail.SName = Convert.ToString(a["SName"]);
                        obj.Add(objplstudetail);
                    }

                    getdata = null;
                }
                else
                {
                    lblSms.Text = "No Matching Record Found.";
                    Session["dtSession"] = null;
                }
            }
            response = null; uri = null;
            return;
        }
        catch
        {

        }
        finally { }
    }

    public int MyProperty { get; set; }

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

    protected void GridStudentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    [WebMethod]
    public static List<string> GetAutoCompleteData(string SName)
    {
        List<string> result = new List<string>();
        using (SqlConnection con = new SqlConnection("Data Source=MLCV004; User Id=sa; Password=odpserver550810998@ ; Initial Catalog=UnivAmravati"))
        {
            using (SqlCommand cmd = new SqlCommand("select SName from TblFoilImgEntry where SName LIKE '%'+@SearchText+'%'", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchText", SName);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["SName"].ToString());
                }
                return result;
            }
        }
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

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string lblExamName = "", lblPartARegNo = "", lblPartAImagePath = "", lblPartBRegNo = "", lblPartBImagePath = "";
        foreach (GridViewRow item in GridStudentDetail.Rows)
        {
            CheckBox Cb = (CheckBox)item.FindControl("CheckBox1");
            if (Cb.Checked)
            {
                lblExamName = (item.FindControl("lblExamName") as Label).Text;
                lblPartARegNo = (item.FindControl("lblPartARegNo") as Label).Text;
                lblPartAImagePath = (item.FindControl("lblPartAImagePath") as Label).Text;
                lblPartBRegNo = (item.FindControl("lblPartBRegNo") as Label).Text;
                lblPartBImagePath = (item.FindControl("lblPartBImagePath") as Label).Text;

            }
        }

        objplstudentDetail = new PlStudentDetail();
        objplstudetail.Ind = 1;
        objplstudentDetail.ApplicationID = 0;
        objplstudentDetail.ExamSession = DDLExamSession.SelectedValue;
        objplstudentDetail.ExamYear = DDLExamYear.SelectedValue;
        objplstudentDetail.ExamName = lblExamName;
        objplstudentDetail.StudentName = TxtStuName.Text;
        objplstudentDetail.Rollno = txtRollNo.Text;
        objplstudentDetail.Remarks = TxtRmark.Text;
        objplstudentDetail.AppliedFor = DDLApply.SelectedValue;
        objplstudentDetail.SubmittedFees = string.IsNullOrEmpty(txtRecieptAmt.Text) ? 0 : Convert.ToDecimal(txtRecieptAmt.Text);
        objplstudentDetail.PartARegNo = string.IsNullOrEmpty(lblPartARegNo) ? 0 : Convert.ToInt32(lblPartARegNo);
        objplstudentDetail.PartAImagePath = lblPartAImagePath;
        objplstudentDetail.PartBRegNo = string.IsNullOrEmpty(lblPartBRegNo) ? 0 : Convert.ToInt32(lblPartBRegNo);
        objplstudentDetail.PartBImagePath = lblPartBImagePath;
        objplstudentDetail.InsertionBy = UserId;
        objplstudentDetail.InsertionByIP = ipAddress;
        objplstudentDetail.EstimateDate = DateTime.Now.ToString("dd/MM/yyyy");

        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objplstudentDetail = new PlStudentDetail();
        var uri = "api/StudentDetail/SaveRecord/";
        var response = HClient.PostAsJsonAsync(uri, objplstudentDetail).Result;
        if (response.IsSuccessStatusCode)
        {
            var getdata = response.Content.ReadAsAsync<IEnumerable<PlStudentDetail>>().Result;
            if (getdata.Count() > 0)
            {

            }
        }
    }

    protected void GridStudentDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
        int index_row = gvr.RowIndex;
    }

    protected void GridStudentDetail_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
        int index_row = gvr.RowIndex;
    }

    protected void GridStudentDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        TxtRmark.Enabled = true;
        lblSms.Text = "";
        int c = 0;
        if (DDLExamYear.SelectedIndex != 0)
        {
            //c = 1;
            if (DDLExamSession.SelectedIndex != 0)
            {
                if (txtRollNo.Text != "" || TxtStuName.Text != "")
                {
                    c = 1;
                    if (c == 1 && DDLApply.SelectedIndex != 0)
                        SearchDetail();
                    else
                    {
                        lblSms.Text = "Please Select Applied For.";
                        DDLApply.Focus();
                    }
                }
                else
                {
                    lblSms.Text = "Please Enter Roll No. Or Student Name.";
                    txtRollNo.Focus();
                }
            }
            else
            {
                lblSms.Text = "Please Select Exam Session.";
                DDLExamSession.Focus();
            }

            foreach (GridViewRow rw in GridStudentDetail.Rows)
            {
                CheckBox chkBx = (CheckBox)rw.FindControl("CheckBox1");
                if (GridStudentDetail.Rows.Count == 1)
                    chkBx.Checked = true;
                else
                    chkBx.Checked = false;
            }
        }
        else
        {
            lblSms.Text = "Please Select Exam Year.";
            DDLExamYear.Focus();
        }
    }
    protected void GridStudentDetail_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowPopup")
        {
            hfRowIndex.Value = "";
            LinkButton btndetails = (LinkButton)e.CommandSource;
            GridViewRow GridStudentDetail = (GridViewRow)btndetails.NamingContainer;
            dt = new DataTable();
            dt = (DataTable)Session["dtSession"];
            int rwindex = GridStudentDetail.RowIndex;
            hfRowIndex.Value = rwindex.ToString();
            if (dt.Rows.Count > 0)
            {
                ImgCF.ImageUrl = dt.Rows[rwindex]["PartBImagePath"].ToString();
                ImageSelected.ImageUrl = dt.Rows[rwindex]["PartAImagePath"].ToString();
                lblExamName.Text = "Exam Name - " + dt.Rows[rwindex]["Faculty_Exam_Name"].ToString();
                lblYeaeSession.Text = "Exam Year - " + DDLExamYear.SelectedItem.ToString() + "  Session - " + DDLExamSession.SelectedItem.ToString() + "  Student Name - " + dt.Rows[rwindex]["SName"].ToString() + "  Roll No - " + dt.Rows[rwindex]["RollNo"].ToString();
                Popup(true);
                return;
            }
        }
    }
    protected void lnkpending_Click(object sender, EventArgs e)
    {
        Clear();
        Response.Redirect("FrmZeroLevel.aspx");
    }

    protected void BtnSave_Click1(object sender, EventArgs e)
    {
        try
        {
            lblSms.Text = "";

            DateTime InputDate;
            if (!DateTime.TryParse(txtRecieptDate.Text, out InputDate))
            {
                lblSms.Text = "Invalid Receipt Date.";
                return;
            }
            DateTime CurrentDate = System.DateTime.Now.Date;
            double day;
            day = (CurrentDate - InputDate).TotalDays;
            if (day > 7)
            {
                lblSms.Text = "Date Cannot Be 7 days old.";
                return;
            }
            else if (day < 0)
            {
                lblSms.Text = "Date Cannot Be More Than Current Date.";
                return;
            }

            if (Session["UserId"] != null && Session["UserId"].ToString() != string.Empty)
            {
                DataTable dtprint = new DataTable();
                int chk = 1;
                int row = 0;

                foreach (GridViewRow rw in GridStudentDetail.Rows)
                {
                    CheckBox chkBx = (CheckBox)rw.FindControl("CheckBox1");
                    if (chkBx.Checked == true)
                    {
                        objplstudetail.Ind = 1;
                        objplstudetail.Rollno = GridStudentDetail.Rows[row].Cells[1].Text.ToString(); //Convert.ToString( );//For RollNo.
                        objplstudetail.StudentName = Convert.ToString(GridStudentDetail.Rows[row].Cells[2].Text.ToString());//For Student Name
                        objplstudetail.ExamName = ((Label)GridStudentDetail.Rows[row].FindControl("lblExamName")).Text;

                        //(Label)gvGridStudentDetail.Rows[row].cells[0].FindControl("TOTAL");
                        //objplstudetail.ExamName = Convert.ToString(GridStudentDetail.Rows[row].Cells[3].Text.ToString());//For Exam Name
                        objplstudetail.ExamYear = DDLExamYear.SelectedItem.ToString();
                        objplstudetail.ExamSession = DDLExamSession.SelectedValue.ToString();
                        objplstudetail.AppliedFor = DDLApply.SelectedItem.ToString();
                        objplstudetail.Remarks = TxtRmark.Text.Trim();
                        objplstudetail.InsertionBy = Session["UserId"].ToString();
                        objplstudetail.InsertionByIP = GetIPAddress();
                        objplstudetail.InserttionDate = Convert.ToString(DateTime.Now);
                        objplstudetail.SubmittedFees = 0;
                        objplstudetail.ApplicationID = applicationID;
                        objplstudetail.EstimateDate = Convert.ToString(DateTime.Now);

                        objplstudetail.PartARegNo = Convert.ToInt32(GridStudentDetail.DataKeys[row][1].ToString());
                        objplstudetail.PartAImagePath = GridStudentDetail.DataKeys[row][2].ToString();
                        objplstudetail.PartBRegNo = Convert.ToInt32(GridStudentDetail.DataKeys[row][3].ToString());
                        objplstudetail.PartBImagePath = GridStudentDetail.DataKeys[row][4].ToString();

                        if (txtRecieptNo.Text != string.Empty)
                            objplstudetail.ReceiptNo = Convert.ToInt32(txtRecieptNo.Text);
                        else
                            objplstudetail.ReceiptNo = 0;

                        if (txtRecieptDate.Text != string.Empty)
                            objplstudetail.ReceiptDate = Convert.ToDateTime(txtRecieptDate.Text);
                        else
                            objplstudetail.ReceiptDate = Convert.ToDateTime("01/01/1900");

                        if (txtRecieptAmt.Text != string.Empty)
                            objplstudetail.ReceiptAMT = Convert.ToDecimal(txtRecieptAmt.Text);
                        else
                            objplstudetail.ReceiptAMT = 0;

                        HttpClient HClient = new HttpClient();
                        HClient.BaseAddress = new Uri(DataAcces.Url);
                        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var uri1 = "api/StudentDetail/SaveRecord";
                        var response1 = HClient.PostAsJsonAsync(uri1, objplstudetail).Result;
                        var getdata1 = response1.Content.ReadAsAsync<DataTable>().Result;
                        if (response1.IsSuccessStatusCode)
                        {
                            if (getdata1.Rows.Count > 0)
                            {
                                PopupPrint(true);

                                lblappno.Text = Convert.ToString(getdata1.Rows[0]["ApplicationID"]);
                                _lblAppNo.Text = Convert.ToString(getdata1.Rows[0]["ApplicationID"]);

                                lblappdate.Text = Convert.ToString(getdata1.Rows[0]["ApplicationDate"]);
                                _lblAppDate.Text = Convert.ToString(getdata1.Rows[0]["ApplicationDate"]);

                                lblRollNo.Text = Convert.ToString(getdata1.Rows[0]["RollNo"]);
                                _lblRollNo.Text = Convert.ToString(getdata1.Rows[0]["RollNo"]);

                                lblStudentName.Text = Convert.ToString(getdata1.Rows[0]["StudentName"]);
                                _lblStudentName.Text = Convert.ToString(getdata1.Rows[0]["StudentName"]);

                                lblExamNm.Text = Convert.ToString(getdata1.Rows[0]["ExamName"]);
                                _lblExamNm.Text = Convert.ToString(getdata1.Rows[0]["ExamName"]);

                                lblSessionYear.Text = Convert.ToString(getdata1.Rows[0]["ExamSession"]);
                                _lblSessionYear.Text = Convert.ToString(getdata1.Rows[0]["ExamSession"]);

                                lblAppliedfor.Text = Convert.ToString(getdata1.Rows[0]["AppliedFor"]);
                                _lblAppliedfor.Text = Convert.ToString(getdata1.Rows[0]["AppliedFor"]);

                                lblSms.Text = "Data Saved.";
                                BtnSave.Enabled = false;
                                chk = 0;
                                break;
                            }
                        }
                        else
                        {
                            lblSms.Text = "Record Not Saved.";
                            getdata1 = null; response1 = null; uri1 = null;
                            return;
                        }
                    }
                    row++;
                }
                if (chk == 1)
                {
                    lblSms.Text = "Please Select 1 Record For Save.";
                    return;
                }
                else if (chk == 0)
                {
                    lblSms.Text = "Record Saved Succesfully On Application No. :" + lblappno.Text;
                    lblappno1.Text = lblappno.Text;
                    return;
                }

            }
        }
        catch (Exception ex)
        {
            lblSms.Text = msg = ex.Message;
        }
    }

    protected void btnImgClose_Click(object sender, ImageClickEventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<script language=JavaScript> HidePopupPrint(); </script>\n");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
        GridStudentDetail.Rows[Convert.ToInt32(hfRowIndex.Value)].Cells[4].Focus();
    }
}