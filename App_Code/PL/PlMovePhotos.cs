using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class PlMovePhotos
{
    public PlMovePhotos()
    {
    }
    public int Ind { get; set; }
    public int PartType { get; set; }
    public string SourcePath { get; set; }
    public string DestPath { get; set; }
    public string RemainingPart { get; set; }
    public int PartNoFrom { get; set; }
    public int PartNoTo { get; set; }

    public DateTime MoveDate { get; set; }
    public int TotalImagesCopied { get; set; }
}