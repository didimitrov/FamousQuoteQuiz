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

        public HomeController(IRepository<Question> questionRepository, IRepository<Question> authorRepository)
        {
            this._questionRepository = questionRepository;
            _authorRepository = authorRepository;
        }

        public ActionResult Index(int? currentQuestionId)
        {
            var questionAndAuthors = this._questionRepository.All().Include("Author").FirstOrDefault();


            if (currentQuestionId != null)
            {
                var exclude = new HashSet<int>() { };
                exclude.Add((int)currentQuestionId);
                var range = Enumerable.Range(1, this._questionRepository.All().Count()).Where(i => !exclude.Contains(i));

                var rand = new System.Random();
                int index = rand.Next(1, this._questionRepository.All().Count() - exclude.Count);
                var newQuestionId = range.ElementAt(index);

                var newQuestion = _questionRepository.All().FirstOrDefault(x => x.Id == newQuestionId);


                return PartialView("_NextQuestionPartial",newQuestion);


            }
            return View(questionAndAuthors);

        }

        [HttpPost]
        public ActionResult IsAnswerCorrect(string btnValue, int questionId, int authorId)
        {
            var question = _questionRepository.All().FirstOrDefault(x => x.Id == questionId);

            if (btnValue == "answer-true" )
            {
                if (question != null) return Content("Correct! The answer is " + question.Author.AuthorName);
            }
            return Content("Incorrect! " + question.Author.AuthorName);
        }

        //public ActionResult GetNext(int currentQuestionId)
        //{

        //    var exclude = new HashSet<int>() {  };
        //    exclude.Add(currentQuestionId);
        //    var range = Enumerable.Range(1, this._questionRepository.All().Count()).Where(i => !exclude.Contains(i));

        //    var rand = new System.Random();
        //    int index = rand.Next(1, this._questionRepository.All().Count() - exclude.Count);
        //    var newQuestionId= range.ElementAt(index);

        //    var newQuestion = _questionRepository.All().FirstOrDefault(x => x.Id == newQuestionId);

        //    return PartialView("_NextQuestion", newQuestion);
        //}


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
