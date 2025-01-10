<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="MyCourses.aspx.cs" Inherits="ELearning_Team3.MyCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="985px">
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("UserEmail") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Master Course">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("MasterCourseName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Course">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("CourseName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
          <asp:TemplateField HeaderText="Days Left">
    <ItemTemplate>
        <asp:Label ID="LabelDaysLeft" runat="server" Text='<%#Eval("DaysLeft") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnNavigate" runat="server" Text="Go to Page" CommandName="Navigate" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <SortedAscendingCellStyle BackColor="#FAFAE7" />
        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
        <SortedDescendingCellStyle BackColor="#E1DB9C" />
        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
    </asp:GridView>
</asp:Content>
