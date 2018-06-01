using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


public class BlTRRegiter
{
    public BlTRRegiter()
    {
    }
    DlTRRegister dl = new DlTRRegister();
    public DataTable GetAllRegister(plTrRegister pl, int Ind)
    {
        return dl.GetAllRegister(pl, Ind);
    }

    public DataTable GetExamName(int Ind, int facultytype)
    {
        return dl.GetExamName(Ind, facultytype);
    }


    public DataTable GetLoginDetails(plTrRegister pl)
    {
        return dl.GetLoginDetails(pl);
    }
    public int CheckLockInd(plTrRegister pl)
    {
        return dl.CheckLockInd(pl);
    }

    public int ChangeLoginIdStatus(plTrRegister pl)
    {
        return dl.ChangeLoginIdStatus(pl);
    }
    public DataTable GetLoginDetailsfromservice(plTrRegister pl)
    {
        return dl.GetLoginDetailsfromservice(pl);

    }
}