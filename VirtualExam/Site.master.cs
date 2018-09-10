using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string currentPage = HttpContext.Current.Request.Url.AbsolutePath;

        // Need to Change the content of the navigation bar based on the user type. Carmen, 2/24 16:00 
        if (Convert.ToChar(Session["userType"]) == 's')
        {
            nav.InnerHtml = @"<ul class='navbar-nav'> 
                                <li id='test' class='nav-item'> 
                                    <a class='nav-link active' href='StudentHome.aspx'><i class='fa fa-home'></i>Home</a>
                                </li>
                                <li class='nav-item'>
                                    <a class='nav-link' href='StudentGrades.aspx'><i class='fa fa-check-circle'></i>Grades</a>
                                </li>
                                <li class='nav-item logout'>
                                    <a class='nav-link' href='Default.aspx'><i class='fa fa-sign-out'></i>Log Out</a>
                                </li>
                              </ul>";
        }

        // We need to keep this for hiding the navbar during the test. Erik, 3/21 20:56
        if (currentPage == "/cs414/team02/VirtualExam/VirtualExam/StudentTest" || currentPage == "/cs414/team02/virtualexam/virtualexam/StudentTest")
        {
            nav.Visible = false;
        }
        else if (currentPage == "/cs414/team02/VirtualExam/VirtualExam/TestInstructions" || currentPage == "/cs414/team02/virtualexam/virtualexam/TestInstructions")
        {
            nav.Visible = false;
        }
        else if (currentPage == "/cs414/team02/VirtualExam/VirtualExam/TestFinish" || currentPage == "/cs414/team02/virtualexam/virtualexam/TestFinish")
        {
            nav.Visible = false;
        }
        else if (currentPage == "/cs414/team02/virtualexam/virtualexam/Default" || currentPage == "/cs414/team02/VirtualExam/VirtualExam/Default")
        {
             mainNavbar.Visible = false;
             navbarToggler.Visible = false;
        }
        else
        {
            nav.Visible = true;
        }

        // Need to Change the content of the navigation bar based on the user type. Jo, 2 / 25 13:30
        if (Convert.ToChar(Session["userType"]) == 't')
        {
            nav.InnerHtml = @"<ul class='navbar-nav'>
                                  <li class='nav-item'>
                                    <a class='nav-link' href='TeacherHome.aspx'><i class='fa fa-home'></i>Home</a>
                                  </li>
                                  <li class='nav-item'>
                                    <a class='nav-link' href='TeacherGrades.aspx'><i class='fa fa-check-circle'></i>Grades</a>
                                  </li>
                                  <li class='nav-item'>
                                    <a class='nav-link' href='ShortAnswerGrade.aspx'><i class='fa fa-pencil'></i>Grade Short Answer</a>
                                  </li>
                                  <li class='nav-item'>
                                    <a class='nav-link' href='EssayGrade.aspx'><i class='fa fa-edit'></i>Grade Essay</a>
                                  </li>
                                  <li class='nav-item logout'>
                                    <a class='nav-link' href='Default.aspx'><i class='fa fa-sign-out'></i>Log Out</a>
                                  </li>
                              </ul>";
        }

        if (Convert.ToChar(Session["userType"]) == 'a')
        {
            nav.InnerHtml = @"<ul class='navbar-nav'>
                                  <li class='nav-item'>
                                    <a class='nav-link active' href='AdminHome.aspx'><i class='fa fa-home'></i>Home</a>
                                  </li>
                                  <li class='nav-item'>
                                    <a class='nav-link' href='http://csmain/cs414/team02/virtualexam/virtualexam/admin.aspx?field1=value1'><i class='fa fa-graduation-cap'></i>Teacher</a>
                                  </li>
                                  <li class='nav-item'>
                                    <a class='nav-link' href='http://csmain/cs414/team02/virtualexam/virtualexam/admin.aspx?field1=value2'><i class='fa fa-user'></i>Student</a>
                                  </li>
                                  <li class='nav-item'>
                                    <a class='nav-link' href='http://csmain/cs414/team02/virtualexam/virtualexam/admin.aspx?field1=value3'><i class='fa fa-book'></i>Course</a>
                                  </li>
                                  <li class='nav-item'>
                                    <a class='nav-link' href='http://csmain/cs414/team02/virtualexam/virtualexam/admin.aspx?field1=value4'><i class='fa fa-bookmark'></i>Section</a>
                                  </li>
                                  <li class='nav-item'>
                                    <a class='nav-link' href='http://csmain/cs414/team02/virtualexam/virtualexam/enrollment.aspx'><i class='fa fa-user-plus'></i>Enrollment</a>
                                  </li>
                                  <li class='nav-item logout'>
                                    <a class='nav-link' href='Default.aspx'><i class='fa fa-sign-out'></i>Log Out</a>
                                  </li>
                              </ul>";
        }
    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }
}