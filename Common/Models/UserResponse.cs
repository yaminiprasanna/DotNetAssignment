using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class UserResponse
    {
        public UserResponse() {
            this.user = new UserDTO();
        }

        public UserDTO user { get; set; }
        public string Status { set; get; }
        public string Message { set; get; }
    }
}
