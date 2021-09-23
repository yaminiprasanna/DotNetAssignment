using Common.Models;
using EFlib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types;

namespace Services
{
    public class LoginService:ILogin
    {
        private readonly EmployeeContext m_DBContext;

        public LoginService(EmployeeContext dbContext)
        {
            m_DBContext = dbContext;

        }


        public async Task<UserResponse> Authenticate(string employeeName, string password)
        {
            UserResponse response = new UserResponse();
            try
            {
                using (var context = new EmployeeContext())
                {
                    var user = await Task.Run(() => context.Users.SingleOrDefault(x => x.EmployeeName == employeeName && x.Password == password));

                    // return null if user not found
                    if (user == null)
                    {
                        response.Message = "Login failed";
                    }
                    else
                    {
                        //return UserDTO.WithoutPassword();
                        response.user.EmployeeName = user.EmployeeName;
                        response.user.ContactNo = user.ContactNo;
                        response.user.Password = user.Password;
                        response.user.Email = user.Email;

                        response.Message = "Login Successful.";
                    }
                       
                }
            }
            catch(Exception ex)
            {
                response.Message = "Error Occurred. Please contact Administrator";

            }


            return response;
            
        }
    }
}
