using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SQLite;


namespace Clone.Pages
{
    public class LoginModel : PageModel
    {
        public string ?username;
        public string ?password;
        public string ?status;
        public bool LoggedIn;
        public void OnGet()
        {
            LoggedIn = false;
            status = "";
        }
        public IActionResult OnPost()
        {
            username = Request.Form["username-field"];
            password = Request.Form["password-field"];

            

            if (string.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                status = "Please enter a username and password";
                return Page();
            }
            else
            {
                string conString = "Data Source=database.db; version=3";
                using (SQLiteConnection con = new SQLiteConnection(conString))
                {
                    con.Open();
                    string sql = "SELECT * FROM users WHERE username = @name AND password = @password";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.Parameters.AddWithValue("@name", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddDays(1);
                        LoggedIn = true;
                        Response.Cookies.Append("username", username, options);
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        status = "Invalid username or password";
                        return Page();
                    }
                }
            }
        }
        
    }

    
}
