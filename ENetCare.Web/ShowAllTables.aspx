<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowAllTables.aspx.cs" Inherits="ENetCare.Web.ShowAllTables" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <hr><h2>Distribution Centres:</h2>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="CentreId" DataSourceID="SqlDataSource1" EmptyDataText="There are no data records to display." AllowSorting="True">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="CentreId" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="CentreId" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                <asp:CheckBoxField DataField="IsHeadOffice" HeaderText="Head Office" SortExpression="IsHeadOffice" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ENetCare %>" DeleteCommand="DELETE FROM [DistributionCentre] WHERE [CentreId] = @CentreId" InsertCommand="INSERT INTO [DistributionCentre] ([Name], [Address], [Phone], [IsHeadOffice]) VALUES (@Name, @Address, @Phone, @IsHeadOffice)" OnSelecting="SqlDataSource1_Selecting" SelectCommand="SELECT [CentreId], [Name], [Address], [Phone], [IsHeadOffice] FROM [DistributionCentre]" UpdateCommand="UPDATE [DistributionCentre] SET [Name] = @Name, [Address] = @Address, [Phone] = @Phone, [IsHeadOffice] = @IsHeadOffice WHERE [CentreId] = @CentreId">
            <DeleteParameters>
                <asp:Parameter Name="CentreId" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="Address" Type="String" />
                <asp:Parameter Name="Phone" Type="String" />
                <asp:Parameter Name="IsHeadOffice" Type="Boolean" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="Address" Type="String" />
                <asp:Parameter Name="Phone" Type="String" />
                <asp:Parameter Name="IsHeadOffice" Type="Boolean" />
                <asp:Parameter Name="CentreId" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        <br />
        <hr><h2>Employees:</h2>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeId" DataSourceID="SqlDataSource2" EmptyDataText="There are no data records to display." AllowSorting="True">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="EmployeeId" HeaderText="Id" ReadOnly="True" SortExpression="EmployeeId" InsertVisible="False" />
                <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                <asp:BoundField DataField="EmailAddress" HeaderText="Email Address" SortExpression="EmailAddress" />
                <asp:BoundField DataField="EmployeeType" HeaderText="Employee Type" SortExpression="EmployeeType" />
                <asp:BoundField DataField="LocationCentreId" HeaderText="Location Id" SortExpression="LocationCentreId" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ENetCare %>" DeleteCommand="DELETE FROM [Employee] WHERE [EmployeeId] = @EmployeeId" InsertCommand="INSERT INTO [Employee] ([UserName], [Password], [FullName], [EmailAddress], [EmployeeType], [LocationCentreId]) VALUES (@UserName, @Password, @FullName, @EmailAddress, @EmployeeType, @LocationCentreId)" SelectCommand="SELECT [EmployeeId], [UserName], [Password], [FullName], [EmailAddress], [EmployeeType], [LocationCentreId] FROM [Employee]" UpdateCommand="UPDATE [Employee] SET [UserName] = @UserName, [Password] = @Password, [FullName] = @FullName, [EmailAddress] = @EmailAddress, [EmployeeType] = @EmployeeType, [LocationCentreId] = @LocationCentreId WHERE [EmployeeId] = @EmployeeId">
            <DeleteParameters>
                <asp:Parameter Name="EmployeeId" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="UserName" Type="String" />
                <asp:Parameter Name="Password" Type="String" />
                <asp:Parameter Name="FullName" Type="String" />
                <asp:Parameter Name="EmailAddress" Type="String" />
                <asp:Parameter Name="EmployeeType" Type="String" />
                <asp:Parameter Name="LocationCentreId" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="UserName" Type="String" />
                <asp:Parameter Name="Password" Type="String" />
                <asp:Parameter Name="FullName" Type="String" />
                <asp:Parameter Name="EmailAddress" Type="String" />
                <asp:Parameter Name="EmployeeType" Type="String" />
                <asp:Parameter Name="LocationCentreId" Type="Int32" />
                <asp:Parameter Name="EmployeeId" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        <br />
        <hr><h2>Package Types:</h2>
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="PackageTypeId" DataSourceID="SqlDataSource3" EmptyDataText="There are no data records to display." AllowSorting="True">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="PackageTypeId" HeaderText="Id" ReadOnly="True" SortExpression="PackageTypeId" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="NumberOfMedications" HeaderText="Medications" SortExpression="NumberOfMedications" />
                <asp:BoundField DataField="ShelfLifeUnitType" HeaderText="Life Unit" SortExpression="ShelfLifeUnitType" />
                <asp:BoundField DataField="ShelfLifeUnits" HeaderText="Shelf Life" SortExpression="ShelfLifeUnits" />
                <asp:CheckBoxField DataField="TemperatureSensitive" HeaderText="Temp. Sensitive" SortExpression="TemperatureSensitive" />
                <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ENetCare %>" DeleteCommand="DELETE FROM [StandardPackageType] WHERE [PackageTypeId] = @PackageTypeId" InsertCommand="INSERT INTO [StandardPackageType] ([Description], [NumberOfMedications], [ShelfLifeUnitType], [ShelfLifeUnits], [TemperatureSensitive], [Value]) VALUES (@Description, @NumberOfMedications, @ShelfLifeUnitType, @ShelfLifeUnits, @TemperatureSensitive, @Value)" ProviderName="<%$ ConnectionStrings:ENetCare.ProviderName %>" SelectCommand="SELECT [PackageTypeId], [Description], [NumberOfMedications], [ShelfLifeUnitType], [ShelfLifeUnits], [TemperatureSensitive], [Value] FROM [StandardPackageType]" UpdateCommand="UPDATE [StandardPackageType] SET [Description] = @Description, [NumberOfMedications] = @NumberOfMedications, [ShelfLifeUnitType] = @ShelfLifeUnitType, [ShelfLifeUnits] = @ShelfLifeUnits, [TemperatureSensitive] = @TemperatureSensitive, [Value] = @Value WHERE [PackageTypeId] = @PackageTypeId">
            <DeleteParameters>
                <asp:Parameter Name="PackageTypeId" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Description" Type="String" />
                <asp:Parameter Name="NumberOfMedications" Type="Int32" />
                <asp:Parameter Name="ShelfLifeUnitType" Type="String" />
                <asp:Parameter Name="ShelfLifeUnits" Type="Int32" />
                <asp:Parameter Name="TemperatureSensitive" Type="Boolean" />
                <asp:Parameter Name="Value" Type="Decimal" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Description" Type="String" />
                <asp:Parameter Name="NumberOfMedications" Type="Int32" />
                <asp:Parameter Name="ShelfLifeUnitType" Type="String" />
                <asp:Parameter Name="ShelfLifeUnits" Type="Int32" />
                <asp:Parameter Name="TemperatureSensitive" Type="Boolean" />
                <asp:Parameter Name="Value" Type="Decimal" />
                <asp:Parameter Name="PackageTypeId" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        <br />
        <hr><h2>Packages:</h2>
        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataKeyNames="PackageId" DataSourceID="SqlDataSource4" EmptyDataText="There are no data records to display." AllowSorting="True">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="PackageId" HeaderText="Id" ReadOnly="True" SortExpression="PackageId" />
                <asp:BoundField DataField="BarCode" HeaderText="Bar Code" SortExpression="BarCode" />
                <asp:BoundField DataField="ExpirationDate" HeaderText="Expiration" SortExpression="ExpirationDate" />
                <asp:BoundField DataField="PackageTypeId" HeaderText="Type Id" SortExpression="PackageTypeId" />
                <asp:BoundField DataField="CurrentLocationCentreId" HeaderText="Location Id" SortExpression="CurrentLocationCentreId" />
                <asp:BoundField DataField="CurrentStatus" HeaderText="Status" SortExpression="CurrentStatus" />
                <asp:BoundField DataField="DistributedByEmployeeId" HeaderText="Distributed By" SortExpression="DistributedByEmployeeId" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ENetCare %>" DeleteCommand="DELETE FROM [Package] WHERE [PackageId] = @PackageId" InsertCommand="INSERT INTO [Package] ([BarCode], [ExpirationDate], [PackageTypeId], [CurrentLocationCentreId], [CurrentStatus], [DistributedByEmployeeId]) VALUES (@BarCode, @ExpirationDate, @PackageTypeId, @CurrentLocationCentreId, @CurrentStatus, @DistributedByEmployeeId)" SelectCommand="SELECT [PackageId], [BarCode], [ExpirationDate], [PackageTypeId], [CurrentLocationCentreId], [CurrentStatus], [DistributedByEmployeeId] FROM [Package]" UpdateCommand="UPDATE [Package] SET [BarCode] = @BarCode, [ExpirationDate] = @ExpirationDate, [PackageTypeId] = @PackageTypeId, [CurrentLocationCentreId] = @CurrentLocationCentreId, [CurrentStatus] = @CurrentStatus, [DistributedByEmployeeId] = @DistributedByEmployeeId WHERE [PackageId] = @PackageId">
            <DeleteParameters>
                <asp:Parameter Name="PackageId" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="BarCode" Type="String" />
                <asp:Parameter Name="ExpirationDate" Type="DateTime" />
                <asp:Parameter Name="PackageTypeId" Type="Int32" />
                <asp:Parameter Name="CurrentLocationCentreId" Type="Int32" />
                <asp:Parameter Name="CurrentStatus" Type="String" />
                <asp:Parameter Name="DistributedByEmployeeId" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="BarCode" Type="String" />
                <asp:Parameter Name="ExpirationDate" Type="DateTime" />
                <asp:Parameter Name="PackageTypeId" Type="Int32" />
                <asp:Parameter Name="CurrentLocationCentreId" Type="Int32" />
                <asp:Parameter Name="CurrentStatus" Type="String" />
                <asp:Parameter Name="DistributedByEmployeeId" Type="Int32" />
                <asp:Parameter Name="PackageId" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        <br />
        <hr><h2>PackageTransits:</h2>
        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataKeyNames="TransitId" DataSourceID="SqlDataSource5" EmptyDataText="There are no data records to display." AllowSorting="True">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="TransitId" HeaderText="Id" ReadOnly="True" SortExpression="TransitId" />
                <asp:BoundField DataField="PackageId" HeaderText="Package Id" SortExpression="PackageId" />
                <asp:BoundField DataField="SenderCentreId" HeaderText="Sender Id" SortExpression="SenderCentreId" />
                <asp:BoundField DataField="ReceiverCentreId" HeaderText="Receiver Id" SortExpression="ReceiverCentreId" />
                <asp:BoundField DataField="DateSent" HeaderText="Date Sent" SortExpression="DateSent" />
                <asp:BoundField DataField="DateReceived" HeaderText="Date Received" SortExpression="DateReceived" />
                <asp:BoundField DataField="DateCancelled" HeaderText="Date Cancelled" SortExpression="DateCancelled" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ENetCare %>" DeleteCommand="DELETE FROM [PackageTransit] WHERE [TransitId] = @TransitId" InsertCommand="INSERT INTO [PackageTransit] ([PackageId], [SenderCentreId], [ReceiverCentreId], [DateSent], [DateReceived], [DateCancelled]) VALUES (@PackageId, @SenderCentreId, @ReceiverCentreId, @DateSent, @DateReceived, @DateCancelled)" ProviderName="<%$ ConnectionStrings:ENetCare.ProviderName %>" SelectCommand="SELECT [TransitId], [PackageId], [SenderCentreId], [ReceiverCentreId], [DateSent], [DateReceived], [DateCancelled] FROM [PackageTransit]" UpdateCommand="UPDATE [PackageTransit] SET [PackageId] = @PackageId, [SenderCentreId] = @SenderCentreId, [ReceiverCentreId] = @ReceiverCentreId, [DateSent] = @DateSent, [DateReceived] = @DateReceived, [DateCancelled] = @DateCancelled WHERE [TransitId] = @TransitId">
            <DeleteParameters>
                <asp:Parameter Name="TransitId" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="PackageId" Type="Int32" />
                <asp:Parameter Name="SenderCentreId" Type="Int32" />
                <asp:Parameter Name="ReceiverCentreId" Type="Int32" />
                <asp:Parameter Name="DateSent" Type="DateTime" />
                <asp:Parameter Name="DateReceived" Type="DateTime" />
                <asp:Parameter Name="DateCancelled" Type="DateTime" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="PackageId" Type="Int32" />
                <asp:Parameter Name="SenderCentreId" Type="Int32" />
                <asp:Parameter Name="ReceiverCentreId" Type="Int32" />
                <asp:Parameter Name="DateSent" Type="DateTime" />
                <asp:Parameter Name="DateReceived" Type="DateTime" />
                <asp:Parameter Name="DateCancelled" Type="DateTime" />
                <asp:Parameter Name="TransitId" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
    
    </div>
    </form>
</body>
</html>
