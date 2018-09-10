<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TeacherGrades.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   <style>
      .table th{
          color: white;
          background: #00bcd4;
      }

      .table td{
          font-weight: 400;
      }

     
   </style>
 
   <div class="container">
      <div class="row">
        <h1>Class Grades</h1>
         <div class="col-md-8 offset-md-2" id="noClass" runat="server">
            <div class="card">
                <div class="card-body">
                No Grades Available
                </div>
            </div>
         </div>
         <asp:Repeater ID="repeater1" runat="server" DataSourceID="SqlDataSource1" OnItemDataBound="repeater1_ItemDataBound">
            <ItemTemplate>
               <div id="class1" class="col-md-12 m-2 text-center bg-light" runat="server">
                  <h2 id="courseNumber1" runat="server" class="text-info"><%# Eval("course_number") + "-" + Eval("section_number") %></h2>
                  <h4 id="courseTitle1" runat="server"><%# Eval("course_title") %></h4>
                  <asp:HiddenField ID="sectionId" runat="server" Value=<%# Eval("section_id") %>/>
                  <asp:Label ID="average" runat="server" Text=<%# Eval("average")%> Visible="false"/>
                  <asp:Label ID="testLabel" runat="server"></asp:Label>
                   <!-- Tests average -->
                  <div class="col-md-8 offset-md-2">
                      <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" GridLines="Horizontal" DataSourceID="SqlDataSource2" DataKeyNames="test_id" CssClass="table">
                          <Columns>
                            <asp:BoundField DataField="test_name" HeaderText="Test Name" ReadOnly="True" SortExpression="test_name" />
                            <asp:BoundField DataField="test_id" Visible="false" HeaderText="Test Id" />
                            <asp:BoundField DataField="Column1" HeaderText="Average" SortExpression="Column1" />
                          </Columns>
                      </asp:GridView>
                  </div>
                  <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Every_Test_Average" SelectCommandType="StoredProcedure">
                     <SelectParameters>
                        <asp:ControlParameter
                            Name="section_id"
                            Type="Int32"
                            ControlID="sectionId"
                            PropertyName="Value"
                        />
                    </SelectParameters>
                  </asp:SqlDataSource>
                  <h3>Class Average : <asp:Label id="courseAverage1" runat="server"></asp:Label></h3>
               </div>
            </ItemTemplate>
         </asp:Repeater>
         
      </div>
      
      <%--<div class="row">
         <h1>Class Grades</h1>
          <div class="col-md-8 offset-md-2" id="noClass" runat="server">
            <div class="card">
                <div class="card-body">
                No Grades Available
                </div>
            </div>
           </div>
         <div id="class1" class="col-md-12 m-2 bg-light" runat="server">
            <h3 id="courseNumber1" runat="server" class="text-primary"></h3>
            <p id="courseTitle1" runat="server"></p>
             <asp:Label runat="server" ID="TestLabel" />
             <asp:GridView ID="courseGridview1" runat="server" AutoGenerateColumns="False" DataKeyNames="test_id" DataSourceID="SqlDataSource3">
                 <Columns>
                     <asp:BoundField DataField="test_name" HeaderText="Test Name" SortExpression="test_name" />
                     <asp:BoundField DataField="test_id" Visible="false" HeaderText="test_id" ReadOnly="True" SortExpression="test_id" />
                     <asp:BoundField DataField="Column1" HeaderText="Average" ReadOnly="True" SortExpression="Column1" />
                 </Columns>
             </asp:GridView>
            <h3>Class Average : <span id="courseAverage1" runat="server"></span></h3>
         </div>
         <div id="class2" class="col-md-12 m-2 bg-light" runat="server">
            <h3 id="courseNumber2" runat="server" class="text-primary"></h3>
            <p id="courseTitle2" runat="server"></p>
             <asp:GridView ID="courseGridview2" runat="server" DataSourceID="SqlDataSource4" AutoGenerateColumns="False" DataKeyNames="test_id">
                 <Columns>
                     <asp:BoundField DataField="test_name" HeaderText="Test Name" SortExpression="test_name" />
                     <asp:BoundField DataField="test_id" Visible="false" HeaderText="test_id" ReadOnly="True" SortExpression="test_id" />
                     <asp:BoundField DataField="Column1" HeaderText="Average" ReadOnly="True" SortExpression="Column1" />
                 </Columns>
             </asp:GridView>
            <h3>Class Average : <span id="courseAverage2" runat="server"></span></h3>
         </div>
         <div id="class3" class="col-md-12 m-2 bg-light" runat="server">
            <h3 id="courseNumber3" runat="server" class="text-primary"></h3>
            <p id="courseTitle3" runat="server"></p>
             <asp:GridView ID="courseGridview3" runat="server" DataSourceID="SqlDataSource5" AutoGenerateColumns="False" DataKeyNames="test_id">
                 <Columns>
                     <asp:BoundField DataField="test_name" HeaderText="Test Name" SortExpression="test_name" />
                     <asp:BoundField DataField="test_id" HeaderText="test_id" Visible="false" ReadOnly="True" SortExpression="test_id" />
                     <asp:BoundField DataField="Column1" HeaderText="Average" ReadOnly="True" SortExpression="Column1" />
                 </Columns>
             </asp:GridView>
            <h3>Class Average : <span id="courseAverage3" runat="server"></span></h3>
         </div>
         <div id="class4" class="col-md-12 m-2 bg-light" runat="server">
            <h3 id="courseNumber4" runat="server" class="text-primary"></h3>
            <p id="courseTitle4" runat="server"></p>
             <asp:GridView ID="courseGridview4" runat="server" DataSourceID="SqlDataSource6" AutoGenerateColumns="False" DataKeyNames="test_id">
                 <Columns>
                     <asp:BoundField DataField="test_name" HeaderText="Test Name" SortExpression="test_name" />
                     <asp:BoundField DataField="test_id" Visible="false" HeaderText="test_id" ReadOnly="True" SortExpression="test_id" />
                     <asp:BoundField DataField="Column1" HeaderText="Average" ReadOnly="True" SortExpression="Column1" />
                 </Columns>
             </asp:GridView>
            <h3>Class Average : <span id="courseAverage4" runat="server"></span></h3>
         </div>
      </div>--%>
   </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Class_Average" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="teacher_id" SessionField="username" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>


    <%--<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Every_Test_Average" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="section_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Every_Test_Average" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="section_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Every_Test_Average" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="section_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Every_Test_Average" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="section_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>--%>

   <%-- <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Visible="false" DataKeyNames="course_number,section_id" DataSourceID="SqlDataSource2">
        <Columns>
            <asp:BoundField DataField="course_number" HeaderText="course_number" ReadOnly="True" SortExpression="course_number" />
            <asp:BoundField DataField="course_title" HeaderText="course_title" SortExpression="course_title" />
            <asp:BoundField DataField="section_number" HeaderText="section_number" SortExpression="section_number" />
            <asp:BoundField DataField="section_id" HeaderText="section_id" ReadOnly="True" SortExpression="section_id" />
        </Columns>
    </asp:GridView>--%>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Visible="false">
        <Columns>
            <asp:BoundField DataField="course_title" HeaderText="course_title" SortExpression="course_title" Visible="false" />
            <asp:BoundField DataField="course_number" HeaderText="course_number" SortExpression="course_number" Visible="false" />
            <asp:BoundField DataField="section_number" HeaderText="section_number" SortExpression="section_number" Visible="false"/>
            <asp:BoundField DataField="section_id" HeaderText="section_id" SortExpression="section_id" Visible="false" />
            <asp:BoundField DataField="Average" HeaderText="Average" ReadOnly="True" SortExpression="Average" Visible="false" />
        </Columns>
    </asp:GridView>
</asp:Content>

