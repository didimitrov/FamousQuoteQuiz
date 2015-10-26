using System.Data.Entity;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using FamousQuoteQuiz.Data.Migrations;
using FamousQuoteQuiz.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FamousQuoteQuiz.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
        public virtual IDbSet<Question> Questions { get; set; }
 
    }
}
