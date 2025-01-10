using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELearning_Team3
{
  public partial class AdminAddCourse : System.Web.UI.Page
  {
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
      conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
      conn.Open();
      if (!IsPostBack)
      {
        PopulateMasterCourses();
      }

    }
    private void PopulateMasterCourses()
    {
      string query = "SELECT MasterCourseName FROM MasterCourses";
      SqlCommand cmd = new SqlCommand(query, conn);

      SqlDataReader reader = cmd.ExecuteReader();
      DropDownList1.Items.Clear(); 
      DropDownList1.Items.Add(new ListItem("-- Select Course --", "")); 

      while (reader.Read())
      {
        DropDownList1.Items.Add(new ListItem(reader["MasterCourseName"].ToString(), reader["MasterCourseName"].ToString()));
      }

      reader.Close();
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
        string MasterCourse = DropDownList1.SelectedValue;
        string Course = TextBox1.Text;
        string description= TextBox2.Text;
        double price=double.Parse(TextBox3.Text);
        string q1 = $"insert into Course values('{Course}','{MasterCourse}','{description}','{price}','{relativePath}')";
        SqlCommand cmd1 = new SqlCommand(q1, conn);
        cmd1.ExecuteNonQuery();
      }
      }
    }
}
