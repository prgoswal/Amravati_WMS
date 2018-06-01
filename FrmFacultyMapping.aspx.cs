using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmFacultyMapping : System.Web.UI.Page
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

    protected void btnShowFaculty_Click(object sender, EventArgs e)
    {
        try
        {
            plFu.ExamSession = ddlSession.SelectedItem.Text;
            plFu.ExamYear = Convert.ToInt32(ddlYear.SelectedItem.Text);
            DataSet ds = dlFu.SoftAndOccplBind(plFu);

            gvSoftDataFaculty.DataSource = ViewState["SoftData"] = ds.Tables[0];
            gvSoftDataFaculty.DataBind();

            gvOccPlFaculty.DataSource = ViewState["OccPl"] = ds.Tables[1];
            gvOccPlFaculty.DataBind();
            divhidden.Visible = true;
        }
        finally
        {
            ViewState["MappedTable"] = null;
            gvMappedData.DataSource = null;
            gvMappedData.DataBind();
        }
    }
    protected void btnMapped_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["MappedTable"];

        if (dt == null)
        {
            dt = new DataTable();
            dt.Columns.Add("ExamFacultyCD", typeof(int));
            dt.Columns.Add("SoftData", typeof(string));
            dt.Columns.Add("OccPl", typeof(string));
            //dt.Columns.Add("CBUnMapped", typeof(bool)).ReadOnly = true;
        }

        DataRow dr = dt.NewRow();
        bool softCheck = false, OccplCheck = false;
        foreach (GridViewRow item in gvSoftDataFaculty.Rows)
        {
            CheckBox CB = (CheckBox)item.FindControl("CBSoftDataFaculty");
            if (CB.Checked == true)
            {
                DataTable dtSoft = (DataTable)ViewState["SoftData"];
                dr["SoftData"] = dtSoft.Rows[item.RowIndex]["ExamFaculty"]; //gvSoftDataFaculty.Rows[item.RowIndex].Cells[1].Text.Replace("&amp;", "&");
                dtSoft.Rows.RemoveAt(item.RowIndex);
                gvSoftDataFaculty.DataSource = dtSoft;
                gvSoftDataFaculty.DataBind();
                //gvSoftDataFaculty.Rows[item.RowIndex].Attributes.Add("class", "bg-primary");
                softCheck = true;
                break;
            }
            softCheck = false;
        }

        if (softCheck == false)
        {
            lblMsg.Text = "Please Select Soft Data Faculty!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
            return;
        }

        foreach (GridViewRow item in gvOccPlFaculty.Rows)
        {
            CheckBox CB = (CheckBox)item.FindControl("CBOccPlFaculty");
            if (CB.Checked == true)
            {
                dr["ExamFacultyCD"] = Convert.ToInt32(gvOccPlFaculty.Rows[item.RowIndex].Cells[0].Text);
                dr["OccPl"] = gvOccPlFaculty.Rows[item.RowIndex].Cells[2].Text;
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

        dt.Rows.Add(dr);
        ViewState["MappedTable"] = dt;
        gvMappedData.DataSource = dt;
        gvMappedData.DataBind();
    }
    protected void btnUnMapped_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["MappedTable"];
        if (dt == null)
        {
            lblMsg.Text = "Please Mapping First Faculty!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
            return;
        }

        int pos = 0; bool unMappCheck = false;
        foreach (GridViewRow item in gvMappedData.Rows)
        {
            CheckBox CB = (CheckBox)item.FindControl("CBUnMapped");
            if (CB.Checked == true)
            {
                DataTable dtSoft = (DataTable)ViewState["SoftData"];
                DataRow drSoft = dtSoft.NewRow();
                drSoft["ExamFacultyCD"] = "0";
                drSoft["ExamFaculty"] = dt.Rows[pos]["SoftData"];
                dtSoft.Rows.Add(drSoft);
                gvSoftDataFaculty.DataSource = dtSoft;
                gvSoftDataFaculty.DataBind();
                ViewState["SoftData"] = dtSoft;
                dt.Rows.RemoveAt(pos);
                unMappCheck = true;
            }
            else
            {
                pos++;
            }
        }
        if (unMappCheck == false)
        {
            lblMsg.Text = "Please Select For Unmapping Faculty!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorModal", "errorModal()", true);
            return;
        }
        gvMappedData.DataSource = dt;
        gvMappedData.DataBind();
    }
    protected void CBSoftDataFaculty_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox CheckedCB = sender as CheckBox;
        foreach (GridViewRow item in gvSoftDataFaculty.Rows)
        {
            CheckBox CB = (CheckBox)item.FindControl("CBSoftDataFaculty");
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
        foreach (GridViewRow item in gvOccPlFaculty.Rows)
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
    void AllClear()
    {
        gvMappedData.DataSource = null;
        gvOccPlFaculty.DataSource = null;
        gvSoftDataFaculty.DataSource = null;
        gvMappedData.DataBind();
        gvOccPlFaculty.DataBind();
        gvSoftDataFaculty.DataBind();
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