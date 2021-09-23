using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Types
{
    public interface ILogin
    {
        
          public  Task<UserResponse> Authenticate(string employeeName, string password);
    }
}
