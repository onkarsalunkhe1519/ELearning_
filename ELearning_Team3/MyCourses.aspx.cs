using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELearning_Team3
{
  public partial class MyCourses : System.Web.UI.Page
  {
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
      conn = new SqlConnection(cs);
      conn.Open();

      if (!IsPostBack)
      {
        Fetcourse();
      }
    }

    private void Fetcourse()
    {
      string Em = Session["MyUser"].ToString();

      string query = @"
        SELECT 
            UserEmail, 
            MasterCourseName, 
            CourseName,
            CASE 
                WHEN DATEDIFF(DAY, GETDATE(), DATEADD(DAY, 30, PurchaseDate)) <= 0 THEN 'Expired' 
                ELSE 'Active' 
            END AS Status,
            CASE 
                WHEN DATEDIFF(DAY, GETDATE(), DATEADD(DAY, 30, PurchaseDate)) <= 0 THEN 0
                ELSE DATEDIFF(DAY, GETDATE(), DATEADD(DAY, 30, PurchaseDate)) 
            END AS DaysLeft
        FROM Purchase
        WHERE UserEmail = @UserEmail";

      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@UserEmail", Em);
      SqlDataReader rdr1 = cmd.ExecuteReader();

      GridView1.DataSource = rdr1;
      GridView1.DataBind();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "Navigate")
      {
        
        GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
        string courseName = ((Label)row.FindControl("Label3")).Text;
        string status = ((Label)row.FindControl("Label4")).Text;

       
        if (status == "Active")
        {
          
          Response.Redirect($"CourseVideos.aspx?courseName={HttpUtility.UrlEncode(courseName)}");
        }
        else
        {
          
          Response.Write("<script>alert('This course has expired. Please repurchase.');</script>");
        }
      }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      Fetcourse();
    }
  }
}
