using System;
using WebAPI.Entities;
using WebAPI.UseCases.Common.Mappings;

namespace WebAPI.UseCases.Common.Dto
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