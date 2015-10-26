namespace FamousQuoteQuiz.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
       
    }
}
