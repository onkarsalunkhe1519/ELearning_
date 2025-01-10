using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELearning_Team3
{
  public partial class CommunityUser : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["MyUser"] == null)
        {
          Response.Redirect("Login.aspx");
          return;
        }

        // Assume CourseID is passed as a query parameter
        int courseId = Convert.ToInt32(Request.QueryString["CourseID"]);
        LoadDiscussions(courseId);
      }
    }

    private void LoadDiscussions(int courseId)
    {
      string query = "EXEC FetchDiscussionsByCourse @CourseID";

      using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
      {
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@CourseID", courseId);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        RepeaterDiscussions.DataSource = reader;
        RepeaterDiscussions.DataBind();

        conn.Close();
      }
    }

    protected void RepeaterDiscussions_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
      {
        int discussionId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "DiscussionID"));
        Repeater repliesRepeater = (Repeater)e.Item.FindControl("RepeaterReplies");

        LoadReplies(discussionId, repliesRepeater);
      }
    }

    private void LoadReplies(int discussionId, Repeater repliesRepeater)
    {
      string query = "EXEC FetchRepliesByDiscussion @DiscussionID";

      using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
      {
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@DiscussionID", discussionId);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        repliesRepeater.DataSource = reader;
        repliesRepeater.DataBind();

        conn.Close();
      }
    }

    protected void ButtonSubmitDiscussion_Click(object sender, EventArgs e)
    {
      if (Session["MyUser"] == null)
      {
        Response.Redirect("Login.aspx");
        return;
      }

      string userEmail = Session["MyUser"].ToString();
      int courseId = Convert.ToInt32(Request.QueryString["CourseID"]);
      

      string query = "EXEC InsertDiscussion @CourseID, @UserEmail, @Title, @Content";

      using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
      {
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@CourseID", courseId);
        cmd.Parameters.AddWithValue("@UserEmail", userEmail);
        

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
      }

      // Reload discussions
      LoadDiscussions(courseId);
    }

    protected void ButtonSubmitReply_Click(object sender, EventArgs e)
    {
      if (Session["MyUser"] == null)
      {
        Response.Redirect("Login.aspx");
        return;
      }

      string userEmail = Session["MyUser"].ToString();
      Button btn = (Button)sender;
      int discussionId = Convert.ToInt32(btn.CommandArgument);

      RepeaterItem item = (RepeaterItem)btn.NamingContainer;
      TextBox replyTextBox = (TextBox)item.FindControl("TextBoxReplyContent");
      string replyContent = replyTextBox.Text;

      string query = "EXEC InsertDiscussionReply @DiscussionID, @UserEmail, @Content";

      using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
      {
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@DiscussionID", discussionId);
        cmd.Parameters.AddWithValue("@UserEmail", userEmail);
        cmd.Parameters.AddWithValue("@Content", replyContent);

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
      }

      // Reload replies for this discussion
      LoadReplies(discussionId, (Repeater)item.FindControl("RepeaterReplies"));
    }
  }
}
