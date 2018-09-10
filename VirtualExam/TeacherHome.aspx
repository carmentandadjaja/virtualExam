<%@ Page Title="Teacher Home " Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="TeacherHome.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8 offset-md-2" id="noClass" runat="server">
            <div class="card">
              <div class="card-body">
                No Classes Available
              </div>
            </div>
        </div>
    </div>

        <!-- Repeater template to display courses dynamically, Carmen 4/10/18, 16:55 -->
        <div class="row">
            <asp:Repeater ID="repeater1" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <div class="col-md-6" id="class1" runat="server">
                    <div class="card text-center">
                    <div class="card-body">
                        <h4 class="card-title"><asp:Label ID="courseTitle" runat="server" Text=<%# Eval("course_title") %> ></asp:Label></h4>
                        <h6 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber" runat="server" Text='<%# Eval("course_number") + "-" + Eval("section_number") %>' ></asp:Label></h6>
                        <asp:HiddenField runat="server" ID="sectionId" Value=<%# Eval("section_id") %>/>
                        <asp:Button ID="Button1" class="btn btn-info login-button" runat="server" Text="View Tests" OnClick="courseNum_Click" />
                        <asp:Button ID="Button2" class="btn btn-info login-button" runat="server" Text="Create Test" OnClick="createTest_Click"/>
                        <asp:Button ID="Button3" class="btn btn-info login-button" runat="server" Text="View Class Roster" OnClick="viewClass_Click" />
                      </div>
                    </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
          </div>

      <%--<div class="row">
        <div class="col-md-6" id="class1" runat="server">
            <div class="card">
              <div class="card-body">
                <h4 class="card-title"><asp:Label ID="courseTitle1" runat="server"></asp:Label></h4>
                <h6 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber1" runat="server"></asp:Label></h6>
                <asp:Button ID="viewTests1" class="btn btn-info login-button" runat="server" Text="View Tests" OnClick="courseNum1_Click" />
                <asp:Button ID="createTest1" class="btn btn-info login-button" runat="server" Text="Create Test" OnClick="createTest1_Click"/>
                <asp:Button ID="viewClass1" class="btn btn-info login-button" runat="server" Text="View Class Roster" OnClick="viewClass1_Click" />
              </div>
            </div>
        </div>
        <div class="col-md-6" id="class2" runat="server">
            <div class="card">
               <div class="card-body">
                  <h4 class="card-title"><asp:Label ID="courseTitle2" runat="server"></asp:Label></h4>
                  <h6 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber2" runat="server"></asp:Label></h6>
                <asp:Button ID="viewTests2" class="btn btn-info login-button" runat="server" Text="View Tests" OnClick="courseNum2_Click" />
                <asp:Button ID="createTest2" class="btn btn-info login-button" runat="server" Text="Create Test" OnClick="createTest2_Click" />
                <asp:Button ID="viewClass2" class="btn btn-info login-button" runat="server" Text="View Class Roster" OnClick="viewClass2_Click"/>
               </div>
            </div>
        </div>
        <div class="col-md-6" id="class3" runat="server">
            <div class="card">
               <div class="card-body">
                  <h4 class="card-title"><asp:Label ID="courseTitle3" runat="server"></asp:Label></h4>
                  <h6 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber3" runat="server"></asp:Label></h6>
                <asp:Button ID="viewTests3" class="btn btn-info login-button" runat="server" Text="View Tests" OnClick="courseNum3_Click" />
                <asp:Button ID="createTest3" class="btn btn-info login-button" runat="server" Text="Create Test" OnClick="createTest3_Click" />
                <asp:Button ID="viewClass3" class="btn btn-info login-button" runat="server" Text="View Class Roster" OnClick="viewClass3_Click"/>
               </div>
            </div>
        </div>
        <div class="col-md-6" id="class4" runat="server">
           <div class="card">
               <div class="card-body">
                  <h4 class="card-title"><asp:Label ID="courseTitle4" runat="server"></asp:Label></h4>
                  <h6 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber4" runat="server"></asp:Label></h6>
                  <asp:Button ID="viewTests4" class="btn btn-info login-button" runat="server" Text="View Tests" OnClick="courseNum4_Click" />
                  <asp:Button ID="createTest4" class="btn btn-info login-button" runat="server" Text="Create Test" OnClick="createTest4_Click" />
                  <asp:Button ID="viewClass4" class="btn btn-info login-button" runat="server" Text="View Class Roster" OnClick="viewClass4_Click"/>
               </div>
            </div>
        </div>
    </div>--%>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Visible="false" DataSourceID="SqlDataSource1" DataKeyNames="course_number,section_id">
        <Columns>
            <asp:BoundField DataField="course_number" HeaderText="course_number" ReadOnly="True" SortExpression="course_number" />
            <asp:BoundField DataField="course_title" HeaderText="course_title" SortExpression="course_title" />
            <asp:BoundField DataField="section_number" HeaderText="section_number" SortExpression="section_number" />
            <asp:BoundField DataField="section_id" HeaderText="section_id" ReadOnly="True" SortExpression="section_id" />
        </Columns>
       
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Teacher_Courses" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="username" SessionField="username" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
