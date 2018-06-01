using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text;
using System.Net;
using System.IO;
using System.Data;

public partial class LoginthroughService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        StringBuilder sbpostdata = new StringBuilder();

        sbpostdata.AppendFormat("&loginid={0}", txtusername.Text);
        sbpostdata.AppendFormat("&Password={0}", txtpassword.Text);
        //string url = "http://oswal.selfip.com/auawebservice/Service.asmx/OTPRequest";
        //string url = "http://localhost:49205/Service.asmx/OTPRequest";
        string url = "http://localhost:3659/WebServiceLogin.asmx/Login";
        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] data = encoding.GetBytes(sbpostdata.ToString());
        httpWReq.Method = "POST";
        httpWReq.ContentType = "application/x-www-form-urlencoded";
        httpWReq.ContentLength = data.Length;

        using (Stream stream = httpWReq.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        HttpWebResponse Response = (HttpWebResponse)httpWReq.GetResponse();
        StreamReader Reader = new StreamReader(Response.GetResponseStream());
        string ResponseString = Reader.ReadToEnd();
        string kg = Convert.ToString(ResponseString);

        string kgs = (kg.Substring((kg.IndexOf("org/\">") + 6))).Substring(0, (kg.Substring((kg.IndexOf("org/\">") + 6))).Length - 9);

        string kgs1 = (Convert.ToString(ResponseString).Substring((Convert.ToString(ResponseString).IndexOf("org/\">") + 6))).Substring(0, (Convert.ToString(ResponseString).Substring((Convert.ToString(ResponseString).IndexOf("org/\">") + 6))).Length - 9);


        string[] words = kgs1.Split(',');

        Session["UserTypeId"] = words[0].ToString();

        Session["UserId"] = words[1].ToString();

        Session["UserType"] = words[2].ToString();

        Session["USERNAME"] = words[3].ToString();

        Session["LoginDateTime"] = DateTime.Now;


        lblMsg.Text = ResponseString;
        Reader.Close();
        Response.Close();

        Server.Transfer("Home.aspx");

    }
}