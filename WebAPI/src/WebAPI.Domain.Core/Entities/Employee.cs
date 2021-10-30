using System;

namespace WebAPI.Domain.Core.Entities
{
    public class Employee
    {
        /// <summary>
        /// Employee ID.
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// Employee Name.
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// Employee's department.
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// Employee date of joining.
        /// </summary>
        public DateTime DateOfJoining { get; set; }
        
        /// <summary>
        /// Employee photo file name.
        /// </summary>
        public string PhotoFileName { get; set; }
    }
}