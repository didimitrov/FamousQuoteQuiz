
using System.Collections.Generic;
using FamousQuoteQuiz.Models;

namespace FamousQuoteQuiz.WebClient.ViewModels.HomeController
{
    public class IndexViewModel
    {
        public Question Question { get; set; }

        public ICollection<Author> Authors { get; set; }

    }
}