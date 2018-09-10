<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudentTest.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style>
    #customizeMCQ input[type="radio"] {
        display:none;
    }

    #customizeMCQ label {
        width:60px;
        margin:auto;
        background-color:#EFEFEF;
        border-radius:4px;
        border:1px solid #D0D0D0;
        overflow:auto;  
    }

    #customizeMCQ label span {
        text-align:center;
        font-size: 20px;
        padding:5px 0px;
        display:block;
    }

    #customizeMCQ input:checked + span {
         background: #00bcd4;
         color: white;
    }

    .form-check .form-check-input:checked ~ .circle {
        border-color: #00bcd4;
    }

    .form-check .form-check-label .circle .check {
        background-color: #00bcd4;
    }
    </style>


    <%--  Disable back button. Jo, 3/19/2018--%>
      <script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward(0);
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
     </script>

    <asp:SqlDataSource ID="SqlDataSource0" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
    <asp:GridView ID="GridView0" runat="server" DataSourceID="SqlDataSource0" Visible="false"></asp:GridView>
    <div class="row pt-4">
         <div class="col-md-8 offset-md-2" style="min-height:500px">
            <div class="row questionContainer" >
               <!-- Question Header -->
               <div class="col-md-12" style="min-height:500px">
                  <!-- Time -->
                  <asp:UpdatePanel ID="up" runat="server" style="text-align:right;">
                       <ContentTemplate>
                          <asp:Timer ID ="timerTest" runat="server" Interval="1000" OnTick="timerTest_Tick"></asp:Timer>
                          <asp:Label  ID="timeDisplay" runat="server"></asp:Label>
                       </ContentTemplate>
                       <Triggers>
                           <asp:AsyncPostbackTrigger ControlID="timerTest" EventName="tick" />
                       </Triggers>
                   </asp:UpdatePanel>
                   <!-- Time -->
                    
                <%-- progress bar--%>
                    <div class="progress" style="height:15px;">
                       <asp:Panel ID="ProgressBar"  runat="server" CssClass="progress-bar bg-success">
                           <asp:Label ID="progressLabel" runat="server" ForeColor="White"></asp:Label>
                       </asp:Panel>
                    </div>

                  <!--question number and question-->
                  <div class="col-md-12">
                      <h3>Question #<asp:Label ID="questionNumberLabel" runat="server"></asp:Label></h3>
                      <asp:Label ID="questionLabel" runat="server" class="text-left pt-2"></asp:Label>
                  </div>
                

                   <!-- Question Template (True/False) -->
                   <div id="trueFalseType" runat="server">
                       <div class="col-md-12 pt-3 p-0">
                            <div class="form-check form-check-radio">
                                <label class="form-check-label">
                                    <input type="radio" name="trueFalseRadio" id="optionTrue" value="A" runat="server" class="form-check-input" />True
                                    <span class="circle">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                            <div class="form-check form-check-radio">
                                <label class="form-check-label">
                                    <input type="radio" name="trueFalseRadio" id="optionFalse" value="B" runat="server" class="form-check-input" />False
                                    <span class="circle">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                       </div>
                    </div>

                   <!-- Question Template (Multiple Choice) -->
                   <div id="multipleChoiceType" runat="server">
                       <div id="customizeMCQ">
                            <div class="row pt-4">
                                <div class=" col-md-2 pb-2 ml-3">
                                    <label class="btn-outline-info">
                                        <input ID="optionA" type="radio" GroupName="answer" runat="server" value="A" />
                                        <span>A</span>
                                    </label>
                                </div>
                                <div class=" col-md-8 pb-2 p-0 mt-2">
                                    <asp:Label id="optionALabel" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-2 pb-2 ml-3">
                                    <label class="btn-outline-info">
                                        <input ID="optionB" type="radio" GroupName="answer" runat="server" value="B" />
                                        <span>B</span>
                                    </label>
                                </div>
                                <div class=" col-md-8 pb-2 p-0 mt-2">
                                    <asp:Label id="optionBLabel" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-2 pb-2 ml-3">
                                    <label class="btn-outline-info">
                                        <input ID="optionC" type="radio" GroupName="answer" runat="server" value="C" />
                                        <span>C</span>
                                    </label>
                                </div>
                                <div class=" col-md-8 pb-2 p-0 mt-2">
                                    <asp:Label id="optionCLabel" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-2 pb-2 ml-3">
                                    <label class="btn-outline-info">
                                        <input ID="optionD" type="radio" GroupName="answer" runat="server" value="D" />
                                        <span>D</span>
                                    </label>
                                </div>
                                <div class=" col-md-8 pb-2 p-0 mt-2">
                                    <asp:Label id="optionDLabel" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-2 pb-2 ml-3">
                                    <label class="btn-outline-info">
                                        <input ID="optionE" type="radio" GroupName="answer" runat="server" value="E" />
                                        <span>E</span>
                                    </label>
                                </div>
                                <div class=" col-md-8 pb-2 p-0 mt-2">
                                    <asp:Label id="optionELabel" class="pb-4" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                   </div>

                    <!-- Question Template (Short Answer) -->
                   <div id="shortAnswerType" runat="server">
                       <div class="col-md-12" >
                            <label for="shortAnswer">Answer</label>
                            <input type="text" class="form-control" ID="shortAnswer" placeholder="Write an answer" runat="server"  MaxLength="255" />
                       </div>
                  </div>

                  <!-- Question Template (Essay) -->
                  <div id="essayType" runat="server">
                       <div class="col-md-12">
                            <asp:TextBox id="essayAnswer" TextMode="multiline" Columns="50" Rows="5" runat="server" MaxLength="2500" placeholder="Write an answer"/>
                       </div>
                  </div>

                 <!-- Navigation Button -->
                <div class="col-md-12 text-center">
                    <%--<asp:Button ID="backButton" runat="server" class="btn btn-info" Text="Back" OnClick="backButton_Click"/>--%>
                    <asp:Button ID="nextButton" runat="server" class="btn btn-info" Text="Next" OnClick="nextButton_Click"/>
                </div>
            <!-- Navigation Button -->
            
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
   <%-- <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="select student_id, question_id, test_id, answer_chosen from student_test_link where student_id = 500011 and test_id = 1000000001"></asp:SqlDataSource>
    <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource2" ></asp:GridView>--%>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" ></asp:GridView>
</asp:Content>