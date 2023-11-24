using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models;
using QuizApp.Services;
using BCrypt.Net;

namespace QuizApp.Pages
{
    public class RegsiterModel : PageModel
    {
        [BindProperty]
        public Users? User { get; set; }
        public List<Users> Users { get; set; }

        private IQuestionsService _service { get; set; }
        public RegsiterModel(IQuestionsService service)
        {
            _service = service;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Check existing username or email
            Users = _service.GetAllUsers();
            int UserMatch = Users.Count(u => u.Username == User.Username);
            int EmailMatch = Users.Count(u => u.Email == User.Email);

            if (UserMatch == 0 && EmailMatch == 0)
            {
                // Hash password before storing
                string RawPassword = User.PasswordHash;
                string HashedPasword = BCrypt.Net.BCrypt.HashPassword(RawPassword);
                User.PasswordHash = HashedPasword;
                _service.AddUser(User);
                return Redirect("/Quiz");
            }
            else
            {
                return Page();
            }
        }
    }
}
