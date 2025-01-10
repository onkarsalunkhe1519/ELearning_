<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UpdateUsesAdmin.aspx.cs" Inherits="ELearning_Team3.UpdateUsesAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="UserEmail" OnRowCommand="gvUsers_RowCommand" CssClass="table">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="UserEmail" HeaderText="Email" SortExpression="UserEmail" />
                    <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact" />
                    
                    <asp:ButtonField CommandName="EditUser"  Text="Edit" ButtonType="Button" />
                    <asp:ButtonField CommandName="ActivateUser"  Text="Activate" ButtonType="Button" />
                    <asp:ButtonField CommandName="DeactivateUser"  Text="Deactivate" ButtonType="Button" />
                </Columns>
            </asp:GridView>
                <br />


       
     <asp:Panel ID="UserDetailsPanel" runat="server" Visible="false">
                <h3>User Details</h3>
                <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox><br />
                
                <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox><br />
                
                <asp:Label ID="lblContact" runat="server" Text="Contact:"></asp:Label>
                <asp:TextBox ID="txtContact" runat="server" CssClass="form-control"></asp:TextBox><br />
                
                <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox><br />
                
                <asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label>
                <asp:TextBox ID="txtRole" runat="server" CssClass="form-control"></asp:TextBox><br />
                
                <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Active" Value="Active" />
                    <asp:ListItem Text="Inactive" Value="Inactive" />
                </asp:DropDownList><br />

                <asp:FileUpload ID="FileUpload1" runat="server" Text="ProfileImg:" />
                <br />
                
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" CssClass="btn btn-primary" />
            </asp:Panel>
</asp:Content>
