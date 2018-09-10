<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <!-- Custom Styles -->
    <style>
        .nav{
            min-height: 500px;
        }
        .nav-pills:not(.flex-column) .nav-item + .nav-item:not(:first-child) {
            margin-left: 0px;
        }

        .nav-pills .nav-item .nav-link:hover {
            background-color: darkorange;
            color: white;
        }

        .mainContainer{
            background-color: white;
            padding: 30px;
            max-height: 450px;
        }

        @media (max-width:991px)
        {
            .logout {
               position: absolute;
               top: 370px;
               right: 80px;
               width: 150px;
            }
        }

    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
      <div class="row">
         <div class="col-md-12 text-center">
             <h1>Welcome, Admin</h1>
         </div>
          
         <%--<asp:Label ID="label" runat="server" Text="Test!"></asp:Label>--%>
         
         <div class="col-md-8 offset-md-2 mainContainer">
            <div class="row">
                <ul class="nav nav-pills nav-pills-icons nav-pills-warning col-md-12">
                <li class="nav-item col-md-6">
                    <a class="nav-link" href="http://csmain/cs414/team02/virtualexam/virtualexam/admin.aspx?field1=value1" >
                        <i class="fa fa-graduation-cap"></i>
                        Teacher
                    </a>
                </li>
                <li class="nav-item col-md-6">
                    <a class="nav-link" href="http://csmain/cs414/team02/virtualexam/virtualexam/admin.aspx?field1=value2">
                        <i class="fa fa-user"></i>
                        Student
                    </a>
                </li>
                <li class="nav-item col-md-6">
                    <a class="nav-link" href="http://csmain/cs414/team02/virtualexam/virtualexam/admin.aspx?field1=value3" >
                        <i class="fa fa-book"></i>
                        Courses
                    </a>
                </li>
                <li class="nav-item col-md-6">
                    <a class="nav-link" href="http://csmain/cs414/team02/virtualexam/virtualexam/admin.aspx?field1=value4" >
                        <i class="fa fa-bookmark"></i>
                        Section
                    </a>
                </li>
            </ul>
          </div>
         </div>
      </div>
</asp:Content>

