using Microsoft.EntityFrameworkCore;
using EmailService.Model;
using EmailService.Configuration;

public class MyDbContext : DbContext
{
    public DbSet<Jobs> Jobs { get; set; }
    public DbSet<Mail> Mail { get; set; }
    public DbSet<MailConfiguration> MailConfiguration { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Globals.config.ConnectionString);
    }
}
