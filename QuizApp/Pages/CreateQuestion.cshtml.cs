using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class CreateQuestionModel : PageModel
    {
        [BindProperty]
        public Questions? Question { get; set; }

        [BindProperty]
        public Answers? Answers { get; set; }

        private IQuestionsService _service { get; set; }
        public CreateQuestionModel(IQuestionsService service)
        {
            _service = service;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            //var value = $"{Question?.QuestionId} - {Question?.QuizId} - {Question?.Content} - {Question?.CorrectAnswerId} - {Question?.Answers.ToArray()}";

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            _service.Add(Question);

            foreach (var answer in Question?.Answers.ToArray())
            {
                var anscontent = $"{answer.Content}";
            }
            var value1 = Question?.Answers.ToArray();

            foreach (var answer in Question.Answers.ToArray())
            {
                answer.QuestionId = Question.QuestionId;
                _service.AddAnswer(answer);
            }

            return Redirect("/Questions");
        }
    }
}
