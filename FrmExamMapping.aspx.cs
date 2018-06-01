using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmExamMapping : System.Web.UI.Page
{
    DLFileUpload dlFu = new DLFileUpload();
    PlUpload plFu = new PlUpload();
    DataSet dsFaculty = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ExamBinding(); //Session & Year Binding.
            ViewState["MappedTable"] = null;
        }
    }

    void ExamBinding()
    {
        ddlYear.DataSource = dlFu.GetExamName(plFu);
        ddlYear.DataTextField = "ItemDesc";
        ddlYear.DataValueField = "ItemID";
        ddlYear.DataBind();
        ddlYear.Items.Insert(0, "-- Select Year --");

        ddlSession.DataSource = dlFu.GetSession(plFu);
        ddlSession.DataTextField = "ItemDesc";
        ddlSession.DataValueField = "ItemID";
        ddlSession.DataBind();
        ddlSession.Items.Insert(0, "-- Select Session --");
    }
    protected void btnShowExams_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex == 0 )
            {
                lblMsg.Text = "Please Select Exam Session!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
            }
            if (ddlYear.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select Exam Year!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
            }
            plFu.ExamSession = ddlSession.SelectedItem.Text;
            plFu.ExamYear = Convert.ToInt32(ddlYear.SelectedItem.Text);
            DataSet ds = dlFu.UniversityAndOccplBind(plFu);

            gvUniversity.DataSource = ViewState["UniversityData"] = ds.Tables[0];
            gvUniversity.DataBind();

            gvOccPlExam.DataSource = ViewState["OccPl"] = ds.Tables[1];
            gvOccPlExam.DataBind();
            divhidden.Visible = true;
        }
        catch(Exception ex)
        {
            lblMsg.Text = ex.Message;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
        }
        finally
        {
            ViewState["MappedTable"] = null;
            gvMappedData.DataSource = null;
            gvMappedData.DataBind();
            lblMsg.Text = "";
        }
    }
    protected void btnMapped_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["MappedTable"];

        if (dt == null)
        {
            dt = new DataTable();
            dt.Columns.Add("ExamFacultyCD", typeof(int));
            dt.Columns.Add("UniversityData", typeof(string));
            dt.Columns.Add("ExamCtrl", typeof(int));
            dt.Columns.Add("ExamNameUniv", typeof(string));

            dt.Columns.Add("FacultyCD", typeof(int));
            dt.Columns.Add("OccPl", typeof(string));
            dt.Columns.Add("ExamCD", typeof(int));
            dt.Columns.Add("ExamNameOCCPL", typeof(string));
            //dt.Columns.Add("CBUnMapped", typeof(bool)).ReadOnly = true;
        }

        DataRow dr = dt.NewRow();
        bool softCheck = false, OccplCheck = false;
        foreach (GridViewRow item in gvUniversity.Rows)
        {
            CheckBox CB = (CheckBox)item.FindControl("CBUniversity");
            if (CB.Checked == true)
            {
                DataTable dtSoft = (DataTable)ViewState["UniversityData"];
                dr["ExamFacultyCD"] = dtSoft.Rows[item.RowIndex]["ExamFacultyCD"];
                dr["UniversityData"] = dtSoft.Rows[item.RowIndex]["ExamFaculty"];
                dr["ExamCtrl"] = dtSoft.Rows[item.RowIndex]["ExamCtrl"];
                dr["ExamNameUniv"] = dtSoft.Rows[item.RowIndex]["ExamName"]; 
                dtSoft.Rows.RemoveAt(item.RowIndex);
                gvUniversity.DataSource = ViewState["UniversityData"] = dtSoft;
                gvUniversity.DataBind();
                softCheck = true;
                break;
            }
            softCheck = false;
        }

        if (softCheck == false)
        {
            lblMsg.Text = "Please Select University Data!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
            return;
        }

        foreach (GridViewRow item in gvOccPlExam.Rows)
        {
            CheckBox CB = (CheckBox)item.FindControl("CBOccPlFaculty");
            if (CB.Checked == true)
            {
                DataTable dtOccpl = (DataTable)ViewState["OccPl"];
                dr["ExamFacultyCD"] = dtOccpl.Rows[item.RowIndex]["FacultyCD"];   //Convert.ToInt32(gvOccPlExam.Rows[item.RowIndex].Cells[0].Text);
                dr["OccPl"] = dtOccpl.Rows[item.RowIndex]["Faculty"];//gvOccPlExam.Rows[item.RowIndex].Cells[2].Text;
                dr["ExamCD"] = dtOccpl.Rows[item.RowIndex]["ExamCD"];
                dr["ExamNameOCCPL"] = dtOccpl.Rows[item.RowIndex]["ExamName"]; 
                dtOccpl.Rows.RemoveAt(item.RowIndex);
                gvOccPlExam.DataSource = ViewState["OccPl"] = dtOccpl;
                gvOccPlExam.DataBind();
                OccplCheck = true;                
                break;
            }
        }

        if (OccplCheck == false)
        {
            lblMsg.Text = "Please Select OCCPL Faculty!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
            return;
        }
        divMapp.Visible = true;
        dt.Rows.Add(dr);
        ViewState["MappedTable"] = dt;
        gvMappedData.DataSource = dt;
        gvMappedData.DataBind();
    }
    protected void CBUniversity_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox CheckedCB = sender as CheckBox;
        foreach (GridViewRow item in gvUniversity.Rows)
        {
            CheckBox CB = (CheckBox)item.FindControl("CBUniversity");
            if (CB == CheckedCB)
            {
                CB.Checked = true;
                continue;
            }
            CB.Checked = false;
        }
    }
    protected void CBOccPlFaculty_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox CheckedCB = sender as CheckBox;
        foreach (GridViewRow item in gvOccPlExam.Rows)
        {
            CheckBox CB = (CheckBox)item.FindControl("CBOccPlFaculty");
            if (CB == CheckedCB)
            {
                CB.Checked = true;
                continue;
            }
            CB.Checked = false;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        AllClear();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void cbShowSearchbox_CheckedChanged(object sender, EventArgs e)
    {
        if (cbShowSearchbox.Checked)
        {
           divDataFind.Visible = true;
           return;
        }
        divDataFind.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //CheckBox CheckedCB = sender as CheckBox;
        foreach (GridViewRow item in gvOccPlExam.Rows)
        {
            CheckBox CB = (CheckBox)item.FindControl("CBOccPlFaculty");
            if (CB.Checked == true)
            {
                lblExamName.Text = gvOccPlExam.Rows[item.RowIndex].Cells[3].Text; // item.Cells[2].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "modalRollNo", "modalRollNo()", true);
                return;
            }
        }
        lblMsg.Text = "Please Select Exam First!";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
    }
    protected void btnMappedbyRoll_Click(object sender, EventArgs e)
    {

    }
    protected void gvMappedData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "UnMap")
        {
            DataTable dt = (DataTable)ViewState["MappedTable"];
            if (dt == null)
            {
                lblMsg.Text = "Please Mapping First Faculty!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
                return;
            }
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            int index = row.RowIndex;
            GridViewRow selectedRow = gvOccPlExam.Rows[index];


            int pos = 0;
            DataTable dtSoft = (DataTable)ViewState["UniversityData"];
            DataRow drSoft = dtSoft.NewRow();
            drSoft["ExamFacultyCD"] = dt.Rows[index]["ExamFacultyCD"];
            drSoft["ExamFaculty"] = dt.Rows[index]["UniversityData"];
            drSoft["ExamCtrl"] = dt.Rows[index]["ExamCtrl"];
            drSoft["ExamName"] = dt.Rows[index]["ExamNameOCCPL"];
            dtSoft.Rows.Add(drSoft);
            gvUniversity.DataSource = ViewState["UniversityData"] = dtSoft;
            gvUniversity.DataBind();

            DataTable dtOcc = (DataTable)ViewState["OccPl"];
            DataRow drOcc = dtOcc.NewRow();
            drOcc["FacultyCD"] = dt.Rows[index]["ExamFacultyCD"];
            drOcc["Faculty"] = dt.Rows[index]["OccPl"];
            drOcc["ExamCD"] = dt.Rows[index]["ExamCD"];
            drOcc["ExamName"] = dt.Rows[index]["ExamNameOCCPL"];
            dtOcc.Rows.Add(drOcc);
            gvOccPlExam.DataSource = ViewState["OccPl"] = dtOcc;
            gvOccPlExam.DataBind();
            dt.Rows.RemoveAt(pos);
           
            gvMappedData.DataSource = dt;
            gvMappedData.DataBind();
        }
    }
    protected void btnFindData_Click(object sender, EventArgs e)
    {
        if (gvUniversity.Rows.Count > 0 && gvOccPlExam.Rows.Count > 0)
        {
            AllVisible();
            if (rbSearch.SelectedValue == "rbBoth")
            {
                ContainsOccPl();
                ContainsUniversity();
            }
            else if (rbSearch.SelectedValue == "rbOccPl") ContainsOccPl();

            else if (rbSearch.SelectedValue == "rbUniversity") ContainsUniversity();
        }
        
    }

    void ContainsOccPl()
    {
        foreach (GridViewRow item in gvOccPlExam.Rows)
        {
            if (item.Cells[2].Text.ToUpper().Contains(txtSearchByName.Text.ToUpper()))
                gvOccPlExam.Rows[item.RowIndex].Cells[2].BackColor = Color.Yellow;
            else
                gvOccPlExam.Rows[item.RowIndex].Visible = false;
        }
    }
    void ContainsUniversity()
    {
        foreach (GridViewRow item in gvUniversity.Rows)
        {
            if (item.Cells[2].Text.ToUpper().Contains(txtSearchByName.Text.ToUpper()))
                gvUniversity.Rows[item.RowIndex].Cells[2].BackColor = Color.Yellow;
            else
                gvUniversity.Rows[item.RowIndex].Visible = false;
        }
    }
    void AllVisible()
    {
        foreach (GridViewRow item in gvUniversity.Rows)
        {
            gvUniversity.Rows[item.RowIndex].Visible = true;
            gvUniversity.Rows[item.RowIndex].Cells[2].BackColor = Color.Transparent;
        }

        foreach (GridViewRow item in gvOccPlExam.Rows)
        {
            gvOccPlExam.Rows[item.RowIndex].Visible = true;
            gvOccPlExam.Rows[item.RowIndex].Cells[2].BackColor = Color.Transparent;
        }
    }
    void AllClear()
    {
        gvMappedData.DataSource = null;
        gvOccPlExam.DataSource = null;
        gvUniversity.DataSource = null;
        gvMappedData.DataBind();
        gvOccPlExam.DataBind();
        gvUniversity.DataBind();
        ViewState["MappedTable"] = null;
        divhidden.Visible = false;
    }
    bool Validation(string i)
    {
        if (i == "btnShow")
        {
            if (ddlSession.SelectedIndex > 0 && ddlYear.SelectedIndex > 0)
                return false;

            return true;
        }
        return false;
    }
}
    //protected void btnUnMapped_Click(object sender, EventArgs e)
    //{
    //    DataTable dt = (DataTable)ViewState["MappedTable"];
    //    if (dt == null)
    //    {
    //        lblMsg.Text = "Please Mapping First Faculty!";
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
    //        return;
    //    }

    //    int pos = 0; bool unMappCheck = false;
    //    foreach (GridViewRow item in gvMappedData.Rows)
    //    {
    //        CheckBox CB = (CheckBox)item.FindControl("CBUnMapped");
    //        if (CB.Checked == true)
    //        {
    //            DataTable dtSoft = (DataTable)ViewState["UniversityData"];
    //            DataRow drSoft = dtSoft.NewRow();
    //            drSoft["ExamFacultyCD"] = dt.Rows[pos]["ExamFacultyCD"];
    //            drSoft["ExamFaculty"] = dt.Rows[pos]["UniversityData"];
    //            drSoft["ExamCtrl"] = dt.Rows[pos]["ExamCtrl"];
    //            drSoft["ExamName"] = dt.Rows[pos]["ExamNameOCCPL"];
    //            dtSoft.Rows.Add(drSoft);
    //            gvUniversity.DataSource = ViewState["UniversityData"] = dtSoft;
    //            gvUniversity.DataBind();

    //            DataTable dtOcc = (DataTable)ViewState["OccPl"];
    //            DataRow drOcc = dtOcc.NewRow();
    //            drOcc["FacultyCD"] = dt.Rows[pos]["ExamFacultyCD"];
    //            drOcc["Faculty"] = dt.Rows[pos]["OccPl"];
    //            drOcc["ExamCD"] = dt.Rows[pos]["ExamCD"];
    //            drOcc["ExamName"] = dt.Rows[pos]["ExamNameOCCPL"];
    //            dtOcc.Rows.Add(drOcc);
    //            gvOccPlExam.DataSource = ViewState["OccPl"] = dtOcc;
    //            gvOccPlExam.DataBind();

    //            dt.Rows.RemoveAt(pos);
    //            unMappCheck = true;
    //        }
    //        else
    //        {
    //            pos++;
    //        }
    //    }
    //    if (unMappCheck == false)
    //    {
    //        lblMsg.Text = "Please Select For Unmapping Faculty!";
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
    //        return;
    //    }
    //    gvMappedData.DataSource = dt;
    //    gvMappedData.DataBind();
    //}