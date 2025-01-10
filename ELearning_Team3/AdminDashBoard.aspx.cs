using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELearning_Team3
{
  public partial class AdminDashBoard : System.Web.UI.Page
  {
    public string MonthlySalesData { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {

      if (!IsPostBack)
      {
        LoadActiveUsers();
        LoadTotalPrice();
        LoadMasterCourseCount();
        LoadCourseCount();
        LoadMostPurchasedCourse();
        LoadMonthlySalesData();
        LoadYoutubeURLCount();

      }
    }
    private void LoadYoutubeURLCount()
    {
      string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      try
      {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
          conn.Open();
          string query = "SELECT COUNT(YoutubeURL) FROM Topic WHERE YoutubeURL IS NOT NULL";

          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            int count = (int)cmd.ExecuteScalar();
            Label6.Text = count.ToString(); // Set the count to Label6
          }
        }
      }
      catch (Exception ex)
      {
        Label6.Text = "Error"; // In case of an error, display "Error"
        Response.Write($"<script>alert('Error fetching Youtube URL count: {ex.Message}');</script>");
      }
    }
    private void LoadMonthlySalesData()
    {
      string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      try
      {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
          conn.Open();
          string query = @"
                        SELECT 
                            MONTH(PurchaseDate) AS Month, 
                            SUM(Price) AS TotalSales 
                        FROM Purchase
                        GROUP BY MONTH(PurchaseDate)
                        ORDER BY MONTH(PurchaseDate)";

          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
              var salesData = new int[12];

              while (reader.Read())
              {
                int month = reader.GetInt32(0); // Month (1-12)
                decimal totalSales = reader.GetDecimal(1); // Total Sales
                salesData[month - 1] = (int)totalSales;
              }

              // Serialize data for JavaScript
              MonthlySalesData = new JavaScriptSerializer().Serialize(salesData);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Response.Write($"<script>alert('Error fetching sales data: {ex.Message}');</script>");
      }
    }
    private void LoadSalesData()
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      try
      {
        using (SqlConnection conn = new SqlConnection(cs))
        {
          conn.Open();
          string query = @"
                        SELECT 
                            MONTH(PurchaseDate) AS Month, 
                            SUM(Price) AS TotalSales
                        FROM Purchase
                        WHERE YEAR(PurchaseDate) = @Year
                        GROUP BY MONTH(PurchaseDate)
                        ORDER BY MONTH(PurchaseDate)";

          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);
            SqlDataReader reader = cmd.ExecuteReader();

            List<object> salesData = new List<object>();
            while (reader.Read())
            {
              salesData.Add(new
              {
                Month = reader.GetInt32(0),
                TotalSales = reader.GetDecimal(1)
              });
            }

            // Serialize data for Chart.js
            var serializer = new JavaScriptSerializer();
            string jsonData = serializer.Serialize(salesData);

            // Pass data to the frontend
            ClientScript.RegisterStartupScript(this.GetType(), "salesData", $"var salesData = {jsonData};", true);
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error loading sales data: {ex.Message}");
      }
    }
    private void LoadMostPurchasedCourse()
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      try
      {
        using (SqlConnection conn = new SqlConnection(cs))
        {
          conn.Open();
          string query = @"
                        SELECT TOP 1 CourseName
                        FROM Purchase
                        GROUP BY CourseName
                        ORDER BY COUNT(*) DESC";

          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
              string mostPurchasedCourse = result.ToString();
              Label5.Text = $"{mostPurchasedCourse}";
            }
            else
            {
              Label5.Text = "None";
            }
          }
        }
      }
      catch (Exception ex)
      {
        Label5.Text = "Error fetching most purchased course.";
        // Log the exception for debugging
        Console.WriteLine(ex.Message);
      }
    }
    private void LoadCourseCount()
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      try
      {
        using (SqlConnection conn = new SqlConnection(cs))
        {
          conn.Open();
          string query = "SELECT COUNT(*) FROM Course";
          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            int courseCount = (int)cmd.ExecuteScalar();
            Label4.Text = $"{courseCount}";
          }
        }
      }
      catch (Exception ex)
      {
        Label4.Text = "Error fetching course count.";
        // Log the exception for debugging
        Console.WriteLine(ex.Message);
      }
    }
    private void LoadMasterCourseCount()
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      try
      {
        using (SqlConnection conn = new SqlConnection(cs))
        {
          conn.Open();
          string query = "SELECT COUNT(*) FROM MasterCourses";
          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            int masterCourseCount = (int)cmd.ExecuteScalar();
            Label3.Text = $"{masterCourseCount}";
          }
        }
      }
      catch (Exception ex)
      {
        Label3.Text = "Error fetching master course count.";
        // Log the exception for debugging
        Console.WriteLine(ex.Message);
      }
    }
    private void LoadTotalPrice()
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      try
      {
        using (SqlConnection conn = new SqlConnection(cs))
        {
          conn.Open();
          string query = "SELECT SUM(Price) FROM Purchase";
          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            object result = cmd.ExecuteScalar();
            decimal totalPrice = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            Label2.Text = $"  ${totalPrice:F2}";
          }
        }
      }
      catch (Exception ex)
      {
        Label2.Text = "Error fetching total price.";
        // Log the exception for debugging
        Console.WriteLine(ex.Message);
      }
    }
    private void LoadActiveUsers()
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      try
      {
        using (SqlConnection conn = new SqlConnection(cs))
        {
          conn.Open();
          string query = "SELECT COUNT(*) FROM Users";
          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            int activeUsersCount = (int)cmd.ExecuteScalar();
            Label1.Text = $"{activeUsersCount}";
          }
        }
      }
      catch (Exception ex)
      {
        Label1.Text = "Error fetching active users.";
        // Log the exception for debugging
        Console.WriteLine(ex.Message);
      }
    }
  }
}
