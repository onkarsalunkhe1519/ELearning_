<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminViewCourses.aspx.cs" Inherits="ELearning_Team3.AdminViewCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style>
        /* Container styling */
        .container {
            padding: 20px;
            margin: 0 auto;
            max-width: 1200px;
            background-color: #f9f9f9;
            border-radius: 10px;
            border: 1px solid #e3e3e3;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }

        /* Header styling */
        .header {
            background-color: #007bff;
            color: #ffffff;
            padding: 15px;
            font-size: 20px;
            text-align: center;
            border-radius: 10px 10px 0 0;
            font-weight: bold;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        /* Grid container styling */
        .grid-container {
            margin-bottom: 40px;
            border: 1px solid #e3e3e3;
            border-radius: 10px;
            background-color: #ffffff;
            overflow: hidden;
        }

        /* GridView styling */
        .grid-view {
            width: 100%;
            border-collapse: collapse;
        }

        .grid-view th {
            background-color: #007bff;
            color: white;
            padding: 10px;
            text-align: left;
            font-size: 16px;
            border-bottom: 2px solid #e3e3e3;
        }

        .grid-view td {
            padding: 10px;
            font-size: 14px;
            border-bottom: 1px solid #e3e3e3;
        }

        .grid-view tr:nth-child(even) {
            background-color: #f5f5f5;
        }

        .grid-view tr:hover {
            background-color: #e9ecef;
        }

        /* Button styling */
        .btn {
            padding: 8px 15px;
            font-size: 14px;
            border-radius: 5px;
            border: none;
            cursor: pointer;
            transition: all 0.3s ease;
        }

        .btn-edit {
            background-color: #28a745;
            color: white;
        }

        .btn-edit:hover {
            background-color: #218838;
        }

        .btn-delete {
            background-color: #dc3545;
            color: white;
        }

        .btn-delete:hover {
            background-color: #c82333;
        }

        /* Responsive styling */
        @media (max-width: 768px) {
            .container {
                padding: 15px;
                max-width: 95%;
            }

            .grid-view th, .grid-view td {
                padding: 8px;
                font-size: 13px;
            }

            .btn {
                padding: 5px 10px;
                font-size: 12px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="container">
        <!-- MasterCourses Grid -->
        <div class="grid-container">
            <div class="header">Manage MasterCourses</div>
            <asp:GridView ID="GridViewMasterCourses" runat="server" CssClass="grid-view" AutoGenerateColumns="False" OnRowEditing="GridViewMasterCourses_RowEditing" OnRowDeleting="GridViewMasterCourses_RowDeleting" OnRowCancelingEdit="GridViewMasterCourses_RowCancelingEdit" OnRowUpdating="GridViewMasterCourses_RowUpdating" DataKeyNames="MasterCourseID">
                <Columns>
                    <asp:BoundField DataField="MasterCourseID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="MasterCourseName" HeaderText="Master Course Name" />
                    <asp:BoundField DataField="Pic" HeaderText="Image" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>

        <!-- Courses Grid -->
        <div class="grid-container">
            <div class="header">Manage Courses</div>
            <asp:GridView ID="GridViewCourses" runat="server" CssClass="grid-view" AutoGenerateColumns="False" OnRowEditing="GridViewCourses_RowEditing" OnRowDeleting="GridViewCourses_RowDeleting" OnRowCancelingEdit="GridViewCourses_RowCancelingEdit" OnRowUpdating="GridViewCourses_RowUpdating" DataKeyNames="CourseId">
                <Columns>
                    <asp:BoundField DataField="CourseId" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                    <asp:BoundField DataField="MasterCourseName" HeaderText="Master Course Name" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Pic" HeaderText="Image" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>

        <!-- Topics Grid -->
        <div class="grid-container">
            <div class="header">Manage Topics</div>
            <asp:GridView ID="GridViewTopics" runat="server" CssClass="grid-view" AutoGenerateColumns="False" OnRowEditing="GridViewTopics_RowEditing" OnRowDeleting="GridViewTopics_RowDeleting" OnRowCancelingEdit="GridViewTopics_RowCancelingEdit" OnRowUpdating="GridViewTopics_RowUpdating" DataKeyNames="TopicId">
                <Columns>
                    <asp:BoundField DataField="TopicId" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="TopicName" HeaderText="Topic Name" />
                    <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                    <asp:BoundField DataField="YoutubeURL" HeaderText="YouTube URL" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
