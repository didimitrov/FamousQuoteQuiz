using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamousQuoteQuiz.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }

    //public enum QuestionType
    //{
    //    Binary,
    //    MultipleChoice
    //}
}
