using System.Collections;

namespace WebAPI.Repositories.Interfaces
{
    public interface ICrudRepository<T>
    {
        public IEnumerable Read();
        public T Create(T model);
        public T Update(T model);
        public void Delete(int id);
    }
}