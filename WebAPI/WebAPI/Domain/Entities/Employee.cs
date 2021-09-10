using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Domain.Entities
{
    public class Employee
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Department { get; set; }
        [Required] public DateTime DateOfJoining { get; set; }
        [Required] public string PhotoFileName { get; set; }
    }
}
