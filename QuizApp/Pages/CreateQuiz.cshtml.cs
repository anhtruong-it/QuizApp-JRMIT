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
        [BindProperty]
        public List<int> QuestionId { get; set; }
        private IQuestionsService _service { get; set; }
        public CreateQuizModel(IQuestionsService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            Questions = _service.GetAll();
        }
        public IActionResult OnPost()
        {
            var value = $"{Quizzes?.QuizId} - {Quizzes?.Title} - {Quizzes?.Description} - {Quizzes?.CreatedBy} -{QuestionId.ToList()}";
            Quizzes.QuestionId = new List<int>(new int[QuestionId.Count]);

            for (var i=0; i < QuestionId.Count; i++ )
            {
                Quizzes.QuestionId[i] = QuestionId[i];
                //Quizzes.Questions.Add(question);
            }
            _service.AddQuiz(Quizzes);
            return Redirect("/Quiz");
        }
    }
}
