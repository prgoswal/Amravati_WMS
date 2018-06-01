using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmApplicationProgress : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        Disable();
        clear();

        if (Request.QueryString["ApplicationID"].ToString() != "0")
        {
            txtapplicationno.Text = Request.QueryString["ApplicationID"].ToString();
            txtapplicationno.Enabled = false;
            txtsearch.Text = "GO BACK";
            GetApplicationProgress();
        }
        else
            txtsearch.Focus();

        this.thediv1.Style.Add("background-color", "#FF00FF");
    }
    public void Disable()
    {
        level0div.Visible = false;
        thediv0.Visible = false;

        level1div.Visible = false;
        thediv1.Visible = false;

        level2div.Visible = false;
        thediv2.Visible = false;

        level3div.Visible = false;
        thediv3.Visible = false;

        level4div.Visible = false;
        thediv4.Visible = false;

        level5div.Visible = false;
        thediv5.Visible = false;

        level6div.Visible = false;
        thediv6.Visible = false;

        level7div.Visible = false;
        thediv7.Visible = false;

        printdiv.Visible = false;
        thedivprint.Visible = false;
    }
    public void clear()
    {
        lbltextFinalstatus.Text = "";
        Lblstudentnamepopup.Text = "";
        lblrollnopopup.Text = "";
        lblexampopup.Text = "";
        lblappliedforpopup.Text = "";
        lblfinalstatuspopup.Text = "";
        lblfinalstatus.Text = "";

        lbltextstudentname.Text = "";
        lbltextrollno.Text = "";
        lbltextappliedfor.Text = "";
        lbltextexamname.Text = "";
        lblapplicant.Text = "";
        lblrollno.Text = "";
        lblappliedfor.Text = "";
        lblexamname.Text = "";
        lblmsg.Text = "";
        lblpreparedatetext.Text = "";
        lblpreparedate.Text = "";
        lbllevel1Date.Text = "";
        lbllevel1datetext.Text = "";
        lbllevel2Date.Text = "";
        lbllevel2Datetext.Text = "";
        lbllevel3Date.Text = "";
        lbllevel3datetext.Text = "";
        lbllevel4Date.Text = "";
        lbllevel4datetext.Text = "";
        lbllevel5Date.Text = "";
        lbllevel5datetext.Text = "";
    }
    public async void GetApplicationProgress()
    {
        try
        {
            RegularExpressionValidator1.ErrorMessage = "";
            RequiredFieldValidator1.ErrorMessage = "";
            lblmsg.Text = ""; LblRemark.Text = "";
            if (txtapplicationno.Text != null || txtapplicationno.Text != "")
            {
                HttpClient HClient = new HttpClient();
                HClient.BaseAddress = new Uri(DataAcces.Url);
                HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                DataSet myDataSet = null;
                var uri = string.Format("api/StudentDetail/GetEntryProcess/?Ind={0}&ApplicationID={1}", 6, txtapplicationno.Text);
                var response = HClient.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var productJsonString = await response.Content.ReadAsStringAsync();
                    myDataSet = JsonConvert.DeserializeObject<DataSet>(productJsonString);
                }
                if (myDataSet != null)
                {

                    StringBuilder sb = new StringBuilder();

                    if (myDataSet.Tables[1].Rows.Count > 0)
                    {
                        sb.Append("<table style='width:85%; border:1px solid; border-collapse: collapse;margin-left: 10%;'>");
                        sb.Append("<tr style='background-color:rgb; border:1px solid;'> <th style='width:18%;border:1px solid;font-size: 11pt;'>USER</th><th style='width:13%;border:1px solid;font-size: 11pt;'> APPROVAL DATE </th><th style='width:5%;border:1px solid;font-size: 11pt;'>ACTION</th> <th style='width:49%;;font-size: 11pt; border:1px solid'>APPROVAL REMARKS</th></tr>");

                        for (int i = 0; i < myDataSet.Tables[1].Rows.Count; i++)
                        {
                            string s = myDataSet.Tables[1].Rows[i]["UserName"] + "(" + myDataSet.Tables[1].Rows[i]["LevelDesc"] + ")";
                            sb.Append("<tr>");
                            sb.Append("<td style='border:1px solid; width:18%;;font-size: 12px;'>" + s + "</td>");
                            sb.Append("<td style='border:1px solid; text-align:center; width:13%;font-size: 12px;'>" + myDataSet.Tables[1].Rows[i]["ApprovalDate"] + " </td>");
                            sb.Append("<td style='border:1px solid;text-align:center; width:5%;font-size: 12px;'> " + myDataSet.Tables[1].Rows[i]["AppStatus"] + "</td>");
                            sb.Append("<td style='border:1px solid; width:49%;font-size: 12px;'>" + myDataSet.Tables[1].Rows[i]["LevelRemark"].ToString().ToUpper() + " </td>");
                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        tblRemark.InnerHtml = sb.ToString();
                    }
                    if (myDataSet.Tables[1].Rows.Count > 0 && myDataSet.Tables[0].Rows.Count > 0)
                    {
                        int i = Convert.ToInt32(myDataSet.Tables[1].Rows[0]["DocType"].ToString());
                        lblapplicant.Text = Convert.ToString(myDataSet.Tables[0].Rows[0]["StudentName"].ToString());
                        lbltextstudentname.Text = "STUDENT NAME";
                        Lblstudentnamepopup.Text = myDataSet.Tables[0].Rows[0]["StudentName"].ToString();
                        lblrollno.Text = Convert.ToString(myDataSet.Tables[0].Rows[0]["RollNo"].ToString());
                        lbltextrollno.Text = "ROLL NO";
                        lblrollnopopup.Text = myDataSet.Tables[0].Rows[0]["RollNo"].ToString();
                        lblappliedfor.Text = Convert.ToString(myDataSet.Tables[0].Rows[0]["AppliedFor"].ToString());
                        lbltextappliedfor.Text = "APPLIED FOR";
                        lblappliedforpopup.Text = myDataSet.Tables[0].Rows[0]["AppliedFor"].ToString();
                        lblexamname.Text = Convert.ToString(myDataSet.Tables[0].Rows[0]["ExamName"].ToString());
                        lbltextexamname.Text = "EXAM NAME";
                        lblexampopup.Text = myDataSet.Tables[0].Rows[0]["ExamName"].ToString();
                        lblpreparedate.Text = Convert.ToString(myDataSet.Tables[0].Rows[0]["InsertionDate"].ToString());
                        lbltextFinalstatus.Text = "APPLICATION STATUS";
                        lblfinalstatus.Text = Convert.ToString(myDataSet.Tables[1].Rows[0]["FinalStatus"].ToString());
                        lblfinalstatuspopup.Text = myDataSet.Tables[1].Rows[0]["FinalStatus"].ToString();
                        lbldate.Text = Convert.ToString(DateTime.Now);
                        lblPopupapplicationNo.Text = myDataSet.Tables[0].Rows[0]["ApplicationID"].ToString();
                        //lblpopupExamYear.Text = myDataSet.Tables[0].Rows[0]["ExamYear"].ToString();
                        if (myDataSet.Tables[0].Rows[0]["ExamSession"].ToString() == "2")
                            lblpopupExamSession.Text = myDataSet.Tables[0].Rows[0]["ExamYear"].ToString() + "-SUMMER";
                        else
                            lblpopupExamSession.Text = myDataSet.Tables[0].Rows[0]["ExamYear"].ToString() + "-WINTER";


                        string[] d = new string[50];
                        int[] sw = new int[10];
                        int count = myDataSet.Tables[1].Rows.Count;
                        for (int s = 0; s < count; s++)
                        {
                            sw[s] = Convert.ToInt32(myDataSet.Tables[1].Rows[s]["ApprovalCount"]);
                            d[s] = Convert.ToString(myDataSet.Tables[1].Rows[s]["ApprovalDate"].ToString());

                            switch (sw[s])
                            {

                                case 0:
                                    lblpreparedate.Text = d[s];
                                    // level0div.Visible = true;//Circle
                                    thediv0.Visible = true;//Line
                                    lblpreparedatetext.Text = "DATE";
                                    lblprepareStatus.Text = myDataSet.Tables[1].Rows[0]["AppStatus"].ToString();
                                    Label1.Text = myDataSet.Tables[1].Rows[0]["LevelDesc"].ToString();
                                    break;
                                case 1:
                                    lbllevel1Date.Text = d[s];
                                    // level1div.Visible = true;
                                    thediv1.Visible = true;
                                    lbllevel1datetext.Text = "DATE";
                                    lbllevel1Status.Text = myDataSet.Tables[1].Rows[1]["AppStatus"].ToString();
                                    Label2.Text = myDataSet.Tables[1].Rows[1]["LevelDesc"].ToString();
                                    if (myDataSet.Tables[1].Rows[1]["IsRejected"].ToString() == "True")
                                        LblRemark.Text = "REJECTION REASON : " + myDataSet.Tables[1].Rows[1]["LevelRemark"].ToString().ToUpper();

                                    break;
                                case 2:
                                    lbllevel2Date.Text = d[s];
                                    //  level2div.Visible = true;
                                    thediv2.Visible = true;
                                    lbllevel2Datetext.Text = "DATE";
                                    lbllevel2Status.Text = myDataSet.Tables[1].Rows[2]["AppStatus"].ToString();
                                    Label3.Text = myDataSet.Tables[1].Rows[2]["LevelDesc"].ToString();
                                    if (myDataSet.Tables[1].Rows[2]["IsRejected"].ToString() == "True")
                                        LblRemark.Text = "REJECTION REASON : " + myDataSet.Tables[1].Rows[2]["LevelRemark"].ToString().ToUpper();
                                    break;
                                case 3:
                                    lbllevel3Date.Text = d[s];
                                    lbllevel3datetext.Text = "DATE";
                                    lbllevel3Status.Text = myDataSet.Tables[1].Rows[3]["AppStatus"].ToString();
                                    Label4.Text = myDataSet.Tables[1].Rows[3]["LevelDesc"].ToString();
                                    if (myDataSet.Tables[1].Rows[3]["IsRejected"].ToString() == "True")
                                        LblRemark.Text = "REJECTION REASON : " + myDataSet.Tables[1].Rows[3]["LevelRemark"].ToString().ToUpper();
                                    //  level3div.Visible = true;
                                    thediv3.Visible = true;
                                    break;
                                case 4:
                                    lbllevel4Date.Text = d[s];
                                    lbllevel4datetext.Text = "DATE";
                                    lbllevel4Status.Text = myDataSet.Tables[1].Rows[4]["AppStatus"].ToString();
                                    Label5.Text = myDataSet.Tables[1].Rows[4]["LevelDesc"].ToString();
                                    if (myDataSet.Tables[1].Rows[4]["IsRejected"].ToString() == "True")
                                        LblRemark.Text = "REJECTION REASON : " + myDataSet.Tables[1].Rows[4]["LevelRemark"].ToString().ToUpper();
                                    //  level4div.Visible = true;
                                    thediv4.Visible = true;
                                    break;
                                case 5:
                                    lbllevel5Date.Text = d[s];
                                    lbllevel5datetext.Text = "DATE";
                                    lbllevel5Status.Text = myDataSet.Tables[1].Rows[5]["AppStatus"].ToString();
                                    Label6.Text = myDataSet.Tables[1].Rows[5]["LevelDesc"].ToString();
                                    if (myDataSet.Tables[1].Rows[5]["IsRejected"].ToString() == "True")
                                        LblRemark.Text = "REJECTION REASON : " + myDataSet.Tables[1].Rows[5]["LevelRemark"].ToString().ToUpper();
                                    //  level5div.Visible = true;
                                    thediv5.Visible = true;
                                    break;
                                case 6:
                                    lbllevel6Date.Text = d[s];
                                    lbllevel6datetext.Text = "DATE";
                                    lbllevel6Status.Text = myDataSet.Tables[1].Rows[6]["AppStatus"].ToString();
                                    Label7.Text = myDataSet.Tables[1].Rows[6]["LevelDesc"].ToString();
                                    if (myDataSet.Tables[1].Rows[6]["IsRejected"].ToString() == "True")
                                        LblRemark.Text = "REJECTION REASON : " + myDataSet.Tables[1].Rows[6]["LevelRemark"].ToString().ToUpper();
                                    // level6div.Visible = true;
                                    thediv6.Visible = true;
                                    break;
                                case 7:
                                    lbllevel7Date.Text = d[s];
                                    lbllevel7datetext.Text = "DATE";
                                    lbllevel7Status.Text = myDataSet.Tables[1].Rows[7]["AppStatus"].ToString();
                                    Label8.Text = myDataSet.Tables[1].Rows[7]["LevelDesc"].ToString();
                                    if (myDataSet.Tables[1].Rows[7]["IsRejected"].ToString() == "True")
                                        LblRemark.Text = "REJECTION REASON : " + myDataSet.Tables[1].Rows[7]["LevelRemark"].ToString().ToUpper();
                                    //   level7div.Visible = true;
                                    thediv7.Visible = true;
                                    break;
                                default:
                                    break;
                            }

                            if (Convert.ToInt32(myDataSet.Tables[1].Rows[s]["ApprovalCount"]) == Convert.ToInt32(myDataSet.Tables[1].Rows[s]["MaxApprovalReqd"]))
                            {
                                //print Div
                                thedivprint.Visible = false; //this will true if print status field is true.
                            }
                            printdiv.Visible = true;
                        }
                        for (int k = 0; k <= Convert.ToInt32(myDataSet.Tables[1].Rows[0]["MaxApprovalReqd"]); k++)
                        {
                            if (k == 0)
                                level0div.Visible = true;//Circle
                            else if (k == 1)
                                level1div.Visible = true;//Circle
                            else if (k == 2)
                                level2div.Visible = true;//Circle
                            else if (k == 3)
                                level3div.Visible = true;//Circle
                            else if (k == 4)
                                level4div.Visible = true;//Circle
                            else if (k == 5)
                                level5div.Visible = true;//Circle
                            else if (k == 6)
                                level6div.Visible = true;//Circle
                            else if (k == 7)
                                level7div.Visible = true;//Circle    
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Entry Not Done For Given Application No.";
                        txtapplicationno.Focus();
                        return;
                    }

                    if (myDataSet.Tables[2].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(myDataSet.Tables[2].Rows[0]["PrintDateTime"].ToString()))
                        {
                            lblPrintDateText.Text = "DATE";
                            lblPrintDate.Text = myDataSet.Tables[2].Rows[0]["PrintDateTime"].ToString();
                            thedivprint.Visible = true;
                        }
                        else
                        {
                            lblPrintDateText.Text = lblPrintDate.Text = "";
                            thedivprint.Visible = false;
                        }
                    }

                    lblmsg.Text = "";
                }
                else
                {
                    lblmsg.Text = "No Record Found For Given Application No. Please Check";
                    txtapplicationno.Text = "";
                    return;
                }
            }
            else
            {
                lblmsg.Text = "Please Enter Application No.";
                return;
            }
        }
        catch
        { }
    }
    protected void txtsearch_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        Disable();
        clear();
        tblRemark.InnerHtml = null;
        if (txtsearch.Text == "GO BACK")
        {
            if (Request.QueryString["CallID"].ToString() == "1")
                Response.Redirect("FrmZeroLevel.aspx");
            else if (Request.QueryString["CallID"].ToString() == "2")
                Response.Redirect("FrmApproval.aspx");
            else if (Request.QueryString["CallID"].ToString() == "3")
                Response.Redirect("FrmApplicationStatus.aspx");
        }
        else
            GetApplicationProgress();
    }

}