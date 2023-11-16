using QuizApp.Models;

namespace QuizApp.Services
{
    public interface IQuestionsService
    {
        Questions GetById(int id);
        void Add(Questions questions);
        List<Questions> GetAll();
        void Edit(Questions questions);

    }
}
