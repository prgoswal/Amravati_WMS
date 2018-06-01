using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
public partial class FrmImageInsertion : System.Web.UI.Page
{
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;
    DataRow dr;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["dtInSession"] = null;
        }
        dt = new DataTable();
        dt.Columns.Add("Image");
        dt.Columns.Add("ImageNo");
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {

    }
    protected void linkAdd_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lblDiffrImg.Text) != 0)
        {

            dt=(DataTable)Session["dtInSession"];
            int totimgDiffr=Convert.ToInt32(lblDiffrImg.Text);
            if (dt.Rows.Count <= totimgDiffr)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string fileNm = ""; ;
                    if (Session["dtInSession"] != null)
                        dt = (DataTable)Session["dtInSession"]; //Getting datatable from session
                    string fileName = "";
                    if (FileUpload1.HasFile)
                    {
                        fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                        string ext = Path.GetExtension(fileName);
                        if (ext != ".JPG")
                        {
                            lblMsg.Text = "Image Extention is Not Valid Plz Select .JPG File";
                            return;
                        }
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/InsertImages/") + fileName);
                    }
                    else
                    {
                        lblMsg.Text = "Please Select Image First";
                        return;
                    }
                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/InsertImages/"));

                    List<ListItem> files = new List<ListItem>();
                    foreach (string filePath in filePaths)
                    {
                        fileNm = Path.GetFileName(filePath);
                        if (fileName == fileNm)
                            files.Add(new ListItem(fileNm, "~/InsertImages/" + fileNm));
                    }

                    dr = dt.NewRow();
                    dr["Image"] = "InsertImages/" + fileName;
                    dr["ImageNo"] = txtPageNo.Text;
                    dt.Rows.Add(dr);
                    Session["dtInSession"] = dt;     //Saving Datatable To Session 
                    grdImagedetails.DataSource = dt;
                    grdImagedetails.DataBind();
                    txtLotNo.Enabled = false;
                    txtImageNo.Enabled = false;
                    txtPageNo.Text = "";
                }
            }
        }
        else
            lblMsg.Text = "Not Difference Of Images";

    }
    protected void linkCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    public void masterTblRegImgsCount()
    {

        SqlCommand  cmd1 = new SqlCommand("SPInsertImages", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@Ind", 1);
        cmd1.Parameters.AddWithValue("@LotNo", txtLotNo.Text);
        con.Open();
        int i=Convert.ToInt32(cmd1.ExecuteScalar());
        con.Close();
        //if (i > 0)
        lblTotalLotImages.Text = i.ToString();

    }
    public void FolderImgsCount()
    {
        SqlCommand cmd1 = new SqlCommand("SPInsertImages", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@Ind", 2);
        cmd1.Parameters.AddWithValue("@LotNo", txtLotNo.Text);
        con.Open();
        int i = Convert.ToInt32(cmd1.ExecuteScalar());
        con.Close();
        //if (i > 0)
        lblTotalFolderImage.Text = i.ToString();
    }
    protected void linkSave_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lblDiffrImg.Text) != 0 )
        {
            dt=(DataTable)Session["dtInSession"];
            int totimgDiffr=Convert.ToInt32(lblDiffrImg.Text);
            if(dt.Rows.Count<=totimgDiffr)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    cmd = new SqlCommand("SPInstertImages", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ind", 4);
                    //cmd.Parameters.AddWithValue("@Ind", 4);
                   

                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        lblMsg.Text = "Images Send Successfully";
                    Clear();
                }
                   
                
            }
            else
                lblMsg.Text = "Please You Can Add Maximum "+totimgDiffr+" Images";
            
        }
        else
            lblMsg.Text = "Not Difference Images";
       
    }
    protected void grdImagedetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ShowPopup")
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["dtInSession"];
            LinkButton btndetails = (LinkButton)e.CommandSource;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            int rwindex = gvrow.RowIndex;


            ImageSelected.ImageUrl = dt1.Rows[rwindex]["Image"].ToString();

            Popup(true);
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

    protected void LinkClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    public void Clear()
    {
        txtLotNo.Enabled = true;
        txtLotNo.Text = "";
        txtImageNo.Enabled = true;
        txtImageNo.Text = "";
        txtPageNo.Text = "";
    }
    protected void txtLotNo_TextChanged(object sender, EventArgs e)
    {
        masterTblRegImgsCount();
        FolderImgsCount();
        lblDiffrImg.Text = (Convert.ToInt32(lblTotalLotImages.Text) - Convert.ToInt32(lblTotalFolderImage.Text)).ToString();
    }
}