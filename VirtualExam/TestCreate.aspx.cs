using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{

    string section_id;
    int test_id;
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

        // Required for comparing start date with Todays Date, Carmen 3/21 22:48
        Page.DataBind();
        
        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;
        int userName = 0;
        userName = Convert.ToInt32(Session["userName"]);

        
        section_id = Session["sectionID"].ToString();
        
    }

    protected void continueButton_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;
        int userName = 0;
        userName = Convert.ToInt32(Session["userName"]);

        string duration = string.Empty;

        if (durationSelect.Value == "2")
        {
            duration = "02:00:00";
        }
        else if (durationSelect.Value == "1")
        {
            duration = "01:00:00";
        }
        else if (durationSelect.Value == "0.25")
        {
            duration = "00:15:00";
        }
        else if (durationSelect.Value == "0.17")
        {
            duration = "00:10:00";
        }
        else if (durationSelect.Value == "0.08")
        {
            duration = "00:05:00";
        }

        //Create a Test instance, Carmen 3 / 8 / 18, 13:30
        using (SqlConnection DBconnection = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Create_Test"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@teacherId", userName);
                cmd.Parameters.AddWithValue("@sectionId", section_id);
                cmd.Parameters.AddWithValue("@testName", testName.Text);
                cmd.Parameters.AddWithValue("@beginDate", startDate.Value);
                cmd.Parameters.AddWithValue("@endDate", endDate.Value);
                cmd.Parameters.AddWithValue("@timeLimit", duration);
                SqlParameter testId = cmd.Parameters.Add("@test_id_output", SqlDbType.Int);
                testId.Direction = ParameterDirection.Output;
                cmd.Connection = DBconnection;

                try
                {
                    DBconnection.Open();
                    cmd.ExecuteNonQuery();
                    Session["testId"] = (int)testId.Value;
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
        Response.Redirect("TestTemplate.aspx");
    }
}