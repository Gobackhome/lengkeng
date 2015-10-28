using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lengkeng.ViewModels
{
    public class SliderViewModel
    {
        public int IDDiv { get; set; }
        public string Background { get; set; }
        public string Thumb1 { get; set; }
        public string Thumb2 { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public bool IsOnlyImage { get; set; }
    }
}