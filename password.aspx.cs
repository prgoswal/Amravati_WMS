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

public partial class password : System.Web.UI.Page
{
    string MACADRESS;
    [DllImport("Iphlpapi.dll")]
    private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
    [DllImport("Ws2_32.dll")]
    private static extern Int32 inet_addr(string ip);
    protected void Page_Load(object sender, EventArgs e)
    {
        string k = "198302312409415470456558560";
      string ss=  Decode(k);
        //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        //String sMacAddress = string.Empty;
        //foreach (NetworkInterface adapter in nics)
        //{
        //    if (sMacAddress == String.Empty)// only return MAC Address from first card  
        //    {
        //        IPInterfaceProperties properties = adapter.GetIPProperties();
        //        sMacAddress = adapter.GetPhysicalAddress().ToString();
        //        lable1.Text = "lable1" + sMacAddress;
        //    }
        //}

        //mac1();

        //mac3();
        //mac4();
        //GetMACAddress();
        //ShowNetworkInterfaces();
        //getmac();
    }
    public void getmac()
    {
        ManagementClass objMC = new
            ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection objMOC = objMC.GetInstances();

        foreach (ManagementObject objMO in objMOC)
        {
            if (!(bool)objMO["ipEnabled"])
                continue;

            Label8.Text = "Getmac" + ((string)objMO["MACAddress"]);
        }
    }
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
        //if (mac_src == "0")
        //{
        //    //if (userip == "127.0.0.1")
        //    //   // Response.Write("visited Localhost!");
        //    //else
        //        //Response.Write("the IP from" + userip + "" + "<br>");
        //}

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
        Label1.Text = "label1-getmacaddress" + MACADRESS;
        return MACADRESS;
    }
    public void mac1()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        Label2.Text = "label2  " + nics[0].GetPhysicalAddress().ToString();

    }
    //public void mac2()
    //{
    //    ManagementObjectSearcher objMOS = new ManagementObjectSearcher("Win32_NetworkAdapterConfiguration");
    //    ManagementObjectCollection objMOC = objMOS.Get();
    //    string MACAddress = String.Empty;
    //    foreach (ManagementObject objMO in objMOC)
    //    {
    //        if (MACAddress == String.Empty) // only return MAC Address from first card   
    //        {
    //            MACAddress = objMO["MacAddress"].ToString();
    //        }
    //        objMO.Dispose();
    //    }
    //    Label3.Text = MACAddress.Replace(":", "");    
    //}
    public void mac3()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
                MACADRESS = sMacAddress;
            }
        }
        Label4.Text = "label4    " + MACADRESS;
    }
    public void mac4()
    {
        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection moc = mc.GetInstances();
        string MACAddress = String.Empty;
        foreach (ManagementObject mo in moc)
        {
            if (MACAddress == String.Empty) // only return MAC Address from first card    
            {
                if ((bool)mo["IPEnabled"] == true) MACAddress = mo["MacAddress"].ToString();
            }
            mo.Dispose();
        }

        Label5.Text = "label5      " + MACAddress.Replace(":", "");

    }
    public static void ShowNetworkInterfaces()
    {
        IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        Console.WriteLine("Interface information for {0}.{1}     ",
                computerProperties.HostName, computerProperties.DomainName);
        if (nics == null || nics.Length < 1)
        {
            Console.WriteLine("  No network interfaces found.");
            return;
        }

        Console.WriteLine("  Number of interfaces .................... : {0}", nics.Length);
        foreach (NetworkInterface adapter in nics)
        {
            IPInterfaceProperties properties = adapter.GetIPProperties(); //  .GetIPInterfaceProperties();
            Console.WriteLine();
            Console.WriteLine(adapter.Description);
            Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
            Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
            Console.Write("  Physical address ........................ : ");
            PhysicalAddress address = adapter.GetPhysicalAddress();
            byte[] bytes = address.GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                // Display the physical address in hexadecimal.
                Console.Write("{0}", bytes[i].ToString("X2"));
                // Insert a hyphen after each byte, unless we are at the end of the 
                // address.
                if (i != bytes.Length - 1)
                {
                    Console.Write("-");
                }
            }
            Console.WriteLine();
        }
    }

    public sealed class NativeMethods
    {
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);

        private NativeMethods() { }

        public static string GetMacAddress(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
            {
                throw new ArgumentNullException("ipAddress");
            }
            IPAddress IP = IPAddress.Parse(ipAddress);
            byte[] macAddr = new byte[6];
            uint macAddrLen = (uint)macAddr.Length;

            if (SendARP((int)IP.Address, 0, macAddr, ref macAddrLen) != 0)
            {
                throw new Exception("ARP command failed");
            }
            string[] str = new string[(int)macAddrLen];
            for (int i = 0; i < macAddrLen; i++)
            {
                str[i] = macAddr[i].ToString("x2");
            }

            return string.Join(":", str).ToUpper();
        }

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

    //protected void btnencode_Click(object sender, EventArgs e)
    //{
    //    //int Calc = 0; String FnlStr = "";
    //    //for (int i = 1; i <= txt.Text.Length; i++)
    //    //{
    //    //    if (i < 2)
    //    //    {
    //    //        Calc = 100;
    //    //    }
    //    //    else if (i < 4)
    //    //    {
    //    //        Calc = 200;
    //    //    }
    //    //    else if (i < 6)
    //    //    {
    //    //        Calc = 300;
    //    //    }
    //    //    else if (i < 8)
    //    //    {
    //    //        Calc = 400;
    //    //    }
    //    //    else if (i < 10)
    //    //    {
    //    //        Calc = 500;
    //    //    }
    //    //    byte[] bt = Encoding.ASCII.GetBytes(txt.Text.ToString().Substring(i - 1, 1));
    //    //    FnlStr = FnlStr + Convert.ToInt32(bt[0] + Calc + i).ToString();


    //    //}
    //    //lbl.Text = FnlStr;
    //}

    //protected void btndecode_Click(object sender, EventArgs e)
    //{
    //    string s = "198302312409415470456558";
    //    Decode(s);
    //SqlConnection con = new SqlConnection("Data Source=MLCV004; User Id=sa; Password=odpserver550810998@ ; Initial Catalog=UnivAmravati;Timeout=0;");
    //SqlCommand cmd = new SqlCommand("Select * From MstUser", con);
    //SqlDataAdapter da = new SqlDataAdapter(cmd);
    //DataTable dt = new DataTable();
    //da.Fill(dt);
    //List<plTrRegister> Decodes=new List<plTrRegister>();
    //for (int i = 0; i < dt.Rows.Count; i++)
    //{
    //   string s = dt.Rows[i]["UserLoginPwd"].ToString();
    //   //string [] dec = new string();
    //   plTrRegister dec = new plTrRegister();
    //   dec.LoginPwd = Decode(s);
    //   Decodes.Add(dec);            
    //    // SqlCommand cmd2 =new SqlCommand("update MstUser UserLoginPwd="'+ +'"")
    //}
    //Decodes.ToList();
    //grd.DataSource = Decodes.ToList();
    //grd.DataBind();
    //}
    string s="198302312409415470456558560";
    public string Decode(string s)
    {

        int Calc = 0; String FnlStr = "";
        int i = 1; int j = 1; int A = 0; byte[] bt; String Str = ""; ;
        while (i <= s.Length)
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
            A = Convert.ToInt32(s.ToString().Substring(i - 1, 3)) - Calc - j;
            bt = new byte[1];
            bt[0] = (byte)A;
            Str = Encoding.ASCII.GetString(bt);
            FnlStr = FnlStr + Str;
            i = i + 3;
            j++;
            Label9.Text = FnlStr;
          //  txt.Text = "";


           

        }
         return FnlStr;
    }
}