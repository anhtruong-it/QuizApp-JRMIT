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
        public bool Logined { get; set; } = false;

        private IQuestionsService _service { get; set; }
        public LoginModel(IQuestionsService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            // Check user logined or not
            string storeUsername = HttpContext.Request.Cookies["Username"];
            if (storeUsername != null)
            {
                Logined = true;
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            // Handle loin and logout forms
            var formId = HttpContext.Request.Form["action"];

            if (formId == "login")
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
            }
            else
            {
                Response.Cookies.Delete("Username");
            }

            return Page();
        }
    }
}