using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class TakeQuizModel : PageModel
    {
        public Quizzes Quizzes { get; set; }
        public List<Questions> Questions { get; set; }
        public Questions Question { get; set; }

        public List<List<Answers>> Answer = new List<List<Answers>>();
        public List<Answers> Answers { get; set; }

        public List<int> AnswersId = new List<int>();
        public List<int> CorrectAnswersId = new List<int>();
        private IQuestionsService _service { get; set; }
        public TakeQuizModel(IQuestionsService service)
        {
            _service = service;
        }
        public IActionResult OnGet(int id)
        {
            Quizzes = _service.GetQuizById(id);
            var quiz = $"{Quizzes.QuizId} - {Quizzes.Title} - {Quizzes.Description}";
            Questions = _service.GetByQuizId(id);
            var question = $"{Questions.Count}";
            foreach (var questionid in Questions)
            {   List<object> ans = new List<object>();
                Answers = _service.GetByQuestionId(questionid.QuestionId);
                var b = $"{Answers[0].AnswerId}";
                Answer.Add(Answers);
            }
            var a = $"{Answer}";
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            Questions = _service.GetByQuizId(id);
            foreach (var question in Questions)
            {
                CorrectAnswersId.Add(question.CorrectAnswerId);
            }
            for ( int i = 0; i < Questions.Count; i++)
            {
                var questionName = $"Question{i}AnswersId";
                var selectedAnswerIdsForQuestion = Request.Form[questionName].Select(value => int.Parse(value));
                AnswersId.AddRange(selectedAnswerIdsForQuestion);
            }
            var correctans = CorrectAnswersId.Intersect(AnswersId);
            List<object> result = new List<object>();
            result.Add(correctans);
            result.Add(Questions.Count);
            return Redirect($"/Score/{result}");
        }
    }
}
