using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;

namespace ELearning_Team3
{
  public partial class CourseVideos : System.Web.UI.Page
  {
    SqlConnection conn;
    DataTable dt;
    v
    protected void Page_Load(object sender, EventArgs e)
    {

      string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

      try
      {
        conn = new SqlConnection(cs);
        conn.Open();

        if (!IsPostBack)
        {
          // Initialize session to track clicked videos
          if (Session["ClickedVideos"] == null)
          {
            Session["ClickedVideos"] = new List<string>();
          }

          string courseName = Request.QueryString["courseName"];
          if (!string.IsNullOrEmpty(courseName))
          {
            BindPlaylists(courseName);
          }
          else
          {
            Response.Write("<script>alert('Invalid course selected.');</script>");
          }
        }
      }
      catch (Exception ex)
      {
        Response.Write($"<script>alert('Error initializing connection: {ex.Message}');</script>");
      }
    }

    protected void GridViewVideos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "Play")
      {
        string topicName = e.CommandArgument.ToString();

        if (string.IsNullOrEmpty(topicName))
        {
          Response.Write("<script>alert('Topic name is null or empty.');</script>");
          return;
        }

        // Store topicName in session for later use
        Session["CurrentTopicName"] = topicName;

        // Play video logic
        string query = "SELECT YoutubeURL FROM Topic WHERE TopicName = @TopicName";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@TopicName", topicName);

        string videoUrl = cmd.ExecuteScalar()?.ToString();
        if (!string.IsNullOrEmpty(videoUrl))
        {
          ltrYouTubePlayer.Text = $"<iframe width='920' height='600' src='https://www.youtube.com/embed/{videoUrl}' frameborder='0' allow='accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>";
        }

        // Load MCQs for the selected topic
        LoadMCQs(topicName);
      }
    }


    private void LoadMCQs(string topicName)
    {
      string query = "SELECT QuestionText FROM Question WHERE TopicName = @TopicName";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@TopicName", topicName);

      rptQuestions.DataSource = cmd.ExecuteReader();
      rptQuestions.DataBind();

      rptQuestions.Visible = true;
      btnSubmitAnswers.Visible = true;
    }

    public DataTable GetChoices(string questionText)
    {
      string query = "SELECT ChoiceText FROM Choice WHERE QuestionText = @QuestionText";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@QuestionText", questionText);

      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataTable choices = new DataTable();
      adapter.Fill(choices);
      return choices;
    }

    protected void btnSubmitAnswers_Click(object sender, EventArgs e)
    {
      int correctAnswers = 0;
      int wrongAnswers = 0;

      // Loop through the MCQ questions in the repeater
      foreach (RepeaterItem item in rptQuestions.Items)
      {
        Label lblQuestion = (Label)item.FindControl("lblQuestion");
        RadioButtonList rblChoices = (RadioButtonList)item.FindControl("rblChoices");

        string selectedChoice = rblChoices.SelectedValue;

        string query = "SELECT IsCorrect FROM Choice WHERE QuestionText = @QuestionText AND ChoiceText = @ChoiceText";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@QuestionText", lblQuestion.Text);
        cmd.Parameters.AddWithValue("@ChoiceText", selectedChoice);

        object result = cmd.ExecuteScalar();
        if (result != null && (bool)result)
        {
          correctAnswers++;
        }
        else
        {
          wrongAnswers++;
        }
      }

      // Store the result in the UserReports table
      try
      {
        string userEmail = Session["MyUser"]?.ToString();
        string topicName = Session["CurrentTopicName"]?.ToString();

        if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(topicName))
        {
          lblResult.Text = "Error: Unable to save results. User or Topic not found.";
          return;
        }

        string insertQuery = @"
            INSERT INTO UserReports (UserEmail, TopicName, CorrectAnswers, WrongAnswers, CreatedAt)
            VALUES (@UserEmail, @TopicName, @CorrectAnswers, @WrongAnswers, GETDATE())";

        SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
        insertCmd.Parameters.AddWithValue("@UserEmail", userEmail);
        insertCmd.Parameters.AddWithValue("@TopicName", topicName);
        insertCmd.Parameters.AddWithValue("@CorrectAnswers", correctAnswers);
        insertCmd.Parameters.AddWithValue("@WrongAnswers", wrongAnswers);

        insertCmd.ExecuteNonQuery();

        lblResult.Text = $"Results saved! You answered {correctAnswers} correctly and {wrongAnswers} incorrectly.";
      }
      catch (Exception ex)
      {
        lblResult.Text = $"Error saving results: {ex.Message}";
      }

      rptQuestions.Visible = false;
      btnSubmitAnswers.Visible = false;

      // Update session for clicked video
      var clickedVideos = (List<string>)Session["ClickedVideos"];
      if (!clickedVideos.Contains(Session["CurrentTopicName"].ToString()))
      {
        clickedVideos.Add(Session["CurrentTopicName"].ToString());
        Session["ClickedVideos"] = clickedVideos;
      }

      // Refresh the button logic
      string courseName = Request.QueryString["courseName"];
      if (!string.IsNullOrEmpty(courseName))
      {
        BindPlaylists(courseName);
      }
    }



    private void BindPlaylists(string courseName)
    {
      try
      {
        string query = "SELECT TopicName FROM Topic WHERE CourseName = @CourseName";
        using (SqlCommand command = new SqlCommand(query, conn))
        {
          command.Parameters.AddWithValue("@CourseName", courseName);

          dt = new DataTable();
          using (SqlDataAdapter adapter = new SqlDataAdapter(command))
          {
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
              GridView1.DataSource = dt;
              GridView1.DataBind();

              foreach (GridViewRow row in GridView1.Rows)
              {
                // Ensure each row's button is enabled
                var btnPlay = row.FindControl("btnPlay") as Button;
                if (btnPlay != null)
                {
                  btnPlay.Enabled = true; // Explicitly enable the button
                }
              }
            }
            else
            {
              Response.Write("<script>alert('No topics available for the selected course.');</script>");
            }
          }
        }
      }
      catch (Exception ex)
      {
        Response.Write($"<script>alert('Error fetching topics: {ex.Message}');</script>");
      }
    }






    protected void btnCertificate_Click(object sender, EventArgs e)
    {
      try
      {
        // Check for user session
        string userEmail = Session["MyUser"]?.ToString();
        if (string.IsNullOrEmpty(userEmail))
        {
          Response.Write("<script>alert('User email not found in session.');</script>");
          return;
        }

        // Fetch user details
        string userName = string.Empty;
        string courseName = Request.QueryString["courseName"];
        if (string.IsNullOrEmpty(courseName))
        {
          Response.Write("<script>alert('Course name not found.');</script>");
          return;
        }

        using (SqlCommand command = new SqlCommand("SELECT Name FROM Users WHERE UserEmail = @UserEmail", conn))
        {
          command.Parameters.AddWithValue("@UserEmail", userEmail);
          userName = command.ExecuteScalar()?.ToString();
        }

        if (string.IsNullOrEmpty(userName))
        {
          Response.Write("<script>alert('User details not found in the database.');</script>");
          return;
        }

        // Generate PDF file path
        string pdfPath = Server.MapPath($"~/Certificates/{userName.Replace(" ", "_")}_Certificate.pdf");
        GenerateCertificatePDF(pdfPath, userName, courseName);

        // Insert certificate record in the database
        using (SqlCommand command = new SqlCommand("INSERT INTO Certificates (UserEmail, CertificatePath, GeneratedDate) VALUES (@UserEmail, @CertificatePath, @GeneratedDate)", conn))
        {
          command.Parameters.AddWithValue("@UserEmail", userEmail);
          command.Parameters.AddWithValue("@CertificatePath", pdfPath);
          command.Parameters.AddWithValue("@GeneratedDate", DateTime.Now);
          command.ExecuteNonQuery();
        }

        // Send certificate email
        SendCertificateEmail(userEmail, userName, pdfPath);

        Response.Write("<script>alert('Certificate generated and sent successfully.');</script>");

        // Re-enable the first button in GridView
        if (GridView1.Rows.Count > 0)
        {
          GridViewRow firstRow = GridView1.Rows[0];
          var btnPlay = firstRow.FindControl("btnPlay") as Button;
          if (btnPlay != null)
          {
            btnPlay.Enabled = true;
          }
        }
      }
      catch (Exception ex)
      {
        Response.Write($"<script>alert('Error generating certificate: {ex.Message}');</script>");
      }
    }



    private void GenerateCertificatePDF(string pdfPath, string userName, string courseName)
    {
      string directoryPath = Path.GetDirectoryName(pdfPath);

      if (!Directory.Exists(directoryPath))
      {
        Directory.CreateDirectory(directoryPath);
      }

      string htmlContent = $@"
                <body>
                    <div>
                        <h2>Certificate of Completion</h2>
                        <p>This certifies that</p>
                        <h3>{userName}</h3>
                        <p>has completed the course</p>
                        <h3>{courseName}</h3>
                        <p>Date Completed: {DateTime.Now:dd MMMM yyyy}</p>
                    </div>
                </body>";

      using (var document = new iTextSharp.text.Document())
      {
        iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
        document.Open();
        using (var stringReader = new StringReader(htmlContent))
        {
          iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(stringReader, null)
              .ForEach(element => document.Add(element));
        }
        document.Close();
      }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      int rating = 0;


      if (rb5.Checked) rating = 5;
      else if (rb4.Checked) rating = 4;
      else if (rb3.Checked) rating = 3;
      else if (rb2.Checked) rating = 2;
      else if (rb1.Checked) rating = 1;

      string reviewText = txtReview.Text.Trim();

      if (rating > 0 && !string.IsNullOrEmpty(reviewText))
      {
        SaveReview(rating, reviewText);
        txtReview.Text = string.Empty;
        rb1.Checked = rb2.Checked = rb3.Checked = rb4.Checked = rb5.Checked = false;
        LoadReviews();
      }
    }
    private void SaveReview(int rating, string reviewText)
    {
      string courseName = Request.QueryString["courseName"];
      string UserEmail = Session["MyUser"].ToString();
      string query = $"INSERT INTO Reviews (UserEmail,CourseName,Rating, ReviewText) VALUES ('{UserEmail}','{courseName}','{rating}','{reviewText}')";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.ExecuteNonQuery();


    }

    private void LoadReviews()
    {
      string UserEmail = Session["MyUser"].ToString();

      string query = $"SELECT Rating, ReviewText,UserEmail,CourseName, CreatedDate FROM Reviews where UserEmail='{UserEmail}'";
      SqlCommand cmd = new SqlCommand(query, conn);


      SqlDataAdapter da = new SqlDataAdapter(cmd);

      DataTable dt = new DataTable();
      da.Fill(dt);
      gvReviews.DataSource = dt;
      gvReviews.DataBind();

    }

    private void SendCertificateEmail(string userEmail, string userName, string pdfPath)
    {
      using (var mail = new System.Net.Mail.MailMessage())
      {
        mail.From = new System.Net.Mail.MailAddress("onkarsalunkhe1519@gmail.com");
        mail.To.Add(userEmail);
        mail.Subject = "Certificate of Completion";
        mail.Body = $"Dear {userName},\n\nCongratulations! Attached is your Certificate of Completion.\n\nBest regards,\nE-Learning Team";
        mail.Attachments.Add(new System.Net.Mail.Attachment(pdfPath));

        using (var smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
        {
          smtp.Credentials = new System.Net.NetworkCredential("onkarsalunkhe1519@gmail.com", "hxwwwrzpuaxpzwpb");
          smtp.EnableSsl = true;
          smtp.Send(mail);
        }
      }
    }
  }
}
