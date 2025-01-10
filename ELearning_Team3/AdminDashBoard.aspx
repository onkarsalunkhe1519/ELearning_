<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminDashBoard.aspx.cs" Inherits="ELearning_Team3.AdminDashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0" />
    <meta name="description" content="" />

    <!-- Favicon -->
    <link rel="icon" type="image/x-icon" href="../assets/img/favicon/favicon.ico" />

    <!-- Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Public+Sans:wght@400;600;700&display=swap" rel="stylesheet" />

    <!-- Core CSS -->
    <link rel="stylesheet" href="../assets/vendor/css/core.css" class="template-customizer-core-css" />
    <link rel="stylesheet" href="../assets/vendor/css/theme-default.css" class="template-customizer-theme-css" />
    <link rel="stylesheet" href="../assets/css/demo.css" />

    <!-- Vendors CSS -->
    <link rel="stylesheet" href="../assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.css" />
    <link rel="stylesheet" href="../assets/vendor/libs/apex-charts/apex-charts.css" />

    <!-- Custom CSS -->
    <style>
        .card {
            border-radius: 10px;
        }

        .dashboard-section {
            margin-bottom: 30px;
        }

        #monthlySalesChart {
            max-width: 1500px;
            margin: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <!-- Statistics Section -->
            <div class="row dashboard-section">
                <!-- Total Users -->
                <div class="col-md-6 col-lg-3 mb-4">
                    <div class="card">
                        <div class="card-body text-center">
                            <div class="avatar mb-3">
                                <img src="../assets/img/icons/unicons/chart-success.png" alt="chart success" class="rounded" />
                            </div>
                            <p class="mb-1">Total Users</p>
                            <h4 class="mb-0">
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </h4>
                        </div>
                    </div>
                </div>

                <!-- Total Sales -->
                <div class="col-md-6 col-lg-3 mb-4">
                    <div class="card">
                        <div class="card-body text-center">
                            <div class="avatar mb-3">
                                <img src="../assets/img/icons/unicons/wallet-info.png" alt="wallet info" class="rounded" />
                            </div>
                            <p class="mb-1">Total Sales</p>
                            <h4 class="mb-0">
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                            </h4>
                        </div>
                    </div>
                </div>

                <!-- Total Master Courses -->
                <div class="col-md-6 col-lg-3 mb-4">
                    <div class="card">
                        <div class="card-body text-center">
                            <div class="avatar mb-3">
                                <img src="../assets/img/icons/unicons/paypal.png" alt="paypal" class="rounded" />
                            </div>
                            <p class="mb-1">Total Master Courses</p>
                            <h4 class="mb-0">
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                            </h4>
                        </div>
                    </div>
                </div>

                <!-- Total Courses -->
                <div class="col-md-6 col-lg-3 mb-4">
                    <div class="card">
                        <div class="card-body text-center">
                            <div class="avatar mb-3">
                                <img src="../assets/img/icons/unicons/cc-primary.png" alt="Credit Card" class="rounded" />
                            </div>
                            <p class="mb-1">Total Courses</p>
                            <h4 class="mb-0">
                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                            </h4>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Most Popular Course -->
            <div class="row dashboard-section">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body text-center">
                            <h5 class="mb-3">Most Popular Course</h5>
                            <h4>
                                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                            </h4>
                        </div>
                      <div class="card-body text-center">
    <h5 class="mb-3">Total Videos</h5>
    <h4>
        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
    </h4>
</div>
                    </div>
                </div>
            </div>
        
            <!-- Monthly Sales Chart -->
            <div class="row dashboard-section">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="text-center mb-4">Monthly Sales</h5>
                            <canvas id="monthlySalesChart" height="200"></canvas>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Chart.js -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script>
  document.addEventListener("DOMContentLoaded", function () {
    const ctx = document.getElementById("monthlySalesChart").getContext("2d");

    // Replace this with the server-side data
    const salesData = <%= MonthlySalesData %>;
      const months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

      new Chart(ctx, {
        type: "bar",
        data: {
          labels: months,
          datasets: [
            {
              label: "Monthly Sales ($)",
              data: salesData,
              backgroundColor: "rgba(75, 192, 192, 0.6)",
              borderColor: "rgba(75, 192, 192, 1)",
              borderWidth: 1
            }
          ]
        },
        options: {
          responsive: true,
          scales: {
            x: { title: { display: true, text: "Months" } },
            y: { title: { display: true, text: "Sales ($)" }, beginAtZero: true }
          }
        }
      });
    });
</script>
</asp:Content>
