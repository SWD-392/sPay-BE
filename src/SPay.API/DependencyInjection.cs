﻿using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.Service.MappingProfile;
using SPay.Service;
using System.Reflection;
using SPay.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SPay.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }

        public static void AddMasterServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
			services.AddScoped<IWalletService, WalletService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IOrderService, OrderService>();

		}
	}
}