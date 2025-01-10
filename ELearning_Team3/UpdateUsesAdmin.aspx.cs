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
  public partial class UpdateUsesAdmin : System.Web.UI.Page
  {
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
      conn = new SqlConnection(cs);
      conn.Open();

      if (!IsPostBack)
      {
        LoadUsers();
      }
    }

    private void LoadUsers()
    {
      string query = "SELECT UserEmail, Name, Contact, UserStatus FROM Users";
      SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
      DataTable dt = new DataTable();
      adapter.Fill(dt);

      gvUsers.DataSource = dt;
      gvUsers.DataBind();
    }



    protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int index;
      if (int.TryParse(e.CommandArgument.ToString(), out index))
      {
        string userEmail = gvUsers.DataKeys[index].Value.ToString();

        if (e.CommandName == "ViewUser")
        {
          ShowUserDetails(userEmail, editable: false);
        }
        else if (e.CommandName == "EditUser")
        {
          ShowUserDetails(userEmail, editable: true);
        }
        else if (e.CommandName == "ActivateUser")
        {
          UpdateUserStatus(userEmail, "Active");
        }
        else if (e.CommandName == "DeactivateUser")
        {
          UpdateUserStatus(userEmail, "Inactive");
        }
      }
    }


    private void ShowUserDetails(string userEmail, bool editable = false)
    {
      string query = "SELECT * FROM Users WHERE UserEmail = @UserEmail";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@UserEmail", userEmail);

      SqlDataReader reader = cmd.ExecuteReader();
      if (reader.Read())
      {
        txtEmail.Text = reader["UserEmail"].ToString();
        txtName.Text = reader["Name"].ToString();
        txtContact.Text = reader["Contact"].ToString();
        txtPassword.Text = reader["Password"].ToString();
        txtRole.Text = reader["UserRole"].ToString();
        ddlStatus.SelectedValue = reader["UserStatus"].ToString();
      }
      reader.Close();

      UserDetailsPanel.Visible = true;

      txtEmail.Enabled = editable;
      txtName.Enabled = editable;
      txtContact.Enabled = editable;
      txtPassword.Enabled = editable;
      txtRole.Enabled = editable;
      ddlStatus.Enabled = editable;

      btnSave.Enabled = editable; // Disable save button in view mode
    }



    private void UpdateUserStatus(string userEmail, string status)
    {
      string query = "UPDATE Users SET UserStatus = @UserStatus WHERE UserEmail = @UserEmail";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@UserStatus", status);
      cmd.Parameters.AddWithValue("@UserEmail", userEmail);
      cmd.ExecuteNonQuery();

      LoadUsers();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
      string email = txtEmail.Text;
      string name = txtName.Text;
      string contact = txtContact.Text;
      string password = txtPassword.Text;
      string role = txtRole.Text;

      string profileImg = null;
      if (FileUpload1.HasFile)
      {
        profileImg = "/uploads/" + FileUpload1.FileName;
        FileUpload1.SaveAs(Server.MapPath(profileImg));
      }

      string query = "UPDATE Users SET Name = @Name, Contact = @Contact, Password = @Password, " +
                     "UserRole = @UserRole, UserStatus = @UserStatus, ProfileImg = @ProfileImg " +
                     "WHERE UserEmail = @UserEmail";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@Name", name);
      cmd.Parameters.AddWithValue("@Contact", contact);
      cmd.Parameters.AddWithValue("@Password", password);
      cmd.Parameters.AddWithValue("@UserRole", role);
      cmd.Parameters.AddWithValue("@UserStatus", ddlStatus.SelectedValue);
      cmd.Parameters.AddWithValue("@ProfileImg", profileImg ?? (object)DBNull.Value);
      cmd.Parameters.AddWithValue("@UserEmail", email);

      cmd.ExecuteNonQuery();
      LoadUsers();

      UserDetailsPanel.Visible = false;
    }
  }
}
