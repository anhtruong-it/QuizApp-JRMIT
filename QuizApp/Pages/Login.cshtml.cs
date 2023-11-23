using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Users User { get; set; }
        public Users Users { get; set; }
        public bool Authentication { get; set; } = false;
        private IQuestionsService _service { get; set; }
        public LoginModel(IQuestionsService service)
        {
            _service = service;
        }


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            Users = _service.GetUserByUserName(User.Username);
            if (Users != null) 
            {
                Authentication = BCrypt.Net.BCrypt.Verify(User.PasswordHash, Users.PasswordHash);

                if (Authentication)
                {
                    Response.Cookies.Append("Username", User.Username);
                    return Redirect("/Quiz");
                }
            }
            return Page();
        }
    }
}