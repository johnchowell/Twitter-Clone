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
                Console.Out.WriteLine("1");
                return Page();
            }
            if (Clone.Pages.SiteUser.GetUser(username) == null)
            {
                Clone.Pages.SiteUser.CreateUser(username, password, name);
                Console.Out.WriteLine("2");
                return RedirectToPage("/Index");
            }
            else
            {
                status = "Username already exists";
                Console.Out.WriteLine("3");
                return Page();
            }
        }
    }
}
