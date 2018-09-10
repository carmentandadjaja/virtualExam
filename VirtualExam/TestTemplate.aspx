<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TestTemplate.aspx.cs" Inherits="Default3" ValidateRequest="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
      #MainContent_questionType input[type="radio"], #MainContent_multipleChoiceRadio input[type="radio"], #MainContent_trueFalseRadio input[type="radio"]{
         display: none;
      }

      #MainContent_questionType li, #MainContent_multipleChoiceRadio li, #MainContent_trueFalseRadio li{
         list-style-type: none;
         display: inline;
      }

      #MainContent_questionType label{
         background: white;
         color: #00bbd3;
         min-width: 150px;
         height: 40px;
         border: 2px solid #00bbd3;
         line-height: 35px;
         text-align: center;
         border-radius: 5px;
      }

      #MainContent_questionType0~label:after{
         content:'\f031';
         font-family: 'FontAwesome';
      }

      #MainContent_questionType_0:checked~label, #MainContent_questionType_1:checked~label, #MainContent_questionType_2:checked~label, #MainContent_questionType_3:checked~label{
         background: #00bbd3;
         color: white;
      }

      #MainContent_questionType_0~label:before{
          content: "\f00c\f00d\00a0";
          font-family: "FontAwesome";
      }

      #MainContent_questionType_1~label:before{
          content: "\f0ca\00a0";
          font-family: "FontAwesome";
      }

      #MainContent_questionType_2~label:before{
          content: "\f040\00a0";
          font-family: "FontAwesome";
      }

      #MainContent_questionType_3~label:before{
          content: "\f044\00a0";
          font-family: "FontAwesome";
      }

      #MainContent_multipleChoiceRadio label, #MainContent_trueFalseRadio label{
          width: 65px;
          height: 27px;
          color: #00bcd4;
          background: #ffffff;
          border: 2px solid #00bcd4;
          padding: 0px;
          margin: 4px;
          line-height: 24px;
          text-align: center;
          border-radius: 5px;
      }

      #MainContent_multipleChoiceRadio_0:checked~label, #MainContent_multipleChoiceRadio_1:checked~label, #MainContent_multipleChoiceRadio_2:checked~label,
      #MainContent_multipleChoiceRadio_3:checked~label, #MainContent_multipleChoiceRadio_4:checked~label, #MainContent_trueFalseRadio_0:checked~label,
      #MainContent_trueFalseRadio_1:checked~label{
         background: #00bcd4;
         color: white;
         border: 2px solid #00bcd4;
      }

    </style>

</asp:Content>

<asp:Content ContentPlaceHolderID="CustomJavaScript" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

<link href="Content/css/testTemplate.css" rel="stylesheet" />

<!-- List of all created questions-->
<div class="card card-nav-pills col-md-8 offset-md-2">
    <div class="col-md-12">
        <label for="QuestionDropDownList">Questions</label>
        <div class="row">
        <asp:DropDownList ID="ddlQuestionList" runat="server" CssClass="col-md-8 m-2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlQuestionList_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="removeButton" type="button" class="btn btn-info col-md-3" runat="server" Text="Remove" CausesValidation="false" OnClick="removeButton_Click" />
        </div>
    </div>
    <p id="testEmptyError" class="text-danger text-center" runat="server"></p>
</div>

