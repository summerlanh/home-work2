using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorld.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage = "name null")]
        public string Name { get; set; }

        [Required(ErrorMessage = "phone null")]
        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "email null")]
        public string Email { get; set; }

        [Required(ErrorMessage = "bool null")]
        public bool? WillAttend { get; set; }
    }
}