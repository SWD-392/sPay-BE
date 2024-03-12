using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SPay.BO.DataBase.Models
{
    public partial class SPayDbContext : DbContext
    {
        public SPayDbContext()
        {
        }

        public SPayDbContext(DbContextOptions<SPayDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<CardStoreCategory> CardStoreCategories { get; set; } = null!;
        public virtual DbSet<CardType> CardTypes { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreCategory> StoreCategories { get; set; } = null!;
        public virtual DbSet<StoreWithdrawal> StoreWithdrawals { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;
        public virtual DbSet<WalletType> WalletTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=(local);Database=SPAY-DB-LOCAL;uid=sa;pwd=12345;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminKey)
                    .HasName("PK__ADMIN__9DDF0C206F3C9352");

                entity.ToTable("ADMIN");

                entity.Property(e => e.AdminKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ADMIN_KEY");

                entity.Property(e => e.AdminName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADMIN_NAME");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_AT");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.UserKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ADMIN__USER_KEY__4E88ABD4");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.CardKey)
                    .HasName("PK__CARD__5C3B5FF3F8A6A05C");

                entity.ToTable("CARD");

                entity.Property(e => e.CardKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CARD_KEY");

                entity.Property(e => e.CardName)
                    .HasMaxLength(255)
                    .HasColumnName("CARD_NAME");

                entity.Property(e => e.CardNumber).HasColumnName("CARD_NUMBER");

                entity.Property(e => e.CardTypeKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CARD_TYPE_KEY");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_AT");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.DiscountPercentage).HasColumnName("DISCOUNT_PERCENTAGE");

                entity.Property(e => e.MoneyValue)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("MONEY_VALUE");

                entity.Property(e => e.NumberDate).HasColumnName("NUMBER_DATE");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.CardTypeKeyNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CardTypeKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARD__CARD_TYPE___4F7CD00D");
            });

            modelBuilder.Entity<CardStoreCategory>(entity =>
            {
                entity.HasKey(e => e.CardStoreCategoryKey)
                    .HasName("PK__CARD_STO__66368F5DA1BCFFBB");

                entity.ToTable("CARD_STORE_CATEGORY");

                entity.Property(e => e.CardStoreCategoryKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CARD_STORE_CATEGORY_KEY");

                entity.Property(e => e.CardTypeKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CARD_TYPE_KEY");

                entity.Property(e => e.StoreCategoryKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STORE_CATEGORY_KEY");

                entity.HasOne(d => d.CardTypeKeyNavigation)
                    .WithMany(p => p.CardStoreCategories)
                    .HasForeignKey(d => d.CardTypeKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARD_STOR__CARD___5070F446");

                entity.HasOne(d => d.StoreCategoryKeyNavigation)
                    .WithMany(p => p.CardStoreCategories)
                    .HasForeignKey(d => d.StoreCategoryKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARD_STOR__STORE__5165187F");
            });

            modelBuilder.Entity<CardType>(entity =>
            {
                entity.HasKey(e => e.CardTypeKey)
                    .HasName("PK__CARD_TYP__2F24A9808836878B");

                entity.ToTable("CARD_TYPE");

                entity.Property(e => e.CardTypeKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CARD_TYPE_KEY");

                entity.Property(e => e.CardTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("CARD_TYPE_NAME");

                entity.Property(e => e.TypeDescription)
                    .HasMaxLength(255)
                    .HasColumnName("TYPE_DESCRIPTION");

                entity.Property(e => e.WithdrawalAllowed).HasColumnName("WITHDRAWAL_ALLOWED");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerKey)
                    .HasName("PK__CUSTOMER__B1AE8B82074E4C2A");

                entity.ToTable("CUSTOMER");

                entity.Property(e => e.CustomerKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_KEY");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("CREATE_BY");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CUSTOMER__USER_K__52593CB8");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderKey)
                    .HasName("PK__ORDER__60FED2054EB767CD");

                entity.ToTable("ORDER");

                entity.Property(e => e.OrderKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ORDER_KEY");

                entity.Property(e => e.CardKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CARD_KEY");

                entity.Property(e => e.CustomerKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_KEY");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE");

                entity.Property(e => e.OrderDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ORDER_DESCRIPTION");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.StoreKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STORE_KEY");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VALUE");

                entity.HasOne(d => d.CardKeyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CardKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDER__CARD_KEY__534D60F1");

                entity.HasOne(d => d.CustomerKeyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerKey)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDER__STORE_KEY__5441852A");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreKey)
                    .HasName("PK__STORE__E5FD03F52780481A");

                entity.ToTable("STORE");

                entity.Property(e => e.StoreKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STORE_KEY");

                entity.Property(e => e.CategoryKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name).HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.Property(e => e.WalletKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WALLET_KEY");

                entity.HasOne(d => d.CategoryKeyNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.CategoryKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__STORE__CATEGORY___5629CD9C");

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.UserKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__STORE__USER_KEY__571DF1D5");

                entity.HasOne(d => d.WalletKeyNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.WalletKey)
                    .HasConstraintName("FK_Store_Wallet");
            });

            modelBuilder.Entity<StoreCategory>(entity =>
            {
                entity.HasKey(e => e.StoreCategoryKey)
                    .HasName("PK__STORE_CA__74ED8565C96AACA2");

                entity.ToTable("STORE_CATEGORY");

                entity.Property(e => e.StoreCategoryKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STORE_CATEGORY_KEY");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");

                entity.Property(e => e.Status).HasColumnName("STATUS");
            });

            modelBuilder.Entity<StoreWithdrawal>(entity =>
            {
                entity.HasKey(e => e.StoreWithdrawalKey)
                    .HasName("PK__STORE_WI__EEF95E012DA04C5C");

                entity.ToTable("STORE_WITHDRAWAL");

                entity.Property(e => e.StoreWithdrawalKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STORE_WITHDRAWAL_KEY");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.StoreKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STORE_KEY");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VALUE");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.StoreWithdrawals)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__STORE_WIT__STORE__59063A47");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionKey)
                    .HasName("PK__TRANSACT__B32A2DF409AF7E9B");

                entity.ToTable("TRANSACTION");

                entity.Property(e => e.TransactionKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRANSACTION_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.OrderKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ORDER_KEY");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.StoreWithDrawalkey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STORE_WITH_DRAWALKEY");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VALUE");

                entity.Property(e => e.WalletKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WALLET_KEY");

                entity.HasOne(d => d.OrderKeyNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.OrderKey)
                    .HasConstraintName("FK__TRANSACTI__ORDER__59FA5E80");

                entity.HasOne(d => d.StoreWithDrawalkeyNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.StoreWithDrawalkey)
                    .HasConstraintName("FK__TRANSACTI__STORE__5AEE82B9");

                entity.HasOne(d => d.WalletKeyNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.WalletKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TRANSACTI__WALLE__5BE2A6F2");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserKey)
                    .HasName("PK__USER__5F13FD3C4E17185D");

                entity.ToTable("USER");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Role).HasColumnName("ROLE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.WalletKey)
                    .HasName("PK__WALLET__241A5FED71DEA0B5");

                entity.ToTable("WALLET");

                entity.Property(e => e.WalletKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WALLET_KEY");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.CardKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CARD_KEY");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_AT");

                entity.Property(e => e.CustomerKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_KEY");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.StoreKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STORE_KEY");

                entity.Property(e => e.WalletTypeKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WALLET_TYPE_KEY");

                entity.HasOne(d => d.CardKeyNavigation)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.CardKey)
                    .HasConstraintName("FK_Wallet_Card");

                entity.HasOne(d => d.CustomerKeyNavigation)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.CustomerKey)
                    .HasConstraintName("FK_Wallet_Cus");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.StoreKey)
                    .HasConstraintName("FK_Wallet_Store");

                entity.HasOne(d => d.WalletTypeKeyNavigation)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.WalletTypeKey)
                    .HasConstraintName("FK__WALLET__WALLET_T__5CD6CB2B");
            });

            modelBuilder.Entity<WalletType>(entity =>
            {
                entity.HasKey(e => e.WalletTypeKey)
                    .HasName("PK__WALLET_T__4FE7BFEF12E560BB");

                entity.ToTable("WALLET_TYPE");

                entity.Property(e => e.WalletTypeKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WALLET_TYPE_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Mode).HasColumnName("MODE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("NAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
