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
        
        public List<Answers> GetAllAnswers() => _context.Answers.OrderBy(a => a.AnswerId).ToList();
        public void Add(Questions questions)
        {
            _context.Questions.Add(questions);
            _context.SaveChanges();
        }
        public void AddAnswer(Answers answers)
        {
            _context.Answers.Add(answers);
            _context.SaveChanges();
        }
        public void Edit(Questions questions) 
        {
            _context.Questions.Update(questions);
            _context.SaveChanges();

        }
        public void EditAnswer(Answers answers)
        {
            _context.Answers.Update(answers);
            _context.SaveChanges();
        }

        public Questions GetById(int id) => _context.Questions.FirstOrDefault(x => x.QuestionId == id);
        public List<Answers> GetByQuestionId(int id) => _context.Answers.Where(x => x.QuestionId == id).ToList();
        public Answers GetByCorrectAnswerId(int id) =>  _context.Answers.FirstOrDefault(x => x.AnswerId == id);
    }
}
