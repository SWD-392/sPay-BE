using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.DTOs.Admin.Store.Response;
using SPay.BO.DTOs.Customer.RespondModel;

namespace SPay.Service.MappingProfile
{
    public class AdminManagerProfile : Profile
    {
        public AdminManagerProfile() {
            CreateMap<GetAllCustomerResponse, Customer>().ReverseMap();
            CreateMap<StoreOwner, GetAllStoreResponse>()
                .ForMember(dest => dest.No, opt => opt.Ignore())
                .ForMember(dest => dest.StoreKey, opt => opt.MapFrom(src => src.StoreKeyNavigation.StoreKey))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.StoreKeyNavigation.Name))
                .ForMember(dest => dest.StoreCategory, opt => opt.MapFrom(src => src.StoreKeyNavigation.CategoryKeyNavigation.Name))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.OwnerName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.StoreKeyNavigation.Phone))
                .ForMember(dest => dest.InsDate, opt => opt.MapFrom(src => src.CreateAt))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StoreKeyNavigation.Status))
                .ReverseMap();

            CreateMap<Card, GetAllCardResponse>()
                .ForMember(dest => dest.No, opt => opt.Ignore())
                .ForMember(dest => dest.CardKey, opt => opt.MapFrom(src => src.CardKey))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.CardNumber))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CardTypeKeyNavigation.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CardTypeKeyNavigation.Description))
                .ForMember(dest => dest.InsDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.ExpiryDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();
        }
    }
}
