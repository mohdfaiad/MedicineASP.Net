<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MissingResource.aspx.cs" Inherits="ENetPlay.ErrorPages.MissingResource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            The resource you are trying to access is missing.
        </p>
    </div>
    <asp:Button runat="server" ID="btnClose" CausesValidation="false" Text="Close" OnClick="btnClose_OnClick"/>
    </form>
</body>
</html>
