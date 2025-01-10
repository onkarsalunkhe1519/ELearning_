<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="HelpSupportUser.aspx.cs" Inherits="ELearning_Team3.HelpSupportUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <div style="width: 50%; margin: auto; padding: 20px; border: 1px solid #ccc; border-radius: 10px;">

            <h2>Help and Support</h2>
            <hr />

            <!-- Course Selection -->
            <div>
                <label for="ddlCourses">Select Course:</label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
            </div>
            <br />

            <!-- Problem Description -->
            <div>
                <label for="txtProblemDescription">Add Problem:</label><br />
                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5" Width="100%"></asp:TextBox>
            </div>
            <br />

            <!-- Submit Button -->
            <div>
                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Button1_Click" />
            </div>

                 <div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SupportID" HeaderText="SupportID" />
                        <asp:BoundField DataField="MasterCourseName" HeaderText="Master Course" />
                        <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                        <asp:BoundField DataField="ProblemDescription" HeaderText="Problem Description" />
                        <asp:BoundField DataField="UserEmail" HeaderText="Email" />
                        <asp:BoundField DataField="Solution" HeaderText="Solution" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
</asp:Content>
