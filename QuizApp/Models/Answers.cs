using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Answers
    { 

        [Key]
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public Questions Question { get; set; }


    }
}
