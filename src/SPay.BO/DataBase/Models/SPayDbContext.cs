using System;
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
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=SPAY_FINAL_DB;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.CardKey)
                    .HasName("PK__CARDS__5C3B5FF39A5B1BA0");

                entity.ToTable("CARDS");

                entity.Property(e => e.CardKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CARD_KEY");

                entity.Property(e => e.CardName)
                    .HasColumnType("datetime")
                    .HasColumnName("CARD_NAME");

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

                entity.Property(e => e.WithdrawAllowed).HasColumnName("WITHDRAW_ALLOWED");

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
                    .HasName("PK__CARD_TYP__2F24A9803E6D39DB");

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
                    .HasName("PK__CARD_TYP__B0478CED3CADCBFE");

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
                entity.HasKey(e => new { e.UserKey, e.CardKey })
                    .HasName("PK__MEMBERSH__7AD048C345A05BB0");

                entity.ToTable("MEMBERSHIPS");

                entity.HasIndex(e => e.MembershipKey, "UQ__MEMBERSH__9FFED3AFD38D59EB")
                    .IsUnique();

                entity.Property(e => e.UserKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.Property(e => e.CardKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CARD_KEY");

                entity.Property(e => e.MembershipKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("MEMBERSHIP_KEY");
            });

            modelBuilder.Entity<MembershipsWallet>(entity =>
            {
                entity.HasKey(e => new { e.MembershipKey, e.WalletKey })
                    .HasName("PK__MEMBERSH__5DBF7650A5035E38");

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
                    .HasName("PK__ORDERS__60FED205E865A10D");

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
                    .HasName("PK__PROMOTIO__907CFB85AA557F0C");

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

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.ValueUsed)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VALUE_USED");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleKey)
                    .HasName("PK__ROLES__C88CECBB8C1BE6D5");

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
                    .HasName("PK__STORES__E5FD03F5B09EE9AB");

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
                    .HasName("PK__STORE_CA__74ED8565FAC7EEA9");

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
                entity.HasNoKey();

                entity.ToTable("TRANSACTIONS");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.OrderKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ORDER_KEY");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.TransactionKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("TRANSACTION_KEY");

                entity.Property(e => e.WithdrawKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("WITHDRAW_KEY");

                entity.HasOne(d => d.OrderKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.OrderKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TRANSACTION_ORDER");

                entity.HasOne(d => d.WithdrawKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.WithdrawKey)
                    .HasConstraintName("FK_TRANSACTION_WITHDRAW");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserKey)
                    .HasName("PK__USERS__5F13FD3C540DE867");

                entity.ToTable("USERS");

                entity.HasIndex(e => e.PhoneNumber, "UQ__USERS__D94A4FFBF45266B7")
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
                    .IsUnicode(false)
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
                    .HasName("PK__WALLETS__241A5FEDE7A531F6");

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
                    .HasName("PK__WITHDRAW__F02EFFAA7ED01FD1");

                entity.ToTable("WITHDRAW_INFORMATIONS");

                entity.Property(e => e.WithdrawKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("WITHDRAW_KEY");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INS_DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UserKey)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USER_KEY");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VALUE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
