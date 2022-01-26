using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Entities.Models
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
        /// User email confirmed.
        /// </summary>
        [Required]
        public new bool EmailConfirmed { get; set; } = true;

        /// <summary>
        /// User create date.
        /// </summary>
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        
        /// <summary>
        /// User modify date.
        /// </summary>
        [Required]
        public DateTime DateModified { get; set; }
    }
}