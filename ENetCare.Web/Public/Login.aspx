<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ENetCare.Web.Public.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Login ID = "Login1" runat = "server" DestinationPageUrl="~/Home.aspx"
        DisplayRememberMe="False" OnAuthenticate= "ValidateUser"></asp:Login>
    </div>
    </form>
</body>
</html>
