using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models;
using QuizApp.Services;
using Microsoft.AspNetCore.Http;

namespace QuizApp.Pages
{
    public class QuizModel : PageModel
    {
        public List<Quizzes> Quizzes { get; set; }
        public List<Questions> Questions { get; set; }

        public List<List<int>> QuestionId = new List<List<int>>();

        private IQuestionsService _service { get; set; }
        public QuizModel(IQuestionsService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            Quizzes = _service.GetAllQuizzes();
            
            foreach (var quiz in Quizzes) 
            {
                List<int> id = new List<int>();
                Questions = _service.GetByQuizId(quiz.QuizId);
                
                foreach (var question in Questions)
                {
                    id.Add(question.QuestionId);
                    id.Distinct().ToList();
                }
                QuestionId.Add(id);
            }
        }
    }
}
