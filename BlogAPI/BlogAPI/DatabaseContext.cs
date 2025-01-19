using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI {
    public class DatabaseContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }


        public override int SaveChanges() {
            // Get all the entities that inherit from AuditableEntity
            // and have a state of Added or Modified
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            // For each entity we will set the Audit properties
            foreach (var entityEntry in entries) {
                // If the entity state is Added let's set
                // the CreatedAt and CreatedBy properties
                if (entityEntry.State == EntityState.Added) {
                    ((AuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
                else {
                    // If the state is Modified then we don't want
                    // to modify the CreatedAt and CreatedBy properties
                    // so we set their state as IsModified to false
                    Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                }

                // In any case we always want to set the properties
                // ModifiedAt and ModifiedBy
                ((AuditableEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }
    }
}
