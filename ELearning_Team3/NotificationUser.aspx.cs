using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELearning_Team3
{
  public partial class NotificationUser : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["MyUser"] != null)
        {
          string email = Session["MyUser"].ToString();
          LoadNotifications(email);
        }
        else
        {
          Response.Redirect("Login.aspx");
        }
      }
    }

    private void LoadNotifications(string email)
    {
      string query = "EXEC GetNotifications @Email";
      using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
      {
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@Email", email);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        RepeaterNotifications.DataSource = reader;
        RepeaterNotifications.DataBind();

        conn.Close();
      }
    }

  }
}
