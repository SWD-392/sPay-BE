﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SPay.BO.DataBase.Models
{
    public partial class SpayDBContext : DbContext
    {
        public SpayDBContext()
        {
        }

        public SpayDBContext(DbContextOptions<SpayDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<CardType> CardTypes { get; set; } = null!;
        public virtual DbSet<CardTypesStoreCategory> CardTypesStoreCategories { get; set; } = null!;
        public virtual DbSet<Membership> Memberships { get; set; } = null!;
        public virtual DbSet<MembershipsWallet> MembershipsWallets { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<PromotionPackage> PromotionPackages { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreCategory> StoreCategories { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;
        public virtual DbSet<WithdrawInformation> WithdrawInformations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=swd-391.database.windows.net;Database=SPay-DB-2024-03-22;uid=swp;pwd=Spay@123!;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.CardKey)
                    .HasName("PK__CARDS__5C3B5FF3A8E57B7A");

                entity.ToTable("CARDS");

                entity.HasIndex(e => e.CardNo, "UQ__CARDS__CF0CC5CDCC493978")
                    .IsUnique();

                entity.Property(e => e.CardKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CARD_KEY");

                entity.Property(e => e.CardName)
                    .HasMaxLength(50)
                    .HasColumnName("CARD_NAME");

                entity.Property(e => e.CardNo)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("CARD_NO");

                entity.Property(e => e.CardTypeKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CARD_TYPE_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.PromotionPackageKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PROMOTION_PACKAGE_KEY");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.CardTypeKeyNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CardTypeKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CARD_CARD_TYPE");

                entity.HasOne(d => d.PromotionPackageKeyNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.PromotionPackageKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CARD_PROMOTION_PACKAGE");
            });

            modelBuilder.Entity<CardType>(entity =>
            {
                entity.HasKey(e => e.CardTypeKey)
                    .HasName("PK__CARD_TYP__2F24A9801F82B5D0");

                entity.ToTable("CARD_TYPES");

                entity.Property(e => e.CardTypeKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CARD_TYPE_KEY");

                entity.Property(e => e.CardTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("CARD_TYPE_NAME");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");
            });

            modelBuilder.Entity<CardTypesStoreCategory>(entity =>
            {
                entity.HasKey(e => new { e.CardTypeKey, e.StoreCateKey })
                    .HasName("PK__CARD_TYP__B0478CED5EB64DBD");

                entity.ToTable("CARD_TYPES_STORE_CATEGORIES");

                entity.Property(e => e.CardTypeKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CARD_TYPE_KEY");

                entity.Property(e => e.StoreCateKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("STORE_CATE_KEY");
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasKey(e => new { e.MembershipKey, e.UserKey })
                    .HasName("PK__MEMBERSH__4A0FEC7DC176291E");

                entity.ToTable("MEMBERSHIPS");

                entity.HasIndex(e => e.MembershipKey, "UQ__MEMBERSH__9FFED3AF6E4123F8")
                    .IsUnique();

                entity.Property(e => e.MembershipKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("MEMBERSHIP_KEY");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.Property(e => e.CardKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CARD_KEY");

                entity.Property(e => e.ExpiritionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("EXPIRITION_DATE");

                entity.Property(e => e.IsDefaultMembership).HasColumnName("IS_DEFAULT_MEMBERSHIP");
            });

            modelBuilder.Entity<MembershipsWallet>(entity =>
            {
                entity.HasKey(e => new { e.MembershipKey, e.WalletKey })
                    .HasName("PK__MEMBERSH__5DBF7650851CA8B8");

                entity.ToTable("MEMBERSHIPS_WALLETS");

                entity.Property(e => e.MembershipKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("MEMBERSHIP_KEY");

                entity.Property(e => e.WalletKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("WALLET_KEY");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderKey)
                    .HasName("PK__ORDERS__60FED205D1225809");

                entity.ToTable("ORDERS");

                entity.Property(e => e.OrderKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ORDER_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.MembershipKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("MEMBERSHIP_KEY");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.StoreKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("STORE_KEY");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("TOTAL_AMOUNT");

                entity.HasOne(d => d.MembershipKeyNavigation)
                    .WithMany(p => p.Orders)
                    .HasPrincipalKey(p => p.MembershipKey)
                    .HasForeignKey(d => d.MembershipKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_MEMBERSHIP");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_STORE");
            });

            modelBuilder.Entity<PromotionPackage>(entity =>
            {
                entity.HasKey(e => e.PromotionPackageKey)
                    .HasName("PK__PROMOTIO__907CFB856026B016");

                entity.ToTable("PROMOTION_PACKAGES");

                entity.Property(e => e.PromotionPackageKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PROMOTION_PACKAGE_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.DiscountPercentage).HasColumnName("DISCOUNT_PERCENTAGE");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.NumberDate).HasColumnName("NUMBER_DATE");

                entity.Property(e => e.PackageName)
                    .HasMaxLength(50)
                    .HasColumnName("PACKAGE_NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UsaebleAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("USAEBLE_AMOUNT");

                entity.Property(e => e.WithdrawAllowed).HasColumnName("WITHDRAW_ALLOWED");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleKey)
                    .HasName("PK__ROLES__C88CECBB41F268E7");

                entity.ToTable("ROLES");

                entity.Property(e => e.RoleKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("ROLE_NAME");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreKey)
                    .HasName("PK__STORES__E5FD03F532FB9D38");

                entity.ToTable("STORES");

                entity.Property(e => e.StoreKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("STORE_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.StoreCateKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("STORE_CATE_KEY");

                entity.Property(e => e.StoreName)
                    .HasMaxLength(50)
                    .HasColumnName("STORE_NAME");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.Property(e => e.WalletKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("WALLET_KEY");

                entity.HasOne(d => d.StoreCateKeyNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.StoreCateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STORE_CATE");

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.UserKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STORE_USER");

                entity.HasOne(d => d.WalletKeyNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.WalletKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STORE_WALLET");
            });

            modelBuilder.Entity<StoreCategory>(entity =>
            {
                entity.HasKey(e => e.StoreCategoryKey)
                    .HasName("PK__STORE_CA__74ED8565C0A1DE9D");

                entity.ToTable("STORE_CATEGORIES");

                entity.Property(e => e.StoreCategoryKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("STORE_CATEGORY_KEY");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("CATEGORY_NAME");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionKey)
                    .HasName("PK__TRANSACT__B32A2DF422D12B93");

                entity.ToTable("TRANSACTIONS");

                entity.Property(e => e.TransactionKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("TRANSACTION_KEY");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.OrderKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ORDER_KEY");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.WithdrawKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("WITHDRAW_KEY");

                entity.HasOne(d => d.OrderKeyNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.OrderKey)
                    .HasConstraintName("FK_TRANSACTION_ORDER");

                entity.HasOne(d => d.WithdrawKeyNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.WithdrawKey)
                    .HasConstraintName("FK_TRANSACTION_WITHDRAW");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserKey)
                    .HasName("PK__USERS__5F13FD3C5A770B3D");

                entity.ToTable("USERS");

                entity.HasIndex(e => e.PhoneNumber, "UQ__USERS__D94A4FFB39A316D9")
                    .IsUnique();

                entity.Property(e => e.UserKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.Password)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PHONE_NUMBER");

                entity.Property(e => e.RoleKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_KEY");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.ZaloId)
                    .HasMaxLength(40)
                    .HasColumnName("ZALO_ID");

                entity.HasOne(d => d.RoleKeyNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_ROLE");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.WalletKey)
                    .HasName("PK__WALLETS__241A5FED37759C63");

                entity.ToTable("WALLETS");

                entity.Property(e => e.WalletKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("WALLET_KEY");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");
            });

            modelBuilder.Entity<WithdrawInformation>(entity =>
            {
                entity.HasKey(e => e.WithdrawKey)
                    .HasName("PK__WITHDRAW__F02EFFAAABF5AD71");

                entity.ToTable("WITHDRAW_INFORMATIONS");

                entity.Property(e => e.WithdrawKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("WITHDRAW_KEY");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("TOTAL_AMOUNT");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.WithdrawInformations)
                    .HasForeignKey(d => d.UserKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WITHDRAW_USER");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
