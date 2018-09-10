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
    int questionNumber = 0;
    int testid;

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

        if (questionType.SelectedValue == "T/F")
        {
            trueFalseType.Visible = true;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = false;
            essayType.Visible = false;
        }
        else if (questionType.SelectedValue == "MC")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = true;
            shortAnswerType.Visible = false;
            essayType.Visible = false;
        }
        else if (questionType.SelectedValue == "SA")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = true;
            essayType.Visible = false;
        }
        else if (questionType.SelectedValue == "EQ")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = false;
            essayType.Visible = true;
        }

      GridView1.Visible = false;

        // Store Test ID session to a string variable
        testid = Convert.ToInt32(Session["testId"]);
    }

    // Add a question to the current Test, Carmen 03/17/18 14:51
    protected void addButton_Click(object sender, EventArgs e)
    {
        string questionText = string.Empty;
        string answer = string.Empty;
        string optionA = string.Empty;
        string optionB = string.Empty;
        string optionC = string.Empty;
        string optionD = string.Empty;
        string optionE = string.Empty;
        int questionPts = 0;

        if (questionType.SelectedValue == "T/F")
        { 
            questionText = trueFalseText.InnerText;
            int.TryParse(trueFalsePts.Text, out questionPts);

            optionA = trueFalseRadio.Items.FindByText("True").Text;
            optionB = trueFalseRadio.Items.FindByText("False").Text;
            if (trueFalseRadio.Items.FindByText("True").Selected)
            {
                answer = trueFalseRadio.Items.FindByText("True").Value;
            }
            else if (trueFalseRadio.Items.FindByText("False").Selected)
            {
                answer = trueFalseRadio.Items.FindByText("False").Value;
            }
        }
        else if (questionType.SelectedValue == "MC")
        {
            questionText = multipleChoiceText.InnerText;
            optionA = optionAtext.Text;
            optionB = optionBtext.Text;
            optionC = optionCtext.Text;
            optionD = optionDtext.Text;
            optionE = optionEtext.Text;

            int.TryParse(multipleChoicePts.Text, out questionPts);

            if (multipleChoiceRadio.Items.FindByText("A").Selected)
            {
                answer = multipleChoiceRadio.Items.FindByText("A").Value;
            }
            else if (multipleChoiceRadio.Items.FindByText("B").Selected)
            {
                answer = multipleChoiceRadio.Items.FindByText("B").Value;
            }
            else if (multipleChoiceRadio.Items.FindByText("C").Selected)
            {
                answer = multipleChoiceRadio.Items.FindByText("C").Value;
            }
            else if (multipleChoiceRadio.Items.FindByText("D").Selected)
            {
                answer = multipleChoiceRadio.Items.FindByText("D").Value;
            }
            else if (multipleChoiceRadio.Items.FindByText("E").Selected)
            {
                answer = multipleChoiceRadio.Items.FindByText("E").Value;
            }

            multipleChoiceText.InnerText = string.Empty;
            multipleChoicePts.Text = string.Empty;
            optionAtext.Text = string.Empty;
            optionBtext.Text = string.Empty;
            optionCtext.Text = string.Empty;
            optionDtext.Text = string.Empty;
            optionEtext.Text = string.Empty;
            multipleChoiceRadio.Items.FindByText("A").Selected =
            multipleChoiceRadio.Items.FindByText("B").Selected =
            multipleChoiceRadio.Items.FindByText("C").Selected =
            multipleChoiceRadio.Items.FindByText("D").Selected =
            multipleChoiceRadio.Items.FindByText("E").Selected = false;
        }
        else if (questionType.SelectedValue == "SA")
        {
            questionText = shortAnswerText.InnerText;
            answer = shortAnswerAnswer.Value;
            int.TryParse(shortAnswerPts.Text, out questionPts);
        }
        else if (questionType.SelectedValue == "EQ")
        {
            questionText = essayText.InnerText;
            int.TryParse(essayPts.Text, out questionPts);
        }

        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;
        int userName = 0;
        userName = Convert.ToInt32(Session["userName"]);


        //Adds a question to the previously created test, Carmen 3 / 8 / 18, 17:21
        using (SqlConnection DBconnection = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Create_Question"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@testId", testid);
                cmd.Parameters.AddWithValue("@questionType", questionType.SelectedValue);
                cmd.Parameters.AddWithValue("@questionText", questionText);
                cmd.Parameters.AddWithValue("@answer", answer);
                cmd.Parameters.AddWithValue("@optionA", optionA);
                cmd.Parameters.AddWithValue("@optionB", optionB);
                cmd.Parameters.AddWithValue("@optionC", optionC);
                cmd.Parameters.AddWithValue("@optionD", optionD);
                cmd.Parameters.AddWithValue("@optionE", optionE);
                cmd.Parameters.AddWithValue("@questionPoints", questionPts);
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

            // Populate the Courses Dropdown list, Carmen, 3/6/18, 16:40
            using (SqlCommand cmd = new SqlCommand("View_Questions_List"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@test_id", testid);

                cmd.Connection = DBconnection;

                try
                {
                    DBconnection.Open();
                    ddlQuestionList.DataSource = cmd.ExecuteReader();
                    ddlQuestionList.DataTextField = "Question";
                    ddlQuestionList.DataValueField = "question_id";
                    ddlQuestionList.DataBind();
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
        clearForm();
    }

    // Change the question template based on type of question selected, Carmen 3/15/18, 14:00
    protected void questionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (questionType.SelectedValue == "T/F")
        {
            trueFalseType.Visible = true;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = false;
            essayType.Visible = false;
        }
        else if (questionType.SelectedValue == "MC")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = true;
            shortAnswerType.Visible = false;
            essayType.Visible = false;
        }
        else if (questionType.SelectedValue == "SA")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = true;
            essayType.Visible = false;
        }
        else if (questionType.SelectedValue == "EQ")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = false;
            essayType.Visible = true;
        }
        MaintainScrollPositionOnPostBack = true;
    }

    // Pull question details on select
    protected void ddlQuestionList_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataSource1.SelectCommand = "Select * from test_questions where question_id =" + ddlQuestionList.SelectedValue;
        GridView1.DataBind();

        if (GridView1.Rows[0].Cells[2].Text == "T/F")
        {
            trueFalseType.Visible = true;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = false;
            essayType.Visible = false;
            questionType.Items.FindByText("True / False").Selected = true;
            questionType.Items.FindByText("Multiple Choice").Selected = false;
            questionType.Items.FindByText("Short Answer").Selected = false;
            questionType.Items.FindByText("Essay").Selected = false;

            trueFalsePts.Text = GridView1.Rows[0].Cells[10].Text;
            trueFalseText.Value = Server.HtmlDecode(GridView1.Rows[0].Cells[3].Text);
            if (GridView1.Rows[0].Cells[4].Text == "A")
            {
                trueFalseRadio.Items.FindByText("True").Selected = true;
                trueFalseRadio.Items.FindByText("False").Selected = false;
            }
            else
            {
                trueFalseRadio.Items.FindByText("False").Selected = true;
                trueFalseRadio.Items.FindByText("True").Selected = false;
            }
        }
        else if (GridView1.Rows[0].Cells[2].Text == "MC")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = true;
            shortAnswerType.Visible = false;
            essayType.Visible = false;

            questionType.Items.FindByText("True / False").Selected = false;
            questionType.Items.FindByText("Multiple Choice").Selected = true;
            questionType.Items.FindByText("Short Answer").Selected = false;
            questionType.Items.FindByText("Essay").Selected = false;

            multipleChoicePts.Text = GridView1.Rows[0].Cells[10].Text;
            multipleChoiceText.Value = Server.HtmlDecode(GridView1.Rows[0].Cells[3].Text);

            // This checks to see what the right answer is and then turns on and off the answer radio buttons accordingly.  Erik, 3/20/18 20:40
            if (GridView1.Rows[0].Cells[4].Text == "A")
            {
                multipleChoiceRadio.Items.FindByValue("A").Selected = true;
                multipleChoiceRadio.Items.FindByValue("B").Selected = false;
                multipleChoiceRadio.Items.FindByValue("C").Selected = false;
                multipleChoiceRadio.Items.FindByValue("D").Selected = false;
                multipleChoiceRadio.Items.FindByValue("E").Selected = false;
            }
            else if (GridView1.Rows[0].Cells[4].Text == "B")
            {
                multipleChoiceRadio.Items.FindByValue("A").Selected = false;
                multipleChoiceRadio.Items.FindByValue("B").Selected = true;
                multipleChoiceRadio.Items.FindByValue("C").Selected = false;
                multipleChoiceRadio.Items.FindByValue("D").Selected = false;
                multipleChoiceRadio.Items.FindByValue("E").Selected = false;
            }
            else if (GridView1.Rows[0].Cells[4].Text == "C")
            {
                multipleChoiceRadio.Items.FindByValue("A").Selected = false;
                multipleChoiceRadio.Items.FindByValue("B").Selected = false;
                multipleChoiceRadio.Items.FindByValue("C").Selected = true;
                multipleChoiceRadio.Items.FindByValue("D").Selected = false;
                multipleChoiceRadio.Items.FindByValue("E").Selected = false;
            }
            else if (GridView1.Rows[0].Cells[4].Text == "D")
            {
                multipleChoiceRadio.Items.FindByValue("A").Selected = false;
                multipleChoiceRadio.Items.FindByValue("B").Selected = false;
                multipleChoiceRadio.Items.FindByValue("C").Selected = false;
                multipleChoiceRadio.Items.FindByValue("D").Selected = true;
                multipleChoiceRadio.Items.FindByValue("E").Selected = false;
            }
            else if (GridView1.Rows[0].Cells[4].Text == "E")
            {
                multipleChoiceRadio.Items.FindByValue("A").Selected = false;
                multipleChoiceRadio.Items.FindByValue("B").Selected = false;
                multipleChoiceRadio.Items.FindByValue("C").Selected = false;
                multipleChoiceRadio.Items.FindByValue("D").Selected = false;
                multipleChoiceRadio.Items.FindByValue("E").Selected = true;
            }
            optionAtext.Text = Server.HtmlDecode(GridView1.Rows[0].Cells[5].Text);
            optionBtext.Text = Server.HtmlDecode(GridView1.Rows[0].Cells[6].Text);
            optionCtext.Text = Server.HtmlDecode(GridView1.Rows[0].Cells[7].Text);
            optionDtext.Text = Server.HtmlDecode(GridView1.Rows[0].Cells[8].Text);
            optionEtext.Text = Server.HtmlDecode(GridView1.Rows[0].Cells[9].Text);

        }
        else if (GridView1.Rows[0].Cells[2].Text == "SA")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = true;
            essayType.Visible = false;

            questionType.Items.FindByText("True / False").Selected = false;
            questionType.Items.FindByText("Multiple Choice").Selected = false;
            questionType.Items.FindByText("Short Answer").Selected = true;
            questionType.Items.FindByText("Essay").Selected = false;

            shortAnswerPts.Text = GridView1.Rows[0].Cells[10].Text;
            shortAnswerText.Value = Server.HtmlDecode(GridView1.Rows[0].Cells[3].Text);
            shortAnswerAnswer.Value = Server.HtmlDecode(GridView1.Rows[0].Cells[4].Text);
        }
        else if (GridView1.Rows[0].Cells[2].Text == "EQ")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = false;
            essayType.Visible = true;

            questionType.Items.FindByText("True / False").Selected = false;
            questionType.Items.FindByText("Multiple Choice").Selected = false;
            questionType.Items.FindByText("Short Answer").Selected = false;
            questionType.Items.FindByText("Essay").Selected = true;

            essayPts.Text = GridView1.Rows[0].Cells[10].Text;
            essayText.Value = Server.HtmlDecode(GridView1.Rows[0].Cells[3].Text);
        }
    }

    //Complete test creation and return to the teacher homepage, Carmen, 03/17 15:50
    protected void confirmComplete_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

        using (SqlConnection DBconnection = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Complete_Test"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@test_id", testid);
                cmd.Connection = DBconnection;

                try
                {
                    if (ddlQuestionList.Items.Count <= 0)
                    {
                        testEmptyError.InnerText = "Please add at least one question!";
                    }
                    else if (ddlQuestionList.SelectedItem.Text == "No Questions in Test")
                    {
                        testEmptyError.InnerText = "Please add at least one question!";
                    }
                    else
                    {
                        DBconnection.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("TeacherHome.aspx");
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
            }
        }
    }

    protected void clearAll_Click(object sender, EventArgs e)
    {
        if (questionType.SelectedValue == "T/F")
        {
            trueFalsePts.Text = string.Empty;
            trueFalseText.Value = string.Empty;
            trueFalseRadio.Items.FindByText("True").Selected = false;
            trueFalseRadio.Items.FindByText("False").Selected = false;
        }
        else if (questionType.SelectedValue == "MC")
        {
            multipleChoicePts.Text = string.Empty;
            multipleChoiceText.Value = string.Empty;
            multipleChoiceRadio.Items.FindByText("A").Selected =
            multipleChoiceRadio.Items.FindByText("B").Selected =
            multipleChoiceRadio.Items.FindByText("C").Selected =
            multipleChoiceRadio.Items.FindByText("D").Selected =
            multipleChoiceRadio.Items.FindByText("E").Selected = false;

            optionAtext.Text = optionBtext.Text = optionCtext.Text
            = optionDtext.Text = optionEtext.Text = string.Empty;
        }
        else if (questionType.SelectedValue == "SA")
        {
            shortAnswerPts.Text = string.Empty;
            shortAnswerText.Value = string.Empty;
            shortAnswerAnswer.Value = string.Empty;
        }
        else if (questionType.SelectedValue == "EQ")
        {
            essayPts.Text = string.Empty;
            essayText.InnerText = string.Empty;
        }
    }

    // Updates currently selected question, Carmen 3/24, 14:10
    protected void saveButton_Click(object sender, EventArgs e)
    {
        string questionText = string.Empty;
        string answer = string.Empty;
        string optionA = string.Empty;
        string optionB = string.Empty;
        string optionC = string.Empty;
        string optionD = string.Empty;
        string optionE = string.Empty;
        int questionPts = 0;

        if (questionType.SelectedValue == "T/F")
        {
            questionText = trueFalseText.InnerText;
            int.TryParse(trueFalsePts.Text, out questionPts);
            optionA = trueFalseRadio.Items.FindByText("True").Text;
            optionB = trueFalseRadio.Items.FindByText("False").Text;
            if (trueFalseRadio.Items.FindByText("True").Selected)
            {
                answer = trueFalseRadio.Items.FindByText("True").Value;
            }
            else if (trueFalseRadio.Items.FindByText("False").Selected)
            {
                answer = trueFalseRadio.Items.FindByText("False").Value;
            }

        }
        else if (questionType.SelectedValue == "MC")
        {
            questionText = multipleChoiceText.InnerText;
            optionA = optionAtext.Text;
            optionB = optionBtext.Text;
            optionC = optionCtext.Text;
            optionD = optionDtext.Text;
            optionE = optionEtext.Text;

            int.TryParse(multipleChoicePts.Text, out questionPts);

            if (multipleChoiceRadio.Items.FindByText("A").Selected)
            {
                answer = multipleChoiceRadio.Items.FindByText("A").Value;
            }
            else if (multipleChoiceRadio.Items.FindByText("B").Selected)
            {
                answer = multipleChoiceRadio.Items.FindByText("B").Value;
            }
            else if (multipleChoiceRadio.Items.FindByText("C").Selected)
            {
                answer = multipleChoiceRadio.Items.FindByText("C").Value;
            }
            else if (multipleChoiceRadio.Items.FindByText("D").Selected)
            {
                answer = multipleChoiceRadio.Items.FindByText("D").Value;
            }
            else if (multipleChoiceRadio.Items.FindByText("E").Selected)
            {
                answer = multipleChoiceRadio.Items.FindByText("E").Value;
            }

            multipleChoiceText.InnerText = string.Empty;
            multipleChoicePts.Text = string.Empty;
            optionAtext.Text = string.Empty;
            optionBtext.Text = string.Empty;
            optionCtext.Text = string.Empty;
            optionDtext.Text = string.Empty;
            optionEtext.Text = string.Empty;
            multipleChoiceRadio.Items.FindByText("A").Selected =
            multipleChoiceRadio.Items.FindByText("B").Selected =
            multipleChoiceRadio.Items.FindByText("C").Selected =
            multipleChoiceRadio.Items.FindByText("D").Selected =
            multipleChoiceRadio.Items.FindByText("E").Selected = false;


        }
        else if (questionType.SelectedValue == "SA")
        {
            questionText = shortAnswerText.InnerText;
            answer = shortAnswerAnswer.Value;
            int.TryParse(shortAnswerPts.Text, out questionPts);
        }
        else if (questionType.SelectedValue == "EQ")
        {
            questionText = essayText.InnerText;
            int.TryParse(essayPts.Text, out questionPts);
        }

        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;
        int userName = 0;
        userName = Convert.ToInt32(Session["userName"]);

        using (SqlConnection DBconnection = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Create_Question"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@questionId", ddlQuestionList.SelectedValue);
                cmd.Parameters.AddWithValue("@testId", testid);
                cmd.Parameters.AddWithValue("@questionType", questionType.SelectedValue);
                cmd.Parameters.AddWithValue("@questionText", questionText);
                cmd.Parameters.AddWithValue("@answer", answer);
                cmd.Parameters.AddWithValue("@optionA", optionA);
                cmd.Parameters.AddWithValue("@optionB", optionB);
                cmd.Parameters.AddWithValue("@optionC", optionC);
                cmd.Parameters.AddWithValue("@optionD", optionD);
                cmd.Parameters.AddWithValue("@optionE", optionE);
                cmd.Parameters.AddWithValue("@questionPoints", questionPts);
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

            using (SqlCommand cmd = new SqlCommand("View_Questions_List"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@test_id", testid);
                cmd.Connection = DBconnection;

                try
                {
                    DBconnection.Open();
                    ddlQuestionList.DataSource = cmd.ExecuteReader();
                    ddlQuestionList.DataTextField = "Question";
                    ddlQuestionList.DataValueField = "question_id";
                    ddlQuestionList.DataBind();
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
        clearForm();
    }

    protected void removeButton_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

        using (SqlConnection DBconnection = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Delete_Question"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@question_id", ddlQuestionList.SelectedValue);
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

            using (SqlCommand cmd = new SqlCommand("View_Questions_List"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@test_id", testid);
                cmd.Connection = DBconnection;

                try
                {
                    DBconnection.Open();
                    ddlQuestionList.DataSource = cmd.ExecuteReader();
                    ddlQuestionList.DataTextField  = "Question";
                    ddlQuestionList.DataValueField = "question_id";
                    ddlQuestionList.DataBind();
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
            clearForm();
        }
    }

    protected void clearForm()
    {
        testEmptyError.InnerText = string.Empty;

        if (questionType.SelectedValue == "T/F")
        {
            trueFalsePts.Text = string.Empty;
            trueFalseText.Value = string.Empty;
            trueFalseRadio.Items.FindByText("True").Selected = false;
            trueFalseRadio.Items.FindByText("False").Selected = false;
        }
        else if (questionType.SelectedValue == "MC")
        {
            multipleChoicePts.Text = string.Empty;
            multipleChoiceText.Value = string.Empty;
            multipleChoiceRadio.Items.FindByText("A").Selected =
            multipleChoiceRadio.Items.FindByText("B").Selected =
            multipleChoiceRadio.Items.FindByText("C").Selected =
            multipleChoiceRadio.Items.FindByText("D").Selected =
            multipleChoiceRadio.Items.FindByText("E").Selected = false;

            optionAtext.Text = optionBtext.Text = optionCtext.Text
            = optionDtext.Text = optionEtext.Text = string.Empty;
        }
        else if (questionType.SelectedValue == "SA")
        {
            shortAnswerPts.Text = string.Empty;
            shortAnswerText.Value = string.Empty;
            shortAnswerAnswer.Value = string.Empty;
        }
        else if (questionType.SelectedValue == "EQ")
        {
            essayPts.Text = string.Empty;
            essayText.InnerText = string.Empty;
        }
    }
}