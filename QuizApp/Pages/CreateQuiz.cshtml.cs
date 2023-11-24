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
        public Questions Question { get; set; }
        
        [BindProperty]
        public Quizzes Quizzes { get; set; }
        [BindProperty]
        public List<int> QuestionId { get; set; }
        private IQuestionsService _service { get; set; }
        public CreateQuizModel(IQuestionsService service)
        {
            _service = service;
        }
        public IActionResult OnGet()
        {
            string storeUsername = HttpContext.Request.Cookies["Username"];
            if (storeUsername == null)
            {
                return Redirect("/Login");
            }
            Questions = _service.GetAll();
            return Page();
        }
        public IActionResult OnPost()
        {
            var value = $"{Quizzes?.QuizId} - {Quizzes?.Title} - {Quizzes?.Description} - {Quizzes?.CreatedBy} -{QuestionId.ToList()}";
            string storeUsername = HttpContext.Request.Cookies["Username"];
            Quizzes.CreatedBy = storeUsername;
            _service.AddQuiz(Quizzes);

            //Questions.QuizId = new List<int>(new int[QuestionId.Count]);

            foreach (var questionid in QuestionId) 
            {
                Question = _service.GetById(questionid);
                Question.QuizId.Add(Quizzes.QuizId);
                _service.Edit(Question);

                //Quizzes.Questions.Add(question);
            }
            //_service.AddQuiz(Quizzes);
            return Redirect("/Quiz");
        }
    }
}
