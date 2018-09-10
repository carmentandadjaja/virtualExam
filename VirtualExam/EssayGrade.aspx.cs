using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EssayGrade : System.Web.UI.Page
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

        if (GridView1.Rows.Count == 0)
        {
            message.Text = "No Essays need to be graded.";
        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton takeTestButton = (LinkButton)e.Row.FindControl("gradeTest");
            takeTestButton.PostBackUrl = "~/EssayGradeDetails.aspx?testId=" + e.Row.Cells[0].Text + "," + e.Row.Cells[3].Text;
        }
    }
}