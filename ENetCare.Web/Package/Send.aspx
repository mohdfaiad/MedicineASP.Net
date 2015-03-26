<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="Send.aspx.cs" Inherits="ENetCare.Web.Sending" %>

<%@ Register TagName="PackageBarcodeUserControl" TagPrefix="uc" Src="~/UserControl/PackageBarcode.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sending Package</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <h2>Sending Package</h2>

    <asp:Panel ID="pnlMessage" runat="server" CssClass="message" Visible="false">
            <asp:Literal ID="litMessage" runat="server" />
    </asp:Panel>

    <asp:Panel ID="pnlErrorMessage" runat="server" Visible="false" CssClass="message error">
        
         <asp:ValidationSummary ID="valSummary" runat="server" 
            ValidationGroup="destinationDetails"
            EnableClientScript="false" ShowSummary="true" DisplayMode="BulletList" ForeColor="Red" /> 
    </asp:Panel>
    
    <uc:PackageBarcodeUserControl ID="ucPackageBarcode" runat="server" />

    <table>
        <tr>
            <td>
                <asp:Label ID="lblDestination" runat="server" 
                        AssociatedControlID="ddlDestination" Text="Send to:" />
            </td>
            <td>
                <asp:DropDownList ID="ddlDestination" runat="server" Width="200" TabIndex="2" Enabled="true"
                     AutoPostBack="True" OnSelectedIndexChanged="ddlDestination_SelectedIndexChanged"/>
                <asp:RequiredFieldValidator ID="valReqDestination" runat="server" 
                    ControlToValidate="ddlDestination" 
                    ValidationGroup="destinationDetails"
                    Display="None" EnableClientScript="false" SetFocusOnError="true"
                    Text="*"
                    ErrorMessage="Please Choose Destination" />
            </td>
        </tr>
    </table>
    <asp:Button ID="btnCancel" Width="80" runat="server" CausesValidation="true" Text="Cancel"  />
    <asp:Button ID="btnSave" Width="80" runat="server" CausesValidation="true" Text="Save" />
</asp:Content>
