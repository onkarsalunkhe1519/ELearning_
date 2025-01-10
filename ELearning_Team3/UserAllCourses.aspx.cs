using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Razorpay.Api;

namespace ELearning_Team3
{
  public partial class UserAllCourses : System.Web.UI.Page
  {
    SqlConnection conn;
    protected void Page_Load(object sender, EventArgs e)
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ToString();
      conn = new SqlConnection(cs);

      if (!IsPostBack) // Populate dropdown only on initial page load
      {
        PopulateMasterCourseDropdown();
      }
    }

    // Populate the dropdown list with master course names
    protected void PopulateMasterCourseDropdown()
    {
      string query = "SELECT MasterCourseName FROM MasterCourses";
      SqlCommand cmd = new SqlCommand(query, conn);

      try
      {
        conn.Open();
        DropDownListMasterCourse.DataSource = cmd.ExecuteReader();
        DropDownListMasterCourse.DataTextField = "MasterCourseName";
        DropDownListMasterCourse.DataValueField = "MasterCourseName";
        DropDownListMasterCourse.DataBind();
        conn.Close();

        // Add a default item
        DropDownListMasterCourse.Items.Insert(0, new ListItem("--Select a Master Course--", ""));
      }
      catch (Exception ex)
      {
        // Handle exceptions (e.g., logging)
        Response.Write("<script>alert('Error loading dropdown: " + ex.Message + "');</script>");
      }
      finally
      {
        if (conn.State == System.Data.ConnectionState.Open)
        {
          conn.Close();
        }
      }
    }

    // Fetch courses based on the selected master course
    protected void ButtonFetch_Click(object sender, EventArgs e)
    {
      string selectedMasterCourse = DropDownListMasterCourse.SelectedValue;

      if (!string.IsNullOrEmpty(selectedMasterCourse))
      {
        // Query for individual course details
        string query = $"EXEC CourseList1 '{selectedMasterCourse}'";
        SqlCommand cmd = new SqlCommand(query, conn);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        RepeaterCourses.DataSource = reader;
        RepeaterCourses.DataBind();
        reader.Close();

        // Query for aggregated master course data
        string queryMasterCourse = @"
                    SELECT 
                        mc.MasterCourseName,
                        STRING_AGG(c.CourseName, ', ') AS CourseNames, 
                        STRING_AGG(c.Description, ', ') AS Descriptions,
                        SUM(c.Price) AS TotalPrice,
                        mc.Pic
                    FROM 
                        Course c
                    JOIN 
                        MasterCourses mc ON c.MasterCourseName = mc.MasterCourseName
                    WHERE 
                        mc.MasterCourseName = @MasterCourseName
                    GROUP BY 
                        mc.MasterCourseName, mc.Pic
                ";
        SqlCommand cmdMaster = new SqlCommand(queryMasterCourse, conn);
        cmdMaster.Parameters.AddWithValue("@MasterCourseName", selectedMasterCourse);

        SqlDataReader readerMaster = cmdMaster.ExecuteReader();
        RepeaterMasterCourse.DataSource = readerMaster;
        RepeaterMasterCourse.DataBind();
        conn.Close();
      }
      else
      {
        Response.Write("<script>alert('Please select a master course.');</script>");
      }
    }
    protected void AddToCart(string itemName, decimal price, string itemType)
    {
      if (Session["MyUser"] != null)
      {
        string userEmail = Session["MyUser"].ToString();

        string query = "INSERT INTO Cart (UserEmail, ItemType, ItemName, Price) VALUES (@UserEmail, @ItemType, @ItemName, @Price)";
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ToString()))
        {
          SqlCommand cmd = new SqlCommand(query, conn);
          cmd.Parameters.AddWithValue("@UserEmail", userEmail);
          cmd.Parameters.AddWithValue("@ItemType", itemType); // 'Course' or 'MasterCourse'
          cmd.Parameters.AddWithValue("@ItemName", itemName);
          cmd.Parameters.AddWithValue("@Price", price);

          conn.Open();
          cmd.ExecuteNonQuery();
          conn.Close();
        }

        Response.Write("<script>alert('Item added to cart successfully!');</script>");
      }
      else
      {
        Response.Redirect("Login.aspx");
      }
    }

    protected void ButtonBuyNow_Command(object sender, CommandEventArgs e)
    {
      // Extract course/master course name and price from the CommandArgument
      string[] args = e.CommandArgument.ToString().Split('|');
      string itemName = args[0];
      decimal price = Convert.ToDecimal(args[1]);

      // Check if user is logged in
      if (Session["MyUser"] != null)
      {
        string userEmail = Session["MyUser"].ToString();

        // Create Razorpay Order
        string orderId = CreateRazorpayOrder(price);

        if (!string.IsNullOrEmpty(orderId))
        {
          // Redirect to Razorpay Checkout
          Response.Redirect($"RazorpayCheckout.aspx?orderId={orderId}&itemName={itemName}&price={price}");
        }
        else
        {
          Response.Write("<script>alert('Error creating Razorpay order.');</script>");
        }
      }
      else
      {
        Response.Redirect("Login.aspx");
      }
    }
    private string CreateRazorpayOrder(decimal amount)
    {
      try
      {
        // Fetch Razorpay API keys from configuration
        string key = "rzp_test_Kl7588Yie2yJTV";
        string secret = "6dN9Nqs7M6HPFMlL45AhaTgp";

        // Initialize Razorpay client
        RazorpayClient client = new RazorpayClient(key, secret);

        // Create order options
        Dictionary<string, object> options = new Dictionary<string, object>
                {
                    { "amount", (int)(amount * 100) }, // Amount in paise
                    { "currency", "INR" },
                    { "receipt", "order_rcptid_11" },
                    { "payment_capture", 1 } // Auto capture payment
                };

        // Create order
        Order order = client.Order.Create(options);
        return order["id"].ToString();
      }
      catch (Exception ex)
      {
        // Log error and return null
        Console.WriteLine("Razorpay Order Error: " + ex.Message);
        return null;
      }
    }


  }
}
