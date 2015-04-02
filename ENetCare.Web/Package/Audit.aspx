<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="Audit.aspx.cs" Inherits="ENetCare.Web.Audit" %>

<%@ Register TagName="PackageBarcodeUserControl" TagPrefix="uc" Src="~/UserControl/PackageBarcode.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Package Audit</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">

    <style type="text/css">
        .hide {display:none;}
    </style>
    
    <h2>Audit Packages</h2>

    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" Height="174px" Width="267px" SideBarButtonStyle-CssClass="hide" OnNextButtonClick="Wizard1_NextButtonClick" OnLoad="Wizard1_Load">
<SideBarButtonStyle CssClass="hide"></SideBarButtonStyle>
        <WizardSteps>
            <asp:WizardStep runat="server" title="Select Barcodes">
                <asp:Panel ID="pnlErrorMessage" runat="server" Visible="false" CssClass="message error">
                    <asp:Literal ID="litErrorMessage" runat="server" />
    
                    <asp:ValidationSummary ID="valSumUserDetails" runat="server" 
                        ValidationGroup="userDetails"
                        EnableClientScript="false" ShowSummary="true" DisplayMode="BulletList" ForeColor="Red" /> 
                </asp:Panel>

                <table border="0">
                    <tr>
                        <td width="100">
                            <asp:Label ID="lblPackageType" runat="server" Text="Package Type:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPackageType" runat="server" Width="200" TabIndex="2" Enabled="true" AutoPostBack="True" OnDataBound="ddlPackageType_DataBound" OnSelectedIndexChanged="ddlPackageType_SelectedIndexChanged" />
                            <asp:RequiredFieldValidator ID="valReqPackageType" runat="server" 
                                ControlToValidate="ddlPackageType" 
                                ValidationGroup="userDetails"
                                Display="None" EnableClientScript="false" SetFocusOnError="true"
                                Text="*"
                                ErrorMessage="Please select a package type" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <uc:PackageBarcodeUserControl ID="ucPackageBarcode" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" title="Confirm Audit">
                <div style ="height:200px; width:700px; overflow:auto;">
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" ShowFooter="true">
                    <Columns>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-50" HeaderText="Bar Code">
                            <ItemTemplate>
                                <asp:Literal ID="litBarcode" runat="server" Text='<%# Eval("BarCode") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-25" HeaderText="Package Id">
                            <ItemTemplate>
                                <asp:Literal ID="litPackageId" runat="server" Text='<%# Eval("PackageId") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-50" HeaderText="Current Status">
                            <ItemTemplate>
                                <asp:Literal ID="litPreviousStatus" runat="server" Text='<%# Eval("CurrentStatus") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-25" HeaderText="Previous Distribution Centre Id">
                            <ItemTemplate>
                                <asp:Literal ID="litPreviousDistributionCentreId" runat="server" Text='<%# Eval("CurrentLocationCentreId") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-50" HeaderText="Previous Distribution Centre Name">
                            <ItemTemplate>
                                <asp:Literal ID="litPreviousDistributionCentreName" runat="server" Text='<%# Eval("CurrentLocationCentreName") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-50" HeaderText="New Status">
                            <ItemTemplate>
                                <asp:Literal ID="litNewStatus" runat="server" Text='<%# Eval("NewStatus") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>


</asp:Content>
