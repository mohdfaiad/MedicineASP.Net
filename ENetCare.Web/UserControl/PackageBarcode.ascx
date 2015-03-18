<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PackageBarcode.ascx.cs" Inherits="ENetCare.Web.UserControl.PackageBarcode" %>

<link href="/Public/Styles/enet-barcode-select.css" rel="stylesheet" />

<asp:Panel ID="pnlMessage" runat="server" CssClass="errormessage" Visible="true">
    <asp:Literal ID="litMessage" runat="server" />
</asp:Panel>

<div>
    <span>
        <asp:Label ID="lblBarcode" runat="server" AssociatedControlID="txtBarcode" Text="Barcode:" />
        <asp:TextBox ID="txtBarcode" runat="server" TextMode="SingleLine" MaxLength="20" />
        <asp:Button ID="btnAdd" runat="server" CssClass="" CausesValidation="true" Text="Add" OnClick="btnAdd_Click" />
    </span>
</div>

<div style ="height:120px; width:700px; overflow:auto;">
<asp:GridView ID="grd" runat="server" AutoGenerateColumns="false"
    OnRowDeleting="grd_OnRowDeleting">
    <Columns>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-25" HeaderText="Barcode">
            <ItemTemplate>
                <asp:Literal ID="litBarcode" runat="server" Text='<%# Eval("Barcode") %>'></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-35" HeaderText="Package Type">
            <ItemTemplate>
                <asp:Literal ID="litPackageType" runat="server" Text='<%# Eval("PackageType") %>'></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-15" HeaderText="Expiration Date">
            <ItemTemplate>
                <asp:Literal ID="litExpirationDate" runat="server" Text='<%# string.Format("{0:d}", Eval("ExpirationDate")) %>'></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-15" HeaderText="Package Id">
            <ItemTemplate>
                <asp:Literal ID="litPackageId" runat="server" Text='<%# string.Format("{0}", Eval("PackageId")) %>'></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="percent-10">
            <ItemTemplate>
                <asp:LinkButton ID="lkbRemove" runat="server" CssClass="" 
                    CommandName="Delete" CausesValidation="False" 
                    Text="Remove" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>