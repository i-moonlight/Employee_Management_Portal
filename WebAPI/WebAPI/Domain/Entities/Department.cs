using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Domain.Entities
{
    public class Department
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
    }
}
