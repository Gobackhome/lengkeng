using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lengkeng.Models;

namespace lengkeng.ViewModels
{
    public class ItemViewModel 
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemInfo { get; set; }
        public int Prices { get; set; }
        public string ThumbnailPath { get; set; }
        public bool IsActive { get; set; }
        public ItemStatus ItemStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}