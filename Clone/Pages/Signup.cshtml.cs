using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class SignupModel : PageModel
    {
        string status = "";
        public void OnGet()
        {
            status = "";
        }

        public IActionResult OnPost()
        {
            string ?username = Request.Form["username"];
            string ?password = Request.Form["pass1"];
            string ?name = Request.Form["name"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
            {
                status = "Please enter a username, password, and name";
                
                return Page();
            }
            if (Clone.Pages.SiteUser.GetUser(username) == null)
            {
                Clone.Pages.SiteUser.CreateUser(name, username, password);
                
                return RedirectToPage("/Index");
            }
            else
            {
                status = "Username already exists";
                
                return Page();
            }
        }
    }
}
