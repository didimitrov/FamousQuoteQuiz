using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamousQuoteQuiz.Models
{
    public class Question
    {
        //private ICollection<Answer> _answers; 
        //public Question()
        //{
        //    this._answers = new HashSet<Answer>();
        //}

        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int  AuthorId { get; set; }

        public virtual Author Author { get; set; }
       
    }

    //public enum QuestionType
    //{
    //    MultipleChoice,
    //    Binary
    //}
}
