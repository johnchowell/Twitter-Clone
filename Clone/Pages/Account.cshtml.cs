using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clone.Pages
{
    public class AccountModel : PageModel
    {
        public Clone.Pages.SiteUser? user;
        public bool LoggedIn;
        public void OnGet()
        {
            string ?username = Request.Cookies["username"];
            if (username != null)
            {
                user = SiteUser.GetUser(username);
                LoggedIn = true;
            }
            else
            {
                LoggedIn = false;
            }
        }

        public IActionResult SignOut()
        {
            Response.Cookies.Delete("username");
            return Page();
        }
    }
}
