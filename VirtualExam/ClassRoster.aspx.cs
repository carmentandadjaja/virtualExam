using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // This ensures that only users with the correct credentials have access to their respective pages. Erik, 3/1/18 12:54
        if (Convert.ToChar(Session["userType"]) == 's')
        {
            Response.Redirect("StudentHome.aspx");
        }
        else if (Convert.ToChar(Session["userType"]) == 'a')
        {
            Response.Redirect("AdminHome.aspx");
        }
        else if (Session["userType"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        int section = Convert.ToInt32(Session["sectionID"]);

        string courseNumber = Request.QueryString["courseNum"];

        courseName.InnerText = courseNumber;
        
        SqlTest.SelectCommand = "exec View_Students_In_Section " + section;
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Alternate)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.BackColor = Color.FromName("#d6d6d6");
            }
        }
        else
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.BackColor = Color.FromName("white");
            }
        }
    }
}