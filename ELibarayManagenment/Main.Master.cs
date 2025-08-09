using System;

namespace ELibarayManagenment
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["role"] == null)
                {
                    Login.Visible = true; // user login link button
                    SignUp.Visible = true; // sign up link button

                    Logout.Visible = false; // logout link button
                    Hellouser.Visible = false; // hello user link button


                    AdminLogin.Visible = true; // admin login link button
                    AuthorManagement.Visible = false; // author management link button
                    PublisherManagement.Visible = false; // publisher management link button
                    BookInventory.Visible = false; // book inventory link button
                    BookIssuing.Visible = false; // book issuing link button
                    MemberManagement.Visible = false; // member management link button

                }
                else if (Session["role"].Equals("user"))
                {
                    Login.Visible = false; // user login link button
                    SignUp.Visible = false; // sign up link button

                    Logout.Visible = true; // logout link button
                    Hellouser.Visible = true; // hello user link button
                    Hellouser.Text = "Hello " + Session["fullname"].ToString();


                    AdminLogin.Visible = false; // admin login link button
                    AuthorManagement.Visible = false; // author management link button
                    PublisherManagement.Visible = false; // publisher management link button
                    BookInventory.Visible = false; // book inventory link button
                    BookIssuing.Visible = false; // book issuing link button
                    MemberManagement.Visible = false; // member management link button
                }
                else if (Session["role"].Equals("admin"))
                {
                    Login.Visible = false; // user login link button
                    SignUp.Visible = false; // sign up link button

                    Logout.Visible = true; // logout link button
                    Hellouser.Visible = true; // hello user link button
                    Hellouser.Text = "Hello Admin";


                    AdminLogin.Visible = false; // admin login link button
                    AuthorManagement.Visible = true; // author management link button
                    PublisherManagement.Visible = true; // publisher management link button
                    BookInventory.Visible = true; // book inventory link button
                    BookIssuing.Visible = true; // book issuing link button
                    MemberManagement.Visible = true; // member management link button
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ViewBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewbooks.aspx");
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("Userlogin.aspx");
        }

        protected void SignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserSignup.aspx");
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Login.Visible = true; // user login link button
            SignUp.Visible = true; // sign up link button

            Logout.Visible = false; // logout link button
            Hellouser.Visible = false; // hello user link button


            AdminLogin.Visible = true; // admin login link button
            AuthorManagement.Visible = false; // author management link button
            PublisherManagement.Visible = false; // publisher management link button
            BookInventory.Visible = false; // book inventory link button
            BookIssuing.Visible = false; // book issuing link button
            MemberManagement.Visible = false; // member management link button
            Response.Redirect("homepage.aspx");

        }

        protected void Hellouser_Click(object sender, EventArgs e)
        {
            Response.Redirect("userprofile.aspx");
        }


        protected void AdminLogin_Click1(object sender, EventArgs e)
        {
            Response.Redirect("adminlogin.aspx");
        }

        protected void AuthorManagement_Click1(object sender, EventArgs e)
        {

            Response.Redirect("authormanagenment.aspx");
        }

        protected void PublisherManagement_Click1(object sender, EventArgs e)
        {
            Response.Redirect("publishermanagenment.aspx");
        }

        protected void BookInventory_Click1(object sender, EventArgs e)
        {
            Response.Redirect("bookinventory.aspx");
        }

        protected void BookIssuing_Click1(object sender, EventArgs e)
        {
            Response.Redirect("bookissuing.aspx");
        }

        protected void MemberManagement_Click1(object sender, EventArgs e)
        {
            Response.Redirect("membermanagenment.aspx");
        }

        //login

    }
}