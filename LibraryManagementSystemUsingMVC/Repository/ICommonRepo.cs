using LibraryManagementSystemUsingMVC.Models;
using System.Linq.Expressions;

namespace LibraryManagementSystemUsingMVC.Repository
{
    public interface ICommonRepo<T> where T : class
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Expression<Func<T,bool>> filter);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync( T entity);
      
    }
}
