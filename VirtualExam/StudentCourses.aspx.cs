using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
    {
        string sectionId = "";

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

        int studentId = Convert.ToInt32(Session["userName"]);

        // Receive the value from previous page. Jo, 3/6/18 14:30
        
            sectionId = Session["sectionID"].ToString();
            courseNumber.Text = Request.QueryString["courseNum"];

        //SqlTest.SelectCommand = "exec get_test_list " + username + ", " + "\'CS 414\'";  Keep this line of code.  We are going to use it later. Bryce, 3/3/18 14:00
        SqlDataSource1.SelectCommand = "exec View_Student_Tests " + studentId + ", " + sectionId;  // Display student's available test. Jo, 3/6/18 21:30
        GridView1.DataBind();

        // This is what we need for checking data available test. Erik,21/18
        DateTime now = DateTime.Now;
        int index = 0;

        foreach (GridViewRow row in GridView1.Rows)
        {
            DateTime testBeginDate = Convert.ToDateTime(GridView1.Rows[index].Cells[6].Text);
            DateTime testEndDate = Convert.ToDateTime(GridView1.Rows[index].Cells[7].Text);

            // Check if the test has been taken or not

            if (GridView1.Rows[index].Cells[8].Text == "1")
            {
                GridView1.Rows[index].Cells[8].Text = "Complete";
                GridView1.Rows[index].Cells[9].Enabled = false; // set to false              
            }
            else if (testBeginDate < now && now > testEndDate && GridView1.Rows[index].Cells[8].Text == "0") // I have no idea how but this is working!  Erik, 3/21/18 21:55 
            {
                GridView1.Rows[index].Cells[8].Text = "Expired";
                GridView1.Rows[index].Cells[9].Enabled = false;
            }
            else if (testBeginDate > now) // This should fix it where students cannot take tests until it is the first day that the test is available. Erik, 4/15/18 21:10
            {
                GridView1.Rows[index].Cells[8].Text = "Unavailable";
                GridView1.Rows[index].Cells[9].Enabled = false;
            }
            else
            {
                GridView1.Rows[index].Cells[8].Text = "Incomplete";
            }

            index++;
        }


        // Do not delete any of this code!!!!! Erik, 3/21/18 21:55 
        //if (now > Convert.ToDateTime(GridView1.Rows[0].Cells[6].Text))
        //{
        //    GridView1.Rows[2].Cells[5].Text = "Today is greater than 2/22/18";
        //}

        //Do not delete this!We will use this in the future to lock the tests down until the date has arrived.Erik, 3 / 19 / 18 19:00
        //SqlDataSource2.SelectCommand = "select * from test_details where test_id = 1000000001";
        //GridView2.DataBind();
        //GridView2.Visible = true;

        //DateTime testBeginDate = Convert.ToDateTime(GridView2.Rows[0].Cells[2].Text);
        //DateTime testEndDate = Convert.ToDateTime(GridView2.Rows[0].Cells[3].Text);

        //if (now > testEndDate && now < testBeginDate)
        //{
        //    GridView1.Rows[0].Cells[7].Enabled = false;
        //}
        //else
        //{
        //    GridView1.Rows[1].Cells[6].Text = "You cannot take the Test";
        //    GridView1.Rows[1].Cells[7].Text = "You cannot take the Test";
        //    GridView1.Rows[1].Cells[8].Enabled = false;
        //}
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // This code will direct users to test instructions page and passing the test name, course #, course section. Jo, 3/14/2018 15:14
            LinkButton takeTestButton = (LinkButton)e.Row.FindControl("takeTest");
            takeTestButton.PostBackUrl = "~/TestInstructions.aspx?field1=" + ((Label) e.Row.FindControl("test")).Text;
        }
    }
}