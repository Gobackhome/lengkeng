﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lengkeng.ViewModels
{
    public class ItemFollowCategoryViewModel
    {
        public CategoryViewModel categoryViewModel{ get; set; }
        public List<ItemViewModel> listItemViewModel { get; set; }
    }
}