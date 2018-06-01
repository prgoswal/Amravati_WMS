using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    BlTRRegiter BlTrReg = new BlTRRegiter();
    plTrRegister plTrReg = new plTrRegister();

    BlExamMaster blExamMst = new BlExamMaster();
    PlExamMaster plExamMst = new PlExamMaster();

    SqlCommand cmd; SqlDataAdapter da; DataTable dt;
    int i, maxCtrl, CourseCode, RefCtrl; string IpAddress, CourseName;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFaculty();
            ddlTotalYr.Items.Insert(0, "Select Year/Sem");
            ddlFaculty.Focus();
            BindCourseType();
            GetLastExamMasterEntry();
        }
    }
    private DataTable BindFaculty()   // Bind Faculty Name Purpose
    {
        dt = BlTrReg.GetAllRegister(plTrReg, 1);
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "0";
        dt.Rows[0][1] = "Select Faculty";
        ddlFaculty.DataSource = dt;
        ddlFaculty.DataValueField = "ItemID";
        ddlFaculty.DataTextField = "ItemDesc";
        ddlFaculty.DataBind();
        return dt;
    }

    private DataTable BindExamType()  // Bind Exam Type  (Main,Reval etc.)
    {
        dt = BlTrReg.GetAllRegister(plTrReg, 4);
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "0";
        dt.Rows[0][1] = "Select Exam Type";
        ddlExamType.DataSource = dt;
        ddlExamType.DataValueField = "ItemID";
        ddlExamType.DataTextField = "ItemDesc";
        ddlExamType.DataBind();
        return dt;
    }

    private DataTable BindCourseType()  // Bind Course Type  (UG/PG etc.)
    {
        dt = BlTrReg.GetAllRegister(plTrReg, 7);
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "0";
        dt.Rows[0][1] = "Select Course Type";
        ddlCourseType.DataSource = dt;
        ddlCourseType.DataValueField = "ItemID";
        ddlCourseType.DataTextField = "ItemDesc";
        ddlCourseType.DataBind();
        return dt;
    }

    protected void linkSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlTotalYr.SelectedIndex > 0)
            {
                for (int j = 0; j < Convert.ToInt32(ddlTotalYr.SelectedValue); j++)
                {
                    plExamMst.Ind = 1;
                    plExamMst.UnivCode = 10;
                    plExamMst.FacultyCD = Convert.ToInt32(ddlFaculty.SelectedValue);
                    int sem = j + 1;
                    if (ddlExamType.SelectedIndex == 1)
                    {
                        plExamMst.ExamName = txtExamFullName.Text + " PART " + sem;
                        plExamMst.ExamShortName = txtExamShortName.Text + " PART " + sem;
                    }
                    else if (ddlExamType.SelectedIndex == 2)
                    {
                        plExamMst.ExamName = txtExamFullName.Text + " SEMESTER " + sem;
                        plExamMst.ExamShortName = txtExamShortName.Text + " SEMESTER " + sem;
                    }
                    plExamMst.ExamType = Convert.ToInt32(ddlExamType.SelectedValue);

                    if (ddlTotalYr.SelectedIndex > 0)
                        plExamMst.TotalYearSem = Convert.ToInt32(ddlTotalYr.SelectedValue);

                    plExamMst.NoOfYearSem = sem;

                    if (string.IsNullOrEmpty(txtCourseCode.Text))
                        plExamMst.CourseCD = 0;
                    else
                        plExamMst.CourseCD = Convert.ToInt32(txtCourseCode.Text);

                    plExamMst.CourseName = txtCourseName.Text;

                    if (string.IsNullOrEmpty(txtRefCtrl.Text))
                        plExamMst.RefCtrl = 0;
                    else
                        plExamMst.RefCtrl = Convert.ToInt32(txtRefCtrl.Text);

                    //plExamMst.IPAddress = GetIpAddress();
                    plExamMst.IPAddress = Request.UserHostAddress;

                    plExamMst.UserID = Convert.ToInt32(Session["UserId"]);
                    plExamMst.CreationDate = DateTime.Now;
                    plExamMst.GroupID = 0;
                    plExamMst.GroupSubID = 0;

                    i = blExamMst.InsertExamMaster(plExamMst);
                    if (i > 0)
                    {
                        lblMsg.Text = "Data Inserted Successfully";
                    }
                }
            }
            GetLastExamMasterEntry();
            Clear();
        }
        catch
        { }
    }
    protected void linkCancel_Click(object sender, EventArgs e)
    {
        Clear();
        lblMsg.Text = "";
    }
    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExamType.SelectedIndex == 1)
        {
            ddlTotalYr.Items.Clear();
            ddlTotalYr.Items.Insert(0, "Select Year");
            ddlTotalYr.Items.Insert(1, "1");
            ddlTotalYr.Items.Insert(2, "2");
            ddlTotalYr.Items.Insert(3, "3");
            ddlTotalYr.Items.Insert(4, "4");
            ddlTotalYr.Items.Insert(5, "5");
            ddlExamType.Focus();

        }
        else if (ddlExamType.SelectedIndex == 2)
        {
            ddlTotalYr.Items.Clear();
            ddlTotalYr.Items.Insert(0, "Select Sem");
            ddlTotalYr.Items.Insert(1, "1");
            ddlTotalYr.Items.Insert(2, "2");
            ddlTotalYr.Items.Insert(3, "3");
            ddlTotalYr.Items.Insert(4, "4");
            ddlTotalYr.Items.Insert(5, "5");
            ddlTotalYr.Items.Insert(6, "6");
            ddlTotalYr.Items.Insert(7, "7");
            ddlTotalYr.Items.Insert(8, "8");
            ddlTotalYr.Items.Insert(9, "9");
            ddlTotalYr.Items.Insert(10, "10");
        }
        else
        {
            ddlTotalYr.Visible = true;
            ddlTotalYr.Items.Clear();
            ddlTotalYr.Items.Insert(0, "Select Year/Sem");
            ddlTotalYr.SelectedIndex = 0;
            ddlExamType.Focus();
        }
    }

    private void Clear()
    {
        if (ddlExamType.SelectedIndex > 0)
            ddlExamType.SelectedIndex = 0;
        if (ddlFaculty.SelectedIndex > 0)
            ddlFaculty.SelectedIndex = 0;
        if (ddlTotalYr.SelectedIndex > 0)
            ddlTotalYr.SelectedIndex = 0;
        if (ddlCourseType.SelectedIndex > 0)
            ddlCourseType.SelectedIndex = 0;
        txtCourseCode.Text = txtCourseName.Text = txtExamFullName.Text = txtExamShortName.Text = txtRefCtrl.Text = "";
        ddlFaculty.Focus();
    }

    public static string GetCompCode()  // Get Computer Name
    {
        string strHostName = "";
        strHostName = Dns.GetHostName();
        return strHostName;
    }

    public static string GetIpAddress()  // Get IP Address
    {
        string ip = "";
        IPHostEntry ipEntry = Dns.GetHostEntry(GetCompCode());
        IPAddress[] addr = ipEntry.AddressList;
        ip = addr[1].ToString();
        return ip;
    }

    private DataTable GetLastExamMasterEntry()  // Bind Gridview (Last 5 Records Displaying)
    {
        plExamMst.Ind = 2;
        dt = blExamMst.GetLastExamMaster(plExamMst);
        if (dt.Rows.Count > 0)
        {
            gridExamDetails.Visible = true;
            gridExamDetails.DataSource = dt;
            gridExamDetails.DataBind();
        }
        else
            gridExamDetails.Visible = false;
        return dt;
    }

}



