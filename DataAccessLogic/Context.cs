using BussinesLogic.Entities;
using BussinesLogic.Enums;
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
        public Context(DbContextOptions<Context> options) : base(options)
        {
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

            modelBuilder.Entity<User>()
                .OwnsMany(u => u.Comments, builder =>
                {
                    builder.WithOwner().HasForeignKey(c => c.UserId);
                    builder.HasKey(c => c.Id);
                });

            modelBuilder.Entity<Ticket>()
                .HasQueryFilter(t => !t.IsDeleted);

            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.Comments)
                .WithOne(c => c.Ticket)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .OwnsMany(t => t.Comments,builder => {
                    builder.WithOwner().HasForeignKey(c => c.TicketId);
                    builder.HasKey(c => c.Id);
                });
            modelBuilder.Entity<User>()
                .HasQueryFilter(u =>u.Status == UserStatus.Active);

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

                var audit = new Audit
                {
                    UserId = 1, // 🔥 después lo conectamos al JWT
                    Date = DateTime.UtcNow,
                    TableName = entry.Metadata.GetTableName(),
                    Action = entry.State.ToString(),
                    RecordId = entry.Properties
                        .FirstOrDefault(p => p.Metadata.IsPrimaryKey())?
                        .CurrentValue?.ToString()
                };

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
