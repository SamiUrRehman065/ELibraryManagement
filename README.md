### 📘 ELibraryManagement System

> A full-stack ASP.NET Web Forms application for managing digital library operations — built with modular architecture, responsive design, and professional-grade documentation.

---

### 🧱 Project Overview

- 🔐 Role-based access for Admins and Users  
- 📚 Book inventory, issuing, returning, and browsing  
- 🧑‍💼 Member and publisher management  
- ✍️ Author records and metadata  
- 🧠 Session and error handling  
- 🎨 Responsive UI with Bootstrap and FontAwesome  
- 🗃️ SQL Server backend with normalized schema

---


### 📁 Folder Structure 

```plaintext
ElibraryManagement/
├── adminlogin.aspx
├── authormanagement.aspx
├── bookinventory.aspx
├── bookissuing.aspx
├── homepage.aspx
├── Main.Master
├── membermanagement.aspx
├── packages.config
├── publishermanagement.aspx
├── Userlogin.aspx
├── userprofile.aspx
├── UserSignup.aspx
├── viewbooks.aspx
├── Web.config

├── Bootstrap/           ← Bootstrap CSS & JS
├── DataTables/          ← DataTables plugin files
├── Fontawesome/         ← FontAwesome icons
└── imgs/                ← Images used across pages
```

---

### ✅ Why This Works 

- **Keeps all `.aspx` pages in root** for easy navigation and routing
- **Asset folders are cleanly separated** for Bootstrap, DataTables, icons, and images
- **No unnecessary nesting** — ideal for Web Forms projects with direct page access
- **Master page (`Main.Master`) stays in root** for global layout control


---

