<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminReviews.aspx.cs" Inherits="ELearning_Team3.AdminReviews" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style>
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:GridView ID="gvReviews" runat="server" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" Width="760px" >
      <FooterStyle BackColor="White" ForeColor="#333333" />
      <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
      <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
      <RowStyle BackColor="White" ForeColor="#333333" />
      <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
      <SortedAscendingCellStyle BackColor="#F7F7F7" />
      <SortedAscendingHeaderStyle BackColor="#487575" />
      <SortedDescendingCellStyle BackColor="#E5E5E5" />
      <SortedDescendingHeaderStyle BackColor="#275353" />
    </asp:GridView>
</asp:Content>
