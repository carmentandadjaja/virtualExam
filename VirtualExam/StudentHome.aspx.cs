using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // This ensures that only users with the correct credentials have access to their respective pages. Erik, 3/1/18 12:54
        if (Convert.ToChar(Session["userType"]) == 't')
        {
            Response.Redirect("TeacherHome.aspx");
        }
        else if (Convert.ToChar(Session["userType"]) == 'a')
        {
            Response.Redirect("AdminHome.aspx");
        }
        else if (Session["userType"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        GridView1.Visible = false;
        int rowCount = GridView1.Rows.Count;

        // Display the class number and title. Jo, 3/6/18 14:30
        if (rowCount == 0)
        {
            noClass.Visible = true;
        }
        else
        {
            noClass.Visible = false;
        }
    }   

    protected void View_Test_Click(object sender, EventArgs e)
    {
        Button myButton = (Button)sender;
        Label test = (Label)myButton.Parent.FindControl("courseNumber");
        String course = test.Text;

        HiddenField section = (HiddenField)myButton.Parent.FindControl("sectionId");
        string sectionId = section.Value;
        Session["sectionID"] = sectionId;
        Response.Redirect("StudentCourses?courseNum=" + course);
    }
}