using QuizApp.Models;

namespace QuizApp.Services
{
    public interface IQuestionsService
    {
        Questions GetById(int id);
        void Add(Questions questions);
        void AddAnswer(Answers answers);
        List<Answers> GetByQuestionId(int id);
        Answers GetByCorrectAnswerId(int id);
        List<Questions> GetAll();
        List<Answers> GetAllAnswers();
        void Edit(Questions questions);
        void EditAnswer(Answers answers);
        void AddQuiz(Quizzes quizzes);
        List<Quizzes> GetAllQuizzes();

    }
}
