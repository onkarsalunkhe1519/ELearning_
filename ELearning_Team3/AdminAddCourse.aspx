<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAddCourse.aspx.cs" Inherits="ELearning_Team3.AdminAddCourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Integrated styling for form container */
        .form-container {
            background-color: #fff;
            padding: 20px 30px;
            margin: 0 auto;
            border-radius: 6px;
            border: 1px solid #ddd;
            max-width: 700px;
            box-shadow: none;
        }

        .form-header {
            background-color: #007bff;
            color: #fff;
            text-align: center;
            padding: 10px 0;
            font-size: 18px;
            font-weight: bold;
            margin-bottom: 20px;
            border-radius: 6px 6px 0 0;
        }

        .form-group label {
            font-weight: 600;
            font-size: 14px;
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
            transition: all 0.3s;
        }

        .btn-primary:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <div>
            <h3>Add Course</h3>
        </div>
        <form>
            <div class="form-group">
                <label for="DropDownList1">Select Master Course:</label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="TextBox1">Course Name:</label>
                <asp:TextBox ID="TextBox1" class="form-control" runat="server" placeholder="Enter course name"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="FileUpload1">Course Image:</label>
                <asp:FileUpload class="form-control" ID="FileUpload1" runat="server" />
            </div>
            <div class="form-group">
                <label for="TextBox2">Description:</label>
                <asp:TextBox ID="TextBox2" class="form-control" runat="server" TextMode="MultiLine" Rows="3" placeholder="Enter course description"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TextBox3">Price:</label>
                <asp:TextBox ID="TextBox3" class="form-control" runat="server" placeholder="Enter course price"></asp:TextBox>
            </div>
            <div class="text-center">
                <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Add Course" OnClick="Button1_Click1" CausesValidation="False" />
            </div>
        </form>
    </div>
</asp:Content>
