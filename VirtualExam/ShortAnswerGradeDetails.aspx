<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShortAnswerGradeDetails.aspx.cs" Inherits="Default2" %>

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
             <h1 class="card-subtitle mb-2 text-muted"><asp:Label ID="courseNumber" ForeColor ="white" runat="server">Short Answer Grading Details</asp:Label></h1> <!-- Replace with whatever class name was clicked -->
         </div>

         <div class ="m-auto">
             <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GridView1" runat="server" autogeneratecolumns="False" GridLines="Horizontal" BorderStyle="Solid" DataSourceID="SqlDataSource1" OnRowUpdating="GridView1_RowUpdating">
                            <Columns>
                                <asp:CommandField ShowEditButton="True" ItemStyle-ForeColor="#0288CF" ControlStyle-Width="80px" ItemStyle-CssClass="pt-0"/>
                                <asp:BoundField DataField="question_id" HeaderText="Question ID" SortExpression="question_id" ReadOnly="true" ControlStyle-Width="100px"/>
                                <asp:TemplateField ShowHeader="true" HeaderText="Question" ControlStyle-Width="200px">
                                    <ItemTemplate>
                                        <div style="width: 100%; height: 150px; overflow: auto;">
                                            <asp:label ID="Label2" runat="server" Text='<%# Bind("question_text") %>' ReadOnly="true"></asp:label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField ShowHeader="true" HeaderText="Student Answer" ControlStyle-Width="200px">
                                    <ItemTemplate>
                                        <div style="width: 100%; height: 150px; overflow: auto;">
                                            <asp:label ID="Label3" runat="server" Text='<%# Bind("answer_chosen") %>' ReadOnly="true"></asp:label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true" HeaderText="Correct Answer" ControlStyle-Width="200px">
                                    <ItemTemplate>
                                        <div style="width: 100%; height: 150px; overflow: auto;">
                                            <asp:label ID="Label4" runat="server" Text='<%# Bind("answer") %>' ReadOnly="true"></asp:label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="question_points" HeaderText="Total Points" SortExpression="question_points" ReadOnly="true" ItemStyle-Width="130px"/>
                                <asp:TemplateField ShowHeader="True" HeaderText="Points Given" ControlStyle-Width="50px" >
                                <EditItemTemplate>
                                    <asp:TextBox ID="possiblePoints" runat="server" MaxLength="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="possiblePoints" Display="Dynamic" ErrorMessage="Enter a point value" CssClass="text-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="possiblePoints" Display="Dynamic" ErrorMessage="Enter only whole numbers" CssClass="text-danger" ValidationExpression="^[0-9]{0,2}$"></asp:RegularExpressionValidator>
                                    <Label ID="errmsg" runat="server" class="text-danger"></Label>
                               </EditItemTemplate>
                               </asp:TemplateField>
                            </Columns>
                        <headerstyle backcolor="#00bcd4" forecolor="white" Font-Bold="true" Font-Size="14pt" Height="40px" Font-Names="calibri" HorizontalAlign="Center"/>
                        <RowStyle HorizontalAlign="Center" Font-Size="12pt"></RowStyle>
                    
                        </asp:GridView>
                    
                        <h3><asp:Label id="errmsg" class="text-danger" runat="server"></asp:Label></h3>
                        <asp:Button ID="publishGradeButton" runat="server" Text="Publish" class="btn btn-info" OnClick="publishGradeButton_Click" />
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" UpdateCommand="select * from administrator" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>">
                        </asp:SqlDataSource>
                       </div>
                  </div>
              </div>
           </div>
             
         </div>
      </div>
   </div>
</asp:Content>
