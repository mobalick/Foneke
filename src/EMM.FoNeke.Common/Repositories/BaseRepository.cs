using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoRepository;
using EMM.FoNeke.Common.Entities;

namespace EMM.FoNeke.Common.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly MongoRepository<T> _repo;

        public BaseRepository(MongoRepository<T> repo)
        {
            _repo = repo;
        }

        public BaseRepository()
        {
            _repo = new MongoRepository<T>();
        }


        



        public T GetById(string id)
        {
            return string.IsNullOrWhiteSpace(id)?new T() : _repo.GetById(id);
        }

        public T GetById(string id, string[] fieldsToExclude)
        {
            //return typeof (T).IsSubclassOf(typeof (Entity))
            //    ? _repo.Collection.FindOneByIdAs<T>(new ObjectId(id))
            //    : _repo.Collection.FindOneByIdAs<T>(id);

            return GetAll(fieldsToExclude).FirstOrDefault(e=>e.Id==id);
        }

        public List<T> GetByIds(List<string> ids)
        {
            return GetAll().Where(e => ids.Contains(e.Id)).ToList();
        }

        public List<T> GetByIds(List<string> ids, string[] fieldsToExclude)
        {
            return GetAll(fieldsToExclude).Where(e => ids.Contains(e.Id)).ToList();
        }

        public IQueryable<T> GetAll()
        {
            return _repo.Collection.AsQueryable();
        }

        public IQueryable<T> GetAll(string[] fieldsToExclude)
        {
            return _repo.Collection.FindAll().SetFields(Fields.Exclude(fieldsToExclude)).AsQueryable();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _repo.SingleOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, string[] fieldsToExclude)
        {
            return
                _repo.Collection.FindAll()
                    .SetFields(Fields.Exclude(fieldsToExclude))
                    .AsQueryable()
                    .Where(predicate)
                    .FirstOrDefault();
        }

        public async Task<T> SaveOrUpdate(T entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                entity.CreationDate = DateTime.Now;
            }
            else
            {
                entity.ModificationDate = DateTime.Now;
            }
             
             return await Task.FromResult(_repo.Update(entity));
        }

        public  void SaveOrUpdate(IEnumerable<T> entities)
        {
            IList<T> entitiesLst = entities as IList<T> ?? entities.ToList();
            foreach (T entity in entitiesLst)
            {
                if (string.IsNullOrWhiteSpace(entity.Id))
                {
                    entity.CreationDate = DateTime.Now;
                }
                else
                {
                    entity.ModificationDate = DateTime.Now;
                }
            }

            _repo.Update(entitiesLst);
        }

        public void Delete(string id)
        {
            _repo.Delete(GetById(id));
        }

        public void DeleteAll()
        {
            _repo.DeleteAll();
        }

        public void Delete(T entity)
        {
            _repo.Delete(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return _repo.Where(predicate);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate, string[] fieldsToExclude)
        {
            return
                _repo.Collection.FindAll()
                    .SetFields(Fields.Exclude(fieldsToExclude))
                    .SetLimit(50)
                    .AsQueryable()
                    .Where(predicate);
        }
    }
}