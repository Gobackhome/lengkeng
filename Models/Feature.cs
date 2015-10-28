using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace lengkeng.Models
{
    public class Feature
    {
        [Key]
        public int  Id{ get; set; }
        [Required]
        [StringLength(200,ErrorMessage="Tiêu Đề không thể quá 200 ký tự.")]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "Tiêu Đề")]
        //[RegularExpression("^([a-zA-Z '])+$", ErrorMessage = "Tiêu Đề chỉ chứa ký tự từ a-z và A-Z.")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Order = 2, Name = "Mô Tả")]
        //[RegularExpression("^([a-zA-Z '])+$", ErrorMessage = "Mô Tả chỉ chứa ký tự từ a-z, A-Z.")]
        public string Description { get; set; }
        [StringLength(300)]
        [DataType(DataType.Text)]
        [Display(Order = 3, Name = "Ảnh Nền")]
        //[RegularExpression("^([a-zA-Z/.~ '])+$", ErrorMessage = "Ảnh Minh Họa chỉ chứa ký tự từ a-z, A-Z,/,.,~")]
        public string BackgroundImage{ get; set; }
        [StringLength(300)]
        [DataType(DataType.Text)]
        [Display(Order = 4, Name = "Ảnh Minh Họa 1")]
        //[RegularExpression("^([a-zA-Z/.~ '])+$", ErrorMessage = "Ảnh Minh Họa 2 chỉ chứa ký tự từ a-z, A-Z,/,.,~")]
        public string ThumbnailPath1 { get; set; }
        [StringLength(300)]
        [DataType(DataType.Text)]
        [Display(Order = 5, Name = "Ảnh Minh Họa 2")]
        //[RegularExpression("^([a-zA-Z/.~ '])+$", ErrorMessage = "Ảnh Đại Diện chỉ chứa ký tự từ a-z, A-Z,/,.,~")]
        public string ThumbnailPath2 { get; set; }
        public bool IsOnlyImage { get; set; }
        public bool IsDelete { get; set; }
    }
}