using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ELearning_Team3
{
  public partial class Certificates : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadCertificates();
      }
    }

    private void LoadCertificates()
    {
      // Fetch user email from session
      string userEmail = Session["MyUser"]?.ToString();
      if (string.IsNullOrEmpty(userEmail))
      {
        Response.Write("<script>alert('User email not found in session. Please log in again.');</script>");
        return;
      }

      // Fetch certificates from the database
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      try
      {
        using (SqlConnection conn = new SqlConnection(cs))
        {
          conn.Open();
          string query = "SELECT CertificateID, CertificatePath, GeneratedDate FROM Certificates WHERE UserEmail = @UserEmail";
          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            cmd.Parameters.AddWithValue("@UserEmail", userEmail);
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
              DataTable dt = new DataTable();
              adapter.Fill(dt);
              GridViewCertificates.DataSource = dt;
              GridViewCertificates.DataBind();
            }
          }
        }
      }
      catch (Exception ex)
      {
        Response.Write($"<script>alert('Error loading certificates: {ex.Message}');</script>");
      }
    }

    protected void GridViewCertificates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "View")
      {
        string certificatePath = e.CommandArgument.ToString();

        if (!string.IsNullOrEmpty(certificatePath) && System.IO.File.Exists(certificatePath))
        {
          Response.ContentType = "application/pdf";
          Response.AppendHeader("Content-Disposition", $"inline; filename={System.IO.Path.GetFileName(certificatePath)}");
          Response.TransmitFile(certificatePath);
          Response.End();
        }
        else
        {
          Response.Write("<script>alert('Certificate file not found.');</script>");
        }
      }
    }

  }
}