// WHEN EXAM TYPE IS SEMSTER THEN 
//if (ddlTotalSem.SelectedIndex > 0)
//{
//    for (int j = 0; j < Convert.ToInt32(ddlTotalSem.SelectedValue); j++)
//    {
//        plExamMst.Ind = 1;
//        plExamMst.UnivCode = 10;
//        plExamMst.FacultyCD = Convert.ToInt32(ddlFaculty.SelectedValue);

//        int sem = j + 1;
//        plExamMst.ExamName = txtExamFullName.Text + " SEMESTER " + sem;
//        plExamMst.ExamShortName = txtExamShortName.Text + " SEMESTER " + sem;
//        plExamMst.ExamType = Convert.ToInt32(ddlExamType.SelectedValue);

//        if (ddlTotalSem.SelectedIndex > 0)
//            plExamMst.TotalYearSem = Convert.ToInt32(ddlTotalSem.SelectedValue);

//        else if (ddlTotalYr.SelectedIndex > 0)
//            plExamMst.TotalYearSem = Convert.ToInt32(ddlTotalYr.SelectedValue);

//        plExamMst.NoOfYearSem = sem;

//        if (string.IsNullOrEmpty(txtCourseCode.Text))
//            plExamMst.CourseCD = 0;
//        else
//            plExamMst.CourseCD = Convert.ToInt32(txtCourseCode.Text);

//        plExamMst.CourseName = txtCourseName.Text;

//        if (string.IsNullOrEmpty(txtRefCtrl.Text))
//            plExamMst.RefCtrl = 0;
//        else
//            plExamMst.RefCtrl = Convert.ToInt32(txtRefCtrl.Text);

//        plExamMst.IPAddress = GetIpAddress();
//        plExamMst.UserID = Convert.ToInt32(Session["UserId"]);
//        plExamMst.CreationDate = DateTime.Now;
//        plExamMst.GroupID = 0;
//        plExamMst.GroupSubID = 0;
//        i = blExamMst.InsertExamMaster(plExamMst);
//        if (i > 0)
//        {
//            lblMsg.Text = "Data Inserted Successfully";
//        }
//    }
//}
