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
  public partial class AdminHelp : System.Web.UI.Page
  {
    SqlConnection conn;
    int selectedSupportID;
    protected void Page_Load(object sender, EventArgs e)
    {

      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
      conn = new SqlConnection(cs);
      conn.Open();
      if (!IsPostBack)
      {
        BindSupportGrid();
      }

    }

    protected void BindSupportGrid()
    {
      string query = "SELECT SupportID, UserEmail,MasterCourseName, CourseName, ProblemDescription, Solution FROM Support WHERE Solution IS NULL";
      SqlCommand cmd = new SqlCommand(query, conn);
      GridView1.DataSource = cmd.ExecuteReader();
      GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "AddSolution")
      {
        selectedSupportID = Convert.ToInt32(e.CommandArgument);
        ViewState["SelectedSupportID"] = selectedSupportID; // Store ID for later
      }
    }

    protected void SubmitSolution_Click(object sender, EventArgs e)
    {
      string solution = TextBoxSolution.Text;
      selectedSupportID = (int)ViewState["SelectedSupportID"];

      string query = "UPDATE Support SET Solution = @Solution WHERE SupportID = @SupportID";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@Solution", solution);
      cmd.Parameters.AddWithValue("@SupportID", selectedSupportID);

      cmd.ExecuteNonQuery();
      BindSupportGrid();
      Response.Write("<script>alert('Solution added successfully.');</script>");
    }
  }
}
