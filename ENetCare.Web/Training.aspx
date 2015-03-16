<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="Training.aspx.cs" Inherits="ENetCare.Web.Training" %>

<%@ Register TagName="PackageBarcodeUserControl" TagPrefix="uc" Src="~/UserControl/PackageBarcode.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Training</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    
    <h2>Training Page</h2>
    <p>Content of Training Page</p>

    <uc:PackageBarcodeUserControl ID="ucPackageBarcode" runat="server" />
    
    <p>
        <asp:Literal ID="litBarcodeList" runat="Server"/>
    </p>
    
    <asp:Button ID="btnSave" runat="server" CssClass="fd-add" CausesValidation="true" Text="Save" OnClick="btnSave_Click" />

</asp:Content>
