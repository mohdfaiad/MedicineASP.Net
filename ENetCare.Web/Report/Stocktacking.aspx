<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/General.Master" AutoEventWireup="true" CodeBehind="Stocktaking.aspx.cs" Inherits="ENetCare.Web.Report.Stocktaking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Distribution Centre Losses Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <h2>Distribution Centre Losses Report</h2>
    
    <div style ="height:250px; width:700px; overflow:auto;">
    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="grd_RowCreated" OnRowDataBound="grd_RowDataBound">
        <Columns>
   






        </Columns>
    </asp:GridView>
    </div>

    <asp:Button runat="server" ID="btnClose" CausesValidation="false" Text="Close" OnClick="btnClose_OnClick"/>
</asp:Content>
