using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace lengkeng.ViewModels
{
    public class HeaderViewModel 
    {
        public List<CategoryViewModel> listCategoryViewModel { get; set; }
        public string Logo { get; set; }
        public List<SliderViewModel> listSliderViewModel{ get; set; }
    }
}