using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELearning_Team3
{
  public partial class ViewCart : Page
  {
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
      // Initialize the connection string
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ToString();
      conn = new SqlConnection(cs);

      if (!IsPostBack) // Load data only on the first page load
      {
        LoadCart();
      }
    }

    private void LoadCart()
    {
      if (Session["MyUser"] == null)
      {
        Response.Redirect("Login.aspx");
        return;
      }

      string userEmail = Session["MyUser"].ToString();

      // SQL query to fetch cart items for the logged-in user
      string query = "SELECT CourseName, MasterCourseName, Price FROM Cart WHERE UserEmail = @UserEmail";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@UserEmail", userEmail);

      try
      {
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        // Bind the fetched data to the GridView
        GridViewCart.DataSource = reader;
        GridViewCart.DataBind();

        conn.Close();

        // Calculate the total price of items in the cart
        CalculateTotalPrice(userEmail);
      }
      catch (Exception ex)
      {
        Response.Write("<script>alert('Error loading cart: " + ex.Message + "');</script>");
      }
      finally
      {
        if (conn.State == System.Data.ConnectionState.Open)
        {
          conn.Close();
        }
      }
    }

    private void CalculateTotalPrice(string userEmail)
    {
      string query = "SELECT SUM(Price) AS TotalPrice FROM Cart WHERE UserEmail = @UserEmail";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@UserEmail", userEmail);

      try
      {
        conn.Open();
        object result = cmd.ExecuteScalar();
        conn.Close();

        decimal totalPrice = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        LabelTotalPrice.Text = "Total Price: $" + totalPrice.ToString("N2");
      }
      catch (Exception ex)
      {
        Response.Write("<script>alert('Error calculating total price: " + ex.Message + "');</script>");
      }
    }

    protected void GridViewCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      if (Session["MyUser"] == null)
      {
        Response.Redirect("Login.aspx");
        return;
      }

      string userEmail = Session["MyUser"].ToString();
      string courseName = GridViewCart.DataKeys[e.RowIndex].Value.ToString();

      string query = "DELETE FROM Cart WHERE UserEmail = @UserEmail AND CourseName = @CourseName";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@UserEmail", userEmail);
      cmd.Parameters.AddWithValue("@CourseName", courseName);

      try
      {
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        LoadCart(); // Refresh the cart
      }
      catch (Exception ex)
      {
        Response.Write("<script>alert('Error deleting item: " + ex.Message + "');</script>");
      }
    }

    protected void ButtonBuyAll_Click(object sender, EventArgs e)
    {
      if (Session["MyUser"] == null)
      {
        Response.Redirect("Login.aspx");
        return;
      }

      string userEmail = Session["MyUser"].ToString();

      string queryGetCart = "SELECT CourseName, MasterCourseName, Price FROM Cart WHERE UserEmail = @UserEmail";
      SqlCommand cmdGetCart = new SqlCommand(queryGetCart, conn);
      cmdGetCart.Parameters.AddWithValue("@UserEmail", userEmail);

      try
      {
        conn.Open();
        SqlDataReader reader = cmdGetCart.ExecuteReader();
        var cartItems = new System.Collections.Generic.List<Tuple<string, string, decimal>>();

        while (reader.Read())
        {
          cartItems.Add(new Tuple<string, string, decimal>(
              reader["CourseName"].ToString(),
              reader["MasterCourseName"]?.ToString(),
              Convert.ToDecimal(reader["Price"])
          ));
        }

        reader.Close();

        foreach (var item in cartItems)
        {
          string queryInsertPurchase = @"
                        INSERT INTO Purchase (UserEmail, CourseName, MasterCourseName, PurchaseType, Price)
                        VALUES (@UserEmail, @CourseName, @MasterCourseName, @PurchaseType, @Price)";
          SqlCommand cmdInsert = new SqlCommand(queryInsertPurchase, conn);
          cmdInsert.Parameters.AddWithValue("@UserEmail", userEmail);
          cmdInsert.Parameters.AddWithValue("@CourseName", (object)item.Item1 ?? DBNull.Value);
          cmdInsert.Parameters.AddWithValue("@MasterCourseName", (object)item.Item2 ?? DBNull.Value);
          cmdInsert.Parameters.AddWithValue("@PurchaseType", item.Item2 == null ? "Course" : "MasterCourse");
          cmdInsert.Parameters.AddWithValue("@Price", item.Item3);

          cmdInsert.ExecuteNonQuery();
        }

        string queryClearCart = "DELETE FROM Cart WHERE UserEmail = @UserEmail";
        SqlCommand cmdClearCart = new SqlCommand(queryClearCart, conn);
        cmdClearCart.Parameters.AddWithValue("@UserEmail", userEmail);
        cmdClearCart.ExecuteNonQuery();

        conn.Close();

        Response.Write("<script>alert('Purchase successful!');</script>");
        LoadCart(); // Reload the cart to reflect the changes
      }
      catch (Exception ex)
      {
        Response.Write("<script>alert('Error processing purchase: " + ex.Message + "');</script>");
      }
    }
  }
}
