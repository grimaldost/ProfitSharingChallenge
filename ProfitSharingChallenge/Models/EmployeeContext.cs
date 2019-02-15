using Microsoft.EntityFrameworkCore;

namespace ProfitSharingChallenge.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
                : base(options)
        {
        }
        public DbSet<EmployeeItem> employees { get; set; }
        public DbSet<ParticipationItem> participation { get; set; }
    }
}
