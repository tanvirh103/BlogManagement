using BlogManagement.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogManagement.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string FirstName { get; set; }
        [StringLength(10)]
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [Phone]
        [StringLength(14,MinimumLength =11)]
        public string Phone { get; set; }
 
        public string Role { get; set; }
        public string Status { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        //[StringLength(20,MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}