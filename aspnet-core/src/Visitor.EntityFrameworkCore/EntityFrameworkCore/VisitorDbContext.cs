using Visitor.Company;
using Abp.IdentityServer4vNext;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Visitor.Authorization.Delegation;
using Visitor.Authorization.Roles;
using Visitor.Authorization.Users;
using Visitor.Blacklist;
using Visitor.Chat;
using Visitor.Editions;
using Visitor.Friendships;
using Visitor.MultiTenancy;
using Visitor.MultiTenancy.Accounting;
using Visitor.MultiTenancy.Payments;
using Visitor.Storage;


namespace Visitor.EntityFrameworkCore
{
    public class VisitorDbContext : AbpZeroDbContext<Tenant, Role, User, VisitorDbContext>, IAbpPersistedGrantDbContext
    {
        public virtual DbSet<Company.CompanyEnt> Companies { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<Appointment.AppointmentEnt> Appointments { get; set; }
        public virtual DbSet<Title.TitleEnt> VisitorTitles { get; set; }
        public virtual DbSet<Tower.TowerEnt> Towers { get; set; }
        public virtual DbSet<PurposeOfVisit.PurposeOfVisitEnt> PurposeOfVisitAppointments { get; set; }
        public virtual DbSet<Level.LevelEnt> LevelAppointments { get; set; }
        public virtual DbSet<Departments.Department> Departments { get; set; }

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public virtual DbSet<RecentPassword> RecentPasswords { get; set; }
        public virtual DbSet<BlacklistEnt> Blacklist { get; set; }

        public VisitorDbContext(DbContextOptions<VisitorDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique()
                    .HasFilter("[IsDeleted] = 0");
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}