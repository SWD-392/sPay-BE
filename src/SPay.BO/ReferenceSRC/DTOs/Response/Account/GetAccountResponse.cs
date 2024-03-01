using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Globalization;

namespace SPay.BO.RerferenceSRC.DTOs.Response.Account
{
    public class GetAccountResponse
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }

        public GetAccountResponse()
        {

        }

        public GetAccountResponse(int id, string firstName, string lastName, string email, bool isActive, string role)
        {
            AccountId = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsActive = isActive;
            Role = role;
        }
    }
}
