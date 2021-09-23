using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Types
{
    public interface IUser
    {
        public Task<UserResponse> RegisterUser(UserDTO user);

    }
}
