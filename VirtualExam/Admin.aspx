<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <link href="Content/css/admin.css" rel="stylesheet" />

    <script src="Scripts/jquery-3.3.1.js"></script>
    <script>
        $(document).ready(function () {
            var gridHeader = $('#<%=GridView1.ClientID%>').clone(true);
            $(gridHeader).find("tr:gt(0)").remove();
            $('#<%=GridView1.ClientID%> tr th').each(function (i) {
                $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()+3).toString() + "px");
            });
            $("#GHead").append(gridHeader);
            $('#GHead').css('position', 'absolute');
            $('#GHead').css('top', $('#<%=GridView1.ClientID%>').offset().top);
        });
    </script>
    <div class="container card pt-4 pb-4">
        <div class ="row">            
            <div class="col-md-2 text-right pt-2">
                <asp:Label ID="Label1"  runat="server" Text="First Name"></asp:Label>
            </div>
            <div class="col-md-4">       
                <asp:TextBox ID="textBox1" MaxLength="255" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-5 pt-3 ml-4">       
                    <asp:Label ID="errormsg1" style="color:red; font-weight:bold;" runat="server" ></asp:Label>
            </div>
        </div>
        <div class ="row">
            <div class="col-md-2 text-right pt-2">
                <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label>
            </div>
            <div class="col-md-4">       
                <asp:TextBox ID="textBox2" MaxLength="255" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-5 pt-3 ml-4">       
                    <asp:Label ID="errormsg2" style="color:red; font-weight:bold;" runat="server" ></asp:Label>
            </div>
        </div>
        <div class ="row">
            <div class="col-md-2 text-right pt-2">
                <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>
            </div>
            <div class="col-md-4">       
                <asp:TextBox ID="textBox3" MaxLength="8" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-5 pt-3 ml-4">       
                    <asp:Label ID="errormsg3" style="color:red; font-weight:bold;" runat="server" ></asp:Label>
            </div>
        </div>
        <div class ="row">
            <div class="col-md-2 text-right pt-2">
                <asp:Label ID="Label4" runat="server" Text="Confirm Password"></asp:Label>
            </div>
                <div class="col-md-4 pb-3">       
                <asp:TextBox ID="textBox4" MaxLength="8" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-5 pt-3 ml-4">       
                    <asp:Label ID="errormsg4" style="color:red; font-weight:bold;" runat="server" ></asp:Label>
            </div>
        </div>
        <div class ="row pl-5">
             <asp:Button ID="submitBtn" runat="server" Text="Add Student" class="btn btn-info login-button"  OnClick="submitBtn_Click" />
        </div>
    </div>
    <div style="width: 55%; margin: 0 auto;">
         <asp:Button ID="showActive"   runat="server" Text="Show Active"   class="btn btn-info login-button" width="182px" OnClick="showActive_Click"/>
        <asp:Button ID="showInactive" runat="server" Text="Show Inactive" class="btn btn-info login-button" width="182px" OnClick="showInactive_Click"/>
        <asp:Button ID="showAll"      runat="server" Text="Show All"      class="btn btn-info login-button" width="182px" OnClick="showAll_Click"/>
    </div>
   

    <asp:Label ID="errorsMsg" runat="server" Text="" style="color:red; font-weight:bold;" ></asp:Label>

    <div id="GHead" style="height:20pt; background-color:#e5e5e5;"></div>
    <div style="width:100%; height:300px; overflow:auto;">
        <asp:GridView ID="GridView1" runat="server" style="margin-left:5px; width:99%" GridLines="Horizontal" BorderStyle="solid" CommandName="sort" EnableViewState="False" AllowSorting="True" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting"  AutoGenerateColumns="true">
        <headerstyle backcolor="#00bcd4" forecolor="white" HorizontalAlign="Center" Height="40pt"/>
        <RowStyle HorizontalAlign="Center"></RowStyle>
        <Columns>
            <asp:CommandField ShowEditButton="True" ItemStyle-ForeColor="#0288CF" />
            <asp:CommandField ShowDeleteButton="True" ItemStyle-ForeColor="#0288CF" />
        </Columns>
        </asp:GridView>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="" UpdateCommand="select * from teacher" DeleteCommand="select * from teacher" >
                                 
    </asp:SqlDataSource>
    
</asp:Content>