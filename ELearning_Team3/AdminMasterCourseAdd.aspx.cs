using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELearning_Team3
{
    public partial class AdminMasterCourseAdd : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
            conn.Open();
        }

    protected void Button1_Click1(object sender, EventArgs e)
    {



      if (FileUpload1.HasFile)
      {
        string folderPath = Server.MapPath("~/CourseImages/");


        if (!Directory.Exists(folderPath))
          Directory.CreateDirectory(folderPath);


        string fileName = Path.GetFileName(FileUpload1.FileName);
        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
        string relativePath = "~/CourseImages/" + uniqueFileName;
        string fullPath = Path.Combine(folderPath, uniqueFileName);


        FileUpload1.SaveAs(fullPath);
        string MasterCourse = TextBox1.Text;
        string q = $"INSERT INTO MasterCourses (MasterCourseName,Pic) VALUES ('{MasterCourse}','{relativePath}')";
        SqlCommand cmd = new SqlCommand(q, conn);
        cmd.ExecuteNonQuery();
        List<string> emailAddresses = new List<string>();
        string emailQuery = "SELECT UserEmail FROM Users"; // Replace 'Users' with the correct table name
        SqlCommand emailCmd = new SqlCommand(emailQuery, conn);
        SqlDataReader reader = emailCmd.ExecuteReader();
        while (reader.Read())
        {
          emailAddresses.Add(reader["UserEmail"].ToString());
        }
        reader.Close();

       
        foreach (string email in emailAddresses)
        {
          
          string notificationText = $"A new master course '{MasterCourse}' has been added!";
          string notificationQuery = "INSERT INTO Notifications (Email, NotificationText) VALUES (@Email, @NotificationText)";
          SqlCommand notificationCmd = new SqlCommand(notificationQuery, conn);
          notificationCmd.Parameters.AddWithValue("@Email", email);
          notificationCmd.Parameters.AddWithValue("@NotificationText", notificationText);
          notificationCmd.ExecuteNonQuery();

          
          SendEmailNotification(email, MasterCourse);

        }
      }
    }
    private void SendEmailNotification(string email, string masterCourseName)
    {
      try
      {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("onkarsalunkhe1519@gmail.com");
        mail.To.Add(email);
        mail.Subject = "New Master Course Added";
        mail.Body = $"Dear User,\r\n\r\nWe are excited to announce that a new master course '{masterCourseName}' has been added to our platform. Check it out now!\r\n\r\nBest regards,\r\nE-Learning Team";

        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        smtp.Credentials = new NetworkCredential("onkarsalunkhe1519@gmail.com", "hxwwwrzpuaxpzwpb");
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.Send(mail);
      }
      catch (Exception ex)
      {
        
        Response.Write("<script>alert('Failed to send email: " + ex.Message + "');</script>");
      }
    }
  }
}
