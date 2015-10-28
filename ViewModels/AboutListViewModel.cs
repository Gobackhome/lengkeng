using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lengkeng.ViewModels
{
    public class AboutListViewModel : BaseViewModel
    {
        public List<AboutViewModel> listAboutViewModel { get; set; }
        public string WelcomeTitle { get; set; }
        public string WelcomeContext { get; set; }
        public string WelcomeBackground { get; set; }
        public string WelcomeLinkVideo { get; set; }
    }
}