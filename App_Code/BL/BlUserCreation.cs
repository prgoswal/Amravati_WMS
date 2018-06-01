using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class BlUserCreation
{
    public BlUserCreation()
    {
    }
    DlUserCreation dl = new DlUserCreation();

    public int InserUser(PlUserCreation pl)
    {
        return dl.InsertUserEntry(pl);
    }
    public int UpdateUnlockedUser(PlUserCreation pl)
    {
        return dl.UpdateUnlockedUser(pl);
    }
}