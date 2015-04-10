<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenericError.aspx.cs" Inherits="ENetPlay.ErrorPages.GenericError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>An error occurred</h1>
        <p>
            An unknown error occurred, please try again or contact client service.
        </p>
    </div>
    <asp:Button runat="server" ID="btnClose" CausesValidation="false" Text="Close" OnClick="btnClose_OnClick"/>
    </form>
</body>
</html>
