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

public partial class FrmFileUploadTextFormat : System.Web.UI.Page
{
    FileUpload f1, f2, f3, f4, f5, f6; String uploadFIle;
    string PathTempFol, FinalPath, WithFileName;
    DLFileUpload dlFU = new DLFileUpload();
    PlUpload PlFU = new PlUpload();
    DataTable dt;
    int errorCount = 0;

    static Int32 lineCount;
    static List<string> ErrorLst, lines;
    static StructUpload[] listStruct;

    public struct StructUpload
    {
        public DataTable BulkDT { get; set; }
        public string FileName { get; set; }
        public string TableName { get; set; }
        public int DataCount { get; set; }
        public bool IsInError { get; set; }
        public string ErrorMessage { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SessionLoad();
            ddlSession.Items.Insert(0, "-- Select Session --");
            ExamNameLoad();
            ddlExamName.Items.Insert(0, "-- Select Exam Name --");

            //listStruct = new List<StructUpload>(4);
            listStruct = new StructUpload[6];
            //Msg1.Text = "";
            //ImportAllFiles();
            //FileSaperate();
        }
        lblException.Text = "";
    }

    #region comme

    void FileSaperate()
    {
        List<string> lst = new List<string>()
        {
            //"W11","W12","W13","W14","W15", "W16",
            //"S11","S12","S13","S14","S15", "S16"

            "W04","W05","W06","W07","W08", "W09","W10",
            "S04","S05","S06","S07","S08", "S09","S10"
        };


        //string createNew = @"C:\Sachin\Amrawati Projects\NewCreate\DIGI_DATA_S11_TO_S16";

        //string directory = @"C:\Sachin\Amrawati Projects\Files\DIGI_DATA_S11_TO_S16";

        string createNew = @"E:\Amrawati Projects\CSV_FIles\NewCreate\DIGI_DATA_S04_TO_W10";
        string directory = @"E:\Amrawati Projects\CSV_FIles\Files\DIGI_DATA_S04_TO_W10";

        foreach (string sess in lst.ToList())
        {
            foreach (var item in Directory.GetFiles(directory))
            {
                FileInfo f = new FileInfo(item);
                string s = f.Name;
                if (s.Contains(sess.ToUpper()))
                {
                    if (!Directory.Exists(createNew + "\\" + sess))
                    {
                        Directory.CreateDirectory(createNew + "\\" + sess);
                    }
                    if (File.Exists(createNew + "\\" + sess + "\\" + s))
                    {
                        continue;
                    }
                    File.Copy(directory + "\\" + s, createNew + "\\" + sess + "\\" + s);
                }
            }
        }
        foreach (var item in lst)
        {

        }

    }

    public void SessionLoad()
    {
        ddlSession.DataSource = dlFU.GetSession(PlFU);
        ddlSession.DataTextField = "ItemDesc";
        ddlSession.DataValueField = "ItemID";
        ddlSession.DataBind();
        AlertSuccess.Visible = false;
    }

