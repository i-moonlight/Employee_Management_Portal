using System;
using System.Collections;

namespace WebAPI.UseCases.Services
{
    public interface ICrudRepository<T>
    {
        public IEnumerable Read();
        public T Read(Guid id);
        public IEnumerable GetDepartmentNameList();
        public T Create(T model);
        public T Update(T model);
        public void Delete(Guid id);
        public string GetPhotoName(Guid id);
    }
}
