using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace ELearning_Team3
{
  public partial class UserProfileUser : System.Web.UI.Page
  {
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
      conn = new SqlConnection(cs);

      if (!IsPostBack)
      {
        if (Session["MyUser"] == null)
        {
          Response.Redirect("Login.aspx");
          return;
        }

        string UserEmail = Session["MyUser"].ToString();
        LoadUserDetails(UserEmail);
      }
    }

    private void LoadUserDetails(string UserEmail)
    {
      string query = "SELECT Name, Contact, ProfileImg FROM Users WHERE UserEmail = @UserEmail";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@UserEmail", UserEmail);

      conn.Open();
      SqlDataReader reader = cmd.ExecuteReader();
      if (reader.Read())
      {
        TextBox1.Text = reader["Name"].ToString();
        TextBox4.Text = reader["Contact"].ToString();

        // Fetch and set the profile image
        string profileImgPath = reader["ProfileImg"] != DBNull.Value ? reader["ProfileImg"].ToString() : "~/assets/img/avatars/default.png";
        uploadedAvatar.ImageUrl = ResolveUrl(profileImgPath);
      }

      TextBox2.Text = UserEmail;
      TextBox2.Enabled = false;
      conn.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      if (Session["MyUser"] == null)
      {
        Response.Redirect("Login.aspx");
        return;
      }

      string userEmail = Session["MyUser"].ToString();
      string name = TextBox1.Text;
      string contact = TextBox4.Text;
      string password = TextBox3.Text;
      string profileImg = null;

      if (FileUpload1.HasFile)
      {
        // Ensure the directory exists
        string uploadDir = Server.MapPath("~/uploads/");
        if (!System.IO.Directory.Exists(uploadDir))
        {
          System.IO.Directory.CreateDirectory(uploadDir);
        }

        
        profileImg = "~/uploads/" + FileUpload1.FileName;
        FileUpload1.SaveAs(Server.MapPath(profileImg));
      }

      string query = "UPDATE Users SET Name = @Name, Contact = @Contact, Password = @Password, ProfileImg = @ProfileImg WHERE UserEmail = @UserEmail";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@Name", name);
      cmd.Parameters.AddWithValue("@Contact", contact);
      cmd.Parameters.AddWithValue("@Password", password);
      cmd.Parameters.AddWithValue("@ProfileImg", profileImg ?? (object)DBNull.Value);
      cmd.Parameters.AddWithValue("@UserEmail", userEmail);

      conn.Open();
      cmd.ExecuteNonQuery();
      conn.Close();

     
      LoadUserDetails(userEmail);
    }

  }
}
