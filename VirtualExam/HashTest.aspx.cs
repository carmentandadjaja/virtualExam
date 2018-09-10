using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class HashTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void submitButton_Click(object sender, EventArgs e)
    {

        string hash = SimpleHash.ComputeHash(password.Text, "SHA256", null);

        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;
        using (SqlConnection DBconnection = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Store_Hash"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username.Text);
                cmd.Parameters.AddWithValue("@hashed_password", hash);

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
        }
    }

    protected void Verify_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;
        using (SqlConnection DBconnection = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Hash"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", testUsername.Text);
                SqlParameter hash = cmd.Parameters.Add("@hashed_password", SqlDbType.VarChar, 1000);
                hash.Direction = ParameterDirection.Output;
                cmd.Connection = DBconnection;
                
                try
                {
                    DBconnection.Open();
                    cmd.ExecuteNonQuery();

                    string x = (string)hash.Value;

                    if (SimpleHash.VerifyHash(testPassword.Text, "SHA256", x))
                    {
                        using (SqlCommand command = new SqlCommand("Get_User_Type"))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@username", testUsername.Text);
                            SqlParameter userType = command.Parameters.Add("@user_type", SqlDbType.VarChar, 5);
                            userType.Direction = ParameterDirection.Output;
                            command.Connection = DBconnection;

                            try
                            {
                                //DBconnection.Open();
                                command.ExecuteNonQuery();

                                string y = (string)userType.Value;
                                Display.Text = y;
                            }
                            catch (SqlException ex)
                            {
                                string errormsg = "Unable to connect to the database!";
                                errormsg += ex.Message;
                                throw new Exception(errormsg);
                            }
                            finally
                            {
                                //DBconnection.Close();
                            }
                        }
                    }
                    else
                        Display.Text = "Failure";
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