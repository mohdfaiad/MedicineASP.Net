<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="Send.aspx.cs" Inherits="ENetCare.Web.Send" %>

<%@ Register TagName="PackageBarcodeUserControl" TagPrefix="uc" Src="~/UserControl/PackageBarcode.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sending Package</title>
    <link href="/Public/Styles/enet-design.css" rel="stylesheet" />
    <link href="/Public/Styles/jquery-ui.css" rel="stylesheet"/>s
    <script src="/Public/Scripts/JQuery/jquery-1.11.2.min.js"></script>
    <script src="/Public/Scripts/JQuery/jquery-ui.min.js"></script>
    <script src="/Public/Scripts/JQuery/jquery.ui.datepicker-en-GB.js"></script>
    <script>
        $(function () {
            $(".datepicker").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">

    <h2>Sending Package</h2>
    <asp:Panel ID="pnlMessage" runat="server" CssClass="message" Visible="false">
            <asp:Literal ID="litMessage" runat="server" />
    </asp:Panel>

    

    <asp:Panel ID="pnlErrorMessage" runat="server" Visible="false" CssClass="message error">
        <asp:Literal ID="litErrorMessage" runat="server" />
         <asp:ValidationSummary ID="valSummary" runat="server" 
            ValidationGroup="sendDetails"
            EnableClientScript="false" ShowSummary="true" DisplayMode="BulletList" ForeColor="Red" /> 
    </asp:Panel>
    
    <uc:PackageBarcodeUserControl ID="ucPackageBarcode" runat="server" />

    <table border="0">
        <tr>
            <td>
                <asp:Label ID="lblDestination" runat="server" 
                        AssociatedControlID="ddlDestination" Text="Send to:" />
            </td>
            <td>
                <asp:DropDownList ID="ddlDestination" runat="server" Width="200" TabIndex="2" Enabled="true"
                     AutoPostBack="True" />
                <asp:RequiredFieldValidator ID="valReqDestination" runat="server" 
                    ControlToValidate="ddlDestination" 
                    ValidationGroup="destinationDetails"
                    Display="None" EnableClientScript="false" SetFocusOnError="true"
                    Text="*"
                    ErrorMessage="Please Choose Destination" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSendDate" runat="server" 
                        AssociatedControlID="txtSendDate" Text="Send Date:" />
            </td>
            <td>
                <asp:TextBox ID="txtSendDate" runat="server" text="Click Here" MaxLength="10" Width="90px" 
                    TabIndex="1" CssClass="datepicker" data="{ altField: '#hidSendDate', altFormat: 'yy-mm-dd'}" ReadOnly="true" />
            </td>
        </tr>
    </table>
    <asp:Button ID="btnSave" Width="60" runat="server" CausesValidation="true" Text="Save" OnClick="btnSave_OnClick" />
    <asp:Button ID="btnClose" Width="60" runat="server" CausesValidation="true" Text="Close" OnClick="btnClose_Click"  />
    <asp:Button ID="btnNext" Width="60" runat="server" CausesValidation="true" Text="Next" OnClick="btnNext_OnClick" Visible="false" />
    
    <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false" CssClass="message error">
        <asp:Literal ID="LitSuccessMsg" runat="server" />
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ValidationGroup="sendDetails"
            EnableClientScript="false" ShowSummary="true" DisplayMode="BulletList" ForeColor="Green" /> 
    </asp:Panel>
</asp:Content>
