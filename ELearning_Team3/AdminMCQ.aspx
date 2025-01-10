<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminMCQ.aspx.cs" Inherits="ELearning_Team3.AdminMCQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            background-color: #f4f6f9;
        }

        .filter-container {
            margin-bottom: 20px;
        }

        .filter-container label {
            font-weight: bold;
            margin-right: 10px;
        }

        .grid-container {
            background: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .grid-container h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #333;
        }

        .grid-container table {
            width: 100%;
            border-collapse: collapse;
        }

        .grid-container th, .grid-container td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: left;
        }

        .grid-container th {
            background-color: #007bff;
            color: #fff;
        }

        .grid-container tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        .grid-container tr:hover {
            background-color: #f1f1f1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="filter-container">
        <label for="txtFilterEmail">Filter by User Email:</label>
        <asp:TextBox ID="txtFilterEmail" runat="server" CssClass="form-control" Width="300px" />
        <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" CssClass="btn btn-primary" />
    </div>

    <div class="grid-container">
        <h2>User Reports</h2>
        <asp:GridView ID="GridViewUserReports" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="UserEmail" HeaderText="User Email" />
                <asp:BoundField DataField="TopicName" HeaderText="Topic Name" />
                <asp:BoundField DataField="CorrectAnswers" HeaderText="Correct Answers" />
                <asp:BoundField DataField="WrongAnswers" HeaderText="Wrong Answers" />
                <asp:BoundField DataField="CreatedAt" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
