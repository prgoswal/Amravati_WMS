using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


public class BlImgEntry
{
    DlImgEntry dl = new DlImgEntry();
    public BlImgEntry()
    {
    }

    public int InserImgEntry(PlImgEntry pl)
    {
        int i = dl.InserImgEntry(pl);
        return i;
    }
    public int InsertCFImgEntry(PlImgEntry pl)
    {
        int i = dl.InsertCFImgEntry(pl);
        return i;
    }
    public int GetCompletedPageNo(PlImgEntry pl)
    {
        int i = dl.GetICompetedPages(pl);
        return i;
    }
    public DataSet LastPageNoWithLotNo(PlImgEntry pl)
    {
        return dl.LastPageNoWithLotNo(pl);
    }
    public int DeletePageNoData(PlImgEntry pl)
    {
        int i = dl.DeletePageNoData(pl);
        return i;
    }
}