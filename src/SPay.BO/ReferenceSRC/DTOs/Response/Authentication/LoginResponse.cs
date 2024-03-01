using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.RerferenceSRC.DTOs.Response.Authentication
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }

        public LoginResponse(int id, string email, string firstName, string role, bool status)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            Role = role;
            Status = status;
        }
    }
}
