<%@ Page Title="Community" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="CommunityUser.aspx.cs" Inherits="ELearning_Team3.CommunityUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .discussion-box {
            border: 1px solid #ddd;
            padding: 10px;
            margin-bottom: 15px;
        }

        .reply-box {
            margin-left: 20px;
            border-left: 2px solid #ddd;
            padding-left: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Discussions Section -->
    <h2>Discussions</h2>
    <asp:Repeater ID="RepeaterDiscussions" runat="server">
        <ItemTemplate>
            <div class="discussion-box">
                <h4><%# Eval("Title") %></h4>
                <p><%# Eval("Content") %></p>
                <small>By <%# Eval("Author") %> on <%# Eval("CreatedAt") %></small>

                <!-- Replies Section -->
                <asp:Repeater ID="RepeaterReplies" runat="server">
                    <ItemTemplate>
                        <div class="reply-box">
                            <p><%# Eval("Content") %></p>
                            <small>By <%# Eval("Author") %> on <%# Eval("CreatedAt") %></small>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <!-- Add Reply Form -->
                <asp:TextBox ID="TextBoxReplyContent" runat="server" TextMode="MultiLine" CssClass="form-control" Placeholder="Enter your reply"></asp:TextBox>
                <asp:Button ID="ButtonSubmitReply" runat="server" Text="Submit Reply" CssClass="btn btn-primary" CommandArgument='<%# Eval("DiscussionID") %>' OnClick="ButtonSubmitReply_Click" />
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <!-- Add New Discussion -->
    </asp:Content>
