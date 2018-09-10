<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
 
    <!-- Custom Styles -->
    <link href="Content/css/login.css" rel="stylesheet" />

    <div class="card card-nav-tabs login-container col-md-6 offset-md-3"">
      <h4 class="card-header card-header-info">Log In</h4>
      <div class="card-body">
        <div class="row">
            <img src="Scripts/Screen Shot 2018-02-28 at 3.09.00 PM.png" />
        </div>
        <div class="row">
            <div class="col-md-8 offset-md-2 col-centered">
                <div class="form-group">
                    <label for="exampleInput1" class="bmd-label-floating" >Username</label>
                    <input type="text" maxlength="6" class="form-control" style="font-size: 18px;" id="username" runat="server"/>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8 offset-md-2 col-centered pb-3">
                <div class="form-group">
                    <label for="exampleInput2" class="bmd-label-floating">Password</label>
                    <input type="password" maxlength="8" class="form-control" id="password" runat="server"/>
                    <label id="errormsg" style="color:red; font-weight:bold;" runat="server" >The username and/or password is incorrect!</label>
                </div>
            </div>
        </div>
        <div class="row pb-3">
            <div class="col-md-6 offset-md-3">
                <asp:Button ID="loginButton" type="submit" class="btn btn-info login-button" runat="server" Text="Login" OnClick="loginButton_Click" />
                <asp:SqlDataSource ID="loginConnection" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" ></asp:SqlDataSource>
            </div>
        </div>
      </div>
    </div>
</asp:Content>