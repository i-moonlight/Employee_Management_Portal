using System;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Domain.Core.Entities
{
    /// <summary>
    /// User model.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// User full name.
        /// </summary>
        public string FullName { get; set; }
        
        /// <summary>
        /// User create date.
        /// </summary>
        public DateTime DateCreated { get; set; }
        
        /// <summary>
        /// User modify date.
        /// </summary>
        public DateTime DateModified { get; set; }
    }
}