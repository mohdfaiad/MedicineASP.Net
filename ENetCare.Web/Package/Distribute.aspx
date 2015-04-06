<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="Distribute.aspx.cs" Inherits="ENetCare.Web.Distribute" %>

<%@ Register TagName="PackageBarcodeUserControl" TagPrefix="uc" Src="~/UserControl/PackageBarcode.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Distribute Package</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    
    <h2>Distribute Package</h2>
    
    <asp:Panel ID="pnlMessage" runat="server" CssClass="message" Visible="false">
            <asp:Literal ID="litMessage" runat="server" />
    </asp:Panel>

    <asp:Panel ID="pnlErrorMessage" runat="server" Visible="false" CssClass="message error">
        <asp:Literal ID="litErrorMessage" runat="server" />
         <asp:ValidationSummary ID="valSummary" runat="server" 
            ValidationGroup="destinationDetails"
            EnableClientScript="false" ShowSummary="true" DisplayMode="BulletList" ForeColor="Red" /> 
    </asp:Panel>

    <uc:PackageBarcodeUserControl ID="ucPackageBarcode" runat="server" />
    
    <p>
        <asp:Literal ID="litBarcodeList" runat="Server"/>
    </p>
    
    <asp:Button ID="btnCancel" runat="server" CssClass="fd-add" CausesValidation="true" Text="Cancel" OnClick="btnCancel_Click" />
    <asp:Button ID="btnSave" runat="server" CssClass="fd-add" CausesValidation="true" Text="Save" OnClick="btnSave_Click" />

</asp:Content>