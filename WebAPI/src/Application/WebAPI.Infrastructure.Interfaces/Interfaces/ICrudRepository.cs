using System;
using System.Collections;

namespace WebAPI.Infrastructure.Interfaces.Interfaces
{
    public interface ICrudRepository<T>
    {
        public IEnumerable Read();
        public IEnumerable ReadAll();
        public T Create(T model);
        public T Update(T model);
        public void Delete(Guid id);
        public string GetFileName(Guid id);
    }
}