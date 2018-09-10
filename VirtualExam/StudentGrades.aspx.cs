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
    string sectionId1 = "";
    string sectionId2 = "";
    string sectionId3 = "";
    string sectionId4 = "";
    int userName = 0;
    float averageCourse1 = 0.0f;
    float averageCourse2 = 0.0f;
    float averageCourse3 = 0.0f;
    float averageCourse4 = 0.0f;


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

        // Get the username and section id, Jo 3/29/2018 20:57
        userName = Convert.ToInt32(Session["userName"]);

        SqlDataSource0.SelectCommand = "exec View_Student_Courses " + userName;
        int rowCount = GridView0.Rows.Count;
        GridView0.Visible = false;
        

        if (rowCount == 0)
        {
            noClassGrade.Visible = true;
            //classOne.Visible = false;
            //classTwo.Visible = false;
            //classThree.Visible = false;
            //classFour.Visible = false;
        }
        else
        {
            noClassGrade.Visible = false;
        }


    //    else if (rowCount == 1)
    //    {
    //        noClassGrade.Visible = false;
    //        classTwo.Visible = false;
    //        classThree.Visible = false;
    //        classFour.Visible = false;
    //        class1Grade();
    //    }
    //    else if (rowCount == 2)
    //    {
    //        noClassGrade.Visible = false;
    //        classThree.Visible = false;
    //        classFour.Visible = false;
    //        class1Grade();
    //        class2Grade();
    //    }
    //    else if (rowCount == 3)
    //    {
    //        noClassGrade.Visible = false;
    //        classFour.Visible = false;
    //        class1Grade();
    //        class2Grade();
    //        class3Grade();

    //    }
    //    else if (rowCount == 4)
    //    {
    //        noClassGrade.Visible = false;
    //        class1Grade();
    //        class2Grade();
    //        class3Grade();
    //        class4Grade();
    //    }
    //}

    //public void class1Grade()
    //{
    //    #region First class grade

    //    // Display the the course number and section number, Jo 3 / 29 / 2018 20:57
    //    SqlDataSource1.SelectCommand = "exec View_Student_Courses " + userName;
    //    sectionId1 = GridView1.Rows[0].Cells[3].Text;
    //    courseNoOne.Text = GridView1.Rows[0].Cells[0].Text + "-" + GridView1.Rows[0].Cells[2].Text;
    //    courseTitleOne.Text = GridView1.Rows[0].Cells[1].Text;
    //    GridView1.Visible = false;

    //    // Display the teacher name, Jo 3/29/2018 20:57
    //    SqlDataSource1.SelectCommand = "exec Get_Teacher_Name " + sectionId1;
    //    teacherNameOne.Text = GridView2.Rows[0].Cells[0].Text + " " + GridView2.Rows[0].Cells[1].Text;
    //    GridView2.Visible = false;

    //    SqlDataSource1.SelectCommand = "exec View_Student_Test_Score " + sectionId1 + ", " + userName;
    //    GridView3.Visible = true;

    //    // Display the class average, Jo 3/29/2018 20:57
    //    SqlDataSource2.SelectCommand = "exec View_Student_Class_Average " + sectionId1 + ", " + userName;
    //    GridView4.Visible = false;

    //    tempGrade1.InnerText = GridView4.Rows[0].Cells[0].Text;
    //    if (tempGrade1.InnerText == "&nbsp;")
    //    {
    //        averageCourseOne.Text = "No Grades";
    //    }
    //    else
    //    {
    //        float.TryParse(GridView4.Rows[0].Cells[0].Text, out averageCourse1);

    //        if (averageCourse1 >= 90.0f)
    //        {
    //            averageCourseOne.Text = "A";
    //            averageCourseOne.Attributes["class"] = "text-success";
    //        }
    //        else if (averageCourse1 >= 80.0f)
    //        {
    //            averageCourseOne.Text = "B";
    //            averageCourseOne.Attributes["class"] = "text-info";
    //        }
    //        else if (averageCourse1 >= 70.0f)
    //        {
    //            averageCourseOne.Text = "C";
    //            averageCourseOne.Attributes["class"] = "text-warning";
    //        }
    //        else if (averageCourse1 >= 60.0f)
    //        {
    //            averageCourseOne.Text = "D";
    //            averageCourseOne.Attributes["class"] = "text-danger";
    //        }
    //        else
    //        {
    //            averageCourseOne.Text = "F";
    //            averageCourseOne.Attributes["class"] = "text-danger";
    //        }
    //    }
    //    #endregion
    //}

    //public void class2Grade()
    //{
    //    #region Second class grade

    //    // Display the the course number and section number, Jo 3 / 29 / 2018 20:57
    //    SqlDataSource3.SelectCommand = "exec View_Student_Courses " + userName;
    //    sectionId2 = GridView5.Rows[1].Cells[3].Text;
    //    courseNoTwo.Text = GridView5.Rows[1].Cells[0].Text + "-" + GridView5.Rows[1].Cells[2].Text;
    //    courseTitleTwo.Text = GridView5.Rows[1].Cells[1].Text;
    //    GridView5.Visible = false;

    //    // Display the teacher name, Jo 3 / 29 / 2018 20:57
    //    SqlDataSource3.SelectCommand = "exec Get_Teacher_Name " + sectionId2;
    //    teacherNameTwo.Text = GridView6.Rows[0].Cells[0].Text + " " + GridView6.Rows[0].Cells[1].Text;
    //    GridView6.Visible = false;

    //    SqlDataSource3.SelectCommand = "exec View_Student_Test_Score " + sectionId2 + ", " + userName;
    //    GridView7.Visible = true;

    //    // Display the class average, Jo 3/29/2018 20:57
    //    SqlDataSource4.SelectCommand = "exec View_Student_Class_Average " + sectionId2 + ", " + userName;
    //    GridView8.Visible = false;

    //    tempGrade2.InnerText = GridView8.Rows[0].Cells[0].Text;
    //    if (tempGrade2.InnerText == "&nbsp;")
    //    {
    //        averageCourseTwo.Text = "No Grades";
    //    }
    //    else
    //    {
    //        float.TryParse(GridView7.Rows[0].Cells[0].Text, out averageCourse2);

    //        if (averageCourse2 >= 90.0f)
    //        {
    //            averageCourseTwo.Text = "A";
    //            averageCourseTwo.Attributes["class"] = "text-success";
    //        }
    //        else if (averageCourse2 >= 80.0f)
    //        {
    //            averageCourseTwo.Text = "B";
    //            averageCourseTwo.Attributes["class"] = "text-info";
    //        }
    //        else if (averageCourse2 >= 70.0f)
    //        {
    //            averageCourseTwo.Text = "C";
    //            averageCourseTwo.Attributes["class"] = "text-warning";
    //        }
    //        else if (averageCourse2 >= 60.0f)
    //        {
    //            averageCourseTwo.Text = "D";
    //            averageCourseTwo.Attributes["class"] = "text-danger";
    //        }
    //        else
    //        {
    //            averageCourseTwo.Text = "F";
    //            averageCourseTwo.Attributes["class"] = "text-danger";
    //        }
    //    }
    //    #endregion
    //}

    //public void class3Grade()
    //{
    //    #region Third class grade

    //    // Display the the course number and section number, Jo 3/29/2018 20:57
    //    SqlDataSource5.SelectCommand = "exec View_Student_Courses " + userName;
    //    sectionId3 = GridView9.Rows[2].Cells[3].Text;
    //    courseNoThree.Text = GridView9.Rows[2].Cells[0].Text + "-" + GridView9.Rows[2].Cells[2].Text;
    //    courseTitleThree.Text = GridView9.Rows[2].Cells[1].Text;
    //    GridView9.Visible = false;

    //    // Display the teacher name, Jo 3/29/2018 20:57
    //    SqlDataSource5.SelectCommand = "exec Get_Teacher_Name " + sectionId3;
    //    teacherNameThree.Text = GridView10.Rows[0].Cells[0].Text + " " + GridView10.Rows[0].Cells[1].Text;
    //    GridView10.Visible = false;

    //    SqlDataSource5.SelectCommand = "exec View_Student_Test_Score " + sectionId3 + ", " + userName;
    //    GridView11.Visible = true;

    //    //Display the class average, Jo 3/29/2018 20:57
    //    SqlDataSource6.SelectCommand = "exec View_Student_Class_Average " + sectionId3 + ", " + userName;

    //    GridView12.Visible = false;
    //    tempGrade3.InnerText = GridView12.Rows[0].Cells[0].Text;
    //    if (tempGrade3.InnerText == "&nbsp;")
    //    {
    //        averageCourseThree.Text = "No Grades";
    //    }
    //    else
    //    {
    //        float.TryParse(GridView12.Rows[0].Cells[0].Text, out averageCourse3);

    //        if (averageCourse3 >= 90.0f)
    //        {
    //            averageCourseThree.Text = "A";
    //            averageCourseThree.Attributes["class"] = "text-success";
    //        }
    //        else if (averageCourse3 >= 80.0f)
    //        {
    //            averageCourseThree.Text = "B";
    //            averageCourseThree.Attributes["class"] = "text-info";
    //        }
    //        else if (averageCourse3 >= 70.0f)
    //        {
    //            averageCourseThree.Text = "C";
    //            averageCourseThree.Attributes["class"] = "text-warning";
    //        }
    //        else if (averageCourse3 >= 60.0f)
    //        {
    //            averageCourseThree.Text = "D";
    //            averageCourseThree.Attributes["class"] = "text-danger";
    //        }
    //        else
    //        {
    //            averageCourseThree.Text = "F";
    //            averageCourseThree.Attributes["class"] = "text-danger";
    //        }
    //    }
    //    #endregion
    //}

    //public void class4Grade()
    //{
    //    #region Fourth class grade
    //    // Display the the course number and section number, Jo 3/29/2018 20:57
    //    SqlDataSource7.SelectCommand = "exec View_Student_Courses " + userName;
    //    sectionId4 = GridView13.Rows[3].Cells[3].Text;
    //    courseNoFour.Text = GridView13.Rows[3].Cells[0].Text + "-" + GridView13.Rows[3].Cells[2].Text;
    //    courseTitleFour.Text = GridView13.Rows[3].Cells[1].Text;
    //    GridView13.Visible = false;

    //    // Display the teacher name, Jo 3/29/2018 20:57
    //    SqlDataSource7.SelectCommand = "exec Get_Teacher_Name " + sectionId4;
    //    teacherNameFour.Text = GridView14.Rows[0].Cells[0].Text + " " + GridView14.Rows[0].Cells[1].Text;
    //    GridView14.Visible = false;

    //    SqlDataSource7.SelectCommand = "exec View_Student_Test_Score " + sectionId4 + ", " + userName;

    //    // Display the class average, Jo 3/29/2018 20:57
    //    SqlDataSource8.SelectCommand = "exec View_Student_Class_Average " + sectionId4 + ", " + userName;

    //    GridView16.Visible = false;
    //    tempGrade4.InnerText = GridView16.Rows[0].Cells[0].Text;
    //    if (tempGrade4.InnerText != "&nbsp;")
    //    {
    //        averageCourseFour.Text = "No Grades";
    //    }
    //    else
    //    {
    //        float.TryParse(GridView16.Rows[0].Cells[0].Text, out averageCourse4);

    //        if (averageCourse4 >= 90.0f)
    //        {
    //            averageCourseFour.Text = "A";
    //            averageCourseFour.Attributes["class"] = "text-success";
    //        }
    //        else if (averageCourse4 >= 80.0f)
    //        {
    //            averageCourseFour.Text = "B";
    //            averageCourseFour.Attributes["class"] = "text-info";
    //        }
    //        else if (averageCourse4 >= 70.0f)
    //        {
    //            averageCourseFour.Text = "C";
    //            averageCourseFour.Attributes["class"] = "text-warning";
    //        }
    //        else if (averageCourse4 >= 60.0f)
    //        {
    //            averageCourseFour.Text = "D";
    //            averageCourseFour.Attributes["class"] = "text-danger";
    //        }
    //        else
    //        {
    //            averageCourseFour.Text = "F";
    //            averageCourseFour.Attributes["class"] = "text-danger";
    //        }
    //    }
    //    #endregion
    }
    protected void repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        Label average = (Label)item.FindControl("Average");
        Label letterAverage = (Label)item.FindControl("courseAverage1");
        
        if (average.Text == "")
        {
            letterAverage.Text = "Grades Unavailable";
            letterAverage.Attributes["class"] = "text-danger";
        }
        else
        {
            float courseAverage;
            float.TryParse(average.Text, out courseAverage);
            if (courseAverage >= 90.0f)
            {
                letterAverage.Text = "A";
                letterAverage.Attributes["class"] = "text-success";
            }
            else if (courseAverage >= 80.0f)
            {
                letterAverage.Text = "B";
                letterAverage.Attributes["class"] = "text-info";
            }
            else if (courseAverage >= 70.0f)
            {
                letterAverage.Text = "C";
                letterAverage.Attributes["class"] = "text-warning";
            }
            else if (courseAverage >= 60.0f)
            {
                letterAverage.Text = "D";
                letterAverage.Attributes["class"] = "text-danger";
            }
            else
            {
                letterAverage.Text = "F";
                letterAverage.Attributes["class"] = "text-danger";
            }
        }
    }
}