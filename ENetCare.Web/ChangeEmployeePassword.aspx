<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="ChangeEmployeePassword.aspx.cs" Inherits="ENetCare.Web.ChangeEmployeePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Change Employee Password"</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <h2>Change Employee Password</h2>
    
    <asp:ChangePassword ID="ChangePassword1" ContinueDestinationPageUrl="~/Home.aspx" CancelDestinationPageUrl="~/Home.aspx" runat="server">
    </asp:ChangePassword>

</asp:Content>
