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
  public partial class AdminReviews : System.Web.UI.Page
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
      // Get the connection string from Web.config
      string connString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      using (SqlConnection conn = new SqlConnection(connString))
      {
        string query = "SELECT ReviewID, Rating, ReviewText, CourseName, UserEmail, CreatedDate FROM Reviews";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
          using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
          {
            DataTable dt = new DataTable();
            sda.Fill(dt);

            gvReviews.DataSource = dt;
            gvReviews.DataBind();
          }
        }
      }
    }
  }
}
