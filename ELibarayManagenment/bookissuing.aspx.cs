using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ELibarayManagenment
{
    public partial class bookissuing : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || !Session["role"].Equals("admin"))
            {
                Session["RedirectAfterLogin"] = Request.Url.AbsoluteUri;
                Response.Redirect("adminlogin.aspx"); // Redirect to the admin login page
            }
            if (!IsPostBack)
            {
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                TextBox5.Text = today;
                TextBox5.Attributes.Add("min", today);
            }
        }
        // Go button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBox1.Text) || String.IsNullOrWhiteSpace(TextBox2.Text))
            {
                Response.Write("<script>alert('Please fill in both Member ID and Book ID fields.');</script>");
            }
            else
            {
                bool isMemberExists = CheckMemberExists();
                bool isBookExists = CheckBookExists();

                if (isMemberExists && isBookExists)
                {
                    GetNames(); // Fill in the names if both exist
                }
                else
                {
                    if (!isMemberExists)
                    {
                        Response.Write("<script>alert('Member does not exist.');</script>");
                    }
                    if (!isBookExists)
                    {
                        Response.Write("<script>alert('Book does not exist.');</script>");
                    }
                }
            }
        }
        // Issue Book Click
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in all fields.');</script>");
            }
            else
            {



                DateTime issueDate = DateTime.Parse(TextBox5.Text.Trim());
                DateTime dueDate = DateTime.Parse(TextBox6.Text.Trim());

                if (dueDate < issueDate)
                {
                    Response.Write("<script>alert('Due date cannot be earlier than the issue date.');</script>");
                }
                else
                {
                    bool isMemberExists = CheckMemberExists();
                    bool isBookExists = CheckBookExists();
                    bool isBookStock = CheckBookStock();
                    bool isBookIssued = CheckBookIsIssued();
                    if (isMemberExists && isBookExists && isBookStock && !isBookIssued)
                    {
                        IssueBook();
                    }
                    else
                    {
                        if (!isMemberExists)
                        {
                            Response.Write("<script>alert('Member does not exist.');</script>");
                        }
                        if (!isBookExists)
                        {
                            Response.Write("<script>alert('Book does not exist.');</script>");
                        }
                        if (!isBookStock)
                        {
                            Response.Write("<script>alert('Book Stock is not available');</script>");
                        }
                        if (isBookIssued)
                        {
                            Response.Write("<script>alert('Member ALready Have This Book!');</script>");
                        }
                    }
                }
            }
        }
        // Return Book Click
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBox1.Text) || String.IsNullOrWhiteSpace(TextBox2.Text))
            {
                Response.Write("<script>alert('Please fill in both Member ID and Book ID fields.');</script>");
            }
            else
            {
                bool isMemberExists = CheckMemberExists();
                bool isBookExists = CheckBookExists();

                bool isBookIssued = CheckBookIsIssued();
                if (isMemberExists && isBookExists && isBookIssued)
                {
                    ReturnBook();
                }
                else
                {
                    if (!isMemberExists)
                    {
                        Response.Write("<script>alert('Member does not exist.');</script>");
                    }
                    if (!isBookExists)
                    {
                        Response.Write("<script>alert('Book does not exist.');</script>");
                    }
                    if (!isBookIssued)
                    {
                        Response.Write("<script>alert('Member Have Not This Book!');</script>");
                    }
                }
            }
        }

        private void GetNames()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    // Get Member Name
                    string memberQuery = "SELECT full_name FROM member_master_tbl WHERE member_id = @member_id";
                    using (SqlCommand memberCmd = new SqlCommand(memberQuery, con))
                    {
                        memberCmd.Parameters.AddWithValue("@member_id", TextBox2.Text.Trim());

                        using (SqlDataAdapter memberAdapter = new SqlDataAdapter(memberCmd))
                        {
                            DataTable memberTable = new DataTable();
                            memberAdapter.Fill(memberTable);

                            if (memberTable.Rows.Count > 0)
                            {
                                TextBox3.Text = memberTable.Rows[0]["full_name"].ToString(); // Fill Member Name
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid Member Id');</script>");
                            }
                        }
                    }

                    // Get Book Name
                    string bookQuery = "SELECT book_name FROM book_master_tbl WHERE book_id = @book_id";
                    using (SqlCommand bookCmd = new SqlCommand(bookQuery, con))
                    {
                        bookCmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());

                        using (SqlDataAdapter bookAdapter = new SqlDataAdapter(bookCmd))
                        {
                            DataTable bookTable = new DataTable();
                            bookAdapter.Fill(bookTable);

                            if (bookTable.Rows.Count > 0)
                            {
                                TextBox4.Text = bookTable.Rows[0]["book_name"].ToString(); // Fill Book Name
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid Book Id');</script>");
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

        private void IssueBook()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string issueBookQuery = @"INSERT INTO book_issue_tbl(member_id, member_name,  book_id, book_name, issue_date, due_date) 
                                      VALUES(@member_id, @member_name,  @book_id, @book_name,  @issue_date, @due_date)";
                    using (SqlCommand cmd = new SqlCommand(issueBookQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@member_id", TextBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@member_name", TextBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@book_name", TextBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@issue_date", TextBox5.Text.Trim());
                        cmd.Parameters.AddWithValue("@due_date", TextBox6.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Book issued successfully.');</script>");
                            GridView1.DataBind();

                        }
                        else
                        {
                            Response.Write("<script>alert('Not SuccessFull');</script>");
                        }

                    }

                    // Optionally, update the stock in the book_master_tbl
                    string updateStockQuery = "UPDATE book_master_tbl SET current_stock = current_stock - 1 WHERE book_id = @book_id";
                    using (SqlCommand stockCmd = new SqlCommand(updateStockQuery, con))
                    {
                        stockCmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                        stockCmd.ExecuteNonQuery();
                        ClearTextBoxes();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void ReturnBook()
        {

            try
            {
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "DELETE FROM book_issue_tbl WHERE book_id = @book_id AND member_id = @member_id";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@member_id", TextBox2.Text.Trim());


                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            string updateStockQuery = "UPDATE book_master_tbl SET current_stock = current_stock + 1 WHERE book_id = @book_id";
                            using (SqlCommand stockCmd = new SqlCommand(updateStockQuery, connection))
                            {
                                stockCmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                                stockCmd.ExecuteNonQuery();
                                ClearTextBoxes();
                                GridView1.DataBind();
                            }

                            Response.Write("<script>alert('Book Returned Successfully');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Not SuccessFull');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


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
                        cmd.Parameters.AddWithValue("@member_id", TextBox2.Text.Trim()); // Use TextBox2 for Member ID

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            return dt.Rows.Count >= 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        private bool CheckBookExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT * FROM book_master_tbl WHERE book_id = @book_id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim()); // Use TextBox1 for Book ID

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            return dt.Rows.Count >= 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }


        private bool CheckBookStock()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT * FROM book_master_tbl WHERE book_id = @book_id AND current_stock > 0";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim()); // Use TextBox1 for Book ID

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            return dt.Rows.Count >= 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        private bool CheckBookIsIssued()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT * FROM book_issue_tbl WHERE book_id = @book_id AND member_id = @member_id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@member_id", TextBox2.Text.Trim());

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            return dt.Rows.Count >= 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }


        private bool CheckEmptyFields()
        {
            if (String.IsNullOrWhiteSpace(TextBox1.Text) || String.IsNullOrWhiteSpace(TextBox2.Text)
                || String.IsNullOrWhiteSpace(TextBox3.Text) || String.IsNullOrWhiteSpace(TextBox4.Text)
                || String.IsNullOrWhiteSpace(TextBox5.Text) || String.IsNullOrWhiteSpace(TextBox6.Text))
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
            TextBox6.Text = string.Empty;
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