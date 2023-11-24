using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class TakeQuizModel : PageModel
    {
        public Quizzes Quizzes { get; set; }
        public Questions Question { get; set; }
        public List<Questions> Questions { get; set; }

        public List<List<Answers>> Answer = new List<List<Answers>>();
        public List<Answers> Answers { get; set; }

        public List<int> AnswersId = new List<int>();

        public List<int> CorrectAnswersId = new List<int>();
        
        // Set time is 10 seconds
        public int CountDown { get; set; } = 10;

        private IQuestionsService _service { get; set; }
        public TakeQuizModel(IQuestionsService service)
        {
            _service = service;
        }

        public IActionResult OnGet(int id)
        {
            Quizzes = _service.GetQuizById(id);
            Questions = _service.GetByQuizId(id);

            // Make random order of questions
            Random random = new Random();
            var randomQuestions = Questions.OrderBy(x => random.Next()).ToList();
            Questions = randomQuestions;

            foreach (var questionid in randomQuestions)
            {   
                List<object> ans = new List<object>();
                Answers = _service.GetByQuestionId(questionid.QuestionId);
                Answer.Add(Answers);
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            Questions = _service.GetByQuizId(id);

            // Find correct answers in the database
            foreach (var question in Questions)
            {
                CorrectAnswersId.Add(question.CorrectAnswerId);
            }

            // Get submitted answers from the quiz
            for ( int i = 0; i < Questions.Count; i++)
            {
                var questionName = $"Question{i}AnswersId";
                var selectedAnswerIdsForQuestion = Request.Form[questionName].Select(value => int.Parse(value));
                AnswersId.AddRange(selectedAnswerIdsForQuestion);
            }

            List<object> result = new List<object>();

            // Compare submitted answers and correct answers
            var correctans = CorrectAnswersId.Intersect(AnswersId);

            // Each correct answer is 1 mark, otherwise 0
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
            var resultString = string.Join(", ", result);

            return Redirect($"/Score?result={resultString}");
            
        }

        // Set time for each quiz
        public async Task<IActionResult> OnPostStartCoutdown(int id)
        {
            await Task.Delay(TimeSpan.FromSeconds(CountDown));

            return RedirectToPage("TakeQuiz", new { id });
        }
    }
}
