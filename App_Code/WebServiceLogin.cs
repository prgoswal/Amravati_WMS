using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;

/// <summary>
/// Summary description for WebServiceLogin
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebServiceLogin : System.Web.Services.WebService {

    public WebServiceLogin () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    string s;
    DataTable dt1 = new DataTable();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    
    plTrRegister objpltrregister = new plTrRegister();
    BlTRRegiter objbltrregister = new BlTRRegiter();
  
    [WebMethod]
    public string Login(string loginid,string Password)     
    {
 
        if (loginid.ToString() != string.Empty && Password.ToString()!=string.Empty)
        {
            objpltrregister.UserLoginId = loginid;
            objpltrregister.LoginPwd = Password;

 
            objbltrregister.GetLoginDetailsfromservice(objpltrregister);
        
        }
        if (objpltrregister.dt != null && objpltrregister.dt.Rows.Count > 0)
        {
            s = objpltrregister.dt.Rows[0]["UserTypeId"].ToString() + "," + objpltrregister.dt.Rows[0]["UserId"] + "," + objpltrregister.dt.Rows[0]["UsertypeId"] + "," + objpltrregister.dt.Rows[0]["UserLoginId"];

        }
        else
        {
            s = "0";
        }

        return s;
           
    }
   
}
