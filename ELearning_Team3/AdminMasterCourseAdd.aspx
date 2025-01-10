<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminMasterCourseAdd.aspx.cs" Inherits="ELearning_Team3.AdminMasterCourseAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            margin: 0;
            padding: 0;
        }

        .card-title {
            font-size: 1.5rem;
            font-weight: bold;
            margin-bottom: 20px;
            color: #333;
            text-align: center;
        }

        label {
            font-size: 1rem;
            color: #555;
            margin-bottom: 10px;
            display: block;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            font-size: 1rem;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
            transition: all 0.3s ease-in-out;
        }

        .form-control:focus {
            border-color: #007bff;
            box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
            outline: none;
        }

        .form-container {
            max-width: 500px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            border-radius: 10px;
        }

        .form-container h4 {
            margin-bottom: 20px;
            color: #444;
        }

        .form-container .form-control {
            margin-bottom: 20px;
        }

        .form-container .form-control[type="file"] {
            padding: 5px;
        }

        .form-container .btn {
            width: 100%;
            padding: 10px;
            font-size: 1rem;
            color: #fff;
            background-color: #007bff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease-in-out;
        }

        .form-container .btn:hover {
            background-color: #0056b3;
        }

        .form-container .message {
            margin-top: 10px;
            font-size: 0.9rem;
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        
    <h4 class="card-title">Add Master Course</h4>

                    <label>Add Master Course</label>
                    

      <asp:TextBox ID="TextBox1"  class="form-control" runat="server"></asp:TextBox>
    <asp:FileUpload class="form-control" ID="FileUpload1" runat="server" />     
        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Add Master Course" OnClick="Button1_Click1" CausesValidation="False" />
   
</asp:Content>
