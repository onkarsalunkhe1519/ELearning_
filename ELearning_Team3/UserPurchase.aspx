<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="UserPurchase.aspx.cs" Inherits="ELearning_Team3.UserPurchase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .gridview {}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>My Purchases</h2>
    <asp:GridView ID="gvPurchases" runat="server" AutoGenerateColumns="False" CssClass="gridview" CellPadding="4" ForeColor="#333333" GridLines="None" Width="821px">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="PurchaseID" HeaderText="ID" />
            <asp:BoundField DataField="MasterCourseName" HeaderText="Master Course Name" />
            <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
            <asp:BoundField DataField="PurchaseType" HeaderText="Purchase Type" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="PurchaseDate" HeaderText="Purchase Date" DataFormatString="{0:yyyy-MM-dd}" />
        </Columns>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
</asp:Content>
