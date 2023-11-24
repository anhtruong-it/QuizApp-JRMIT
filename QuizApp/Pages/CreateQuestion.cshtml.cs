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
            // Create new question without answers
            Question.QuizId = new List<int>();
            _service.Add(Question);
            
            // Assign question ID to answers coressponding
            for (int i = 0; i < Answers.Count; i++)
            {
                var answer = Answers[i];
                answer.QuestionId = Question.QuestionId;
                _service.AddAnswer(answer);
                if (CorrectAnswer == i)
                {
                    Question.CorrectAnswerId = answer.AnswerId;
                }
            }

            // Retrieve question and store with answers
            var existingQuestion = _service.GetById(Question.QuestionId);
            existingQuestion.QuizId = Question.QuizId;
            existingQuestion.Content = Question.Content;
            existingQuestion.CorrectAnswerId = Question.CorrectAnswerId;
            _service.Edit(existingQuestion);
        
            return Redirect("/Questions");
        }
    }
}
