using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Basics

{
    public class EfRepository<T, TContext> : IEfRepository<T>
        where T : DbEntity, new()
        where TContext : DbContext, new()
    {
        public T Create(T entity)
        {
            using (var c = new TContext())
            {
                var a = c.Add(entity).Entity;
                c.SaveChanges();
                return a;
            }
        }

        public T Delete(int id)
        {
            using (var c = new TContext())
            {
                var entity = c.Set<T>().FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
                if(entity != null)
                    entity.IsDeleted = true;
                c.SaveChanges();
                return entity;
            }
        }

        public T Get(int id)
        {
            using (var c = new TContext())
            {
                return c.Set<T>().FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            }
        }

        public List<T> Get(Func<T, bool> exp)
        {
            using (var c = new TContext())
            {
                return c.Set<T>().Where(exp).ToList();
            }
        }

        public T Update(T entity)
        {
            using (var c = new TContext())
            {
                c.Set<T>().Update(entity);
                c.SaveChanges();
                return entity;
            }
        }
    }
}
