using System;
using Microsoft.EntityFrameworkCore;
using sunny_dn_01.Domains;

namespace sunny_dn_01.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Voting> Votings { get; set; }
    }
}
