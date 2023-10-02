using CRMConnect.CRMConnect.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRMConnect.CRMConnect.Data.DataAccess
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Account> Accounts { get; set; }

}
