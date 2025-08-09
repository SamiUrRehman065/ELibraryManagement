<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="homepage.cs" Inherits="ELibarayManagenment.homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .background-image {
            position: relative;
            height: 100vh;
            color: white;
            text-align: center;
            display: flex;
            align-items: center;
            justify-content: center;
            overflow: hidden;
        }

        .background-image::before {
                content: "";
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background-image: url('imgs/library-book-bookshelf-read.jpg'); 
                background-size: cover;
                background-position: center;
                filter: blur(2px);
        }

        .heading {
            font-size: 4vw;
        }

        .para {
            font-size: 2.5vw;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid background-image ">
        <div class="row">
            <div class="col-md-12 col-xs-12 col-sm-12">
                <h1 class="fw-bolder heading mt-2">Empowering Digital Learning</h1>
                <p class="fw-bolder para mt-2 ">
                    Seamlessly Manage Your Library Resources Anytime, Anywhere.
                    <br />
                    Access A Vast Collection Of Books And Resources At Your Fingertips
                </p>
                <div class="search-bar d-flex  shadow-sm mx-auto" style="max-width: 400px;">
                    <input type="text" class="form-control flex-grow-1" placeholder="Search books, authors, or categories">
                    <button class="btn btn-primary border-primary rounded ms-2">
                        <i class="fas fa-search"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>



    <section>
        <div class="container ">
            <div class="row">
                <div class="col-12">
                    <center>
                        <h2>Our Features</h2>
                        <p><b>Our 3 Primary Features</b></p>
                    </center>
                </div>
            </div>
            <div class="row justify-content-between">
                <div class="col-md-4  ">
                    <center>
                        <img width="150" src="imgs/digital-inventory.png" />
                        <h4>Digital Book Inventory</h4>
                        <p class="text-justify">Effortlessly manage and update your library's entire book collection digitally. Keep track of every title with ease and precision</p>
                    </center>
                </div>
                <div class="col-md-4 ">
                    <center>
                        <img width="150" src="imgs/search-online.png" />
                        <h4>Search Books</h4>
                        <p class="text-justify">Quickly find and discover books with advanced search capabilities. Explore the vast collection to locate exactly what you need</p>
                    </center>
                </div>
                <div class="col-md-4  ">
                    <center>
                        <img width="150" src="imgs/defaulters-list.png" />
                        <h4>Defaulter List</h4>
                        <p class="text-justify">Track overdue books and manage defaulters efficiently. Stay on top of returns and ensure timely book circulation</p>
                    </center>
                </div>
            </div>
        </div>
    </section>

    <section>
        <img src="imgs/in-homepage-banner.jpg" class="img-fluid" />
    </section>
    <section class="choose-us">
        <div class="container ">
            <div class="row">
                <div class="col-12 text-center ">
                    <h2 class="section-title">Why Choose Us</h2>
                    <p class="section-subtitle">Discover the benefits of our digital library</p>
                </div>
            </div>
            <div class="row text-center">
                <div class="col-md-4 benefit-box ">
                    <center>
                        <div>
                            <img src="imgs/easy-access.png" alt="Easy Access" width="150">
                        </div>
                        <h4>Easy Access</h4>
                        <p class="text-justify">Get access to thousands of books from the comfort of your home. Our platform is available 24/7</p>
                    </center>
                </div>
                <div class="col-md-4 benefit-box  ">
                    <center>
                        <div>
                            <img src="imgs/variety-genres.jpg" alt="Variety of Genres" width="200">
                        </div>
                        <h4>Variety of Genres</h4>
                        <p class="text-justify">We offer a wide range of genres to satisfy every reader's taste. From fiction to academic resources</p>
                    </center>
                </div>
                <div class="col-md-4 benefit-box ">
                    <center>
                        <div>
                            <img src="imgs/user-friendly.jpeg" alt="User Friendly" width="150">
                        </div>
                        <h4>User-Friendly Interface</h4>
                        <p class="text-justify">Our platform is designed with the user in mind. Easy navigation and search options for a seamless experience</p>
                    </center>
                </div>
            </div>
        </div>
    </section>
    <section>
        <img src="imgs/home-bg.jpg" class="img-fluid" />
    </section>

    <section>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <center>
                        <h2>Our Process</h2>
                        <p><b>We have a Simple 3 Step Process</b></p>
                    </center>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <center>
                        <img width="150" src="imgs/sign-up.png" />
                        <h4>Sign Up</h4>
                        <p class="text-justify">Join our library community effortlessly by signing up online. Access a world of books and resources with just a few clicks</p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img width="150" src="imgs/search-online.png" />
                        <h4>Search Books</h4>
                        <p class="text-justify">Easily find and explore a wide range of books using our advanced search tools. Discover exactly what you're looking for in seconds</p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img width="150" src="imgs/library.png" />
                        <h4>Visit Us</h4>
                        <p class="text-justify">Experience our library in person for a richer, more immersive journey. Connect with the physical space and enjoy the benefits of on-site resources</p>
                    </center>
                </div>
            </div>
        </div>
    </section>




</asp:Content>
