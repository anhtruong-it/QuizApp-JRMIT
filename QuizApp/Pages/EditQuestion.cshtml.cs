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
        [BindProperty]
        public List<Answers> Answers { get; set; }
        public Answers CorrectAnswer { get; set; }

        [BindProperty]
        public int CorrectAnswerId { get; set; }
        private IQuestionsService _service { get; set; }
        public EditQuestionModel(IQuestionsService service)
        {
            _service = service;
        }
        public IActionResult OnGet(int id)
        {
            Question = _service.GetById(id);
            Answers = _service.GetByQuestionId(Question.QuestionId);
            
            if (Question == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var value = $"{Question?.QuestionId} - {Question?.QuizId} - {Question?.Content} - {Question?.CorrectAnswerId}";
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}


            var existingQuestion = _service.GetById(Question.QuestionId);
            var existingAnswer = _service.GetByQuestionId(Question.QuestionId);

            if (existingQuestion == null)
            {
                return NotFound();
            }

            for (int i = 0; i < Answers.Count; i++)

            {
                var answer = Answers[i];
                answer.QuestionId = Question.QuestionId;
                var ans = $"{i} - {answer.AnswerId} - {answer.QuestionId} - {answer.Content}";
                //_service.AddAnswer(answer);
                if (Question.CorrectAnswerId == i)
                {
                    Question.CorrectAnswerId = answer.AnswerId;
                }

            }

            existingQuestion.QuizId = Question.QuizId;
            existingQuestion.Content = Question.Content;
            existingQuestion.CorrectAnswerId = Question.CorrectAnswerId;

            //_service.Edit(existingQuestion);

            return Redirect("/Questions");
        }
    }
}
