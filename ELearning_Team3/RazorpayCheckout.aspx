<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RazorpayCheckout.aspx.cs" Inherits="ELearning_Team3.RazorpayCheckout" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Razorpay Payment</title>
    <script src="https://checkout.razorpay.com/v1/checkout.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <script>
          document.addEventListener("DOMContentLoaded", function () {
            var options = {
              "key": "<%= ConfigurationManager.AppSettings["RazorpayKey"] %>",
                  "amount": <%= Convert.ToDecimal(Request.QueryString["price"]) * 100 %>, // Amount in paise
                  "currency": "INR",
                  "name": "E-Learning Platform",
                  "description": "<%= Request.QueryString["itemName"] %>",
                    "order_id": "<%= Request.QueryString["orderId"] %>", // Razorpay Order ID
                    "handler": function (response) {
                        // Redirect to success page with payment details
                        window.location.href = "PaymentSuccess.aspx?paymentId=" + response.razorpay_payment_id +
                            "&itemName=" + encodeURIComponent("<%= Request.QueryString["itemName"] %>") +
                            "&price=" + "<%= Request.QueryString["price"] %>";
                },
                "theme": {
                  "color": "#007bff"
                }
              };

              var rzp = new Razorpay(options);
              rzp.open();
            });
        </script>
    </form>
</body>
</html>
