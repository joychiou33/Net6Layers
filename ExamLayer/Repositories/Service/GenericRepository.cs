using ExamLayer.Data;
using ExamLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Transactions;

namespace ExamLayer.Repositories.Service
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BookStoreDbContext _dbContext;

        public GenericRepository(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region 查詢

        public T Get(Expression<Func<T, bool>> expression, string Include1 = "", string Include2 = "", string Include3 = "", string Include4 = "", string Include5 = "")
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                if (!string.IsNullOrWhiteSpace(Include5))
                {
                    return this._dbContext.Set<T>().Include(Include1).Include(Include2).Include(Include3).Include(Include4).Include(Include5).FirstOrDefault(expression);
                }
                else if (!string.IsNullOrWhiteSpace(Include4))
                {
                    return this._dbContext.Set<T>().Include(Include1).Include(Include2).Include(Include3).Include(Include4).FirstOrDefault(expression);
                }
                else if (!string.IsNullOrWhiteSpace(Include3))
                {
                    return this._dbContext.Set<T>().Include(Include1).Include(Include2).Include(Include3).FirstOrDefault(expression);
                }
                else if (!string.IsNullOrWhiteSpace(Include2))
                {
                    return this._dbContext.Set<T>().Include(Include1).Include(Include2).FirstOrDefault(expression);
                }
                else if (!string.IsNullOrWhiteSpace(Include1))
                {
                    return this._dbContext.Set<T>().Include(Include1).FirstOrDefault(expression);
                }
                else
                {
                    return this._dbContext.Set<T>().FirstOrDefault(expression);
                }
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(expression);
        }
        

        /// <summary>
        /// GetAll() TransactionScope
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                return this._dbContext.Set<T>().AsQueryable();
            }
        }
       
        /// <summary>
        /// GetAll(Expression) TransactionScope 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="Include1"></param>
        /// <param name="Include2"></param>
        /// <param name="Include3"></param>
        /// <param name="Include4"></param>
        /// <param name="Include5"></param>
        /// <returns></returns>
        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string Include1 = "", string Include2 = "", string Include3 = "", string Include4 = "", string Include5 = "")
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                if (!string.IsNullOrWhiteSpace(Include5))
                {
                    return this._dbContext.Set<T>().Include(Include1).Include(Include2).Include(Include3).Include(Include4).Include(Include5).Where(expression);
                }
                else if (!string.IsNullOrWhiteSpace(Include4))
                {
                    return this._dbContext.Set<T>().Include(Include1).Include(Include2).Include(Include3).Include(Include4).Where(expression);
                }
                else if (!string.IsNullOrWhiteSpace(Include3))
                {
                    return this._dbContext.Set<T>().Include(Include1).Include(Include2).Include(Include3).Where(expression);
                }
                else if (!string.IsNullOrWhiteSpace(Include2))
                {
                    return this._dbContext.Set<T>().Include(Include1).Include(Include2).Where(expression);
                }
                else if (!string.IsNullOrWhiteSpace(Include1))
                {
                    return this._dbContext.Set<T>().Include(Include1).Where(expression);
                }
                else
                {
                    return this._dbContext.Set<T>().Where(expression);
                }
            }
        }
        //public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
        //    _dbContext.Set<T>().Where(expression).AsNoTracking();

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() => this._dbContext.Set<T>().Where(expression));
        }
        #endregion

        #region 新增
        public async Task<int> CreateAsync(T entity)
        {
            int rtnNumber = 0;
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                this._dbContext.Set<T>().Add(entity);

                bool bFlag = true;
                string sErrorMsg = string.Empty;

                try
                {
                    rtnNumber = await this.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    bFlag = false;
                    sErrorMsg = ex.Message;
                }

                if (!bFlag)
                {
                    throw new Exception(sErrorMsg);
                }
            }

            return rtnNumber;
        }
        public async Task<int> BulkCreateAsync(List<T> entity)
        {
            int rtnNumber = 0;

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                bool bFlag = true;
                string sErrorMsg = string.Empty;

                try
                {
                    this._dbContext.Set<T>().AddRange(entity);
                    rtnNumber = await this.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    bFlag = false;
                    sErrorMsg = ex.Message;
                }

                if (!bFlag)
                {
                    throw new Exception(sErrorMsg);
                }
            }

            return rtnNumber;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region 修改
        public async Task<int> UpdateAsync(T entity)
        {
            int rtnNumber = 0;

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                bool bFlag = true;
                string sErrorMsg = string.Empty;

                _dbContext.Entry(entity).State = EntityState.Modified;

                try
                {
                    rtnNumber = await this.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    bFlag = false;
                    sErrorMsg = ex.Message;
                }

                if (!bFlag)
                {
                    throw new Exception(sErrorMsg);
                }
            }

            return rtnNumber;
        }

        #endregion

        #region 刪除
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            string sErrorMsg = string.Empty;
            int rtnNumber = 0;

            try
            {
                IEnumerable<T> entities = this._dbContext.Set<T>().Where(expression).AsEnumerable();
                this._dbContext.Set<T>().RemoveRange(entities);
                rtnNumber = await this.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                sErrorMsg = ex.Message;
            }

            return rtnNumber;

        }

        #endregion

    }
}
