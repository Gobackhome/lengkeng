using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lengkeng.ViewModels
{
    public class HomeIndexViewModel : BaseViewModel
    {
        public HomeWelcomeViewModel homeWelcomeVM { get; set; }
        public List<HomeServiceViewModel> listServicVM { get; set; }
        public List<HomeNewsViewModel> listNewsVM { get; set; }
        public HomeVideoViewModel homeVideoVM { get; set; }
        public HomeStepViewModel homeStepVM { get; set; }
    }
}