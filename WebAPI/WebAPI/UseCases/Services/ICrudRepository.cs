﻿using System;
using System.Collections;

namespace WebAPI.UseCases.Services
{
    public interface ICrudRepository<T>
    {
        public IEnumerable Read();
        public T Read(Guid id);
        public IEnumerable GetDepartmentNameList();
        public void Create(T model);
        public void Update(T model);
        public void Delete(Guid id);
        public string GetPhotoName(Guid id);
    }
}
