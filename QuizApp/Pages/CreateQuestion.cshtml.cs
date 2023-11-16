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
            var value = $"{Question?.QuestionId} - {Question?.QuizId} - {Question?.Content} - {Question?.CorrectAnswerId}";
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.Add(Question);

            return Redirect("/Questions");
        }
    }
}
