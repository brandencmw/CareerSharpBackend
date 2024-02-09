using Microsoft.EntityFrameworkCore;

namespace CareerSharp.Models
{
    public class JobListingContext : DbContext
    {
        public JobListingContext(DbContextOptions<JobListingContext> options) : base(options)
        {
        }

        public DbSet<JobListing> JobListings { get; set; } = null!;

    }
}
