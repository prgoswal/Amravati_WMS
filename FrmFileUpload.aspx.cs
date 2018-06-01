using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmFileUpload : System.Web.UI.Page
{
    FileUpload f1, f2, f3, f4; String uploadFIle;
    string PathTempFol, FinalPath, WithFileName;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());//change connection string name later when work is in progress.
    DLFileUpload dlFU = new DLFileUpload();
    PlUpload PlFU =new PlUpload();
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   SessionLoad();
            ddlSession.Items.Insert(0, "-- Select Session --");
            ExamNameLoad();
            ddlExamName.Items.Insert(0, "-- Select Exam Name --");
            //Msg1.Text = "";
        }
    }
    public void SessionLoad()
    {
        PlFU.Ind = 2;
        dt = new DataTable();
        dt = dlFU.GetSession(PlFU);
        ddlSession.DataSource = dt;
        ddlSession.DataTextField = "ItemDesc";
        ddlSession.DataValueField = "ItemID";
        ddlSession.DataBind();
        AlertSuccess.Visible = false;
    }

    public void ExamNameLoad()
    {
        PlFU.Ind = 3;
        dt = new DataTable();
        dt = dlFU.GetExamName(PlFU);
        ddlExamName.DataSource = dt;
        ddlExamName.DataTextField = "ItemDesc";
        ddlExamName.DataValueField = "ItemID";
        ddlExamName.DataBind();
        AlertSuccess.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(false) //(spanFName1.InnerText == "" || spanFName2.InnerText == "" || spanFName3.InnerText == "" || spanFName4.InnerText == "" || ddlSession.SelectedIndex == 0 || ddlExamName.SelectedIndex == 0)
        {
            AlertWarning.Visible = true;
            AlertSuccess.Visible = false;
            //lblMsg.Text = "Please Upload All Files.";
        }
       
        else
        {
            string FinalPath = Server.MapPath("~/FrmUploadFol/");
            string TempFol = Server.MapPath("~/TempUpload/");
            string FolderName = "";
            string examName = ddlExamName.SelectedValue.ToString();
            string SessionName = ddlSession.SelectedValue.ToString();
            FolderName = examName + "_" + SessionName;
            string UploadFol = System.IO.Path.Combine(FinalPath, FolderName);
            if (Directory.Exists(UploadFol))
            {
                System.IO.Directory.Delete(UploadFol, true);
            }
            Directory.CreateDirectory(UploadFol);

            //for (int i = 0; i < 100000; i++)
            //{
                
            //}
            System.Threading.Thread.Sleep(2000);
            foreach (var item in Directory.GetFiles(TempFol))
            {
                string FileName = System.IO.Path.Combine(UploadFol, Path.GetFileName(item));
                System.IO.File.Move(item, FileName);               
                OleDbConnection OleObj = new OleDbConnection();
                string conString = "";
                if (Path.GetExtension(FileName) == ".xls")
                {
                    conString = String.Format(@"Provider=.;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                }
                else if (Path.GetExtension(FileName) == ".xlsx")
                {
                    conString = String.Format(@"Provider=.;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";");
                }
                OleObj = new OleDbConnection(conString);
                OleDbCommand ocmd = new OleDbCommand("Select * From [Sheet1$]", OleObj);
                OleObj.Open();
                OleDbDataAdapter oda = new OleDbDataAdapter(ocmd);
                DataTable dt = new DataTable();
                oda.Fill(dt);

                string tableName = "tbl_" + FolderName + Path.GetFileNameWithoutExtension(item).ToString();
                DynamicTable(dt, tableName);
            }
            AlertSuccess.Visible = true;
            AlertWarning.Visible = false;
            spanFName1.InnerText = spanFName2.InnerText = spanFName3.InnerText = spanFName4.InnerText = "";
            ddlExamName.SelectedIndex = ddlSession.SelectedIndex = 0;
            //Response.Redirect("FrmFileUpload.aspx");
        }
    }

    public void DynamicTable(DataTable dt, string tableName)
    {
        string Query = "", CheckTable = "", tblTruncate = ""; int count = 0;//, tblCount; 
        //int col = dt.Columns.Count;
        int row = dt.Rows.Count;
        for (int i = row; i >= row-1; i--)
        {
            dt.Rows[i-1].Delete();  
        }        
        tableName = tableName.Replace("-", "");
        PlFU.tableName = tableName;
        bool tblCount = dlFU.CheckTable(PlFU);
        //CheckTable = string.Format("select COUNT(TABLE_NAME) from INFORMATION_SCHEMA.TABLES where TABLE_NAME = '{0}'",tableName);       
        //SqlCommand ChktbleCMD = new SqlCommand(CheckTable,con);
        //con.Open();
        //tblCount =  ChktbleCMD.ExecuteNonQuery();
        //con.Close();
        if (!tblCount)
        {
            Query += "create table " + tableName + "(";
            foreach (var item in dt.Columns)
            {
                Query += item + " varchar(200)";
                count++;
                if (dt.Columns.Count > count)
                {
                    Query += ",";
                }
            }
            Query += ")";

            SqlCommand CreateTableCMD = new SqlCommand(Query, con);
            con.Open();
            CreateTableCMD.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            dlFU.TruncateTable(PlFU);

            //tblTruncate = string.Format("truncate table {0}",tableName);
            //SqlCommand tblTruncteCMD = new SqlCommand(tblTruncate,con);
            ////SqlDataAdapter sda = new SqlDataAdapter(tblTruncteCMD);
            ////DataTable dtTrunc = new DataTable();
            ////sda.Fill(dtTrunc);
            //con.Open();
            //tblTruncteCMD.ExecuteNonQuery();
            //con.Close();
        }
        SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con);
        sqlBulkCopy.DestinationTableName = tableName;
        con.Open();
        sqlBulkCopy.WriteToServer(dt);
        con.Close();

        #region Full Query
        /*
        int countCol = 0; string InsertQuery = "Insert Into " + tableName + "(";
        foreach (var col in dt.Columns)
        {
            InsertQuery += col;
            countCol++;
            if (dt.Columns.Count > countCol)
            {
                InsertQuery += ",";
            }

        }

        for (int i = 0; i < dt.Rows.Count-2; i++)
        {
            if (i==0)
            {
                InsertQuery += ") values (";
            }
            else
            {
                InsertQuery += ",(";
            }
            int c = 0;
            foreach (var item in dt.Columns)
            {
                InsertQuery += "'" + dt.Rows[i][item.ToString()] + "'";
                c++;
                if (dt.Columns.Count > c)
                {
                    InsertQuery += ",";
                }
            }
            InsertQuery += ")";    
        }
        SqlCommand cmd = new SqlCommand(InsertQuery, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //Query = "Create Table +"
        //int col = dt.Columns.Count;
        //int row = dt.Rows.Count;
        //foreach (var items in dt.Columns)
        //{

        //}
          */
        #endregion
    }

    public String SaveTemp(FileUpload fu)
    {   
        #region Comment
        //string FullPath, filname;
        //string Extension = System.IO.Path.GetExtension(FileUpload1.FileName);
        //string TempPath = Server.MapPath("TempUpload/");
        //string newFullPath = TempPath + FileUpload1.FileName;
        //Session["FileName"] = filname =  FileUpload1.FileName;
        //int count=2;
        //if (FileUpload1.HasFile)
        //{
        //    while (File.Exists(newFullPath))
        //    {
        //        string tempFileName = string.Format("({0}){1}{2}","Copy - ", count++, filname);
        //        newFullPath = Path.Combine(TempPath, tempFileName);
        //    }
        //    FileUpload1.SaveAs(newFullPath);
        //    FullPath = Server.MapPath("TempUpload/" + FileUpload1.FileName);
        //}      
        //string uploadPath = TempPath + filname;
        //// string Path = System.IO.Path.GetFullPath(FileUpload1.PostedFile.FileName);
        //Directory.CreateDirectory("~/TempUpload/");
        //var file = new FileInfo(Server.MapPath("~/TempUpload/" + Path.GetFileName(FileUpload1.FileName)));
        ////FileInfo newFile = new FileInfo("FullPath");
        #endregion
        int rowCnt;
        ExcelWorksheet worksheet;
        using (ExcelPackage xlPackage = new ExcelPackage(fu.FileContent))
        {
            worksheet = xlPackage.Workbook.Worksheets[1];
            rowCnt = worksheet.Dimension.End.Row - 3;
            var colCnt = worksheet.Dimension.End.Column;
        }

        if (uploadFIle == "U1")
        {
            CreateDire(fu);
        }
        else if (uploadFIle == "U2")
        {
            CreateDire(fu);
        }
        else if (uploadFIle == "U3")
        {
            CreateDire(fu);
        }
        else if (uploadFIle == "U4")
        {
            CreateDire(fu);
        }

        return rowCnt.ToString();
    }

    public void CreateDire(FileUpload fu)
    {
        PathTempFol = Server.MapPath("~/TempUpload/");
        WithFileName = System.IO.Path.Combine(PathTempFol, fu.FileName);
        int count = 2; string changeName = WithFileName;
        while (Directory.Exists(WithFileName))
        {
            changeName = fu.FileName + "(Copy2)" + count;
            count++;
        }
        string Save = System.IO.Path.Combine(PathTempFol, changeName);
        fu.SaveAs(Save);
    }

    protected void btnHide_Click(object sender, EventArgs e)
    {
        //lblMsg.Text = "";
        AlertSuccess.Visible = false;
        AlertWarning.Visible = false;
        if (FU1.HasFile)
        {
            for (double i = 0; i < 1500000; i++)
            {
                //lblMsg.Text = i.ToString();
            }

            f1 = FU1;
            uploadFIle = "U1";
            spanFName1.InnerText = SaveTemp(f1);
            spanFName1.InnerText += " - " + FU1.FileName;
        }
        else if (FU2.HasFile)
        {
            uploadFIle = "U2";
            f2 = FU2;
            spanFName2.InnerText = SaveTemp(f2);
            spanFName2.InnerText += " - " + FU2.FileName;
        }
        else if (FU3.HasFile)
        {
            uploadFIle = "U3";
            f3 = FU3;
            spanFName3.InnerText = SaveTemp(f3);
            spanFName3.InnerText += " - " + FU3.FileName;
        }
        else if (FU4.HasFile)
        {
            uploadFIle = "U4";
            f4 = FU4;
            spanFName4.InnerText = SaveTemp(f4);
            spanFName4.InnerText += " - " + FU4.FileName;
        }
        //else if (InFU1.PostedFile.FileName != "")
        //{
        //    uploadFIle = "InU1";
        //    f1.PostedFile = InFU1.PostedFile;
        //    spanFName1.InnerText = SaveTemp(InFU1);
        //}
        
    }

    [WebMethod]
    public static String SetName(string name)
    {
        return " // Code for some functionality    ";
    }

    protected void btnGetIP_Click(object sender, EventArgs e)
    {
        string ip = GetIpAddress();
        Response.Write("Your IP address is: " + ip + "<br />");
    }
    public static string GetIpAddress()  // Get IP Address
    {
        string ipV4, ipV6 = ""; string strHostName = "";
        strHostName = Dns.GetHostName();           // Get Computer Name
        string hostadd = Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
        IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
        IPAddress[] addr = ipEntry.AddressList;
        ipV4 = addr[1].ToString();
        ipV6 = addr[0].ToString();
        return ipV4;
    }

    //public static string GetCompCode()  // Get Computer Name
    //{
    //    string strHostName = "";
    //    strHostName = Dns.GetHostName();
    //    return strHostName;
    //}
}

