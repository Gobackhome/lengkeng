using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lengkeng.Models
{
    public class UserDetails
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}