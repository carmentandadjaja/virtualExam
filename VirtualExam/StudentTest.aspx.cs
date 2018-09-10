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
    private int questionCounter
    {
        get
        {
            if (Session["questionCounter"] == null)
                return 0;

            return (int)Session["questionCounter"];
        }
        set
        {
            Session["questionCounter"] = value;
        }
    }

    string testId = "";
    string questionType = "";
    string question = "";
    int studentId;                       // needed for database
    int endOfQuestion = 0;
    public static int questionId = 0;    // needed for database

    protected void Page_Load(object sender, EventArgs e)
    {
        testId = Session["testId"].ToString();
        studentId = Convert.ToInt32(Session["userName"]);

        
        if (!IsPostBack)
        {
            if (questionCounter == 0)
            {
                double timeLimit = 0;

                SqlDataSource0.SelectCommand = "exec Get_Test_Time " + testId;

                if (GridView0.Rows[0].Cells[0].Text == "02:00:00")
                {
                    timeLimit = 2;
                }
                else if (GridView0.Rows[0].Cells[0].Text == "01:00:00")
                {
                    timeLimit = 1;
                }
                else if (GridView0.Rows[0].Cells[0].Text == "00:15:00")
                {
                    timeLimit = 0.25;
                }
                else if (GridView0.Rows[0].Cells[0].Text == "00:10:00")
                {
                    timeLimit = 0.167;
                }
                else
                {
                    timeLimit = 0.083;
                }
                Session["Timer"] = DateTime.Now.AddHours(timeLimit).ToString(); // We can change value from database. J0, 3/19/18 11:25
            }
        }

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

        SqlDataSource1.SelectCommand = "exec View_Test_Questions " + testId + ", " + studentId;
        GridView1.DataBind();

        endOfQuestion = GridView1.Rows.Count;
        GridView1.Visible = false;

        // Forcing the user not to back. JO, 3/24/2018
        if (questionCounter <= 0)
        {
            questionCounter = 1;
            //backButton.Enabled = false;
            //backButton.Visible = false;
            questionNumberLabel.Text = questionCounter.ToString();
            questionLabel.Text = GridView1.Rows[questionCounter - 1].Cells[2].Text;
            questionId = Convert.ToInt32(GridView1.Rows[questionCounter - 1].Cells[0].Text);
            questionType = GridView1.Rows[questionCounter - 1].Cells[1].Text;
            checkTemplate();
        }
        else
        {
            //if (questionCounter == 1)
            //{
            //    backButton.Enabled = false;
            //    backButton.Visible = false;
            //}
            //else
            //{
            //    backButton.Enabled = true;
            //    backButton.Visible = true;
            //}
            questionNumberLabel.Text = questionCounter.ToString();
            questionLabel.Text = GridView1.Rows[questionCounter - 1].Cells[2].Text;
            questionId = Convert.ToInt32(GridView1.Rows[questionCounter - 1].Cells[0].Text);
            questionType = GridView1.Rows[questionCounter - 1].Cells[1].Text;
            checkTemplate();
        }
    }

    //protected void backButton_Click(object sender, EventArgs e)
    //{
    //    if (optionA.Checked)
    //    {
    //        answerChosen = optionA.Value;
    //    }
    //    else if (optionB.Checked)
    //    {
    //        answerChosen = optionB.Value;
    //    }
    //    else if (optionC.Checked)
    //    {
    //        answerChosen = optionC.Value;
    //    }
    //    else if (optionD.Checked)
    //    {
    //        answerChosen = optionD.Value;
    //    }
    //    else if (optionE.Checked)
    //    {
    //        answerChosen = optionE.Value;
    //    }

    //    if (optionTrue.Checked)
    //    {
    //        answerChosen = optionTrue.Value;
    //    }
    //    else if (optionFalse.Checked)
    //    {
    //        answerChosen = optionFalse.Value;
    //    }

    //    string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

    //    using (SqlConnection DBconnection = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("Store_Answer"))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@student_id", studentId);
    //            cmd.Parameters.AddWithValue("@question_id", questionId);
    //            cmd.Parameters.AddWithValue("@answer_chosen", answerChosen);
    //            cmd.Connection = DBconnection;

    //            try
    //            {
    //                DBconnection.Open();
    //                cmd.ExecuteNonQuery();
    //            }
    //            catch (System.Data.SqlClient.SqlException ex)
    //            {
    //                string errormsg = "Unable to connect to the database!";
    //                errormsg += ex.Message;
    //                throw new Exception(errormsg);
    //            }
    //            finally
    //            {
    //                DBconnection.Close();
    //            }
    //        }
    //    }

    //    questionCounter--;
    //    if (questionCounter == 1)
    //    {
    //        backButton.Enabled = false;
    //        backButton.Visible = false;
    //        questionNumberLabel.Text = questionCounter.ToString();
    //        questionLabel.Text = GridView1.Rows[questionCounter - 1].Cells[2].Text;
    //        questionType = GridView1.Rows[questionCounter - 1].Cells[1].Text;
    //        checkTemplate();
    //    }
    //    else
    //    {
    //        if (questionCounter < 1)
    //        {
    //            questionCounter = 1;
    //        }
    //        else
    //        {
    //            backButton.Enabled = true;
    //            backButton.Visible = true;
    //            questionNumberLabel.Text = questionCounter.ToString();
    //            questionLabel.Text = GridView1.Rows[questionCounter - 1].Cells[2].Text;
    //            questionType = GridView1.Rows[questionCounter - 1].Cells[1].Text;
    //            checkTemplate();
    //        }
    //    }

    //    // need to check from database 
    //    if (answerChosen == string.Empty)
    //    {
    //        optionA.Checked = true;
    //    }


    //    //GridView2.DataBind();
    //}

    protected void nextButton_Click(object sender, EventArgs e)
    {
        string answerChosen = "";            // needed for database

        if (optionA.Checked)
        {
            answerChosen = optionA.Value;
        }
        else if (optionB.Checked)
        {
            answerChosen = optionB.Value;
        }
        else if (optionC.Checked)
        {
            answerChosen = optionC.Value;
        }
        else if (optionD.Checked)
        {
            answerChosen = optionD.Value;
        }
        else if (optionE.Checked)
        {
            answerChosen = optionE.Value;
        }

        if (optionTrue.Checked)
        {
            answerChosen = optionTrue.Value;
        }
        else if (optionFalse.Checked)
        {
            answerChosen = optionFalse.Value;
        }

        if (shortAnswer.Value != string.Empty)
        {
            answerChosen = shortAnswer.Value;
        }

        if (essayAnswer.Text != string.Empty)
        {
            answerChosen = essayAnswer.Text;
        }

        optionA.Checked = false;
        optionB.Checked = false;
        optionC.Checked = false;
        optionD.Checked = false;
        optionE.Checked = false;
        optionTrue.Checked = false;
        optionFalse.Checked = false;
        shortAnswer.Value = string.Empty;
        essayAnswer.Text = string.Empty;

        string constr = ConfigurationManager.ConnectionStrings["CS414_VirtualExamConnectionString"].ConnectionString;

        using (SqlConnection DBconnection = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Store_Answer"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@question_id", questionId);
                cmd.Parameters.AddWithValue("@answer_chosen", answerChosen);
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

        // progress bar
        int YourPercentage = (int)(questionCounter * 100 / endOfQuestion);

        // Set your minimum/maximum percentages
        ProgressBar.Attributes["aria-valuemin"] = "0";
        ProgressBar.Attributes["aria-valuemax"] = "100";
        // Set the current percentage
        ProgressBar.Attributes["aria-valuenow"] = YourPercentage.ToString(); // Should be between 0-100
        // Set the actual styled width
        ProgressBar.Style["width"] = string.Format("{0}%", YourPercentage);
        progressLabel.Text = YourPercentage.ToString() + "%";

        //// Display your Label on the Progress bar (as a percentage)
        //ProgressBar.Controls.Add(new LiteralControl(String.Format("{0}%", YourPercentage)));

        questionCounter++;
        if (questionCounter <= endOfQuestion)
        {
            //backButton.Enabled = true;
            //backButton.Visible = true;
            questionNumberLabel.Text = questionCounter.ToString();
            questionLabel.Text = GridView1.Rows[questionCounter - 1].Cells[2].Text;
            questionId = Convert.ToInt32(GridView1.Rows[questionCounter - 1].Cells[0].Text);
            questionType = GridView1.Rows[questionCounter - 1].Cells[1].Text;
            checkTemplate();
        }
        else
        {
            questionCounter = 0;
            Response.Redirect("TestFinish.aspx");
        }
    }
    
    // Check question type and display the question template. Jo, 3/17/2018 16:22
    public void checkTemplate()
    {
        // True False
        if (questionType == "T/F")
        {
            trueFalseType.Visible = true;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = false;
            essayType.Visible = false;
        }
        // Multiple Choice
        else if (questionType == "MC")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = true;
            shortAnswerType.Visible = false;
            essayType.Visible = false;

            // Check and display radio button if it is available. Jo, 3/17/2018 16:22
            optionALabel.Text = GridView1.Rows[questionCounter - 1].Cells[3].Text;
            optionBLabel.Text = GridView1.Rows[questionCounter - 1].Cells[4].Text;
            optionCLabel.Text = GridView1.Rows[questionCounter - 1].Cells[5].Text;
            optionDLabel.Text = GridView1.Rows[questionCounter - 1].Cells[6].Text;
            optionELabel.Text = GridView1.Rows[questionCounter - 1].Cells[7].Text;


            //if (optionALabel.Text == string.Empty)
            //{
            //    optionA.Visible = false;
            //}
            //else
            //{
            //    optionA.Visible = true;
            //}

            //if (optionBLabel.Text == string.Empty)
            //{
            //    optionB.Visible = false;
            //}
            //else
            //{
            //    optionB.Visible = true;
            //}

            //if (optionCLabel.Text == string.Empty)
            //{
            //    optionC.Visible = false;
            //}
            //else
            //{
            //    optionC.Visible = true;
            //}

            //if (optionDLabel.Text == string.Empty)
            //{
            //    optionD.Visible = false;
            //}
            //else
            //{
            //    optionD.Visible = true;
            //}

            //if (optionELabel.Text == DBNull.Value.ToString())
            //{
            //    optionELabel.Visible = false;
            //}
            //else
            //{
            //    optionE.Visible = true;
            //}
        }
        // Short Answer
        else if (questionType == "SA")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = true;
            essayType.Visible = false;
        }
        else if (questionType == "EQ")
        {
            trueFalseType.Visible = false;
            multipleChoiceType.Visible = false;
            shortAnswerType.Visible = false;
            essayType.Visible = true;
        }
    }
    
    protected void timerTest_Tick(object sender, EventArgs e)
    {
        if (DateTime.Compare(DateTime.Now, DateTime.Parse(Session["Timer"].ToString())) < 0)
        {
            timeDisplay.Text = "Time Left: " + ((Int32)DateTime.Parse(Session["Timer"].ToString()).Subtract(DateTime.Now).TotalHours).ToString() + " Hours " +
                                               ((Int32)DateTime.Parse(Session["Timer"].ToString()).Subtract(DateTime.Now).TotalMinutes % 60).ToString() + " Minutes " +
                                               (((Int32)DateTime.Parse(Session["Timer"].ToString()).Subtract(DateTime.Now).TotalSeconds) % 60).ToString() + " Seconds";
        }
        else
        {
            questionCounter = 0;
            Response.Redirect("TestFinish.aspx");
        }
    }
}