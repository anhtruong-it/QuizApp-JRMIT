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
        void Edit(Questions questions);

    }
}
