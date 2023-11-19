using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class QuestionsModel : PageModel
    {
        public List<Questions> Questions { get; set; }
        public List<Answers> Answers { get; set; }
        private IQuestionsService _service { get; set; }
        public QuestionsModel(IQuestionsService service)
        { 
            _service = service;
        }
        public void OnGet()
        {
            Questions = _service.GetAll();
            Answers = _service.GetAllAnswers();
        }
    }
}
