using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

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
        public virtual DbSet<Deposit> Deposits { get; set; } = null!;
        public virtual DbSet<DepositPackage> DepositPackages { get; set; } = null!;
        public virtual DbSet<DepositPackageCardType> DepositPackageCardTypes { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreCategory> StoreCategories { get; set; } = null!;
        public virtual DbSet<StoreOwner> StoreOwners { get; set; } = null!;
        public virtual DbSet<StoreWithdrawal> StoreWithdrawals { get; set; } = null!;
        public virtual DbSet<TopupMember> TopupMembers { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;
        public virtual DbSet<WalletType> WalletTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminKey)
                    .HasName("PK__ADMIN__9DDF0C2040D5A9CF");

                entity.ToTable("ADMIN");

                entity.Property(e => e.AdminKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ADMIN_KEY");

                entity.Property(e => e.AdminName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADMIN_NAME");

                entity.Property(e => e.CreateAt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CREATE_AT");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.UserKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ADMIN__USER_KEY__4A8310C6");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.CardKey)
                    .HasName("PK__CARD__5C3B5FF37A3790BD");

                entity.ToTable("CARD");

                entity.Property(e => e.CardKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CARD_KEY");

                entity.Property(e => e.CardNumber).HasColumnName("CARD_NUMBER");

                entity.Property(e => e.CardTypeKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CARD_TYPE_KEY");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_AT");

                entity.Property(e => e.CustomerKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_KEY");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("EXPIRY_DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.CardTypeKeyNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CardTypeKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARD__CARD_TYPE___4C6B5938");

                entity.HasOne(d => d.CustomerKeyNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CustomerKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARD__CUSTOMER_K__4B7734FF");
            });

            modelBuilder.Entity<CardStoreCategory>(entity =>
            {
                entity.HasKey(e => e.CardStoreCategoryKey)
                    .HasName("PK__CARD_STO__66368F5D4408DC3B");

                entity.ToTable("CARD_STORE_CATEGORY");

                entity.Property(e => e.CardStoreCategoryKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CARD_STORE_CATEGORY_KEY");

                entity.Property(e => e.CardTypeKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CARD_TYPE_KEY");

                entity.Property(e => e.StoreCategoryKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STORE_CATEGORY_KEY");

                entity.HasOne(d => d.CardTypeKeyNavigation)
                    .WithMany(p => p.CardStoreCategories)
                    .HasForeignKey(d => d.CardTypeKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARD_STOR__CARD___4D5F7D71");

                entity.HasOne(d => d.StoreCategoryKeyNavigation)
                    .WithMany(p => p.CardStoreCategories)
                    .HasForeignKey(d => d.StoreCategoryKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARD_STOR__STORE__4E53A1AA");
            });

            modelBuilder.Entity<CardType>(entity =>
            {
                entity.HasKey(e => e.CardTypeKey)
                    .HasName("PK__CARD_TYP__2F24A9800CF06653");

                entity.ToTable("CARD_TYPE");

                entity.Property(e => e.CardTypeKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CARD_TYPE_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerKey)
                    .HasName("PK__CUSTOMER__B1AE8B824A687282");

                entity.ToTable("CUSTOMER");

                entity.Property(e => e.CustomerKey)
                    .HasMaxLength(10)
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

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CUSTOMER__USER_K__55009F39");
            });

            modelBuilder.Entity<Deposit>(entity =>
            {
                entity.HasKey(e => e.DepositKey)
                    .HasName("PK__DEPOSIT__40905B6DB6DD930F");

                entity.ToTable("DEPOSIT");

                entity.Property(e => e.DepositKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DEPOSIT_KEY");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.CardKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CARD_KEY");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.DepositPackageKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DEPOSIT_PACKAGE_KEY");

                entity.HasOne(d => d.CardKeyNavigation)
                    .WithMany(p => p.Deposits)
                    .HasForeignKey(d => d.CardKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DEPOSIT__CARD_KE__55F4C372");

                entity.HasOne(d => d.DepositPackageKeyNavigation)
                    .WithMany(p => p.Deposits)
                    .HasForeignKey(d => d.DepositPackageKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DEPOSIT__DEPOSIT__56E8E7AB");
            });

            modelBuilder.Entity<DepositPackage>(entity =>
            {
                entity.HasKey(e => e.DepositPackageKey)
                    .HasName("PK__DEPOSIT___0C2E9BDF9934F157");

                entity.ToTable("DEPOSIT_PACKAGE");

                entity.Property(e => e.DepositPackageKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DEPOSIT_PACKAGE_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price).HasColumnName("PRICE");
            });

            modelBuilder.Entity<DepositPackageCardType>(entity =>
            {
                entity.HasKey(e => e.DepositPackageCardTypeKey)
                    .HasName("PK__DEPOSIT___1C3D25DCB135C119");

                entity.ToTable("DEPOSIT_PACKAGE_CARD_TYPE");

                entity.Property(e => e.DepositPackageCardTypeKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DEPOSIT_PACKAGE_CARD_TYPE_KEY");

                entity.Property(e => e.CardTypeKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CARD_TYPE_KEY");

                entity.Property(e => e.DepositPackageKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DEPOSIT_PACKAGE_KEY");

                entity.HasOne(d => d.CardTypeKeyNavigation)
                    .WithMany(p => p.DepositPackageCardTypes)
                    .HasForeignKey(d => d.CardTypeKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DEPOSIT_P__CARD___58D1301D");

                entity.HasOne(d => d.DepositPackageKeyNavigation)
                    .WithMany(p => p.DepositPackageCardTypes)
                    .HasForeignKey(d => d.DepositPackageKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DEPOSIT_P__DEPOS__57DD0BE4");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderKey)
                    .HasName("PK__ORDER__60FED205DD8FC180");

                entity.ToTable("ORDER");

                entity.Property(e => e.OrderKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ORDER_KEY");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.CardKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CARD_KEY");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.StoreKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STORE_KEY");

                entity.HasOne(d => d.CardKeyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CardKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDER__CARD_KEY__531856C7");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDER__STORE_KEY__5224328E");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreKey)
                    .HasName("PK__STORE__E5FD03F5F0C26C66");

                entity.ToTable("STORE");

                entity.Property(e => e.StoreKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STORE_KEY");

                entity.Property(e => e.CategoryKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_KEY");

                entity.Property(e => e.Location).HasColumnName("LOCATION");

                entity.Property(e => e.Name).HasColumnName("NAME");

                entity.Property(e => e.Phone).HasColumnName("PHONE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.CategoryKeyNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.CategoryKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__STORE__CATEGORY___4F47C5E3");
            });

            modelBuilder.Entity<StoreCategory>(entity =>
            {
                entity.HasKey(e => e.StoreCategoryKey)
                    .HasName("PK__STORE_CA__74ED856573A48E67");

                entity.ToTable("STORE_CATEGORY");

                entity.Property(e => e.StoreCategoryKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STORE_CATEGORY_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");

                entity.Property(e => e.Status).HasColumnName("STATUS");
            });

            modelBuilder.Entity<StoreOwner>(entity =>
            {
                entity.HasKey(e => e.StoreOwnerKey)
                    .HasName("PK__STORE_OW__499922784D43AA62");

                entity.ToTable("STORE_OWNER");

                entity.Property(e => e.StoreOwnerKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STORE_OWNER_KEY");

                entity.Property(e => e.CreateAt)
                    .HasMaxLength(100)
                    .HasColumnName("CREATE_AT");

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OWNER_NAME");

                entity.Property(e => e.StoreKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STORE_KEY");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.StoreOwners)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__STORE_OWN__STORE__51300E55");

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.StoreOwners)
                    .HasForeignKey(d => d.UserKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__STORE_OWN__USER___503BEA1C");
            });

            modelBuilder.Entity<StoreWithdrawal>(entity =>
            {
                entity.HasKey(e => e.StoreWithdrawalKey)
                    .HasName("PK__STORE_WI__EEF95E014EDB4CE7");

                entity.ToTable("STORE_WITHDRAWAL");

                entity.Property(e => e.StoreWithdrawalKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STORE_WITHDRAWAL_KEY");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.StoreKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STORE_KEY");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.StoreWithdrawals)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__STORE_WIT__STORE__540C7B00");
            });

            modelBuilder.Entity<TopupMember>(entity =>
            {
                entity.HasKey(e => e.TopupMemberKey)
                    .HasName("PK__TOPUP_ME__B365D5B29EB94D74");

                entity.ToTable("TOPUP_MEMBER");

                entity.HasIndex(e => e.UserKey, "UQ_USER_KEY")
                    .IsUnique();

                entity.Property(e => e.TopupMemberKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TOPUP_MEMBER_KEY");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionKey)
                    .HasName("PK__TRANSACT__B32A2DF4B90513E5");

                entity.ToTable("TRANSACTION");

                entity.Property(e => e.TransactionKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TRANSACTION_KEY");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.DepositKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DEPOSIT_KEY");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.OrderKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ORDER_KEY");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.StoreWithDrawalkey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STORE_WITH_DRAWALKEY");

                entity.Property(e => e.WalletKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("WALLET_KEY");

                entity.HasOne(d => d.DepositKeyNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.DepositKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TRANSACTI__DEPOS__5AB9788F");

                entity.HasOne(d => d.OrderKeyNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.OrderKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TRANSACTI__ORDER__5BAD9CC8");

                entity.HasOne(d => d.StoreWithDrawalkeyNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.StoreWithDrawalkey)
                    .HasConstraintName("FK__TRANSACTI__STORE__5CA1C101");

                entity.HasOne(d => d.WalletKeyNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.WalletKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TRANSACTI__WALLE__59C55456");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserKey)
                    .HasName("PK__USER__5F13FD3C82BC91FD");

                entity.ToTable("USER");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

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
                    .HasName("PK__WALLET__241A5FED528ACB07");

                entity.ToTable("WALLET");

                entity.Property(e => e.WalletKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("WALLET_KEY");

                entity.Property(e => e.Balance).HasColumnName("BALANCE");

                entity.Property(e => e.CardKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CARD_KEY");

                entity.Property(e => e.CreateAt)
                    .HasMaxLength(100)
                    .HasColumnName("CREATE_AT");

                entity.Property(e => e.StoreKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STORE_KEY");

                entity.Property(e => e.WalletTypeKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("WALLET_Type_KEY");

                entity.HasOne(d => d.CardKeyNavigation)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.CardKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WALLET__CARD_KEY__5E8A0973");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WALLET__STORE_KE__5F7E2DAC");

                entity.HasOne(d => d.WalletTypeKeyNavigation)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.WalletTypeKey)
                    .HasConstraintName("FK__WALLET__WALLET_T__5D95E53A");
            });

            modelBuilder.Entity<WalletType>(entity =>
            {
                entity.HasKey(e => e.WalletTypeKey)
                    .HasName("PK__WALLET_T__4FE7BFEFA0570584");

                entity.ToTable("WALLET_TYPE");

                entity.Property(e => e.WalletTypeKey)
                    .HasMaxLength(10)
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

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config["ConnectionStrings:DefaultConnectionString"];
            return strConn;
        }
    }
}
