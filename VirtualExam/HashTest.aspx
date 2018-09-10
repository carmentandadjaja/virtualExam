<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HashTest.aspx.cs" Inherits="HashTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="username" />
        <br />
        <asp:TextBox runat="server" ID="password" />
        <br />
        <asp:Button runat="server" ID="submitButton" Text="Submit" OnClick="submitButton_Click" style="height: 26px" />
        <br /><br /><br />
        <asp:TextBox runat="server" ID="testUsername" />
        <br />
        <asp:TextBox runat="server" ID="testPassword" />
        <br />
        <asp:Button runat="server" ID="Verify" Text="Verify" style="height: 26px" OnClick="Verify_Click" />
        <br />
        <asp:Label runat="server" ID="Display" />
    </div>
    </form>
</body>
</html>
