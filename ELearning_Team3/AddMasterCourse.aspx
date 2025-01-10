<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddMasterCourse.aspx.cs" Inherits="ELearning_Team3.AddMasterCourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #body{
            margin-left:20%;
            margin-top:7%

        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body" id="body">
    <div class="basic-form">
       
        <div class="card-header">
    <h4 class="card-title">Add Master Course</h4>
</div><br />
            <div class="form-row">
                <div class="form-group col-md-6">
                    <label>Add Master Course</label>
                    <br />

                    <asp:TextBox ID="TextBox1"  class="form-control" runat="server"></asp:TextBox>
                </div>
                
</div><br />
            </div>
      
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click1" />
    </div>
</div>
</asp:Content>
