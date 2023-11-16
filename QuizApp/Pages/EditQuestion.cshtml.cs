using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class EditQuestionModel : PageModel
    {
        [BindProperty]
        public Questions Question { get; set; }
        private IQuestionsService _service { get; set; }
        public EditQuestionModel(IQuestionsService service)
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

        public IActionResult OnPost()
        {
            var value = $"{Question?.QuestionId} - {Question?.QuizId} - {Question?.Content} - {Question?.CorrectAnswerId}";
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingQuestion = _service.GetById(Question.QuestionId);

            if (existingQuestion == null)
            {
                return NotFound();
            }

            existingQuestion.QuizId = Question.QuizId;
            existingQuestion.Content = Question.Content;
            existingQuestion.CorrectAnswerId = Question.CorrectAnswerId;

            _service.Edit(existingQuestion);

            return Redirect("/Questions");
        }
    }
}
