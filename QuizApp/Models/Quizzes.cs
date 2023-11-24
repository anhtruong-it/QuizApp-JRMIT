using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Quizzes
    {
        [Key]
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
    }
}
