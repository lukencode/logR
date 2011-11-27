using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace logR.Demo.Models
{
    public class HomeViewModel
    {
        public string Message { get; set; }
        public string SelectedLevel { get; set; }

        public List<SelectListItem> Levels { get; set; }
    }
}