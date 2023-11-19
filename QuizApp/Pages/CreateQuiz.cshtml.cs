using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class CreateQuizModel : PageModel
    {
        [BindProperty]
        public List<Questions> Questions { get; set; }
        [BindProperty]
        public Quizzes Quizzes { get; set; }
        private IQuestionsService _service { get; set; }
        public CreateQuizModel(IQuestionsService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            Questions = _service.GetAll();
        }
    }
}
