using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lengkeng.Models
{
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }

        [Required(ErrorMessage = "Không thể để trống trường này.")]
        public int ItemId { get; set; }
       
        [Required(ErrorMessage = "Không thể để trống trường này.")]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "Tiêu Đề")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Không thể để trống trường này.")]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "Mô Tả")]
        public string Descriptions { get; set; }

        [Required(ErrorMessage = "Không thể để trống trường này.")]
        [Range(0, 100, ErrorMessage = "Chỉ nhập 0-100.")]
        public int Amount { get; set; }

        [StringLength(300)]
        [DataType(DataType.Text)]
        [Display(Order = 2, Name = "Ảnh Minh Họa")]
        public string Thumbnails { get; set; }

        public bool IsDelete { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}