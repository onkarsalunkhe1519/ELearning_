<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="CourseVideos.aspx.cs" Inherits="ELearning_Team3.CourseVideos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
    <style>
        body {
            background-color: #f9f9f9;
        }

        .sidebar {
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
        }

        .content-area {
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
        }

        iframe {
            border-radius: 8px;
        }

        .rating img {
            width: 25px;
            height: 25px;
        }

        .btn-primary {
            background-color: #007bff;
            border: none;
        }

        .btn-primary:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <!-- Sidebar for Video List -->
            <div class="col-md-4 sidebar">
                <h4 class="text-center">Available Videos</h4>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" OnRowCommand="GridViewVideos_RowCommand">
    <Columns>
        <asp:TemplateField HeaderText="Topic Name">
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Eval("TopicName") %>' CssClass="fw-bold"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:Button ID="btnPlay" runat="server" CommandName="Play" CommandArgument='<%# Eval("TopicName") %>' Text="Play Video" CssClass="btn btn-primary btn-sm" Enabled="true" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

            </div>

            <!-- Main Content Area -->
            <div class="col-md-8 content-area">
                <asp:Literal ID="ltrYouTubePlayer" runat="server"></asp:Literal>

                <!-- MCQs Section -->
                <asp:Repeater ID="rptQuestions" runat="server" Visible="false">
                    <ItemTemplate>
                        <div class="mb-4">
                            <p class="fw-bold"><asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("QuestionText") %>'></asp:Label></p>
                            <asp:RadioButtonList ID="rblChoices" runat="server" DataSource='<%# GetChoices(Eval("QuestionText").ToString()) %>' DataTextField="ChoiceText" DataValueField="ChoiceText" CssClass="form-check"></asp:RadioButtonList>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Button ID="btnSubmitAnswers" runat="server" Text="Submit Answers" OnClick="btnSubmitAnswers_Click" CssClass="btn btn-success mt-3" Visible="false" />
                <asp:Label ID="lblResult" runat="server" CssClass="mt-2"></asp:Label>
                <asp:Button ID="btnCertificate" runat="server" Text="Download Certificate" OnClick="btnCertificate_Click" CssClass="btn btn-warning mt-3" Enabled="true" />
            </div>
        </div>

        <!-- Review Section -->
        <div class="row mt-5">
            <div class="col-12">
                <h4>Rate and Review</h4>
                <div class="rating">
                    <asp:RadioButton ID="rb1" runat="server" GroupName="Rating" /><img src="star.png" />
                    <asp:RadioButton ID="rb2" runat="server" GroupName="Rating" /><img src="star.png" />
                    <asp:RadioButton ID="rb3" runat="server" GroupName="Rating" /><img src="star.png" />
                    <asp:RadioButton ID="rb4" runat="server" GroupName="Rating" /><img src="star.png" />
                    <asp:RadioButton ID="rb5" runat="server" GroupName="Rating" /><img src="star.png" />
                </div>
                <asp:TextBox ID="txtReview" runat="server" TextMode="MultiLine" CssClass="form-control mt-3" Rows="3" Placeholder="Write your review here..."></asp:TextBox>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary mt-3" />
            </div>

            <div class="col-12 mt-4">
                <h4>All Reviews</h4>
                <asp:GridView ID="gvReviews" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                    <Columns>
                        <asp:BoundField DataField="Rating" HeaderText="Rating" />
                        <asp:BoundField DataField="ReviewText" HeaderText="Review" />
                        <asp:BoundField DataField="UserEmail" HeaderText="User Email" />
                        <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Date" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
