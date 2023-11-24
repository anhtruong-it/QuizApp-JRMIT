using QuizApp.Models;

namespace QuizApp.Services
{
    public interface IQuestionsService
    {
        List<Questions> GetAll();
        List<Questions> GetByQuizId(int quizId);
        Questions GetById(int id);
        void Add(Questions questions);
        void Edit(Questions questions);

        List<Answers> GetAllAnswers();
        List<Answers> GetByQuestionId(int id);
        Answers GetByCorrectAnswerId(int id);
        void AddAnswer(Answers answers);
        void EditAnswer(Answers answers);

        List<Quizzes> GetAllQuizzes();
        Quizzes GetQuizById(int id);
        void AddQuiz(Quizzes quizzes);
        
        List<Users> GetAllUsers();
        Users GetUserByUserName(string userName);
        void AddUser(Users user);
    }
}
