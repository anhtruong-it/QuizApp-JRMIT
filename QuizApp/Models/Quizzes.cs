using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Quizzes
    {
        [Key]
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public Users Users { get; set; }

        public List<Questions> Questions { get; set; }

    }
}
