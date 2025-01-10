<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="UserProfileUser.aspx.cs" Inherits="ELearning_Team3.UserProfileUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

  <meta charset="utf-8" />
<meta
  name="viewport"
  content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0" />

<title>User Profile</title>

<meta name="description" content="" />

<!-- Favicon -->
<link rel="icon" type="image/x-icon" href="../assets/img/favicon/favicon.ico" />

<!-- Fonts -->
<link rel="preconnect" href="https://fonts.googleapis.com" />
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
<link
  href="https://fonts.googleapis.com/css2?family=Public+Sans:ital,wght@0,300;0,400;0,500;0,600;0,700;1,300;1,400;1,500;1,600;1,700&display=swap"
  rel="stylesheet" />

<link rel="stylesheet" href="../assets/vendor/fonts/boxicons.css" />

<!-- Core CSS -->
<link rel="stylesheet" href="../assets/vendor/css/core.css" class="template-customizer-core-css" />
<link rel="stylesheet" href="../assets/vendor/css/theme-default.css" class="template-customizer-theme-css" />
<link rel="stylesheet" href="../assets/css/demo.css" />

<!-- Vendors CSS -->
<link rel="stylesheet" href="../assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <script src="../assets/vendor/js/helpers.js"></script>
<!--! Template customizer & Theme config files MUST be included after core stylesheets and helpers.js in the <head> section -->
<!--? Config:  Mandatory theme config file contain global vars & default theme options, Set your preferred theme option in this file.  -->
<script src="../assets/js/config.js"></script>

  <div class="card mb-6">
  <!-- Account -->
  <div class="card-body">
    <div class="d-flex align-items-start align-items-sm-center gap-6 pb-4 border-bottom">
      <asp:Image
    ID="uploadedAvatar"
    runat="server"
    CssClass="d-block w-px-100 h-px-100 rounded"
    AlternateText="User Avatar"
    ImageUrl="../assets/img/avatars/default.png" />


      <div class="button-wrapper">
        <label for="upload" class="btn btn-primary me-3 mb-4" tabindex="0">
          <span class="d-none d-sm-block">Upload new photo</span>
          <i class="bx bx-upload d-block d-sm-none"></i>
          <asp:FileUpload ID="FileUpload1" class="account-file-input" runat="server" />
          
        </label>
        

        
      </div>
    </div>
  </div>
  <div class="card-body pt-4">
   
      <div class="row g-6">
        <div class="col-md-6">
          <label for="firstName" class="form-label">Name :</label>
          <asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox>
          
        </div>
        
        <div class="col-md-6">
          <label for="email" class="form-label">E-mail</label>
          <asp:TextBox ID="TextBox2" class="form-control" runat="server"  ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-md-6">
          <label for="organization" class="form-label">Password</label>
          <asp:TextBox ID="TextBox3" class="form-control" runat="server"></asp:TextBox>
          
        </div>
        <div class="col-md-6">
          <label class="form-label" for="phoneNumber">Phone Number</label>
          <div class="input-group input-group-merge">
            <asp:TextBox ID="TextBox4" class="form-control" runat="server"></asp:TextBox>
            
          </div>
        </div>
        
      </div>
      <div class="mt-6">
        
        <asp:Button ID="Button1" class="btn btn-primary me-3" runat="server" Text="Save" OnClick="Button1_Click" />
      </div>
    
  </div>
  <!-- /Account -->
</div>
</asp:Content>
