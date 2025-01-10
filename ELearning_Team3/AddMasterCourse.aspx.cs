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
    public partial class AddMasterCourse : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn=new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
            conn.Open();
        }

       
        protected void Button1_Click1(object sender, EventArgs e)
        {
            string MasterCourse = TextBox1.Text;
            string q = $"INSERT INTO MasterCourses (MasterCourseName) VALUES ('{MasterCourse}')";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
        }
    }
}