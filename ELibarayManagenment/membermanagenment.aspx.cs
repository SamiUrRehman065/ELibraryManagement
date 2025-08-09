using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ELibarayManagenment
{
    public partial class membermanagenment : System.Web.UI.Page
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
        // go button click 
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in Id field.');</script>");
            }
            else
            {
                GetMemberById();
            }
        }
        //approved button click
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in Id field.');</script>");
            }
            else
            {
                UpdateMemberStatusById("Active");
            }
        }
        //pause button click 
        protected void LinkButton6_Click(object sender, EventArgs e)
        {

            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in Id field.');</script>");
            }
            else
            {
                UpdateMemberStatusById("Pending");
            }
        }
        // deactivate button click 
        protected void LinkButton7_Click(object sender, EventArgs e)
        {

            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in Id field.');</script>");
            }
            else
            {
                UpdateMemberStatusById("Deactivate");
            }
        }
        // delete button click 
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please fill in Id field.');</script>");
            }
            else
            {
                DeleteMemberById();
            }
        }


        private void GetMemberById()
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
                        cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());

                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                while (Reader.Read())
                                {
                                    TextBox2.Text = Reader.GetValue(0).ToString();
                                    TextBox5.Text = Reader.GetValue(10).ToString();
                                    TextBox11.Text = Reader.GetValue(6).ToString();
                                    TextBox8.Text = Reader.GetValue(1).ToString();
                                    TextBox3.Text = Reader.GetValue(2).ToString();
                                    TextBox4.Text = Reader.GetValue(3).ToString();
                                    TextBox9.Text = Reader.GetValue(4).ToString();
                                    TextBox10.Text = Reader.GetValue(5).ToString();
                                    TextBox6.Text = Reader.GetValue(7).ToString();
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid Member Id');</script>");
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

        private void UpdateMemberStatusById(String Status)
        {
            try
            {
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "UPDATE member_master_tbl SET account_status = @account_status WHERE member_id = @member_id ";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@account_status", Status);


                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Status Updated SuccessFully');</script>");

                            GridView1.DataBind();
                            GetMemberById();
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

        private void DeleteMemberById()
        {

            try
            {
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "DELETE FROM member_master_tbl WHERE member_id = @member_id";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());



                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Member Deleted SuccessFully');</script>");
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
        private bool CheckEmptyFields()
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(TextBox1.Text))
            {
                return true;
            }
            return false;
        }

        private void ClearTextBoxes()
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox5.Text = string.Empty;
            TextBox11.Text = string.Empty;
            TextBox8.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox9.Text = string.Empty;
            TextBox10.Text = string.Empty;
            TextBox6.Text = string.Empty;
        }
    }
}