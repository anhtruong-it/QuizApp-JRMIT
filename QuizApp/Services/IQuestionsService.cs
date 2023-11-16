using QuizApp.Models;

namespace QuizApp.Services
{
    public interface IQuestionsService
    {
        Questions GetById(int id);
        void Add(Questions questions);
        void AddAnswer(Answers answers);
        Answers GetByQuestionId(int id);
        List<Questions> GetAll();
        void Edit(Questions questions);

    }
}
