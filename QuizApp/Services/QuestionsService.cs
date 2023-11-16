using QuizApp.Context;
using QuizApp.Models;

namespace QuizApp.Services
{
    public class QuestionsService : IQuestionsService
    {
        private DBContext _context { get; set; }
        public QuestionsService(DBContext context)
        {
            _context = context;
        }

        public List<Questions> GetAll() => _context.Questions.OrderBy(q => q.QuestionId).ToList();
        public void Add(Questions questions)
        {
            _context.Questions.Add(questions);
            _context.SaveChanges();
        }
        public void Edit(Questions questions) 
        {
            _context.Questions.Update(questions);
            _context.SaveChanges();

        }

       


        public Questions GetById(int id) => _context.Questions.FirstOrDefault(x => x.QuestionId == id);
    }
}
