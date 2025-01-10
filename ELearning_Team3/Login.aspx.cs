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
  public partial class Login : System.Web.UI.Page
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

      string UserEmail = TextBox1.Text;

      string Password = TextBox2.Text;


      string q = $"exec LoginProc '{UserEmail}','{Password}'";
      SqlCommand cmd = new SqlCommand(q, conn);
      SqlDataReader rdr = cmd.ExecuteReader();



      if (rdr.HasRows)
      {
        while (rdr.Read())
        {
          if ((rdr["UserEmail"].Equals(UserEmail) && rdr["Password"].Equals(Password)) && rdr["UserRole"].Equals("Admin"))
          {
            Session["MyUser"] = UserEmail;
            Response.Redirect("AdminDashBoard.aspx");
          }
          if (rdr["UserEmail"].Equals(UserEmail) && rdr["Password"].Equals(Password) && rdr["UserRole"].Equals("Student"))
          {
            Session["MyUser"] = UserEmail;
            Response.Redirect("UserAllCourses.aspx");
          }

        }
      }
      else
      {
        Response.Write("<script>alert('Invalid Email Or Password');</script>");

      }


    }
  }
}
