using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace lengkeng.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }

        public string DoContactAuthor { get; set; }
        public string DoContactEmail { get; set; }
        public string DoContactSubject { get; set; }
        public string DoContactMessage { get; set; }
    }
}