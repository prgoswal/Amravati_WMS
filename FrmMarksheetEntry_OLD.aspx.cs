using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmMarksheetEntry : System.Web.UI.Page
{
    BlStudentDetail BlStudentDetail = new BlStudentDetail();
    PlStudentDetail objplstudetail = new PlStudentDetail();
    PL_SecondLevel objplsecondlevel = new PL_SecondLevel();
    DataTable dt;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    List<PlStudentDetail> obj = new List<PlStudentDetail>();
    PlStudentDetail objplstudentDetail = new PlStudentDetail();
    int j = 0;
    PL_SecondLevel objPlSecondLevel = new PL_SecondLevel();
    static int usertypeid = 0;
    static string UserId = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            hiddenfield.Value = DataAcces.hiddenURL;
            if (Session["UserId"] != null)
                UserId = Convert.ToString(Session["UserId"]);

            if (Session["UserTypeId"] != null)
                usertypeid = Convert.ToInt32(Session["UserTypeId"]);

            GetStudentDetail();

            pnlstudentdetail.Visible = true;
            pnlSubject.Visible = false;
            PnlResult.Visible = false;
            pnlFinalSub.Visible = false;

            btnstudetail.Enabled = true;
            btnsubject.Enabled = false;
            btnresult.Enabled = false;
            btnfinal.Enabled = false;
            //GetAppDetails();//Calling funtion for load data according to users.
        }
    }


    public void GetStudentDetail()
    {
        dt = new DataTable();
        dt = (DataTable)Session["PendingPopup"];
        int rwindex = Convert.ToInt32(Session["gvrowIndex"]);
        if (dt.Rows.Count > 0)
        {
            ImgCF.ImageUrl = dt.Rows[0]["PartBImagePath"].ToString();
            ImageSelected.ImageUrl = dt.Rows[0]["PartAImagePath"].ToString();

            // lblYeaeSession.Text = "Exam Year - " +dt.Rows[rwindex]["Examsession"].ToString();
            lblstuname.Text = "STUDENT NAME -" + dt.Rows[rwindex]["StudentName"].ToString() + " , ";
            lblapplicationnumber.Text = " APPLICATION NO. - " + dt.Rows[rwindex]["ApplicationID"].ToString() + " , ";
            lblappliedfor.Text = " APPLIED FOR" + dt.Rows[rwindex]["AppliedFor"].ToString();
            lblroll.Text = " ROLL NO." + dt.Rows[rwindex]["RollNo"].ToString() + " , ";
            txtStudentName.Text = dt.Rows[rwindex]["StudentName"].ToString();
            txtStudentRoll.Text = dt.Rows[rwindex]["RollNo"].ToString();
            Popup(true);
            return;
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

    //protected void btnOk_Click(object sender, EventArgs e)
    //{
    //    if (btnstudetail.Enabled == true && btnsubject.Enabled==false && btnresult.Enabled==false && btnfinal.Enabled==false)
    //    {
    //       
            
    //    }
    //    else if (btnstudetail.Enabled == false && btnsubject.Enabled == true && btnresult.Enabled == false && btnfinal.Enabled == false)
    //    {
    //     
    //    }
    //    else if (btnstudetail.Enabled == false && btnsubject.Enabled == false && btnresult.Enabled == true && btnfinal.Enabled == false)
    //    {
    //        pnlFinalSub.Visible = true;
    //        PnlResult.Visible = btnresult.Enabled = false;
    //        btnfinal.Enabled = true;
    //    }
    //    else if (btnstudetail.Enabled == false && btnsubject.Enabled == false && btnresult.Enabled == false && btnfinal.Enabled == true)
    //    {
    //        btnOk.Visible = false;
    //    }

    //}
    protected void btnstudentdetailok_Click(object sender, EventArgs e)
    {
         pnlSubject.Visible = true;
         pnlstudentdetail.Visible = false;
         pnlFinalSub.Visible = false;
         PnlResult.Visible = false;

         btnsubject.Enabled = true;
         btnstudetail.Enabled = true;
     
    }
    protected void btnsubjectOK_Click(object sender, EventArgs e)
    {

        pnlSubject.Visible = false;
        pnlstudentdetail.Visible = false;
        pnlFinalSub.Visible = false;
        PnlResult.Visible = true;


        btnsubject.Enabled = true;
        btnresult.Enabled = true;
    }
    protected void btnresultok_Click(object sender, EventArgs e)
    {
        pnlSubject.Visible = false;
        pnlstudentdetail.Visible = false;
        pnlFinalSub.Visible = true;
        PnlResult.Visible = false;

        btnstudetail.Enabled = true;
        btnsubject.Enabled = true;
        btnresult.Enabled = true;
        btnfinal.Enabled = true;
    }
    protected void btnFinalSub_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnstudetail_Click(object sender, EventArgs e)
    {
        pnlSubject.Visible = false;
        pnlstudentdetail.Visible = true;
        pnlFinalSub.Visible = false;
        PnlResult.Visible = false;
    }
    protected void btnsubject_Click(object sender, EventArgs e)
    {
        pnlSubject.Visible = true;
        pnlstudentdetail.Visible = false;
        pnlFinalSub.Visible = false;
        PnlResult.Visible = false;
    }
    protected void btnresult_Click(object sender, EventArgs e)
    {
        pnlSubject.Visible = false;
        pnlstudentdetail.Visible = false;
        pnlFinalSub.Visible = false;
        PnlResult.Visible = true;
    }
    protected void btnfinal_Click(object sender, EventArgs e)
    {
        pnlSubject.Visible = false;
        pnlstudentdetail.Visible = false;
        pnlFinalSub.Visible = true;
        PnlResult.Visible = false;
    }
    //protected void lnkhome_Click(object sender, EventArgs e)
    //{
       
    //    Server.Transfer("FrmZeroLevel.aspx");
    //   // Response.Redirect();
    //}
}