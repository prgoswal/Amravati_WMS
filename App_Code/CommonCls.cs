using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


public class CommonCls
{
    public CommonCls()
    {
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
}