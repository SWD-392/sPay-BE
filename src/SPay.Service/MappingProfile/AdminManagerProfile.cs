using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.DTOs.Admin.Customer.ResponseModel;
using SPay.BO.DTOs.Admin.Store.Response;

namespace SPay.Service.MappingProfile
{
    public class AdminManagerProfile : Profile
    {
        public AdminManagerProfile() {
            CreateMap<CustomerResponse, Customer>().ReverseMap();
            CreateMap<Store, StoreResponse>()
                .ForMember(dest => dest.No, opt => opt.Ignore())
                .ForMember(dest => dest.StoreKey, opt => opt.MapFrom(src => src.StoreKey))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StoreCategory, opt => opt.MapFrom(src => src.CategoryKeyNavigation.Name))
                .ForMember(dest => dest.StoreCategoryKey, opt => opt.MapFrom(src => src.CategoryKey))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.OwnerKeyNavigation.OwnerName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Wallets.FirstOrDefault(x => x.WalletKey == src.StoreKey).Balance ?? 0))
                .ForMember(dest => dest.InsDate, opt => opt.MapFrom(src => src.OwnerKeyNavigation.CreateAt))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();

            CreateMap<Card, CardResponse>()
                .ForMember(dest => dest.No, opt => opt.Ignore())
                .ForMember(dest => dest.CardKey, opt => opt.MapFrom(src => src.CardKey))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.CardNumber))
                .ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.CardTypeKeyNavigation.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CardTypeKeyNavigation.Description))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Deposits.Where(d => d.CardKey == src.CardKey).FirstOrDefault().Value))
                .ForMember(dest => dest.InsDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.DateNumber, opt => opt.MapFrom(src => src.NumberDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();
        }
    }
}
