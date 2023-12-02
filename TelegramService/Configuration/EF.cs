using Microsoft.EntityFrameworkCore;
using TelegramService.Configuration;

public class MyDbContext : DbContext
{
    public DbSet<Jobs> Jobs { get; set; }
    public DbSet<TelegramMessage> TelegramMessages { get; set; }
    public DbSet<TelegramConfiguration> TelegramConfiguration { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Globals.config.ConnectionString);
    }
}
