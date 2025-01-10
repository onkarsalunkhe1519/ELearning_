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
  public partial class HelpSupportUser : System.Web.UI.Page
  {
    SqlConnection conn;
    protected void Page_Load(object sender, EventArgs e)
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
      conn = new SqlConnection(cs);
      conn.Open();
      if (!IsPostBack)
      {
        // Bind DropDownList
        BindPurchasedCourses();
        BindGridData();
      }
    }


    protected void BindPurchasedCourses()
    {
      string userEmail = Session["MyUser"].ToString(); // Assume user is logged in
      string query = "SELECT PurchaseID, CONCAT(MasterCourseName, ' - ', CourseName) AS DisplayName FROM Purchase WHERE UserEmail = @UserEmail";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@UserEmail", userEmail);

      DropDownList1.DataSource = cmd.ExecuteReader();
      DropDownList1.DataTextField = "DisplayName"; // Combined MasterCourseName and CourseName
      DropDownList1.DataValueField = "PurchaseID";
      DropDownList1.DataBind();

      DropDownList1.Items.Insert(0, new ListItem("--Select a Course--", ""));
    }
    protected void BindGridData()
    {
      string userEmail = Session["MyUser"].ToString(); // Assume user is logged in
      string query = "SELECT * FROM Support WHERE UserEmail = @UserEmail";


      SqlCommand cmd = new SqlCommand(query, conn);

      cmd.Parameters.AddWithValue("@UserEmail", userEmail);
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataTable dt = new DataTable();
      adapter.Fill(dt);

      GridView1.DataSource = dt;
      GridView1.DataBind();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        string userEmail = Session["MyUser"].ToString(); 
        int purchaseId = Convert.ToInt32(DropDownList1.SelectedValue); 
        string problemDescription = TextBox1.Text;

        string insertQuery = @"INSERT INTO Support (UserEmail, MasterCourseName, CourseName, ProblemDescription, Solution)
                                       SELECT @UserEmail, MasterCourseName, CourseName, @ProblemDescription, NULL
                                       FROM Purchase WHERE PurchaseID = @PurchaseID";

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
        {
          conn.Open();
          using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
          {
            cmd.Parameters.AddWithValue("@UserEmail", userEmail);
            cmd.Parameters.AddWithValue("@ProblemDescription", problemDescription);
            cmd.Parameters.AddWithValue("@PurchaseID", purchaseId);

            cmd.ExecuteNonQuery();
          }
        }

        // Refresh the GridView
        BindGridData();

        Response.Write("<script>alert('Your problem has been submitted successfully.');</script>");
      }
      catch (Exception ex)
      {
        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
      }
    }
  }
}
