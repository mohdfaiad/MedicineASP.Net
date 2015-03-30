<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="DistributionCentreLosses.aspx.cs" Inherits="ENetCare.Web.Report.DistributionCentreLosses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Distribution Centre Losses Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <h2>Distribution Centre Losses Report</h2>
    
    <div style ="height:250px; width:700px; overflow:auto;">
    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="grd_RowCreated" OnRowDataBound="grd_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-15" HeaderText="Distribution Centre Id">
                <ItemTemplate>
                    <asp:Literal ID="litDistributionCentreId" runat="server" Text='<%# Eval("DistributionCentreId") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-55" HeaderText="Distribution Centre Name">
                <ItemTemplate>
                    <asp:Literal ID="litDistributionCentreName" runat="server" Text='<%# Eval("DistributionCentreName") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Loss Ratio">
                <ItemTemplate>
                    <asp:Literal ID="litLossRatio" runat="server" Text=''></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-20" HeaderText="Total Value">
                <ItemTemplate>
                    <asp:Literal ID="litTotalLossDiscardedValue" runat="server" Text='<%# string.Format("{0:C2}", Eval("TotalLossDiscardedValue")) %>'></asp:Literal>
                </ItemTemplate>
                <FooterTemplate>
                    <div style="text-align: right;">
                        <asp:Literal ID="litGrandTotalLossDiscardedValue" runat="server" />
                    </div>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>

    <asp:Button runat="server" ID="btnClose" CausesValidation="false" Text="Close" OnClick="btnClose_OnClick"/>
</asp:Content>
