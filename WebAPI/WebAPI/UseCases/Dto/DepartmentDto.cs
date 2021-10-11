using System;
using WebAPI.Domain.Entities;
using WebAPI.UseCases.Mappings;

namespace WebAPI.UseCases.Dto
{
    /// <summary>
    /// Sets a properties of the data transfer object for department entity.
    /// </summary>
    public class DepartmentDto : IMapFrom<Department>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
