using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace lengkeng.Models
{
    public class DoContact
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Author { get; set; }
        [StringLength(300)]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(300)]
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}