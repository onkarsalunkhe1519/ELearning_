<%@ Page Title="Certificates" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="Certificates.aspx.cs" Inherits="ELearning_Team3.Certificates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .grid-view {
            width: 100%;
            border-collapse: collapse;
        }

        .grid-view th, .grid-view td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        .grid-view th {
            background-color: #f4f4f4;
        }

        .grid-view tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        .grid-view tr:hover {
            background-color: #f1f1f1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Your Certificates</h2>
    <asp:GridView ID="GridViewCertificates" runat="server" AutoGenerateColumns="False" CssClass="grid-view" OnRowCommand="GridViewCertificates_RowCommand">
        <Columns>
            <asp:BoundField DataField="CertificateID" HeaderText="Certificate ID" />
           
            <asp:BoundField DataField="GeneratedDate" HeaderText="Generated Date" DataFormatString="{0:dd MMM yyyy}" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button ID="btnViewCertificate" runat="server" CommandName="View" CommandArgument='<%# Eval("CertificatePath") %>' Text="View Certificate" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
