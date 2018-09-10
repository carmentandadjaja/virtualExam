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
        
        // Add courses currently taught by the teacher, Carmen, 3/6/18 13:53
        //string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;
        //int userName = 0;
        //userName = Convert.ToInt32(Session["userName"]);

      //using (SqlConnection DBconnection = new SqlConnection(constr))
      //{
        //    using (SqlCommand cmd = new SqlCommand("View_Teacher_Courses"))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@username", userName);

        //        cmd.Connection = DBconnection;

        //        try
        //        {
        //            DBconnection.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (System.Data.SqlClient.SqlException ex)
        //        {
        //            string errormsg = "Unable to connect to the database!";
        //            errormsg += ex.Message;
        //            throw new Exception(errormsg);
        //        }
        //        finally
        //        {
        //            DBconnection.Close();
        //        }
        //    }

            
        //}

        int rowCount = GridView1.Rows.Count;  // Counts the number of rows in the gridview.  Erik, 3/6/18 20:10


        // Display the class number and title.
        if (rowCount == 0)
        {
            noClass.Visible = true;
            //class1.Visible = false;
            //class2.Visible = false;
            //class3.Visible = false;
            //class4.Visible = false;
        }
        else
        {
            noClass.Visible = false;
        }
        //else if (rowCount == 1)
        //{
        //    courseNumber1.Text = GridView1.Rows[0].Cells[0].Text + '-' + GridView1.Rows[0].Cells[2].Text;
        //    courseTitle1.Text = GridView1.Rows[0].Cells[1].Text;
        //    noClass.Visible = false;
        //    class2.Visible = false;
        //    class3.Visible = false;
        //    class4.Visible = false;
        //}
        //else if (rowCount == 2)
        //{
        //    courseNumber1.Text = GridView1.Rows[0].Cells[0].Text + '-' + GridView1.Rows[0].Cells[2].Text;
        //    courseTitle1.Text = GridView1.Rows[0].Cells[1].Text;
        //    courseNumber2.Text = GridView1.Rows[1].Cells[0].Text + '-' + GridView1.Rows[1].Cells[2].Text;
        //    courseTitle2.Text = GridView1.Rows[1].Cells[1].Text;
        //    noClass.Visible = false;
        //    class3.Visible = false;
        //    class4.Visible = false;
        //}
        //else if (rowCount == 3)
        //{
        //    courseNumber1.Text = GridView1.Rows[0].Cells[0].Text + '-' + GridView1.Rows[0].Cells[2].Text;
        //    courseTitle1.Text = GridView1.Rows[0].Cells[1].Text;
        //    courseNumber2.Text = GridView1.Rows[1].Cells[0].Text + '-' + GridView1.Rows[1].Cells[2].Text;
        //    courseTitle2.Text = GridView1.Rows[1].Cells[1].Text;
        //    courseNumber3.Text = GridView1.Rows[2].Cells[0].Text + '-' + GridView1.Rows[2].Cells[2].Text;
        //    courseTitle3.Text = GridView1.Rows[2].Cells[1].Text;
        //    noClass.Visible = false;
        //    class4.Visible = false;

        //}
        //else if (rowCount == 4)
        //{
        //    courseNumber1.Text = GridView1.Rows[0].Cells[0].Text + '-' + GridView1.Rows[0].Cells[2].Text;
        //    courseTitle1.Text = GridView1.Rows[0].Cells[1].Text;
        //    courseNumber2.Text = GridView1.Rows[1].Cells[0].Text + '-' + GridView1.Rows[1].Cells[2].Text;
        //    courseTitle2.Text = GridView1.Rows[1].Cells[1].Text;
        //    courseNumber3.Text = GridView1.Rows[2].Cells[0].Text + '-' + GridView1.Rows[2].Cells[2].Text;
        //    courseTitle3.Text = GridView1.Rows[2].Cells[1].Text;
        //    courseNumber4.Text = GridView1.Rows[3].Cells[0].Text + '-' + GridView1.Rows[3].Cells[2].Text;
        //    courseTitle4.Text = GridView1.Rows[3].Cells[1].Text;
        //    noClass.Visible = false;
        //}

        
    }

    // Passing the course number string to the next page.
    protected void courseNum_Click(object sender, EventArgs e)
    {
        //Session["sectionID"] = GridView1.Rows[0].Cells[3].Text;
        Button myButton = (Button)sender;
        Label test = (Label)myButton.Parent.FindControl("courseNumber");
        String course = test.Text;

        HiddenField section = (HiddenField)myButton.Parent.FindControl("sectionId");
        string sectionId = section.Value;
        Session["sectionID"] = sectionId;
        Response.Redirect("TestList?courseNum=" + course);
   }

    //protected void courseNum2_Click(object sender, EventArgs e)
    //{
    //    Session["sectionID"] = GridView1.Rows[1].Cells[3].Text;
    //    Response.Redirect("TestList?courseNum=" + courseNumber2.Text);
    //}
    //protected void courseNum3_Click(object sender, EventArgs e)
    //{
    //    Session["sectionID"] = GridView1.Rows[2].Cells[3].Text;
    //    Response.Redirect("TestList?courseNum=" + courseNumber3.Text);
    //}
    //protected void courseNum4_Click(object sender, EventArgs e)
    //{
    //    Session["sectionID"] = GridView1.Rows[3].Cells[3].Text;
    //    Response.Redirect("TestList?courseNum=" + courseNumber4.Text);
    //}

    // Passing the course number string to the next page.   START HERE AND CHANGE THIS!!!!!!!!!!!!!!!!
    protected void viewClass_Click(object sender, EventArgs e)
    {
      Button myButton = (Button)sender;
      Label test = (Label)myButton.Parent.FindControl("courseNumber");
      String course = test.Text;

      HiddenField section = (HiddenField)myButton.Parent.FindControl("sectionId");
      string sectionId = section.Value;
      Session["sectionID"] = sectionId;
      Response.Redirect("ClassRoster?courseNum=" + course);

      //Session["sectionID"] = GridView1.Rows[0].Cells[3].Text;
      //  Response.Redirect("ClassRoster?courseNum=" + courseNumber1.Text);
    }

    //protected void viewClass2_Click(object sender, EventArgs e)
    //{
    //    Session["sectionID"] = GridView1.Rows[1].Cells[3].Text;
    //    Response.Redirect("ClassRoster?courseNum=" + courseNumber2.Text);
    //}

    //protected void viewClass3_Click(object sender, EventArgs e)
    //{
    //    Session["sectionID"] = GridView1.Rows[2].Cells[3].Text;
    //    Response.Redirect("ClassRoster?courseNum=" + courseNumber3.Text);
    //}

    //protected void viewClass4_Click(object sender, EventArgs e)
    //{
    //    Session["sectionID"] = GridView1.Rows[3].Cells[3].Text;
    //    Response.Redirect("ClassRoster?courseNum=" + courseNumber4.Text);
    //}


    // Pass course number to next page to default which test to create
    protected void createTest_Click(object sender, EventArgs e)
    {
      Button myButton = (Button)sender;
      HiddenField section = (HiddenField)myButton.Parent.FindControl("sectionId");
      string sectionId = section.Value;
      Session["sectionID"] = sectionId;
      Response.Redirect("TestCreate");

      //Session["sectionID"] = GridView1.Rows[0].Cells[3].Text;
      //  Response.Redirect("TestCreate?sectionID=" + Session["sectionID"]);
   }

    //protected void createTest2_Click(object sender, EventArgs e)
    //{
    //    Session["sectionID"] = GridView1.Rows[1].Cells[3].Text;
    //    Response.Redirect("TestCreate?sectionID=" + Session["sectionID"]);
    //}

    //protected void createTest3_Click(object sender, EventArgs e)
    //{
    //    Session["sectionID"] = GridView1.Rows[2].Cells[3].Text;
    //    Response.Redirect("TestCreate?sectionID=" + Session["sectionID"]);
    //}

    //protected void createTest4_Click(object sender, EventArgs e)
    //{
    //    Session["sectionID"] = GridView1.Rows[3].Cells[3].Text;
    //    Response.Redirect("TestCreate?sectionID=" + Session["sectionID"]);
    //}
}