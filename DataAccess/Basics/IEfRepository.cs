using Entities;

namespace DataAccess.Basics

{
    public interface IEfRepository<T> where T : DbEntity, new()
    {
        T Get(int id);
        T Create(T entity);
        T Update(T entity);
        T Delete(int id);
        List<T> Get(Func<T, bool> exp);
    }
}
