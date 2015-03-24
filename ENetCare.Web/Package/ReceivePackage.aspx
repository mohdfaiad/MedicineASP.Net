<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceivePackage.aspx.cs" Inherits="ENetCare.Web.Package.ReceivePackage" MasterPageFile="~/MasterPages/General.Master"%>


<%--  [COMMENT] This is just a protoype. This page is suppose to reuse components .    (Pablo 24-03-15)      --%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Receive Package</title>
    <link href="/Public/Styles/enet-design.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    
    <h2>Receive Package</h2>
    <br/>Please scan a package or type its code <br/><br/>
    <asp:Panel ID="pnlErrorMessage" runat="server" Visible="false" CssClass="message error">
        <asp:Literal ID="litErrorMessage" runat="server" />
    
        <asp:ValidationSummary ID="valSumUserDetails" runat="server" 
            ValidationGroup="userDetails"
            EnableClientScript="false" ShowSummary="true" DisplayMode="BulletList" ForeColor="Red" /> 
    </asp:Panel>
    
    <table border="0">
        <tr>
            <td>
                <asp:Label ID="Label_code" runat="server" AssociatedControlID="txtExpirationDate" Text="Package Code:" />
            </td>
            <td>
                <asp:TextBox ID="TextBox_Code" runat="server"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td> <%-- width="100" --%>
                <asp:Label ID="lblPackageType" runat="server" Text="Package Type:" />
            </td>
            <td>
                    
                <asp:TextBox ID="TextBox_Type" runat="server" ReadOnly="true"></asp:TextBox>
                    
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblExpirationDate" runat="server" AssociatedControlID="txtExpirationDate" Text="Expiration Date:" />
            </td>
            <td>
                <asp:TextBox ID="TextBox_Expiration" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLocation" runat="server" AssociatedControlID="ddlLocation" Text="Coming From:" />
            </td>
            <td>
                <asp:TextBox ID="TextBox_From" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        
    </table>

    <asp:Button ID="btnSave" runat="server" CssClass="fd-add" ValidationGroup="userDetails" CausesValidation="true" Text="Check In" OnClick="btnSave_OnClick" />
    <!-- <asp:Button runat="server" ID="btnClose" CausesValidation="false" Text="Close" OnClick="btnClose_OnClick"/>  -->
    <asp:Button ID="btnNext" runat="server" CssClass="fd-add" ValidationGroup="" CausesValidation="false" Text="Cancel" OnClick="cancel_OnClick" />
</asp:Content>



