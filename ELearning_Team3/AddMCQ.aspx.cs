using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ELearning_Team3
{
  public partial class AddMCQ : System.Web.UI.Page
  {
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
      conn = new SqlConnection(cs);
      conn.Open();

      if (!IsPostBack)
      {
        LoadTopics(); 
        LoadQuestions(); 
      }
    }

    private void LoadTopics()
    {
      string query = "SELECT TopicName FROM Topic";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlDataReader reader = cmd.ExecuteReader();

      ddlTopics.DataSource = reader;
      ddlTopics.DataTextField = "TopicName";
      ddlTopics.DataValueField = "TopicName";
      ddlTopics.DataBind();

      reader.Close();
    }

    private void LoadQuestions()
    {
      string query = "SELECT QuestionText FROM Question"; 
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlDataReader reader = cmd.ExecuteReader();

      ddlQuestions.DataSource = reader;
      ddlQuestions.DataTextField = "QuestionText";
      ddlQuestions.DataValueField = "QuestionText";
      ddlQuestions.DataBind();

      reader.Close();
    }

    protected void btnAddQuestion_Click(object sender, EventArgs e)
    {
      string questionText = txtQuestion.Text;
      string topicName = ddlTopics.SelectedValue;

      string insertQuestionQuery = "INSERT INTO Question (QuestionText, TopicName) VALUES (@QuestionText, @TopicName)";
      SqlCommand cmd = new SqlCommand(insertQuestionQuery, conn);
      cmd.Parameters.AddWithValue("@QuestionText", questionText);
      cmd.Parameters.AddWithValue("@TopicName", topicName);

      cmd.ExecuteNonQuery();

      lblMessage.Text = "Question added successfully!";

      
      LoadQuestions();
    }

    protected void btnAddChoice_Click(object sender, EventArgs e)
    {
      string choiceText = txtChoice.Text;
      string questionText = ddlQuestions.SelectedValue;
      bool isCorrect = chkIsCorrect.Checked;

      string insertChoiceQuery = @"
                INSERT INTO Choice (ChoiceText, QuestionText, IsCorrect)
                VALUES (@ChoiceText, @QuestionText, @IsCorrect)";
      SqlCommand cmd = new SqlCommand(insertChoiceQuery, conn);
      cmd.Parameters.AddWithValue("@ChoiceText", choiceText);
      cmd.Parameters.AddWithValue("@QuestionText", questionText);
      cmd.Parameters.AddWithValue("@IsCorrect", isCorrect);

      cmd.ExecuteNonQuery();

      lblMessage.Text = "Choice added successfully!";
    }
  }
}
