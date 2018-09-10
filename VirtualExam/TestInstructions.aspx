<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TestInstructions.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward(0);
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
    <label id="test" runat="server">

    </label>
    <div class="row pt-4">
        <div class="col-md-8 offset-md-2">
            <div class="row instructionContainer">
               <!-- Question Section -->
               <div class="col-md-12">
                  <h2 class="text-center pt-3">Test Instructions</h2>
                  <p class="text-center">
                      The following test is timed. Once you click the continue button the timer will start. The time is displayed in the upper right hand
                      corner. A progress bar is displayed across the top of the screen to let you know how far along you are with the test. Please focus on the
                      test, and do not allow yourself to be distracted. You cannot go back to a previous question so make sure that your answers are final 
                      before clicking next. 
                   </p>
                   <p class="text-center pb-5">
                       Happy Testing!
                  </p>
                  <div class="col-md-12 text-center pt-5">
                    <asp:Button ID="continueButton" runat="server" class="btn btn-info" Text="Continue" OnClick="continue_Click"></asp:Button>
                  </div>
               </div>    
            </div>
         </div>
      </div>

</asp:Content>

