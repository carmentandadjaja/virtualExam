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
    int testId = 0;
    int studentId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        errormsg.Visible = false;

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
        
        // request passing string. Jo, 3/14/2018 15:20
        testId = Convert.ToInt32(Session["testId"]);
        studentId = Convert.ToInt32(Session["userName"]);
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        string studentSignature = "";

        if (pledgeSign.Text != string.Empty)
        {
            warning.InnerText = string.Empty;
            studentSignature = pledgeSign.Text;
            int db_validSignature = 0;          // This will hold 1 if a student's signature is valid and 0 if it is not valid.

            string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

            using (SqlConnection DBconnection = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Check_Signature"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@student_id", studentId);
                    cmd.Parameters.AddWithValue("@signature", studentSignature);
                    SqlParameter authentication = cmd.Parameters.Add("@authentication", SqlDbType.Int);
                    authentication.Direction = ParameterDirection.Output;
                    cmd.Connection = DBconnection;

                    try
                    {
                        DBconnection.Open();
                        cmd.ExecuteNonQuery();
                        db_validSignature = Convert.ToInt32(authentication.Value);  // Store the value returned by the database. This has to have .value after the variable name in order to work.                   
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

            // If the student's signature is not valid, show them the error message and allow them to edit their signature.
            if (db_validSignature == 0)
            {
                errormsg.Text = "Please type your name with a space in between your first and last name";
                errormsg.Visible = true;
                warning.InnerText = string.Empty;
            }
            else // If the student's signature is valid allow the examination to be graded. 
            {
                using (SqlConnection DBconnection = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("Grade_Test"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@test_id", testId);
                        cmd.Parameters.AddWithValue("@student_id", studentId);
                        cmd.Parameters.AddWithValue("@student_signature", studentSignature);

                        cmd.Connection = DBconnection;

                        try
                        {
                            DBconnection.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (System.Data.SqlClient.SqlException ex)
                        {
                            string errormsg = "Unable to connect to the database! ";
                            errormsg += ex.Message;
                            throw new Exception(errormsg);
                        }
                        finally
                        {
                            DBconnection.Close();
                        }
                    }
                }
                Response.Redirect("StudentHome.aspx"); // Sends the student back to the student home page. 
            }
        }
        else
        {
            warning.InnerText = "Please Sign the Pledge";
        }
    }
}