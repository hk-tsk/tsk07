namespace DAEF
{
    using DACommon.Entities;
    using DAEFC.EntityConfigurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    public class LearningSiteDBContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }

        public LearningSiteDBContext(DbContextOptions<LearningSiteDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
            //base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EntityTypeConfiguration<Course>());
            modelBuilder.ApplyConfiguration(new EntityTypeConfiguration<Category>());
            modelBuilder.ApplyConfiguration(new EntityTypeConfiguration<Content>());
        }
    }
    /*
     dotnet ef migrations add InitialMigration --project=DAEFC //////-s ../Web/
     
        // To undo this action, use 'ef migrations remove'
        dotnet ef migrations remove --project=DAEFC

dotnet ef database update --project=DAEFC -s ../Web/

-s stands for startup project and ../Web/ is the location of my web/startup project.
     * */
    public class LearningSiteDBContextDesignFactory : IDesignTimeDbContextFactory<LearningSiteDBContext>
    {
        public LearningSiteDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LearningSiteDBContext>()
             .UseSqlServer("Server=.;Initial Catalog=LSDA;Integrated Security=true")
             ;

            return new LearningSiteDBContext(optionsBuilder.Options);
        }
    }
}