using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lengkeng.Models
{
    public class Price
    {
        [Key]
        public int PriceId { get; set; }

        [Required(ErrorMessage = "Không thể để trống trường này.")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Không thể để trống trường này.")]
        public int UnitPrice { get; set; }
        public bool IsDelete { get; set; }

        public DateTime DateStart { get; set; }
    }
}