using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace ELearning_Team3
{
  public partial class AdminMCQ : System.Web.UI.Page
  {
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      try
      {
        conn = new SqlConnection(cs);
        conn.Open();

        if (!IsPostBack)
        {
          LoadUserReports();
        }
      }
      catch (Exception ex)
      {
        Response.Write($"<script>alert('Error initializing connection: {ex.Message}');</script>");
      }
    }

    private void LoadUserReports(string filterEmail = "")
    {
      try
      {
        string query = "SELECT UserEmail, TopicName, CorrectAnswers, WrongAnswers, CreatedAt FROM UserReports";

        if (!string.IsNullOrEmpty(filterEmail))
        {
          query += " WHERE UserEmail LIKE @FilterEmail";
        }

        query += " ORDER BY CreatedAt DESC";

        SqlCommand cmd = new SqlCommand(query, conn);
        if (!string.IsNullOrEmpty(filterEmail))
        {
          cmd.Parameters.AddWithValue("@FilterEmail", "%" + filterEmail + "%");
        }

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        GridViewUserReports.DataSource = dt;
        GridViewUserReports.DataBind();
      }
      catch (Exception ex)
      {
        Response.Write($"<script>alert('Error loading user reports: {ex.Message}');</script>");
      }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
      string filterEmail = txtFilterEmail.Text.Trim();
      LoadUserReports(filterEmail);
    }
  }
}
