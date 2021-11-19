using System;

namespace WebAPI.Entities.Models
{
    public class Department
    {
        /// <summary>
        /// Department ID.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Department Name.
        /// </summary>
        public string Name { get; set; }
    }
}