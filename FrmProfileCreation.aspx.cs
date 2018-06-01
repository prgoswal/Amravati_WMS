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
    plTrRegister objpl = new plTrRegister();
    Pl_ProfileCreation objplProfile = new Pl_ProfileCreation();
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
            BindProfile();//For User Profile Purpose
            BindMenu();//For User Menu Purpose
            Matrix();//For User Profile & Menu (Both) Diplayed Into Grid Purpose - With Checkbox          
            ShowProfile();
            //  txtuser.Enabled = false;
            // btnadd.Enabled = false;

        }
    }

    class LinkColumn : ITemplate
    {
        public void InstantiateIn(System.Web.UI.Control container)
        {
            LinkButton link = new LinkButton();
            link.ID = "EDIT";
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

            GridMatrix.DataSource = dt;
            GridMatrix.DataBind();

            //For Hiding Profile Level Value From Grid
            GridMatrix.HeaderRow.Cells[1].Visible = false;
            foreach (GridViewRow gvr in GridMatrix.Rows)
                gvr.Cells[1].Visible = false;
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    public DataTable BindProfile()
    {
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(DataAcces.Url);
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
            objpl.Ind = 16;
            var uri1 = string.Format("api/Login/GetUserLevel/?Ind={0}", objpl.Ind);
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

    public DataTable BindMenu()
    {
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(DataAcces.Url);
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
            objpl.Ind = 17;
            var uri1 = string.Format("api/Login/GetMenu/?Ind={0}", objpl.Ind);
            var response1 = HClient.GetAsync(uri1).Result;
            getmenu = null;
            if (response1.IsSuccessStatusCode)
                getmenu = response1.Content.ReadAsAsync<DataTable>().Result;
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
            objpl.Ind = 3;
            var uri1 = string.Format("api/Profile/ShowProfile/?Ind={0}", 3);
            var response1 = HClient.GetAsync(uri1).Result;
            allotedTable = null;
            if (response1.IsSuccessStatusCode)
                allotedTable = response1.Content.ReadAsAsync<DataTable>().Result;
            DataTable dt = new DataTable();
            dt = getLevel.Copy();
            label1.Text = "";
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

    }

    int rIndex;
    protected async void GridMatrix_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //try
        //{
        HttpClient HClient1 = new HttpClient();
        HClient1.BaseAddress = new Uri(DataAcces.Url);
        HClient1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));

        int flag = 0;
        string GridMerixRowID = "";
        string ColID = "";
        label1.Text = "";
        if (e.CommandName == "btnEdit")
        {
            lblmsg.Text = "";
            lblMyMsg.Text = "";
            btnadd.Enabled = false;
            LinkButton btEdit = (LinkButton)e.CommandSource;
            GridViewRow gvRow = (GridViewRow)btEdit.NamingContainer;
            rIndex = gvRow.RowIndex;
            GridMerixRowID = gvRow.Cells[1].Text.ToString();
            LinkButton lbEdit = (LinkButton)GridMatrix.Rows[rIndex].FindControl("linkbtnEdit");

            if (lbEdit.Text == "EDIT" && rIndex >= 0)
            {
                GridMatrix.Rows[rIndex].BackColor = Color.Beige;
                EnabledDisabledCheckbox(rIndex, true, Convert.ToInt32(GridMerixRowID));

                lbEdit.Text = "SAVE";
                editsavecheck = 1;
            }
            else if (lbEdit.Text == "SAVE")
            {
                if (Convert.ToInt32(GridMerixRowID) == 52)//Menu Rigths For Counter(52)
                {
                    DataRow[] itemID = getmenu.Select("ItemID='" + 61 + "'");
                    if (itemID.Count() <= 0)
                    {
                        DataRow menuRow = getmenu.NewRow();
                        menuRow["ItemID"] = 61;
                        menuRow["LevelDescription"] = "ENTRY";
                        getmenu.Rows.Add(menuRow);
                    }

                    DataView dv = new DataView(getmenu);
                    dv.RowFilter = "ItemID<>62";

                    getmenu = dv.ToTable();

                    for (int k = 0; k < getmenu.Rows.Count; k++)
                    {
                        ColID = getmenu.Rows[k]["ItemID"].ToString();

                        if (flag == 0)
                        {
                            objplProfile.Ind = 2;

                            objplProfile.UserTypeId = Convert.ToInt32(GridMerixRowID);
                            objplProfile.ItemId = Convert.ToInt32(ColID);


                            var uri1 = "api/Profile/ProfileDelete/";
                            var response1 = HClient1.PostAsJsonAsync(uri1, objplProfile).Result;

                            if (response1.IsSuccessStatusCode)
                            {
                                var productJsonString = await response1.Content.ReadAsStringAsync();
                            }
                        }
                        flag = 1;
                        objplProfile.Ind = 1;
                        objplProfile.UserTypeId = Convert.ToInt32(GridMerixRowID);
                        objplProfile.ItemId = Convert.ToInt32(ColID);
                        var uri = "api/Profile/InsertProfile/";
                        var response = HClient1.PostAsJsonAsync(uri, objplProfile).Result;
                        DataTable dt = new DataTable();
                        if (response.IsSuccessStatusCode)
                        {
                            var productJsonString = await response.Content.ReadAsStringAsync();
                            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(productJsonString);
                            if (dt.Rows[0][0].ToString() != "")
                            {
                                txtuser.Text = "";
                            }
                        }
                    }

                    for (int i = getmenu.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = getmenu.Rows[i];
                        if (Convert.ToInt32(dr["ItemID"].ToString()) == 61 || Convert.ToInt32(dr["ItemID"].ToString()) == 62)
                            dr.Delete();
                    }
                }
                else//Menu Rigths Other Then Counter(52)
                {
                    for (int k = 0; k < getmenu.Rows.Count; k++)
                    {
                        CheckBox chkRemove = (CheckBox)GridMatrix.Rows[rIndex].Cells[k + 3].Controls[0];
                        if (chkRemove.Checked == true)
                        {
                            string tempName = GridMatrix.HeaderRow.Cells[k + 3].Text.ToString().Replace("&amp;", "&");

                            DataRow[] row = getmenu.Select("LevelDescription='" + tempName + "'");

                            ColID = row[0].Table.Rows[k][0].ToString();

                            if (flag == 0)
                            {
                                objplProfile.Ind = 2;

                                objplProfile.UserTypeId = Convert.ToInt32(GridMerixRowID);
                                objplProfile.ItemId = Convert.ToInt32(ColID);


                                var uri1 = "api/Profile/ProfileDelete/";
                                var response1 = HClient1.PostAsJsonAsync(uri1, objplProfile).Result;

                                // DataTable dt = new DataTable();
                                if (response1.IsSuccessStatusCode)
                                {
                                    var productJsonString = await response1.Content.ReadAsStringAsync();

                                }
                            }
                            flag = 1;
                            objplProfile.Ind = 1;
                            objplProfile.UserTypeId = Convert.ToInt32(GridMerixRowID);
                            objplProfile.ItemId = Convert.ToInt32(ColID);
                            var uri = "api/Profile/InsertProfile/";
                            var response = HClient1.PostAsJsonAsync(uri, objplProfile).Result;
                            DataTable dt = new DataTable();
                            //  editsavecheck = 0;
                            if (response.IsSuccessStatusCode)
                            {
                                var productJsonString = await response.Content.ReadAsStringAsync();
                                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(productJsonString);
                                if (dt.Rows[0][0].ToString() != "")
                                {
                                    // lblmsg.Text = "User Already Exist";
                                    txtuser.Text = "";
                                }
                            }
                        }
                    }
                }


                // lblMyMsg.Text = "Current Row Item-Id = " + GridMerixRowID + ", and Column-Item-ID=" + ColID + "";
                EnabledDisabledCheckbox(rIndex, false, Convert.ToInt32(GridMerixRowID));
                lbEdit.Text = "EDIT";
                //This for change grid color by default
                foreach (GridViewRow r in GridMatrix.Rows)
                {
                    r.BackColor = System.Drawing.Color.Transparent;
                }
                txtuser.Text = "";
                BindProfile();//For User Profile Purpose
                BindMenu();//For User Menu Purpose
                Matrix();//For User Profile & Menu (Both) Diplayed Into Grid Purpose - With Checkbox
                //EnabledDisabledCheckbox(true);
                ShowProfile();
                txtuser.Enabled = true;
                btnadd.Enabled = true;
                editsavecheck = 0;
                label1.Text = "Menu Alloted to Selected Profile.";
            }
        }
        //}
        //catch
        //{ }
    }

    protected void btnfinalsave_Click(object sender, EventArgs e)
    {

    }

    private void EnabledDisabledCheckbox(int gridrowindex, bool isEnabled, int userTypeID)
    {
        for (int k = 1; k < getmenu.Rows.Count; k++)
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

    protected void lnkProfileName_Click(object sender, EventArgs e)
    {

    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            label1.Text = "";
            if (txtuser.Text != null && txtuser.Text != "")
            {
                HttpClient HClient = new HttpClient();
                HClient.BaseAddress = new Uri(DataAcces.Url);
                HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
                //Insert Data into mstallmaster
                objpl.Ind = 18;
                objpl.ItemDesc = txtuser.Text;
                var uri = string.Format("api/Login/InsertUserLevel/?Ind={0}&ItemDesc={1}", objpl.Ind, objpl.ItemDesc);
                var response = HClient.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var itemdata = response.Content.ReadAsAsync<DataTable>().Result;
                    if (itemdata.Rows.Count > 0)
                    {
                        lblmsg.Text = "Profile Saved";
                        BindProfile();//For User Profile Purpose
                        BindMenu();//For User Menu Purpose
                        Matrix();//For User Profile & Menu (Both) Diplayed Into Grid Purpose - With Checkbox
                        //EnabledDisabledCheckbox(true);
                        ShowProfile();

                        txtuser.Text = "";
                    }
                }
            }
            else
            { lblmsg.Text = "Please Enter Profile Name "; }

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
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