using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lengkeng.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "Username")]
        //[RegularExpression("^([a-zA-Z0-9 '])+$", ErrorMessage = "Username chỉ chứa ký tự từ a-z, A-Z và số.")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserStatus? Userstatus { get; set; }
        [Required]
        [StringLength(100)]
        [DataType(DataType.Text)]
        [Display(Order = 2, Name = "Tên Đầy Đủ")]
        //[RegularExpression("^([a-zA-Z '])+$", ErrorMessage = "Tên chỉ chứa ký tự từ a-z, A-Z.")]
        public string Fullname { get; set; }
        [StringLength(12)]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression("^([01]?[- .]?\\(?[2-9]\\d{2}\\)?[- .]?\\d{3}[- .]?\\d{4})+$",
        //        ErrorMessage = "Số điện thoại không đúng định dạng.")]
        [Display(Order = 3, Name = "Số điện thoại")]
        public string Tel { get; set; }
        public bool IsDelete { get; set; }
        public virtual ICollection<News> Newss{ get; set; }
    }
}