using Learning.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learning.WebSite.Models
{
    public class UserAddClass
    {
        public int UserId { get; set; }
        public int SelectedClassId { get; set; }       
    }
}