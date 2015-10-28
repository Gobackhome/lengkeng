using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lengkeng.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(350)]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "Tiêu Đề")]
        //[RegularExpression("^([a-zA-Z '])+$", ErrorMessage = "Tiêu Đề chỉ chứa ký tự từ a-z và A-Z.")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        [Display(Order = 2, Name = "Nội Dung")]
        public string content { get; set; }
        [StringLength(300)]
        [DataType(DataType.Text)]
        [Display(Order = 3, Name = "Ảnh Minh Họa")]
        public string Thumbnails { get; set; }
        [StringLength(300)]
        [DataType(DataType.Text)]
        [Display(Order = 4, Name = "Ảnh Minh Họa")]
        public string Thumbnails2 { get; set; }
        public bool IsDelete { get; set; }
    }
}