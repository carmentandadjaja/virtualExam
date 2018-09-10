using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;                   // Need these for the database connection.  Erik, 2/23/18 20:31
using System.Configuration;          // Need these for the database connection.  Erik, 2/23/18 20:31
using System.Data.SqlClient;         // Need these for the database connection.  Erik, 2/23/18 20:31
using System.Drawing;                // Need these for the database connection.  Jo, 3/9/18 14:22

public partial class EssayGradeDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int studentId = 0;
        int testId = 0;
        string[] s;

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

        // request passing string. Jo, 3/14/2018 15:20
        if (Request.QueryString["testId"] != null)
        {
            string test = Request.QueryString["testId"];
            s = test.Split(',');
            studentId = Convert.ToInt32(s[0]);
            testId = Convert.ToInt32(s[1]);
        }
        publishGradeButton.Enabled = false;
        SqlDataSource1.SelectCommand = "exec Grade_Essay_Questions " + testId + ", " + studentId;


        if (GridView1.Rows.Count == 0)
        {
            publishGradeButton.Enabled = true;
            errmsg.Text = "Click Publish to post the grade";
            errmsg.Visible = true;
        }
        //message.Visible = true;
        //message.Text = GridView1.Rows[1].Cells[5].Text;
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        errmsg.Visible = false;

        int testId = 0;      //Uncomment this  Erik 4/10/18 21:15
        int studentId = 0;   //Uncomment this  Erik 4/10/18 21:15
        int questionId = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[1].Text);
        int points = 0;
        string[] s;              //Uncomment this  Erik 4/10/18 21:15
        //TextBox points = GridView1.Rows[e.RowIndex].FindControl("possiblePoints") as TextBox;


        // Jo I believe I got all this working for you but please test it out before uncommenting everything that I say needs to be uncommented.  Erik 4/10/18 21:15
        try
        {
            string pointString = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("possiblePoints")).Text;
            points = Convert.ToInt32(pointString);


            if (points <= Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[4].Text))
            {
                errmsg.Text = string.Empty;
            }
            else
            {
                errmsg.Text = "Points given should not be higher than the maximum of " + GridView1.Rows[e.RowIndex].Cells[4].Text + "!!!";
                errmsg.Visible = true;
            }
        }
        catch (FormatException)
        {
            errmsg.Text = "Please submit a grade that is a positive whole number!!!";
            errmsg.Visible = true;
        }

        //Uncomment all this  Erik 4 / 10 / 18 21:15
        if (errmsg.Visible == false)
        {
            if (Request.QueryString["testId"] != null)
            {
                string test = Request.QueryString["testId"];
                s = test.Split(',');
                studentId = Convert.ToInt32(s[0]);
                testId = Convert.ToInt32(s[1]);
            }

            string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

            using (SqlConnection DBconnection = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Update_Essay_Grade"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@student_id", studentId);
                    cmd.Parameters.AddWithValue("@question_id", questionId);
                    cmd.Parameters.AddWithValue("@question_points", points);
                    cmd.Parameters.AddWithValue("@test_id", testId);

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
            }

            SqlDataSource1.SelectCommand = "exec Grade_Essay_Questions " + testId + ", " + studentId;
            GridView1.DataBind();
            if (GridView1.Rows.Count == 0)
            {
                publishGradeButton.Enabled = true;
                errmsg.Text = "Click Publish to post the grade";
                errmsg.Visible = true;
            }
        }
    }

    protected void publishGradeButton_Click(object sender, EventArgs e)
    {
        int studentId = 0;
        int testId = 0;
        string[] s;

        if (Request.QueryString["testId"] != null)
        {
            string test = Request.QueryString["testId"];
            s = test.Split(',');
            studentId = Convert.ToInt32(s[0]);
            testId = Convert.ToInt32(s[1]);
        }

        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

        using (SqlConnection DBconnection = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Set_Graded"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@test_id", testId);
                cmd.Parameters.AddWithValue("@student_id", studentId);

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
        }
        //SqlDataSource1.SelectCommand = "exec Set_Graded " + testId + ", " + studentId;
        Response.Redirect("EssayGrade.aspx");
    }
}