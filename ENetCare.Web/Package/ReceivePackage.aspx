<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceivePackage.aspx.cs" Inherits="ENetCare.Web.ReceivePackage" MasterPageFile="~/MasterPages/General.Master"%>

<%@ Register TagName="PackageBarcodeUserControl" TagPrefix="uc" Src="~/UserControl/PackageBarcode.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Receive Package</title>
    <link href="/Public/Styles/jquery-ui.css" rel="stylesheet"/>
    <link href="/Public/Styles/enet-design.css" rel="stylesheet" />
    <script src="/Public/Scripts/JQuery/jquery-1.11.2.min.js"></script>
    <script src="/Public/Scripts/JQuery/jquery-ui.min.js"></script>
    <script src="/Public/Scripts/JQuery/jquery.ui.datepicker-en-GB.js"></script>
    <script>
        $(function() {
            $( ".datepicker" ).datepicker();
            });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    
    <h2>Receive Package</h2>
    <asp:Panel ID="pnlMessage" runat="server" CssClass="message" Visible="false">
            <asp:Literal ID="litMessage" runat="server" />
    </asp:Panel>

    <asp:Panel ID="pnlErrorMessage" runat="server" Visible="false" CssClass="message error">
        <asp:Literal ID="litErrorMessage" runat="server" />
         <asp:ValidationSummary ID="valSummary" runat="server" 
            ValidationGroup="receiveDetails"
            EnableClientScript="false" ShowSummary="true" DisplayMode="BulletList" ForeColor="Red" /> 
    </asp:Panel>
    
    <uc:PackageBarcodeUserControl ID="ucPackageBarcode" runat="server" />
    
    <table border="0">
        <tr>
            <td>
                <asp:Label ID="lblReceiveDate" runat="server" 
                        AssociatedControlID="txtReceiveDate" Text="Receive Date:" />
            </td>
            <td>
                <asp:TextBox ID="txtReceiveDate" runat="server" MaxLength="10" Width="90px" TabIndex="1" CssClass="datepicker" data="{ altFormat: 'yy-mm-dd'}" ReadOnly="true" />
            </td>
        </tr>
        
    </table>

    <asp:Button ID="btnSave" runat="server" CssClass="fd-add" ValidationGroup="userDetails" CausesValidation="true" Text="Check In" OnClick="btnSave_OnClick" />
    <asp:Button runat="server" ID="btnClose" CausesValidation="false" Text="Close" OnClick="btnClose_OnClick"/>
</asp:Content>



