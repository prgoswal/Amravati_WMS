﻿using System;
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
using System.Drawing;
using System.Drawing.Printing;
using System.Windows;

using Microsoft.Reporting.WebForms;


public partial class FrmPrint : System.Web.UI.Page
{
    BlStudentDetail BlStudentDetail = new BlStudentDetail();
    PlStudentDetail objplstudentDetail = new PlStudentDetail();
    PL_SecondLevel objplsecondlevel = new PL_SecondLevel();
    static DataTable dt = null;
    static string UserId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable myDataTable = new DataTable();
        foreach (GridViewRow r in GridDocumentType.Rows)
        {
            r.BackColor = System.Drawing.Color.Transparent;
        }

        if (!IsPostBack)
        {
            PanelclickDetail.Visible = false;
            UserId = Convert.ToString(Session["UserId"]);
            BindForApply();
            pendingData();

        }
    }

 
    private void BindForApply()   // Document Type  Purpose For Apply
    {
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        objplstudentDetail = new PlStudentDetail();
        var uri = "api/StudentDetail/GetApply/?ind=" + "15";
        var response = HClient.GetAsync(uri).Result;
        if (response.IsSuccessStatusCode)
        {
            // var getdata = response.Content.ReadAsAsync<IEnumerable<PlStudentDetail>>().Result;
            dt = new DataTable();
            dt = response.Content.ReadAsAsync<DataTable>().Result;
            if (dt.Rows.Count > 0)
            {


            }

        }
        //response = null; uri = null;
    }

    public async void pendingData()
    {
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        DataTable dt = new DataTable();
        //  objplsecondlevel.Ind = 3;
        //if (Convert.ToInt32(Session["UserTypeId"]) == 51)
        //    objplsecondlevel.UserID = "0";
        //else
        //    objplsecondlevel.UserID = UserId;

        var uri = string.Format("api/Print/GetAllPrintCount/?Ind={0}&UserID={1}&UserTypeID={2}", 25, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(Session["UserTypeId"]));
        var response = HClient.GetAsync(uri).Result;
        if (response.IsSuccessStatusCode)
        {
            var productJsonString = await response.Content.ReadAsStringAsync();
            Session["BindDocument"] = dt;
            dt = JsonConvert.DeserializeObject<DataTable>(productJsonString);
            if (dt.Rows.Count > 0)
            {
                GridDocumentType.DataSource = dt;
                GridDocumentType.DataBind();
            }
        }
       
    }

    public async void TestPrintDetail()
    {
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        DataTable dt = new DataTable();
        //  objplsecondlevel.Ind = 3;
        //if (Convert.ToInt32(Session["UserTypeId"]) == 51)
        //    objplsecondlevel.UserID = "0";
        //else
        //    objplsecondlevel.UserID = UserId;
 
      int DocTypeId = Convert.ToInt32(Request.QueryString["docid"].ToString());
      int UserID = Convert.ToInt32(Session["UserId"]);
      int LevelID = Convert.ToInt32(Session["UserTypeId"]);
      var uri = string.Format("api/Print/TestPrintDetail/?Ind={0}&UserID={1}&LevelID={2}&DocTypeId={3}", 1, UserID, LevelID, DocTypeId);
        var response = HClient.GetAsync(uri).Result;
        if (response.IsSuccessStatusCode)
        {
            var productJsonString = await response.Content.ReadAsStringAsync();
            dt = JsonConvert.DeserializeObject<DataTable>(productJsonString);
            Session["PrintPopup"] = dt;
            if (dt.Rows.Count > 0)
            {
                grdPandingDetails.DataSource = dt;
                grdPandingDetails.DataBind();
            }

        }
        if (dt != null)
        {


        }
      
    }

    protected void grdPandingDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        pnlReport.Visible = true;
        if (e.CommandName == "ShowPopup")
        {

           // int rwindex = int.Parse(e.CommandArgument.ToString());

          //  GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //LinkButton lnkbtn = (LinkButton)row.FindControl("LinkButtonPrint");
            //LinkButton lnkbtnpreview = (LinkButton)row.FindControl("LinkButtonEdit");
            //lnkbtn.Visible = true;
            //lnkbtnpreview.Visible = false;

            LinkButton btndetails = (LinkButton)e.CommandSource;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            dt = new DataTable();
            dt = (DataTable)Session["PrintPopup"];
            int rwindex = gvrow.RowIndex;
            if (dt.Rows.Count > 0)
            {
                LblApplicationNo.Text = dt.Rows[rwindex]["ApplicationId"].ToString();
                LblRoll.Text = dt.Rows[rwindex]["RollNo"].ToString();
                LblStuName.Text = dt.Rows[rwindex]["StudentName"].ToString();
                LblDateApplication.Text = dt.Rows[rwindex]["EntryDate"].ToString();
                LblLastDate.Text = dt.Rows[rwindex]["LastApprovalDate"].ToString();

            }

            if (Session["DocType"].ToString() == "2")
            {
                Session["Report"] = "Report_4";
            }
            else if (Session["DocType"].ToString() == "9")
            {
                Session["Report"] = "Report_6";
            }
            else if (Session["DocType"].ToString() == "4")
            {
                Session["Report"] = "Report_8";
            }
            else if (Session["DocType"].ToString() == "3")
            {
                Session["Report"] = "Report_9";
            }
            else if (Session["DocType"].ToString() == "8")
            {
                Session["Report"] = "Report_10";
            }
            else if (Session["DocType"].ToString() == "11")
            {
                Session["Report"] = "Report_11";
            }else
            {
                lblMsg.Text = "Report Not Prepare till Now.";
                return;
            }
           
            Hashtable Ht=new Hashtable();
            Ht.Add("Ind",2);
            Ht.Add("UserID",Session["UserId"]);
            Ht.Add("LevelID",Session["UserTypeId"]);
            Ht.Add("ApplicationID",  LblApplicationNo.Text);
            Ht.Add("DocType", Session["DocType"]);
            Session["HT"]=Ht;



            //HiddenField1.Value = Request.UrlReferrer.AbsoluteUri;
            ReportViewer1.ShowCredentialPrompts = true;

            Microsoft.Reporting.WebForms.IReportServerCredentials irsc = new CustomReportCredentials("administrator", ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["ReportServer"].ToString());//"http://occweb02/ReportServer");
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            Hashtable HT = new Hashtable();
            HT = (Hashtable)Session["HT"];
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"].ToString());
            // ReportViewer1.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportProjectName"].ToString() + Convert.ToString(ConfigurationSettings.AppSettings["Report"]);
            ReportViewer1.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportProjectName"].ToString() + Session["Report"].ToString();
            if (HT != null)
            {
                ReportParameter[] parm = new ReportParameter[HT.Count];
                int i = 0;
                foreach (DictionaryEntry Dt in HT)
                {
                    parm[i] = new ReportParameter(Convert.ToString(Dt.Key), Convert.ToString(Dt.Value));
                    i++;
                }
                ReportViewer1.ServerReport.SetParameters(parm);
                ReportViewer1.ServerReport.Refresh();
            }
            //downloadpdf();

        }

        
        

    }
    public class CustomReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        // From: http://community.discountasp.net/default.aspx?f=14&m=15967
        // local variable for network credential.
        private string _UserName;
        private string _PassWord;
        private string _DomainName;
        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }
        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;  // not use ImpersonationUser
            }
        }
        public ICredentials NetworkCredentials
        {
            get
            {    // use NetworkCredentials
                return new NetworkCredential(_UserName, _PassWord, _DomainName);
            }
        }
        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
        {
            // not use FormsCredentials unless you have implements a custom autentication.
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }

    //void Popup(bool isDisplay)
    //{
    //    StringBuilder builder = new StringBuilder();
    //    if (isDisplay)
    //    {
    //        builder.Append("<script language=JavaScript> LoadDiv(); </script>\n");
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "LoadDiv", builder.ToString());
    //    }
    //    else
    //    {
    //        builder.Append("<script language=JavaScript> HidePopup(); </script>\n");
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
    //    }
    //}




    int Cnt;
    protected void lnkpending_Click(object sender, System.EventArgs e)
    {
       // ReportViewer1.Visible = false;
      //  pnlReport.Visible = false;
        TestPrintDetail();
       

        lblMsg.Text = "";
        PanelclickDetail.Visible = false;
        int docid = Convert.ToInt32(Request.QueryString["docid"].ToString());
        Session["DocType"] = Request.QueryString["docid"].ToString();
         Cnt = Convert.ToInt32(Request.QueryString["Cnt"].ToString());
        string DocType = Convert.ToString(Request.QueryString["DocType"].ToString());
        if (docid == 1 && Cnt != 0)
        {
             lblForData.Text = DocType;
            PanelclickDetail.Visible = true;
            //a1.attributes.add("bgcolor", "green");
            //lnkpending.forecolor = system.drawing.color.white;
        }
        else if (docid == 2 && Cnt != 0)
        {
            lblForData.Text = DocType;
            PanelclickDetail.Visible = true;
          //  a2.attributes.add("bgcolor", "green");
           // linkbutton1.forecolor = system.drawing.color.white;
        }
        else if (docid == 3 && Cnt != 0)
        {
            lblForData.Text = DocType;
            PanelclickDetail.Visible = true;
            //a3.attributes.add("bgcolor", "green");
            //linkbutton2.forecolor = system.drawing.color.white;
        }
        else if (docid == 4 && Cnt != 0)
        {
            lblForData.Text = DocType;
            PanelclickDetail.Visible = true;
            //a4.attributes.add("bgcolor", "green");
            //linkbutton3.forecolor = system.drawing.color.white;
        }
        else if (docid == 5 && Cnt != 0)
        {
            lblForData.Text = DocType;
            PanelclickDetail.Visible = true;
           // a5.attributes.add("bgcolor", "green");
            //linkbutton4.forecolor = system.drawing.color.white;
        }
        else if (docid == 6 && docid != 0)
        {
            lblForData.Text = DocType;
            PanelclickDetail.Visible = true;
           // a6.attributes.add("bgcolor", "green");
           // linkbutton5.forecolor = system.drawing.color.white;
        }
        else if (docid == 7 && Cnt != 0)
        {
            lblForData.Text = DocType;
            PanelclickDetail.Visible = true;
           // a7.attributes.add("bgcolor", "green");
           // linkbutton6.forecolor = system.drawing.color.white;
        }
        else if (docid == 8 && Cnt != 0)
        {
            lblForData.Text = DocType;
            TestPrintDetail();
            PanelclickDetail.Visible = true;
          //  a8.attributes.add("bgcolor", "green");
            //linkbutton7.forecolor = system.drawing.color.white;
        }
        else if (docid == 9 && Cnt != 0)
        {
            TestPrintDetail();
            lblForData.Text = DocType;

            PanelclickDetail.Visible = true;
          //  a9.attributes.add("bgcolor", "green");
           // linkbutton8.forecolor = system.drawing.color.white;
        }
        else if (docid == 10 && Cnt != 0)
        {
            TestPrintDetail();
            lblForData.Text = DocType;

            PanelclickDetail.Visible = true;
          //  a10.attributes.add("bgcolor", "green");
           // linkbutton9.forecolor = system.drawing.color.white;
        }
        else if (docid == 11 && Cnt != 0)
        {
            lblForData.Text = DocType;
            TestPrintDetail();
            PanelclickDetail.Visible = true;
            //a11.attributes.add("bgcolor", "green");
           // linkbutton10.forecolor = system.drawing.color.white;
        }
        else
        {
            lblForData.Text = "";
            PanelclickDetail.Visible = false;
            lblMsg.Text = "no record available for printing.";
        }

    }
    protected void GridDocumentType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
          LinkButton btEdit = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)btEdit.NamingContainer;
              int  rIndex = gvRow.RowIndex;
            
                LinkButton lbEdit = (LinkButton)GridDocumentType.Rows[rIndex].FindControl("linkbtnEdit");
                //GridMatrix.Rows[1].Cells[2].Enabled = false;
                if (Cnt != 0)
                {

                    GridDocumentType.Rows[rIndex].BackColor = Color.LightGreen;
                   // EnabledDisabledCheckbox(rIndex, true);
                }
    }
}

