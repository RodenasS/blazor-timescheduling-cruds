using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using TimeScheduling.Data.Models;

namespace TimeScheduling.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    
    public DbSet<EmployeeJob> EmployeeJobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeJob>()
            .HasKey(ej => new { ej.EmployeeId, ej.JobId });

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Job> Jobs { get; set; }
}