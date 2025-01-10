using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELearning_Team3
{
  public partial class AdminViewCourses : System.Web.UI.Page
  {
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
      conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);

      if (!IsPostBack)
      {
        BindMasterCourses();
        BindCourses();
        BindTopics();
      }
    }
    private void BindMasterCourses()
    {
      string query = "SELECT * FROM MasterCourses";
      SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
      DataTable dt = new DataTable();
      adapter.Fill(dt);
      GridViewMasterCourses.DataSource = dt;
      GridViewMasterCourses.DataBind();
    }

    protected void GridViewMasterCourses_RowEditing(object sender, GridViewEditEventArgs e)
    {
      GridViewMasterCourses.EditIndex = e.NewEditIndex;
      BindMasterCourses();
    }

    protected void GridViewMasterCourses_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      int id = Convert.ToInt32(GridViewMasterCourses.DataKeys[e.RowIndex].Value);
      string name = ((TextBox)GridViewMasterCourses.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
      string pic = ((TextBox)GridViewMasterCourses.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

      string query = "UPDATE MasterCourses SET MasterCourseName = @Name, Pic = @Pic WHERE MasterCourseID = @ID";
      SqlCommand cmd = new SqlCommand(query, conn);

      cmd.Parameters.AddWithValue("@Name", name);
      cmd.Parameters.AddWithValue("@Pic", pic);
      cmd.Parameters.AddWithValue("@ID", id);

      conn.Open();
      cmd.ExecuteNonQuery();
      conn.Close();

      GridViewMasterCourses.EditIndex = -1;
      BindMasterCourses();
    }

    protected void GridViewMasterCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      int id = Convert.ToInt32(GridViewMasterCourses.DataKeys[e.RowIndex].Value);

      string query = "DELETE FROM MasterCourses WHERE MasterCourseID = @ID";
      SqlCommand cmd = new SqlCommand(query, conn);

      cmd.Parameters.AddWithValue("@ID", id);

      conn.Open();
      cmd.ExecuteNonQuery();
      conn.Close();

      BindMasterCourses();
    }

    protected void GridViewMasterCourses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      GridViewMasterCourses.EditIndex = -1;
      BindMasterCourses();
    }

    // Courses Operations
    private void BindCourses()
    {
      string query = "SELECT * FROM Course";
      SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
      DataTable dt = new DataTable();
      adapter.Fill(dt);
      GridViewCourses.DataSource = dt;
      GridViewCourses.DataBind();
    }

    protected void GridViewCourses_RowEditing(object sender, GridViewEditEventArgs e)
    {
      GridViewCourses.EditIndex = e.NewEditIndex;
      BindCourses();
    }

    protected void GridViewCourses_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      int id = Convert.ToInt32(GridViewCourses.DataKeys[e.RowIndex].Value);
      string name = ((TextBox)GridViewCourses.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
      string masterCourseName = ((TextBox)GridViewCourses.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
      string description = ((TextBox)GridViewCourses.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
      decimal price = Convert.ToDecimal(((TextBox)GridViewCourses.Rows[e.RowIndex].Cells[4].Controls[0]).Text);
      string pic = ((TextBox)GridViewCourses.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

      string query = "UPDATE Course SET CourseName = @Name, MasterCourseName = @MasterCourseName, Description = @Description, Price = @Price, Pic = @Pic WHERE CourseId = @ID";
      SqlCommand cmd = new SqlCommand(query, conn);

      cmd.Parameters.AddWithValue("@Name", name);
      cmd.Parameters.AddWithValue("@MasterCourseName", masterCourseName);
      cmd.Parameters.AddWithValue("@Description", description);
      cmd.Parameters.AddWithValue("@Price", price);
      cmd.Parameters.AddWithValue("@Pic", pic);
      cmd.Parameters.AddWithValue("@ID", id);

      conn.Open();
      cmd.ExecuteNonQuery();
      conn.Close();

      GridViewCourses.EditIndex = -1;
      BindCourses();
    }

    protected void GridViewCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      int id = Convert.ToInt32(GridViewCourses.DataKeys[e.RowIndex].Value);

      string query = "DELETE FROM Course WHERE CourseId = @ID";
      SqlCommand cmd = new SqlCommand(query, conn);

      cmd.Parameters.AddWithValue("@ID", id);

      conn.Open();
      cmd.ExecuteNonQuery();
      conn.Close();

      BindCourses();
    }

    protected void GridViewCourses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      GridViewCourses.EditIndex = -1;
      BindCourses();
    }

    
    private void BindTopics()
    {
      string query = "SELECT * FROM Topic";
      SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
      DataTable dt = new DataTable();
      adapter.Fill(dt);
      GridViewTopics.DataSource = dt;
      GridViewTopics.DataBind();
    }

    protected void GridViewTopics_RowEditing(object sender, GridViewEditEventArgs e)
    {
      GridViewTopics.EditIndex = e.NewEditIndex;
      BindTopics();
    }

    protected void GridViewTopics_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      int id = Convert.ToInt32(GridViewTopics.DataKeys[e.RowIndex].Value);
      string name = ((TextBox)GridViewTopics.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
      string courseName = ((TextBox)GridViewTopics.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
      string youtubeURL = ((TextBox)GridViewTopics.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

      string query = "UPDATE Topic SET TopicName = @Name, CourseName = @CourseName, YoutubeURL = @YoutubeURL WHERE TopicId = @ID";
      SqlCommand cmd = new SqlCommand(query, conn);

      cmd.Parameters.AddWithValue("@Name", name);
      cmd.Parameters.AddWithValue("@CourseName", courseName);
      cmd.Parameters.AddWithValue("@YoutubeURL", youtubeURL);
      cmd.Parameters.AddWithValue("@ID", id);

      conn.Open();
      cmd.ExecuteNonQuery();
      conn.Close();

      GridViewTopics.EditIndex = -1;
      BindTopics();
    }

    protected void GridViewTopics_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      int id = Convert.ToInt32(GridViewTopics.DataKeys[e.RowIndex].Value);

      string query = "DELETE FROM Topic WHERE TopicId = @ID";
      SqlCommand cmd = new SqlCommand(query, conn);

      cmd.Parameters.AddWithValue("@ID", id);

      conn.Open();
      cmd.ExecuteNonQuery();
      conn.Close();

      BindTopics();
    }

    protected void GridViewTopics_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      GridViewTopics.EditIndex = -1;
      BindTopics();
    }
  }
}
