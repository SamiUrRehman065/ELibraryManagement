using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

namespace ELibarayManagenment
{
    public partial class bookinventory : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath;
        static int global_actual_stock, global_current_stock, global_issued_books;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || !Session["role"].Equals("admin"))
            {
                Session["RedirectAfterLogin"] = Request.Url.AbsoluteUri;
                Response.Redirect("adminlogin.aspx"); // Redirect to the admin login page
            }
            if (!Page.IsPostBack)
            {
                FillAuthorPublisherDetails();
            }

            GridView1.DataBind();
        }

        //go button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox1.Text))
            {
                Response.Write("<script>alert('Please Fill In Id Field');</script>");
            }
            else if (CheckBookExists())
            {
                GetBookById();
            }
            else
            {
                Response.Write("<script>alert('Invalid ID!');</script>");
            }
        }

        //add button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields() || !FileUpload1.HasFile)
            {
                Response.Write("<script>alert('Please Fill In All Fields');</script>");
            }
            else if (CheckBookExists())
            {
                Response.Write("<script>alert('Book Exist!');</script>");
            }
            else
            {
                AddNewBook();
            }
        }
        // update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                Response.Write("<script>alert('Please Fill In All Fields');</script>");
            }
            else if (CheckBookExists())
            {
                UpdateBookById();
            }
            else
            {
                Response.Write("<script>alert('Invalid Id!');</script>");
            }
        }
        // delete button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox1.Text))
            {
                Response.Write("<script>alert('Please Fill In Id Field');</script>");
            }
            else if (CheckBookExists())
            {
                DeleteBookById();
            }
            else
            {
                Response.Write("<script>alert('Invalid Id!');</script>");
            }
        }

        private void AddNewBook()
        {
            try
            {
                string genres = "";
                foreach (int i in ListBox1.GetSelectedIndices())
                {
                    genres = genres + ListBox1.Items[i] + ",";
                }

                genres = genres.Remove(genres.Length - 1);

                string filepath = "~/book_inventory/books1.png";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                filepath = "~/book_inventory/" + filename;
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "INSERT INTO book_master_tbl(book_id,book_name,genre,author_name,publisher_name,publish_date,language,edition,book_cost,no_of_pages,book_description,actual_stock,current_stock,book_img_link) values(@book_id,@book_name,@genre,@author_name,@publisher_name,@publish_date,@language,@edition,@book_cost,@no_of_pages,@book_description,@actual_stock,@current_stock,@book_img_link)";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@genre", genres);
                        cmd.Parameters.AddWithValue("@author_name", DropDownList3.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@publish_date", TextBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                        cmd.Parameters.AddWithValue("@book_cost", TextBox10.Text.Trim());
                        cmd.Parameters.AddWithValue("@no_of_pages", TextBox11.Text.Trim());
                        cmd.Parameters.AddWithValue("@book_description", TextBox6.Text.Trim());
                        cmd.Parameters.AddWithValue("@actual_stock", TextBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@current_stock", TextBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@book_img_link", filepath);


                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Book Added SuccessFully');</script>");
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

        private void GetBookById()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string Query = "SELECT * FROM book_master_tbl WHERE book_id = @book_id";
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                        using (SqlDataAdapter Adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            Adapter.Fill(dt);
                            if (dt.Rows.Count >= 1)
                            {
                                TextBox2.Text = dt.Rows[0]["book_name"].ToString();
                                TextBox3.Text = dt.Rows[0]["publish_date"].ToString();
                                TextBox9.Text = dt.Rows[0]["edition"].ToString();
                                TextBox10.Text = dt.Rows[0]["book_cost"].ToString().Trim();
                                TextBox11.Text = dt.Rows[0]["no_of_pages"].ToString().Trim();
                                TextBox4.Text = dt.Rows[0]["actual_stock"].ToString().Trim();
                                TextBox5.Text = dt.Rows[0]["current_stock"].ToString().Trim();
                                TextBox6.Text = dt.Rows[0]["book_description"].ToString();
                                TextBox7.Text = "" + (Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString()) - Convert.ToInt32(dt.Rows[0]["current_stock"].ToString()));

                                DropDownList1.SelectedValue = dt.Rows[0]["language"].ToString().Trim();
                                DropDownList2.SelectedValue = dt.Rows[0]["publisher_name"].ToString().Trim();
                                DropDownList3.SelectedValue = dt.Rows[0]["author_name"].ToString().Trim();
                                imgview.Src = dt.Rows[0]["book_img_link"].ToString();
                                ListBox1.ClearSelection();
                                string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                                for (int i = 0; i < genre.Length; i++)
                                {
                                    for (int j = 0; j < ListBox1.Items.Count; j++)
                                    {
                                        if (ListBox1.Items[j].ToString() == genre[i])
                                        {
                                            ListBox1.Items[j].Selected = true;

                                        }
                                    }
                                }

                                global_actual_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                                global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                                global_issued_books = global_actual_stock - global_current_stock;
                                global_filepath = dt.Rows[0]["book_img_link"].ToString();
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid ID!');</script>");
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

        private void UpdateBookById()
        {
            try
            {
                int actual_stock = Convert.ToInt32(TextBox4.Text.Trim());
                int current_stock = Convert.ToInt32(TextBox4.Text.Trim());

                if (global_actual_stock != actual_stock)
                {
                    if (actual_stock < global_issued_books)
                    {
                        Response.Write("<script>alert('Actual Stock value cannot be less than the Issued books');</script>");
                        return;
                    }
                    else
                    {
                        current_stock = actual_stock - global_issued_books;
                        TextBox5.Text = "" + current_stock;
                    }
                }

                string genres = "";
                foreach (int i in ListBox1.GetSelectedIndices())
                {
                    genres = genres + ListBox1.Items[i] + ",";
                }
                genres = genres.Remove(genres.Length - 1);

                string filepath = "~/book_inventory/books1";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (filename == "" || filename == null)
                {
                    filepath = global_filepath;

                }
                else
                {
                    FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                    filepath = "~/book_inventory/" + filename;
                }
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "UPDATE book_master_tbl set book_name=@book_name, genre=@genre, author_name=@author_name, publisher_name=@publisher_name, publish_date=@publish_date, language=@language, edition=@edition, book_cost=@book_cost, no_of_pages=@no_of_pages, book_description=@book_description, actual_stock=@actual_stock, current_stock=@current_stock, book_img_link=@book_img_link where book_id=@book_id";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@genre", genres);
                        cmd.Parameters.AddWithValue("@author_name", DropDownList3.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@publish_date", TextBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                        cmd.Parameters.AddWithValue("@book_cost", TextBox10.Text.Trim());
                        cmd.Parameters.AddWithValue("@no_of_pages", TextBox11.Text.Trim());
                        cmd.Parameters.AddWithValue("@book_description", TextBox6.Text.Trim());
                        cmd.Parameters.AddWithValue("@actual_stock", actual_stock.ToString());
                        cmd.Parameters.AddWithValue("@current_stock", current_stock.ToString());
                        cmd.Parameters.AddWithValue("@book_img_link", filepath);



                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Book Updated SuccessFully');</script>");
                            GetBookById();
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

        private void DeleteBookById()
        {
            try
            {
                // Using statement ensures connection is closed properly
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string querry = "DELETE FROM book_master_tbl WHERE book_id = @book_id";
                    using (SqlCommand cmd = new SqlCommand(querry, connection))
                    {
                        cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());



                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Book Deleted SuccessFully');</script>");
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

        private void FillAuthorPublisherDetails()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string authorQuery = "SELECT author_name FROM author_master_tbl";
                    using (SqlCommand cmdAuthor = new SqlCommand(authorQuery, con))
                    {
                        using (SqlDataAdapter authorAdapter = new SqlDataAdapter(cmdAuthor))
                        {
                            DataTable dtAuthor = new DataTable();
                            authorAdapter.Fill(dtAuthor);
                            DropDownList3.DataSource = dtAuthor;
                            DropDownList3.DataValueField = "author_name";
                            DropDownList3.DataBind();
                        }
                    }

                    // Filling DropDownList2 with publisher names
                    string publisherQuery = "SELECT publisher_name FROM publisher_master_tbl";
                    using (SqlCommand cmdPublisher = new SqlCommand(publisherQuery, con))
                    {
                        using (SqlDataAdapter publisherAdapter = new SqlDataAdapter(cmdPublisher))
                        {
                            DataTable dtPublisher = new DataTable();
                            publisherAdapter.Fill(dtPublisher);
                            DropDownList2.DataSource = dtPublisher;
                            DropDownList2.DataValueField = "publisher_name";
                            DropDownList2.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");

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

                    string query = "SELECT * FROM book_master_tbl WHERE book_id = @book_id OR book_name = @book_name";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());

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
            // Check if any TextBox is empty
            if (string.IsNullOrWhiteSpace(TextBox1.Text) ||
                string.IsNullOrWhiteSpace(TextBox2.Text) ||
                string.IsNullOrWhiteSpace(TextBox3.Text) ||
                string.IsNullOrWhiteSpace(TextBox4.Text) ||

                string.IsNullOrWhiteSpace(TextBox6.Text) ||

                string.IsNullOrWhiteSpace(TextBox9.Text) ||
                string.IsNullOrWhiteSpace(TextBox10.Text) ||
                string.IsNullOrWhiteSpace(TextBox11.Text))
            {
                return true;
            }

            // Check if any DropDownList is not selected (default is selected index 0)
            if (DropDownList1.SelectedIndex == -1 || // Language
                DropDownList2.SelectedIndex == -1 || // Publisher Name
                DropDownList3.SelectedIndex == -1)   // Author Name
            {
                return true;
            }

            // Check if any item is selected in the ListBox (Genre)
            if (ListBox1.GetSelectedIndices().Length == 0) // No items selected
            {
                return true;
            }


            // If all fields are filled and selections are made
            return false;
        }
        private void ClearTextBoxes()
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox5.Text = string.Empty;
            TextBox11.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox9.Text = string.Empty;
            TextBox10.Text = string.Empty;
            TextBox6.Text = string.Empty;
            TextBox7.Text = string.Empty;
            DropDownList1.SelectedIndex = -1;
            DropDownList2.SelectedIndex = -1;
            DropDownList3.SelectedIndex = -1;
            ListBox1.ClearSelection();
            imgview.Src = "imgs/books.png";
        }


    }
}