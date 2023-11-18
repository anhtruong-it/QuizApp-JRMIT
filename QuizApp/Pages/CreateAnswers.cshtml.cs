using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class CreateAnswersModel : PageModel
    {
        [BindProperty]
        public List<Answers> Answers { get; set; }

        [BindProperty]
        public Questions Question { get; set; }
        private IQuestionsService _service { get; set; }
        public CreateAnswersModel(IQuestionsService service)
        {
            _service = service;
        }
        public IActionResult OnGet(int id)
        {
            Question = _service.GetById(id);
            if (Question == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            Question = _service.GetById(id);
            var questionId = $"{Question.QuestionId}";
            foreach (var answer in Answers)

            {
                answer.QuestionId = Question.QuestionId;
                var ans = $"{answer.AnswerId} - {answer.QuestionId} - {answer.Content}";
                _service.AddAnswer(answer);
            }
            //_service.AddAnswer(Answers);
            return Redirect("/Questions");
        }
    }
}
