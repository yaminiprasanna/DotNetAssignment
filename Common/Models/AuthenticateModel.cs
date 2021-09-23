using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
