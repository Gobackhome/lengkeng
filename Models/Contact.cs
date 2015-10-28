using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using lengkeng.ViewModels;
namespace lengkeng.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "Mô Tả")]
        public string Descriptions { get; set; }
        [Required]
        [EmailAddress(ErrorMessage="Email phải là kiểu vidu@vidu.com")]
        [StringLength(200,ErrorMessage="Email không thể quá 200 ký tự")]
        public string Email { get; set; }
        [Phone]
        [StringLength(12,ErrorMessage="Số điện thoại không thể hơn 12 chữ số.")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression("^([01]?[- .]?\\(?[2-9]\\d{2}\\)?[- .]?\\d{3}[- .]?\\d{4})+$",
        //        ErrorMessage = "Số điện thoại không đúng định dạng.")]
        [Display(Order = 2, Name = "Số điện thoại")]
        public string Tel { get; set; }
        [Required]
        [StringLength(300,ErrorMessage="Địa chỉ không thể quá 300 ký tự")]
        [DataType(DataType.Text)]
        [Display(Order = 3, Name = "Địa Chỉ")]
        //[RegularExpression("^([0-9A-Za-z #.,])+$", ErrorMessage = "Địa Chỉ không thể chứa ký tự đặc biệt.")]
        public string Address { get; set; }
        [StringLength(150)]
        public string TimeStartEnd { get; set; }
        public bool IsDelete { get; set; }

       
    }
}