<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudentGrades.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   <link href="Content/css/studentGrades.css" rel="stylesheet" />
  
          <asp:SqlDataSource ID="SqlDataSource0" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
          <asp:GridView ID="GridView0" runat="server" DataSourceID="SqlDataSource0"> </asp:GridView>
      <div class="container">
          <div class="row">
            <h1>Class Grades</h1>
             <div class="col-md-8 offset-md-2" id="noClassGrade" runat="server">
                <div class="card">
                    <div class="card-body">
                    No Grades Available
                    </div>
                </div>
             </div>
        <asp:Repeater ID="repeater1" runat="server" DataSourceID="SqlDataSource1" >
            <ItemTemplate>
                <div id="classNumber" class="col-md-12 m-2 text-center bg-light" runat="server">
                    <div class="classInfo pl-3">
                        <h2 id="courseNumber" class="text-info"><%# Eval("course_number") + " - " + Eval("section_number")%></h2>
                        <p id="courseTitle" runat="server"><%# Eval("course_title") %></p>
                        <asp:HiddenField ID="sectionId" runat="server" Value=<%# Eval("section_id") %>/>
                        <div class="teacherInfo">
                            <h4>Instructor: <%# Eval("first_name") + " " + Eval("last_name")%></h4>
                        </div>
                    </div>

                    <div class="col-md-8 offset-md-2">
                        <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource2" GridLines="Horizontal" BorderStyle="Solid" AutoGenerateColumns="False" CssClass="table"> 
                            <Columns>
                                <asp:BoundField DataField="Test Name" HeaderText="Test Name" SortExpression="Test Name" />
                                <asp:BoundField DataField="Grade" HeaderText="Grade" ReadOnly="True" SortExpression="Grade" />
                            </Columns>
                            <headerstyle backcolor="#00bcd4" forecolor="white" HorizontalAlign="Center" Height="40pt"/>
                            <RowStyle HorizontalAlign="Center"></RowStyle>
                        </asp:GridView>
                    </div>

                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Student_Test_Score" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                        <asp:ControlParameter
                            Name="section_id"
                            Type="Int32"
                            ControlID="sectionId"
                            PropertyName="Value"
                        />
                            <asp:SessionParameter Name="student_id" SessionField="username" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Student_Class_Average" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                        <asp:ControlParameter
                            Name="section_id"
                            Type="Int32"
                            ControlID="sectionId"
                            PropertyName="Value"
                        />
                            <asp:SessionParameter Name="student_id" SessionField="username" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" Visible="false"> </asp:GridView>
                    
                    <asp:Repeater ID="repeater2" runat="server" DataSourceID="SqlDataSource3" OnItemDataBound="repeater1_ItemDataBound">
                        <ItemTemplate>
                            <asp:Label ID="average" runat="server" Text=<%# Eval("Average") %> visible="false"></asp:Label>
                            <h3 class="pl-4">Current Grade : <asp:Label ID="courseAverage1" runat="server"></asp:Label></h3>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        </div>

        <!-- This might be replaced by a repeater from db -->
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Student_Courses" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="username" SessionField="username" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        
         

        <%--  <!-- first class grade -->
         <div id="classOne" runat="server" class="col-md-12 m-2 bg-light">
            <div class="classInfo pl-4">
               <h3 class="text-info"><asp:Label ID="courseNoOne" runat="server" CssClass="pr-3"></asp:Label><asp:Label ID="courseTitleOne" runat="server"></asp:Label></h3>
                <div class="teacherInfo">
                  <h4>Instructor: <asp:Label ID="teacherNameOne" runat="server"></asp:Label></h4>
               </div>
            </div>
            <!-- This might be replaced by a repeater from db -->
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
          
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"> </asp:GridView>
            <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource1"> </asp:GridView>
            <asp:GridView ID="GridView3" runat="server" DataSourceID="SqlDataSource1" GridLines="Horizontal" BorderStyle="solid" AutoGenerateColumns="False"> 
                <headerstyle backcolor="#00bcd4" forecolor="white" HorizontalAlign="Center" Height="40pt"/>
                <RowStyle HorizontalAlign="Center"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Test Name" HeaderText="Test Name" ItemStyle-Width="165px"/>
                    <asp:BoundField DataField="Grade" HeaderText="Grade" ItemStyle-Width="150px"/>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridView4" runat="server" DataSourceID="SqlDataSource2"> </asp:GridView>
            <label id="tempGrade1" runat="server" visible="false"></label>
            <!-- -->
            <h3 class="pl-4">Current Grade : <asp:Label ID="averageCourseOne" runat="server"></asp:Label></h3>
         </div>

        <!-- second class grade -->
         <div id="classTwo" runat="server" class="col-md-12 m-2 bg-light">
            <div class="classInfo pl-4">
               <h3 style="color:#00bcd4;"><asp:Label ID="courseNoTwo" runat="server" CssClass="pr-3"></asp:Label><asp:Label ID="courseTitleTwo" runat="server" Text="Label"></asp:Label></h3>
               <div class="teacherInfo">
                  <h4>Instructor: <asp:Label ID="teacherNameTwo" runat="server"></asp:Label></h4>
               </div>
            </div>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
          
            <asp:GridView ID="GridView5" runat="server" DataSourceID="SqlDataSource3"> </asp:GridView>
            <asp:GridView ID="GridView6" runat="server" DataSourceID="SqlDataSource3"> </asp:GridView>
            <asp:GridView ID="GridView7" runat="server" DataSourceID="SqlDataSource3" GridLines="Horizontal" BorderStyle="solid" AutoGenerateColumns="False"> 
                <headerstyle backcolor="#00bcd4" forecolor="white" HorizontalAlign="Center" Height="40pt"/>
                <RowStyle HorizontalAlign="Center"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Test Name" HeaderText="Test Name" ItemStyle-Width="165px"/>
                    <asp:BoundField DataField="Grade" HeaderText="Grade" ItemStyle-Width="150px"/>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridView8" runat="server" DataSourceID="SqlDataSource4"> </asp:GridView>
            <label id="tempGrade2" runat="server" visible="false"></label>
            <!-- -->
            <h3 class="pl-4">Current Grade : <asp:Label ID="averageCourseTwo" runat="server"></asp:Label></h3>
        </div>

        <!-- third class grade -->
         <div id="classThree" runat="server" class="col-md-12 m-2 bg-light">
            <div class="classInfo pl-4">
               <h3 style="color:#00bcd4;"><asp:Label ID="courseNoThree" runat="server" CssClass="pr-3"></asp:Label><asp:Label ID="courseTitleThree" runat="server" Text="Label"></asp:Label></h3>
               <div class="teacherInfo">
                  <h4>Instructor: <asp:Label ID="teacherNameThree" runat="server"></asp:Label></h4>
               </div>
            </div>
            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
          
            <asp:GridView ID="GridView9" runat="server" DataSourceID="SqlDataSource5"> </asp:GridView>
            <asp:GridView ID="GridView10" runat="server" DataSourceID="SqlDataSource5"> </asp:GridView>
            <asp:GridView ID="GridView11" runat="server" DataSourceID="SqlDataSource5" GridLines="Horizontal" BorderStyle="solid" AutoGenerateColumns="False"> 
                <headerstyle backcolor="#00bcd4" forecolor="white" HorizontalAlign="Center" Height="40pt"/>
                <RowStyle HorizontalAlign="Center"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Test Name" HeaderText="Test Name" ItemStyle-Width="165px"/>
                    <asp:BoundField DataField="Grade" HeaderText="Grade" ItemStyle-Width="150px"/>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridView12" runat="server" DataSourceID="SqlDataSource6"> </asp:GridView>
            <label id="tempGrade3" runat="server" visible="false"></label>
            <!-- -->
            <h3 class="pl-4">Current Grade : <asp:Label ID="averageCourseThree" runat="server"></asp:Label></h3>
        </div>

        
        <!-- fourth class grade -->
         <div id="classFour" runat="server" class="col-md-12 m-2 bg-light">
            <div class="classInfo pl-4">
               <h3 style="color:#00bcd4;"><asp:Label ID="courseNoFour" runat="server" CssClass="pr-3"></asp:Label><asp:Label ID="courseTitleFour" runat="server" Text="Label"></asp:Label></h3>
               <div class="teacherInfo">
                  <h4>Instructor: <asp:Label ID="teacherNameFour" runat="server"></asp:Label></h4>
               </div>
            </div>
            <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
          
            <asp:GridView ID="GridView13" runat="server" DataSourceID="SqlDataSource7"> </asp:GridView>
            <asp:GridView ID="GridView14" runat="server" DataSourceID="SqlDataSource7"> </asp:GridView>
            <asp:GridView ID="GridView15" runat="server" DataSourceID="SqlDataSource7" GridLines="Horizontal" BorderStyle="solid" AutoGenerateColumns="False"> 
                <headerstyle backcolor="#00bcd4" forecolor="white" HorizontalAlign="Center" Height="40pt"/>
                <RowStyle HorizontalAlign="Center"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Test Name" HeaderText="Test Name" ItemStyle-Width="165px"/>
                    <asp:BoundField DataField="Grade" HeaderText="Grade" ItemStyle-Width="150px"/>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridView16" runat="server" DataSourceID="SqlDataSource8"> </asp:GridView>
            <label id="tempGrade4" runat="server" visible="false"></label>
            <!-- -->
            <h3 class="pl-4">Current Grade : <asp:Label ID="averageCourseFour" runat="server"></asp:Label></h3>
        </div>--%>

        </div>
    </div>
</asp:Content>

