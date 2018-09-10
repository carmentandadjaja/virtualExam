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

        int userName = Convert.ToInt32(Session["userName"]);

        // Get the Course Information and the course average, Carmen 3/29/18 15:36
        //float averageCourse1;
        //float averageCourse4;
        //float averageCourse3;
        //float averageCourse2;

        // Gets all courses with available average
        int rowCount = GridView1.Rows.Count;
        GridView1.Visible = false;

        // Get all courses taught by the teacher
        //int courseRowCount = GridView2.Rows.Count;

        //if (courseRowCount == 0)
        //{
        //    noClass.Visible = true;
        //    class1.Visible = false;
        //    class2.Visible = false;
        //    class3.Visible = false;
        //    class4.Visible = false;
        //}

        //if (courseRowCount == 1)
        //{
        //    noClass.Visible = false;
        //    class1.Visible = true;
        //    class2.Visible = false;
        //    class3.Visible = false;
        //    class4.Visible = false;

        //    courseNumber1.InnerText = GridView2.Rows[0].Cells[0].Text + "-" + GridView2.Rows[0].Cells[2].Text;
        //    courseTitle1.InnerText = GridView2.Rows[0].Cells[1].Text;
        //}
        //else if (courseRowCount == 2)
        //{
        //    noClass.Visible = false;
        //    class1.Visible = true;
        //    class2.Visible = true;
        //    class3.Visible = false;
        //    class4.Visible = false;

        //    courseNumber1.InnerText = GridView2.Rows[0].Cells[0].Text + "-" + GridView2.Rows[0].Cells[2].Text;
        //    courseTitle1.InnerText = GridView2.Rows[0].Cells[1].Text;

        //    courseNumber2.InnerText = GridView2.Rows[1].Cells[0].Text + "-" + GridView2.Rows[1].Cells[2].Text;
        //    courseTitle2.InnerText = GridView2.Rows[1].Cells[1].Text;
        //}
        //else if (courseRowCount == 3)
        //{
        //    noClass.Visible = false;
        //    class1.Visible = true;
        //    class2.Visible = true;
        //    class3.Visible = true;
        //    class4.Visible = false;

        //    courseNumber1.InnerText = GridView2.Rows[0].Cells[0].Text + "-" + GridView2.Rows[0].Cells[2].Text;
        //    courseTitle1.InnerText = GridView2.Rows[0].Cells[1].Text;

        //    courseNumber2.InnerText = GridView2.Rows[1].Cells[0].Text + "-" + GridView2.Rows[1].Cells[2].Text;
        //    courseTitle2.InnerText = GridView2.Rows[1].Cells[1].Text;

        //    courseNumber3.InnerText = GridView2.Rows[2].Cells[0].Text + "-" + GridView2.Rows[2].Cells[2].Text;
        //    courseTitle3.InnerText = GridView2.Rows[2].Cells[1].Text;

        //}
        //else if (courseRowCount == 4)
        //{
        //    noClass.Visible = false;
        //    class1.Visible = true;
        //    class2.Visible = true;
        //    class3.Visible = true;
        //    class4.Visible = true;

        //    courseNumber1.InnerText = GridView2.Rows[0].Cells[0].Text + "-" + GridView2.Rows[0].Cells[2].Text;
        //    courseTitle1.InnerText = GridView2.Rows[0].Cells[1].Text;

        //    courseNumber2.InnerText = GridView2.Rows[1].Cells[0].Text + "-" + GridView2.Rows[1].Cells[2].Text;
        //    courseTitle2.InnerText = GridView2.Rows[1].Cells[1].Text;

        //    courseNumber3.InnerText = GridView2.Rows[2].Cells[0].Text + "-" + GridView2.Rows[2].Cells[2].Text;
        //    courseTitle3.InnerText = GridView2.Rows[2].Cells[1].Text;

        //    courseNumber4.InnerText = GridView2.Rows[3].Cells[0].Text + "-" + GridView2.Rows[3].Cells[2].Text;
        //    courseTitle4.InnerText = GridView2.Rows[3].Cells[1].Text;
        //}


        if (rowCount == 0)
        {
            noClass.Visible = true;
            //class1.Visible = false;
            //class2.Visible = false;
            //class3.Visible = false;
            //class4.Visible = false;

            //courseAverage1.InnerText = "No Grades available";
            //courseAverage2.InnerText = "No Grades available";
            //courseAverage3.InnerText = "No Grades available";
            //courseAverage4.InnerText = "No Grades available";
        }
        else
        {
            noClass.Visible = false;
        }
        //else if (rowCount == 1)
        //{
        //    noClass.Visible = false;
        //    class1.Visible = true;
        //    class2.Visible = false;
        //    class3.Visible = false;
        //    class4.Visible = false;
        //    courseNumber1.InnerText = GridView1.Rows[0].Cells[1].Text + "-" + GridView1.Rows[0].Cells[2].Text;
        //    courseTitle1.InnerText = GridView1.Rows[0].Cells[0].Text;

        //    float.TryParse(GridView1.Rows[0].Cells[4].Text, out averageCourse1);
        //    viewAverage(averageCourse1, 1);

        //}
        //else if (rowCount == 2)
        //{
        //    noClass.Visible = false;
        //    class1.Visible = true;
        //    class2.Visible = true;
        //    class3.Visible = false;
        //    class4.Visible = false;

        //    courseNumber1.InnerText = GridView1.Rows[0].Cells[1].Text + "-" + GridView1.Rows[0].Cells[2].Text;
        //    courseTitle1.InnerText = GridView1.Rows[0].Cells[0].Text;

        //    courseNumber2.InnerText = GridView1.Rows[1].Cells[1].Text + "-" + GridView1.Rows[1].Cells[2].Text;
        //    courseTitle2.InnerText = GridView1.Rows[1].Cells[0].Text;

        //    float.TryParse(GridView1.Rows[0].Cells[4].Text, out averageCourse1);
        //    float.TryParse(GridView1.Rows[1].Cells[4].Text, out averageCourse2);
        //    viewAverage(averageCourse1, 1);
        //    viewAverage(averageCourse2, 2);

        //}
        //else if (rowCount == 3)
        //{
        //noClass.Visible = false;
        //class1.Visible = true;
        //class2.Visible = true;
        //class3.Visible = true;
        //class4.Visible = false;

        //courseNumber1.InnerText = GridView1.Rows[0].Cells[1].Text + "-" + GridView1.Rows[0].Cells[2].Text;
        //courseTitle1.InnerText = GridView1.Rows[0].Cells[0].Text;

        //courseNumber2.InnerText = GridView1.Rows[1].Cells[1].Text + "-" + GridView1.Rows[1].Cells[2].Text;
        //courseTitle2.InnerText = GridView1.Rows[1].Cells[0].Text;

        //courseNumber3.InnerText = GridView1.Rows[2].Cells[1].Text + "-" + GridView1.Rows[2].Cells[2].Text;
        //courseTitle3.InnerText = GridView1.Rows[2].Cells[0].Text;

        //float.TryParse(GridView1.Rows[0].Cells[4].Text, out averageCourse1);
        //float.TryParse(GridView1.Rows[1].Cells[4].Text, out averageCourse2);
        //float.TryParse(GridView1.Rows[2].Cells[4].Text, out averageCourse3);
        //viewAverage(averageCourse1, 1);
        //viewAverage(averageCourse2, 2);
        //viewAverage(averageCourse3, 3);
        //}
        //else if (rowCount == 4)
        //{
        //noClass.Visible = false;
        //class1.Visible = true;
        //class2.Visible = true;
        //class3.Visible = true;
        //class4.Visible = true;

        //courseNumber1.InnerText = GridView1.Rows[0].Cells[1].Text + "-" + GridView1.Rows[0].Cells[2].Text;
        //courseTitle1.InnerText = GridView1.Rows[0].Cells[0].Text;

        //courseNumber2.InnerText = GridView1.Rows[1].Cells[1].Text + "-" + GridView1.Rows[1].Cells[2].Text;
        //courseTitle2.InnerText = GridView1.Rows[1].Cells[0].Text;

        //courseNumber3.InnerText = GridView1.Rows[2].Cells[1].Text + "-" + GridView1.Rows[2].Cells[2].Text;
        //courseTitle3.InnerText = GridView1.Rows[2].Cells[0].Text;

        //courseNumber4.InnerText = GridView1.Rows[3].Cells[1].Text + "-" + GridView1.Rows[3].Cells[2].Text;
        //courseTitle4.InnerText = GridView1.Rows[3].Cells[0].Text;


        //if (float.TryParse(GridView1.Rows[0].Cells[4].Text, out averageCourse1))
        //{
        //    viewAverage(averageCourse1, 1);
        //}
        //else
        //{
        //    courseAverage1.InnerText = "Average Unavailable";
        //}

        //if (float.TryParse(GridView1.Rows[1].Cells[4].Text, out averageCourse2))
        //{
        //    viewAverage(averageCourse2, 2);
        //}
        //else
        //{
        //    courseAverage2.InnerText = "Average Unavailable";
        //}


        //if (float.TryParse(GridView1.Rows[2].Cells[4].Text, out averageCourse3))
        //{
        //    viewAverage(averageCourse3, 3);
        //}
        //else
        //{
        //    courseAverage3.InnerText = "Average Unavailable";
        //}


        //if (float.TryParse(GridView1.Rows[3].Cells[4].Text, out averageCourse4))
        //{
        //    viewAverage(averageCourse4, 4);
        //}
        //else
        //{
        //    courseAverage4.InnerText = "Average Unavailable";
        //}
        //}

        GridView1.Visible = true;

        //SqlDataSource3.SelectParameters["section_id"].DefaultValue = GridView1.Rows[0].Cells[3].Text;
        //courseGridview1.DataBind();

        //SqlDataSource4.SelectParameters["section_id"].DefaultValue = GridView1.Rows[1].Cells[3].Text;
        //courseGridview2.DataBind();

        //SqlDataSource5.SelectParameters["section_id"].DefaultValue = GridView1.Rows[2].Cells[3].Text;
        //courseGridview3.DataBind();

        //SqlDataSource6.SelectParameters["section_id"].DefaultValue = GridView1.Rows[3].Cells[3].Text;
        //courseGridview4.DataBind();

    }
    


    //protected void viewAverage(float courseAverage, int courseNum)
    //{
        //if (courseNum == 1)
        //{
        //    if (courseAverage >= 90.0f)
        //    {
        //        courseAverage1.InnerText = "A";
        //        courseAverage1.Attributes["class"] = "text-success";
        //    }
        //    else if (courseAverage >= 80.0f)
        //    {
        //        courseAverage1.InnerText = "B";
        //        courseAverage1.Attributes["class"] = "text-info";
        //    }
        //    else if (courseAverage >= 70.0f)
        //    {
        //        courseAverage1.InnerText = "C";
        //        courseAverage1.Attributes["class"] = "text-warning";
        //    }
        //    else if (courseAverage >= 60.0f)
        //    {
        //        courseAverage1.InnerText = "D";
        //        courseAverage1.Attributes["class"] = "text-danger";
        //    }
        //    else if (courseAverage < 60.0f)
        //    {
        //        courseAverage1.InnerText = "F";
        //        courseAverage1.Attributes["class"] = "text-danger";
        //    }
            
        //}
        //else if (courseNum == 2)
        //{
        //    if (courseAverage >= 90.0f)
        //    {
        //        courseAverage2.InnerText = "A";
        //        courseAverage2.Attributes["class"] = "text-success";
        //    }
        //    else if (courseAverage >= 80.0f)
        //    {
        //        courseAverage2.InnerText = "B";
        //        courseAverage2.Attributes["class"] = "text-info";
        //    }
        //    else if (courseAverage >= 70.0f)
        //    {
        //        courseAverage2.InnerText = "C";
        //        courseAverage2.Attributes["class"] = "text-warning";
        //    }
        //    else if (courseAverage >= 60.0f)
        //    {
        //        courseAverage2.InnerText = "D";
        //        courseAverage2.Attributes["class"] = "text-danger";
        //    }
        //    else if (courseAverage < 60.0f)
        //    {
        //        courseAverage2.InnerText = "F";
        //        courseAverage2.Attributes["class"] = "text-danger";
        //    }
           
        //}
        //else if (courseNum == 3)
        //{
        //    if (courseAverage >= 90.0f)
        //    {
        //        courseAverage3.InnerText = "A";
        //        courseAverage3.Attributes["class"] = "text-success";
        //    }
        //    else if (courseAverage >= 80.0f)
        //    {
        //        courseAverage3.InnerText = "B";
        //        courseAverage3.Attributes["class"] = "text-info";
        //    }
        //    else if (courseAverage >= 70.0f)
        //    {
        //        courseAverage3.InnerText = "C";
        //        courseAverage3.Attributes["class"] = "text-warning";
        //    }
        //    else if (courseAverage >= 60.0f)
        //    {
        //        courseAverage3.InnerText = "D";
        //        courseAverage3.Attributes["class"] = "text-danger";
        //    }
        //    else if (courseAverage < 60.0f)
        //    {
        //        courseAverage3.InnerText = "F";
        //        courseAverage3.Attributes["class"] = "text-danger";
        //    }
           
        //}
        //else if (courseNum == 4)
        //{
        //    if (courseAverage >= 90.0f)
        //    {
        //        courseAverage4.InnerText = "A";
        //        courseAverage4.Attributes["class"] = "text-success";
        //    }
        //    else if (courseAverage >= 80.0f)
        //    {
        //        courseAverage4.InnerText = "B";
        //        courseAverage4.Attributes["class"] = "text-info";
        //    }
        //    else if (courseAverage >= 70.0f)
        //    {
        //        courseAverage4.InnerText = "C";
        //        courseAverage4.Attributes["class"] = "text-warning";
        //    }
        //    else if (courseAverage >= 60.0f)
        //    {
        //        courseAverage4.InnerText = "D";
        //        courseAverage4.Attributes["class"] = "text-danger";
        //    }
        //    else if (courseAverage < 60.0f)
        //    {
        //        courseAverage4.InnerText = "F";
        //        courseAverage4.Attributes["class"] = "text-danger";
        //    }
           
        //}

    //}

    //protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
        //GridView gridview = (GridView)sender;
        //Label average = (Label)gridview.Parent.FindControl("average");
        //Label letterAverage = (Label)gridview.Parent.FindControl("courseAverage1");

        //Label test = (Label)gridview.Parent.FindControl("testLabel");
        //test.Text = average.Text;

        //if (average.Text == "1000")
        //{
        //    letterAverage.Text = "Grades Unavailable";
        //    letterAverage.Attributes["class"] = "text-danger";
        //}
        //else
        //{
        //    float courseAverage;
        //    float.TryParse(average.Text, out courseAverage);
        //    if (courseAverage >= 90.0f)
        //    {
        //        letterAverage.Text = "A";
        //        letterAverage.Attributes["class"] = "text-success";
        //    }
        //    else if (courseAverage >= 80.0f)
        //    {
        //        letterAverage.Text = "B";
        //        letterAverage.Attributes["class"] = "text-info";
        //    }
        //    else if (courseAverage >= 70.0f)
        //    {
        //        letterAverage.Text = "C";
        //        letterAverage.Attributes["class"] = "text-warning";
        //    }
        //    else if (courseAverage >= 60.0f)
        //    {
        //        letterAverage.Text = "D";
        //        letterAverage.Attributes["class"] = "text-danger";
        //    }
        //    else if (courseAverage >= 50.0f)
        //    {
        //        letterAverage.Text = "F";
        //        letterAverage.Attributes["class"] = "text-danger";
        //    }
        //    else
        //    {
        //        letterAverage.Text = "Average Unavailable";
        //    }
        //}
    //}

    protected void repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        Label average = (Label)item.FindControl("average");
        Label letterAverage = (Label)item.FindControl("courseAverage1");

        //Label test = (Label)item.FindControl("testLabel");
        //test.Text = average.Text;

        if (average.Text == "1000")
        {
            letterAverage.Text = "Average Unavailable";
            letterAverage.Attributes["class"] = "text-danger";
        }
        else
        {
            float courseAverage;
            string.Format(average.Text, 2f);
            float.TryParse(average.Text, out courseAverage);
            if (courseAverage >= 90.0f)
            {
                letterAverage.Text = $" A ({courseAverage.ToString("F2")}%)";
                letterAverage.Attributes["class"] = "text-success";
            }
            else if (courseAverage >= 80.0f)
            {
                letterAverage.Text = $" B ({courseAverage.ToString("F2")}%)";
                letterAverage.Attributes["class"] = "text-info";
            }
            else if (courseAverage >= 70.0f)
            {
                letterAverage.Text = $" C ({courseAverage.ToString("F2")}%)";
                letterAverage.Attributes["class"] = "text-warning";
            }
            else if (courseAverage >= 60.0f)
            {
                letterAverage.Text = $" D ({courseAverage.ToString("F2")}%)";
                letterAverage.Attributes["class"] = "text-danger";
            }
            else
            {
                letterAverage.Text = $" F ({courseAverage.ToString("F2")}%)";
                letterAverage.Attributes["class"] = "text-danger";
            }
        }
    }
}
