<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TestList.aspx.cs" Inherits="TestList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:LoginView runat="server" ViewStateMode="Enabled"></asp:LoginView>
   <div class="container">
      <div class="row">
         <div class="card card-nav-tabs">
           <div class="card-header" style="background:#00bcd4; color:white;">
             <h1 id="courseNumber" runat="server"></h1> <!-- Replace with whatever class name was clicked -->
           </div>
           <div class="card-body">
             <h4 class="card-title">Tests</h4>
             <div class="row">
                <div class="m-auto">
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" Width="800px" GridLines="Horizontal" BorderStyle="solid" DataSourceID="SqlTest" AllowSorting="True" OnRowDataBound="GridView1_RowDataBound">
                        <headerstyle backcolor="#00bcd4" forecolor="white" Font-Bold="true" Font-Size="14pt" Height="40px" Font-Names="calibri" HorizontalAlign="Center"/>
                        <RowStyle HorizontalAlign="Center" Font-Size="12pt"></RowStyle>
                        <Columns>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlTest" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="SELECT * FROM [administrator]"></asp:SqlDataSource>
                </div>
             </div>
           </div>
         </div>
      </div>
   </div>
</asp:Content>