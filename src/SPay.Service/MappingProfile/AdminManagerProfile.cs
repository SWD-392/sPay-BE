using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.Order.Response;
using SPay.BO.DTOs.Card.Request;
using SPay.BO.DTOs.Card.Response;
using SPay.BO.DTOs.CardType.Request;
using SPay.BO.DTOs.CardType.Response;
using SPay.BO.DTOs.PromotionPackage.Request;
using SPay.BO.DTOs.PromotionPackage.Response;
using SPay.BO.DTOs.Role.Response;
using SPay.BO.DTOs.Store.Request;
using SPay.BO.DTOs.Store.Response;
using SPay.BO.DTOs.StoreCategory.Request;
using SPay.BO.DTOs.StoreCategory.Response;
using SPay.BO.DTOs.User.Request;
using SPay.BO.DTOs.User.Response;

namespace SPay.Service.MappingProfile
{
    public class AdminManagerProfile : Profile
    {
        public AdminManagerProfile() {

			CreateMap<Role, RoleResponse>();

			CreateMap<PromotionPackage, PromotionPackageResponse>();
			CreateMap<CreateOrUpdatePromotionPackageRequest, PromotionPackage>();

			CreateMap<CardType, CardTypeResponse>();
			CreateMap<CreateOrUpdateCardTypeRequest, CardType>();

			CreateMap<StoreCategory, StoreCateResponse>();
			CreateMap<CreateOrUpdateStoreCateRequest, StoreCategory>();

			CreateMap<Store, StoreResponse>();
			CreateMap<CreateOrUpdateStoreRequest, Store>();

			CreateMap<Card, CardResponse>()
				             .ForMember(dest => dest.No, opt => opt.Ignore())
							 .ForMember(dest => dest.CardTypeName, opt => opt.MapFrom(src => src.CardTypeKeyNavigation.CardTypeName))
							 .ForMember(dest => dest.ValueUsed, opt => opt.MapFrom(src => src.PromotionPackageKeyNavigation.ValueUsed))
							 .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.PromotionPackageKeyNavigation.DiscountPercentage))
							 .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.PromotionPackageKeyNavigation.Price))
							 .ForMember(dest => dest.NumberDate, opt => opt.MapFrom(src => src.PromotionPackageKeyNavigation.NumberDate))
							 .ForMember(dest => dest.PackageName, opt => opt.MapFrom(src => src.PromotionPackageKeyNavigation.PackageName));

			CreateMap<CreateOrUpdateCardRequest, Card>();

			CreateMap<User, UserResponse>();
			CreateMap<CreateOrUpdateUserRequest, User>();


			//CreateMap<Store, StoreResponse>()
			//             .ForMember(dest => dest.No, opt => opt.Ignore())
			//             .ForMember(dest => dest.StoreKey, opt => opt.MapFrom(src => src.StoreKey))
			//             .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Name))
			//             .ForMember(dest => dest.StoreCategory, opt => opt.MapFrom(src => src.CategoryKeyNavigation.Name))
			//             .ForMember(dest => dest.StoreCategoryKey, opt => opt.MapFrom(src => src.CategoryKey))
			//             .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.UserKeyNavigation.Fullname))
			//             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
			//             .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Wallets.FirstOrDefault(x => x.WalletKey == src.StoreKey).Balance ?? 0))
			//             .ForMember(dest => dest.InsDate, opt => opt.MapFrom(src => src.UserKeyNavigation.InsDate))
			//             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
			//	.ForAllOtherMembers(src => src.Ignore());

			//CreateMap<Card, CardResponse>()
			//             .ForMember(dest => dest.No, opt => opt.Ignore())
			//             .ForMember(dest => dest.CardKey, opt => opt.MapFrom(src => src.CardKey))
			//	.ForMember(dest => dest.CardTypeKey, opt => opt.MapFrom(src => src.CardTypeKey))
			//             .ForMember(dest => dest.CardTypeName, opt => opt.MapFrom(src => src.CardTypeKeyNavigation.CardTypeName))
			//	.ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.CardNumber))
			//             .ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.CardName))
			//             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
			//             .ForMember(dest => dest.InsDate, opt => opt.MapFrom(src => src.CreatedAt))
			//             .ForMember(dest => dest.DateNumber, opt => opt.MapFrom(src => src.NumberDate))
			//             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
			//	.ForMember(dest => dest.MoneyValue, opt => opt.MapFrom(src => src.MoneyValue))
			//	.ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
			//	.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
			//	.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
			//	.ForAllOtherMembers(src => src.Ignore());

			//         CreateMap<CreateCardRequest, Card>()
			//	.ForMember(dest => dest.CardTypeKey, opt => opt.MapFrom(src => src.CardTypeKey))
			//	.ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardNumber))
			//	.ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.Name))
			//	.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
			//	.ForMember(dest => dest.NumberDate, opt => opt.MapFrom(src => src.NumberDate))
			//	.ForMember(dest => dest.MoneyValue, opt => opt.MapFrom(src => src.MoneyValue))
			//	.ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
			//	.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
			//	.ForAllOtherMembers(src => src.Ignore());


			//CreateMap<Order, OrderResponse>()
			//	.ForMember(dest => dest.OrderKey, opt => opt.MapFrom(src => src.OrderKey))
			//	.ForMember(dest => dest.CustomerKey, opt => opt.MapFrom(src => src.CustomerKey))
			//	.ForMember(dest => dest.FromCustomer, opt => opt.MapFrom(src => src.CustomerKeyNavigation.UserKeyNavigation.Fullname))
			//	.ForMember(dest => dest.StoreKey, opt => opt.MapFrom(src => src.StoreKey))
			//	.ForMember(dest => dest.StoreKey, opt => opt.MapFrom(src => src.StoreKeyNavigation.UserKeyNavigation.Fullname))
			//	.ForMember(dest => dest.CardKey, opt => opt.MapFrom(src => src.CardKey))
			//	.ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.CardKeyNavigation.CardName))
			//	.ForMember(dest => dest.OrderDescription, opt => opt.MapFrom(src => src.OrderDescription))
			//	.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
			//	.ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
			//	.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

			CreateMap<CreateOrUpdateCardRequest, CardType>().ReverseMap();

		//	CreateMap<CreateUserModel, User>()
		//		.ForMember(dest => dest.UserKey, opt => opt.MapFrom(src => src.UserKey))
		//		.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.NumberPhone))
		//		.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
		//		.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
		//		.ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.FullName));


		//CreateMap<StoreCategory, StoreCateResponse>()
		//		.ForMember(dest => dest.StoreCategoryKey, opt => opt.MapFrom(src => src.StoreCategoryKey))
		//		.ForMember(dest => dest.StoreCategoryName, opt => opt.MapFrom(src => src.Name))
		//		.ReverseMap();

		}
	}
}
