using CRMConnect.CRMConnect.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Task = CRMConnect.CRMConnect.Core.Entities.Task;

namespace CRMConnect.CRMConnect.Data.DataAccess;
public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Opportunity> Opportunity { get; set; }
    public DbSet<Deal> Deals { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Note> Notes { get; set; }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