<div class="card card-nav-pills col-md-8 offset-md-2 pb-4 cardStyle">
    <div class="col-md-12 p-4">
        <asp:RadioButtonList ID="questionType" RepeatLayout="UnorderedList" runat="server" OnSelectedIndexChanged="questionType_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text ="True / False" Value="T/F" Selected="True"/>
            <asp:ListItem Text ="Multiple Choice" Value="MC" />
            <asp:ListItem Text ="Short Answer" Value="SA" />
            <asp:ListItem Text ="Essay" Value="EQ" />
        </asp:RadioButtonList>
    </div>

    <div class="col-md-10 offset-md-1" id="questionTemplate" runat="server">
         <!-- Multiple Choice Template -->
         <div id="multipleChoiceType" runat="server">   
            <div class="row">
                <div class="col-md-8">
                    <label>Multiple Choice</label>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                      <label class="bmd-label-floating">Points</label>
                        <asp:TextBox type="text" class="form-control" maxlength="1" id="multipleChoicePts" runat="server" CausesValidation="false"/>
                        <asp:RequiredFieldValidator ID="multipleChoicePtsRequiredValidator" runat="server" ControlToValidate="multipleChoicePts" Display="Dynamic" ErrorMessage="Enter a point value" CssClass="text-danger"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="multipleChoicePtsRegexValidator" runat="server" ControlToValidate="multipleChoicePts" Display="Dynamic" ErrorMessage="Invalid point value!" CssClass="text-danger" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-md-12 pb-4">
                    <textarea class="form-control btn-style" rows="4" cols="40" id="multipleChoiceText" runat="server"></textarea>
                    <asp:RequiredFieldValidator runat="server" ID="multipleChoiceTextValidator" ControlToValidate="multipleChoiceText" ErrorMessage="Enter question" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="optionAtext">Option A</label>
                                <asp:TextBox ID="optionAtext" type="text" class="form-control" runat="server" CausesValidation="false"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="optionAText" ErrorMessage="Option required" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                              <label for="optionBtext">Option B</label>
                              <asp:TextBox CssClass="form-control pt-2" ID="optionBtext" runat="server" CausesValidation="false" />
                              <asp:RequiredFieldValidator runat="server" ControlToValidate="optionBText" ErrorMessage="Option required" ValidationGroup="multipleChoiceAnswers" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                              <label for="optionCtext">Option C</label>
                              <asp:TextBox CssClass="form-control pt-2" ID="optionCtext" runat="server" CausesValidation="false"/>
                              <asp:RequiredFieldValidator runat="server" ControlToValidate="optionCText" ErrorMessage="Option required" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                              <label for="optionDtext">Option D</label>
                              <asp:TextBox CssClass="form-control pt-2" ID="optionDtext" runat="server" CausesValidation="false" />
                              <asp:RequiredFieldValidator runat="server" ControlToValidate="optionDText" ErrorMessage="Option required" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                              <label for="optionEtext">Option E</label>
                              <asp:TextBox CssClass="form-control pt-2" ID="optionEtext" runat="server" CausesValidation="false" ValidateRequestMode="Disabled"/>
                              <asp:RequiredFieldValidator runat="server" ControlToValidate="optionEText" ErrorMessage="Option required" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                 </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                   <label class="col-form-label">Select an Answer</label>
                    <asp:RadioButtonList ID="multipleChoiceRadio" runat="server" RepeatLayout="OrderedList" BorderStyle="None" CssClass="p-0">
                        <asp:ListItem Text="A" Value="A"></asp:ListItem>
                        <asp:ListItem Text="B" Value="B"></asp:ListItem>
                        <asp:ListItem Text="C" Value="C"></asp:ListItem>
                        <asp:ListItem Text="D" Value="D"></asp:ListItem>
                        <asp:ListItem Text="E" Value="E"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="multipleChoiceRadio"  ErrorMessage="Select an answer" CssClass="text-danger"></asp:RequiredFieldValidator>
         </div>
        <!-- End Multiple Choice Template -->

        <!-- True/False Template -->
        <div id="trueFalseType" runat="server">
            <div class="row">
                <div class="col-md-8">
                    <label for="trueFalseText">True/ False Question</label>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="bmd-label-floating">Points</label>
                        <asp:TextBox type="text" CssClass="form-control" maxlength="1" id="trueFalsePts" runat="server" CausesValidation="false"/>
                        <asp:RequiredFieldValidator ID="questionPtsRequiredValidator" runat="server" Display="Dynamic" ControlToValidate="trueFalsePts" ErrorMessage="Enter a point value" CssClass="text-danger"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="questionPtsRegexValidator" runat="server" Display="Dynamic" ControlToValidate="trueFalsePts" ErrorMessage="Invalid point value!" ValidationExpression="^[0-9]+$" CssClass="text-danger"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-md-12 pb-4">
                    <textarea class="form-control" rows="4" cols="40" id="trueFalseText" runat="server"></textarea>
                    <asp:RequiredFieldValidator ID="trueFalseTextRequired" runat="server" ControlToValidate="trueFalseText" ErrorMessage="Enter a Question!" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-12">
                    <asp:RadioButtonList ID="trueFalseRadio" runat="server" RepeatLayout="UnorderedList" BorderStyle="None" CssClass="p-0 text-center">
                        <asp:ListItem Text="True" Value="A"></asp:ListItem>
                        <asp:ListItem Text="False" Value="B"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="trueFalseAnswerValidator" runat="server" ControlToValidate="trueFalseRadio" ErrorMessage="Select an Answer" CssClass="text-danger text-center"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <!-- End True/False Template -->

        <!-- Short Answer Template -->
        <div id="shortAnswerType" runat="server">
            <div class="row">
                <div class="col-md-8">
                    <label for="shortAnswerText">Short Answer Question</label>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="bmd-label-floating">Points</label>
                        <asp:TextBox type="text" class="form-control" maxlength="2" id="shortAnswerPts" runat="server" CausesValidation="false"/>
                        <asp:RequiredFieldValidator ID="shortAnswerPtsValidator" runat="server" Display="Dynamic" ControlToValidate="shortAnswerPts" ErrorMessage="Enter number of points" CssClass="text-danger"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="shortAnswerPtsRegexValidator" runat="server" Display="Dynamic" ControlToValidate="shortAnswerPts" ErrorMessage="Invalid point value!" ValidationExpression="^[0-9]+$" CssClass="text-danger"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-md-12 pb-4">
                    <textarea class="form-control btn-style questionText" rows="4" cols="40" id="shortAnswerText" runat="server"></textarea>
                    <asp:RequiredFieldValidator ID="shortAnswerQuestionValidator" runat="server" ControlToValidate="shortAnswerText" ErrorMessage="Enter a question" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>  
                <div class="col-md-12 pb-4">
                    <label for="shortAnswer">Answer</label>
                    <input type="text" class="form-control" id="shortAnswerAnswer" placeholder="Write an answer" runat="server">
                    <asp:RequiredFieldValidator ID="shortAnswerAnswerValidator" runat="server" ControlToValidate="shortAnswerAnswer" CssClass="text-danger" ErrorMessage="Enter an answer"></asp:RequiredFieldValidator>
                </div>
            </div> 
        </div>
        <!-- End Short Answer Template -->

        <!-- Essay Question Template -->
        <div id="essayType" runat="server">
            <div class="row">
                <div class="col-md-8">
                    <label for="essayQuestionText">Essay Question</label>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="bmd-label-floating">Points</label>
                        <asp:TextBox type="text" class="form-control" maxlength="2" id="essayPts" runat="server" CausesValidation="false"/>
                        <asp:RequiredFieldValidator ID="essayPtsValidator" runat="server" Display="Dynamic" ControlToValidate="essayPts" ErrorMessage="Enter number of points" CssClass="text-danger"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="essayPtsRegexValidator" runat="server" Display="Dynamic" ControlToValidate="essayPts" ErrorMessage="Invalid point value!" ValidationExpression="^[0-9]+$" CssClass="text-danger"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-md-12 pb-4">
                    <textarea class="form-control btn-style questionText" rows="4" cols="40" id="essayText" runat="server"></textarea>
                    <asp:RequiredFieldValidator ID="essayTextValidator" runat="server" ControlToValidate="essayText" ErrorMessage="Enter a question" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>  
            </div> 
        </div>
       <!-- End Essay Question Template -->
       
   
       <!-- Action Buttons -->
       <div class="row">
          <div class="col-md-12 text-center">
            <asp:Button ID="saveButton" class="btn btn-info col-md-3" runat="server" Text="Update" OnClick="saveButton_Click"/>
            <asp:Button ID="addButton" type="button" class="btn btn-info col-md-3" runat="server" Text="Add" OnClick="addButton_Click" />
            <asp:Button type="button" ID="clearAll" CssClass="btn btn-info col-md-3" runat="server" Text="Clear Form" OnClick="clearAll_Click" CausesValidation="false"/>
            <button type="button" class="btn btn-info col-md-4 mt-4" data-toggle="modal" data-target="#confirmTestCreate">Finish</button>
          </div>
       </div>
        
        <!-- Action Buttons -->
       
        <!-- Publish Test Confirmation -->
        <div class="modal fade" id="confirmTestCreate" tabindex="-1" role="dialog" aria-labelledby="confirmCreateModal" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="confirmationHeader">Complete Creating Test</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                Finish creating the test and publish?
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                <!-- Call complete test procedure -->
                <asp:Button ID="confirmComplete" type="button" class="btn btn-info" runat="server" Text="Yes" OnClick="confirmComplete_Click" CausesValidation="false"/>
              </div>
            </div>
          </div>
        </div>
    </div>
</div>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1">
    </asp:GridView>
</asp:Content>