using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace lengkeng.Models
{
    public class News
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
        [DataType(DataType.Text)]
        [Display(Order = 2, Name = "Nội Dung")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }
        [Required]
        [Display(Order = 3, Name = "Ngày Post")]
        public DateTime DatePost { get; set; }
        [Display(Order = 4, Name = "Ảnh Đại Diện")]
        public string Thumb { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool IsDelete { get; set; }
    }
}