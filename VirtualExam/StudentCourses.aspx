<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudentCourses.aspx.cs" Inherits="Default2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style>
        .btn {
            padding:8px 16px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward(0);
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>

    <div class="container">
      <div class="row">
         <div class="card card-nav-tabs">
           <div class="card-header card-header" style="background:#00bcd4;">
             <h1 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber" ForeColor ="white" runat="server"></asp:Label></h1> <!-- Replace with whatever class name was clicked -->
         </div>

         <div class="card-body">
            <div class="row">
                <div class="m-auto">
                    
                    <asp:GridView ID="GridView1" runat="server" autogeneratecolumns="False" GridLines="Horizontal" BorderStyle="solid" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound" CssClass="margin-left:auto; margin-right:auto;">  <%--auto generate equals false to not show another table. link: https://stackoverflow.com/questions/13013241/change-header-text-of-columns-in-a-gridview. Jo. 3/8/2018--%>
                    <headerstyle backcolor="#00bcd4" forecolor="white" Font-Bold="true" Font-Size="14pt" Height="40px" Font-Names="calibri" HorizontalAlign="Center"/>
                    <RowStyle HorizontalAlign="Center" Font-Size="12pt"></RowStyle>
                    <Columns>
                        <asp:TemplateField ShowHeader="True">
                            <ItemTemplate>
                                <asp:Label ID="test" runat="server" Visible="false" Text='<%# Bind("test_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Test Name" DataField="test_name" sortExpression="test_name" ItemStyle-Width="110"></asp:BoundField>
                        <asp:BoundField HeaderText="First Name" DataField="first_name" sortExpression="first_name" ItemStyle-Width="130"></asp:BoundField>
                        <asp:BoundField HeaderText="Last Name" DataField="last_name" sortExpression="last_name" ItemStyle-Width="130"></asp:BoundField>
                        <asp:BoundField HeaderText="Course No." DataField="course_number" sortExpression="course_number" ItemStyle-Width="110" Visible="false"></asp:BoundField>
                        <asp:BoundField HeaderText="Section No." DataField="section_number" sortExpression="section_number" ItemStyle-Width="110" Visible="false"></asp:BoundField>
                        <asp:BoundField HeaderText="Begin Date" DataField="begin_date" sortExpression="begin_date" ItemStyle-Width="100"></asp:BoundField>
                        <asp:BoundField HeaderText="End Date" DataField="end_date" sortExpression="end_date" ItemStyle-Width="120"></asp:BoundField>
                        <asp:BoundField HeaderText="Test Status" DataField="test_taken" sortExpression="test_taken" ItemStyle-Width="100"></asp:BoundField>
                        <asp:TemplateField ShowHeader="True">
                            <ItemTemplate>
                                <asp:LinkButton ID="takeTest" runat="server" CssClass="btn btn-info" Text="Take Test">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>

                    <asp:GridView ID="GridView2" runat="server" GridLines="Horizontal" BorderStyle="Solid" DataSourceID="SqlDataSource2" >  <%--auto generate equals false to not show another table. link: https://stackoverflow.com/questions/13013241/change-header-text-of-columns-in-a-gridview. Jo. 3/8/2018--%>
                        <headerstyle backcolor="#00bcd4" forecolor="white" Font-Bold="true" Font-Size="14pt" Height="40px" Font-Names="calibri" HorizontalAlign="Center"/>
                        <RowStyle HorizontalAlign="Center" Font-Size="12pt"></RowStyle>
                        <Columns>
                        </Columns>
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
                 </div>
              </div>
           </div>
         </div>
      </div>
   </div>
</asp:Content>

