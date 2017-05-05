using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorld.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "name null")]
        public string UserName { get; set; }
    }
}