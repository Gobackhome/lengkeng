using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lengkeng.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required(ErrorMessage = "Không thể để trống trường này.")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Không thể để trống trường này.")]
        [StringLength(200)]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "Tên Món")]
        //[RegularExpression("^([a-zA-Z '])+$", ErrorMessage = "Tên Món chỉ chứa ký tự từ a-z, A-Z.")]
        public string ItemName { get; set; }
        [DataType(DataType.Text)]
        [Display(Order = 2, Name = "Mô Tả Món")]
        public string ItemInfo { get; set; }

        [StringLength(500)]
        [DataType(DataType.Text)]
        [Display(Order = 3, Name = "Ảnh Minh Họa")]
        //[RegularExpression("^([a-zA-Z/.~ '])+$", ErrorMessage = "Ảnh Minh Họa chỉ chứa ký tự từ a-z, A-Z,/,.,~")]
        public string ThumbnailPath { get; set; }
        [Display(Order = 4, Name = "Kích Hoạt")]
        public bool IsActive { get; set; }
        [Display(Order = 5, Name = "Trạng Thái")]
        public ItemStatus ItemStatus { get; set; }
        [Display(Order = 6, Name = "Xóa")]
        public bool IsDelete { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}