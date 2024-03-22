using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Card.Request;
using SPay.BO.DTOs.Card.Response;
using SPay.BO.DTOs.CardType.Request;
using SPay.BO.DTOs.CardType.Response;
using SPay.BO.DTOs.Membership.Request;
using SPay.BO.DTOs.Membership.Response;
using SPay.BO.DTOs.Order.Request;
using SPay.BO.DTOs.Order.Response;
using SPay.BO.DTOs.PromotionPackage.Request;
using SPay.BO.DTOs.PromotionPackage.Response;
using SPay.BO.DTOs.Role.Response;
using SPay.BO.DTOs.Store.Request;
using SPay.BO.DTOs.Store.Response;
using SPay.BO.DTOs.StoreCategory.Request;
using SPay.BO.DTOs.StoreCategory.Response;
using SPay.BO.DTOs.Transaction.Response;
using SPay.BO.DTOs.User.Request;
using SPay.BO.DTOs.User.Response;
using SPay.Repository.Enum;
using SPay.Repository.ResponseDTO;
using SPay.Service.Utils;

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
			CreateMap<Transaction, TransactionResponse>()
				.ForMember(dest => dest.Type, opt =>
					opt.MapFrom(src =>
						src.OrderKeyNavigation != null ? Constant.Transaction.TYPE_PURCHASE :
						(src.WithdrawKeyNavigation != null ? Constant.Transaction.TYPE_WITHDRAWL : Constant.Transaction.UNDEFINE_STR)))
				.ForMember(dest => dest.Amount, opt =>
					opt.MapFrom(src =>
						src.OrderKeyNavigation != null ? src.OrderKeyNavigation.TotalAmount : 
						(src.WithdrawKeyNavigation != null ? src.WithdrawKeyNavigation.TotalAmount : Constant.Transaction.UNDEFINE_AMOUNT)))
				.ForMember(dest => dest.Receiver, opt =>
					opt.MapFrom(src =>
						src.OrderKeyNavigation != null ? src.OrderKeyNavigation.StoreKey :
						(src.WithdrawKeyNavigation != null ? src.WithdrawKeyNavigation.UserKey : Constant.Transaction.UNDEFINE_STR)))
				.ForMember(dest => dest.Sender, opt =>
					opt.MapFrom(src =>
						src.OrderKeyNavigation != null ? src.OrderKeyNavigation.MembershipKeyNavigation.UserKey :
						(src.WithdrawKeyNavigation != null ? Constant.Transaction.WITHDRAW_SENDER : Constant.Transaction.UNDEFINE_STR)))
				.ForMember(dest => dest.DescriptionDetails, opt =>
					opt.MapFrom(src =>
						src.OrderKeyNavigation != null ? src.OrderKeyNavigation.Description :
						(src.WithdrawKeyNavigation != null ? Constant.Transaction.WITHDRAW_DETAILS_DES : Constant.Transaction.UNDEFINE_STR)))
				.ForMember(dest => dest.TransactionDate, opt =>
					opt.MapFrom(src =>
						src.OrderKeyNavigation != null ? src.OrderKeyNavigation.InsDate :
						(src.WithdrawKeyNavigation != null ? src.WithdrawKeyNavigation.InsDate : DateTimeHelper.GetDateTimeNow())));

			CreateMap<CreateOrUpdateStoreCateRequest, StoreCategory>();

			CreateMap<Membership, MembershipResponse>();
			CreateMap<CreateOrUpdateMembershipRequest, Membership>();

			CreateMap<Store, StoreResponse>()
				.ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.UserKeyNavigation.Fullname))
				.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.UserKeyNavigation.PhoneNumber))
				.ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.WalletKeyNavigation.Balance))
				.ForMember(dest => dest.StoreCategoryName, opt => opt.MapFrom(src => src.StoreCateKeyNavigation.CategoryName));

			CreateMap<CreateStoreRequest, Store>();
			CreateMap<CreateStoreRequest, User>();

			CreateMap<UpdateStoreRequest, Store>();
			CreateMap<UpdateStoreRequest, User>();

			CreateMap<Card, CardResponse>()
				             .ForMember(dest => dest.No, opt => opt.Ignore())
							 .ForMember(dest => dest.CardTypeName, opt => opt.MapFrom(src => src.CardTypeKeyNavigation.CardTypeName))
							 .ForMember(dest => dest.UsaebleAmount, opt => opt.MapFrom(src => src.PromotionPackageKeyNavigation.UsaebleAmount))
							 .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.PromotionPackageKeyNavigation.DiscountPercentage))
							 .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.PromotionPackageKeyNavigation.Price))
							 .ForMember(dest => dest.NumberDate, opt => opt.MapFrom(src => src.PromotionPackageKeyNavigation.NumberDate))
							 .ForMember(dest => dest.PackageName, opt => opt.MapFrom(src => src.PromotionPackageKeyNavigation.PackageName));

			CreateMap<CreateOrUpdateCardRequest, Card>();

			CreateMap<Order, OrderResponse>()
				.ForMember(dest => dest.FromUserName, opt => opt.MapFrom(src => src.MembershipKeyNavigation.UserKey))
				.ForMember(dest => dest.ByCardName, opt => opt.MapFrom(src => src.MembershipKeyNavigation.CardKey))
				.ForMember(dest => dest.ToStoreName, opt => opt.MapFrom(src => src.StoreKeyNavigation.StoreName))
				.ForMember(dest => dest.StoreCateName, opt => opt.MapFrom(src => src.StoreKeyNavigation.StoreCateKeyNavigation.CategoryName));

			CreateMap<CreateOrderRequest, Order>();

			CreateMap<User, UserResponse>();

			
			CreateMap<CreateOrUpdateUserRequest, User>();

			CreateMap<MembershipResponseDTO, MembershipResponse>()
				.ForMember(dest => dest.MembershipKey, opt => opt.MapFrom(src => src.Membership.MembershipKey))
				.ForMember(dest => dest.UserKey, opt => opt.MapFrom(src => src.Membership.UserKey))
				.ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.Card.CardName))
				.ForMember(dest => dest.CardTypeName, opt => opt.MapFrom(src => src.CardType.CardTypeName))
				.ForMember(dest => dest.CardDescription, opt => opt.MapFrom(src => src.Card.Description))
				.ForMember(dest => dest.StoreCateName, opt => opt.MapFrom(src => src.StoreCategory.CategoryName))
				.ForMember(dest => dest.UsaebleAmount, opt => opt.MapFrom(src => src.PromotionPackage.UsaebleAmount))
				.ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Wallet.Balance))
				.ForMember(dest => dest.WithdrawAllowed, opt => opt.MapFrom(src => src.PromotionPackage.WithdrawAllowed))
				.ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom(src => src.Membership.ExpiritionDate))
				.ForMember(dest => dest.IsDefaultMembership, opt => opt.MapFrom(src => src.Membership.IsDefaultMembership));
		}
	}
}
