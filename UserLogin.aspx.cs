using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Management;
using System.Management.Instrumentation;

public partial class UserLogin : System.Web.UI.Page
{
    BlTRRegiter BlTrReg = new BlTRRegiter();
    plTrRegister plTrReg = new plTrRegister();
    DataTable dt;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    plTrRegister objpltrRegister = new plTrRegister();
    string macaddress = "";

    HttpClient HClient = new HttpClient();

    protected void Page_Load(object sender, EventArgs e)
    {
        //  Response.Write("<script>window.name=Date(); $('#hfvalue') .val(window .name);  </script>");
        txtLoginId.Focus();
        if (!IsPostBack)
        {
            pnlpassword.Visible = false;
            pnllogout.Visible = false;
            GetMACAddress();

        }
    }

    string MACADRESS;
    [DllImport("Iphlpapi.dll")]
    private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
    [DllImport("Ws2_32.dll")]
    private static extern Int32 inet_addr(string ip);

    public string GetMACAddress()
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
        return MACADRESS;
    }

    //public string Mac()
    //{
    //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
    //MACADRESS = nics[0].GetPhysicalAddress().ToString();
    //return MACADRESS;
    //-----------------------------------------------------------

    //ManagementObjectSearcher objMOS = new ManagementObjectSearcher("Win32_NetworkAdapterConfiguration");
    //ManagementObjectCollection objMOC = objMOS.Get();
    //string MACAddress = String.Empty;
    //foreach (ManagementObject objMO in objMOC)
    //{
    //    if (MACAddress == String.Empty) // only return MAC Address from first card   
    //    {
    //        MACAddress = objMO["MacAddress"].ToString();
    //    }
    //    objMO.Dispose();
    //}
    //MACAddress = MACAddress.Replace(":", "");
    //return MACAddress;
    //--------------------------------------
    //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
    //String sMacAddress = string.Empty;
    //foreach (NetworkInterface adapter in nics)
    //{
    //    if (sMacAddress == String.Empty)// only return MAC Address from first card
    //    {
    //        IPInterfaceProperties properties = adapter.GetIPProperties();
    //        sMacAddress = adapter.GetPhysicalAddress().ToString();
    //        MACADRESS = sMacAddress;
    //    }
    //} return MACADRESS;
    //------------------------------------------------------------
    //ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
    //ManagementObjectCollection moc = mc.GetInstances();
    //string MACAddress = String.Empty;
    //foreach (ManagementObject mo in moc)
    //{
    //    if (MACAddress == String.Empty) // only return MAC Address from first card    
    //    {
    //        if ((bool)mo["IPEnabled"] == true) MACAddress = mo["MacAddress"].ToString();
    //    }
    //    mo.Dispose();
    //}

    //MACAddress = MACAddress.Replace(":", "");
    //MACADRESS = MACAddress;
    //----------------------------------------------
    //     return MACADRESS;
    //}

    //public static void ShowNetworkInterfaces()
    //{
    //    IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
    //    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
    //    Console.WriteLine("Interface information for {0}.{1}     ",
    //            computerProperties.HostName, computerProperties.DomainName);
    //    if (nics == null || nics.Length < 1)
    //    {
    //        Console.WriteLine("  No network interfaces found.");
    //        return;
    //    }

    //    Console.WriteLine("  Number of interfaces .................... : {0}", nics.Length);
    //    foreach (NetworkInterface adapter in nics)
    //    {
    //        IPInterfaceProperties properties = adapter.GetIPProperties(); //  .GetIPInterfaceProperties();
    //        Console.WriteLine();
    //        Console.WriteLine(adapter.Description);
    //        Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
    //        Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
    //        Console.Write("  Physical address ........................ : ");
    //        PhysicalAddress address = adapter.GetPhysicalAddress();
    //        byte[] bytes = address.GetAddressBytes();
    //        for (int i = 0; i < bytes.Length; i++)
    //        {
    //            // Display the physical address in hexadecimal.
    //            Console.Write("{0}", bytes[i].ToString("X2"));
    //            // Insert a hyphen after each byte, unless we are at the end of the 
    //            // address.
    //            if (i != bytes.Length - 1)
    //            {
    //                Console.Write("-");
    //            }
    //        }
    //        Console.WriteLine();
    //    }
    //}

    void PopupPrint(bool isDisplayPopupPrint)
    {

        StringBuilder builder = new StringBuilder();
        if (isDisplayPopupPrint)
        {
            builder.Append("<script language=JavaScript> ShowPopupPrint(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
        }
        else
        {
            builder.Append("<script language=JavaScript> HidePopupPrint(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        lblmsg1.Text = "";
        if (Convert.ToString(Session["UserLoginId"]) != "" && Convert.ToString(Session["UserLoginId"]) != null && Convert.ToString(Session["UserLoginPwd"]) != "" && Convert.ToString(Session["UserLoginPwd"]) != null)
        {
            HttpClient hclient = new HttpClient();
            hclient.BaseAddress = new Uri(DataAcces.Url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            objpltrRegister.Ind = 11;
            objpltrRegister.UserLoginId = Convert.ToString(Session["UserLoginId"]);
            objpltrRegister.UserLoginPwd = EncodePassword(txtLoginPwd.Text);
            // string pwd = DecodePassword(objpltrRegister.UserLoginPwd);
            var uri = string.Format("api/Login/UpdateInvalidAttempts/?Ind={0}&UserLoginId={1}&UserLoginPwd={2}", objpltrRegister.Ind, objpltrRegister.UserLoginId, objpltrRegister.UserLoginPwd);
            var resp1 = hclient.GetAsync(uri).Result;

            if (resp1.IsSuccessStatusCode)
            {
                var get = resp1.Content.ReadAsAsync<IEnumerable<plTrRegister>>().Result;
                foreach (var a in get)
                {
                    Session["UserType"] = Convert.ToString(a.ItemDesc);
                }

                if (get.Count() > 0)
                {
                    Session["tabId"] = hfvalue.Value.ToString();
                    if (Convert.ToString(Session["HistoryCnt"]) == "0")
                    {
                        Session["st"] = "1";
                        Server.Transfer("ChangePassword.aspx");
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["UserTypeId"]) > 53)
                        {
                            Response.Redirect("FrmApproval.aspx");
                        }
                        else
                        {
                            Response.Redirect("Home.aspx");
                        }
                        // Server.Transfer("Home.aspx");
                    }
                }
                else
                {
                    lblmsg1.Text = "Please Check Password!";
                    txtLoginPwd.Text = "";
                    txtLoginPwd.Focus();
                    lblmsg1.Visible = true;
                }
            }
        }
        else
        {
            Server.Transfer("UserLogin.aspx");
        }

        #region
        //GetMACAddress();
        //HClient.BaseAddress = new Uri(DataAcces.Url);
        //HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //objpltrRegister = new plTrRegister();
        //objpltrRegister.UserLoginId = txtLoginId.Text;
        //objpltrRegister.LoginPwd = EncodePassword(txtLoginPwd.Text);
        //var uri = string.Format("api/Login/Get/?UserLoginID={0}&Password={1}", objpltrRegister.UserLoginId, objpltrRegister.LoginPwd);//for example macaddress is 123 we replace later.
        //var response = HClient.GetAsync(uri).Result;

        //if (response.IsSuccessStatusCode)
        //{
        //    var getdata1 = response.Content.ReadAsAsync<IEnumerable<plTrRegister>>().Result;
        //    if (getdata1.Count() > 0)
        //    {
        //        foreach (var a in getdata1)
        //        {

        //            //objpltrRegister.Ind = 3;
        //            //objpltrRegister.UserId = a.UserId;
        //            //var v = string.Format("api/Login/UpdateActiveStatus/?Ind={0}&UserId={1}", objpltrRegister.Ind, objpltrRegister.UserId);

        //            //var resp = HClient.PutAsJsonAsync(v, objpltrRegister).Result;
        //            //if (resp.IsSuccessStatusCode)
        //            //{
        //            //    var get = resp.Content.ReadAsAsync<IEnumerable<plTrRegister>>().Result;
        //            //    if (get != null)
        //            //    {
        //                    objpltrRegister.Ind = 6;
        //                    objpltrRegister.UserId = a.UserId;
        //                    objpltrRegister.NewPassword =EncodePassword(txtLoginPwd.Text);
        //                    var uri1 = string.Format("api/Login/CheckPassword/?Ind={0}&UserId={1}&NewPwd={2}", objpltrRegister.Ind, objpltrRegister.UserId, objpltrRegister.NewPassword);//for example macaddress is 123 we replace later.
        //                    var response1 = HClient.GetAsync(uri1).Result;

        //                    if (response1.IsSuccessStatusCode)
        //                    {
        //                        IEnumerable<plTrRegister> getdata = response1.Content.ReadAsAsync<IEnumerable<plTrRegister>>().Result;



        //                        if (getdata.Count() == 0)
        //                        {

        //                           // PopupPrint(true);
        //                        }
        //                        else
        //                        {

        //                        }
        //                    }

        //                    // Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "windowOpen()", true);  
        //            //    }
        //            //}

        //        }
        //    }
        //    else
        //    {
        //        //HttpClient hc = new HttpClient();
        //        //hc.BaseAddress = new Uri(DataAcces.Url);
        //        //HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));

        //    }
        //}
        //else
        //{
        //    Server.Transfer("UserLogin.aspx");
        //}
        //// }
        #endregion
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

    protected async void btnnext_Click(object sender, EventArgs e)
    {
        lblmsg1.Text = "";
        GetMACAddress();
        //  Mac();
        HttpClient hclient = new HttpClient();
        hclient.BaseAddress = new Uri(DataAcces.Url);
        hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        DataSet mydataset = null;
        string loginid = txtLoginId.Text;
        objpltrRegister.Ind = 12;
        objpltrRegister.MacAddress = MACADRESS;

        var uri = string.Format("api/Login/GetUserLoginID/?Ind={0}&UserLoginId={1}&MacAddress={2}", objpltrRegister.Ind, loginid, objpltrRegister.MacAddress);

        var response = hclient.GetAsync(uri).Result;
        if (response.IsSuccessStatusCode)
        {
            var productJsonString = await response.Content.ReadAsStringAsync();
            mydataset = JsonConvert.DeserializeObject<DataSet>(productJsonString);

            if (mydataset.Tables[0].Rows.Count > 0)
            {
                if (mydataset.Tables[0].Rows[0]["MacAddress"].ToString() == objpltrRegister.MacAddress)
                {
                    if (mydataset.Tables[0].Rows[0]["LockInd"].ToString().ToUpper() != "TRUE")
                    {
                        if (Convert.ToDateTime(mydataset.Tables[0].Rows[0]["UserValidity"].ToString()) >= DateTime.Now.Date)
                        {
                            if (mydataset.Tables[0].Rows[0]["IsActive"].ToString().ToUpper() == "TRUE")
                            {
                                Session["UserId"] = mydataset.Tables[0].Rows[0]["UserId"].ToString();
                                Session["UserValidity"] = mydataset.Tables[0].Rows[0]["UserValidity"].ToString();
                                Session["UserTypeId"] = mydataset.Tables[0].Rows[0]["UserTypeId"].ToString();
                                Session["UserName"] = mydataset.Tables[0].Rows[0]["UserName"].ToString();
                                Session["LoginDateTime"] = DateTime.Now;
                                Session["HistoryCnt"] = mydataset.Tables[0].Rows[0]["HistoryCnt"].ToString();
                                Session["UserLoginPwd"] = mydataset.Tables[0].Rows[0]["UserLoginPwd"].ToString();
                                Session["UserLoginId"] = mydataset.Tables[0].Rows[0]["UserLoginId"].ToString();
                                if (mydataset.Tables[0].Rows[0]["UserLoginStatus"].ToString().ToUpper() == "TRUE")
                                {
                                    lbllogout.Text = txtLoginId.Text;
                                    pnllogout.Visible = true;
                                    pnlpassword.Visible = false;
                                    pnlusernmae.Visible = false;
                                    txtlogoutpassword.Focus();
                                }
                                else
                                {
                                    if (Convert.ToInt32(mydataset.Tables[1].Rows[0]["LoginCnt"]) == 0)
                                    {
                                        lbluser.Text = "Welcome : " + txtLoginId.Text;
                                        pnlusernmae.Visible = false;
                                        pnlpassword.Visible = true;
                                        txtLoginPwd.Focus();
                                    }
                                    else
                                    {
                                        lblmsg1.Text = "This Machine Is Already Used By Login-ID : " + mydataset.Tables[1].Rows[0]["UserLoginID"].ToString();
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                lblmsg1.Text = "User are not Active.";
                                return;
                            }
                        }
                        else
                        {
                            lblmsg1.Text = "User Login Date Is Expired.Please Contact to Admin.";
                            return;
                        }
                    }
                    else
                    {
                        lblmsg1.Text = "User Is Blocked. Please Contact to Admin";
                        return;
                    }
                }
                else
                {
                    lblmsg1.Text = "MAC Address Not Valid" + " - DataBase - " + mydataset.Tables[0].Rows[0]["MacAddress"].ToString() + " - Machine - " + objpltrRegister.MacAddress;
                    return;
                }
            }
            else
            {
                lblmsg1.Text = "Login ID Not Valid";
                return;
            }

            mydataset = null; productJsonString = null;
        }
        response = null; uri = null;
    }

    protected void btnlogout_Click(object sender, EventArgs e)
    {
        lblmsg1.Text = "";
        if (Convert.ToString(Session["UserLoginPwd"]) == Convert.ToString(EncodePassword(txtlogoutpassword.Text)))
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(DataAcces.Url);
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
            objpltrRegister.Ind = 3;
            objpltrRegister.UserId = Convert.ToInt32(Session["UserId"]);

            var v = string.Format("api/Login/UpdateActiveStatus/?Ind={0}&UserId={1}", objpltrRegister.Ind, objpltrRegister.UserId);
            var resp = hc.GetAsync(v).Result;
            if (resp.IsSuccessStatusCode)
            {
                var get = resp.Content.ReadAsAsync<IEnumerable<plTrRegister>>().Result;
                if (get.Count() > 0)
                {
                    Session["USERNAME"] = null;
                    Session["UserId"] = null;
                    Session["UserType"] = null;
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect("UserLogin.aspx");
                    Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "closeWin()", true);
                }
                get = null;
            }
            resp = null; v = null;
        }
        else
        {
            lblmsg1.Text = "password not matched";
            return;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

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

    protected void btnsavepassword_Click(object sender, EventArgs e)
    {
        HClient.BaseAddress = new Uri(DataAcces.Url);
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
        objpltrRegister.Ind = 16;
        objpltrRegister.UserId = Convert.ToInt32(Session["UserId"]);
        objpltrRegister.LoginPwd = EncodePassword(txtConfirmPwd.Text);

        var v = string.Format("api/Login/ChangePasswordFirst/?Ind={0}&UserId={1}&LoginPwd={2}", objpltrRegister.Ind, objpltrRegister.UserId, objpltrRegister.LoginPwd);
        var resp = HClient.PostAsJsonAsync(v, objpltrRegister).Result;
        if (resp.IsSuccessStatusCode)
        {
            var get = resp.Content.ReadAsAsync<IEnumerable<plTrRegister>>().Result;
            get = null;
        }
        resp = null; v = null;
    }

    protected void linklogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserLogin.aspx");
    }
    protected void linklogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserLogin.aspx");
    }
}