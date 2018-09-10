<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ClassRoster.aspx.cs" Inherits="TestList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:LoginView runat="server" ViewStateMode="Enabled"></asp:LoginView>
   <div class="container">
      <div class="row">
         <div class="card card-nav-tabs">
           <div class="card-header text-white" style="background:#00bcd4;">
             <h1 id="courseName" runat="server"></h1> <!-- Replace with whatever class name was clicked -->
           </div>
           <div class="card-body">
             <h4 class="card-title">Class Roster</h4>
             <div class="row">
                <div class="m-auto">
                    <asp:GridView ID="GridView1" runat="server" Width="800px" GridLines="Horizontal" BorderStyle="solid" DataSourceID="SqlTest" AllowSorting="True" OnRowDataBound="GridView1_RowDataBound">
                    <headerstyle backcolor="#00bcd4" forecolor="white" Font-Bold="true" Font-Size="14pt" Height="40px" Font-Names="calibri" HorizontalAlign="Center"/>
                    <RowStyle HorizontalAlign="Center" Font-Size="12pt"></RowStyle>                    
                    </asp:GridView>
                </div>
             </div>
             
           </div>
         </div>
      </div>
   </div>
   
   <asp:SqlDataSource ID="SqlTest" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="SELECT * FROM [administrator]"></asp:SqlDataSource>

</asp:Content>