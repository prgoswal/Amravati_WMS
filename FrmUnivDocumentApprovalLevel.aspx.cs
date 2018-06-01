using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmUnivDocumentApprovalLevel : System.Web.UI.Page
{
    DataTable dt;
    DataRow dr;
    string[] col;
    string[] row;
    public static int[] colorrow = new int[300];
    public static int[] colorcol = new int[300];
    static int[] Tcolorrow = new int[300];
    static int[] Tcolorcol = new int[300];
    static int[] ArrDenyRow;
    static int[] ArrDenyCol;
    static int ind = 0;
    static int[] temp;
    public static int beforeind = 0;
    static int arind = 0; int rind; Constraint deny;
    static int StartuserLevel=0;
    static int StartDocumentLevel=0;
    Hashtable ht; SqlCommand cmd; SqlDataAdapter da;
    

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            arind = 0;
            Array.Clear(colorrow, 0, colorrow.Length);
            Array.Clear(colorcol, 0, colorrow.Length);
            Array.Clear(Tcolorrow, 0, Tcolorrow.Length);
            Array.Clear(Tcolorcol, 0, Tcolorrow.Length);

            beforeind = 1;
            cmd = new SqlCommand("SpMenus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", 12);
            dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count == 0 || dt == null)
            {
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                dt1 = (DataTable)Session["documentlabel"];
                dt2 = (DataTable)Session["menuLabel"];

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    cmd = new SqlCommand("SpMenus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ind", 11);
                    cmd.Parameters.AddWithValue("@ParentMenuId", 1);
                    cmd.Parameters.AddWithValue("@Title", "ApprovalMenus");
                    cmd.Parameters.AddWithValue("@Url", "Home.aspx");
                    cmd.Parameters.AddWithValue("@LevelProfile", Convert.ToInt32(dt1.Rows[i]["ItemId"]) + StartDocumentLevel);
                    cmd.Parameters.AddWithValue("@AllowMenu", Convert.ToInt32(dt2.Rows[1]["ItemId"]));
                    con.Open();
                    int j = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }         
        }
        
        BindDdlUserLevel();
        BindDdlSubMenu();
        
        AddColumns();
        dr = dt.NewRow();
        GetDtRow();
        dt.Rows.Add(dr);
        
        BindGrid();

        
        if (beforeind == 1)
        {
            beforeselect();
            beforeind = 0;
        }

    }
    protected void beforeselect()
    {

        beforepermission();
        if (dt.Rows.Count > 0)
        {
            //if (beforeind == 1)
            //{
            ArrDenyCol = new int[dt.Rows.Count];
            ArrDenyRow = new int[dt.Rows.Count];
            for (int k = 0; k < dt.Rows.Count; k++)
            {

                int col = Convert.ToInt32(dt.Rows[k]["AllowMenu"]) - (1+StartuserLevel);
                int row = Convert.ToInt32(dt.Rows[k]["LevelProfile"])-StartDocumentLevel;
                // row = row - 1;
                // Response.Write("<br/> Index =" + rind + " " + Tcolorcol[k]);
                GridView1.Rows[row - 1].Cells[col].BackColor = Color.LightGreen;
                colorcol[arind] = col;
                colorrow[arind] = row;
                ArrDenyRow[k] = row;
                ArrDenyCol[k] = col;
                arind++;
            }
            beforeind = 0;
            // }
        }
        else
        {
            ArrDenyCol = new int[0];
            ArrDenyRow = new int[0];
        }
    }
    protected void beforepermission()
    {
        SqlCommand cmd1 = new SqlCommand("SpMenus", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@ind", 6);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd1);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

        }

    }

    public void BindDdlUserLevel()
    {
        cmd = new SqlCommand("SpGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ind", 15);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            //dt.Rows.RemoveAt(0);
            Session["documentlabel"] = dt;
            StartDocumentLevel = Convert.ToInt32(dt.Rows[0]["ItemId"])-1;
            row = new string[dt.Rows.Count];
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                row[i] = dt.Rows[i]["DocumentType"].ToString();
            }
        }
    }
    public void BindDdlSubMenu()
    {
        cmd = new SqlCommand("SpGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ind", 16);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Session["menuLabel"] = dt;
            StartuserLevel=Convert.ToInt32(dt.Rows[0]["ItemId"])-2;
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0][0] = 0;
            dt.Rows[0][1] = "0";
            col = new string[dt.Rows.Count];

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                col[i] = dt.Rows[i]["LevelDescription"].ToString();
            }
        }
    }
    private void AddRow()
    {

        if (ViewState["dt"] == null)
        {

            ViewState["dt"] = dt;

        }
    }


    private void GetDtRow()
    {
        for (int r = 0; r < row.Length; r++)
        {
            for (int i = 0; i < col.Length; i++)
            {
                string ind = (col[i]).ToString();
                if (i == 0)
                    dr[" "] = row[r];
                else
                {

                    dr[ind] = "+";
                }
            }
            if (r < row.Length - 1)
            {
                dt.Rows.Add(dr);
                dr = dt.NewRow();
            }

        }
    }

    private void AddColumns()
    {
        dt = new DataTable();
        DataColumn coll = null;
        for (int i = 0; i < col.Length; i++)
        {
            if (i == 0)
            {
                coll = new DataColumn(" ");
            }
            else
            {
                coll = new DataColumn(col[i]);
            }
            dt.Columns.Add(coll);

        }

    }
    public void BindGrid()
    {
        GridView1.DataSource = dt;
        GridView1.DataBind();
        if (ViewState["dt"] == null)
        {
            ViewState["dt"] = dt;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Click")
        {
            int f = 0;
            int r = 0;
            int remrow = -1;
            int remcol = -1;
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = (gvr.RowIndex) + 1;

            int columnindex = Convert.ToInt32(e.CommandArgument);


            int l = RowIndex;

            int m = columnindex;

            if (arind > 0)
            {
                for (int j = 0; j < arind; j++)
                {
                    int ro = colorcol[j];
                    int cl = colorrow[j];
                    if ((colorrow[j] == RowIndex) && (colorcol[j] == columnindex))
                    {
                        remrow = RowIndex;
                        remcol = columnindex;
                        f = 1;
                        break;

                    }

                }
            }
            if (arind < 1)
            {

                colorrow[arind] = RowIndex;
                colorcol[arind] = columnindex;

                arind++;
                f = 1;
            }

            if (f != 1 && arind > 0)
            {
                colorrow[arind] = RowIndex;
                colorcol[arind] = columnindex;
                f = 0;
                arind++;
            }
            for (int j = 0; j < arind; j++)
            {
                {
                    if (colorrow[j] == remrow && colorcol[j] == remcol)
                    {

                    }
                    else
                    {
                        Tcolorrow[r] = colorrow[j];
                        Tcolorcol[r] = colorcol[j];
                        r++;
                    }
                }
            }
            for (int k = 0; k <= r - 1; k++)
            {
                int Tcol = Tcolorcol[k];
                int Trow = Tcolorrow[k];
                colorrow[k] = Tcolorrow[k];
                colorcol[k] = Tcolorcol[k];
                int rind = Tcolorrow[k] + 1;
                GridView1.Rows[Trow - 1].Cells[Tcol].BackColor = Color.Yellow;
            }
            int DltIndR = -1;
            if (ArrDenyCol.Length > 0)
            {
                for (int s = 0; s < ArrDenyCol.Length; s++)
                {
                    if (RowIndex == ArrDenyRow[s] && columnindex == ArrDenyCol[s])
                    {
                        GridView1.Rows[ArrDenyRow[s] - 1].Cells[ArrDenyCol[s]].BackColor = Color.White;
                        DltIndR = s;

                    }
                    else
                    {
                        GridView1.Rows[ArrDenyRow[s] - 1].Cells[ArrDenyCol[s]].BackColor = Color.LightGreen;
                    }


                }
            }
            if (DltIndR >= 0)
            {
                ArrDenyCol = ArrDenyCol.Where((source, index) => index != DltIndR).ToArray();
                ArrDenyRow = ArrDenyRow.Where((source, index) => index != DltIndR).ToArray();
            }
            arind = r;

            ind += 1;

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // determine cell you want to set as a hyperlink

            LinkButton hl = null;
            for (int i = 1; i < col.Length; i++)
            {
                // e.Row.Cells[i].Attributes.Add("Style", "background: url(~/Images/correct.png) no-repeat 5px center;");
                hl = new LinkButton();


                //hl.NavigateUrl = e.Row.Cells[i].Text; // take the data from the datatable and make it the URL
                hl.Text = "Allow";

                //e.Row.Cells[4].BackColor = System.Drawing.Color.Red; 
                hl.CommandName = "Click";
                hl.CommandArgument = i.ToString();

                e.Row.Cells[i].Text = ""; // clear it out
                e.Row.Cells[i].Controls.Add(hl);

                // For First Column Disabled in Grid View
                if (i == 1)
                {
                    e.Row.Cells[i].Enabled = false;

                }
            }
        }

    }
    protected void Btnselect_Click(object sender, EventArgs e)
    {
        lblvalue.Text = "";
        try
        {

            cmd = new SqlCommand("SpMenus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", 12);
           // cmd.Parameters.AddWithValue("@ParentMenuId", 1);
            dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            int flag = 1;
            if (dt.Rows.Count > 0 && dt != null)
            {
                flag = 0;
            }
            cmd = new SqlCommand("SpMenus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", 4);
            //  cmd.Parameters.AddWithValue("@LevelProfile", DltRind);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            //for (int k = 0; k <= arind - 1; k++)
            //{
            for (int k = 0; k <= arind - 1; k++)
            {
                rind = Tcolorrow[k];
                GridView1.Rows[colorrow[k] - 1].Cells[colorcol[k]].BackColor = Color.Yellow;
                //GridView1.Rows[colorrow[k]].Cells[colorcol[k]].ForeColor = Color.Red;

                cmd = new SqlCommand("SpMenus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ind", 5);
                cmd.Parameters.AddWithValue("@ParentMenuId", 1);
                cmd.Parameters.AddWithValue("@Title", "ApprovalMenus");
                cmd.Parameters.AddWithValue("@Url", "Home.aspx");
                cmd.Parameters.AddWithValue("@LevelProfile", rind+StartDocumentLevel);
                cmd.Parameters.AddWithValue("@AllowMenu", Tcolorcol[k] + 1+StartuserLevel);
                // lblvalue.Text = lblvalue.Text + ("<br/> Index =" + rind + " " + Tcolorcol[k]);
                con.Open();
                int j = cmd.ExecuteNonQuery();
                con.Close();
                if (j > 0)
                {
                    lblvalue.Text = "Permission Granted Successfully";
                }
                else
                    lblvalue.Text = "Permission Not Granted Successfully";

            }

            arind = 0;
            beforeind = 1;

            if (flag == 1)
            {
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                dt1 = (DataTable)Session["documentlabel"];
                dt2 = (DataTable)Session["menuLabel"];

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    cmd = new SqlCommand("SpMenus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ind", 11);
                    cmd.Parameters.AddWithValue("@ParentMenuId", 1);
                    cmd.Parameters.AddWithValue("@Title", "ApprovalMenus");
                    cmd.Parameters.AddWithValue("@Url", "Home.aspx");
                    cmd.Parameters.AddWithValue("@LevelProfile", Convert.ToInt32(dt1.Rows[i]["ItemId"]) + StartDocumentLevel);
                    cmd.Parameters.AddWithValue("@AllowMenu", Convert.ToInt32(dt2.Rows[1]["ItemId"]));
                    // lblvalue.Text = lblvalue.Text + ("<br/> Index =" + rind + " " + Tcolorcol[k]);
                    con.Open();
                    int j = cmd.ExecuteNonQuery();
                    con.Close();
                    if (j > 0)
                    {
                        lblvalue.Text = "Permission Granted Successfully";
                    }
                    else
                        lblvalue.Text = "Permission Not Granted Successfully";
                }
            }           
            //Response.Redirect("Home.aspx");

        }
        catch (Exception ex)
        {
            lblvalue.Text = ex.Message;
        }

    }

}