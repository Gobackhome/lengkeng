using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace lengkeng.Models
{
    public class Category
    {
        [Key]
        [Display(Order = 1, Name = "ID Danh Mục")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Không thể để trống trường này.")]
        [StringLength(100, ErrorMessage = "Nhập không quá 100 ký tự.")]
        [DataType(DataType.Text)]
        [Display(Order = 2, Name = "Tên Danh Mục")]
        //[RegularExpression("^([a-zA-Z '])+$", ErrorMessage = "Tên Danh Mục chỉ chứa ký tự từ a-z, A-Z.")]
        public string CategoryName { get; set; }

        [StringLength(500)]
        [DataType(DataType.Text)]
        [Display(Order = 3, Name = "Ảnh Minh Họa")]
        //[RegularExpression("^([a-zA-Z/.~ '])+$", ErrorMessage = "Ảnh Minh Họa chỉ chứa ký tự từ a-z, A-Z,/,.,~")]
        public string Thumbnail { get; set; }

        public bool IsDelete { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}