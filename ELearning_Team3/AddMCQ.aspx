<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddMCQ.aspx.cs" Inherits="ELearning_Team3.AddMCQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <h2>Add MCQ Questions</h2>

    <!-- Add Question -->
    <label for="ddlTopics">Select Topic:</label>
    <asp:DropDownList ID="ddlTopics" runat="server"></asp:DropDownList><br />

    <label for="txtQuestion">Enter Question:</label>
    <asp:TextBox ID="txtQuestion" runat="server" Width="400"></asp:TextBox><br />

    <asp:Button ID="btnAddQuestion" runat="server" Text="Add Question" OnClick="btnAddQuestion_Click" /><br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label><br />

    <hr />

    <!-- Add Choices -->
    <h2>Add Choices</h2>

    <label for="ddlQuestions">Select Question:</label>
    <asp:DropDownList ID="ddlQuestions" runat="server"></asp:DropDownList><br />

    <label for="txtChoice">Enter Choice Text:</label>
    <asp:TextBox ID="txtChoice" runat="server" Width="400"></asp:TextBox><br />

    <label for="chkIsCorrect">Is Correct:</label>
    <asp:CheckBox ID="chkIsCorrect" runat="server" /><br />

    <asp:Button ID="btnAddChoice" runat="server" Text="Add Choice" OnClick="btnAddChoice_Click" /><br />
    <asp:Label ID="Label1" runat="server" ForeColor="Green"></asp:Label><br />
</asp:Content>
