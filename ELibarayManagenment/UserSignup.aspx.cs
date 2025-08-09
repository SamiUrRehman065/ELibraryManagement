using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ELibarayManagenment
{
    public partial class UserSignup : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // sign up button event 
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in all fields.');</script>");
            }
            else if (CheckMemberExists())
            {
                Response.Write("<script>alert('User ID exists ');</script>");
            }
            else
            {
                SignUpNewUser();
            }

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
        // function to check for duplicate id 
        private bool CheckMemberExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT * FROM member_master_tbl WHERE member_id = @member_id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@member_id", TextBox8.Text.Trim());

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            return dt.Rows.Count >= 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for further inspection if necessary
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        private void SignUpNewUser()
        {
            try
            {


                // Hash the password
                string hashedPassword = HashPassword(TextBox9.Text.Trim());

                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO member_master_tbl(full_name,dob,contact_no,email,state,city,pincode,full_address,member_id,password,account_status) VALUES (@full_name, @dob, @contact_no, @email, @state, @city, @pincode, @full_address, @member_id, @password, @account_status)", connection))
                    {
                        cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                        cmd.Parameters.AddWithValue("@pincode", TextBox7.Text.Trim());
                        cmd.Parameters.AddWithValue("@full_address", TextBox5.Text.Trim());
                        cmd.Parameters.AddWithValue("@member_id", TextBox8.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", hashedPassword); // Use the hashed password
                        cmd.Parameters.AddWithValue("@account_status", "pending");

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login');</script>");
                            ClearTextBoxes();

                        }
                        else
                        {
                            Response.Write("<script>alert('Sign Up Failed. Please try again.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private bool CheckEmptyFields()
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(TextBox1.Text) ||
                string.IsNullOrWhiteSpace(TextBox2.Text) ||
                string.IsNullOrWhiteSpace(TextBox3.Text) ||
                string.IsNullOrWhiteSpace(TextBox4.Text) ||
                string.IsNullOrWhiteSpace(TextBox6.Text) ||
                string.IsNullOrWhiteSpace(TextBox7.Text) ||
                string.IsNullOrWhiteSpace(TextBox5.Text) ||
                string.IsNullOrWhiteSpace(TextBox8.Text) ||
                string.IsNullOrWhiteSpace(TextBox9.Text))
            {

                return true;
            }
            return false;
        }

        private void ClearTextBoxes()
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox5.Text = string.Empty;
            TextBox6.Text = string.Empty;
            TextBox7.Text = string.Empty;
            TextBox8.Text = string.Empty;
            TextBox9.Text = string.Empty;
            // Reset the DropDownList to its default value
            DropDownList1.SelectedIndex = 0;
        }

    }
}