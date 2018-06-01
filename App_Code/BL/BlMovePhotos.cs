using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class BlMovePhotos
{
    DlMovePhotos dl = new DlMovePhotos();

    public BlMovePhotos()
    {
    }
    public DataTable GetDestPath(PlMovePhotos pl)
    {
        return dl.GetDestPath(pl);
    }

    public int InsertDestPath(PlMovePhotos pl)
    {
        return dl.InsertDestPath(pl);
    }

    public DataTable GetMachineName(PlMovePhotos pl)
    {
        return dl.GetMachineName(pl);
    }

    // ---------------------------------------------------------------------------------------
    public int GetPartNoFromTo(PlMovePhotos pl)
    {
        return dl.GetPartNoFromTo(pl);
    }

    public int UpdateRemainingPart(PlMovePhotos pl)
    {
        return dl.UpdateRemainingPart(pl);
    }
    // ---------------------------------------------------------------------------------------
    public DataTable FindLastNo(PlMovePhotos pl)
    {
        return dl.FindLastNo(pl);
    }
}