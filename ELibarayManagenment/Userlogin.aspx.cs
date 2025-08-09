using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ELibarayManagenment
{
    public partial class Userlogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // user login button event 
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in all fields.');</script>");
            }
            else
            {
                UserLogin();
            }
        }


        private bool CheckEmptyFields()
        {
            // Input validation
            return string.IsNullOrWhiteSpace(TextBox1.Text) || string.IsNullOrWhiteSpace(TextBox2.Text);
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void UserLogin()
        {
            try
            {
                string hashedpassword = HashPassword(TextBox2.Text.Trim());
                using (SqlConnection Connection = new SqlConnection(strcon))
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    string querry = "SELECT * FROM member_master_tbl WHERE member_id = @member_id AND password = @password";
                    using (SqlCommand cmd = new SqlCommand(querry, Connection))
                    {
                        cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", hashedpassword);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Response.Write("<script>alert('Login Successfull');</script>");

                                Session["username"] = reader.GetValue(8).ToString();
                                Session["fullname"] = reader.GetValue(0).ToString();
                                Session["role"] = "user";
                                Session["status"] = reader.GetValue(10).ToString();
                            }
                            if (Session["RedirectAfterLogin"] != null)
                            {
                                Response.Redirect(Session["RedirectAfterLogin"].ToString());
                                Session["RedirectAfterLogin"] = null; // Clear the session variable
                            }
                            else
                            {
                                Response.Redirect("homepage.aspx");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid Credentials ');</script>");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}