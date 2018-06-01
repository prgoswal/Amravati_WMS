using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
public partial class FrmProfileCreation : System.Web.UI.Page
{
    HttpClient HClient = new HttpClient();
    PL_DocumentApprovalLevel objpl = new PL_DocumentApprovalLevel();
    PL_DocumentApprovalLevel objpldocumentProfile = new PL_DocumentApprovalLevel();
    static int ItemIdMenu = 0;
    static int ItemIdUsertype = 0;
    static int lngid = 0;
    DataTable dt;//getLevel,getmenu;
    static DataTable getLevel, getmenu, allotedTable;
    DataRow dr;
    public static int editsavecheck = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            editsavecheck = 0;
            BindUserProfile();//For User Profile Purpose
            BindDocumentMenu();//For User Menu Purpose
            Matrix();//For User Profile & Menu (Both) Diplayed Into Grid Purpose - With Checkbox           
            ShowProfile();

            string s = GridMatrix.HeaderRow.Cells[3].Text.ToString().Replace("DEGREE CERTIFICATE", "DEGREE");

        }
    }

    class LinkColumn : ITemplate
    {
        public void InstantiateIn(System.Web.UI.Control container)
        {
            LinkButton link = new LinkButton();
            link.ID = "Edit";
            container.Controls.Add(link);
        }
    }

    public void Matrix()
    {
        try
        {
            dt = new DataTable();
            dt = getLevel.Copy();

            for (int j = 0; j < getmenu.Rows.Count; j++)
                dt.Columns.Add(new DataColumn(getmenu.Rows[j][1].ToString(), typeof(bool)));


            //dt.Columns["DEGREE CERTIFICATE"].ColumnName = "DEGREE";
            //dt.Columns["PROVISIONAL DEGREE CERTIFICATE"].ColumnName = "PROVISIONAL DEGREE";
            //dt.Columns["PASSING CERTIFICATE"].ColumnName = "PASSING";
            //dt.Columns["MERIT CERTIFICATE"].ColumnName = "MERIT";
            //dt.Columns["ATTEMPT CERTIFICATE"].ColumnName = "ATTEMPT";
            //dt.Columns["MEDAL CERTIFICATE"].ColumnName = "MEDAL";
            //dt.Columns["DUPLICATE MARKSHEET"].ColumnName = "DUPLICATE";
            //dt.Columns["MARKSHEET VERIFICATION CERTIFICATE / LETTER"].ColumnName = "MARKSHEET VERIFICATION";
            //dt.Columns["DEGREE VERIFICATION CERTIFICATE / LETTER"].ColumnName = "DEGREE VERIFICATION";
            //dt.Columns["DATE OF DECLARATION CERTIFICATE / LETTER"].ColumnName = "DATE OF DECLARATION ";
            //dt.Columns["MEDIUM OF INSTRUCTION CERTIFICATE / LETTER"].ColumnName = "MEDIUM OF INSTRUCTION";

            GridMatrix.DataSource = dt;
            GridMatrix.DataBind();

            //For Hiding Profile Level Value From Grid
            GridMatrix.HeaderRow.Cells[1].Visible = false;
            foreach (GridViewRow gvr in GridMatrix.Rows)
            {
                gvr.Cells[1].Visible = false;
                gvr.Cells[2].Width = 150;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    public DataTable BindUserProfile()
    {
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(DataAcces.Url);
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
            objpl.Ind = 16;
            var uri1 = string.Format("api/DocumentApproval/GetUserLevel/?Ind={0}", objpl.Ind);
            var response1 = HClient.GetAsync(uri1).Result;
            getLevel = null;
            if (response1.IsSuccessStatusCode)
                getLevel = response1.Content.ReadAsAsync<DataTable>().Result;
            getLevel.Columns["LevelDescription"].ColumnName = "USER PROFILE";

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
        return getLevel;
    }

    public DataTable BindDocumentMenu()
    {
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(DataAcces.Url);
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
            objpl.Ind = 15;
            var uri1 = string.Format("api/DocumentApproval/GetDocumentLevel/?Ind={0}", objpl.Ind);
            var response1 = HClient.GetAsync(uri1).Result;
            getmenu = null;
            if (response1.IsSuccessStatusCode)
                getmenu = response1.Content.ReadAsAsync<DataTable>().Result;

            //  getmenu.Rows["DEGREE CERTIFICATE"] = "DEGREE";

            // getmenurow["DEGREE CERTIFICATE"] = "";


        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
        return getmenu;
    }

    public DataTable ShowProfile()
    {
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(DataAcces.Url);
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
            objpl.Ind = 13;
            var uri1 = string.Format("api/DocumentApproval/ShowApproval/?Ind={0}", 13);
            var response1 = HClient.GetAsync(uri1).Result;
            allotedTable = null;
            if (response1.IsSuccessStatusCode)
                allotedTable = response1.Content.ReadAsAsync<DataTable>().Result;
            DataTable dt = new DataTable();
            dt = getLevel.Copy();
            Label1.Text = "";
            for (int i = 0; i < getmenu.Rows.Count; i++)
            {
                for (int j = 0; j < allotedTable.Rows.Count; j++)
                {
                    if (Convert.ToInt32(getmenu.Rows[i][0]) == Convert.ToInt32(allotedTable.Rows[j][1]))
                    {
                        for (int K = 0; K < getLevel.Rows.Count; K++)
                        {

                            if (Convert.ToInt32(getLevel.Rows[K][0]) == Convert.ToInt32(allotedTable.Rows[j][0]))
                            {
                                CheckBox chkRemove = (CheckBox)GridMatrix.Rows[K].Cells[i + 3].Controls[0];
                                chkRemove.Checked = true;
                                break;
                            }
                        }

                    }
                }


            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
        return allotedTable;
    }

    protected void linkadd_Click(object sender, EventArgs e)
    {//add new data 

        ItemIdMenu = 0;
        ItemIdUsertype = 0;
        lngid = 0;
        BindDocumentMenu();
        BindUserProfile();

    }

    int rIndex;
    protected async void GridMatrix_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int flag = 0;
            string GridMerixRowID = "";
            string ColID = "";
            Label1.Text = "";
            if (e.CommandName == "btnEdit")
            {
                LinkButton btEdit = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)btEdit.NamingContainer;
                rIndex = gvRow.RowIndex;

                //CheckBox chk = (CheckBox)GridMatrix.Rows[rIndex].FindControl("");
                LinkButton lbEdit = (LinkButton)GridMatrix.Rows[rIndex].FindControl("linkbtnEdit");
                if (editsavecheck == 0 || lbEdit.Text == "Save")
                {

                    if (lbEdit.Text != "Save" && rIndex >= 0)
                    {
                        editsavecheck = 1;//for one time one active edit button
                        GridMatrix.Rows[rIndex].BackColor = Color.Beige;
                        EnabledDisabledCheckbox(rIndex, true);
                        lbEdit.Text = "Save";

                    }
                    else if (lbEdit.Text == "Save")
                    {
                        for (int k = 0; k < getmenu.Rows.Count; k++)
                        {
                            CheckBox chkRemove = (CheckBox)GridMatrix.Rows[rIndex].Cells[k + 3].Controls[0];
                            if (chkRemove.Checked == true)
                            {
                                string tempName = GridMatrix.HeaderRow.Cells[k + 3].Text.ToString();
                                DataRow[] row = getmenu.Select("DocumentType='" + tempName + "'");
                                GridMerixRowID = gvRow.Cells[1].Text.ToString();
                                //if (ColID == "") ColID = row[0].Table.Rows[k][0].ToString(); else ColID = ColID + "," + row[0].Table.Rows[k][0].ToString();
                                ColID = row[0].Table.Rows[k][0].ToString();
                                if (flag == 0)
                                {
                                    HttpClient HClient1 = new HttpClient();
                                    HClient1.BaseAddress = new Uri(DataAcces.Url);
                                    HClient1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));


                                    objpldocumentProfile.Ind = 14;

                                    objpldocumentProfile.AllowMenu = Convert.ToInt32(GridMerixRowID);
                                    objpldocumentProfile.LevelProfile = Convert.ToInt32(ColID);


                                    var uri1 = "api/DocumentApproval/ApprovalDelete/";
                                    var response1 = HClient1.PostAsJsonAsync(uri1, objpldocumentProfile).Result;

                                    // DataTable dt = new DataTable();
                                    if (response1.IsSuccessStatusCode)
                                    {
                                        var productJsonString = await response1.Content.ReadAsStringAsync();
                                    }
                                }
                                flag = 1;
                                HttpClient HClient = new HttpClient();
                                HClient.BaseAddress = new Uri(DataAcces.Url);
                                HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
                                objpldocumentProfile.Ind = 5;
                                objpldocumentProfile.AllowMenu = Convert.ToInt32(GridMerixRowID);
                                objpldocumentProfile.LevelProfile = Convert.ToInt32(ColID);
                                objpldocumentProfile.ParentMenuId = 1;
                                var uri = "api/DocumentApproval/InsertDocumentApprovalLevel/";
                                var response = HClient.PostAsJsonAsync(uri, objpldocumentProfile).Result;
                                DataTable dt = new DataTable();

                                if (response.IsSuccessStatusCode)
                                {
                                    var productJsonString = await response.Content.ReadAsStringAsync();
                                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(productJsonString);
                                    
                                    if (dt.Rows[0][0].ToString() != "")
                                    {
                                        //lblmsg.Text = "User Already Exist";
                                    }
                                    else
                                    {
                                        // lblmsg.Text = "Profile Created Successfully";
                                    }
                                }
                            }
                        }
                        // lblMyMsg.Text = "Current Row Item-Id = " + GridMerixRowID + ", and Column-Item-ID=" + ColID + "";
                        EnabledDisabledCheckbox(rIndex, false);
                        editsavecheck = 0;
                        lbEdit.Text = "Edit";
                        foreach (GridViewRow r in GridMatrix.Rows)
                        {
                            r.BackColor = System.Drawing.Color.Transparent;
                        }
                        Label1.Text = "Document Approved Successfully";
                    }
                }
                else
                    Label1.Text = "Please Save Record Before Edit";
                //editsavecheck = 0;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }

    }

    protected void btnfinalsave_Click(object sender, EventArgs e)
    {

    }

    private void EnabledDisabledCheckbox(int gridrowindex, bool isEnabled)
    {
        for (int k = 0; k < getmenu.Rows.Count; k++)
        {
            CheckBox chk = (CheckBox)GridMatrix.Rows[gridrowindex].Cells[k + 3].Controls[0];
            if (chk != null)
                chk.Enabled = isEnabled;
        }

    }

    protected void linkbtnEdit_Click(object sender, EventArgs e)
    {

    }
    protected void linkbtnSave_Click(object sender, EventArgs e)
    {

    }

    protected void GridMatrix_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbSave = (LinkButton)GridMatrix.FindControl("linkbtnSave");
            if (lbSave != null)
            {
                lbSave.Visible = false;
            }
            else
            {
                return;
            }
        }


    }

    public class MyCustomTemplate : ITemplate
    {
        public void InstantiateIn(System.Web.UI.Control container)
        {
            CheckBox cb = new CheckBox();
            cb.ID = "chkprofile";
            cb.Text = "Action";
            container.Controls.Add(cb);
        }
    }
}