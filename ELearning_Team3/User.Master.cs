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
    public partial class User : System.Web.UI.MasterPage
    {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["MyUser"] != null)
        {
          string email = Session["MyUser"].ToString();
          GetUnreadNotificationCount(email);
          LoadNotifications(email);
        }
      }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      Session["MyUser"] = null;
      Response.Redirect("Login.aspx");
    }
    private void GetUnreadNotificationCount(string email)
    {
      string query = "EXEC GetUnreadNotificationCount @Email";
      using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
      {
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@Email", email);

        conn.Open();
        int unreadCount = (int)cmd.ExecuteScalar();
        conn.Close();

        // Update the label for unread notification count
        NotificationCountLabel.Text = unreadCount.ToString();
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

        NotificationDropDownList.Items.Clear(); // Clear existing items
        NotificationDropDownList.Items.Add(new ListItem("Notifications", "")); // Add a default placeholder

        while (reader.Read())
        {
          string notificationText = reader["NotificationText"].ToString();
          string notificationDate = Convert.ToDateTime(reader["SentAt"]).ToString("g");
          string isRead = (bool)reader["IsRead"] ? "[Read]" : "[Unread]";
          string displayText = $"{isRead} {notificationText} - {notificationDate}";

          NotificationDropDownList.Items.Add(new ListItem(displayText, reader["NotificationID"].ToString()));
        }

        conn.Close();
      }
    }

    protected void MarkAllReadButton_Click(object sender, EventArgs e)
    {
      if (Session["MyUser"] != null)
      {
        string email = Session["MyUser"].ToString();
        string query = "EXEC MarkNotificationsAsRead @Email";
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
        {
          SqlCommand cmd = new SqlCommand(query, conn);
          cmd.Parameters.AddWithValue("@Email", email);

          conn.Open();
          cmd.ExecuteNonQuery();
          conn.Close();
        }

        // Refresh the notification panel
        GetUnreadNotificationCount(email);
        LoadNotifications(email);
      }
    }
    protected void NotificationDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (NotificationDropDownList.SelectedValue != "")
      {
        string notificationId = NotificationDropDownList.SelectedValue;

        string query = "UPDATE Notifications SET IsRead = 1 WHERE NotificationID = @NotificationID";
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
        {
          SqlCommand cmd = new SqlCommand(query, conn);
          cmd.Parameters.AddWithValue("@NotificationID", notificationId);

          conn.Open();
          cmd.ExecuteNonQuery();
          conn.Close();
        }

        // Refresh notifications
        string email = Session["MyUser"].ToString();
        GetUnreadNotificationCount(email);
        LoadNotifications(email);
      }
    }


  }
}
