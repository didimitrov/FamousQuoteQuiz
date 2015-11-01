using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using FamousQuoteQuiz.Common;
using FamousQuoteQuiz.Data;
using FamousQuoteQuiz.Data.Repositories;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.WebClient.ViewModels.HomeController;

namespace FamousQuoteQuiz.WebClient.Controllers
{
    public class HomeController : BaseController
    {
        //private readonly IRepository<Question> _questionRepository;
        //private readonly IRepository<Author> _authorRepository;
        private readonly RandomGenerator _generator;

        //public HomeController(IRepository<Question> questionRepository, IRepository<Author> authorRepository, RandomGenerator generator)
        //{
        //    this._questionRepository = questionRepository;
        //    _authorRepository = authorRepository;
        //    this._generator = generator;
        //}

        public HomeController(ApplicationDbContext context, RandomGenerator generator) : base(context)
        {
            _generator = generator;
        }

        public ActionResult Index()  
        {
            //var rndQuestion = _questionRepository.All().OrderBy(r => Guid.NewGuid()).Select(x => new QuestionViewModel()
            //{
            //    AuthorName = x.Author.AuthorName,
            //    AuthorId = x.Id,
            //    Content = x.Content,
            //    Id = x.Id
            //}).Take(1).FirstOrDefault();

            //var authors = Queryable.Select(_authorRepository.All().OrderBy(x => x.AuthorName), x => new AuthorViewModel()
            //{
            //    AuthorName = x.AuthorName,
            //    Id = x.Id
            //}).ToList();

            //var model = new IndexViewModel();
            //model.Question = rndQuestion;
            //model.Authors = authors;

            return View(); //model
        }
        
        [HttpPost]
        public ActionResult CheckBinaryAnswer(string btnValue, int questionId, int? authorId)
        {
            var question = this.Context.Questions.FirstOrDefault(x => x.Id == questionId);
         //   var author = this.Context.Authors.FirstOrDefault(x => x.Id == authorId);

            if (btnValue == "true" || question.AuthorId == authorId)
            {
                ViewData["true"] = "Correct! The answer is " + question.Author.AuthorName;
            }
            else if (btnValue == "false" || question.AuthorId != authorId)
            {
                ViewData["false"] = "Wrong! Answer is: " + question.Author.AuthorName;
            }

            return PartialView("_IsCorrectMessage");
        }

        public ActionResult MultipleChoice(int currQuestionId)
        {
            var question = Context.Questions.SingleOrDefault(x => x.Id == currQuestionId);
            var rightAuthor = Context.Authors.SingleOrDefault(x => x.Id == question.AuthorId);
            var currentAuthorId = question.AuthorId;

          //  var rightAuthor = this.Context.Authors.SingleOrDefault(x => x.Id == currentAuthorId);

            var randomAuthors = Context.Authors
                .Where(x=>x.Id!=currentAuthorId)
                .GroupBy(a => a.AuthorName)
                .Select(g => g.FirstOrDefault())
                .OrderBy(r => Guid.NewGuid())
                .Take(2)
                .ToList();

            randomAuthors.Add(rightAuthor);
            _generator.Shuffle(randomAuthors);

            return PartialView("_MultipleChoiceAuthors", randomAuthors);
        }

        public ActionResult GetNext(int? currentQuestionId)
        {
            QuestionViewModel model;

            if (currentQuestionId == null)
            {
                model
                    = Context.Questions.OrderBy(r => Guid.NewGuid()).Select(x => new QuestionViewModel()
                    {
                        AuthorName = x.Author.AuthorName,
                        AuthorId = x.Id,
                        Content = x.Content,
                        Id = x.Id
                    }).Take(1).FirstOrDefault();

            }
            else
            {
                var newQuestionId = this._generator.GetRandomIndex(this.Context.Questions.ToList(),(int)currentQuestionId);

                model =
                    Context.Questions.Where(x => x.Id == newQuestionId).Select(x => new QuestionViewModel()
                    {
                        AuthorName = x.Author.AuthorName,
                        AuthorId = x.Id,
                        Content = x.Content,
                        Id = x.Id
                    }).FirstOrDefault();
            }

            return PartialView("_NextQuestionPartial", model);
        }

        //public ActionResult GetNext(int currentQuestionId)
        //{
        //    var exclude = new HashSet<int>() { };
        //    exclude.Add(currentQuestionId);
        //    var range = Enumerable.Range(1, this._questionRepository.All().Count()).Where(i => !exclude.Contains(i));

        //    var rand = new System.Random();
        //    int index = rand.Next(1, this._questionRepository.All().Count() - exclude.Count);
        //    var newQuestionId = range.ElementAt(index);

        //    var newQuestion =
        //        _questionRepository.All().Where(x => x.Id == newQuestionId).Select(x => new QuestionViewModel()
        //        {
        //            AuthorName = x.Author.AuthorName,
        //            AuthorId = x.Id,
        //            Content = x.Content,
        //            Id = x.Id
        //        }).FirstOrDefault();

        //    return PartialView("_NextQuestionPartial", newQuestion);
        //}

        //public ActionResult CheckMultipleAnswer(int authorId, int questionId)
        //{
        //    var question = this.Context.Questions.FirstOrDefault(x => x.Id == questionId);
        //    if (question.AuthorId == authorId)
        //    {
        //        return Content("Correct");
        //    }
        //    return Content("Wrong");
        //}
    }
}
