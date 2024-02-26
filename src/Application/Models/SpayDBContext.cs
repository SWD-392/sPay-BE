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

    public virtual DbSet<Deposit> Deposits { get; set; }

    public virtual DbSet<DepositPackage> DepositPackages { get; set; }

    public virtual DbSet<DepositPackageCardType> DepositPackageCardTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreCategory> StoreCategories { get; set; }

    public virtual DbSet<StoreOwner> StoreOwners { get; set; }

    public virtual DbSet<StoreWithdrawal> StoreWithdrawals { get; set; }

    public virtual DbSet<TopupMember> TopupMembers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    public virtual DbSet<WalletType> WalletTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=swd-spay-db-fptu.a.aivencloud.com;Port=20880;Database=db.2024.01.27;User Id=avnadmin;Password=AVNS_JfKQ8uqUwAV05y2rixU;SSL Mode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminKey).HasName("admin_pkey");

            entity.ToTable("admin");

            entity.Property(e => e.AdminKey).HasColumnName("admin_key");
            entity.Property(e => e.AdminName)
                .HasMaxLength(50)
                .HasColumnName("admin_name");
            entity.Property(e => e.CreateAt)
                .HasMaxLength(50)
                .HasColumnName("create_at");
            entity.Property(e => e.UserKey).HasColumnName("user_key");

            entity.HasOne(d => d.UserKeyNavigation).WithMany(p => p.Admins)
                .HasForeignKey(d => d.UserKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("admin_user_key_fkey");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardKey).HasName("card_pkey");

            entity.ToTable("card");

            entity.Property(e => e.CardKey).HasColumnName("card_key");
            entity.Property(e => e.CardNumber).HasColumnName("card_number");
            entity.Property(e => e.CardTypeKey).HasColumnName("card_type_key");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerKey).HasColumnName("customer_key");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiry_date");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.CardTypeKeyNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.CardTypeKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("card_card_type_key_fkey");

            entity.HasOne(d => d.CustomerKeyNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.CustomerKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("card_customer_key_fkey");
        });

        modelBuilder.Entity<CardStoreCategory>(entity =>
        {
            entity.HasKey(e => e.CardStoreCategoryKey).HasName("card_store_category_pkey");

            entity.ToTable("card_store_category");

            entity.Property(e => e.CardStoreCategoryKey).HasColumnName("card_store_category_key");
            entity.Property(e => e.CardTypeKey).HasColumnName("card_type_key");
            entity.Property(e => e.StoreCategoryKey).HasColumnName("store_category_key");

            entity.HasOne(d => d.CardTypeKeyNavigation).WithMany(p => p.CardStoreCategories)
                .HasForeignKey(d => d.CardTypeKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("card_store_category_card_type_key_fkey");

            entity.HasOne(d => d.StoreCategoryKeyNavigation).WithMany(p => p.CardStoreCategories)
                .HasForeignKey(d => d.StoreCategoryKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("card_store_category_store_category_key_fkey");
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.HasKey(e => e.CardTypeKey).HasName("card_type_pkey");

            entity.ToTable("card_type");

            entity.Property(e => e.CardTypeKey).HasColumnName("card_type_key");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerKey).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.HasIndex(e => e.UserKey, "customer_user_key_key").IsUnique();

            entity.Property(e => e.CustomerKey).HasColumnName("customer_key");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.UserKey).HasColumnName("user_key");

            entity.HasOne(d => d.UserKeyNavigation).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.UserKey)
                .HasConstraintName("customer_user_key_fkey");
        });

        modelBuilder.Entity<Deposit>(entity =>
        {
            entity.HasKey(e => e.DepositKey).HasName("deposit_pkey");

            entity.ToTable("deposit");

            entity.Property(e => e.DepositKey).HasColumnName("deposit_key");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CardKey).HasColumnName("card_key");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DepositPackageKey).HasColumnName("deposit_package_key");

            entity.HasOne(d => d.CardKeyNavigation).WithMany(p => p.Deposits)
                .HasForeignKey(d => d.CardKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deposit_card_key_fkey");

            entity.HasOne(d => d.DepositPackageKeyNavigation).WithMany(p => p.Deposits)
                .HasForeignKey(d => d.DepositPackageKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deposit_deposit_package_key_fkey");
        });

        modelBuilder.Entity<DepositPackage>(entity =>
        {
            entity.HasKey(e => e.DepositPackageKey).HasName("deposit_package_pkey");

            entity.ToTable("deposit_package");

            entity.Property(e => e.DepositPackageKey).HasColumnName("deposit_package_key");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<DepositPackageCardType>(entity =>
        {
            entity.HasKey(e => e.DepositPackageCardTypeKey).HasName("deposit_package_card_type_pkey");

            entity.ToTable("deposit_package_card_type");

            entity.Property(e => e.DepositPackageCardTypeKey).HasColumnName("deposit_package_card_type_key");
            entity.Property(e => e.CardTypeKey).HasColumnName("card_type_key");
            entity.Property(e => e.DepositPackageKey).HasColumnName("deposit_package_key");

            entity.HasOne(d => d.CardTypeKeyNavigation).WithMany(p => p.DepositPackageCardTypes)
                .HasForeignKey(d => d.CardTypeKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deposit_package_card_type_card_type_key_fkey");

            entity.HasOne(d => d.DepositPackageKeyNavigation).WithMany(p => p.DepositPackageCardTypes)
                .HasForeignKey(d => d.DepositPackageKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deposit_package_card_type_deposit_package_key_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderKey).HasName("ORDER_pkey");

            entity.ToTable("ORDER");

            entity.Property(e => e.OrderKey).HasColumnName("order_key");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CardKey).HasColumnName("card_key");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StoreKey).HasColumnName("store_key");

            entity.HasOne(d => d.CardKeyNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CardKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDER_card_key_fkey");

            entity.HasOne(d => d.StoreKeyNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDER_store_key_fkey");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreKey).HasName("store_pkey");

            entity.ToTable("store");

            entity.Property(e => e.StoreKey).HasColumnName("store_key");
            entity.Property(e => e.CategoryKey).HasColumnName("category_key");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.CategoryKeyNavigation).WithMany(p => p.Stores)
                .HasForeignKey(d => d.CategoryKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("store_category_key_fkey");
        });

        modelBuilder.Entity<StoreCategory>(entity =>
        {
            entity.HasKey(e => e.StoreCategoryKey).HasName("store_category_pkey");

            entity.ToTable("store_category");

            entity.Property(e => e.StoreCategoryKey).HasColumnName("store_category_key");
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
            entity.HasKey(e => e.StoreOwnerKey).HasName("store_owner_pkey");

            entity.ToTable("store_owner");

            entity.Property(e => e.StoreOwnerKey).HasColumnName("store_owner_key");
            entity.Property(e => e.CreateAt)
                .HasMaxLength(100)
                .HasColumnName("create_at");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(50)
                .HasColumnName("owner_name");
            entity.Property(e => e.StoreKey).HasColumnName("store_key");
            entity.Property(e => e.UserKey).HasColumnName("user_key");

            entity.HasOne(d => d.StoreKeyNavigation).WithMany(p => p.StoreOwners)
                .HasForeignKey(d => d.StoreKey)
                .HasConstraintName("store_owner_store_key_fkey");

            entity.HasOne(d => d.UserKeyNavigation).WithMany(p => p.StoreOwners)
                .HasForeignKey(d => d.UserKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("store_owner_user_key_fkey");
        });

        modelBuilder.Entity<StoreWithdrawal>(entity =>
        {
            entity.HasKey(e => e.StoreWithdrawalKey).HasName("store_withdrawal_pkey");

            entity.ToTable("store_withdrawal");

            entity.Property(e => e.StoreWithdrawalKey).HasColumnName("store_withdrawal_key");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StoreKey).HasColumnName("store_key");

            entity.HasOne(d => d.StoreKeyNavigation).WithMany(p => p.StoreWithdrawals)
                .HasForeignKey(d => d.StoreKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("store_withdrawal_store_key_fkey");
        });

        modelBuilder.Entity<TopupMember>(entity =>
        {
            entity.HasKey(e => e.TopupMemberKey).HasName("topup_member_pkey");

            entity.ToTable("topup_member");

            entity.HasIndex(e => e.UserKey, "topup_member_user_key_key").IsUnique();

            entity.Property(e => e.TopupMemberKey).HasColumnName("topup_member_key");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserKey).HasColumnName("user_key");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionKey).HasName("transaction_pkey");

            entity.ToTable("transaction");

            entity.Property(e => e.TransactionKey).HasColumnName("transaction_key");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.DepositKey).HasColumnName("deposit_key");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.OrderKey).HasColumnName("order_key");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StoreWithDrawalkey).HasColumnName("store_with_drawalkey");
            entity.Property(e => e.WalletKey).HasColumnName("wallet_key");

            entity.HasOne(d => d.DepositKeyNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.DepositKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transaction_deposit_key_fkey");

            entity.HasOne(d => d.OrderKeyNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.OrderKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transaction_order_key_fkey");

            entity.HasOne(d => d.StoreWithDrawalkeyNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.StoreWithDrawalkey)
                .HasConstraintName("transaction_store_with_drawalkey_fkey");

            entity.HasOne(d => d.WalletKeyNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.WalletKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transaction_wallet_key_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserKey).HasName("USER_pkey");

            entity.ToTable("USER");

            entity.Property(e => e.UserKey).HasColumnName("user_key");
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
            entity.HasKey(e => e.WalletKey).HasName("wallet_pkey");

            entity.ToTable("wallet");

            entity.Property(e => e.WalletKey).HasColumnName("wallet_key");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.CardKey).HasColumnName("card_key");
            entity.Property(e => e.CreateAt)
                .HasMaxLength(100)
                .HasColumnName("create_at");
            entity.Property(e => e.StoreKey).HasColumnName("store_key");
            entity.Property(e => e.WalletTypeKey).HasColumnName("wallet_type_key");

            entity.HasOne(d => d.CardKeyNavigation).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.CardKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("wallet_card_key_fkey");

            entity.HasOne(d => d.StoreKeyNavigation).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.StoreKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("wallet_store_key_fkey");

            entity.HasOne(d => d.WalletTypeKeyNavigation).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.WalletTypeKey)
                .HasConstraintName("wallet_wallet_type_key_fkey");
        });

        modelBuilder.Entity<WalletType>(entity =>
        {
            entity.HasKey(e => e.WalletTypeKey).HasName("wallet_type_pkey");

            entity.ToTable("wallet_type");

            entity.Property(e => e.WalletTypeKey).HasColumnName("wallet_type_key");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Mode).HasColumnName("mode");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
