using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FamousQuoteQuiz.Models;

namespace FamousQuoteQuiz.WebClient.ViewModels.HomeController
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}