using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;


/// <summary>
/// Summary description for Logout
/// </summary>
public class Logout
{
    plTrRegister objpltrRegister = new plTrRegister();
	public  Logout()
	{       
		//
		// TODO: Add constructor logic here
		//
        //HttpClient hc = new HttpClient();
        //hc.BaseAddress = new Uri(DataAcces.Url);
        //hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
        //objpltrRegister.Ind = 3;
        //objpltrRegister.UserLoginId = Convert.ToString(Session["USERNAME"]);
        //var v = string.Format("api/Login/UpdateActiveStatus/?Ind={0}&UserLoginId={1}", objpltrRegister.Ind, objpltrRegister.UserLoginId);

        //var resp = hc.PutAsJsonAsync(v, objpltrRegister).Result;
        //if (resp.IsSuccessStatusCode)
        //{
        //    var get = resp.Content.ReadAsAsync<IEnumerable<plTrRegister>>().Result;
        //    if (get != null)
        //    {
               
                
        //        pl.UserLoginId = null;
        //        pl.UserId = 0;
        //        pl.usertypeID = 0;
        //        Session["USERNAME"] = null;
        //        Session["UserId"] = null;
        //        Session["UserType"] = null;
        //        Session.Clear();
        //        Session.Abandon();
        //        Response.Redirect("UserLogin.aspx");
        //        Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "closeWin()", true);

        //    }
        //}
        
	}

    
    
}