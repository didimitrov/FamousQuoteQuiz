
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamousQuoteQuiz.Models
{
    public class Author
    {
        private ICollection<Question> _questions; 
        public Author ()
        {
            this._questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string AuthorName { get; set; }
        
        public int QuestionId { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

       // public bool IsCorrect { get; set; }
    }
}
