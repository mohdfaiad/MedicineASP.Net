<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="Sending.aspx.cs" Inherits="ENetCare.Web.Sending" %>

<%@ Register TagName="PackageBarcodeUserControl" TagPrefix="uc" Src="~/UserControl/PackageBarcode.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sending Package</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <h2>Sending Package</h2>
    
    <uc:PackageBarcodeUserControl ID="ucPackageBarcode" runat="server" />

    <table>
        <tr>
            <td>
                <asp:Label ID="lblDestination" runat="server" 
                        AssociatedControlID="ddlDestination" Text="Send to:" />
            </td>
            <td>
                <asp:DropDownList ID="ddlDestination" runat="server" Width="200" TabIndex="2" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDestination_SelectedIndexChanged" />
            </td>
        </tr>
    </table>
    <asp:Button ID="btnCancel" align="right" Width="80" runat="server" CausesValidation="true" Text="Cancel"  />
    <asp:Button ID="btnSave" align="right" Width="80" runat="server" CausesValidation="true" Text="Save"  />
</asp:Content>
