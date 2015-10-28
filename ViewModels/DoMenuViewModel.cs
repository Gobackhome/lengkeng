using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lengkeng.ViewModels
{
    public class DoMenuViewModel : BaseViewModel
    {
        public List<CategoryViewModel> categoryViewModels { get; set; }
        public ItemFollowCategoryViewModel itemFollowCategoryViewModels { get; set; }
    }
}