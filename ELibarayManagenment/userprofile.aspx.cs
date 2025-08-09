using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELibarayManagenment
{
    public partial class userprofile : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        int DueBookCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null)
            {
                Session["RedirectAfterLogin"] = Request.Url.AbsoluteUri;
                Response.Redirect("Userlogin.aspx");  // Redirect to the admin login page
            }
            else if (Session["role"].Equals("admin"))
            {
                Response.Write("<script>alert('Access Denied: Admins are not allowed to access this page.');</script>");
                Response.Redirect("homepage.aspx"); // Redirect to an admin dashboard or any other page
            }
            else
            {
                GetUserBookData();
                if (!Page.IsPostBack)
                {
                    GetUserData();
                }

            }
            Label2.Text = "Your Due Books - " + DueBookCount.ToString();

        }

        //update buuton click 
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Plz Fill In All Fields.');</script>");
            }
            else if (string.IsNullOrWhiteSpace(TextBox9.Text))
            {
                Response.Write("<script>alert('Plz Enter Your Password To Proceed');</script>");
            }
            else
            {
                UpdateUserData();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // Check your condition here
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                        e.Row.ForeColor = System.Drawing.Color.White;
                        DueBookCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
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

        private void UpdateUserData()
        {
            try
            {
                string hashedPassword;

                hashedPassword = HashPassword(TextBox9.Text.Trim().ToString());

                string NewHashedPassword = string.IsNullOrWhiteSpace(TextBox10.Text) ? null : HashPassword(TextBox10.Text.Trim());

                using (SqlConnection Connection = new SqlConnection(strcon))
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    string querry = "SELECT * FROM member_master_tbl WHERE member_id = @member_id AND password = @password";
                    using (SqlCommand cmd = new SqlCommand(querry, Connection))
                    {
                        cmd.Parameters.AddWithValue("@member_id", TextBox8.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Close();
                            string UpdateQuerry = "UPDATE member_master_tbl SET full_name = @full_name , dob = @dob , contact_no = @contact_no, email = @email, state = @state , city = @city , pincode = @pincode , full_address = @full_address , password = @password ,   account_status = @account_status WHERE member_id = @member_id ";
                            using (SqlCommand UpdateCmd = new SqlCommand(UpdateQuerry, Connection))
                            {
                                UpdateCmd.Parameters.AddWithValue("@member_id", TextBox8.Text.Trim());
                                UpdateCmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                                UpdateCmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                                UpdateCmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                                UpdateCmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                                UpdateCmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                                UpdateCmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                                UpdateCmd.Parameters.AddWithValue("@pincode", TextBox7.Text.Trim());
                                UpdateCmd.Parameters.AddWithValue("@full_address", TextBox5.Text.Trim());
                                UpdateCmd.Parameters.AddWithValue("@account_status", "Pending");
                                UpdateCmd.Parameters.AddWithValue("@password", NewHashedPassword ?? hashedPassword);




                                int rowsAffected = UpdateCmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    Response.Write("<script>alert(' Updated SuccessFully');</script>");
                                    GetUserBookData();
                                    GetUserData();


                                }
                                else
                                {
                                    Response.Write("<script>alert('Not SuccessFull');</script>");
                                }
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Wrong Password!!! ');</script>");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void GetUserBookData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT * FROM book_issue_tbl WHERE member_id = @member_id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@member_id", Session["username"].ToString()); // Use TextBox2 for Member ID

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        private void GetUserData()
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
                        cmd.Parameters.AddWithValue("@member_id", Session["username"].ToString()); // Use TextBox2 for Member ID

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            TextBox1.Text = dt.Rows[0]["full_name"].ToString();
                            TextBox2.Text = dt.Rows[0]["dob"].ToString();
                            TextBox3.Text = dt.Rows[0]["contact_no"].ToString();
                            TextBox4.Text = dt.Rows[0]["email"].ToString();
                            TextBox5.Text = dt.Rows[0]["full_address"].ToString();
                            TextBox6.Text = dt.Rows[0]["city"].ToString();
                            TextBox7.Text = dt.Rows[0]["pincode"].ToString();
                            TextBox8.Text = dt.Rows[0]["member_id"].ToString();
                            DropDownList1.SelectedValue = dt.Rows[0]["state"].ToString().Trim();
                            Label1.Text = dt.Rows[0]["account_status"].ToString().Trim();
                            if (dt.Rows[0]["account_status"].ToString().Trim() == "Active")
                            {
                                Label1.Attributes.Add("class", "badge badge-pill  badge-success");
                            }
                            else if (dt.Rows[0]["account_status"].ToString().Trim() == "Pending")
                            {
                                Label1.Attributes.Add("class", "badge badge-pill  badge-warning");
                            }
                            else if (dt.Rows[0]["account_status"].ToString().Trim() == "Deactivate")
                            {
                                Label1.Attributes.Add("class", "badge badge-pill  badge-danger");
                            }
                            else
                            {
                                Label1.Attributes.Add("class", "badge badge-pill  badge-secondary");
                            }

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
            // Check if any TextBox is empty
            if (string.IsNullOrWhiteSpace(TextBox1.Text) || // Full Name
                string.IsNullOrWhiteSpace(TextBox2.Text) || // Date of Birth
                string.IsNullOrWhiteSpace(TextBox3.Text) || // Contact No
                string.IsNullOrWhiteSpace(TextBox4.Text) || // Email ID
                string.IsNullOrWhiteSpace(TextBox6.Text) || // City
                string.IsNullOrWhiteSpace(TextBox7.Text) || // Pincode
                string.IsNullOrWhiteSpace(TextBox5.Text) || // Full Address
                string.IsNullOrWhiteSpace(TextBox8.Text))   // New Password
            {
                return true; // At least one TextBox is empty
            }

            // Check if any DropDownList has the default "Select" option
            if (DropDownList1.SelectedIndex == 0) // State (default index is 0 for "Select")
            {
                return true;
            }

            // If all fields are filled and selections are made
            return false;
        }


    }
}