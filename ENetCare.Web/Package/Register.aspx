<%@ Page Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ENetCare.Web.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Register Package</title>
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
    
    <h2>Register Package</h2>
    
    <asp:Panel ID="pnlMessage" runat="server" CssClass="message" Visible="false">
            <asp:Literal ID="litMessage" runat="server" />
    </asp:Panel>
        
    <asp:Panel ID="pnlErrorMessage" runat="server" Visible="false" CssClass="message error">
        <asp:Literal ID="litErrorMessage" runat="server" />
    
        <asp:ValidationSummary ID="valSumUserDetails" runat="server" 
            ValidationGroup="userDetails"
            EnableClientScript="false" ShowSummary="true" DisplayMode="BulletList" ForeColor="Red" /> 
    </asp:Panel>
    
    <table border="0">
        <tr>
            <td> <!-- width='100'>  -->
                <asp:Label ID="lblPackageType" runat="server" Text="Package Type:" />
            </td>
            <td>
                <asp:DropDownList ID="ddlPackageType" runat="server" Width="200" TabIndex="2" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlPackageType_SelectedIndexChanged" OnDataBound="ddlPackageType_DataBound" />
                <asp:RequiredFieldValidator ID="valReqPackageType" runat="server" 
                    ControlToValidate="ddlPackageType" 
                    ValidationGroup="userDetails"
                    Display="None" EnableClientScript="false" SetFocusOnError="true"
                    Text="*"
                    ErrorMessage="Please select a package type" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblExpirationDate" runat="server" 
                        AssociatedControlID="txtExpirationDate" Text="Expiration Date:" />
            </td>
            <td>
                <asp:TextBox ID="txtExpirationDate" runat="server" MaxLength="10" Width="90px" TabIndex="1" CssClass="datepicker" data="{ altField: '#hidExpirationDate', altFormat: 'yy-mm-dd'}" ReadOnly="true" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLocation" runat="server" 
                        AssociatedControlID="ddlLocation" Text="Distribution Centre:" />
            </td>
            <td>
                <asp:DropDownList ID="ddlLocation" runat="server" Width="200" TabIndex="2" Enabled="true" />
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="lblBarcode" runat="server" Text="Barcode:" />
            </td>
            <td>
                <asp:Literal ID="litBarcode" runat="server"/>
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
                <asp:Image ID="ImageBarcode" runat="server" />
            </td>
        </tr>
    </table>

    <asp:Button ID="btnSave" runat="server" CssClass="fd-add" ValidationGroup="userDetails" CausesValidation="true" Text="Save" OnClick="btnSave_OnClick" />
    <asp:Button runat="server" ID="btnClose" CausesValidation="false" Text="Close" OnClick="btnClose_OnClick"/>
    <asp:Button ID="btnNext" runat="server" CssClass="fd-add" ValidationGroup="userDetails" CausesValidation="false" Text="Next" OnClick="btnNext_OnClick" />
</asp:Content>
