using Agricargo.Domain.Entities;

namespace Agricargo.Infrastructure.Repositories
{
    public interface IBaseRepository <T> where T : class
    {
        public void Add(T entity);
        public void Delete(T entity);
        public T Get(int id);
        public List<T> Get();
        public void Update(T entity);
    }
}