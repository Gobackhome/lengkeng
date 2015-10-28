using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace lengkeng.Models
{
    public class Welcome
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "Tiêu Đề")]
        //[RegularExpression("^([a-zA-Z '])+$", ErrorMessage = "Tiêu Đề chỉ chứa ký tự từ a-z và A-Z.")]
        public string Title { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }
        [Required]
        [StringLength(500)]
        public string LinkVideo { get; set; }
        [StringLength(500)]
        public string Background { get; set; }
        public bool IsDelete { get; set; }
    }
}