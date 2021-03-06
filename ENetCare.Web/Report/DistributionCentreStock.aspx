﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="DistributionCentreStock.aspx.cs" Inherits="ENetCare.Web.Report.DistributionCentreStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Distribution Centre Stock Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <h2>Distribution Centre Stock Report</h2>
    
    <div style ="height:250px; width:700px; overflow:auto;">
    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="grd_RowCreated" OnRowDataBound="grd_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Package Type Id">
                <ItemTemplate>
                    <asp:Literal ID="litPackageTypeId" runat="server" Text='<%# Eval("PackageTypeId") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-25" HeaderText="Package Type Description">
                <ItemTemplate>
                    <asp:Literal ID="litPackageTypeDescription" runat="server" Text='<%# Eval("PackageTypeDescription") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Cost Per Package">
                <ItemTemplate>
                    <asp:Literal ID="litCostPerPackage" runat="server" Text='<%# string.Format("{0:C2}", Eval("CostPerPackage")) %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Distribution Centre Id">
                <ItemTemplate>
                    <asp:Literal ID="litDistributionCentreId" runat="server" Text='<%# Eval("DistributionCentreId") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-25" HeaderText="Distribution Centre Name">
                <ItemTemplate>
                    <asp:Literal ID="litDistributionCentreName" runat="server" Text='<%# Eval("DistributionCentreName") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Number Of Packages">
                <ItemTemplate>
                    <asp:Literal ID="litNumberOfPackages" runat="server" Text='<%# string.Format("{0}", Eval("NumberOfPackages")) %>'></asp:Literal>
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
