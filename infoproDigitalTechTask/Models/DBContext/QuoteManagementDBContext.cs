using Microsoft.EntityFrameworkCore;

namespace infoproDigitalTechTask.Models.DBContext
{
    public class QuoteManagementDBContext : DbContext
    {
        public QuoteManagementDBContext(DbContextOptions<QuoteManagementDBContext> options) : base(options)
        {

        }

        public DbSet<Quote> Quote { get; set; } = null!;
        public DbSet<Job> Job { get; set; } = null!;
        public DbSet<Part> Part { get; set; } = null!;
        public DbSet<MechanicalPart> MechanicalPart { get; set; } = null!;
        public DbSet<FluidPart> FluidPart { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>()
                .HasMany(e => e.Jobs)
                .WithOne(e => e.Quote)
                .HasForeignKey(e => e.QuoteId)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .HasMany(e => e.Parts)
                .WithOne(e => e.Job)
                .HasForeignKey(e => e.JobId)
            .IsRequired();

            modelBuilder.Entity<Part>()
                .HasDiscriminator(x => x.PartType)
                .HasValue<MechanicalPart>("Mechanical")
                .HasValue<FluidPart>("Fluid");
        }        
    }
}
