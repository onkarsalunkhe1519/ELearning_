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
  public partial class Register : System.Web.UI.Page
  {
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
      conn = new SqlConnection(cs);
      conn.Open();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      string Name = TextBox1.Text;
      string UserEmail = TextBox2.Text;
      string Password = TextBox3.Text;
      string Contact = TextBox4.Text;
      string UserRole = "Student";
      string query = $"exec FetchUser '{UserEmail}'";
      SqlCommand ccmd = new SqlCommand(query, conn);
      SqlDataReader rdr = ccmd.ExecuteReader();
      if (rdr.HasRows)
      {
        Response.Write("<script> alert('UserEmail Already Register')</script> ");
      }
      else
      {
        string q = $"exec InsertUser '{Name}','{UserEmail}','{Password}','{Contact}','{UserRole}'";
        SqlCommand cmd = new SqlCommand(q, conn);
        cmd.ExecuteNonQuery();
        Session["MyUser"] = UserEmail;
        Response.Write("<script>alert('User Registered');window.location.href='Login.aspx'</script>");


      }
    }
  }
}
