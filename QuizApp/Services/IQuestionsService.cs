﻿using QuizApp.Models;

namespace QuizApp.Services
{
    public interface IQuestionsService
    {
        Questions GetById(int id);
        void Add(Questions questions);
        List<Questions> GetByQuizId(int quizId);
        void AddAnswer(Answers answers);
        List<Answers> GetByQuestionId(int id);
        Answers GetByCorrectAnswerId(int id);
        List<Questions> GetAll();
        List<Answers> GetAllAnswers();
        void Edit(Questions questions);
        void EditAnswer(Answers answers);
        void AddQuiz(Quizzes quizzes);
        List<Quizzes> GetAllQuizzes();
        Quizzes GetQuizById(int id);
        void AddUser(Users user);
        List<Users> GetAllUsers();
        Users GetUserByUserName(string userName);

    }
}
