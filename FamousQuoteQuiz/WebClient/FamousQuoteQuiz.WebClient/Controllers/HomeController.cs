using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using FamousQuoteQuiz.Data.Repositories;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.WebClient.ViewModels.HomeController;

namespace FamousQuoteQuiz.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Question> _authorRepository;
        
        public HomeController( IRepository<Question> questionRepository, IRepository<Question> authorRepository)
        {
            this._questionRepository = questionRepository;
            _authorRepository = authorRepository;
        }

        public ActionResult Index()
        {
            var questionAndAuthors = this._questionRepository.All().Include("Author").FirstOrDefault();

            return View(questionAndAuthors);
            //var rnd = new Random();
            //int number = rnd.Next(minNumber, maxNumberi + 1);
        }

        [HttpPost]
        public ActionResult IsAnswerCorrect(string btnValue, int questionId, int authorId)
        {
            var question = _questionRepository.All().FirstOrDefault(x => x.Id == questionId);
            
            if (btnValue == "answer-true")
            {
                if (question != null) return Content("Correct! The answer is " + question.Author.AuthorName);
            }
            return Content("Incorrect! " + question.Author.AuthorName);
        }


        //[HttpPost]
        //public ActionResult IsUserChoiseCorrect(int questionId, int authorId, bool isAnswerCorrect = true)
        //{
        //    var question = this._questionRepository.All()
                
        //        .FirstOrDefault(q => q.Id == questionId);

        //    isAnswerCorrect = !((question.AuthorId == authorId) ^ isAnswerCorrect);

        //    var model = new Tuple<string, bool>(question.Author.AuthorName, isAnswerCorrect);

        //    return PartialView("_ResultView", model);
        //}
    }
}