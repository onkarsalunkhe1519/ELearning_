<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="NotificationUser.aspx.cs" Inherits="ELearning_Team3.NotificationUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <h2>Your Notifications</h2>
    <asp:Repeater ID="RepeaterNotifications" runat="server">
        <ItemTemplate>
            <div class="card mb-3">
                <div class="card-body">
                    <p class="card-text"><%# Eval("NotificationText") %></p>
                    <p class="card-text"><small class="text-muted">Sent: <%# Eval("SentAt", "{0:g}") %></small></p>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
