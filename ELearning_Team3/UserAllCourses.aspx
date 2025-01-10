<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="UserAllCourses.aspx.cs" Inherits="ELearning_Team3.UserAllCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
  <style>
    .card {
    width: 320px;
    margin: 20px auto; /* Center the card */
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    border-radius: 10px;
    overflow: hidden;
    transition: transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out;
    background-color: #fff;
}

.card:hover {
    transform: translateY(-10px);
    box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
}

.card img {
    width: 100%;
    height: 200px;
    object-fit: cover; /* Ensures image scaling looks good */
}

.card-body {
    padding: 15px;
    text-align: left; /* Align text to the left */
}

.card-title {
    font-size: 20px;
    font-weight: bold;
    color: #333;
    margin-bottom: 10px;
}

.card-text {
    font-size: 14px;
    color: #555;
    line-height: 1.6;
    margin-bottom: 10px;
}

.btn-primary {
    background-color: #007bff;
    border: none;
    padding: 10px 20px;
    font-size: 14px;
    border-radius: 5px;
    transition: background-color 0.2s ease-in-out, transform 0.2s ease-in-out;
}

.btn-primary:hover {
    background-color: #0056b3;
    transform: scale(1.05);
}

h2 {
    text-align: center;
    font-size: 24px;
    font-weight: bold;
    margi

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div>
            <h2>Select a Course</h2>
            <div class="form-group">
    <label for="DropDownListMasterCourse">Master Course:</label>
    <asp:DropDownList ID="DropDownListMasterCourse" runat="server" CssClass="form-control">
    </asp:DropDownList>
</div>
            <div class="form-group">
                <asp:Button ID="ButtonFetch" runat="server" Text="Fetch Courses" CssClass="btn btn-primary" OnClick="ButtonFetch_Click" />
            </div>
            <div class="row mt-4">
              <div class="row mt-4">
    <asp:Repeater ID="RepeaterCourses" runat="server">
        <ItemTemplate>
            <div class="card">
                <img src='<%# ResolveUrl(Eval("Pic").ToString()) %>' alt="Course Image" class="card-img-top" />
                <div class="card-body">
                    <h5 class="card-title"><%# Eval("CourseName") %></h5>
                    <p class="card-text"><%# Eval("Description") %></p>
                    <p class="card-text">Price: $<%# Eval("Price") %></p>
                    <asp:Button 
                        ID="ButtonBuyCourse" 
                        runat="server" 
                        Text="Buy Now" 
                        CssClass="btn btn-primary"
                        CommandArgument='<%# Eval("CourseName") + "|" + Eval("Price") %>' 
                        OnCommand="ButtonBuyNow_Command" />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

<div>
    <asp:Repeater ID="RepeaterMasterCourse" runat="server">
        <ItemTemplate>
            <div class="card">
                <img src='<%# ResolveUrl(Eval("Pic").ToString()) %>' alt="Master Course Image" class="card-img-top" />
                <div class="card-body">
                    <h5 class="card-title"><%# Eval("MasterCourseName") %></h5>
                    <p class="card-text">
                        <strong>Courses:</strong> <%# Eval("CourseNames") %><br />
                        <strong>Descriptions:</strong> <%# Eval("Descriptions") %><br />
                        <strong>Total Price:</strong> $<%# Eval("TotalPrice") %>
                    </p>
                    <asp:Button 
                        ID="ButtonBuyMasterCourse" 
                        runat="server" 
                        Text="Buy Now" 
                        CssClass="btn btn-primary"
                        CommandArgument='<%# Eval("MasterCourseName") + "|" + Eval("TotalPrice") %>' 
                        OnCommand="ButtonBuyNow_Command" />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>


    </div>
        </div>
   <%-- <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>--%>

</asp:Content>
