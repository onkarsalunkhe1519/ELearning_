<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="ELearning_Team3.ViewCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <style>
        .cart-container {
            margin: 20px auto;
            width: 80%;
        }

        .btn-buy {
            margin-top: 20px;
        }

        .grid-view {
            width: 100%;
            border-collapse: collapse;
        }

        .grid-view th, .grid-view td {
            padding: 10px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .grid-view th {
            background-color: #f4f4f4;
        }

        .total-price {
            margin-top: 20px;
            font-size: 18px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="cart-container">
        <h2>Your Cart</h2>
        <asp:GridView ID="GridViewCart" runat="server" AutoGenerateColumns="False" CssClass="grid-view" OnRowDeleting="GridViewCart_RowDeleting">
            <Columns>
                <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                <asp:BoundField DataField="MasterCourseName" HeaderText="Master Course Name" />
                <asp:BoundField DataField="Price" HeaderText="Price ($)" DataFormatString="{0:N2}" />
                <asp:ButtonField CommandName="Delete" Text="Delete" ButtonType="Button" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="LabelTotalPrice" runat="server" CssClass="total-price"></asp:Label>
        <asp:Button ID="ButtonBuyAll" runat="server" Text="Buy All" CssClass="btn btn-primary btn-buy" OnClick="ButtonBuyAll_Click" />
    </div>
</asp:Content>
