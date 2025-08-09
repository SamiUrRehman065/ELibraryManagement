using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ELibarayManagenment
{
    public partial class authormanagenment : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || !Session["role"].Equals("admin"))
            {
                Session["RedirectAfterLogin"] = Request.Url.AbsoluteUri;
                Response.Redirect("adminlogin.aspx");  // Redirect to the admin login page
            }
            GridView1.DataBind();
        }
        // ADD BUTTON CLICK
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in all fields.');</script>");
            }
            else if (CheckAuthorExists())
            {
                Response.Write("<script>alert('Invalid Publisher Id. ');</script>");
            }
            else
            {
                AddNewAuthor();
            }
        }
        //UPDATE BUTTON CLICK
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in all fields.');</script>");
            }
            else
            {
                bool authorExists = CheckAuthorExists();
                if (authorExists)
                {
                    UpdateAuthor();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Author Id .');</script>");
                }

            }


        }
        //DELETE BUTTON CLICK
        protected void Button4_Click(object sender, EventArgs e)
        {

            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in all fields.');</script>");
            }
            else
            {
                bool authorExists = CheckAuthorExists();
                if (authorExists)
                {
                    DeleteAuthor();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Author Id .');</script>");
                }

            }
        }
        //GO BUTTON CLICK
        protected void Button1_Click(object sender, EventArgs e)
        {

            GetAuthorById();



        }
        private void AddNewAuthor()
        {
            try
            {
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "INSERT INTO author_master_tbl(author_id , author_name) VALUES (@author_id, @author_name)";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());


                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Author Added SuccessFully');</script>");
                            ClearTextBoxes();
                            GridView1.DataBind();
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

        private void UpdateAuthor()
        {
            try
            {
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "UPDATE author_master_tbl SET author_name = @author_name WHERE author_id = @author_id ";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());


                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Author Updated SuccessFully');</script>");
                            ClearTextBoxes();
                            GridView1.DataBind();
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

        private void DeleteAuthor()
        {

            try
            {
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "DELETE FROM author_master_tbl WHERE author_id = @author_id";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());



                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Author Deleted SuccessFully');</script>");
                            ClearTextBoxes();
                            GridView1.DataBind();

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

        private void ClearTextBoxes()
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
        }

        private bool CheckEmptyFields()
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(TextBox1.Text) ||
                string.IsNullOrWhiteSpace(TextBox2.Text))
            {
                return true;
            }
            return false;
        }

        private bool CheckAuthorExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT * FROM author_master_tbl WHERE author_id = @author_id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());

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

        private void GetAuthorById()
        {
            try
            {

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT * FROM author_master_tbl WHERE author_id = @author_id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count >= 1)
                            {
                                TextBox2.Text = dt.Rows[0][1].ToString();
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid Author Id');</script>");
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
    }
}