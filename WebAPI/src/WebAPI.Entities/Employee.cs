using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Employee
    {
        /// <summary>
        /// Employee ID.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Employee Name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Employee's department.
        /// </summary>
        [Required]
        public string Department { get; set; }

        /// <summary>
        /// Employee date of joining.
        /// </summary>
        [Required]
        public DateTime DateOfJoining { get; set; }

        /// <summary>
        /// Employee photo file name.
        /// </summary>
        [Required]
        public string PhotoFileName { get; set; }
    }
}