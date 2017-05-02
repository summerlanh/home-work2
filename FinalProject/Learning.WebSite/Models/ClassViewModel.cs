using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learning.WebSite.Models
{
    public class ClassViewModel
    {
        public List<ClassModel> ClassList;
        public SelectList Class { get; set; }
        public int ClassID { get; set; }
    }
}