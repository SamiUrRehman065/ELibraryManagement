using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ELibarayManagenment
{
    public partial class publishermanagenment : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || !Session["role"].Equals("admin"))
            {
                Session["RedirectAfterLogin"] = Request.Url.AbsoluteUri;
                Response.Redirect("adminlogin.aspx"); // Redirect to the admin login page
            }
            GridView1.DataBind();
        }
        // add button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in all fields.');</script>");
            }
            else if (CheckPublisherExists())
            {
                Response.Write("<script>alert('Invalid Publisher Id. ');</script>");
            }
            else
            {
                AddNewPublisher();
            }
        }
        // update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in all fields.');</script>");
            }
            else
            {
                bool authorExists = CheckPublisherExists();
                if (authorExists)
                {
                    UpdatePublisher();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Publisher Id .');</script>");
                }

            }
        }
        // delete button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in all fields.');</script>");
            }
            else
            {
                bool authorExists = CheckPublisherExists();
                if (authorExists)
                {
                    DeletePublisher();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Publisher Id .');</script>");
                }

            }
        }
        // go button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            GetPublisherById();
        }



        private void AddNewPublisher()
        {
            try
            {
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "INSERT INTO publisher_master_tbl(publisher_id , publisher_name) VALUES (@publisher_id, @publisher_name)";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());


                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Publisher Added SuccessFully');</script>");
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

        private void UpdatePublisher()
        {
            try
            {
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "UPDATE publisher_master_tbl SET publisher_name = @publisher_name WHERE publisher_id = @publisher_id ";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());


                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Publisher Updated SuccessFully');</script>");
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

        private void DeletePublisher()
        {

            try
            {
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "DELETE FROM publisher_master_tbl WHERE publisher_id = @publisher_id";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());



                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Publisher Deleted SuccessFully');</script>");
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

        private bool CheckPublisherExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT * FROM publisher_master_tbl WHERE publisher_id = @publisher_id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());

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

        private void GetPublisherById()
        {
            try
            {

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT * FROM publisher_master_tbl WHERE publisher_id = @publisher_id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());

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
                                Response.Write("<script>alert('Invalid Publisher Id');</script>");
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