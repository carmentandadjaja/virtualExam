using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;                   // Need these for the database connection.  Erik, 2/23/18 20:31
using System.Configuration;          // Need these for the database connection.  Erik, 2/23/18 20:31
using System.Data.SqlClient;         // Need these for the database connection.  Erik, 2/23/18 20:31
using System.Drawing;                // Need these for the database connection.  Jo, 3/9/18 14:22
using System.Activities.Expressions;

public partial class Default2 : System.Web.UI.Page
{
    private static int button = 1; // Intial Button set to show active. Jo, 3/10/2018 18:53
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            button = 1; // Always set to active gridview everytime it loads. Jo, 3/10/18 20:47
        }

        // This ensures that only users with the correct credentials have access to their respective pages. Erik, 3/1/18 12:54
        if (Convert.ToChar(Session["userType"]) == 't')
        {
            Response.Redirect("TeacherHome.aspx");
        }
        else if (Convert.ToChar(Session["userType"]) == 's')
        {
            Response.Redirect("StudentHome.aspx");
        }
        else if (Session["userType"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        // This receives a value from the url in the browswer. Erik, 3/1/18 12:52
        string h_value = Request.QueryString["field1"];

        // This is will dynamically fill the gridview and show the appropriate boxes for adding something to the database 
        // based upon the user's selection.  Erik 3/1/18 12:51
        switch (h_value)
        {

            case "value1": // case "teacher"

                // Button to pass which button is clicked. Jo, 3/10/2018 18:53
                selectedButtonTeacherUpdate();
                GridView1.DataBind();
                submitBtn.Text = "Add Teacher";
                textBox1.Visible = true;
                // Set the max length so a overflow was avoided. Richard, 4/27/18
                textBox1.MaxLength = 25; 
                textBox2.MaxLength = 30;
                errormsg1.Visible = false;

                break;

            case "value2": // case "student"

                // Button to pass which button is clicked. Jo, 3/10/2018 18:53
                selectedButtonStudentUpdate();
                GridView1.DataBind();
                submitBtn.Text = "Add Student";
                textBox1.Visible = true;
                // Set the max length so a overflow was avoided. Richard, 4/27/18
                textBox1.MaxLength = 25;
                textBox2.MaxLength = 30;
                errormsg1.Visible = false;
                textBox4.Visible = true;

                break;

            case "value3": // case "course"

                SqlDataSource1.SelectCommand = "exec View_All_Courses";
                GridView1.DataBind();

                Label1.Text = "Course Number";
                Label2.Text = "Course Title";
                Label3.Text = "Teacher ID";
                Label4.Visible = false;
                textBox1.MaxLength = 6;
                textBox2.MaxLength = 255;
                textBox3.MaxLength = 6;
                textBox4.Visible = false;
                submitBtn.Text = "Add Course";
                showActive.Visible = false;
                showInactive.Visible = false;
                showAll.Visible = false;

                break;

            case "value4": // case "section"

                SqlDataSource1.SelectCommand = "exec View_All_Sections";
                GridView1.DataBind();

                Label1.Text = "Course Number";
                Label2.Text = "Teacher ID";
                submitBtn.Text = "Add Section";
                Label3.Visible = false;
                Label4.Visible = false;
                textBox1.MaxLength = 6;
                textBox2.MaxLength = 6;
                textBox3.Visible = false;
                textBox4.Visible = false;
                errormsg4.Visible = false;
                showActive.Visible = false;
                showInactive.Visible = false;
                showAll.Visible = false;

                break;

            default:

                SqlDataSource1.SelectCommand = "exec View_All_Students";
                GridView1.DataBind();
                textBox4.Visible = true;

                break;
        }
    }

    protected void submitBtn_Click(object sender, EventArgs e)
    {
        // This receives a value from the url in the browswer. Erik, 3/1/18 12:52
        string h_value = Request.QueryString["field1"];

        bool result = false;
        bool confirmResult = false;
        int number = 0; // This is created purely for the TryParse function.  It is not used for anything else.  Erik, 3/1/18 19:05
        int confirmNumber = 0;

        errormsg1.Visible = false;
        errormsg2.Visible = false;
        errormsg3.Visible = false;
        errormsg4.Visible = false;

        switch (h_value)
        {
            #region CaseTeacher
            case "value1": // case "teacher"
                
                // Test the input for the teacher's first name.  Erik, 3/1/18 19:00
                if (textBox1.Text == "")
                {
                    errormsg1.Text = "Please enter only letters for the new teacher's first name!!!";
                    errormsg1.Visible = true;
                }
                else
                {
                    foreach (char letter in textBox1.Text)
                    {
                        if (!Char.IsLetter(letter))
                        {
                            errormsg1.Text = "Please enter only letters for the new teacher's first name!!!";
                            errormsg1.Visible = true;
                        }
                    }
                }
                // Test the input for the teacher's last name.  Erik, 3/1/18 19:00
                if (textBox2.Text == "")
                {
                    errormsg2.Text = "Please enter only letters for the new teacher's last name!!!";
                    errormsg2.Visible = true;
                }
                else
                {
                    foreach (char letter in textBox2.Text)
                    {
                        if (!Char.IsLetter(letter))
                        {
                            errormsg2.Text = "Please enter only letters for the new teacher's last name!!!";
                            errormsg2.Visible = true;
                        }
                    }
                }

                // Test the input for the teacher's password.  Erik, 3/1/18 19:00
                result = Int32.TryParse(textBox3.Text, out number);
                confirmResult = Int32.TryParse(textBox4.Text, out confirmNumber);
                if (!result)
                {
                    errormsg3.Text = "Please enter only whole numbers for the new teacher's password!!!";
                    errormsg3.Visible = true;
                }
                if (!confirmResult)
                {
                    errormsg4.Text = "Please enter only whole numbers for the new teacher's password!!!";
                    errormsg4.Visible = true;
                }
                if (number != confirmNumber)
                {
                    errormsg3.Text = "Password does not match!!!";
                    errormsg3.Visible = true;
                    errormsg4.Text = "Password does not match!!!";
                    errormsg4.Visible = true;
                }

                break;
            #endregion

            #region CaseStudent
            case "value2": // case "student"

                // Test the input for the student's first name.  Erik, 3/1/18 19:00
                if (textBox1.Text == "")
                {
                    errormsg1.Text = "Please enter only letters for the new student's first name!!!";
                    errormsg1.Visible = true;
                }
                else
                {
                    foreach (char letter in textBox1.Text)
                    {
                        if (!Char.IsLetter(letter))
                        {
                            errormsg1.Text = "Please enter only letters for the new student's first name!!!";
                            errormsg1.Visible = true;
                        }
                    }
                }

                // Test the input for the student's last name.  Erik, 3/1/18 19:00
                if (textBox2.Text == "")
                {
                    errormsg2.Text = "Please enter only letters for the new student's last name!!!";
                    errormsg2.Visible = true;
                }
                else
                {
                    foreach (char letter in textBox2.Text)
                    {
                        if (!Char.IsLetter(letter))
                        {
                            errormsg2.Text = "Please enter only letters for the new student's last name!!!";
                            errormsg2.Visible = true;
                        }
                    }
                }

                // Test the input for the student's password.  Erik, 3/1/18 19:00
                result = Int32.TryParse(textBox3.Text, out number);
                confirmResult = Int32.TryParse(textBox4.Text, out confirmNumber);
                if (!result)
                {
                    errormsg3.Text = "Please enter only whole numbers for the new teacher's password!!!";
                    errormsg3.Visible = true;
                }
                if (!confirmResult)
                {
                    errormsg4.Text = "Please enter only whole numbers for the new teacher's password!!!";
                    errormsg4.Visible = true;
                }
                if (number != confirmNumber)
                {
                    errormsg3.Text = "Password does not match!!!";
                    errormsg3.Visible = true;
                    errormsg4.Text = "Password does not match!!!";
                    errormsg4.Visible = true;
                }

                break;
            #endregion

            #region  CaseCourse
            case "value3": // case "course"

                
                // Test the input for the course number.  Erik, 3/1/18 19:00
                if (textBox1.Text == "")
                {
                    errormsg1.Text = "Please enter only alphanumeric characters for the course number!!!";
                    errormsg1.Visible = true;
                }
                else
                {
                    foreach (char letterOrDigit in textBox1.Text)
                    {
                        if (!Char.IsLetterOrDigit(letterOrDigit) && !Char.IsWhiteSpace(letterOrDigit))
                        {
                            errormsg1.Text = "Please enter only alphanumeric characters for the course number!!!";
                            errormsg1.Visible = true;
                        }
                    }
                }

                // Test the input for the course title.  Erik, 3/1/18 19:00
                if (textBox2.Text == "")
                {
                    errormsg2.Text = "Please enter only letters for the course title!!!";
                    errormsg2.Visible = true;
                }
                else
                {
                    foreach (char letter in textBox2.Text)
                    {
                        if (!Char.IsLetter(letter) && !Char.IsWhiteSpace(letter))
                        {
                            errormsg2.Text = "Please enter only letters for the course title!!!";
                            errormsg2.Visible = true;
                        }
                    }
                }

                //Test the input for the teacher's id.  Erik, 3/1/18 19:00

                result = Int32.TryParse(textBox3.Text, out number);

                if (!result)
                {
                    errormsg3.Text = "Please enter only whole numbers for the teacher's id number!!!";
                    errormsg3.Visible = true;
                }

                selectedButtonStudentUpdate(); // Checks and update the gridview after deleting a teacher, Jo 3/10/18 19:18

                break;
            #endregion

            #region CaseSection
            case "value4": // case "section"
                

                // Test the input for the course number.  Erik, 3/1/18 19:00
                if (textBox1.Text == "")
                {
                    errormsg1.Text = "Please enter only alphanumeric characters for the course number!!!";
                    errormsg1.Visible = true;
                }
                else
                {
                    foreach (char letterOrDigit in textBox1.Text)
                    {
                        if (!Char.IsLetterOrDigit(letterOrDigit) && !Char.IsWhiteSpace(letterOrDigit))
                        {
                            errormsg1.Text = "Please enter only alphanumeric characters for the course number!!!";
                            errormsg1.Visible = true;
                        }
                    }
                }

                // Test the input for the teacher's id.  Erik, 3/1/18 19:00
                result = Int32.TryParse(textBox2.Text, out number);

                if (!result)
                {
                    errormsg2.Text = "Please enter only whole numbers for the teacher's id number!!!";
                    errormsg2.Visible = true;
                }

                break;

            default:

                break;
                #endregion
        }

        #region DatabaseAddConnection
        if (errormsg1.Visible == false && errormsg2.Visible == false &&
            errormsg3.Visible == false && errormsg4.Visible == false)
        {
            // Make the database connection to add the student here.  This if check ensures that there was nothing wrong with the data that was inputed.  Erik, 3/1/18 20:08

            string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

            using (SqlConnection DBconnection = new SqlConnection(constr))
            {
                switch (h_value)
                {
                    #region AddTeacher
                    case "value1":

                        using (SqlCommand cmd = new SqlCommand("Create_Teacher"))
                        {
                            string hash = SimpleHash.ComputeHash(textBox3.Text, "SHA256", null);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@fName", textBox1.Text);
                            cmd.Parameters.AddWithValue("@lName", textBox2.Text);
                            cmd.Parameters.AddWithValue("@teacherPassword", hash);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }

                        button = 1;
                        SqlDataSource1.SelectCommand = "exec Select_Active_Teachers"; // Updates the gridview after adding a teacher, Jo 3/10/18 19:18
                        GridView1.DataBind();

                        break;
                    #endregion

                    #region AddStudent
                    case "value2":

                        using (SqlCommand cmd = new SqlCommand("Create_Student"))
                        {
                            string hash = SimpleHash.ComputeHash(textBox3.Text, "SHA256", null);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@fName", textBox1.Text);
                            cmd.Parameters.AddWithValue("@lName", textBox2.Text);
                            cmd.Parameters.AddWithValue("@studentPassword", hash);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }

                        button = 1;
                        SqlDataSource1.SelectCommand = "exec Select_Active_Students"; // Updates the gridview after adding a student, Jo 3/10/18 19:18
                        GridView1.DataBind();


                        break;
                    #endregion

                    #region AddCourse
                    case "value3":

                        using (SqlCommand cmd = new SqlCommand("Create_Course"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@courseNum", textBox1.Text);
                            cmd.Parameters.AddWithValue("@courseTitle", textBox2.Text);
                            cmd.Parameters.AddWithValue("@teacherId", textBox3.Text);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }

                        SqlDataSource1.SelectCommand = "exec View_All_Courses";  // Updates the gridview after adding a course, Erik 3/3/18 13:10
                        GridView1.DataBind();


                        break;
                    #endregion

                    #region AddSection
                    case "value4":

                        using (SqlCommand cmd = new SqlCommand("Create_Section"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@courseNum", textBox1.Text);
                            cmd.Parameters.AddWithValue("@teacherId", textBox2.Text);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }

                        SqlDataSource1.SelectCommand = "exec View_All_Sections"; // Updates the gridview after adding a section. Erik, 3/3/18 13:35
                        GridView1.DataBind();

                        break;
                    #endregion

                    default:

                        break;
                }
            }
        }
        #endregion

        // These need to come after extracting the data out of those fields and into our database variables. Erik, 3/1/18 12:49 
        textBox1.Text = "";
        textBox2.Text = "";
        textBox3.Text = "";
        textBox4.Text = "";

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
        string h_value = Request.QueryString["field1"];

        // This lock down primary key column. Eric, 3/12/2018 13:07
        if (h_value == "value4")
        {
            e.Row.Cells[2].Enabled = false;
            e.Row.Cells[3].Enabled = false;
        }
        else
        {
            e.Row.Cells[2].Enabled = false;
        }
       
        // Change the gridview header name. Jo, 3/10/2018 15:42
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (h_value == "value1")
            {
                e.Row.Cells[2].Text = "Teacher ID";
                LinkButton TeacherFNameHeaderText = e.Row.Cells[3].Controls[0] as LinkButton;
                TeacherFNameHeaderText.Text = "First Name";
                LinkButton TeacherLNameHeaderText = e.Row.Cells[4].Controls[0] as LinkButton;
                TeacherLNameHeaderText.Text = "Last Name";
                LinkButton TeacherSNameHeaderText = e.Row.Cells[5].Controls[0] as LinkButton;
                TeacherSNameHeaderText.Text = "Status";
            }
            if (h_value == "value2")
            {
                e.Row.Cells[2].Text = "Student ID";
                LinkButton StudentFNameHeaderText = e.Row.Cells[3].Controls[0] as LinkButton;
                StudentFNameHeaderText.Text = "First Name";
                LinkButton StudentLNameHeaderText = e.Row.Cells[4].Controls[0] as LinkButton;
                StudentLNameHeaderText.Text = "Last Name";
                LinkButton StudentSNameHeaderText = e.Row.Cells[5].Controls[0] as LinkButton;
                StudentSNameHeaderText.Text = "Status";
            }
            if (h_value == "value3")
            {
                e.Row.Cells[2].Text = "Course Number";
                LinkButton CourseTitleHeaderText = e.Row.Cells[3].Controls[0] as LinkButton;
                CourseTitleHeaderText.Text = "Course Title";
            }
            if (h_value == "value4")
            {
                e.Row.Cells[2].Text = "Section ID";
                e.Row.Cells[3].Text = "Course Number";
                LinkButton TeacherIDHeaderText = e.Row.Cells[4].Controls[0] as LinkButton;
                TeacherIDHeaderText.Text = "Teacher ID";
                LinkButton SectionNumHeaderText = e.Row.Cells[5].Controls[0] as LinkButton;
                SectionNumHeaderText.Text = "Section Number";
            }
        }

        // Display different color of row. Jo, 3/9/2018 14:22
        #region ColumnStyling

       
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Alternate)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.BackColor = Color.FromName("d6d6d6");
            }
        }

        else
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.BackColor = Color.FromName("white");
            }
        }
            #endregion
        }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        errormsg1.Visible = false;
        errormsg2.Visible = false;
        errormsg3.Visible = false;
        errormsg4.Visible = false;

        // This receives a value from the url in the browswer. Erik, 3/1/18 12:52
        string h_value = Request.QueryString["field1"];

        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

        using (SqlConnection DBconnection = new SqlConnection(constr))
        {

            switch (h_value)
            {
                #region UpdateTeacher
                case "value1":

                    int teacherId = 0;
                    int teacherStatus = 0;
                    string teacherfName = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text;
                    string teacherlName = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text;
                    
                    try
                    {
                        teacherId = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text);
                        teacherStatus = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text);
                    }
                    catch(FormatException)
                    {
                        errormsg4.Text = "The status of any student or teacher can only be a 1 or a 0!!!";
                        errormsg4.Visible = true;
                        errormsg1.Visible = false;
                        errormsg2.Visible = false;
                        errormsg3.Visible = false;
                    }

                    // Test the input for the teacher's first name.  Erik, 3/1/18 19:00
                    if (teacherfName == "")
                    {
                        errormsg2.Text = "Please enter only letters for the teacher's first name!!!";
                        errormsg2.Visible = true;
                    }
                    else
                    {
                        foreach (char letter in teacherfName)
                        {
                            if (!Char.IsLetter(letter))
                            {
                                errormsg2.Text = "Please enter only letters for the teacher's first name!!!";
                                errormsg2.Visible = true;
                            }
                        }
                    }

                    // Test the input for the teacher's last name.  Erik, 3/1/18 19:00
                    if (teacherlName == "")
                    {
                        errormsg3.Text = "Please enter only letters for the teacher's last name!!!";
                        errormsg3.Visible = true;
                    }
                    else
                    {
                        foreach (char letter in teacherlName)
                        {
                            if (!Char.IsLetter(letter))
                            {
                                errormsg3.Text = "Please enter only letters for the teacher's last name!!!";
                                errormsg3.Visible = true;
                            }
                        }
                    }

                    if (teacherStatus != 1 && teacherStatus != 0)
                    {
                        errormsg4.Text = "The status of any student or teacher can only be a 1 or a 0!!!";
                        errormsg4.Visible = true;
                    }

                    if (errormsg1.Visible == false && errormsg2.Visible == false &&
                        errormsg3.Visible == false && errormsg4.Visible == false)

                    {
                        using (SqlCommand cmd = new SqlCommand("Update_User"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@user_id", teacherId);
                            cmd.Parameters.AddWithValue("@first_name", teacherfName);
                            cmd.Parameters.AddWithValue("@last_name", teacherlName);
                            cmd.Parameters.AddWithValue("@active", teacherStatus);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }

                        selectedButtonTeacherUpdate();
                       // SqlDataSource1.SelectCommand = "exec View_All_Teachers";  // Updates the gridview after updating a teacher, Erik 3/8/18 13:10
                        GridView1.DataBind();
                    }

                    break;
                #endregion

                #region UpdateStudent
                case "value2":

                    int studentId = 0;
                    string studentfName = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text;
                    string studentlName = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text;
                    int studentStatus = 0;

                    try
                    {
                        studentId = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text);
                        studentStatus = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text);
                    }
                    catch (FormatException)
                    {
                        errormsg4.Text = "The status of any student or teacher can only be a 1 or a 0!!!";
                        errormsg4.Visible = true;
                        errormsg1.Visible = false;
                        errormsg2.Visible = false;
                        errormsg3.Visible = false;
                    }

                    // Test the input for the student's first name.  Erik, 3/1/18 19:00
                    if (studentfName == "")
                    {
                        errormsg2.Text = "Please enter only letters for the student's first name!!!";
                        errormsg2.Visible = true;
                    }
                    else
                    {
                        foreach (char letter in studentfName)
                        {
                            if (!Char.IsLetter(letter))
                            {
                                errormsg2.Text = "Please enter only letters for the student's first name!!!";
                                errormsg2.Visible = true;
                            }
                        }
                    }

                    // Test the input for the student's last name.  Erik, 3/1/18 19:00
                    if (studentlName == "")
                    {
                        errormsg3.Text = "Please enter only letters for the student's last name!!!";
                        errormsg3.Visible = true;
                    }
                    else
                    {
                        foreach (char letter in studentlName)
                        {
                            if (!Char.IsLetter(letter))
                            {
                                errormsg3.Text = "Please enter only letters for the student's last name!!!";
                                errormsg3.Visible = true;
                            }
                        }
                    }

                    if (studentStatus != 1 && studentStatus != 0)
                    {
                        errormsg4.Text = "The status of any student or teacher can only be a 1 or a 0!!!";
                        errormsg4.Visible = true;
                    }

                    if (errormsg1.Visible == false && errormsg2.Visible == false &&
                        errormsg3.Visible == false && errormsg4.Visible == false)

                    {
                        using (SqlCommand cmd = new SqlCommand("Update_User"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@user_id", studentId);
                            cmd.Parameters.AddWithValue("@first_name", studentfName);
                            cmd.Parameters.AddWithValue("@last_name", studentlName);
                            cmd.Parameters.AddWithValue("@active", studentStatus);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }

                        selectedButtonStudentUpdate(); // Checks and update the gridview after deleting a teacher, Jo 3/10/18 19:18
                        //SqlDataSource1.SelectCommand = "exec View_All_Students";  // Updates the gridview after updating a student, Erik 3/8/18 13:10
                        GridView1.DataBind();
                    }

                    break;
                #endregion

                #region UpdateCourse
                case "value3":

                    string courseNumber = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text;
                    string courseTitle = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text;

                    // Test the input for the course number.  Erik, 3/1/18 19:00
                    if (courseNumber == "")
                    {
                        errormsg1.Text = "Please enter only alphanumeric characters for the course number!!!";
                        errormsg1.Visible = true;
                    }
                    else
                    {
                        foreach (char letterOrDigit in courseNumber)
                        {
                            if (!Char.IsLetterOrDigit(letterOrDigit) && !Char.IsWhiteSpace(letterOrDigit))
                            {
                                errormsg1.Text = "Please enter only alphanumeric characters for the course number!!!";
                                errormsg1.Visible = true;
                            }
                        }
                    }

                    // Test the input for the course title.  Erik, 3/1/18 19:00
                    if (courseTitle == "")
                    {
                        errormsg2.Text = "Please enter only letters for the course title!!!";
                        errormsg2.Visible = true;
                    }
                    else
                    {
                        foreach (char letter in courseTitle)
                        {
                            if (!Char.IsLetter(letter) && !Char.IsWhiteSpace(letter))
                            {
                                errormsg2.Text = "Please enter only letters for the course title!!!";
                                errormsg2.Visible = true;
                            }
                        }
                    }

                    if (errormsg1.Visible == false && errormsg2.Visible == false &&
                        errormsg3.Visible == false && errormsg4.Visible == false)

                    {
                        using (SqlCommand cmd = new SqlCommand("Update_Course"))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@courseNum", courseNumber);
                            cmd.Parameters.AddWithValue("@courseTitle", courseTitle);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (NotSupportedException)
                            {
                                string errormsg = "Unable to connect to the database! ";
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }

                        SqlDataSource1.SelectCommand = "exec View_All_Courses";  // Updates the gridview after updating a course, Erik 3/8/18 13:10
                        GridView1.DataBind();
                    }

                    break;
                #endregion

                #region UpdateSection
                case "value4":

                    int number = 0;  // This is only used as a test variable for try Parse.  Erik, 3/8/18 18:51

                    int sectionId = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text);
                    string courseNumberForSection = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text;
                    int teacherIdForSection = 0;
                    int sectionNumber = 0;
                    bool result = Int32.TryParse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text, out number);

                    try
                    {
                        teacherIdForSection = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text);
                    }
                    catch (FormatException)
                    {
                        errormsg1.Text = "You must enter a valid teacher ID number!";
                        errormsg1.Visible = true;
                        errormsg2.Visible = false;
                        errormsg3.Visible = false;
                        errormsg4.Visible = false;
                    }

                    if (teacherIdForSection < 100000)
                    {
                        errormsg1.Text = "You must enter a valid teacher ID number!";
                        errormsg1.Visible = true;
                    }

                    try
                    {
                        sectionNumber = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text);
                    }
                    catch (FormatException)
                    {
                        errormsg1.Text = "You must enter a whole number for a section number!";
                        errormsg1.Visible = true;
                        errormsg2.Visible = false;
                        errormsg3.Visible = false;
                        errormsg4.Visible = false;
                    }

                    if (sectionNumber <= 0)
                    {
                        errormsg1.Text = "You must enter a positive whole number for a section number!";
                        errormsg1.Visible = true;
                    }

                    if (!result)
                    {
                        errormsg1.Text = "Please enter only whole numbers for the teacher's id number!!!";
                        errormsg1.Visible = true;
                    }

                    result = Int32.TryParse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text, out number);

                    if (!result)
                    {
                        errormsg2.Text = "Please enter only whole numbers for the section number!!!";
                        errormsg2.Visible = true;
                    }

                    if (errormsg1.Visible == false && errormsg2.Visible == false &&
                        errormsg3.Visible == false && errormsg4.Visible == false)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update_Section"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@sectionId", sectionId);
                            cmd.Parameters.AddWithValue("@courseNum", courseNumberForSection);
                            cmd.Parameters.AddWithValue("@teacherId", teacherIdForSection);
                            cmd.Parameters.AddWithValue("@sectionNum", sectionNumber);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }

                        SqlDataSource1.SelectCommand = "exec View_All_Sections";  // Updates the gridview after updating a teacher, Erik 3/8/18 13:10
                        GridView1.DataBind();
                    }

                    break;
                #endregion

                default:

                    break;
            }
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        errormsg1.Visible = false;
        errormsg2.Visible = false;
        errormsg3.Visible = false;
        errormsg4.Visible = false;

        // This receives a value from the url in the browswer. Erik, 3/1/18 12:52
        string h_value = Request.QueryString["field1"];

        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

        using (SqlConnection DBconnection = new SqlConnection(constr))
        {

            switch (h_value)
            {
                #region DeleteTeacher
                case "value1":
                    int teacherId = 0;
                    try
                    {
                        teacherId = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[2].Text);
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    catch(ArgumentOutOfRangeException)
                    { }

                    if (errormsg1.Visible == false && errormsg2.Visible == false &&
                        errormsg3.Visible == false && errormsg4.Visible == false)

                    {
                        using (SqlCommand cmd = new SqlCommand("Delete_Teacher"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@teacherId", teacherId);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }

                        selectedButtonTeacherUpdate(); // Checks and update the gridview after deleting a teacher, Jo 3/10/18 19:18
                        //SqlDataSource1.SelectCommand = "exec View_All_Teachers";  // Updates the gridview after deleting a teacher, Erik 3/8/18 13:10
                        GridView1.DataBind();
                    }

                    break;
                #endregion

                #region DeleteStudent
                case "value2":
                    int studentId = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[2].Text);

                    if (errormsg1.Visible == false && errormsg2.Visible == false &&
                        errormsg3.Visible == false && errormsg4.Visible == false)

                    {
                        using (SqlCommand cmd = new SqlCommand("Delete_Student"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@studentId", studentId);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }
                        selectedButtonStudentUpdate(); // Checks and update the gridview after deleting a student, Jo 3/10/18 19:18
                        //SqlDataSource1.SelectCommand = "exec View_All_Students";  // Updates the gridview after deleting a student, Erik 3/8/18 13:10
                        GridView1.DataBind();
                    }

                    break;
                #endregion

                #region DeleteCourse
                case "value3":

                    string courseNumber = GridView1.Rows[e.RowIndex].Cells[2].Text;

                    if (errormsg1.Visible == false && errormsg2.Visible == false &&
                        errormsg3.Visible == false && errormsg4.Visible == false)

                    {
                        using (SqlCommand cmd = new SqlCommand("Delete_Course"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@courseNum", courseNumber);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }

                        SqlDataSource1.SelectCommand = "exec View_All_Courses";  // Updates the gridview after deleting a course, Erik 3/8/18 13:10
                        GridView1.DataBind();
                    }

                    break;
                #endregion

                #region DeleteSection
                case "value4":

                    string sectionId = GridView1.Rows[e.RowIndex].Cells[2].Text;

                    if (errormsg1.Visible == false && errormsg2.Visible == false &&
                        errormsg3.Visible == false && errormsg4.Visible == false)

                    {
                        using (SqlCommand cmd = new SqlCommand("Delete_Section"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@sectionId", sectionId);

                            cmd.Connection = DBconnection;

                            try
                            {
                                DBconnection.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                DBconnection.Close();
                            }
                        }

                        SqlDataSource1.SelectCommand = "exec View_All_Sections";  // Updates the gridview after deleting a section, Erik 3/8/18 13:10
                        GridView1.DataBind();
                    }

                    break;
                #endregion

                default:

                    break;

            }
        }
    }

    protected void showActive_Click(object sender, EventArgs e)
    {
        string h_value = Request.QueryString["field1"];

        if (h_value == "value1")
        {
            SqlDataSource1.SelectCommand = "exec Select_Active_Teachers";
            GridView1.DataBind();
        }
        else if (h_value == "value2")
        {
            SqlDataSource1.SelectCommand = "exec Select_Active_Students";
            GridView1.DataBind();
        }
        button = 1; // Button Value. Jo, 3/10/2018 18:53
    }

    protected void showInactive_Click(object sender, EventArgs e)
    {
        string h_value = Request.QueryString["field1"];

        if (h_value == "value1")
        {
            SqlDataSource1.SelectCommand = "exec Select_Inactive_Teachers";
            GridView1.DataBind();
        }
        else if (h_value == "value2")
        {
            SqlDataSource1.SelectCommand = "exec Select_Inactive_Students";
            GridView1.DataBind();
        }
        button = 2; // Button Value. Jo, 3/10/2018 18:53
    }

    protected void showAll_Click(object sender, EventArgs e)
    {
        string h_value = Request.QueryString["field1"];

        if (h_value == "value1")
        {
            SqlDataSource1.SelectCommand = "exec View_All_Teachers";
            GridView1.DataBind();
        }
        else if (h_value == "value2")
        {
            SqlDataSource1.SelectCommand = "exec View_All_Students";
            GridView1.DataBind();
        }
        button = 3; // Button Value. Jo, 3/10/2018 18:53
    }

    // Check which button is clicked and called procedure 
    public void selectedButtonTeacherUpdate()
    {
        if (button == 1)
        {
            SqlDataSource1.SelectCommand = "exec Select_Active_Teachers";
            button = 1;
        }
        else if (button == 2)
        {
            SqlDataSource1.SelectCommand = "exec Select_InActive_Teachers";
            button = 2;
        }
        else if (button == 3)
        {
            SqlDataSource1.SelectCommand = "exec View_All_Teachers";
            button = 3;
        }
        else
        {
            SqlDataSource1.SelectCommand = "exec View_All_Teachers";
        }
    }

    public void selectedButtonStudentUpdate()
    {
        if (button == 1)
        {
            SqlDataSource1.SelectCommand = "exec Select_Active_Students";
        }
        else if (button == 2)
        {
            SqlDataSource1.SelectCommand = "exec Select_InActive_Students";
        }
        else if (button == 3)
        {
            SqlDataSource1.SelectCommand = "exec View_All_Students";
        }
        else
        {
            SqlDataSource1.SelectCommand = "exec View_All_Students";
        }
    }
}
