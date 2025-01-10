<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminHelp.aspx.cs" Inherits="ELearning_Team3.AdminHelp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
    <Columns>
        <asp:BoundField DataField="SupportID" HeaderText="ID" />
        <asp:BoundField DataField="UserEmail" HeaderText="User Email" />
        <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
        <asp:BoundField DataField="MasterCourseName" HeaderText="Course Name" />
        <asp:BoundField DataField="ProblemDescription" HeaderText="Problem Description" />
        <asp:BoundField DataField="Solution" HeaderText="Solution" />
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:Button ID="ButtonEdit" Text="Add Solution" CommandName="AddSolution" CommandArgument='<%# Eval("SupportID") %>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:TextBox ID="TextBoxSolution" runat="server" placeholder="Enter solution"></asp:TextBox>
<asp:Button ID="ButtonSubmitSolution" Text="Submit Solution" runat="server" OnClick="SubmitSolution_Click" />

        </div>
</asp:Content>
