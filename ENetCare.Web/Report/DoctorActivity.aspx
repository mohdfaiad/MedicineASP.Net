<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="DoctorActivity.aspx.cs" Inherits="ENetCare.Web.Report.DoctorActivity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Doctor Activity Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <h2>Doctor Activity Report</h2>
    
    <div style ="height:250px; width:700px; overflow:auto;">
    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="grd_RowCreated" OnRowDataBound="grd_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Doctor Id">
                <ItemTemplate>
                    <asp:Literal ID="litDoctorId" runat="server" Text='<%# Eval("DoctorId") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-30" HeaderText="Doctor Name">
                <ItemTemplate>
                    <asp:Literal ID="litDoctorName" runat="server" Text='<%# Eval("DoctorName") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Package Type Id">
                <ItemTemplate>
                    <asp:Literal ID="litPackageTypeId" runat="server" Text='<%# Eval("PackageTypeId") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-30" HeaderText="Package Type Description">
                <ItemTemplate>
                    <asp:Literal ID="litPackageTypeDescription" runat="server" Text='<%# Eval("PackageTypeDescription") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Package Count">
                <ItemTemplate>
                    <asp:Literal ID="litPackageCount" runat="server" Text='<%# Eval("PackageCount") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Total Package Value">
                <ItemTemplate>
                    <asp:Literal ID="litTotalPackageValue" runat="server" Text='<%# string.Format("{0:C2}", Eval("TotalPackageValue")) %>'></asp:Literal>
                </ItemTemplate>
                <FooterTemplate>
                    <div style="text-align: right;">
                        <asp:Literal ID="litGrandTotalPackageValue" runat="server" />
                    </div>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>

    <asp:Button runat="server" ID="btnClose" CausesValidation="false" Text="Close" OnClick="btnClose_OnClick"/>
</asp:Content>
