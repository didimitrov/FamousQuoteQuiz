
using System.Collections.Generic;
using FamousQuoteQuiz.Models;

namespace FamousQuoteQuiz.WebClient.ViewModels.HomeController
{
    public class IndexViewModel
    {
        public QuestionViewModel Question { get; set; }

        public IList<Author> Authors { get; set; }

    }
}