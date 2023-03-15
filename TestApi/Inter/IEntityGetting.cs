using System.Linq.Expressions;
using TestApi.Models;

namespace TestApi.Inter;

public interface IEntityGetting<T> where T : class
{
    IQueryable<T> Get(MyContext context, Expression<Func<T, bool>> selector);
}