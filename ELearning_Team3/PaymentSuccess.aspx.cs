using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Razorpay.Api;

namespace ELearning_Team3
{
  public partial class PaymentSuccess : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (!string.IsNullOrEmpty(Request.QueryString["paymentId"]) &&
            !string.IsNullOrEmpty(Request.QueryString["itemName"]) &&
            !string.IsNullOrEmpty(Request.QueryString["price"]))
        {
          string paymentId = Request.QueryString["paymentId"];
          string itemName = Request.QueryString["itemName"];
          decimal price;

          if (!decimal.TryParse(Request.QueryString["price"], out price))
          {
            Response.Write("<script>alert('Invalid price value.');</script>");
            return;
          }

          if (Session["MyUser"] != null)
          {
            string userEmail = Session["MyUser"].ToString();

            // Populate the literals
            litPaymentId.Text = paymentId;
            litItemName.Text = itemName;
            litPrice.Text = price.ToString("C");
            litUserEmail.Text = userEmail;
            litPurchaseDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");

            // Save purchase data to the database
            SavePurchaseToDatabase(userEmail, itemName, price);

            // Generate the PDF invoice
            string pdfPath = Server.MapPath($"~/Invoices/Invoice_{paymentId}.pdf");
            GeneratePdfInvoice(paymentId, itemName, price, userEmail, pdfPath);

            // Link the download button
            downloadBtn.HRef = $"~/Invoices/Invoice_{paymentId}.pdf";

            // Send the email with the PDF attached
            SendEmail(userEmail, pdfPath);
          }
          else
          {
            Response.Redirect("Login.aspx");
          }
        }
        else
        {
          Response.Write("<script>alert('Invalid payment data.');</script>");
        }
      }
    }

    private void SavePurchaseToDatabase(string userEmail, string itemName, decimal price)
    {
      bool isMasterCourse = IsMasterCourse(itemName);
      string purchaseType = isMasterCourse ? "MasterCourse" : "Course";

      string query = isMasterCourse
          ? @"INSERT INTO Purchase (UserEmail, MasterCourseName, PurchaseType, Price, PurchaseDate)
           VALUES (@UserEmail, @ItemName, @PurchaseType, @Price, GETDATE())"
          : @"INSERT INTO Purchase (UserEmail, CourseName, PurchaseType, Price, PurchaseDate)
           VALUES (@UserEmail, @ItemName, @PurchaseType, @Price, GETDATE())";

      try
      {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ToString()))
        {
          SqlCommand cmd = new SqlCommand(query, conn);
          cmd.Parameters.AddWithValue("@UserEmail", userEmail);
          cmd.Parameters.AddWithValue("@ItemName", itemName);
          cmd.Parameters.AddWithValue("@PurchaseType", purchaseType);
          cmd.Parameters.AddWithValue("@Price", price);

          conn.Open();
          cmd.ExecuteNonQuery();
          conn.Close();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error saving purchase to database: {ex.Message}");
      }
    }


    private void GeneratePdfInvoice(string paymentId, string itemName, decimal price, string userEmail, string pdfPath)
    {
      // Ensure the directory exists
      string directoryPath = Path.GetDirectoryName(pdfPath);
      if (!Directory.Exists(directoryPath))
      {
        Directory.CreateDirectory(directoryPath);
      }

      // Generate the PDF
      Document document = new Document(PageSize.A4);
      PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
      document.Open();

      document.Add(new Paragraph("E-Learning Platform Invoice"));
      document.Add(new Paragraph("---------------------------"));
      document.Add(new Paragraph($"Payment ID: {paymentId}"));
      document.Add(new Paragraph($"User Email: {userEmail}"));
      document.Add(new Paragraph($"Item Purchased: {itemName}"));
      document.Add(new Paragraph($"Price: {price:C}"));
      document.Add(new Paragraph($"Purchase Date: {DateTime.Now}"));
      document.Add(new Paragraph("---------------------------"));
      document.Add(new Paragraph("Thank you for your purchase!"));

      document.Close();
    }



    private bool IsMasterCourse(string itemName)
    {
      string query = "SELECT COUNT(*) FROM MasterCourses WHERE MasterCourseName = @MasterCourseName";

      try
      {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ToString()))
        {
          SqlCommand cmd = new SqlCommand(query, conn);
          cmd.Parameters.AddWithValue("@MasterCourseName", itemName);

          conn.Open();
          int count = (int)cmd.ExecuteScalar();
          conn.Close();

          return count > 0;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error checking MasterCourse: {ex.Message}");
        return false;
      }
    }

    private string GenerateInvoice(string paymentId, string itemName, decimal price, string userEmail)
    {
      StringBuilder invoiceBuilder = new StringBuilder();

      invoiceBuilder.AppendLine("E-Learning Platform Invoice");
      invoiceBuilder.AppendLine("---------------------------");
      invoiceBuilder.AppendLine($"Payment ID: {paymentId}");
      invoiceBuilder.AppendLine($"User Email: {userEmail}");
      invoiceBuilder.AppendLine($"Item Purchased: {itemName}");
      invoiceBuilder.AppendLine($"Price: {price:C}");
      invoiceBuilder.AppendLine($"Purchase Date: {DateTime.Now}");
      invoiceBuilder.AppendLine("---------------------------");
      invoiceBuilder.AppendLine("Thank you for your purchase!");

      return invoiceBuilder.ToString();
    }

    private void SendEmail(string email, string pdfPath)
    {
      try
      {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("onkarsalunkhe1519@gmail.com");
        mail.To.Add(email);
        mail.Subject = "Your Purchase Invoice";
        mail.Body = "Dear User,\n\nThank you for your purchase! Please find your invoice attached.\n\nBest regards,\nE-Learning Team";

        // Attach the PDF invoice
        mail.Attachments.Add(new Attachment(pdfPath));

        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        smtp.Credentials = new NetworkCredential("onkarsalunkhe1519@gmail.com", "hxwwwrzpuaxpzwpb");
        smtp.Port = 587;
        smtp.EnableSsl = true;

        smtp.Send(mail);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error sending email: {ex.Message}");
      }
    }

  }
}
