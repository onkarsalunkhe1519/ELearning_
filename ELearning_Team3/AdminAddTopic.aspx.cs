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
  public partial class AdminAddTopic : System.Web.UI.Page
  {
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
      conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
      conn.Open();
      if (!IsPostBack)
      {
        PopulateCourses();
      }
    }

    private void PopulateCourses()
    {
      string query = "SELECT CourseName FROM Course";
      SqlCommand cmd = new SqlCommand(query, conn);

      
      SqlDataReader reader = cmd.ExecuteReader();

      DropDownList1.Items.Clear();
      DropDownList1.Items.Add(new ListItem("-- Select Course --", ""));

      while (reader.Read())
      {
        DropDownList1.Items.Add(new ListItem(reader["CourseName"].ToString(), reader["CourseName"].ToString()));
      }

      
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
      string courseName = DropDownList1.SelectedValue;
      string topicName = TextBox1.Text;
      string youtubeURL = TextBox2.Text;
      string q = $"INSERT INTO Topic (TopicName, CourseName, YoutubeURL) VALUES ('{topicName}', '{courseName}','{youtubeURL}')";
      SqlCommand cmd = new SqlCommand(q, conn);
      cmd.ExecuteNonQuery();
    }
    }
}
