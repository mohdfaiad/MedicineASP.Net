<%@ Page Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="EditEmployee.aspx.cs" Inherits="ENetCare.Web.EditEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Edit Employee</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    
    <h2>Edit Employee</h2>
    
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
            <td width="100">
                <asp:Label ID="lblUserName" runat="server" Text="User Name:" />
            </td>
            <td>
                <asp:Literal ID="litUserName" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFullName" runat="server" 
                        AssociatedControlID="txtFullName" Text="Full Name:" />
            </td>
            <td>
                <asp:TextBox ID="txtFullName" runat="server" MaxLength="50" Width="180" TabIndex="1" />
                <asp:RequiredFieldValidator ID="valReqName" runat="server" 
                    ControlToValidate="txtFullName" 
                    ValidationGroup="userDetails"
                    Display="None" EnableClientScript="false" SetFocusOnError="true"
                    Text="*"
                    ErrorMessage="Please enter a full name" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEmailAddress" runat="server" 
                        AssociatedControlID="txtEmailAddress" Text="Email Address:" />
            </td>
            <td>
                <asp:TextBox ID="txtEmailAddress" runat="server" MaxLength="50" Width="180" TabIndex="2" />
                <asp:RequiredFieldValidator ID="valReqEmailAddress" runat="server" 
                    ControlToValidate="txtEmailAddress" 
                    ValidationGroup="userDetails"
                    Display="None" EnableClientScript="false" SetFocusOnError="true"
                    Text="*"
                    ErrorMessage="Please enter an Email Address" />
                <asp:RegularExpressionValidator ID="valRegexEmail" runat="server" ControlToValidate="txtEmailAddress"
                    Display="None" EnableClientScript="false" SetFocusOnError="true" ValidationExpression="[^,\s]{1,64}@[^,\s]{1,249}" ValidationGroup="userDetails"
                    Text="*"
                    ErrorMessage="Please enter a valid email address" />
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="lblEmployeeType" runat="server" AssociatedControlID="rblEmployeeType" Text="Employee Type:" />
            </td>
            <td>
                <asp:RadioButtonList ID="rblEmployeeType" RepeatDirection="Horizontal" enabled="false" runat="server">
                    <asp:ListItem Text="Agent" Value="Agent" />
                    <asp:ListItem Text="Doctor" Value="Doctor" />
                    <asp:ListItem Text="Manager" Value="Manager"/>
                </asp:RadioButtonList>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="lblLocation" runat="server" AssociatedControlID="ddlLocation" Text="Location:" />
            </td>
            <td>
                <asp:DropDownList ID="ddlLocation" runat="server" Width="200" TabIndex="2" Enabled="true" />
            </td>
        </tr>
    </table>

    <asp:Button ID="btnSave" runat="server" CssClass="fd-add" ValidationGroup="userDetails" CausesValidation="true" Text="Save" OnClick="btnSave_OnClick" />
    <asp:Button runat="server" ID="btnClose" CausesValidation="false" Text="Close" OnClick="btnClose_OnClick"/>
</asp:Content>