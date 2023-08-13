using ExamLayer.Data;
using ExamLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExamLayer.Repositories.Service
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BookStoreDbContext _dbContext;

        public GenericRepository(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<T>> GetAllAsync()
        {
            try {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }


        public IQueryable<T> FindAll() => _dbContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _dbContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity)
        {
            _dbContext.Set<T>().AddAsync(entity); 
            _dbContext.SaveChangesAsync(); 
        }

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);
        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);


    }
}
