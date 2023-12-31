using System;
using WebAPI.Domain.Entities;
using WebAPI.UseCases.Mappings;

namespace WebAPI.UseCases.Dto
{
    /// <summary>
    /// Sets a properties of the data transfer object for employee entity.
    /// </summary>
    public class EmployeeDto : IMapFrom<Employee>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}
