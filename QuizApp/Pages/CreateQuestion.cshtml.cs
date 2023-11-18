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
        public List<Answers> Answers { get; set; }

        [BindProperty]
        public int CorrectAnswer { get; set; }

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
            var value = $"{CorrectAnswer}";

            for (int i = 0; i < Answers.Count; i++)

            {
                var answer = Answers[i];
                answer.QuestionId = Question.QuestionId;
                var ans = $"{i} - {answer.AnswerId} - {answer.QuestionId} - {answer.Content}";
                _service.AddAnswer(answer);
                if (CorrectAnswer == i)
                {
                    Question.CorrectAnswerId = answer.AnswerId;
                }

            }
            var existingQuestion = _service.GetById(Question.QuestionId);
            existingQuestion.QuizId = Question.QuizId;
            existingQuestion.Content = Question.Content;
            existingQuestion.CorrectAnswerId = Question.CorrectAnswerId;
            _service.Edit(existingQuestion);
        


            //return Redirect($"/CreateAnswers/{Question.QuestionId}");
            return Redirect("/Questions");
        }
    }
}
