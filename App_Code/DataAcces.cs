using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for DataAcces
/// </summary>
public class DataAcces
{
    public DataAcces()
    {

    }
    public static SqlConnection Occconnection()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        if (con.State == ConnectionState.Closed)
            con.Open();
        return con;
    }

    //public static string Url = "http://prg90/WMSAPI/";//FOR SATISH SIR MACHINE  
    //public static string Url = "http://boi7/AWAPI/";FOR ASHISH SIR MACHINE
    //public static string Url = "http://localhost/amravatiwmsapi/"; FOR LAPTOP
    //public static string Url = "http://MLCV004/WMSAPI/";FOR MLCV
    //public static string Url = "http://SGBAU/WMSAPI/";//FOR SGBAU  
    //Section for header and footer in html Reports

    public static string hiddenURL = "http://SGBAU/";
    public static string Url = "http://localhost:1162/";//FOR MY LOCAL MACHINE OSWAL1332
    //public static string Url = "http://SGBAU/WMS_Api/";
    public static string universityName = "Sant Gadge Baba Amravati University ";
    public static string city = "Amravati";
    public static string LogoUrl = "bootstrap-3.3.6-dist/images/logo.png";
    public static DateTime Date = DateTime.Now.Date;

}