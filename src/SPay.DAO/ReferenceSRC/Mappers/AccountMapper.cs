using AutoMapper;
using SPay.BO.ReferenceSRC.Models;
using SPay.BO.RerferenceSRC.DTOs.Request.Account;
using SPay.BO.RerferenceSRC.DTOs.Response.Account;

namespace SPay.DAO.ReferenceSRC.Mappers
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<CreateAccountRequest, Account>();
            CreateMap<Account, UpdateAccountResponse>();
        }
    }
}
