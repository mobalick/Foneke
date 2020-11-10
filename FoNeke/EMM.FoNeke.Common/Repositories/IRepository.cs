using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EMM.FoNeke.Common.Repositories
{
    public interface IRepository<T>
    {
        T GetById(string id);
        T GetById(string id, string[] fieldsToExclude);

        List<T> GetByIds(List<string> ids);
        List<T> GetByIds(List<string> ids, string[] fieldsToExclude);


        T GetSingle(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, string[] fieldsToExclude);

        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate, string[] fieldsToExclude);

        IQueryable<T> GetAll();
        IQueryable<T> GetAll(string[] fieldsToExclude);

        Task<T> SaveOrUpdate(T entity);
        void SaveOrUpdate(IEnumerable<T> entities);

        void Delete(string id);
        void Delete(T entity);
        void DeleteAll();
    }
}