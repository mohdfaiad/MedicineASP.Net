<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="ValueInTransit.aspx.cs" Inherits="ENetCare.Web.Report.ValueInTransit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Value In Transit Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">

    <h2>Value In Transit Report</h2>
    
    <div style ="height:250px; width:700px; overflow:auto;">
    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="grd_RowCreated" OnRowDataBound="grd_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Sender Distribution Centre Id">
                <ItemTemplate>
                    <asp:Literal ID="litSenderDistributionCentreId" runat="server" Text='<%# Eval("SenderDistributionCentreId") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-25" HeaderText="Sender Distribution Centre Name">
                <ItemTemplate>
                    <asp:Literal ID="litSenderDistributionCentreName" runat="server" Text='<%# Eval("SenderDistributionCentreName") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
     
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Receiver Distribution Centre Id">
                <ItemTemplate>
                    <asp:Literal ID="litReceiverDistributionCentreId" runat="server" Text='<%# Eval("ReceiverDistributionCentreId") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-25" HeaderText="Receiver Distribution Centre Name">
                <ItemTemplate>
                    <asp:Literal ID="litReceiverDistributionCentreName" runat="server" Text='<%# Eval("ReceiverDistributionCentreName") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
     
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Total Packages">
                <ItemTemplate>
                    <asp:Literal ID="litTotalPackages" runat="server" Text='<%# Eval("TotalPackages") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Total Value">
                <ItemTemplate>
                    <asp:Literal ID="litTotalValue" runat="server" Text='<%# string.Format("{0:C2}", Eval("TotalValue")) %>'></asp:Literal>
                </ItemTemplate>
                <FooterTemplate>
                    <div style="text-align: right;">
                        <asp:Literal ID="litGrandTotalValue" runat="server" />
                    </div>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>

    <asp:Button runat="server" ID="btnClose" CausesValidation="false" Text="Close" OnClick="btnClose_OnClick"/>

</asp:Content>
