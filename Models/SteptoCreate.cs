using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace lengkeng.Models
{
    public class SteptoCreate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "Tiêu Đề")]
        //[RegularExpression("^([a-zA-Z '])+$", ErrorMessage = "Tiêu Đề chỉ chứa ký tự từ a-z và A-Z.")]
        public string Title { get; set; }
        [Required]
        [Display(Order = 2, Name = "Mô Tả Các Bước")]
        //[RegularExpression("^([a-zA-Z- '])+$", ErrorMessage = "Tiêu Đề chỉ chứa ký tự từ a-z và A-Z.")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Steps { get; set; }
        [StringLength(300)]
        [DataType(DataType.Text)]
        [Display(Order = 3, Name = "Khuyến cáo")]
        public string Slogan { get; set; }
        [StringLength(300)]
        [DataType(DataType.Text)]
        [Display(Order = 4, Name = "Ảnh Minh Họa")]
        public string ThumbnailPath { get; set; }
       
        public bool IsDelete { get; set; }
    }
}