using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models;
using QuizApp.Services;
using System.Threading.Tasks;

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
        public int CountDown { get; set; } = 5;
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
            List<object> result = new List<object>();
            var correctans = CorrectAnswersId.Intersect(AnswersId);
            if (correctans.Any())
            {
                int count = 0;
                foreach (var answer in correctans)
                {
                    count++;
                }
                result.Add(count);
            }
            else
            {
                result.Add(0);
            }

            result.Add(Questions.Count);
            var a = $"{result}";
            var resultString = string.Join(", ", result);
            return Redirect($"/Score?result={resultString}");
            
        }

        public async Task<IActionResult> OnPostStartCoutdown(int id)
        {
            await Task.Delay(TimeSpan.FromSeconds(CountDown));
            return RedirectToPage("TakeQuiz", new { id });
        }
    }
}
