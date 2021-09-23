using System;
using System.Collections.Generic;

#nullable disable

namespace EFlib.Entities
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }
        public string EmployeeName { get; set; }
    }
}
