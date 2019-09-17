using Microsoft.EntityFrameworkCore;
using DACommon.Entities;

namespace DAEF
{
    public class LearningSiteDBContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }

        public LearningSiteDBContext(DbContextOptions options) : base(options)
        {
            //this.Courses = this.Set<Course>();
            //this.Categories = this.Set<Category>();
            //this.Contents = this.Set<Content>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
            base.OnModelCreating(modelBuilder);

        }

    }
}