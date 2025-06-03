using LibraryManagementSystemUsingMVC.Data;
using LibraryManagementSystemUsingMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagementSystemUsingMVC.Repository
{
    public class CommonRepo<T> : ICommonRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> DbSet;
        public CommonRepo(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
           await  DbSet.AddAsync(entity);
           await   _context.SaveChangesAsync();
           
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }
            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
          
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           var result =await DbSet.ToListAsync();
            return result;
        }

        public Task<T> GetByIdAsync(Expression<Func<T,bool>> filter)
        {
            var result = DbSet.Where(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task UpdateAsync(T entity)
        {
           if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }
            DbSet.Update(entity);
           await _context.SaveChangesAsync();
          
        }
       
    }
}
