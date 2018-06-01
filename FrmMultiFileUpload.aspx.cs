using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmMultiFileUpload : System.Web.UI.Page
{
    FileUpload f1, f2, f3, f4, f5; String uploadFIle;
    string PathTempFol, FinalPath, WithFileName;
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());//change connection string name later when work is in progress.
    DLFileUpload dlFU = new DLFileUpload();
    PlUpload PlFU = new PlUpload();
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SessionLoad();
            ddlSession.Items.Insert(0, "-- Select Session --");
            ExamNameLoad();
            ddlExamName.Items.Insert(0, "-- Select Exam Name --");
            
            //listStruct = new List<StructUpload>(4);
            listStruct = new StructUpload[5];
            //Msg1.Text = "";
        } 
            lblException.Text ="";
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

    static Int32 lineCount;
    static List<string> ErrorLst, lines;
    //static List<string[]> SavingLst;
    //static List<StructUpload> listStruct;
    static StructUpload[] listStruct;

    public struct StructUpload
    {
        public DataTable BulkDT { get; set; }
        public string FileName { get; set; }
        public string TableName { get; set; }
        public int DataCount { get; set; }
    }

    StructUpload ConvertCSVtoDataTable(FileUpload fu)
    {
        ErrorLst = new List<string>();
        lines = new List<string>();
     
        string strForHeader = "";
        using (System.IO.StreamReader file = new StreamReader(fu.FileContent))
        {
            strForHeader = file.ReadLine();
            while (!file.EndOfStream)
            {
                lines.Add(file.ReadLine());
                lineCount = lines.Count;
            }
        }

        string[] headers = strForHeader.Split('~');
        DataTable dtSave = new DataTable();
        int errorCount = 0;
        foreach (string header in headers)
        {

                string colName = header;
                #region Rename ColName
                //int a = 1;
                //foreach (DataColumn item in dtSave.Columns)
                //{
                //    while (item.ColumnName == colName)
                //    {
                //        a++;
                //        colName += a;
                //    }
                //}
                #endregion
                try
                {
                    dtSave.Columns.Add(colName);
                }
                catch (Exception ex)
                {
                    errorCount++;
                    lblException.Text += errorCount + ". " + ex.Message + "<br />";
                }
            
        }
        dtSave.Columns.Add("FileName", typeof(string));

        foreach (string item in lines)
        {
            string[] rows = Regex.Split(item, "~(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)", RegexOptions.IgnoreCase);
            DataRow dr = dtSave.NewRow();

            if (rows.Count() < headers.Count())
            {
                ErrorLst.Add(item);
                //lblException.Text += "System Can Not Read This Lines : <br />" + ErrorLst + "<br />";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowException", "ShowException()", true);
            }
            else
            {
                int i;
                for (i = 0; i < rows.Count(); i++)
                    dr[i] = rows[i];

                dr[i] = fu.FileName;
                dtSave.Rows.Add(dr);
            }
            rows = null;
        }

        StructUpload structUpload = new StructUpload();
        structUpload.BulkDT = dtSave;
        structUpload.DataCount = (int)dtSave.Rows.Count;
        structUpload.FileName = fu.FileName;

        if (listStruct == null)
        {
            listStruct = new StructUpload[5];
            //listStruct = new List<StructUpload>(4);
        }
        //structUpload.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + "_File" + (listStruct.Count() + 1).ToString();

        return structUpload;
    }

    string ValidateNameOfFile(FileUpload fu)
    {
        string s, Session, Year, NameOfFile;
        s = Path.GetFileNameWithoutExtension(fu.FileName);
        s = s.Substring(s.LastIndexOf("_")).Replace("_", "");

        Session = ddlSession.SelectedItem.Value;
        Year = ddlExamName.SelectedItem.Text.Substring(2); //EX - 2012 To 12

        if (Session == "1") NameOfFile = "W";
        else NameOfFile = "S";

        NameOfFile += Year;

        if (s != NameOfFile)
        {
            return NameOfFile; //"File name does not match with selected Session & Year";
        }

        else
            return "true";
    }

    protected void btnHide_Click(object sender, EventArgs e)
    {
        try
        {
            #region Code
            AlertSuccess.Visible = false;
            AlertWarning.Visible = false;
            if (FU1.HasFile)
            {
                f1 = FU1;

                string NameOfFile = ValidateNameOfFile(f1); //Validate FileName

                string Tab = Path.GetFileNameWithoutExtension(f1.FileName);
                Tab = Tab.Substring(Tab.IndexOf("_"));
                Tab = Tab.Split('_')[1].Split('_')[0];
                Tab = Tab.Substring(3);

                if (Tab != "1")
                {
                    spanFName1.Attributes.Add("class", "text-danger");
                    spanFName1.InnerText = "File Should be _TAB1_" + NameOfFile;
                    return;
                }
                if (NameOfFile != "true")
                {
                    spanFName1.Attributes.Add("class", "text-danger");
                    spanFName1.InnerText = "File Should be .._TAB1_" + NameOfFile;//"File name does not match with selected Session & Year";
                    return;
                }
              
                StructUpload FUData = ConvertCSVtoDataTable(f1);
                FUData.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + "_File1";

                //string filter = NameOfFile;
                //FU2.Attributes.Add("accept", NameOfFile);//".xls, .xlsx");

                #region Implement
                //string TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + "_File1";
                //DataTable dtColumnCheck = dlFU.DtColumnCheck(TableName);
                //int i = 0;
                //foreach (DataRow dr in dtColumnCheck.Rows)
                //{
                //    foreach (DataColumn item in FUData.BulkDT.Columns)
                //    {
                //        if (dr[0].ToString() != item.ColumnName)
                //        {
                //            lblException.Text += "<br />" + item + " is not matching with " + dr[0]; 
                //        }
                //    }
                //    if (dr[i] == )
                //    {
                        
                //    }
                //}
                #endregion

                //listStruct.Insert(0, FUData);
                listStruct.SetValue(FUData, 0);//[0] = FUData;
                spanFName1.Attributes.Add("class", "text-primary");
                spanFName1.InnerText = FUData.DataCount.ToString();
                spanFName1.InnerText += " - " + FU1.FileName;

            }
            else if (FU2.HasFile)
            {
                f2 = FU2;

                string NameOfFile = ValidateNameOfFile(f2); //Validate FileName

                string Tab = Path.GetFileNameWithoutExtension(f2.FileName);
                Tab = Tab.Substring(Tab.IndexOf("_"));
                Tab = Tab.Split('_')[1].Split('_')[0];
                Tab = Tab.Substring(3);

                if (Tab != "2")
                {
                    spanFName2.Attributes.Add("class", "text-danger");
                    spanFName2.InnerText = "File Should be .._TAB2_" + NameOfFile;
                    return;
                }
                if (NameOfFile != "true")
                {
                    spanFName2.Attributes.Add("class", "text-danger");
                    spanFName2.InnerText = "File Should be .._TAB2_" + NameOfFile;
                    return;
                }
                StructUpload FUData = ConvertCSVtoDataTable(f2);
                FUData.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + "_File2";
                listStruct.SetValue(FUData, 1);
                //listStruct.Insert(1, FUData);
                spanFName2.Attributes.Add("class", "text-primary");
                spanFName2.InnerText = FUData.DataCount.ToString();
                spanFName2.InnerText += " - " + FU2.FileName;
            }
            else if (FU3.HasFile)
            {
                f3 = FU3;
                string NameOfFile = ValidateNameOfFile(f3); //Validate FileName

                string Tab = Path.GetFileNameWithoutExtension(f3.FileName);
                Tab = Tab.Substring(Tab.IndexOf("_"));
                Tab = Tab.Split('_')[1].Split('_')[0];
                Tab = Tab.Substring(3);

                if (Tab != "3")
                {
                    spanFName3.Attributes.Add("class", "text-danger");
                    spanFName3.InnerText = "File Should be .._TAB3_" + NameOfFile;
                    return;
                }
                if (NameOfFile != "true")
                {
                    spanFName3.Attributes.Add("class", "text-danger");
                    spanFName3.InnerText = "File Should be .._TAB3_" + NameOfFile;
                    return;
                }
                StructUpload FUData = ConvertCSVtoDataTable(f3);
                FUData.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + "_File3";
                listStruct.SetValue(FUData, 2);
                //listStruct.Insert(2, FUData);
                spanFName3.InnerText = FUData.DataCount.ToString();
                spanFName3.InnerText += " - " + FU3.FileName;
                spanFName3.Attributes.Add("class", "text-primary");
            }
            else if (FU4.HasFile)
            {

                f4 = FU4;

                string NameOfFile = ValidateNameOfFile(f4); //Validate FileName

                string Tab = Path.GetFileNameWithoutExtension(f4.FileName);
                Tab = Tab.Substring(Tab.IndexOf("_"));
                Tab = Tab.Split('_')[1].Split('_')[0];
                Tab = Tab.Substring(3);

                if (Tab != "4")
                {
                    spanFName4.Attributes.Add("class", "text-danger");
                    spanFName4.InnerText = "File Should be .._TAB4_" + NameOfFile;
                    return;
                }
                if (NameOfFile != "true")
                {
                    spanFName4.Attributes.Add("class", "text-danger");
                    spanFName4.InnerText = "File Should be .._TAB4_" + NameOfFile;
                    return;
                }
                StructUpload FUData = ConvertCSVtoDataTable(f4);
                FUData.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + "_File4";
                listStruct.SetValue(FUData, 3);
                //listStruct.Insert(3, FUData);
                spanFName4.InnerText = FUData.DataCount.ToString();
                spanFName4.InnerText += " - " + FU4.FileName;
                spanFName4.Attributes.Add("class", "text-primary");

            }
            else if (FU5.HasFile)
            {

                f5 = FU5;

                string NameOfFile = ValidateNameOfFile(f5); //Validate FileName
                string Tab = Path.GetFileNameWithoutExtension(f5.FileName);
                Tab = Tab.Substring(Tab.IndexOf("_"));
                Tab = Tab.Split('_')[1].Split('_')[0];
                Tab = Tab.Substring(3);

                if (Tab != "5")
                {
                    spanFName5.Attributes.Add("class", "text-danger");
                    spanFName5.InnerText = "File Should be .._TAB5_" + NameOfFile;
                    return;
                }
                if (NameOfFile != "true")
                {
                    spanFName5.Attributes.Add("class", "text-danger");
                    spanFName5.InnerText = "File Should be .._TAB5_" + NameOfFile;
                    return;
                }

                StructUpload FUData = ConvertCSVtoDataTable(f5);
                FUData.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + "_File5";
                listStruct.SetValue(FUData, 4);
                //listStruct.Insert(4, FUData);
                spanFName5.InnerText = FUData.DataCount.ToString();
                spanFName5.InnerText += " - " + FU5.FileName;
                spanFName5.Attributes.Add("class", "text-primary");

            }
            #endregion
        }
        catch (Exception ex)
        {
            lblException.Text += ex.Message;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowException", "ShowException()", true);
        }
        finally
        {
            if (listStruct != null)
            {
                if (!string.IsNullOrEmpty(listStruct[0].FileName) && !string.IsNullOrEmpty(listStruct[1].FileName) && !string.IsNullOrEmpty(listStruct[2].FileName) && !string.IsNullOrEmpty(listStruct[3].FileName) && !string.IsNullOrEmpty(listStruct[4].FileName))
                {
                    btnSave.Visible = true;
                }                
            }
            else
            {
                listStruct = new StructUpload[5];
                spanFName1.InnerText = spanFName2.InnerText = spanFName3.InnerText = spanFName4.InnerText = spanFName5.InnerText = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('All files has empty. please file choose again')",true);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowConfirmation", "ShowConfirmation()", true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (spanFName1.InnerText == "" || spanFName2.InnerText == "" ||
                spanFName3.InnerText == "" || spanFName4.InnerText == "" ||
                spanFName5.InnerText == "" || ddlSession.SelectedIndex == 0 || ddlExamName.SelectedIndex == 0 || listStruct.Count() < 5) /**/
            {
                AlertWarning.Visible = true;
                AlertSuccess.Visible = false;
            }

            else
            {
                DynamicTable();
                AlertSuccess.Visible = true;
                AlertWarning.Visible = false;
                spanFName1.InnerText = spanFName2.InnerText = spanFName3.InnerText = spanFName4.InnerText = spanFName5.InnerText = "";
                ddlExamName.SelectedIndex = ddlSession.SelectedIndex = 0;
                //listStruct = new StructUpload[5];
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowConfirmation", "ShowConfirmation()", true);
            }
        }
        catch (Exception ex)
        {
            lblException.Text = ex.Message;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowException", "ShowException()", true);
        }
    }

    public void DynamicTable()
    {
        foreach (StructUpload StructObj in listStruct.ToList())
        {
            if (StructObj.BulkDT==null && StructObj.FileName==null)
            {
                continue;
            }
            string Query = ""; int count = 0;

            PlFU.tableName = StructObj.TableName;
            bool tblCount = dlFU.CheckTable(PlFU);

            if (!tblCount)
            {
                #region DataTypes
                //DataRow datarow = dt.Rows[2];
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    var item = datarow[i];
                //    string a = item.GetType() == typeof(int) ? "int(10)"
                //          : item.GetType() == typeof(string) ? "varcahr(100)"
                //          : item.GetType() == typeof(DateTime) ? "DateTime" : "";

                //}
                #endregion
                //Query += "CREATE SCHEMA UnivAmravati  ";
                Query += "Create Table " + StructObj.TableName + "(";
                foreach (DataColumn item in StructObj.BulkDT.Columns)
                {
                    Query += item + " varchar(200)";
                    count++;
                    Query += ", ";
                }
                //Query += "FileName varchar(20)  DEFAULT '" + StructObj.FileName + "'";
                Query += ")";

                int Status = dlFU.DynamicTable(Query);
            }            

            PlFU.FileName = StructObj.FileName;
            bool status = dlFU.FileNameCheck(PlFU);
            if (status == true)
            {
                dlFU.DeleteData(PlFU);
            }
            dlFU.BulkDataInsert(StructObj.BulkDT, StructObj.TableName);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        listStruct = new StructUpload[5];
        AlertSuccess.Visible = false;
        AlertWarning.Visible = false;
        spanFName1.InnerText = spanFName2.InnerText = spanFName3.InnerText = spanFName4.InnerText = spanFName5.InnerText = "";
        ddlExamName.SelectedIndex = ddlSession.SelectedIndex = 0;
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {

    }
}