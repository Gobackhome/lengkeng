using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace lengkeng.Models
{
    public class YoutubeHome
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        [Required]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }
        [Required]
        [StringLength(50)]
        public string YoutubeId1 { get; set; }
        [StringLength(300)]
        public string TitleYoutubeId1 { get; set; }
        [Required]
        [StringLength(50)]
        public string YoutubeId2 { get; set; }
        [StringLength(300)]
        public string TitleYoutubeId2 { get; set; }
        [StringLength(300)]
        public string Thumb { get; set; }
        public bool IsDelete{ get; set; }
    }
}