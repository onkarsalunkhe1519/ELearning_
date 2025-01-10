using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELearning_Team3
{
  public partial class UserPurchase : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        BindGridView();
      }
    }

    private void BindGridView()
    {
      // Get the logged-in user's email from the session
      string userEmail = Session["MyUser"] as string;

      if (string.IsNullOrEmpty(userEmail))
      {
        Response.Redirect("Login.aspx"); // Redirect to login if session is null
        return;
      }

      string connString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      using (SqlConnection conn = new SqlConnection(connString))
      {
        string query = "SELECT PurchaseID, MasterCourseName, CourseName, PurchaseType, Price, PurchaseDate FROM Purchase WHERE UserEmail = @UserEmail";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
          cmd.Parameters.AddWithValue("@UserEmail", userEmail);

          using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
          {
            DataTable dt = new DataTable();
            sda.Fill(dt);

            gvPurchases.DataSource = dt;
            gvPurchases.DataBind();
          }
        }
      }
    }
  }
}