    public void ExamNameLoad()
    {
        ddlExamName.DataSource = dlFU.GetExamName(PlFU);
        ddlExamName.DataTextField = "ItemDesc";
        ddlExamName.DataValueField = "ItemID";
        ddlExamName.DataBind();
        AlertSuccess.Visible = false;
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
        foreach (string header in headers)
        {
            string colName = header;
            try
            {
                dtSave.Columns.Add(colName);
            }
            catch (Exception ex)
            {
                errorCount++;
                lblException.Text += "<i class='fa fa-frown-o'></i> &nbsp;" + errorCount + ". " + ex.Message + "<br />";
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
            listStruct = new StructUpload[6];
        }

        return structUpload;
    }

    string[] dataType(DataTable dtSave)
    {
        #region DataTypes
        DataRow datarow = dtSave.Rows[1];
        string[] dataType = new string[dtSave.Columns.Count];
        for (int i = 0; i < dtSave.Columns.Count; i++)
        {
            var item = datarow[i];
            string type = "";
            try
            {
                Convert.ToDecimal(item);
                type = "float";
            }
            catch
            {
                try
                {
                    Convert.ToDateTime(item);
                    type = "datetime";
                }
                catch
                {
                    type = "varchar(MAX)";
                }
            }
            dataType[i] = type;

            //string a = item.GetType() == typeof(int) ? "varchar(8)"
            //          : item.GetType() == typeof(decimal) ? "varchar(8)"
            //          : item.GetType() == typeof(string) ? "varcahr(100)"
            //          : item.GetType() == typeof(DateTime) ? "varchar(12)" : "varcahr(100)";
        }
        return dataType;
        #endregion
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

        string Exam = Path.GetFileNameWithoutExtension(fu.FileName);
        Exam = Exam.Substring(0, Exam.IndexOf("_"));

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

            string CG = string.Empty;

            if (CbCG.Checked == true)
            {
                CG = "CG";
                if (!string.IsNullOrEmpty(FU4.FileName))
                {
                    string examName = Path.GetFileNameWithoutExtension(FU4.FileName);
                    examName = examName.Substring(0, examName.IndexOf("_"));
                    CG += "_" + examName;
                }
            }

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
                FUData.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + CG + "_File1";
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

                listStruct.SetValue(FUData, 0);
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
                FUData.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + CG + "_File2";
                listStruct.SetValue(FUData, 1);
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
                FUData.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + CG + "_File3";
                listStruct.SetValue(FUData, 2);
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
                FUData.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + CG + "_File4";
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
                FUData.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + CG + "_File5";
                listStruct.SetValue(FUData, 4);
                //listStruct.Insert(4, FUData);
                spanFName5.InnerText = FUData.DataCount.ToString();
                spanFName5.InnerText += " - " + FU5.FileName;
                spanFName5.Attributes.Add("class", "text-primary");

            }
            else if (FU6.HasFile)
            {

                f6 = FU6;

                string NameOfFile = ValidateNameOfFile(f6); //Validate FileName
                string Tab = Path.GetFileNameWithoutExtension(f6.FileName);
                Tab = Tab.Substring(Tab.IndexOf("_"));
                Tab = Tab.Split('_')[1].Split('_')[0];
                Tab = Tab.Substring(3);

                if (Tab != "6")
                {
                    spanFName6.Attributes.Add("class", "text-danger");
                    spanFName6.InnerText = "File Should be .._TAB6_" + NameOfFile;
                    return;
                }
                if (NameOfFile != "true")
                {
                    spanFName6.Attributes.Add("class", "text-danger");
                    spanFName6.InnerText = "File Should be .._TAB6_" + NameOfFile;
                    return;
                }

                StructUpload FUData = ConvertCSVtoDataTable(f6);
                FUData.TableName = "Tbl_" + ddlExamName.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "_" + CG + "_File6";
                listStruct.SetValue(FUData, 5);
                //listStruct.Insert(4, FUData);
                spanFName6.InnerText = FUData.DataCount.ToString();
                spanFName6.InnerText += " - " + FU6.FileName;
                spanFName6.Attributes.Add("class", "text-primary");

            }
            else
            {
                lblException.Text += "File is empty please check!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowException", "ShowException()", true);
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
                    if (CbCG.Checked == true)
                    {
                        if (!string.IsNullOrEmpty(listStruct[5].FileName))
                        {
                            btnSave.Visible = true;
                        }
                    }
                    else btnSave.Visible = true;
                }
            }
            else
            {
                listStruct = new StructUpload[6];
                spanFName1.InnerText = spanFName2.InnerText = spanFName3.InnerText = spanFName4.InnerText = spanFName5.InnerText = spanFName6.InnerText = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('All files has empty. please file choose again')", true);
            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowConfirmation", "ShowConfirmation()", true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblException.Text = "";
            if (spanFName1.InnerText == "" || spanFName2.InnerText == "" ||
                spanFName3.InnerText == "" || spanFName4.InnerText == "" || spanFName5.InnerText == "" ||
                ddlSession.SelectedIndex == 0 || ddlExamName.SelectedIndex == 0 ||
                listStruct[0].FileName == null || listStruct[1].FileName == null || listStruct[2].FileName == null ||
                listStruct[3].FileName == null || listStruct[4].FileName == null)
            {
                if (CbCG.Checked == true)
                {
                    if (listStruct[5].FileName == null)
                    {
                        AlertWarning.Visible = true;
                        AlertSuccess.Visible = false;
                    }
                }
                else
                {
                    AlertWarning.Visible = true;
                    AlertSuccess.Visible = false;
                }
            }
            else
            {
                string ex = DynamicTable();
                if (!string.IsNullOrEmpty(ex))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowException", "ShowException()", true);
                    return;
                }
                AlertSuccess.Visible = true;
                AlertWarning.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowConfirmation", "ShowConfirmation()", true);
            }
        }
        catch (Exception ex)
        {

            lblException.Text += ex.Message;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowException", "ShowException()", true);
        }
        finally
        {
            if (listStruct == null)
            {
                ClearAll();
            }

        }
    }

    public string DynamicTable()
    {

        foreach (StructUpload StructObj in listStruct.ToList())
        {
            try
            {
                if (StructObj.BulkDT == null && StructObj.FileName == null)
                {
                    continue;
                }
                string Query = ""; int count = 0;


                PlFU.tableName = StructObj.TableName;
                bool tblCount = dlFU.CheckTable(PlFU);

                if (tblCount == false)
                {
                    Query += "Create Table " + PlFU.tableName + "(";
                    foreach (DataColumn item in StructObj.BulkDT.Columns)
                    {
                        Query += item + " varchar(130)";
                        count++;
                        Query += ", ";
                    }
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
            catch (Exception ex)
            {
                lblException.Text += "<i class='fa fa-frown-o'></i> &nbsp;" + ++errorCount + ".  &nbsp;";
                lblException.Text += "Problem In File Please Check File - " + StructObj.FileName + " This Is Not Create Because of - <br />" + ex.Message + "<br />";
                //continue;
            }
            //finally
            //{
            //    SqlConnection con = DataAcces.Occconnection();
            //    con.Close();
            //    con.Dispose();
            //}
        }
        return lblException.Text;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            PlFU = new PlUpload();
            PlFU.ExamYear = Convert.ToInt32(ddlExamName.SelectedItem.Text);
            PlFU.ExamSession = ddlSession.SelectedItem.Text;
            //PlFU.tableName = "TblUniversityData_" + PlFU.ExamSession + "_" + PlFU.ExamYear + "_RD";
            bool tblCount = dlFU.CheckRDRL(PlFU);
            if (tblCount)
            {
                DataSet ds = dlFU.CreateRDRL(PlFU);
                if (ds.Tables.Count != 2)
                {
                    
                }    
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            ClearAll();
        }
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    void ClearAll()
    {
        spanFName1.InnerText = spanFName2.InnerText = spanFName3.InnerText = spanFName4.InnerText = spanFName5.InnerText = spanFName6.InnerText = "";
        //ddlExamName.SelectedIndex = ddlSession.SelectedIndex = 0;
        listStruct = new StructUpload[6];
        AlertSuccess.Visible = false;
        AlertWarning.Visible = false;
        //CbCG.Checked = false;
        //ImportAllFiles();
    }

    #endregion

    async void ImportAllFiles()
    {
        //string createNew = @"E:\Amrawati Projects\CSV_FIles\NewCreate\DIGI_DATA_S04_TO_W10";
        string directory = @"E:\Amrawati Projects\CSV_FIles\Files\DIGI_DATA_S11_TO_S16";

        string TableName = "";
        int count = 0;
        foreach (var getFIles in Directory.GetFiles(directory))
        {
            FileInfo currFile = new FileInfo(getFIles);
            string fileName = currFile.Name;
            string Sesion = "";
            string getSession = fileName.Substring(fileName.LastIndexOf("_") + 1, 1).ToUpper();
            string getYear = fileName.Substring(fileName.LastIndexOf("_") + 2, 2).ToUpper();
            if (getYear != "13")
            {
                continue;
            }

            if (getSession == "S")
            {
                Sesion = "Summer";
            }
            else if (getSession == "W")
            {
                Sesion = "Winter";
            }
            try
            {
                string Tab = fileName;
                string File = "";
                Tab = Tab.Substring(Tab.IndexOf("_"));
                File = Tab = Tab.Split('_')[1].Split('_')[0];
                File = File.Replace("TAB", "FILE");
                TableName = "Tbl_" + (Convert.ToInt16(getYear) + 2000) + "_" + Sesion + "_" + File;
            }
            catch (Exception ex)
            {
                if (DtErrorFILE == null)
                {
                    DtErrorFILE = ErrorData();
                }
                DataRow dr = DtErrorFILE.NewRow();
                dr["ExcelName"] = "Invalid File Name - " + fileName;
                dr["TableName"] = "";
                dr["Exception"] = ex.Message;
                DtErrorFILE.Rows.Add(dr);
                continue;
            }
            //Tab = Tab.Substring(3);

            StructUpload StructObj = ConvertCSVtoDataTable(currFile);
            StructObj.TableName = TableName;
            StructObj.FileName = fileName;

            if (StructObj.IsInError)
            {
                if (DtErrorFILE == null)
                {
                    DtErrorFILE = ErrorData();
                }
                DataRow dr = DtErrorFILE.NewRow();
                dr["ExcelName"] = StructObj.FileName;
                dr["TableName"] = StructObj.TableName;
                dr["Exception"] = StructObj.ErrorMessage.Replace("<i class='fa fa-frown-o'></i> &nbsp;", "Error : ").Replace("<br />", "~");//lblException.Text.Replace("<i class='fa fa-frown-o'></i> &nbsp;", "Error : ").Replace("<br />","~");
                DtErrorFILE.Rows.Add(dr);
                continue;
            }
            

            await System.Threading.Tasks.Task.Run(() => UploadDataTable(StructObj));
            Thread.Sleep(1500);

            count++;
            lblCountFile.Text = count.ToString() + " Files Uploaded.";
        }

        CreateExcelError();
    }

    void CreateExcelError()
    {
        if (DtErrorFILE != null && DtErrorFILE.Rows.Count > 0)
        {
            try
            {
                DataTable dtdata = DtErrorFILE;
                string attach = "attachment;filename=MemberList.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attach);
                Response.ContentType = "application/ms-excel";
                
                if (dtdata != null)
                {
                    Response.Write("Member List \t");
                    Response.Write("Date : " + DateTime.Now.ToShortDateString() + "\t");
                    Response.Write(System.Environment.NewLine);
                    foreach (DataColumn dc in dtdata.Columns)
                    {
                        Response.Write(dc.ColumnName + "\t");
                        //sep = ";";
                    }
                    Response.Write(System.Environment.NewLine);
                    foreach (DataRow dr in dtdata.Rows)
                    {
                        for (int i = 0; i < dtdata.Columns.Count; i++)
                        {
                            string[] errors = dr[i].ToString().Split('~');
                            foreach (string error in errors)
                            {
                                Response.Write(error + "\n");
                            }
                            //Response.Write(dr[i].ToString() + "\t");
                        }
                        Response.Write("\n");
                    }
                    Response.Write("\n");
                    Response.Write("Total - " + lblCountFile.Text);
                    Response.Write("\n");
                    Response.Write("Total - " + dtdata.Rows.Count.ToString() + " Files Generate Errors.");
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblException.Text = ex.Message;
                //lblErrorMsg.Text = ex.Message;
            }
        }
    }

    void UploadDataTable(StructUpload StructObj)
    {
        try
        {
            PlFU = new PlUpload();
            PlFU.tableName = StructObj.TableName;
            bool tblCount = dlFU.CheckTable(PlFU);
            string Query = "";
            int count = 0;
            if (tblCount == false)
            {
                Query += "Create Table " + PlFU.tableName + "(";
                foreach (DataColumn item in StructObj.BulkDT.Columns)
                {
                    Query += item + " varchar(130)";
                    count++;
                    Query += ", ";
                }
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
        catch (Exception ex)
        {
            if (DtErrorFILE == null)
            {
                DtErrorFILE = ErrorData();
            }
            DataRow dr = DtErrorFILE.NewRow();
            dr["ExcelName"] = StructObj.FileName;
            dr["TableName"] = StructObj.TableName;
            dr["Exception"] = ex.Message;
            DtErrorFILE.Rows.Add(dr);

            //lblException.Text += "<i class='fa fa-frown-o'></i> &nbsp;" + ++errorCount + ".  &nbsp;";
            //lblException.Text += "Problem In File Please Check File - " + StructObj.FileName + " This Is Not Create Because of - <br />" + ex.Message + "<br />";
            //continue;
        }
    }

    StructUpload ConvertCSVtoDataTable(FileInfo currFile)
    {
        StructUpload structUpload = new StructUpload();

        bool IsInError = false; string ErrorMsg = "";

        ErrorLst = new List<string>();
        lines = new List<string>();

        string strForHeader = "";
        using (System.IO.StreamReader file = currFile.OpenText())
        {
            strForHeader = file.ReadLine();
            while (!file.EndOfStream)
            {
                lines.Add(file.ReadLine());
                lineCount = lines.Count;
            }
        }

        #region Columns Add
        string[] headers = strForHeader.Split('~');
        DataTable dtSave = new DataTable();
        int CopyCol = 1;
        foreach (string header in headers)
        {
            string colName = header;
            try
            {
                dtSave.Columns.Add(colName);
            }
            catch (DuplicateNameException ex)
            {
                dtSave.Columns.Add((colName + CopyCol++).ToString());

                structUpload.IsInError = true;
                ErrorMsg += "" + ex.Message + Environment.NewLine + "<br />";
                errorCount++;
                lblException.Text += "<i class='fa fa-frown-o'></i> &nbsp;" + errorCount + ". " + ex.Message + "<br />";
                //return structUpload;
            }
            catch (Exception ex)
            {
                structUpload.IsInError = true;
                ErrorMsg += "" + ex.Message + Environment.NewLine + "<br />";
                errorCount++;
                lblException.Text += "<i class='fa fa-frown-o'></i> &nbsp;" + errorCount + ". " + ex.Message + "<br />";
            }

        }
        dtSave.Columns.Add("FileName", typeof(string));
        #endregion

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
                try
                {
                    dr[i] = currFile.Name;
                    dtSave.Rows.Add(dr);
                }
                catch (Exception ex)
                {
                    structUpload.IsInError = true;
                    IsInError = true;
                    ErrorMsg += "Error Of Reading Line" + ex.Message + Environment.NewLine + "<br />";
                    lblException.Text += "Error Of Reading Line" + ex.Message + "<br />";
                }
            }
            rows = null;
        }

        //if (IsInError)
        //{
        //    if (DtErrorFILE == null)
        //    {
        //        DtErrorFILE = ErrorData();
        //    }
        //    DataRow drErr = DtErrorFILE.NewRow();
        //    drErr["ExcelName"] = currFile.Name;
        //    drErr["TableName"] = "";//StructObj.TableName;
        //    drErr["Exception"] = ErrorMsg;
        //    DtErrorFILE.Rows.Add(drErr);
        //}

        structUpload.BulkDT = dtSave;
        structUpload.DataCount = (int)dtSave.Rows.Count;
        structUpload.FileName = currFile.Name;
        structUpload.ErrorMessage = ErrorMsg;
        return structUpload;
    }

    DataTable ErrorData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ExcelName", typeof(string));
        dt.Columns.Add("TableName", typeof(string));
        dt.Columns.Add("Exception", typeof(string));
        return dt;
    }

    public DataTable DtErrorFILE
    {
        get { return (DataTable)ViewState["ErrorFiles"]; }
        set { ViewState["ErrorFiles"] = value; }
    }
}