using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class Enrollment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // This ensures that only users with the correct credentials have access to their respective pages. Erik, 3/1/18 12:54
        if (Convert.ToChar(Session["userType"]) == 's')
        {
            Response.Redirect("StudentHome.aspx");
        }
        else if (Convert.ToChar(Session["userType"]) == 't')
        {
            Response.Redirect("TeacherHome.aspx");
        }
        else if (Session["userType"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        errorsMsg.Visible = false;

        if (!IsPostBack)
        { 
            string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

            using (SqlConnection DBconnection = new SqlConnection(constr))
            {
                // Populate the Courses Dropdown list, Carmen, 3/6/18, 16:40
                using (SqlCommand cmd = new SqlCommand("View_Students_List"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection = DBconnection;

                    try
                    {
                        DBconnection.Open();
                        studentList.DataSource = cmd.ExecuteReader();
                        studentList.DataBind();
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

                using (SqlCommand cmd = new SqlCommand("View_Courses_List"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection = DBconnection;

                    try
                    {
                        DBconnection.Open();
                        sectionList.DataSource = cmd.ExecuteReader();
                        sectionList.DataBind();
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
            }
        }
    }

    protected void addToSection_Click(object sender, EventArgs e)
    {
        errorsMsg.Visible = false;

        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

        using (SqlConnection DBconnection = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Add_Student_To_Section"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@section_id", sectionList.SelectedValue);
                cmd.Parameters.AddWithValue("@student_id", studentList.SelectedValue);

                cmd.Connection = DBconnection;

                try
                {
                    DBconnection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    //string errormsg = sectionList.SelectedValue;
                    //errormsg += ex.Message;
                    //throw new Exception(errormsg);

                    errorsMsg.Text = "" + studentList.SelectedItem + " is already enrolled in " + sectionList.SelectedItem + ".";
                    errorsMsg.Visible = true;
                }
                finally
                {
                    DBconnection.Close();
                }

                SqlDataSource1.SelectCommand = "View_Every_Section";
                GridView1.DataBind();
            }
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

        using (SqlConnection DBconnection = new SqlConnection(constr))
        {
            int sectionID = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[4].Text);
            int studentID = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[5].Text);

            using (SqlCommand cmd = new SqlCommand("Delete_Student_From_Section"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@section_id", sectionID);
                cmd.Parameters.AddWithValue("@student_id", studentID);

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
            SqlDataSource1.SelectCommand = "View_Every_Section";
            GridView1.DataBind();
        }
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