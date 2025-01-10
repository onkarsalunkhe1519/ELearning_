<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentSuccess.aspx.cs" Inherits="ELearning_Team3.PaymentSuccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Success</title>
    <style>
        body {
            font-family: 'Roboto', sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
            color: #333;
        }
        .invoice-container {
            max-width: 700px;
            margin: 50px auto;
            background: #fff;
            border: 1px solid #ccc;
            border-radius: 10px;
            padding: 30px;
            box-shadow: 0 10px 15px rgba(0, 0, 0, 0.1);
        }
        .invoice-header {
            text-align: center;
            margin-bottom: 30px;
        }
        .invoice-header h1 {
            margin: 0;
            font-size: 28px;
            color: #007bff;
        }
        .invoice-header h2 {
            margin: 0;
            font-size: 20px;
            color: #555;
        }
        .invoice-header img {
            width: 200px;
            margin-bottom: 10px;
        }
        .invoice-body {
            padding: 20px;
            border-top: 1px solid #eee;
            border-bottom: 1px solid #eee;
        }
        .invoice-body p {
            margin: 10px 0;
            font-size: 16px;
        }
        
        .invoice-body .item {
            display: flex;
            justify-content: space-between;
        }
        .invoice-body .item strong {
            font-weight: bold;
        }
        .invoice-footer {
            text-align: center;
            margin-top: 20px;
            font-size: 14px;
            color: #777;
        }
        .button-container {
            text-align: center;
            margin-top: 30px;
        }
        .btn {
            display: inline-block;
            margin: 5px;
            padding: 10px 20px;
            background: #007bff;
            color: #ffffff;
            text-decoration: none;
            font-size: 16px;
            border-radius: 25px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        .btn:hover {
            background: #0056b3;
            box-shadow: 0 6px 8px rgba(0, 0, 0, 0.15);
        }
        .btn-print {
            background: #28a745;
        }
        .btn-print:hover {
            background: #218838;
        }
        .btn-download {
            background: #ffc107;
            color: #333;
        }
        .btn-download:hover {
            background: #e0a800;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="invoice-container">
            <div class="invoice-header">
                <img src="https://masstechbusiness.com/wp-content/uploads/2021/12/Masstech-Logo-1536x331.png" alt="E-Learning Logo" />
                
                <h2>Invoice</h2>
                <p id="date"><strong>Date:</strong> <asp:Literal ID="litPurchaseDate" runat="server"></asp:Literal></p>
            </div>
            <div class="invoice-body">
                <div class="item">
                    <p><strong>Payment ID:</strong></p>
                    <p><asp:Literal ID="litPaymentId" runat="server"></asp:Literal></p>
                </div>
                <div class="item">
                    <p><strong>User Email:</strong></p>
                    <p><asp:Literal ID="litUserEmail" runat="server"></asp:Literal></p>
                </div>
                <div class="item">
                    <p><strong>Item Purchased:</strong></p>
                    <p><asp:Literal ID="litItemName" runat="server"></asp:Literal></p>
                </div>
                <div class="item">
                    <p><strong>Price:</strong></p>
                    <p><asp:Literal ID="litPrice" runat="server"></asp:Literal></p>
                </div>
            </div>
            <div class="invoice-footer">
                <p>Thank you for your purchase!</p>
                <p>For any queries, contact us at <strong>support@elearning.com</strong></p>
            </div>
            <div class="button-container">
                <a href="MyCourses.aspx" class="btn">Go to My Courses</a>
                <a id="downloadBtn" runat="server" class="btn btn-download">Download Invoice</a>
                <button onclick="window.print()" class="btn btn-print">Print Invoice</button>
            </div>
        </div>
    </form>
</body>
</html>
