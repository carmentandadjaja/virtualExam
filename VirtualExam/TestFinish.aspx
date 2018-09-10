<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TestFinish.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row pt-4">
        <div class="col-md-8 offset-md-2">
            <div class="row instructionContainer">
               <!-- Question Section -->
               <div class="col-md-12">
                  <h2 class="text-center pt-3">Test Finish</h2>
                  <p class="text-center pb-5">This test will be graded as soon as possible and posted on your grades page.</p>
                </div>
                <div class="m-auto">
                      <p class="m-0">Pledge:</p>  
                      <p class="m-0">I hereby agree that cheating on this exam will result in an academic penalty.</p>
                      <p class="m-0 pb-3">Please sign the pledge if you have not cheated on this exam.</p>
                        <asp:SqlDataSource ID="userInfoSds" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>" SelectCommand="View_Student_Info" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:SessionParameter Name="student_id" SessionField="username" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:Repeater DataSourceID="userInfoSds" runat="server">
                            <ItemTemplate>
                                <span id="user" runat="server" class="m-0 text-info"><%# Eval("Name") %></span>
                            </ItemTemplate>
                        </asp:Repeater>
                       <div class="row form-group p-0">
                         <div class="col-md-6">
                             <label>Signature</label>
                             <label id="warning" class="text-danger pl-2" runat="server"></label>
                             <asp:TextBox id="pledgeSign" runat="server" type="text" class="form-control"/>
                             <span class="bmd-help" style="min-width: 100%;">Please type your name exactly as shown.</span><br />
                         </div>
                      </div>
                         <label style="font-size:12pt;"><asp:Label ID="errormsg" runat="server" class="text-sm-center text-danger" Text=""></asp:Label></label>
                    
                     <div class="col-md-12 text-center">
                         <asp:Button ID="submitButton" runat="server" class="btn btn-info" Text="Submit" OnClick="submit_Click"></asp:Button>  
                     </div>
                   </div>
               </div>    
            </div>
         </div>
    
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CS414_VirtualExamConnectionString %>"></asp:SqlDataSource>     
</asp:Content>

