<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShortAnswerGrade.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style>
        .btn {
            padding:8px 16px;
        }
    </style>
    
    <div class="container">
      <div class="row">
         <div class="card card-nav-tabs">
           <div class="card-header card-header" style="background:#00bcd4;">
             <h1 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber" ForeColor ="white" runat="server">Short Answer Grading</asp:Label></h1> <!-- Replace with whatever class name was clicked -->
         </div>

        <div class ="m-auto">
             <div class="card-body">
                <asp:GridView ID="GridView1" runat="server" autogeneratecolumns="False" GridLines="Horizontal" BorderStyle="Solid" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound">  <%--auto generate equals false to not show another table. link: https://stackoverflow.com/questions/13013241/change-header-text-of-columns-in-a-gridview. Jo. 3/8/2018--%>
                    <Columns>
                        <asp:BoundField DataField="student_id" HeaderText="Student ID" SortExpression="student_id" ItemStyle-Width="150px"/>
                        <asp:BoundField DataField="first_name" HeaderText="First Name" SortExpression="first_name" ItemStyle-Width="130px" />
                        <asp:BoundField DataField="last_name" HeaderText="Last Name" SortExpression="last_name" ItemStyle-Width="130px"/>
                        <asp:BoundField DataField="test_id" HeaderText="Test ID" SortExpression="test_id" ItemStyle-Width="150px"/>
                        <asp:BoundField DataField="test_name" HeaderText="Test Name" SortExpression="test_name" ItemStyle-Width="130px"/>
                        <asp:TemplateField ShowHeader="True">
                        <ItemTemplate>
                            <asp:LinkButton ID="gradeTest" runat="server" CssClass="btn btn-info" Text="Grade Test">
                            </asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                <headerstyle backcolor="#00bcd4" forecolor="white" Font-Bold="true" Font-Size="14pt" Height="40px" Font-Names="calibri" HorizontalAlign="Center"/>
                <RowStyle HorizontalAlign="Center" Font-Size="12pt"></RowStyle>
                    
                </asp:GridView>
                <h3><asp:Label id="message" CssClass="text-danger" runat="server"></asp:Label></h3>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Students_With_Finished_Tests" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="teacher_id" SessionField="username" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                </div>
             </div>
          </div>
       </div>
    </div>
</asp:Content>
