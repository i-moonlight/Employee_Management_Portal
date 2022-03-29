using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Department
    {
        /// <summary>
        /// Department ID.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Department Name.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}