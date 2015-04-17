<%@ Page Title="Stocktake Report" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="Stocktaking.aspx.cs" Inherits="ENetCare.Web.Report.Stocktaking" %>
    


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Stocktake Report</title>   
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
        <h2>Stocktake Report</h2> 

   <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="grd_RowCreated" OnRowDataBound="grd_RowDataBound">
        
           <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Package Id">
                <ItemTemplate>
                    <asp:Literal ID="litPackageId" runat="server" Text='<%# Eval("PackageId") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Bar Code">
                <ItemTemplate>
                    <asp:Literal ID="litBarCode" runat="server" Text='<%# Eval("BarCode") %>'></asp:Literal>
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
               
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Expiration Date">
                <ItemTemplate>
                    <asp:Literal ID="litExpirationDate" runat="server" Text='<%# string.Format("{0:d}", Eval("ExpirationDate")) %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
       
            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Cost Per Package">
                <ItemTemplate>
                    <asp:Literal ID="litCostPerPackage" runat="server" Text='<%# string.Format("{0:C2}", Eval("CostPerPackage")) %>'></asp:Literal>
                </ItemTemplate>

                <FooterTemplate>
                    <div style="text-align: right;">
                        <asp:Literal ID="litGrandTotalValue" runat="server" />
                    </div>
                </FooterTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="percent-10" HeaderText="Days Left" Visible="False">
                <ItemTemplate>
                    <asp:Literal ID="litDaysLeft" runat="server" Text='<%# Eval("DaysLeft") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
 
        </Columns>
           
    </asp:GridView>









    <asp:Button runat="server" ID="btnClose" CausesValidation="false" Text="Close" OnClick="btnClose_OnClick"/>
   
        <br />
        <br />
        <br />
        <br />
        
</asp:Content>


