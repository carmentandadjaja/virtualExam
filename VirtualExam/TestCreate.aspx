<%@ Page Title="Create a Test " Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TestCreate.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
         .form-control, .is-focused .form-control {
            background-image: linear-gradient(to top, #00bcd4 2px, rgba(156, 39, 176, 0) 2px), linear-gradient(to top, #d2d2d2 1px, rgba(210, 210, 210, 0) 1px);
         }
        
         .is-focused [class*='bmd-label']{
             color: #00bcd4;
         }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CustomJavaScript" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 
      <div class="card card-nav-tabs login-container col-md-6 offset-md-3">
          <h4 class="card-header card-header-info">Create Test</h4>
          <div class="card-body">

            <!-- Course Selector -->
            <div class="row">
                <div class="col-md-8 offset-md-2">
                    <div class="form-group bmd-form-group">
                        <label class="label-control">Course</label>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="Get_Course_Name" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:SessionParameter Name="section_id" SessionField="sectionID" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:Repeater DataSourceID="SqlDataSource2" runat="server">
                            <ItemTemplate>
                                <asp:label CssClass="form-control-plaintext" runat="server" Text='<%# Eval("Name") %>' ID="sectionLabel"/>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <!-- Course Selector -->

            <!-- Test Duration -->
            <div class="row">
                <div class="col-md-8 offset-md-2">
                    <div class="form-group">
                      <label for="testName" class="bmd-label-floating">Test Name</label>
                        <asp:TextBox ID="testName" runat="server" CssClass="form-control" CausesValidation="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="testNameValidator" ControlToValidate="testName" ErrorMessage="Enter test name" CssClass="text-danger" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <!-- Test Duration -->

            <!-- Test Duration -->
            <div class="row">
                <div class="col-md-8 offset-md-2">
                    <div class="form-group">
                      <label for="DurationSelect">Duration</label>
                        <select class="form-control" id="durationSelect" runat="server">
                            <option id="fiveMins" value="0.08">5 minutes</option>
                            <option id="tenMins" value="0.17">10 minutes</option>
                            <option id="fifteenMins" value="0.25">15 minutes</option>
                            <option id="oneHour" value="1">1 hour</option>
                            <option id="twoHour" value="2">2 hour</option>
                        </select>
                    </div>
                </div>
            </div>
            <!-- Test Duration -->

            <!-- Dates available -->
            <div class="row">
                <div class="col-md-8 offset-md-2">
                    <div class="form-group">
                        <label class="label-control">Start Date</label>
                        <input type="date" class="form-control" id="startDate" runat="server"/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="startDate" Display="Dynamic" ErrorMessage="Select a start Date" CssClass="text-danger"></asp:RequiredFieldValidator>
                        <asp:CompareValidator runat="server" Type="Date" ControlToValidate="startDate" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>" Operator="GreaterThanEqual" ErrorMessage="Date must be today or greater!" CssClass="text-danger"></asp:CompareValidator>
                    </div>
                    <div class="form-group">
                        <label class="label-control">End Date</label>
                        <input type="date" class="form-control" id="endDate" runat="server"/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="endDate" Display="Dynamic" ErrorMessage="Select an end Date" CssClass="text-danger"></asp:RequiredFieldValidator>
                        <asp:CompareValidator runat="server" ControlToValidate="endDate" Display="Dynamic" ControlToCompare="startDate" Operator="GreaterThan" ErrorMessage="End Date must be greater than start Date!" CssClass="text-danger"></asp:CompareValidator>
                    </div>

                </div>
            </div>
            <!-- Dates available -->

            <!-- Continue/Cancel Button -->
            <div class="row">
                <div class="col-md-8 offset-md-2">
                    <a href="TeacherHome.aspx" class="btn btn-info col-md-5 ml-3">Cancel</a>
                    <asp:Button ID="continueButton" name="continueButton" class="btn btn-info col-md-5 ml-3" runat="server" Text="Continue" OnClick="continueButton_Click"></asp:Button>
                </div>
            </div>
          </div>
      </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
</asp:Content>

