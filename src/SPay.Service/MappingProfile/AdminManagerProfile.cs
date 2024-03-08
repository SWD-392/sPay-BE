using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.Card.Request;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.DTOs.Admin.Customer.ResponseModel;
using SPay.BO.DTOs.Admin.Store.Response;

namespace SPay.Service.MappingProfile
{
    public class AdminManagerProfile : Profile
    {
        public AdminManagerProfile() {
            CreateMap<Customer, CustomerResponse > ()
                .ForMember(dest => dest.No, opt => opt.Ignore())
				.ForMember(dest => dest.CustomerKey, opt => opt.MapFrom(src => src.CustomerKey))
				.ForMember(dest => dest.UserKey, opt => opt.MapFrom(src => src.UserKey))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
				.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.UserKeyNavigation.Fullname))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.UserKeyNavigation.Status))
				.ForAllOtherMembers(src => src.Ignore());

			CreateMap<Store, StoreResponse>()
                .ForMember(dest => dest.No, opt => opt.Ignore())
                .ForMember(dest => dest.StoreKey, opt => opt.MapFrom(src => src.StoreKey))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StoreCategory, opt => opt.MapFrom(src => src.CategoryKeyNavigation.Name))
                .ForMember(dest => dest.StoreCategoryKey, opt => opt.MapFrom(src => src.CategoryKey))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.UserKeyNavigation.Fullname))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Wallets.FirstOrDefault(x => x.WalletKey == src.StoreKey).Balance ?? 0))
                .ForMember(dest => dest.InsDate, opt => opt.MapFrom(src => src.UserKeyNavigation.InsDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
				.ForAllOtherMembers(src => src.Ignore());

			CreateMap<Card, CardResponse>()
                .ForMember(dest => dest.No, opt => opt.Ignore())
                .ForMember(dest => dest.CardKey, opt => opt.MapFrom(src => src.CardKey))
				.ForMember(dest => dest.CardTypeKey, opt => opt.MapFrom(src => src.CardTypeKey))
                .ForMember(dest => dest.CardTypeName, opt => opt.MapFrom(src => src.CardTypeKeyNavigation.CardTypeName))
				.ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.CardNumber))
                .ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.CardName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.InsDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.DateNumber, opt => opt.MapFrom(src => src.NumberDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
				.ForMember(dest => dest.MoneyValue, opt => opt.MapFrom(src => src.MoneyValue))
				.ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
				.ForAllOtherMembers(src => src.Ignore());

            CreateMap<CreateCardRequest, Card>()
				.ForMember(dest => dest.CardTypeKey, opt => opt.MapFrom(src => src.CardTypeKey))
				.ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardNumber))
				.ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.NumberDate, opt => opt.MapFrom(src => src.NumberDate))
				.ForMember(dest => dest.MoneyValue, opt => opt.MapFrom(src => src.MoneyValue))
				.ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
				.ForAllOtherMembers(src => src.Ignore());
		}
    }
}
