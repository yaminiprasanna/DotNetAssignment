using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
