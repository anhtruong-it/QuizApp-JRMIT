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

        [BindProperty]
        public int CorrectAnswerId { get; set; }
        public Answers CorrectAnswer { get; set; }

        private IQuestionsService _service { get; set; }
        public EditQuestionModel(IQuestionsService service)
        {
            _service = service;
        }

        public IActionResult OnGet(int id)
        {
            // Retrieve a question and its answers from database
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
            //Check valid form
            bool ValidAnswers = Answers.All(answer => !string.IsNullOrEmpty(answer.Content));
            if (!ValidAnswers)
            {
                return Page();
            }

            // Update the question and answers
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

                existingAnswer[i].QuestionId = answer.QuestionId;
                existingAnswer[i].Content = answer.Content;
                _service.EditAnswer(existingAnswer[i]);

                if (Question.CorrectAnswerId == i)
                {
                    Question.CorrectAnswerId = existingAnswer[i].AnswerId;
                }
            }

            existingQuestion.Content = Question.Content;
            existingQuestion.CorrectAnswerId = Question.CorrectAnswerId;

            _service.Edit(existingQuestion);

            return Redirect("/Questions");
        }
    }
}