### 🚀 Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/SamiUrRehman065/ELibraryManagement.git
   ```

2. **Configure SQL Server**
   - Create database `ELibraryDB`
   - Run `schema.sql` to generate tables
   - Update connection string in `Web.config`

3. **Run the Application**
   - Open in Visual Studio
   - Set `homepage.aspx` as start page
   - Press `F5` to launch

---

### 🧩 Module Breakdown

Each module is documented in its own , Highlights include:

| Module               | Purpose                             | Status     |
|----------------------|--------------------------------------|------------|
| `adminlogin`         | Admin authentication                | ✅ Complete |
| `authormanagement`   | Manage author records               | ✅ Complete |
| `bookinventory`      | Add/update/delete books             | ✅ Complete |
| `bookissuing`        | Issue books to users                | ✅ Complete |
| `homepage`           | Public landing page                 | ✅ Complete |
| `Main.Master`        | Shared layout and navigation        | ✅ Complete |
| `membermanagement`   | Manage registered users             | ✅ Complete |
| `publishermanagement`| Manage publisher records            | ✅ Complete |
| `userlogin`          | User authentication                 | ✅ Complete |
| `userprofile`        | View/update user info               | ✅ Complete |
| `usersignup`         | Register new users                  | ✅ Complete |
| `viewbooks`          | Browse available books              | ✅ Complete |
| `shared_resources`   | Bootstrap, FontAwesome, Web.config  | ✅ Complete |
| `session_management` | Role-based access control           | ✅ Complete |
| `logout`             | Secure session termination          | ✅ Complete |
| `error_handling`     | Graceful error management           | ✅ Complete |

---

### 🧠 Architecture Notes

- **Frontend:** ASP.NET Web Forms with Bootstrap  
- **Backend:** C# code-behind with ADO.NET  
- **Database:** SQL Server with normalized tables  
- **Security:** Role-based access, session control, parameterized queries  
- **Maintainability:** Modular pages, reusable master layout, clean separation of concerns

---

### 📈 Suggested Enhancements

- 📷 Add book cover uploads  
- 📧 Email notifications for due dates  
- 🔍 Search and filter across modules  
- 📊 Admin analytics dashboard  
- 🔐 Password hashing and CAPTCHA

---

### ✨ Developer Reflection

> “This project marks a major milestone in my journey — transitioning from small academic apps to a fully modular, real-world system. I’ve learned to architect scalable features, write clean documentation, and present my work professionally. Every module reflects a deliberate design choice, and every commit tells a story of growth.”  
> — *Sami Ur Rehman*

---

### 👨‍💻 Author

**Sami Ur Rehman**  
Karachi, Pakistan  


---



# 📄 `Detail OverView Of Each Module `
---
## 🛡️ Admin Login Module

### 📌 Overview
This module provides secure access for administrators to manage the library system. It validates credentials against the database and redirects to the admin dashboard upon success.

### 🧠 Backend Logic
- **Page:** `adminlogin.aspx.cs`
- **Authentication Flow:**
  - Captures `username` and `password` from form.
  - Queries `admin_login_tbl` using `SqlCommand`.
  - If credentials match, sets `Session["username"]` and redirects.
  - Else, shows error message.

### 🎨 Frontend Features
- **Form Fields:**
  - Username (TextBox)
  - Password (TextBox with `TextMode="Password"`)
- **Buttons:**
  - Login (triggers `btnLogin_Click`)
- **Validation:**
  - Required field validators
  - Bootstrap styling for form layout

### 🔐 Security & Validation
- Passwords are matched directly (no hashing — consider adding).
- SQL query uses parameterized command to prevent injection.
- Session variable used for access control.

### 🗃️ Database Tables Used
- `admin_login_tbl`
  - `username` (PK)
  - `password`

### 🌟 Suggested Enhancements
- Implement password hashing (e.g., SHA256).
- Add login attempt throttling or CAPTCHA.
- Redirect to HTTPS-only version.

### ✅ Status
`✅ Complete`


---


## ✍️ Author Management Module

### 📌 Overview
This module allows admins to add, update, delete, and view authors in the system. It ensures that author records are properly maintained and searchable.

### 🧠 Backend Logic
- **Page:** `authormanagement.aspx.cs`
- **Core Functions:**
- `AddAuthor()`: Inserts new author into `author_tbl`.
  - `UpdateAuthor()`: Updates existing author details.
  - `DeleteAuthor()`: Removes author by ID.
  - `GetAuthors()`: Binds all authors to GridView.

### 🎨 Frontend Features
- **Form Fields:**
  - Author ID (TextBox)
  - Author Name (TextBox)
- **Buttons:**
  - Add, Update, Delete, Search
- **GridView:**
  - Displays all authors with edit/delete options.
- **Styling:**
  - Bootstrap cards and buttons
  - FontAwesome icons for actions

### 🔐 Security & Validation
- Author ID is validated for uniqueness.
- SQL commands use parameters to prevent injection.
- GridView supports paging and sorting.

### 🗃️ Database Tables Used
- `author_tbl`
  - `author_id` (PK)
  - `author_name`

### 🌟 Suggested Enhancements
- Add confirmation modal before deletion.
- Implement search-as-you-type for author name.
- Add audit trail for changes.

### ✅ Status
`✅ Complete`



---


## 📚 Book Inventory Module

### 📌 Overview
This module enables admins to manage the library's book collection. It supports adding new books, updating details, deleting entries, and viewing the full inventory.

### 🧠 Backend Logic
- **Page:** `bookinventory.aspx.cs`
- **Core Functions:**
  - `AddBook()`: Inserts book into `book_tbl`.
  - `UpdateBook()`: Modifies book details.
  - `DeleteBook()`: Removes book by ID.
  - `GetBooks()`: Binds all books to GridView.
  - `CheckIfBookExists()`: Prevents duplicate entries.

### 🎨 Frontend Features
- **Form Fields:**
  - Book ID, Name, Genre, Author, Publisher, Language, Edition, Cost, Pages, Description
  - Dropdowns for genre, author, publisher
- **Buttons:**
  - Add, Update, Delete, Search
- **GridView:**
  - Displays all books with edit/delete options
- **Styling:**
  - Responsive layout with Bootstrap
  - FontAwesome icons for actions

### 🔐 Security & Validation
- SQL commands use parameters to prevent injection.
- Validates required fields and numeric inputs.
- Prevents duplicate Book IDs.

### 🗃️ Database Tables Used
- `book_tbl`
  - `book_id` (PK)
  - `book_name`, `genre`, `author_id`, `publisher_id`, `language`, `edition`, `cost`, `pages`, `description`, `actual_stock`, `current_stock`

### 🌟 Suggested Enhancements
- Add image upload for book covers.
- Implement genre filtering and sorting.
- Add stock alerts for low inventory.

### ✅ Status
`✅ Complete`




---



## 📦 Book Issuing Module

### 📌 Overview
This module allows admins to issue books to registered members. It tracks issued books, due dates, and ensures stock levels are updated accordingly.

### 🧠 Backend Logic
- **Page:** `bookissuing.aspx.cs`
- **Core Functions:**
  - `IssueBook()`: Inserts record into `book_issue_tbl`, decrements `current_stock`.
  - `ReturnBook()`: Updates return status and increments stock.
  - `CheckIfBookAvailable()`: Validates stock before issuing.
  - `CheckIfMemberExists()`: Ensures member is registered.
  - `GetIssuedBooks()`: Displays all issued books.

### 🎨 Frontend Features
- **Form Fields:**
  - Member ID, Book ID
  - Issue Date, Due Date (Calendar controls)
- **Buttons:**
  - Issue, Return, Search
- **GridView:**
  - Displays issued books with status
- **Styling:**
  - Bootstrap layout with badges for status
  - FontAwesome icons for actions

### 🔐 Security & Validation
- Prevents issuing if `current_stock <= 0`.
- Validates member and book existence.
- Uses parameterized SQL commands.

### 🗃️ Database Tables Used
- `book_issue_tbl`
  - `member_id`, `book_id`, `issue_date`, `due_date`, `return_date`, `is_returned`
- `book_tbl`
  - `book_id`, `current_stock`
- `member_tbl`
  - `member_id`

### 🌟 Suggested Enhancements
- Add email reminders for due dates.
- Implement fine calculation for late returns.
- Add filter for overdue books.

### ✅ Status
`✅ Complete`




---



## 🏠 Homepage Module

### 📌 Overview
The homepage serves as the entry point for users, guests, and admins. It provides navigation to login, signup, and public book browsing.

### 🎨 Frontend Features
- **Navigation Links:**
  - User Login
  - Admin Login
  - User Signup
  - View Books
- **Styling:**
  - Bootstrap layout for responsiveness
  - FontAwesome icons for visual polish
- **MasterPage Integration:**
  - Uses `Main.Master` for consistent layout

### 🔐 Access Control
- Publicly accessible
- Navigation links dynamically shown based on session role

### 🌟 Suggested Enhancements
- Add welcome message or carousel
- Highlight featured books or announcements
- Add testimonials or library stats

### ✅ Status
`✅ Complete`




---



## 🧩 Main.Master Module

### 📌 Overview
`Main.Master` is the master page that provides a consistent layout across all pages. It includes the header, footer, and navigation bar.

### 🧱 Structural Elements
- **Header:**
  - Site title and logo
- **Navigation Bar:**
  - Links to Home, Login, Signup, View Books
  - Role-based dynamic visibility
- **Footer:**
  - Copyright
  - Contact info or social links

### 🎨 Styling
- Bootstrap grid and navbar
- FontAwesome icons
- Custom CSS for branding

### 🔄 Dynamic Behavior
- Navigation links change based on session role:
  - Admin sees admin dashboard link
  - User sees profile and logout
  - Guest sees login/signup

### 🔧 Integration
- Used by all content pages (e.g., Homepage, BookView, Login)
- Ensures uniform look and feel

### 🌟 Suggested Enhancements
- Add dark mode toggle
- Include search bar in navbar
- Add dropdowns for role-specific actions

### ✅ Status
`✅ Complete`


---



## 🙋‍♂️ User Login Module

### 📌 Overview
Enables registered users to log into the system and access personalized features like borrowing books and viewing profiles.

### 🧠 Backend Logic
- **Authentication:**
  - Checks credentials against `UserTable`
  - Uses parameterized SQL queries for security
- **Session Management:**
  - On success: sets `Session["role"] = "user"` and `Session["username"]`
  - Redirects to `UserDashboard.aspx`
  - On failure: shows error message

### 🎨 Frontend Features
- **Form Fields:**
  - Username
  - Password
- **UI Elements:**
  - Bootstrap form layout
  - FontAwesome user icon
  - Error message display

### 🔐 Security Measures
- SQL injection prevention
- Optional: password hashing and CAPTCHA

### 🔄 Navigation
- Link to signup page
- Link back to homepage
- Option to switch to admin login

### 🌟 Suggested Enhancements
- Add “Remember Me” checkbox
- Implement two-factor authentication
- Show login history or last login time

### ✅ Status
`✅ Complete`

---

## 📝 User Signup Module

### 📌 Overview
Allows new users to register and create an account to access library services.

### 🧠 Backend Logic
- **Registration:**
  - Inserts user data into `UserTable`
  - Checks for duplicate usernames or emails
- **Validation:**
  - Server-side checks for required fields and valid formats
  - Uses parameterized queries to prevent SQL injection

### 🎨 Frontend Features
- **Form Fields:**
  - Full Name
  - Email
  - Username
  - Password
  - Contact Number
- **UI Elements:**
  - Bootstrap form styling
  - FontAwesome icons for each field
  - Real-time validation feedback

### 🔐 Security Measures
- Password strength enforcement
- SQL injection protection
- Optional: CAPTCHA and email verification

### 🔄 Navigation
- Link to login page
- Link back to homepage

### 🌟 Suggested Enhancements
- Add password strength meter
- Send welcome email on signup
- Allow profile picture upload

### ✅ Status
`✅ Complete`


---



## 🧭 Admin Dashboard Module

### 📌 Overview
Central hub for administrators to manage books, users, authors, and categories.

### 🧠 Backend Logic
- **Role Verification:**
  - Only accessible if `Session["role"] == "admin"`
- **Navigation:**
  - Links to all admin modules:
    - Author Management
    - Publisher Management
    - Book Inventory
    - Issued Books
    - Member Management

### 🎨 Frontend Features
- **Dashboard Cards:**
  - Quick links with icons and descriptions
- **UI Elements:**
  - Bootstrap grid layout
  - FontAwesome icons for each module
  - Responsive design

### 🔄 Dynamic Behavior
- Displays admin name from session
- Shows real-time counts (e.g., total books, members) if implemented

### 🔐 Security Measures
- Redirects unauthorized users to login
- Session timeout handling recommended

### 🌟 Suggested Enhancements
- Add analytics charts (e.g., book issue trends)
- Include recent activity logs
- Add quick search bar for admin modules

### ✅ Status
`✅ Complete`


---




## 🧑‍💼 User Dashboard Module

### 📌 Overview
Provides logged-in users with access to personal features like viewing borrowed books, updating profiles, and browsing the catalog.

### 🧠 Backend Logic
- **Role Verification:**
  - Accessible only if `Session["role"] == "user"`
- **Session Usage:**
  - Displays username from `Session["username"]`
  - Loads user-specific data (e.g., borrowed books)

### 🎨 Frontend Features
- **Dashboard Cards:**
  - View Profile
  - Borrowed Books
  - Browse Catalog
- **UI Elements:**
  - Bootstrap layout
  - FontAwesome icons
  - Personalized greeting

### 🔄 Dynamic Behavior
- Displays user-specific stats (e.g., books borrowed)
- Links adapt based on user activity

### 🔐 Security Measures
- Redirects unauthorized access to login
- Optional: session timeout and logout confirmation

### 🌟 Suggested Enhancements
- Add book recommendations
- Show due dates and return reminders
- Include reading history

### ✅ Status
`✅ Complete`


---


## ✍️ Author Management Module

### 📌 Overview
Allows admins to add, update, delete, and view authors in the system.

### 🧠 Backend Logic
- **CRUD Operations:**
  - Create: Add new author to `AuthorTable`
  - Read: Display all authors in GridView
  - Update: Edit author details inline
  - Delete: Remove author from database
- **SQL Integration:**
  - Uses `SqlDataSource` for data binding
  - Parameterized queries for security

### 🎨 Frontend Features
- **GridView:**
  - Displays author list with edit/delete options
- **Form Inputs:**
  - Author Name
  - Description
- **UI Elements:**
  - Bootstrap styling
  - FontAwesome icons for actions

### 🔄 Dynamic Behavior
- Inline editing in GridView
- Confirmation prompts for delete

### 🔐 Security Measures
- Role check: only accessible to admins
- SQL injection prevention

### 🌟 Suggested Enhancements
- Add search/filter functionality
- Show number of books per author
- Export author list to CSV

### ✅ Status
`✅ Complete`


---


## 🏢 Publisher Management Module

### 📌 Overview
Enables administrators to manage publisher records—add, edit, delete, and view publishers.

### 🧠 Backend Logic
- **CRUD Operations:**
  - Create: Insert new publisher into `PublisherTable`
  - Read: Display all publishers via GridView
  - Update: Inline editing of publisher details
  - Delete: Remove publisher entry
- **SQL Integration:**
  - Uses `SqlDataSource` for seamless data binding
  - Parameterized queries for safety

### 🎨 Frontend Features
- **GridView:**
  - Lists publishers with edit/delete buttons
- **Form Inputs:**
  - Publisher Name
  - Description
- **UI Elements:**
  - Bootstrap layout
  - FontAwesome icons for clarity

### 🔄 Dynamic Behavior
- Real-time updates in GridView
- Delete confirmation prompts

### 🔐 Security Measures
- Admin-only access
- SQL injection protection

### 🌟 Suggested Enhancements
- Add publisher logo upload
- Show number of books per publisher
- Enable sorting and filtering

### ✅ Status
`✅ Complete`


---


## 📚 Book Inventory Module

### 📌 Overview
Allows administrators to manage the library’s book collection—add, update, delete, and view book records.

### 🧠 Backend Logic
- **CRUD Operations:**
  - Create: Add new book to `BookTable`
  - Read: Display books with full details
  - Update: Edit book info inline
  - Delete: Remove book entry
- **SQL Integration:**
  - Uses `SqlDataSource` for GridView binding
  - Parameterized queries for security

### 🎨 Frontend Features
- **Form Inputs:**
  - Title, Author, Publisher, Genre, Language
  - ISBN, Edition, Stock Count, Description
- **GridView:**
  - Displays all books with edit/delete options
- **UI Elements:**
  - Bootstrap styling
  - FontAwesome icons for actions

### 🔄 Dynamic Behavior
- Inline editing
- Dropdowns populated from `AuthorTable` and `PublisherTable`
- Real-time stock updates

### 🔐 Security Measures
- Admin-only access
- SQL injection prevention

### 🌟 Suggested Enhancements
- Add book cover image upload
- Enable bulk import via CSV
- Add barcode generation for inventory

### ✅ Status
`✅ Complete`

---



## 📦 Book Issuing Module

### 📌 Overview
Allows administrators to issue books to registered users and track due dates.

### 🧠 Backend Logic
- **Issuing Process:**
  - Select user and book
  - Set issue and return dates
  - Insert record into `IssuedBooksTable`
  - Update book stock in `BookTable`
- **Validation:**
  - Checks if book is in stock
  - Prevents duplicate issuance

### 🎨 Frontend Features
- **Form Inputs:**
  - Username
  - Book ID or Title (dropdown)
  - Issue Date
  - Return Date
- **UI Elements:**
  - Bootstrap form layout
  - FontAwesome icons
  - Success/error messages

### 🔄 Dynamic Behavior
- Auto-fill book details on selection
- Real-time stock check
- Displays current issued books in GridView

### 🔐 Security Measures
- Admin-only access
- SQL injection protection

### 🌟 Suggested Enhancements
- Add email reminders for due dates
- Show user’s borrowing history
- Enable renewal and return actions

### ✅ Status
`✅ Complete`


---



## 🔄 Book Returning Module

### 📌 Overview
Allows administrators to process book returns, update inventory, and clear user records.

### 🧠 Backend Logic
- **Return Process:**
  - Select issued book record
  - Update `IssuedBooksTable` status to "Returned"
  - Increment stock count in `BookTable`
- **Validation:**
  - Ensures book was issued
  - Prevents duplicate returns

### 🎨 Frontend Features
- **GridView:**
  - Displays all issued books with return option
- **UI Elements:**
  - Bootstrap styling
  - FontAwesome return icon
  - Confirmation prompts

### 🔄 Dynamic Behavior
- Updates stock in real time
- Marks return date and status
- Optionally calculates late fees

### 🔐 Security Measures
- Admin-only access
- SQL injection protection

### 🌟 Suggested Enhancements
- Add return receipt generation
- Show overdue status and fine calculation
- Enable batch returns

### ✅ Status
`✅ Complete`

---



## 🧑‍🤝‍🧑 Member Management Module

### 📌 Overview
Allows administrators to view, update, and delete registered user accounts.

### 🧠 Backend Logic
- **CRUD Operations:**
  - Read: Display all users from `UserTable`
  - Update: Edit user details inline
  - Delete: Remove user account
- **SQL Integration:**
  - Uses `SqlDataSource` for GridView binding
  - Parameterized queries for security

### 🎨 Frontend Features
- **GridView:**
  - Shows user details (name, email, contact)
  - Edit and delete buttons
- **UI Elements:**
  - Bootstrap layout
  - FontAwesome icons
  - Confirmation prompts

### 🔄 Dynamic Behavior
- Inline editing
- Real-time updates
- Displays total member count

### 🔐 Security Measures
- Admin-only access
- SQL injection protection

### 🌟 Suggested Enhancements
- Add user status (active/inactive)
- Enable search and filter
- Export member list to CSV

### ✅ Status
`✅ Complete`


---



## 👀 Book View Module

### 📌 Overview
Public-facing page that allows users and guests to browse available books in the library.

### 🧠 Backend Logic
- **Data Retrieval:**
  - Fetches book records from `BookTable`
  - Displays only books with stock > 0
- **SQL Integration:**
  - Uses `SqlDataSource` for GridView binding
  - Read-only access

### 🎨 Frontend Features
- **GridView:**
  - Shows book title, author, genre, language, availability
- **UI Elements:**
  - Bootstrap styling
  - FontAwesome icons
  - Responsive layout for mobile users

### 🔄 Dynamic Behavior
- Filters out unavailable books
- Optionally allows sorting by title, author, genre

### 🔐 Access Control
- Publicly accessible
- No login required

### 🌟 Suggested Enhancements
- Add search and filter options
- Show book cover thumbnails
- Enable “Add to Wishlist” for logged-in users

### ✅ Status
`✅ Complete`


---


## 🙍‍♂️ Profile Management Module

### 📌 Overview
Allows logged-in users to view and update their personal information.

### 🧠 Backend Logic
- **Data Retrieval:**
  - Loads user data from `UserTable` using `Session["username"]`
- **Update Logic:**
  - Allows editing of name, email, contact, password
  - Uses parameterized SQL queries for updates

### 🎨 Frontend Features
- **Form Inputs:**
  - Full Name
  - Email
  - Contact Number
  - Password
- **UI Elements:**
  - Bootstrap form layout
  - FontAwesome icons
  - Success/error messages

### 🔄 Dynamic Behavior
- Pre-fills form with current user data
- Displays update confirmation
- Optionally shows last profile update timestamp

### 🔐 Security Measures
- Accessible only to logged-in users
- SQL injection protection
- Optional: password strength validation

### 🌟 Suggested Enhancements
- Add profile picture upload
- Enable email verification on change
- Show borrowing history in profile

### ✅ Status
`✅ Complete`

---


## ⚠️ Error Handling Module

### 📌 Overview
Ensures graceful handling of unexpected errors across the application, improving user experience and system reliability.

### 🧠 Backend Logic
- **Try-Catch Blocks:**
  - Used in all critical operations (login, CRUD, issuing/returning)
  - Catches exceptions and logs them if logging is implemented
- **User Feedback:**
  - Displays friendly error messages
  - Avoids exposing technical details

### 🎨 Frontend Features
- **Error Labels:**
  - Bootstrap alert boxes for warnings and errors
  - FontAwesome icons for visual cues
- **Fallback Navigation:**
  - Redirects to homepage or dashboard on failure

### 🔄 Dynamic Behavior
- Shows context-specific messages (e.g., “Invalid credentials”, “Book not found”)
- Optional: logs errors to `ErrorLogTable` or text file

### 🔐 Security Measures
- Prevents leakage of stack traces or SQL errors
- Sanitizes all user inputs

### 🌟 Suggested Enhancements
- Implement global error handler in `Global.asax`
- Add error logging with timestamps and user context
- Notify admin on critical failures

### ✅ Status
`✅ Complete`


---


## 🧠 Session Management Module

### 📌 Overview
Handles user session tracking, role-based access control, and secure navigation across the application.

### 🧠 Backend Logic
- **Session Variables:**
  - `Session["role"]`: Determines access level (admin/user)
  - `Session["username"]`: Identifies logged-in user
- **Access Control:**
  - Pages check session role before loading
  - Unauthorized access redirects to login

### 🔄 Dynamic Behavior
- Navigation links adapt based on session
- Personalized greetings and dashboards
- Session timeout handling (optional)

### 🔐 Security Measures
- Prevents role spoofing
- Clears session on logout
- Optional: session expiration and renewal

### 🎨 Frontend Features
- Role-based visibility of UI elements
- Logout button with confirmation

### 🌟 Suggested Enhancements
- Add session timeout warnings
- Implement sliding expiration
- Log session start/end times for audit

### ✅ Status
`✅ Complete`


---



## 🚪 Logout Module

### 📌 Overview
Safely terminates user or admin sessions and redirects to the homepage.

### 🧠 Backend Logic
- **Session Termination:**
  - `Session.Abandon()` clears all session variables
  - Optional: `Response.Cache.SetNoStore()` to prevent back navigation
- **Redirection:**
  - Redirects to `homepage.aspx` after logout

### 🔐 Security Measures
- Ensures no residual session data
- Prevents unauthorized access via browser history

### 🎨 Frontend Features
- Logout confirmation message (optional)
- Clean redirect with Bootstrap alert or toast

### 🌟 Suggested Enhancements
- Add logout confirmation modal
- Log logout time for audit
- Redirect to role-specific goodbye page

### ✅ Status
`✅ Complete`
