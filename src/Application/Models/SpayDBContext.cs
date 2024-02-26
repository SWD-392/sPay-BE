using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Application.Models;

public partial class SpayDBContext : DbContext
{
    public SpayDBContext()
    {
    }

    public SpayDBContext(DbContextOptions<SpayDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CardStoreCategory> CardStoreCategories { get; set; }

    public virtual DbSet<CardType> CardTypes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreCategory> StoreCategories { get; set; }

    public virtual DbSet<StoreOwner> StoreOwners { get; set; }

    public virtual DbSet<StoreWithdrawal> StoreWithdrawals { get; set; }

    public virtual DbSet<TopupMember> TopupMembers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=swd-spay-db-fptu.a.aivencloud.com;Port=20880;Database=db.2024.01.27;User Id=avnadmin;Password=AVNS_JfKQ8uqUwAV05y2rixU;SSL Mode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("admin_pkey");

            entity.ToTable("admin");

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.AdminName)
                .HasMaxLength(50)
                .HasColumnName("admin_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Admins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_admin_user");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("card_pkey");

            entity.ToTable("card");

            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.CardNumber).HasColumnName("card_number");
            entity.Property(e => e.CardTypeId).HasColumnName("card_type_id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiry_date");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.CardType).WithMany(p => p.Cards)
                .HasForeignKey(d => d.CardTypeId)
                .HasConstraintName("fk_card_card_type");

            entity.HasOne(d => d.Customer).WithMany(p => p.Cards)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("fk_card_customer");
        });

        modelBuilder.Entity<CardStoreCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("card_store_category_pkey");

            entity.ToTable("card_store_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardTypeId).HasColumnName("card_type_id");
            entity.Property(e => e.StoreCategoryId).HasColumnName("store_category_id");

            entity.HasOne(d => d.CardType).WithMany(p => p.CardStoreCategories)
                .HasForeignKey(d => d.CardTypeId)
                .HasConstraintName("fk_card_store_category_card_type");

            entity.HasOne(d => d.StoreCategory).WithMany(p => p.CardStoreCategories)
                .HasForeignKey(d => d.StoreCategoryId)
                .HasConstraintName("fk_card_store_category_store_category");
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.HasKey(e => e.CardTypeId).HasName("card_type_pkey");

            entity.ToTable("card_type");

            entity.Property(e => e.CardTypeId).HasColumnName("card_type_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.HasIndex(e => e.UserId, "customer_user_id_key").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.UserId)
                .HasConstraintName("fk_customer_user");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("ORDER_pkey");

            entity.ToTable("ORDER");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Card).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("fk_order_card");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("fk_order_store");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("store_pkey");

            entity.ToTable("store");

            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Category).WithMany(p => p.Stores)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("fk_store_store_category");
        });

        modelBuilder.Entity<StoreCategory>(entity =>
        {
            entity.HasKey(e => e.StoreCategoryId).HasName("store_category_pkey");

            entity.ToTable("store_category");

            entity.Property(e => e.StoreCategoryId).HasColumnName("store_category_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<StoreOwner>(entity =>
        {
            entity.HasKey(e => e.StoreOwnerId).HasName("store_owner_pkey");

            entity.ToTable("store_owner");

            entity.HasIndex(e => e.UserId, "store_owner_user_id_key").IsUnique();

            entity.Property(e => e.StoreOwnerId).HasColumnName("store_owner_id");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(50)
                .HasColumnName("owner_name");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Store).WithMany(p => p.StoreOwners)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("fk_store_owner_store");

            entity.HasOne(d => d.User).WithOne(p => p.StoreOwner)
                .HasForeignKey<StoreOwner>(d => d.UserId)
                .HasConstraintName("fk_store_owner_user");
        });

        modelBuilder.Entity<StoreWithdrawal>(entity =>
        {
            entity.HasKey(e => e.WithdrawalId).HasName("store_withdrawal_pkey");

            entity.ToTable("store_withdrawal");

            entity.Property(e => e.WithdrawalId).HasColumnName("withdrawal_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Store).WithMany(p => p.StoreWithdrawals)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("fk_store_withdrawal_store");
        });

        modelBuilder.Entity<TopupMember>(entity =>
        {
            entity.HasKey(e => e.TopupId).HasName("topup_member_pkey");

            entity.ToTable("topup_member");

            entity.Property(e => e.TopupId).HasColumnName("topup_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.TopupMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_topup_member_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("USER_pkey");

            entity.ToTable("USER");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.WalletId).HasName("wallet_pkey");

            entity.ToTable("wallet");

            entity.Property(e => e.WalletId).HasColumnName("wallet_id");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
