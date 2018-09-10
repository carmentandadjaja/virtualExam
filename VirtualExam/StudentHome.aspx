<%@ Page Title="Student Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="StudentHome.aspx.cs" Inherits="_Default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
   <style>
      @media (max-width:991px)
        {
            .logout {
               position: absolute;
               top: 125px;
               right: 80px;
               width: 150px;
            }
        }
   </style>
   
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:SqlDataSource ID="userInfoSds" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Student_Info" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="student_id" SessionField="username" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Repeater DataSourceID="userInfoSds" runat="server">
        <ItemTemplate>
            <h3 id="user" runat="server"><%# Eval("Name") %></h3>
        </ItemTemplate>
    </asp:Repeater>
    <div class="row" id="noClass" runat="server">
        <div class="col-md-8 offset-md-2">
            <div class="card">
              <div class="card-body">
                No Classes Available
              </div>
            </div>
        </div>
    </div>

    <div class="row">
        <asp:Repeater ID="repeater1" runat="server" DataSourceID="SqlDataSource1">
            <ItemTemplate>
                <div class="col-md-6" id="class1" runat="server">
                <div class="card text-center">
                <div class="card-body">
                    <h4 class="card-title"><asp:Label ID="courseTitle" runat="server" Text=<%# Eval("course_title") %> ></asp:Label></h4>
                    <h6 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber" runat="server" Text='<%# Eval("course_number") + "-" + Eval("section_number") %>' >></asp:Label></h6>
                    <asp:HiddenField runat="server" ID="sectionId" Value=<%# Eval("section_id") %>/>
                    <asp:Button ID="Button1" class="btn btn-info login-button" runat="server" Text="View Tests" OnClick="View_Test_Click"/>
                    </div>
                </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

        <%--<div class="col-md-6" id="class1" runat="server">
            <div class="card">
              <div class="card-body">
                <h4 class="card-title"><asp:Label ID="courseTitle1" runat="server"></asp:Label></h4>
                <h6 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber1" runat="server"></asp:Label></h6>
                <asp:Button ID="Button1" class="btn btn-info login-button" runat="server" Text="View Test" OnClick="courseNum1_Click" />
              </div>
            </div>
        </div>
        <div class="col-md-6" id="class2" runat="server">
            <div class="card">
               <div class="card-body">
                  <h4 class="card-title"><asp:Label ID="courseTitle2" runat="server"></asp:Label></h4>
                  <h6 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber2" runat="server"></asp:Label></h6>
                <asp:Button ID="Button2" class="btn btn-info login-button" runat="server" Text="View Test" OnClick="courseNum2_Click" />
               </div>
            </div>
        </div>
        <div class="col-md-6" id="class3" runat="server">
            <div class="card">
               <div class="card-body">
                  <h4 class="card-title"><asp:Label ID="courseTitle3" runat="server"></asp:Label></h4>
                  <h6 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber3" runat="server"></asp:Label></h6>
                <asp:Button ID="Button3" class="btn btn-info login-button" runat="server" Text="View Test" OnClick="courseNum3_Click" />
               </div>
            </div>
        </div>
        <div class="col-md-6" id="class4" runat="server">
           <div class="card">
               <div class="card-body">
                  <h4 class="card-title"><asp:Label ID="courseTitle4" runat="server"></asp:Label></h4>
                  <h6 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber4" runat="server"></asp:Label></h6>
                  <asp:Button ID="Button4" class="btn btn-info login-button" runat="server" Text="View Test" OnClick="courseNum4_Click" />
               </div>
            </div>
        </div>
    </div>--%>

    <asp:GridView ID="GridView1" Visible="false" runat="server" AutoGenerateColumns="true"  DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="course_number" HeaderText="course_number" SortExpression="course_number" />
            <asp:BoundField DataField="course_title" HeaderText="course_title" SortExpression="course_title" />
            <asp:BoundField DataField="section_number" HeaderText="section_number" SortExpression="section_number" />
            <asp:BoundField DataField="section_id" HeaderText="section_id" SortExpression="section_id" />
        </Columns>
       
    </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Student_Courses" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="username" SessionField="username" Type="Int32" />
            </SelectParameters>
    </asp:SqlDataSource>
    
</asp:Content>
