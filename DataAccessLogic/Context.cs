using BussinesLogic.Entities;
using BussinesLogic.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DataAccessLogic
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<Audit> Audits {  get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions {  get; set; }
        public DbSet<TicketHistory> TicketHistories { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Context(DbContextOptions<Context> options,IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedTickets)
                .WithOne(t => t.CreatorUser)
                .HasForeignKey(t => t.CreatorUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.AssignedTickets)
                .WithOne(t => t.AssignedUser)
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(u => u.Id);

                builder.HasMany(u => u.Comments)
                       .WithOne(c => c.User)
                       .HasForeignKey(c => c.UserId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.Navigation(u => u.Comments)
                       .UsePropertyAccessMode(PropertyAccessMode.Field);
            });
            modelBuilder.Entity<UserComment>(builder =>
            {
                builder.HasKey(c => c.Id);

                builder.Property(c => c.Content)
                       .IsRequired()
                       .HasMaxLength(500);
            });

            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.Id);

                user.HasOne(u => u.Role)
                    .WithMany() 
                    .IsRequired();
            });
            modelBuilder.Entity<User>()
                .HasQueryFilter(u =>u.Status == UserStatus.Active);


            modelBuilder.Entity<User>(builder =>
            {
                builder.Navigation(r => r.UserPermissions)
                       .UsePropertyAccessMode(PropertyAccessMode.Field);
            });
            modelBuilder.Entity<UserPermission>(up =>
            {
                up.HasKey(x => new { x.UserId, x.PermissionId });

                up.HasOne(x => x.User)
                  .WithMany(u => u.UserPermissions)
                  .HasForeignKey(x => x.UserId)
                  .IsRequired();

                up.HasOne(x => x.Permission)
                  .WithMany()
                  .HasForeignKey(x => x.PermissionId)
                  .IsRequired();
            });

            modelBuilder.Entity<Ticket>()
                .HasQueryFilter(t => !t.IsDeleted);


            modelBuilder.Entity<Ticket>(builder =>
            {
                builder.HasKey(t => t.Id);

                builder.HasMany(t => t.Comments)
                       .WithOne(c => c.Ticket)
                       .HasForeignKey(c => c.TicketId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.Navigation(t => t.Comments)
                       .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            modelBuilder.Entity<TicketComment>(builder =>
            {
                builder.HasKey(c => c.Id);

                builder.Property(c => c.Content)
                       .IsRequired()
                       .HasMaxLength(1000);
             
            });

            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.History)
                .WithOne(h => h.Ticket)
                .HasForeignKey(h => h.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>(e =>
            {
                e.Property(t => t.RowVersion)
                .IsRowVersion();
            });

            modelBuilder.Entity<Role>(builder =>
            {
                builder.Navigation(r => r.RolPermissions)
                       .UsePropertyAccessMode(PropertyAccessMode.Field);
            });
            modelBuilder.Entity<RolePermission>(rp =>
            {
                rp.HasKey(x => new { x.RoleId, x.PermissionId });

                rp.HasOne(x => x.Role)
                  .WithMany(r => r.RolPermissions)
                  .HasForeignKey(x => x.RoleId)
                  .IsRequired();

                rp.HasOne(x => x.Permission)
                  .WithMany()
                  .HasForeignKey(x => x.PermissionId)
                  .IsRequired();
            });

            modelBuilder.Entity<TicketAttachment>()
                  .HasOne(x => x.Ticket)
                  .WithMany(x => x.Attachments)
                  .HasForeignKey(x => x.TicketId);
        }


        public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
        {
            var auditEntries = new List<Audit>();

            var entries = ChangeTracker.Entries()
                .Where(e =>
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified ||
                    e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in entries)
            {
                if (entry.Entity is Audit)
                    continue;
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;
                var userId = userIdClaim != null ? int.Parse(userIdClaim) : 0; // 0 si no hay JWT


                var audit = new Audit
                (
                    DateTime.UtcNow,
                    userId,
                    entry.State.ToString(),
                    entry.Metadata.GetTableName()                   
                );
                audit.RecordId = entry.Properties
                       .FirstOrDefault(p => p.Metadata.IsPrimaryKey())?
                       .CurrentValue?.ToString();

                var oldValues = new Dictionary<string, object>();
                var newValues = new Dictionary<string, object>();
                var changedColumns = new List<string>();

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                        continue;

                    if (entry.State == EntityState.Added)
                    {
                        newValues[propertyName] = property.CurrentValue;
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        oldValues[propertyName] = property.OriginalValue;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        if (property.IsModified)
                        {
                            changedColumns.Add(propertyName);
                            oldValues[propertyName] = property.OriginalValue;
                            newValues[propertyName] = property.CurrentValue;
                        }
                    }
                    if (entry.Entity is TicketSoftDelete softDeleteEntity && softDeleteEntity.IsDeleted)
                    {
                        audit.Action = "SoftDeleted";
                    }
                }

                audit.OldValues = oldValues.Any()
                    ? JsonSerializer.Serialize(oldValues)
                    : null;

                audit.NewValues = newValues.Any()
                    ? JsonSerializer.Serialize(newValues)
                    : null;

                audit.ChangedColumns = changedColumns.Any()
                    ? string.Join(", ", changedColumns)
                    : null;

                auditEntries.Add(audit);
            }

            if (auditEntries.Any())
                Audits.AddRange(auditEntries);

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
