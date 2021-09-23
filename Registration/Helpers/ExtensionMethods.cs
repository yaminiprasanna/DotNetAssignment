using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<UserDTO> WithoutPasswords(this IEnumerable<UserDTO> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static UserDTO WithoutPassword(this UserDTO user)
        {
            user.Password = null;
            return user;
        }
    }
}
