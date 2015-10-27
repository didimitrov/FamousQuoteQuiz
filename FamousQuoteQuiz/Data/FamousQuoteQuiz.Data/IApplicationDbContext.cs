using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FamousQuoteQuiz.Models;

namespace FamousQuoteQuiz.Data
{
    interface IApplicationDbContext
    {
        IDbSet<Author> Authors { get; set; }

        IDbSet<Question> Questions { get; set; }

        IDbSet<ApplicationUser> Users { get; set; }  

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
