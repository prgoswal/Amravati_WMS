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

public partial class UserCreation : System.Web.UI.Page
{
    PlUserCreation plUserCreation = new PlUserCreation();
    BlUserCreation blUserCreation = new BlUserCreation();
    string MACADRESS;
    [DllImport("Iphlpapi.dll")]
    private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
    [DllImport("Ws2_32.dll")]
    private static extern Int32 inet_addr(string ip); 
    static int lngid = 0;
    static  int val = 0;  

    DataTable dt;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    static int id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlUserType.Focus();
            BindUserType();
            divtable.Visible = false;

            link2day.ForeColor = Color.Red;
            link7day.ForeColor = Color.Red;
            linkall.ForeColor = Color.Red;
        }
    }
    private async void BindUserType()   // Bind User Type Purpose From "SPGetAllMaster", Ind=6, @ItemId From UserType
    {
        int i = Convert.ToInt32(Session["UserTypeId"]);
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);//DataAcces.Url==http://prg90/AwAPI/
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/Json"));
        var uri = string.Format("api/UserCreation/GetAllRegister/?Ind={0}&ItemId={1}", 16, i);
        var response = HClient.GetAsync(uri).Result;
        dt = new DataTable();
        if (response.IsSuccessStatusCode)
        {
            var productJsonString = await response.Content.ReadAsStringAsync();
            dt = JsonConvert.DeserializeObject<DataTable>(productJsonString);

            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0][0] = "0";
            dt.Rows[0][1] = "Select User Profile";
            //var getdata = response.Content.ReadAsAsync<IEnumerable<PlUserCreation>>();
            ddlUserType.DataSource = dt;
            ddlUserType.DataValueField = "ItemID";
            ddlUserType.DataTextField = "LevelDescription";
            ddlUserType.DataBind();

        }
    }

    private async void GetAllUserInGrid()   // Bind User Type Purpose From "SPGetAllMaster", Ind=6, @ItemId From UserType
    {
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/Json"));
        var uri = string.Format("api/UserCreation/GetAllUserInGrid/?Ind={0}", 14);
        var response = HClient.GetAsync(uri).Result;
        DataSet ds = new DataSet();
        if (response.IsSuccessStatusCode)
        {
            var productJsonString = await response.Content.ReadAsStringAsync();
            ds = JsonConvert.DeserializeObject<DataSet>(productJsonString);
          
            if (val == 1)
            {
                lblmsg.Text = "";
                Clear();

                link2day.ForeColor = Color.Green;         
                link7day.ForeColor = Color.Red;
                linkall.ForeColor = Color.Red;
             
                grddata.DataSource = null;
                grddata.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    divtable.Visible = true;
                    grddata.Visible = true;
                    grddata.DataSource = ds.Tables[0];
                    grddata.DataBind();
                }
                else 
                { 
                    lblmsg.Text = "No User(s) Found.";
                    divtable.Visible = false;
                    grddata.Visible = false;
                }
            }
            else if (val == 2)
            {
                lblmsg.Text = "";
                Clear();

                link7day.ForeColor = Color.Green;
                link2day.ForeColor = Color.Red;          
                linkall.ForeColor = Color.Red;

                grddata.DataSource = null;
                grddata.DataBind();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    divtable.Visible = true;
                    grddata.Visible = true;
                    grddata.DataSource = ds.Tables[1];
                    grddata.DataBind();
                }
                else
                {
                    lblmsg.Text = "No User(s) Found.";
                    divtable.Visible = false;
                    grddata.Visible = false;
                }
            }
            else if(val==3)
            {
                lblmsg.Text = "";
                Clear();

                linkall.ForeColor = Color.Green;
                link2day.ForeColor = Color.Red;
                link7day.ForeColor = Color.Red;
       
                grddata.DataSource = null;
                grddata.DataBind();
                if (ds.Tables[2].Rows.Count > 0)
                {
                    divtable.Visible = true;
                    grddata.Visible = true;
                    grddata.DataSource = ds.Tables[2];
                    grddata.DataBind();
                }
                else
                {
                    lblmsg.Text = "No User(s) Found.";
                    divtable.Visible = false;
                    grddata.Visible = false;
                }
            }

        }
    }

    protected async void linkSave_Click(object sender, EventArgs e)
    {
        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));

       // GetMACAddress();
        if (lngid == 0)
        {           
            plUserCreation.Ind = 8;
            plUserCreation.MacAddress = txt1.Text.ToUpper() + "-" + txt2.Text.ToUpper() + "-" + txt3.Text.ToUpper() + "-" + txt4.Text.ToUpper() + "-" + txt5.Text.ToUpper() + "-" + txt6.Text.ToUpper();
            plUserCreation.UserType = Convert.ToInt32(ddlUserType.SelectedValue);
            plUserCreation.UserName = txtUserName.Text.ToUpper();
           
            plUserCreation.EmployeeID = txtempID.Text.ToUpper();
            plUserCreation.ContactNo = txtContactNo.Text;
            plUserCreation.Email = txtEmailAddress.Text;
            plUserCreation.LoginId = txtLoginId.Text;
            plUserCreation.Pwd = EncodePassword(txtLoginId.Text.ToString().Substring(0, 4) + "@123");// + txtContactNo.Text.ToString().Substring(7, 3));
            plUserCreation.IsOutSideUser = 0;
            plUserCreation.IsActive = 1;
            plUserCreation.CreationDate = DateTime.Now;
            plUserCreation.UserLoginStatus = 0;
            if (ddluservalidity.SelectedIndex == 1)
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(7));
            }
            else if (ddluservalidity.SelectedIndex == 2)
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(15));
            }
            else if (ddluservalidity.SelectedIndex == 3)
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(30));
            }
            else if (ddluservalidity.SelectedIndex == 4)
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(90));
            }
            else if (ddluservalidity.SelectedIndex == 5)
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(180));
            }
            else
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(21900));
            }
            plUserCreation.LoginId = txtLoginId.Text;
            var uri = "api/UserCreation/InsertUserEntry/";
            var response = HClient.PostAsJsonAsync(uri, plUserCreation).Result;

            DataTable dt = new DataTable();
            if (response.IsSuccessStatusCode)
            {

                var productJsonString = await response.Content.ReadAsStringAsync();
                dt = JsonConvert.DeserializeObject<DataTable>(productJsonString);

                if (dt.Rows[0][0].ToString() == "0")
                {
                    lblmsg.Text = "User Already Exist";
                }
                else
                {
                    Clear();
                   // Server.Transfer("UserCreation.aspx");
                    lblmsg.Text = "User Created Successfully With Login Password :- " + DecodePassword(plUserCreation.Pwd) + "";
                }
                
            }
            //   }

        }
        else
        {
            plUserCreation.Ind = 15;
            //  plUserCreation.UserValidity = Convert.ToDateTime(txtvalidity.Text);//Convert.ToDateTime(txtvalidity.Text);
            if (ddluservalidity.SelectedIndex == 1)
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(7));
            }
            else if (ddluservalidity.SelectedIndex == 2)
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(15));
            }
            else if (ddluservalidity.SelectedIndex == 3)
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(30));
            }
            else if (ddluservalidity.SelectedIndex == 4)
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(90));
            }
            else if (ddluservalidity.SelectedIndex == 5)
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(180));
            }
            else
            {
                plUserCreation.UserValidity = Convert.ToDateTime(DateTime.Now.AddDays(21900));
            }
            plUserCreation.EmployeeID = txtempID.Text;
           
            plUserCreation.MacAddress = txt1.Text.ToUpper() + "-" + txt2.Text.ToUpper() + "-" + txt3.Text.ToUpper() + "-" + txt4.Text.ToUpper() + "-" + txt5.Text.ToUpper() + "-" + txt6.Text.ToUpper();
            var uri = string.Format("api/UserCreation/UpdateUserValidity?Ind={0}&UserId={1}&UserValidity={2}&MacAddress={3}&EmployeeID={4}", plUserCreation.Ind, id, plUserCreation.UserValidity, plUserCreation.MacAddress,plUserCreation.EmployeeID);
           
            var response = HClient.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var getdata = response.Content.ReadAsAsync<IEnumerable<PlUserCreation>>().Result;
                if (getdata.Count() > 0)
                {
                    Clear();
                    GetAllUserInGrid();
                    lblmsg.Text = "updated";
                    lngid = 0;
                   // Server.Transfer("UserCreation.aspx");
                }
                else { lblmsg.Text = "Not updated"; }
            }
        }
    }
    protected void linkClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        lngid = 0;
        if (ddlUserType.SelectedIndex > 0)
            ddlUserType.SelectedIndex = 0;

        if (ddluservalidity.SelectedIndex > 0)
            ddluservalidity.SelectedIndex = 0;
        txtContactNo.Text = txtEmailAddress.Text = txtLoginId.Text =txt1.Text = txt2.Text = txt3.Text = txt4.Text = txt5.Text = txt6.Text= "";
        txtUserName.Text = "";
        
        txtempID.Text = "";
        txtUserName.Enabled = true;
        txtEmailAddress.Enabled = true;
        txtContactNo.Enabled = true;      
        txtLoginId.Enabled = true;
        lblmsg.Text = "";
        divtable.Visible = false;
        grddata.Visible = false;
        grddata.DataSource = null;
        grddata.DataBind();
    }

    private String EncodePassword(String Pwd)
    {
        int Calc = 0; String FnlStr = "";
        for (int i = 1; i <= Pwd.Length; i++)
        {
            if (i < 2)
            {
                Calc = 100;
            }
            else if (i < 4)
            {
                Calc = 200;
            }
            else if (i < 6)
            {
                Calc = 300;
            }
            else if (i < 8)
            {
                Calc = 400;
            }
            else if (i < 10)
            {
                Calc = 500;
            }
            byte[] bt = Encoding.ASCII.GetBytes(Pwd.ToString().Substring(i - 1, 1));
            FnlStr = FnlStr + Convert.ToInt32(bt[0] + Calc + i).ToString();
        }
        return FnlStr;
    }

    public String DecodePassword(String Pwd)
    {
        int Calc = 0; String FnlStr = "";
        int i = 1; int j = 1; int A = 0; byte[] bt; String Str = ""; ;
        while (i <= Pwd.Length)
        {
            if (j < 2)
            {
                Calc = 100;
            }
            else if (j < 4)
            {
                Calc = 200;
            }
            else if (j < 6)
            {
                Calc = 300;
            }
            else if (j < 8)
            {
                Calc = 400;
            }
            else if (j < 10)
            {
                Calc = 500;
            }
            A = Convert.ToInt32(Pwd.ToString().Substring(i - 1, 3)) - Calc - j;
            bt = new byte[1];
            bt[0] = (byte)A;
            Str = Encoding.ASCII.GetString(bt);
            FnlStr = FnlStr + Str;
            i = i + 3;
            j++;
        }
        return FnlStr;
    }
    protected void gvDataGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {  
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
    }
  
    protected void rbuservalidityexpire_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void link2day_Click(object sender, EventArgs e)
    {
       // grddata = null;
        val = 1;
        GetAllUserInGrid();
    }
    protected void link7day_Click(object sender, EventArgs e)
    {
        val = 2;
      //  grddata = null;
        GetAllUserInGrid();
    }
    public void disable()
    {
        txtUserName.Enabled = false;
        txtEmailAddress.Enabled = false;
        txtContactNo.Enabled = false;      
        txtLoginId.Enabled = false;

    }
  
    protected void grddata_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(DataAcces.Url);
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/Json"));
            if (e.CommandName == "EditData")
            {
                lblmsg.Text = "";
                disable();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            
                int index = row.RowIndex;
                id = Convert.ToInt32(grddata.DataKeys[index].Value);
                lngid = id;

             
                var uri = string.Format("api/UserCreation/GetUpdateValidity/?Ind={0}&UserId={1}", 16, id);
                var response = HClient.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    var itemdata = response.Content.ReadAsAsync<IEnumerable<PlUserCreation>>().Result;
                    foreach (var item in itemdata)
                    {
                        txtUserName.Text = item.UserName;
                        txtEmailAddress.Text = item.Email;
                        txtContactNo.Text = item.ContactNo;
                        txtempID.Text = item.EmployeeID;
                      
                        txtLoginId.Text = item.LoginId;
                        string mac = item.MacAddress;
                        if (mac != "123")
                        {
                            if (txt1.Text == "" || txt1.Text==null)
                            {
                                txt1.Text = "00";
                            }
                            else
                            {
                                txt1.Text = mac.Substring(0, 2);
                            }
                            if (txt2.Text == "" || txt2.Text == null)
                            {
                                txt2.Text = "00";
                            }
                            else
                            {
                                txt2.Text = mac.Substring(3, 2);
                            }
                            if (txt3.Text == "" || txt3.Text == null)
                            {
                                txt3.Text = "00";
                            }
                            else
                            {
                                txt3.Text = mac.Substring(6, 2);
                            } if (txt4.Text == "" || txt4.Text == null)
                            {
                                txt4.Text = "00";
                            }
                            else
                            {
                                txt4.Text = mac.Substring(9, 2);
                            } if (txt5.Text == "" || txt5.Text == null)
                            {
                                txt5.Text = "00";
                            }
                            else
                            {
                                txt5.Text = mac.Substring(12, 2);
                            } if (txt6.Text == "" || txt6.Text == null)
                            {
                                txt6.Text = "00";
                            }
                            else
                            {
                                txt6.Text = mac.Substring(15, 2);
                            }
                        }
                        else
                        {
                            txt1.Text = "00";
                            txt2.Text = "00";
                            txt3.Text = "00";
                            txt4.Text = "00";
                            txt5.Text = "00";
                            txt6.Text = "00";
                        }
                        
                        ddlUserType.SelectedIndex = item.UserType - 51;

                    }

                }
            }
      
    }
    protected void txt1_TextChanged(object sender, EventArgs e)
    {
       
    }   
    protected void linkall_Click(object sender, EventArgs e)
    {
        val = 3;
        GetAllUserInGrid();
    }
    public string GetMACAddress()
    {
        try
        {
            string userip = Request.UserHostAddress;
            string strClientIP = Request.UserHostAddress.ToString().Trim();
            Int32 ldest = inet_addr(strClientIP);
            Int32 lhost = inet_addr("");
            Int64 macinfo = new Int64();
            Int32 len = 6;
            int res = SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");
     
            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }

            string mac_dest = "";

            for (int i = 0; i < 11; i++)
            {
                if (0 == (i % 2))
                {
                    if (i == 10)
                    {
                        mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                    else
                    {
                        mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                }
            }
            MACADRESS = mac_dest;

        }
        catch (Exception err)
        {
            Response.Write(err.Message);
        }

        return MACADRESS;
    }

    protected void lnkmac_Click(object sender, EventArgs e)
    {
        GetMACAddress();     
        txt1.Text = MACADRESS.Substring(0, 2);
        txt2.Text = MACADRESS.Substring(3, 2);
        txt3.Text = MACADRESS.Substring(6, 2);
        txt4.Text = MACADRESS.Substring(9, 2);
        txt5.Text = MACADRESS.Substring(12, 2);
        txt6.Text = MACADRESS.Substring(15, 2);
        ddluservalidity.Focus();
    }
}
