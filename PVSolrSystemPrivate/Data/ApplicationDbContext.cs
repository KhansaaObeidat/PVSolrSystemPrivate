using Microsoft.EntityFrameworkCore;
using PVSolrSystemPrivate.Models;

namespace PVSolrSystemPrivate.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base
            (options)
        {
        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<CustomerFile> CustomerFiles { get; set; }
    }
}
