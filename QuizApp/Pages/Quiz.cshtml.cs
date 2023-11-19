using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class QuizModel : PageModel
    {
        public List<Quizzes> Quizzes { get; set; }
        public List<Questions> Questions { get; set; }
        private IQuestionsService _service { get; set; }
        public QuizModel(IQuestionsService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            Quizzes = _service.GetAllQuizzes();
            Questions = _service.GetAll();
        }
    }
}
