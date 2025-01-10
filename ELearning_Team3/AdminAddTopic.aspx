<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAddTopic.aspx.cs" Inherits="ELearning_Team3.AdminAddTopic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style>
        /* Styling for the form */
        .form-container {
            background-color: #fff;
            padding: 20px;
            margin: 0 auto;
            border-radius: 6px;
            border: 1px solid #ddd;
            max-width: 700px;
        }

        .form-header {
            background-color: #007bff;
            color: white;
            text-align: center;
            padding: 10px 10px;
            margin-bottom: 20px;
            border-radius: 6px 6px 0 0;
            font-size: 18px;
            font-weight: bold;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group label {
            font-weight: bold;
        }

        .form-control {
            margin-bottom: 15px;
        }

        .btn-primary {
            background-color: #007bff;
            border: none;
            padding: 10px 15px;
            font-size: 16px;
            border-radius: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="form-container">
        <div><h3>Add Topic</h3></div>
        
            <div class="form-group">
                <label for="DropDownList1">Select Course:</label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="TextBox1">Topic Name:</label>
                <asp:TextBox ID="TextBox1" class="form-control" runat="server" placeholder="Enter topic name"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TextBox2">YouTube URL:</label>
                <asp:TextBox ID="TextBox2" class="form-control" runat="server" placeholder="Enter YouTube URL"></asp:TextBox>
            </div>
            <div class="text-center">
                <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Add Topic" OnClick="Button1_Click" CausesValidation="False" />
            </div>
        
    </div>
</asp:Content>
