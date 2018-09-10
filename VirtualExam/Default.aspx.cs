using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;                 // Need these for the database connection.  Erik, 2/23/18 20:31
using System.Configuration;        // Need these for the database connection.  Erik, 2/23/18 20:31
using System.Data.SqlClient;       // Need these for the database connection.  Erik, 2/23/18 20:31
using System.Web.Security;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        errormsg.Visible = false;

        if (Session["userType"] != null)
        {
            Session.Clear();
            Session.Abandon();
        }
    }

    protected void loginButton_Click(object sender, EventArgs e)
    {
        string h_usernameValue = username.Value;
        string h_passwordValue = password.Value;
        char db_userType = 'z';
        bool result = false;
        int number = 0;  // This variable does nothing for us.  It just holds a test value.  Erik, 3/12/18 12:37

        errormsg.Visible = false;
        
        result = Int32.TryParse(h_usernameValue, out number);

        if (!result)
        {
            errormsg.Visible = true;
            username.Value = string.Empty;
        }

        result = Int32.TryParse(h_passwordValue, out number);

        if (!result)
        {
            errormsg.Visible = true;
        }

        if (errormsg.Visible == false)
        {
            string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

            using (SqlConnection DBconnection = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("Get_Hash"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", h_usernameValue);
                    SqlParameter hash = cmd.Parameters.Add("@hashed_password", SqlDbType.VarChar, 1000);
                    hash.Direction = ParameterDirection.Output;
                    cmd.Connection = DBconnection;

                    try
                    {
                        DBconnection.Open();
                        cmd.ExecuteNonQuery();

                        string x = (string)hash.Value;
                        if (x == "Failure")
                        {

                        }
                        else
                        {
                            if (SimpleHash.VerifyHash(h_passwordValue, "SHA256", x))
                            {
                                using (SqlCommand command = new SqlCommand("Get_User_Type"))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@username", h_usernameValue);
                                    SqlParameter userType = command.Parameters.Add("@user_type", SqlDbType.VarChar, 1);
                                    userType.Direction = ParameterDirection.Output;
                                    command.Connection = DBconnection;

                                    try
                                    {
                                        command.ExecuteNonQuery();

                                        db_userType = Convert.ToChar(userType.Value);
                                        Session["userType"] = db_userType;     // This creates a session for the user and allows us to give them access to only certain portions of the website.  Erik, 2/23/18 20:14
                                        Session["userName"] = h_usernameValue;
                                    }
                                    //    using (SqlCommand command = new SqlCommand("Validate_User"))
                                    //{
                                    //    cmd.CommandType = CommandType.StoredProcedure;
                                    //    cmd.Parameters.AddWithValue("@Username", h_usernameValue);
                                    //    cmd.Parameters.AddWithValue("@Password", h_passwordValue);

                                    //    cmd.Connection = DBconnection;

                                    //    try
                                    //    {
                                    //        DBconnection.Open();

                                    //        db_userType = Convert.ToChar(cmd.ExecuteScalar());
                                    //        Session["userType"] = db_userType;     // This creates a session for the user and allows us to give them access to only certain portions of the website.  Erik, 2/23/18 20:14
                                    //        Session["userName"] = h_usernameValue;
                                    //    }
                                    catch (SqlException ex)
                                    {
                                        Response.Redirect("http://csmain/cs414/team02/virtualexam/virtualexam/Default");
                                        string errormsg = "Unable to connect to the database! ";
                                        errormsg += ex.Message;
                                        throw new Exception(errormsg);
                                    }
                                    finally
                                    {
                                    }
                                }
                            }
                        }
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

                    // This sends the user to the correct webpage based on the login credentials that they have entered.  If they entered invalid login
                    // credentials, an error message is displayed as the default case. Erik, 3/1/18 16:26
                    switch (db_userType)
                    {
                        case 'a':
                            Response.Redirect("AdminHome.aspx");
                            username.Value = "a";
                            break;

                        case 't':
                            Response.Redirect("TeacherHome.aspx");
                            username.Value = "t";
                            break;

                        case 's':
                            Response.Redirect("StudentHome.aspx");
                            username.Value = "s";
                            break;

                        default:
                            errormsg.Visible = true;
                            username.Value = "";
                            password.Value = "";
                            Session.Clear();
                            Session.Abandon();
                            break;
                    }
                }
            }
        }
    }
}