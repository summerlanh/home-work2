using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BirthdayCard.Models
{
    public class CardForm
    {
        [Required(ErrorMessage = "Please enter From email")]
        [EmailAddress]
        public string FromEmail { get; set; }

        [Required(ErrorMessage = "Please enter To email")]
        [EmailAddress]
        public string ToEmail { get; set; }

        [Required(ErrorMessage = "Please enter Message")]
        public string Message { get; set; }
    }
}