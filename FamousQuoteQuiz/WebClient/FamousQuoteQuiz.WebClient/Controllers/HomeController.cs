using System.Web.Mvc;
using System.Web.UI;
using FamousQuoteQuiz.Data.Repositories;
using FamousQuoteQuiz.Models;

namespace FamousQuoteQuiz.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Question> _questionRepository;
      

        public HomeController( IRepository<Question> questionRepository)
        {
            this._questionRepository = questionRepository;            
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}