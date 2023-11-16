using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Questions
    {
        [Key]
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public string Content { get; set; }
        public int CorrectAnswerId { get; set; }
        public List<Answers> Answers { get; set; }



    }
}
