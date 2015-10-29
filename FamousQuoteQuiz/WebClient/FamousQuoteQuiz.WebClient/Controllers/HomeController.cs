using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using FamousQuoteQuiz.Common;
using FamousQuoteQuiz.Data.Repositories;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.WebClient.ViewModels.HomeController;

namespace FamousQuoteQuiz.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly RandomShuffle _shuffle;

        public HomeController(IRepository<Question> questionRepository, IRepository<Author> authorRepository, RandomShuffle shuffle)
        {
            this._questionRepository = questionRepository;
            _authorRepository = authorRepository;
            this._shuffle = shuffle;
        }

        public ActionResult Index()  
        {
            var model = _questionRepository.All().OrderBy(r => Guid.NewGuid()).Select(x => new QuestionViewModel()
            {
                Author = x.Author,
                AuthorId = x.Id,
                Content = x.Content,
                Id = x.Id
            }).Take(1).FirstOrDefault();

            return View( model);
        }

        [HttpPost]
        public ActionResult CheckBinaryAnswer(string btnValue)
        {
            if (btnValue == "true")
            {
                ViewData["true"] = "Correct!";
                // if (question != null) return Content("Correct! The answer is " + author.AuthorName);
            }
            if (btnValue=="false")
            {
                ViewData["false"] = "Wrong!";
                // return Content("Wrong! Answer is: " + question.Author.AuthorName);
            }

            return PartialView("_IsCorrectMessage");
        }

        public ActionResult MultipleChoice(int currentAuthorId)
        {
            var rightAuthor = this._authorRepository.All().SingleOrDefault(x => x.Id == currentAuthorId);
            var randomAuthors = _authorRepository
                .All().Where(x=>x.Id!=currentAuthorId)
                .GroupBy(a => a.AuthorName)
                .Select(g => g.FirstOrDefault())
                .OrderBy(r => Guid.NewGuid())
                .Take(2)
                .ToList();

            randomAuthors.Add(rightAuthor);
            _shuffle.Shuffle(randomAuthors);

            return PartialView("_MultipleChoiceAuthors", randomAuthors);
        }

        public ActionResult GetNext(int currentQuestionId)
        {
            var exclude = new HashSet<int>() { };
            exclude.Add(currentQuestionId);
            var range = Enumerable.Range(1, this._questionRepository.All().Count()).Where(i => !exclude.Contains(i));

            var rand = new System.Random();
            int index = rand.Next(1, this._questionRepository.All().Count() - exclude.Count);
            var newQuestionId = range.ElementAt(index);

            var newQuestion =
                _questionRepository.All().Where(x => x.Id == newQuestionId).Select(x => new QuestionViewModel()
                {
                    Author = x.Author,
                    AuthorId = x.Id,
                    Content = x.Content,
                    Id = x.Id
                }).FirstOrDefault();

            return PartialView("_NextQuestionPartial", newQuestion);
        }

        //public ActionResult CheckMultipleAnswer(int authorId, int questionId)
        //{
        //    var question = this._questionRepository.All().Where(x => x.Id == questionId).FirstOrDefault();
        //    if (question.AuthorId==authorId)
        //    {
        //        return Content("Correct");
        //    }
        //    return Content("Wrong");
        //}
    }
}
