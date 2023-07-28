using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class AccountModel : PageModel
    {
        public string ?username;
        public Clone.Pages.SiteUser ?user;
        public void OnGet()
        {
            username = Request.Cookies["username"];
            if (username != null)
            {
                user = Clone.Pages.SiteUser.GetUser(username);
                if (user == null)
                {
                    Response.Cookies.Delete("username");
                } 
            }
        }
    }
}
