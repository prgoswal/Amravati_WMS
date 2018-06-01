using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class BlRegister
{
    DlRegister dl = new DlRegister();
    PlRegister pl = new PlRegister();
    public BlRegister()
    {
    }

    public Int64 InsertRegister(PlRegister pl)
    {
        Int64 i = dl.InsertRegister(pl);
        return i;
    }
    public int UpdateRegister(PlRegister pl)
    {
        int i = dl.UpdateRegister(pl);
        return i;
    }
    public DataTable GetRegister()
    {
        return dl.BindRegisterGrid();
    }
    public DataSet ShowBothImages(PlRegister pl)
    {
        return dl.ShowBothImages(pl);
    }
    public DataTable SearchRegister(PlRegister pl)
    {
        return dl.SearchRegister(pl);
    }
    public int CancelRegister(PlRegister pl)
    {
        return dl.CancelRegister(pl);
    }
    public int InsertAvailablePages(PlRegister pl)
    {
        int i = dl.InsertAvailablePageNo(pl);
        return i;
    }

    public int UpdateRegisterFilePath(PlRegister pl)
    {
        int i = dl.UpdateRegisterFilePath(pl);
        return i;
    }

    public DataTable MoveTrRegisterFiles(PlRegister pl)
    {
        return dl.MoveTrRegisterFiles(pl);
    }
    public int RegisterEntry(PlRegister pl)
    {
        int i = dl.RegisterEntry(pl);
        return i;
    }

   
}