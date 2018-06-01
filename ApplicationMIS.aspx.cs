using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmApplicationStatus : System.Web.UI.Page
{
    PlStudentDetail objpl = new PlStudentDetail();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtfromdate.Text = Convert.ToString(DateTime.Now.Date);
            //txttodate.Text = Convert.ToString(DateTime.Now.Date);
            //ddlfor.SelectedIndex = 1;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (txtfromdate.Text != "" && txtfromdate.Text != null  )
        {
            if (txttodate.Text != "" && txttodate.Text != null)
            {
                if (Convert.ToDateTime(txttodate.Text.Substring(6, 4) + "/" + txttodate.Text.Substring(3, 2) + "/" + txttodate.Text.Substring(0, 2)) <= DateTime.Now.Date && Convert.ToDateTime(txtfromdate.Text.Substring(6, 4) + "/" + txtfromdate.Text.Substring(3, 2) + "/" + txtfromdate.Text.Substring(0, 2)) <= DateTime.Now.Date)
                {
                    if (Convert.ToDateTime(txttodate.Text.Substring(6, 4) + "/" + txttodate.Text.Substring(3, 2) + "/" + txttodate.Text.Substring(0, 2)) > Convert.ToDateTime("01/01/2016") && Convert.ToDateTime(txtfromdate.Text.Substring(6, 4) + "/" + txtfromdate.Text.Substring(3, 2) + "/" + txtfromdate.Text.Substring(0, 2)) > Convert.ToDateTime("01/01/2016"))
                    {
                        if (Convert.ToDateTime(txtfromdate.Text) <= Convert.ToDateTime(txttodate.Text))
                        {
                            
                                grdListApplication.DataSource = null;
                                grdListApplication.DataBind();
                                lblmsg.Text = "";
                                objpl.Ind = 22;
                                //objpl.FromDate = DateTime.Now.Date;
                                objpl.FromDate = txtfromdate.Text.Substring(6, 4) + "/" + txtfromdate.Text.Substring(3, 2) + "/" + txtfromdate.Text.Substring(0, 2);
                                //objpl.ToDate = DateTime.Now.Date;
                                objpl.ToDate = txttodate.Text.Substring(6, 4) + "/" + txttodate.Text.Substring(3, 2) + "/" + txttodate.Text.Substring(0, 2);
                                objpl.LevelID = Convert.ToInt32(Session["UserTypeId"]);
                                objpl.UserID = Convert.ToInt32(Session["UserId"]);
                          
                                HttpClient HClient = new HttpClient();
                                HClient.BaseAddress = new Uri(DataAcces.Url);
                                HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var uri = string.Format("api/StudentDetail/GetApplicationMIS/?ind={0}&FromDate={1}&ToDate={2}&LevelID={3}&UserID={4}", objpl.Ind, objpl.FromDate, objpl.ToDate, objpl.LevelID, objpl.UserID);

                                var response = HClient.GetAsync(uri).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    var getdata = response.Content.ReadAsAsync<DataTable>().Result;
                                    if (getdata.Rows.Count > 0)
                                    {
                                        grdListApplication.DataSource = getdata;
                                        grdListApplication.DataBind();
                                    }
                                    else
                                        lblmsg.Text = "Records Not Available";
                                    getdata = null;
                                }
                                response = null; uri = null;
                        }
                        else
                            lblmsg.Text = "From Date Must be less than To Date";
                    }
                    else
                        lblmsg.Text = "Date must not be less than 01/01/2016.";
                }
                else
                    lblmsg.Text = "Date must not be Greater than Today Date";
            }
            else
                lblmsg.Text = "Please Select To Date ";
        }
        else
            lblmsg.Text = "Please Select From Date ";


    }
    protected void btnshow_Click1(object sender, EventArgs e)
    {

    }
    protected void grdListApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowStatus")
        {
            Response.Redirect("FrmApplicationProgress.aspx?CallID=3&ApplicationID=" + e.CommandArgument);
        }
    }
    public void clear()
    {
        txtfromdate.Text = txttodate.Text = ""; 
        grdListApplication.DataSource = null;
        grdListApplication.DataBind();
        txtfromdate.Focus();
        lblmsg.Text = "";
    }
    protected void linkClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void gridtoexcel_Click(object sender, EventArgs e)
    {
        if (grdListApplication.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";

            // grdListApplication.Columns.RemoveAt(5);
            string FileName = "SGBAU_Application_MIS_From_" + txtfromdate.Text + " _To_ " + txttodate.Text + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

            grdListApplication.GridLines = GridLines.Both;
            grdListApplication.HeaderStyle.Font.Bold = true;
            grdListApplication.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        else
            lblmsg.Text = "Data Not Found.";
    }
   
}