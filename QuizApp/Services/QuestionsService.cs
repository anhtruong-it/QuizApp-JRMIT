using Microsoft.EntityFrameworkCore;
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
        
        
        public List<Quizzes> GetAllQuizzes() => _context.Quizzes.OrderBy(q => q.QuizId).ToList();
        public List<Answers> GetAllAnswers() => _context.Answers.OrderBy(a => a.AnswerId).ToList();
        public List<Users> GetAllUsers() => _context.Users.ToList();
        public void Add(Questions questions)
        {
            _context.Questions.Add(questions);
            _context.SaveChanges();
        }
        public void AddUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void AddQuiz(Quizzes quizzes)
        {
            _context.Quizzes.Add(quizzes);
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

        public Users GetUserByUserName(string username) => _context.Users.FirstOrDefault(u => u.Username == username);
       
        public List<Answers> GetByQuestionId(int id) => _context.Answers.Where(x => x.QuestionId == id).ToList();
        public Answers GetByCorrectAnswerId(int id) =>  _context.Answers.FirstOrDefault(x => x.AnswerId == id);

        public List<Questions> GetByQuizId(int id) => _context.Questions.Where(q => q.QuizId.Contains(id)).ToList();

        public Quizzes GetQuizById(int id) => _context.Quizzes.FirstOrDefault(q => q.QuizId == id);
    }
    
}
