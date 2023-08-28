using System.Linq.Expressions;

namespace ExamLayer.Repositories.Interface
{
    public interface IGenericRepository<T> where T : class
    {

        #region 查詢
        T Get(Expression<Func<T, bool>> expression, string Include1 = "", string Include2 = "", string Include3 = "", string Include4 = "", string Include5 = "");
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string Include1 = "", string Include2 = "", string Include3 = "", string Include4 = "", string Include5 = "");
        ////IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        Task<List<T>> GetAllAsync();
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression);

        #endregion
        //TEntity Get(Expression<Func<TEntity, bool>> predicate);

        #region 新增
        Task<int> CreateAsync(T entity);
        Task<int> BulkCreateAsync(List<T> entity);
        Task<int> SaveChangesAsync();
        #endregion

        #region 修改
        Task<int> UpdateAsync(T entity);
        #endregion

        #region 刪除
        Task<int> DeleteAsync(Expression<Func<T, bool>> expression);
        #endregion

    }
}
