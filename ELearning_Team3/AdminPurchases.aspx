<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminPurchases.aspx.cs" Inherits="ELearning_Team3.AdminPurchases" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style>
        .gridview {
            border: 1px solid #ccc;
            width: 100%;
            margin: 20px 0;
            border-collapse: collapse;
        }
        .gridview th, .gridview td {
            border: 1px solid #ccc;
            padding: 8px;
            text-align: left;
        }
        .gridview th {
            background-color: #f4f4f4;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:GridView ID="GridView1" runat="server" CssClass="gridview" AutoGenerateColumns="true" />
</asp:Content>
