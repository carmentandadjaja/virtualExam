<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Enrollment.aspx.cs" Inherits="Enrollment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CustomJavaScript" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <link href="Content/css/admin.css" rel="stylesheet" />

    <script src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var gridHeader = $('#<%=GridView1.ClientID%>').clone(true);
            $(gridHeader).find("tr:gt(0)").remove();
            $('#<%=GridView1.ClientID%> tr th').each(function (i) {
                $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()+3).toString() + "px");
            });
            $("#GHead").append(gridHeader);
            $('#GHead').css('position', 'absolute');
            $('#GHead').css('top', $('#<%=GridView1.ClientID%>').offset().bottom);
        });
    </script>

    <div class="container card pt-4 pb-4">
        <div class="row">
            <div class ="col-md-2 text-right pt-2">
                <asp:Label runat="server" ID="studentLabel" Text="Student List"></asp:Label>
            </div>
            <asp:DropDownList ID ="studentList" runat="server" CssClass="form-control" DataTextField="Student" DataValueField="student_id" onchange="studentList(this.value)" >
            </asp:DropDownList>
            <script type="text/javascript">
                function studentList(value) {
                    var student = document.getElementById('<%= studentList.ClientID %>');
                    student.value = value;
                }
                </script>
        </div>

        <div class="row">
            <div class ="col-md-2 text-right pt-2">
                <asp:Label ID="sectionLabel" runat="server" Text="Class List"></asp:Label>
            </div>
            <asp:DropDownList ID="sectionList" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="section_id" onchange="sectionList(this.value)">
            </asp:DropDownList>
            <script type="text/javascript">
                function sectionList(value) {
                    var section = document.getElementById('<%= sectionList.ClientID %>');
                    section.value = value;
                }
                </script>
        </div>

        <div class ="row pl-5 pt-3">
             <asp:Button ID="addToSection" runat="server" Text="Add Student" class="btn btn-info login-button" OnClick="addToSection_Click"/>
        </div>
    </div>  

    <asp:Label ID="errorsMsg" runat="server" Text="" style="color:red; font-weight:bold;" ></asp:Label>

    <div id="GHead" style="height:20pt; background-color:#e5e5e5;"></div>
    <div style="width:100%; height:300px; overflow:auto;">
        <asp:GridView ID="GridView1" runat="server" style="margin-left:5px; width:99%" GridLines="Horizontal" BorderStyle="Solid" CommandName="sort" EnableViewState="False" AllowSorting="True" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting">
        <headerstyle backcolor="#00bcd4" forecolor="white" HorizontalAlign="Center" Height="40pt"/>
        <RowStyle HorizontalAlign="Center"></RowStyle>
        <Columns>
            <asp:CommandField ShowDeleteButton="True"  ItemStyle-ForeColor="#0288CF" />
            <asp:BoundField DataField="course_number" HeaderText="Course No." SortExpression="course_number" />
            <asp:BoundField DataField="section_number" HeaderText="Section No." SortExpression="section_number" />
            <asp:BoundField DataField="course_title" HeaderText="Course Title" SortExpression="course_title" />
            <asp:BoundField DataField="section_id" HeaderText="Section ID" SortExpression="section_id" />
            <asp:BoundField DataField="student_id" HeaderText="Student ID" SortExpression="student_id" />
            <asp:BoundField DataField="first_name" HeaderText="First Name" SortExpression="first_name" />
            <asp:BoundField DataField="last_name" HeaderText="Last Name" SortExpression="last_name" />
        </Columns>
        </asp:GridView>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Every_Section" SelectCommandType="StoredProcedure" DeleteCommand="Delete_Student_From_Section" DeleteCommandType="StoredProcedure" >
        <DeleteParameters>
            <asp:Parameter Name="section_id" Type="Int32" />
            <asp:Parameter Name="student_id" Type="Int32" />
        </DeleteParameters>
                                 
    </asp:SqlDataSource>
</asp:Content>